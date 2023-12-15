using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ClClient
    {
        public ClClient() { }
        public ClClient(int id, string nameclient, string email, string adress, string login, string password )
        {
            this.ID = id;
            this.NameClient = nameclient;
            this.Email = email;
            this.Adress = adress;
            this.Login = login;
            this.Password = password;


        }
        public int ID { get; set; }
        public string NameClient { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
