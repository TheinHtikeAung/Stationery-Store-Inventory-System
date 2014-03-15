using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationLayer.Utils;
namespace ApplicationLayer.Controllers
{
    public class PurchaseOrderController : MainController
    {
        public Message actionCreateNewReOrderWithDefaultValue(int stationery_id, int supplier_id) {

            Stationery stationery = new StationeryController().actionGetStationeryByID(stationery_id);
            Supplier supplier = new SupplierController().actionGetSuppierByID(supplier_id);

            PurchaseOrder order = new PurchaseOrder();
            order.po_stationery = stationery.id;
            order.po_quantity_in_hand = stationery.quantity_in_stock - stationery.pending_quantity_to_distribute;
            order.po_reorder_date = DateTime.Now;
            order.po_reorder_level = stationery.reorder_level;
            order.po_reordered_quantity = stationery.reorder_quantity;
            order.po_supplier = supplier.id;

            try
            {
                data.PurchaseOrders.AddObject(order);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public List<PurchaseOrderView> actionGetWaitingPurchaseOrderViews() {
            return data.PurchaseOrderViews.Where(o => o.po_status == 0).ToList();
        }

        public Message actionPurchaseOrderArrive(int po_id) {
            try
            {
                PurchaseOrder po = data.PurchaseOrders.Where(o => o.id == po_id).SingleOrDefault();
                Stationery stationery = data.Stationeries.Where(o => o.id == po.po_stationery).SingleOrDefault();
                Supplier supplier = data.Suppliers.Where(o => o.id == po.po_supplier).SingleOrDefault();

                stationery.quantity_in_stock += po.po_reordered_quantity;
                po.po_status = 1;
                data.SaveChanges();
                StockTransactionController transactionController = new StockTransactionController();
                transactionController.actionCreateStockTransaction(stationery.id, stationery.quantity_in_stock,
                                                                "Supplier - " + supplier.supplier_name, "+" + po.po_reordered_quantity);
                return new Message(true);
            }
            catch (Exception e) {
                return new Message(false, Helper.getExceptionMessage(e));
            }
            
        }
    }
}
