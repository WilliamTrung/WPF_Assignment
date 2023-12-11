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
using System.Windows.Shapes;

namespace SalesWPFApp.Admin.ModifyItem
{
    /// <summary>
    /// Interaction logic for ModifyProduct.xaml
    /// </summary>
    public partial class ModifyProduct : Window
    {
        Product _product;
        ProductDAO _proDao;
        public ModifyProduct(Product product, ProductDAO proDao)
        {
            InitializeComponent();
            _product = product;
            _proDao = proDao;
            this.DataContext = _product;
        }
        private void Add()
        {
            _proDao.Add(_product);
        }
        private void Update()
        {
            _proDao.Update(_product.ProductId, _product);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_product.ProductId == 0)
                {
                    Add();
                }
                else
                {
                    Update();
                }
                this.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
