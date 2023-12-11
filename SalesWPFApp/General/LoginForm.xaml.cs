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
using Microsoft.Extensions.DependencyInjection;
using SalesWPFApp.Helper;
using BusinessObject;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        IServiceProvider _service;
        MemberDAO _members;
        public static Member? loginUser;
        public LoginForm(IServiceProvider service)
        {
            InitializeComponent();
            //_member = member;
            _service = service;
            _members = _service.GetRequiredService<MemberDAO>();
            loginUser = null;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var form_register = new RegisterForm(_members, new BusinessObject.Member());
            form_register.ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure quitting application?",
                    "Quitting",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Password;
            
            if(email.Length == 0)
            {
                MessageBox.Show("Please enter email!");
                txtEmail.Focus();
            } else if(password.Length == 0)
            {
                MessageBox.Show("Please enter password!");
                txtPassword.Focus();
            } else
            {
                //login
                loginUser = _members.Login(email, password);
                if(loginUser == null)
                {
                    MessageBox.Show("Credentials not found!");
                } else if(loginUser.Role == BusinessObject.Enum.Role.User)
                {
                    //is user
                    this.Hide();
                    new SalesApplication(loginUser, _service).ShowDialog();
                    loginUser = null;
                    this.Show();
                } else
                {
                    //is admin
                    this.Hide();
                    new SalesManagement(_service, loginUser).ShowDialog();
                    loginUser = null;
                    this.Show();
                }
                //var admin = ExternalFunc.Instance.GetAdminAccount();
                //if(email == admin.Email && password == admin.Password)
                //{
                //    //is admin
                //    this.Hide();
                //    loginUser = admin;
                //    new SalesManagement(_service, loginUser).ShowDialog();
                //    loginUser = null;
                //    this.Show();
                //} else
                //{
                //    loginUser = _members.Login(email,password);
                //    if(loginUser != null)
                //    {
                //        //is user
                //        this.Hide();
                //        new SalesApplication(loginUser, _service).ShowDialog();
                //        loginUser = null;
                //        this.Show();
                //    } else
                //    {
                //        //anonymous
                //        MessageBox.Show("Wrong email or password!", "Error");
                //    }

                //}
            }

        }
    }
}
