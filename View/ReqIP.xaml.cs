using DHCP.DHCPModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DHCP
{
    /// <summary>
    /// Interaction logic for ReqIP.xaml
    /// </summary>
    public partial class ReqIP : Window
    {
        public EventHandler<ClientEvent> MACSent;
        public String eventType;
        public ReqIP()
        {
            InitializeComponent();
        }

        public string ResponseText
        {
            get { return MAC.Text; }
            set { MAC.Text = value; }
        }

        public bool OnlyHexInString(string test)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(OnlyHexInString(ResponseText) && ResponseText.Length == 12)
            {
                OnMACSent(ResponseText);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("It isn't a valid MAC address!");
            }
        }
        private void OnMACSent(String mac)
        {
            if(MACSent != null)
            {
                MACSent(this, new ClientEvent(eventType, mac));
            }
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Add "using Windows.UI;" for Color and Colors.
            //MessageBox.Show(e.AddedItems[0].ToString());
            eventType = e.AddedItems[0].ToString();
            var splitted = eventType.Split(' ');
            eventType = splitted[splitted.Length - 1];
        }
    }
}
