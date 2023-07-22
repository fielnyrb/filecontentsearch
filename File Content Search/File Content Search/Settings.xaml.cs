using System.Linq;
using System.Windows;
using File_Content_Search.Entities;
using File_Content_Search.Implementations;
using Microsoft.PowerShell.Telemetry;

namespace File_Content_Search
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        PortNumberSetting portNumberSetting;

        public Settings()
        {
            InitializeComponent();

            portNumberSetting = new PortNumberSetting();
            PortNumberTextBox.Text = portNumberSetting.GetPortNumber();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string portNumber = PortNumberTextBox.Text;

            portNumberSetting.UpdatePortNumber(portNumber);
        }
    }
}
