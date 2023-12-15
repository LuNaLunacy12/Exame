using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ClProduct
    {
        public ClProduct() { }
        public ClProduct(int id, string nameproduct, string color, int manufacture)
        {
            this.ID = id;
            this.NameProduct = nameproduct;
            this.Color = color;
            this.Manufacture = manufacture;

        }
        public int ID { get; set; }
        public string NameProduct { get; set; }
        public string Color { get; set; }
        public int Manufacture { get; set; }
    }
}
