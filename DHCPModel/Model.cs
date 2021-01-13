using DHCP.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DHCP.DHCPModel
{
    public class Lease : ViewModelBase
    {
        public String ip_address { get; private set; }
        public String MAC_address{ get; private set; }
        public DateTime timeout{ get; private set; }
        public Lease(String ip, String mac, int time)
        {
            ip_address = ip;
            MAC_address = mac;
            timeout = DateTime.Now.AddSeconds(time);
        }
    }

    public struct Reservation
    {
        public String ip_address { get; private set; }
        public String MAC_address { get; private set; }
        public Reservation(String ip, String mac)
        {
            ip_address = ip;
            MAC_address = mac;
        }
    }
    public class Model
    {
        List<Lease> leases;
        List<Reservation> reservations;
        int timeout;
        public Model()
        {
            timeout = 20;
            leases = new List<Lease>();
            reservations = new List<Reservation>();
        }
        public void pushReleaseMAC(ClientEvent args)
        {
            string mac = args._mac;
            if(args._type == ClientEventType.REQUEST)
            {
                var reserved = reservations.FindAll(e => e.MAC_address == mac);

                if (reserved.Count == 1)
                {
                    leases.Add(
                        new Lease(reserved[0].ip_address, mac, timeout)
                    );
                }
                else
                {
                    if(leases.FindAll(l => l.MAC_address == mac).Count == 1)
                    {
                        MessageBox.Show("We gave already an IP to this MAC adress!");
                        return;
                    }
                    giveAvailableIP(mac);
                }
            }
            else
            {
                leases.RemoveAll(l => l.MAC_address == mac);
            }
        }
        private void giveAvailableIP(string mac)
        {
            for(int i = 10; i < 254; ++i)
            {
                string IP = "21.1.7." + i.ToString();
                if((reservations.FindAll(e => e.ip_address == IP).Count == 0))
                {
                    if(leases.FindAll(e => e.ip_address == IP).Count == 0)
                    {
                        leases.Add(
                            new Lease(IP, mac, timeout)
                        );
                        return;
                    }
                }
                
            }
            MessageBox.Show("No IP-s are currently available!");
        }
        public void setTimeout(int x)
        {
            timeout = x;
        }
        public int getTimeout()
        {
            return timeout;
        }
        public List<Reservation> GetReservations()
        {
            return reservations;
        }
        public List<Lease> GetLeases()
        {
            return leases;
        }

        public void deleteOldOnes()
        {
            leases.RemoveAll(e => e.timeout <= DateTime.Now);
        }

        public void reservMAC(string mac)
        {
            Lease selected = leases.Find(l => l.MAC_address == mac);
            if (selected == null) return;
            if (reservations.FindAll(r => r.MAC_address == mac).Count == 1) return;
            reservations.Add(
                new Reservation(selected.ip_address, selected.MAC_address)
            );
        }

        public void deleteReservations()
        {
            reservations.Clear();
        }
    }
}
