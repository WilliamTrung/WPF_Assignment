using BusinessObject;
using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using SalesWPFApp.Management;
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
    /// Interaction logic for SalesManagement.xaml
    /// </summary>
    public partial class SalesManagement : Window
    {
        IServiceProvider _service;
        ProductManagement productManagement;
        OrderManagement orderManagement;
        MemberManagement memberManagement;
        Member _authorize;
        public SalesManagement(IServiceProvider service, Member authorize)
        {
            InitializeComponent();
            _service = service;
            _authorize = authorize;
            var proDao = service.GetRequiredService<ProductDAO>();
            var ordDao = service.GetRequiredService<OrderDAO>();
            var ordDetailDao = service.GetRequiredService<OrderDetailDAO>();
            var memDao = service.GetRequiredService<MemberDAO>();
            productManagement = new ProductManagement(_authorize,proDao);
            orderManagement = new OrderManagement(_authorize, ordDao, ordDetailDao);
            memberManagement = new MemberManagement(_authorize,memDao);

            Management.Content = productManagement;
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            Management.Content = productManagement;
        }

        private void btnMember_Click(object sender, RoutedEventArgs e)
        {
            Management.Content = memberManagement;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            Management.Content = orderManagement;
        }
    }
}
