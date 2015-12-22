using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NB_Mailer_Plakhtiy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            string debugOrRelease = "D E B U G";
#else
            string debugOrRelease = "R E L E A S E";
#endif
            MessageBoxResult mbr = MessageBox.Show(debugOrRelease + " !!! - version started." + Environment.NewLine +
            Environment.NewLine + "Close App.?", "WARNING!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (mbr == MessageBoxResult.Yes) Application.Current.Shutdown();

            StartJob();
        }


        private void StartJob()
        {
            // throw new NotImplementedException();
        }
    }
}
