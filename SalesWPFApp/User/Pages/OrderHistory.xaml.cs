using BusinessObject;
using DataAccess;
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

namespace SalesWPFApp.User.Pages
{
    /// <summary>
    /// Interaction logic for OrderHistory.xaml
    /// </summary>
    public partial class OrderHistory : Page
    {
        Member _authorize;
        OrderDetailDAO _ordDetailDao;
        OrderDAO _ordDao;

        private double total;
        public OrderHistory(Member authorize, OrderDAO ordDao, OrderDetailDAO ordDetailDao)
        {
            InitializeComponent();
            _authorize = authorize;
            _ordDao = ordDao;
            _ordDetailDao = ordDetailDao;
            Load();
            
        }

        private bool Authorize()
        {
            if (_authorize != LoginForm.loginUser)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Load()
        {
            var list = _ordDao.Get(order => order.MemberId == _authorize.MemberId, includeProperties: "Member");
            lvOrders.ItemsSource = list;
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                Load();
            }
        }
        private Order Selected()
        {
            return (Order)lvOrders.SelectedItem;
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
            lvOrderDetails.ItemsSource = list_details;
            total = 0;
            foreach (var item in list_details)
            {
                total += CalculateTotal(item);
            }
            txtTotal.Text = total.ToString();
        }
    }
}
