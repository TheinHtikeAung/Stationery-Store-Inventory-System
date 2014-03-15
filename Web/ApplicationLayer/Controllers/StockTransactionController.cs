using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.Controllers
{
    public class StockTransactionController : MainController
    {
        public void actionCreateStockTransaction(int stationery, int balance, String description, String quantity)
        {
            StockTransaction st=new StockTransaction();
            st.stationery=stationery;
            st.balance = balance.ToString();
            st.description = description;
            st.quantity = quantity;
            st.datetime_inserted = DateTime.Now;

            data.StockTransactions.AddObject(st);
            data.SaveChanges();
        }

        public List<StockTransaction> actionGetStockTransactionByStationeryId(int stationery) {
            
            List<StockTransaction> transactions = data.StockTransactions.Where(o => o.stationery == stationery).OrderByDescending(o=> o.id).ToList();
            return transactions;
        }
    }
}
