using BusinessObject;
using DataAccess;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SalesWPFApp.User.Pages
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        Member _authorize;
        MemberDAO _memberDAO;
        public Profile(Member authorize, MemberDAO memDao)
        {
            InitializeComponent();
            _memberDAO = memDao;
            
            _authorize = new Member()
            {
                City = authorize.City,
                CompanyName = authorize.CompanyName,
                Country = authorize.Country,
                Email = authorize.Email,
                MemberId = authorize.MemberId,
                Password = authorize.Password
            };
            //_authorize = authorize;
            onLoad();
        }
        private void onLoad()
        {
            if (Authorize())
            {
                DataContext = _authorize;
            }
        }
        private bool Authorize()
        {
            if (LoginForm.loginUser != null && _authorize.MemberId != LoginForm.loginUser.MemberId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                try
                {
                    if (ExternalFunc.Instance.DialogBox(_memberDAO.Confirm(_authorize), "Confirm"))
                    {
                        _memberDAO.Update(_authorize.MemberId, _authorize);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
    }
}
