using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ClOrder
    {
        public ClOrder() { }
        public ClOrder(int id, int product, int quantity, int client)
        {
            this.ID = id;
            this.Product = product;
            this.Quantity = quantity;
            this.Client = client;


        }
        public int ID { get; set; }
        public int Product { get; set; }
        public int Quantity { get; set; }
        public int Client { get; set; }

    }
}
