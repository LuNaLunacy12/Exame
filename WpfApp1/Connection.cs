using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp1
{
    public class Connection
    {
        public static NpgsqlConnection connection;

        public static void Connect(string host, string port, string user, string pass, string database)
        {
            string cs = string.Format("Server = {0}; Port = {1}; User Id = {2}; Password = {3}; Database = {4}", host, port, user, pass, database);

            connection = new NpgsqlConnection(cs);
            connection.Open();
        }


        public static ObservableCollection<ClProduct> products { get; set; } = new ObservableCollection<ClProduct>();
        public static ObservableCollection<ClManufacture> manufactures { get; set; } = new ObservableCollection<ClManufacture>();
        public static ObservableCollection<ClOrder> orders { get; set; } = new ObservableCollection<ClOrder> { };
        public static ObservableCollection<ClClient> users { get; set; } = new ObservableCollection<ClClient> { };

        public static ClClient user { get; set; }
        public static NpgsqlCommand GetCommand(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = sql;
            return command;
        }

        public static void SelectTableProduct()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"ID\",\"NameProduct\",\"Color\",\"Manufacture\" FROM \"Product\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    products.Add(new ClProduct(result.GetInt32(0), result.GetString(1), result.GetString(2), result.GetInt32(3)));
                }

            }
            result.Close();
        }

        public static void SelectTableManufacture()
        {
            NpgsqlCommand cmd = GetCommand("Select \"ID\",\"NameManufacture\", \"Adress\"  from \"Manufacture\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    manufactures.Add(new ClManufacture(result.GetInt32(0), result.GetString(1), result.GetString(2)));
                }

            }
            result.Close();
        }

        public static void SelectTableOrder()
        {
            NpgsqlCommand cmd = GetCommand("Select \"ID\", \"Product\", \"Quantity\", \"Client\" from \"Order\" ");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    orders.Add(new ClOrder(result.GetInt32(0), result.GetInt32(1), result.GetInt32(2), result.GetInt32(3)));
                }

            }
            result.Close();
        }
       
        public static void SelectTableUsers()
        {
            NpgsqlCommand cmd = GetCommand("Select \"ID\", \"NameClient\", \"Email\", \"Adress\",  \"Login\",  \"Password\" from \"Client\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    users.Add(new ClClient(result.GetInt32(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetString(4), result.GetString(5)));
                }

            }
            result.Close();
        }
       
    }

}
    