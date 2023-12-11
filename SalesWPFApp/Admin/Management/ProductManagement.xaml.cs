using BusinessObject;
using DataAccess;
using SalesWPFApp.Admin.ModifyItem;
using SalesWPFApp.Helper;
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

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for ProductManagement.xaml
    /// </summary>
    public partial class ProductManagement : Page
    {
        ProductDAO _productDAO;
        Member _authorize;
        private bool Authorize()
        {
            if (_authorize != LoginForm.loginUser)
            {
                return false;
            }
            else if (_authorize.Role != BusinessObject.Enum.Role.User)
            {
                return true;
            }
            return false;
        }
        public ProductManagement(Member authorize, ProductDAO productDAO)
        {
            InitializeComponent();
            _authorize = authorize;
            _productDAO = productDAO;
            LoadProducts(_productDAO.Get());
        }
        private void LoadProducts(IEnumerable<Product> list)
        {
            lvProducts.ItemsSource = list;
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                LoadProducts(_productDAO.Get());
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                var modify = new ModifyProduct(new Product(), _productDAO);
                modify.ShowDialog();
                LoadProducts(_productDAO.Get());
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                var selected = Selected();
                if(selected != null)
                {
                    var modify = new ModifyProduct(selected, _productDAO);
                    modify.ShowDialog();
                    LoadProducts(_productDAO.Get());
                }
            }
        }
        private Product? Selected()
        {
            try
            {
                int id = Int32.Parse(txtId.Text);
                int categoryid = Int32.Parse(txtCategory.Text);
                decimal price = Convert.ToDecimal(txtPrice.Text);
                int instock = Convert.ToInt32(txtInStock.Text);
                var product = new Product()
                {
                    CategoryId = categoryid,
                    ProductId = id,
                    ProductName = txtName.Text,
                    UnitsInStock = instock,
                    UnitPrice = price,
                    Weight = txtWeight.Text
                };
                return product;
            } catch
            {
                MessageBox.Show("Invalid product values!", "Error");
                return null;
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                var selected = Selected();
                if(selected != null)
                {
                    if (ExternalFunc.Instance.DialogBox("Delete this product?\n" + _productDAO.Confirm(selected), "Warning"))
                    {
                        if (ExternalFunc.Instance.DialogBox("Are you sure deleting this product?", "Warning"))
                        {
                            _productDAO.Delete(selected.ProductId);
                        }
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = -1;
                var _id = txtSearchId.Text;
                if(_id.Trim() != "")
                {
                    id = Convert.ToInt32(_id);
                }
                var name = txtSearchName.Text;
                IEnumerable<Product> list;
                if(id > -1)
                {
                    //search id and name
                    list = _productDAO.Get(product => product.ProductId == id && product.ProductName.ToLower().Contains(name.Trim().ToLower()));
                } else
                {
                    //search name
                    list = _productDAO.Get(product => product.ProductName.ToLower().Contains(name.Trim().ToLower()));
                }
                LoadProducts(list);
            } catch
            {
                MessageBox.Show("An Error has occured!");
            }
        }

        private void btnOrderPrice_Click(object sender, RoutedEventArgs e)
        {
            var list_current = lvProducts.Items.Cast<Product>().ToList();
            var list = list_current.OrderBy(p => p.UnitPrice).AsEnumerable<Product>();
            LoadProducts(list);
        }

        private void btnOrderStock_Click(object sender, RoutedEventArgs e)
        {
            var list_current = lvProducts.Items.Cast<Product>().ToList();
            var list = list_current.OrderBy(p => p.UnitsInStock).AsEnumerable<Product>();
            LoadProducts(list_current);
        }
    }
}
