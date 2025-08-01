using Microsoft.Extensions.DependencyInjection;
using PhoneLinesApp.Core.Interfaces;
using PhoneLinesApp.Data;
using PhoneLinesApp.Data.Repositories;
using PhoneLinesApp.Services;
using PhoneLinesApp.Services.Interfaces;
using PhoneLinesApp.UI.ViewModels;
using PhoneLinesApp.UI.Views;
using System.Windows;

namespace PhoneLinesApp.UI
{
    public partial class App : Application
    {
        private ServiceProvider _provider;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<IPhoneLineRepository, PhoneLineRepository>();
            services.AddScoped<IPhoneLineService, PhoneLineService>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<PhoneLineViewModel>();

            _provider = services.BuildServiceProvider();

            var mainVm = _provider.GetService<MainViewModel>();
            var window = new MainWindow { DataContext = mainVm };
            window.Show();
        }
    }
}
