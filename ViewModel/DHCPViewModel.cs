using DHCP.DHCPModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace DHCP.ViewModel
{
    public class DHCPViewModel : ViewModelBase
    {
        private Model _model;
        public DelegateCommand OpenReq { get; private set; }
        public DelegateCommand OpenReserv { get; private set; }
        public DelegateCommand OpenSettings { get; private set; }
        public DelegateCommand DeleteReservations { get; private set; }
        public ReqIP ReqIPWindow;
        public Reserv ReservIPWindow;
        public Settings SettingsWindow;
        //public ObservableCollection<string> leasesList { get; set; }
        public ObservableCollection<Lease> leasesList { get; set; }
        public ObservableCollection<Reservation> reservationList { get; set; }

        public DHCPViewModel(Model m)
        {
            _model = m;
            ReqIPWindow = new ReqIP();
            ReservIPWindow = new Reserv();
            SettingsWindow = new Settings(_model.getTimeout());

            ReqIPWindow.MACSent += CatchMacAddress;
            ReservIPWindow.MACSent += CatchMacAddressForReserv;
            SettingsWindow.timeSent += CatchTime;
            
            OpenReq = new DelegateCommand(
                (_) => popupDialog()
            );
            OpenReserv = new DelegateCommand(
                (_) => popupDialogReserv()
            );
            DeleteReservations = new DelegateCommand(
                (_) => _model.deleteReservations()
            );
            OpenSettings = new DelegateCommand(
                (_) => openSettingsWindow()
            );

            leasesList = new ObservableCollection<Lease>();
            reservationList = new ObservableCollection<Reservation>();
        }

        private void openSettingsWindow()
        {
            SettingsWindow.ShowDialog();
            SettingsWindow = new Settings(_model.getTimeout());
            SettingsWindow.timeSent += CatchTime;
        }

        private void CatchTime(object sender, int e)
        {
            _model.setTimeout(e);
        }

        private void CatchMacAddressForReserv(object sender, string mac)
        {
            _model.reservMAC(mac);
            OnPropertyChanged("leasesList");
            OnPropertyChanged("reservationList");
        }

        public void popupDialog()
        {
            ReqIPWindow.ShowDialog();
            ReqIPWindow = new ReqIP();
            ReqIPWindow.MACSent += CatchMacAddress;
        }

        public void popupDialogReserv()
        {
            ReservIPWindow.ShowDialog();
            ReservIPWindow = new Reserv();
            ReservIPWindow.MACSent += CatchMacAddressForReserv;
        }

        public void CatchMacAddress(object sender, ClientEvent args)
        {
            //MessageBox.Show(mac);
            _model.pushReleaseMAC(args);
            OnPropertyChanged("leasesList");
            OnPropertyChanged("reservationList");
        }

        public void update()
        {
            leasesList.Clear();
            reservationList.Clear();

            _model.deleteOldOnes();

            var list = _model.GetLeases();
            var list2 = _model.GetReservations();
            foreach (var item in list)
            {
                leasesList.Add(item);
            }
            foreach (var item in list2)
            {
                reservationList.Add(item);
            }
            OnPropertyChanged("leasesList");
            OnPropertyChanged("reservationList");
        }
    }
}
