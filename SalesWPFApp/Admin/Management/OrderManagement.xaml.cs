using BusinessObject;
using DataAccess;
using SalesWPFApp.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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

namespace SalesWPFApp.Management
{
    /// <summary>
    /// Interaction logic for OrderManagement.xaml
    /// </summary>
    public partial class OrderManagement : Page
    {
        OrderDAO _ordDao;
        OrderDetailDAO _ordDetailDao;
        Member _authorize;

        private double total;
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
        public OrderManagement(Member authorize, OrderDAO ordDao, OrderDetailDAO ordDetailDao)
        {
            InitializeComponent();
            _authorize = authorize;
            _ordDao = ordDao;
            _ordDetailDao = ordDetailDao;
            Load();

        }
        private void Load()
        {
            var list = _ordDao.Get(includeProperties: "Member,OrderDetails");
            lvOrders.ItemsSource = list;
        }
        

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                Load();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                try
                {
                    throw new Exception("Not yet implemented!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                try
                {
                    throw new Exception("Not yet implemented!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
        private Order Selected()
        {
            return (Order)lvOrders.SelectedItem;
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                var selected = Selected();
                if (selected != null)
                {
                    if (ExternalFunc.Instance.DialogBox("Delete this order?", "Warning"))
                    {
                        if (ExternalFunc.Instance.DialogBox("Are you sure deleting this order?", "Warning"))
                        {
                            try
                            {
                                //delete order detail
                                _ordDetailDao.Delete(selected.OrderId);
                                //delete order
                                _ordDao.Delete(selected.OrderId);
                            } catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message,"Error");    
                            }
                            
                        }
                    }
                }
            }
        }
        private double CalculateTotal(OrderDetail detail)
        {
            double total = 0;
            double price = (double)detail.UnitPrice * detail.Quantity;
            price -= price * detail.Discount;
            total = price;
            return total;
        }
        private void lvOrderDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list_details = ((Order)lvOrders.SelectedItem).OrderDetails;
            lvOrderDetails.ItemsSource =list_details;
            total = 0;
            foreach (var item in list_details)
            {
                total += CalculateTotal(item);
            }
            txtTotal.Text = total.ToString();
        }
    }
}
