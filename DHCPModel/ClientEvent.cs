using System;
using System.Collections.Generic;
using System.Text;

namespace DHCP.DHCPModel
{
    public enum ClientEventType
    {
        REQUEST, RELEASE
    };
    public class ClientEvent : EventArgs
    {
        public ClientEventType _type;
        public string _mac;
        public ClientEvent(string type, string mac)
        {
            if(type == "REQUEST")
            {
                _type = ClientEventType.REQUEST;
            }
            else
            {
                _type = ClientEventType.RELEASE;
            }
            _mac = mac;
        }
    }
}
