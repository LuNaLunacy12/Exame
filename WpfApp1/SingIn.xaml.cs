using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для SingIn.xaml
    /// </summary>
    public partial class SingIn : Page
    {
        public SingIn()
        {
            InitializeComponent();
        }

        private void bSingIn_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand cmd = Connection.GetCommand("Select \"ID\", \"NameClient\", \"Email\", \"Adress\",  \"Login\",  \"Password\" from \"Client\"" +
                    "WHERE \"Login\" = @log AND \"Password\" = @pass");
            cmd.Parameters.AddWithValue("@log", NpgsqlDbType.Varchar, tbLogin.Text.Trim());
            cmd.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, tbPassword.Text.Trim());
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    Connection.user = new ClClient(result.GetInt32(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetString(4), result.GetString(5));
                    
                }
                result.Close();
                NavigationService.Navigate(new ContentProducts());
            }
            else
            {
                MessageBox.Show("Проверьте правильность вводимых данных!");
            }
           
        }

    }
    
}
