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
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        MemberDAO _members;
        Member _member;
        private bool isUpdate;
        private List<BusinessObject.Enum.Role> roles;
        public RegisterForm(MemberDAO memberDAO, Member member)
        {
            InitializeComponent();
            _members = memberDAO;
            _member = member;
            roles = Enum.GetValues(typeof(BusinessObject.Enum.Role))
                    .Cast<BusinessObject.Enum.Role>()
                    .ToList();
            if (member.MemberId != 0)
            {
                isUpdate = true;
            } else
            {
                isUpdate = false;
            }
            this.DataContext = _member;
            Load();
        }

        private void Load()
        {
            if (isUpdate)
            {
                btnRegister.Content = "Update";
                this.Title = "Update Member";
                this.Height = 450;
                if(_member.Role == BusinessObject.Enum.Role.User)
                {
                    cmbRole.IsEnabled = false;
                } else
                {
                    //disable select value Role.User but others Role enum values
                    roles.Remove(BusinessObject.Enum.Role.User);
                    cmbRole.ItemsSource = roles;
                }
            }
            else
            {
                this.Height = 350;
                lbId.Visibility = Visibility.Collapsed;
                txtId.Visibility = Visibility.Collapsed;
                cmbRole.Visibility = Visibility.Collapsed;
                lbRole.Visibility = Visibility.Collapsed;
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ExternalFunc.Instance.DialogBox(_members.Confirm(_member), "Confirm"))
                {
                    if (isUpdate)
                    {
                        _members.Update(_member.MemberId, _member);
                    } else
                    {
                        _members.Add(_member);
                    }
                    this.Close();
                }
            } catch (Exception ex)
            {
                ExternalFunc.Instance.DialogBox(ex.Message, "Error");
            }
            
        }
    }
}
