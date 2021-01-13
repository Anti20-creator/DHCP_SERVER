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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public EventHandler<int> timeSent;
        public Settings(int x)
        {
            InitializeComponent();
            time.Text = x.ToString();
        }

        public string ResponseText
        {
            get { return time.Text; }
            set { time.Text = value; }
        }

        public bool OnlyNums(string test)
        {
            return !System.Text.RegularExpressions.Regex.IsMatch(test, "[^0-9]");
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (OnlyNums(ResponseText) && Convert.ToInt32(ResponseText) > 0)
            {
                OnTimeSent(ResponseText);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("It isn't a valid time!");
            }
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
        private void OnTimeSent(String time)
        {
            if (timeSent != null)
            {
                timeSent(this, Convert.ToInt32(time));
            }
        }

    }
}
