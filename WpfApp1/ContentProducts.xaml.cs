using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ContentProducts.xaml
    /// </summary>
    public partial class ContentProducts : Page
    {
        public ContentProducts()
        {
            InitializeComponent();

            lvBindingProduct();
            cbBindingManufacture();

            if (Connection.user != null)
            {
                LogIn.Content = Connection.user.NameClient;
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
             
        }

        public void lvBindingProduct()
        {
            Binding binding = new Binding
            { Source = Connection.products };
            lvProduct.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableProduct();

        }

        public void cbBindingManufacture()
        {
            Binding binding = new Binding
            { Source = Connection.manufactures };
            tbManufacture.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableManufacture();

        }

        private void Filter()
        {
            string searchString = tbSearch.Text.Trim();

            var view = CollectionViewSource.GetDefaultView(lvProduct.ItemsSource);
            view.Filter = new Predicate<object>(o =>
            {
                ClProduct product = o as ClProduct;
                if (product == null) { return false; }

                bool isVisible = true;
                if (searchString.Length > 0)
                {
                    isVisible = product.NameProduct.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                        product.Color.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1;
                }
                return isVisible;
            });
        }

        public void ClearingInformationElements()
        {
            lvProduct.SelectedItem = null;
            tbNameProduct.Clear();
            tbColor.Clear();
            tbManufacture.Text = null;
            
        }

        public void InsertProductInfo()
        {
          
            var nameProduct = tbNameProduct.Text.Trim();
            var color = tbColor.Text.Trim();

            ClManufacture manufacture = tbManufacture.SelectedItem as ClManufacture;
            
      
            NpgsqlCommand cmd = Connection.GetCommand("insert into \"Product\" (\"NameProduct\", \"Color\", \"Manufacture\")" +
                 "values (@nameProduct, @color, @manufacture) returning \"ID\"");
            cmd.Parameters.AddWithValue("@nameProduct", NpgsqlDbType.Varchar, nameProduct);
            cmd.Parameters.AddWithValue("@color", NpgsqlDbType.Varchar, color);
            cmd.Parameters.AddWithValue("@manufacture", NpgsqlDbType.Integer, manufacture.ID);
      
            var result = cmd.ExecuteScalar();

            if (result == null)
            {
                MessageBox.Show("Данные не добавлены");
            }
            else
            {
                MessageBox.Show("Данные добавлены");
            }
        }

        public void DeleteProductInfo()
        {

            ClProduct product = lvProduct.SelectedItem as ClProduct;


            NpgsqlCommand cmd = Connection.GetCommand("Delete FROM \"Product\" WHERE \"ID\" = @id");
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, product.ID);
            cmd.Parameters.AddWithValue("@nameProduct", NpgsqlDbType.Varchar, product.NameProduct);
            cmd.Parameters.AddWithValue("@color", NpgsqlDbType.Varchar, product.Color);
            cmd.Parameters.AddWithValue("@manufacture", NpgsqlDbType.Integer, product.Manufacture);

            var result = cmd.ExecuteNonQuery();

            if (result == 0)
            {
                MessageBox.Show("Данные не удалены");
            }
            else
            {
                MessageBox.Show("Данные удалены");
            }
        }

        public void ChangedProductInfo()
        {
            ClProduct product = lvProduct.SelectedItem as ClProduct;
            var nameProduct = tbNameProduct.Text.Trim();
            var color = tbColor.Text.Trim();

            ClManufacture manufacture = tbManufacture.SelectedItem as ClManufacture;


            NpgsqlCommand cmd = Connection.GetCommand("UPDATE \"Product\" SET \"NameProduct\" = @nameProduct, \"Color\" = @color, \"Manufacture\" = @manufacture WHERE \"ID\" = @id");
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, product.ID);
            cmd.Parameters.AddWithValue("@nameProduct", NpgsqlDbType.Varchar, nameProduct);
            cmd.Parameters.AddWithValue("@color", NpgsqlDbType.Varchar, color);
            cmd.Parameters.AddWithValue("@manufacture", NpgsqlDbType.Integer, manufacture.ID);

            var result = cmd.ExecuteNonQuery();

            if (result == 0)
            {
                MessageBox.Show("Данные не изменены");
            }
            else
            {
                MessageBox.Show("Данные изменены");
            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            InsertProductInfo();
            Connection.products.Clear();
            lvBindingProduct();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearingInformationElements();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            ChangedProductInfo();
            Connection.products.Clear();
            lvBindingProduct();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteProductInfo();
            Connection.products.Clear();
            lvBindingProduct();
        }

        private void lvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvProduct.SelectedItem != null)
            {
                tbNameProduct.Text = (lvProduct.SelectedItem as ClProduct).NameProduct.ToString();
                tbColor.Text = (lvProduct.SelectedItem as ClProduct).Color.ToString();
                tbManufacture.Text = (lvProduct.SelectedItem as ClProduct).Manufacture.ToString();
              
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
    }
}
