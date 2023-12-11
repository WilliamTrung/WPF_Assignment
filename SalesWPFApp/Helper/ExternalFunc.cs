using BusinessObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SalesWPFApp.Helper
{
    public class ExternalFunc
    {
        private static ExternalFunc? instance = null;
        private ExternalFunc() { }
        public static ExternalFunc Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExternalFunc();
                }
                return instance;
            }
        }      
        public bool DialogBox(string message, string title)
        {
            return MessageBox.Show(message, title,
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
}
