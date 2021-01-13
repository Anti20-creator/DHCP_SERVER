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
    /// Interaction logic for Reserv.xaml
    /// </summary>
    public partial class Reserv : Window
    {
        public EventHandler<String> MACSent;
        public Reserv()
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
            if (OnlyHexInString(ResponseText) && ResponseText.Length == 12)
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
            if (MACSent != null)
            {
                MACSent(this, mac);
            }
        }
    }
}
