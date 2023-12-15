using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ClManufacture
    {
        public ClManufacture() { }
        public ClManufacture(int id, string namemanufacture, string adress)
        {
            this.ID = id;
            this.NameManufacture = namemanufacture;
            this.Adress = adress;


        }
        public int ID { get; set; }
        public string NameManufacture { get; set; }
        public string Adress { get; set; }
    }
}
