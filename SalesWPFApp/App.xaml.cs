using BusinessObject;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            Configuration(services);
            _serviceProvider = services.BuildServiceProvider(); ;
        }
        private void OnStartUp(object sender, StartupEventArgs e)
        {
            
            //main work
            var form_login = _serviceProvider.GetService<LoginForm>();
            if(form_login != null)
            {
                form_login.Show();
            }
            
            /*
            //for test admin
            var form_admin = _serviceProvider.GetService<SalesManagement>();
            if(form_admin != null)
            {
                form_admin.Show();
            }
            */
            //for test user
            /*
            var form_user = _serviceProvider.GetService<SalesApplication>();
            if (form_user != null)
            {
                form_user.Show();
            }
            */
        }
        private void Configuration(IServiceCollection services)
        {
            var connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
            services.AddDbContext<SaleWPFAppContext>(options => options.UseSqlServer(connection));
            services.AddScoped<MemberDAO>(service =>
            {
                var _context = service.GetRequiredService<SaleWPFAppContext>();
                return new MemberDAO(_context);
            });
            services.AddSingleton<ProductDAO>(service =>
            {
                var _context = service.GetRequiredService<SaleWPFAppContext>();
                return new ProductDAO(_context);
            });
            services.AddSingleton<OrderDAO>(service =>
            {
                var _context = service.GetRequiredService<SaleWPFAppContext>();
                return new OrderDAO(_context);
            });
            services.AddSingleton<OrderDetailDAO>(service =>
            {
                var _context = service.GetRequiredService<SaleWPFAppContext>();
                return new OrderDetailDAO(_context);
            });
            services.AddSingleton<LoginForm>();
            services.AddSingleton<SalesManagement>();
            services.AddSingleton<SalesApplication>();
        }
    }
}
