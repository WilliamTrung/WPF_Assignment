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

namespace SalesWPFApp.Management
{
    /// <summary>
    /// Interaction logic for MemberManagement.xaml
    /// </summary>
    public partial class MemberManagement : Page
    {
        Member _authorize;
        MemberDAO _memDao;
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
        public MemberManagement(Member authorize, MemberDAO memDao)
        {
            InitializeComponent();
            _authorize = authorize;
            _memDao = memDao;
            Load(_memDao.Get());
        }
        private void Load(IEnumerable<Member> members)
        {
            lvMembers.ItemsSource = members;
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                var list = _memDao.Get();
                Load(list);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                RegisterForm createForm = new RegisterForm(_memDao, new Member());
                createForm.ShowDialog();
                var list = _memDao.Get();
                Load(list);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                var selected = (Member)lvMembers.SelectedItem;
                if(selected != null)
                {
                    RegisterForm updateForm = new RegisterForm(_memDao, selected);
                    updateForm.ShowDialog();
                    var list = _memDao.Get();
                    Load(list);
                } else
                {
                    MessageBox.Show("No member selected!", "Error");
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Authorize())
            {
                if (Authorize())
                {
                    try
                    {
                        var selected = (Member)lvMembers.SelectedItem;
                        if (selected != null)
                        {
                            if (ExternalFunc.Instance.DialogBox("Delete this member?\n" + _memDao.Confirm(selected), "Warning"))
                            {
                                if (ExternalFunc.Instance.DialogBox("Are you sure deleting this member?", "Warning"))
                                {
                                    _memDao.Delete(selected.MemberId);
                                    Load(_memDao.Get());
                                }
                            }
                        }
                    } catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                    
                }
            }
        }
    }
}
