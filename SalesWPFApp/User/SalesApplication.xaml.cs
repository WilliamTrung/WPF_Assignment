using BusinessObject;
using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using SalesWPFApp.User.Pages;
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
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for SalesApplication.xaml
    /// </summary>
    public partial class SalesApplication : Window
    {
        Profile _profile;
        Member _authorize;
        OrderHistory _orderHistory;
        public SalesApplication(Member authorize, IServiceProvider services)
        {
            InitializeComponent();
            MemberDAO memDao = services.GetRequiredService<MemberDAO>();
            OrderDAO ordDao = services.GetRequiredService<OrderDAO>();
            OrderDetailDAO orderDetailDao = services.GetRequiredService<OrderDetailDAO>();
            _authorize = authorize;
            _profile = new Profile(_authorize, memDao);
            _orderHistory = new OrderHistory(_authorize, ordDao, orderDetailDao);

            userFrame.Content = _profile;
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            userFrame.Content = _profile;
        }

        private void btnOrderHistory_Click(object sender, RoutedEventArgs e)
        {
            userFrame.Content = _orderHistory;
        }
    }
}
