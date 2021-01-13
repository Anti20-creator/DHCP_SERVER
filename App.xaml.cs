using DHCP.DHCPModel;
using DHCP.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DHCP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        DHCPViewModel _viewModel;
        MainWindow _view;
        Model _model;
        DispatcherTimer _timer;

        public App()
        {
            this.Startup += Startup_Event;
        }

        private void Startup_Event(object sender, StartupEventArgs e)
        {
            _model = new Model();
            _viewModel = new DHCPViewModel(_model);
            _view = new MainWindow();
            _view.DataContext = _viewModel;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;
            _timer.Start();

            _view.Show();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _viewModel.update();
        }
    }
}
