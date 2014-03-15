using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.ViewModels
{
    class StockTransactionModel
    {
        public int id;
        public int stationery;
        public string balance;
        public string description;
        public string quantity;
        public DateTime datetime_inserted;
    }
}
