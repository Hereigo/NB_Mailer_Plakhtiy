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
using NLog;
using System.Diagnostics;
using System.IO;
using System.Windows.Threading;

namespace NB_Mailer_Plakhtiy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // SET UP NLOG :
        public static Logger nLog = LogManager.GetCurrentClassLogger();

        public static string rootDir;

        public MainWindow()
        {
            nLog.Trace("Ver.Net: {0}", Environment.Version.ToString());

            InitializeComponent();

            String[] settingsStr = GetRootDirFromSettsFile();
#if DEBUG
            string debugOrRelease = "D E B U G";
            rootDir = settingsStr[0];
#else
            string debugOrRelease = "R E L E A S E";
            rootDir = settingsStr[1];
#endif
            MessageBoxResult mbr = MessageBox.Show(debugOrRelease + " !!! - version started." + Environment.NewLine +
            Environment.NewLine + "Close The App.?", "WARNING!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (mbr == MessageBoxResult.Yes)
                Application.Current.Shutdown();

            int timeH = 0;
            int timeM = 0;
            int timeS = 10;

            labelForTimer.Content = "Timer is Start Every: " + timeH + " Hrs. " + timeM + " Min. " + timeS + " Sec.";

            DispatcherTimer dispchTimer = new DispatcherTimer();
            dispchTimer.Interval = new TimeSpan(timeH, timeM, timeS);
            dispchTimer.Tick += DispchTimer_Tick;
            dispchTimer.Start();
        }

        private void DispchTimer_Tick(object sender, EventArgs e) { StartJob(); }

        private void button_Click(object sender, RoutedEventArgs e) { StartJob(); }

        private String[] GetRootDirFromSettsFile()
        {
            String[] stringsSetts = new string[2];
            try
            {
                StreamReader sr = new StreamReader("Git_Ignore_Strings_Settings.txt");
                stringsSetts[0] = sr.ReadLine();
                stringsSetts[1] = sr.ReadLine();
                sr.Close();
            }
            catch (Exception exc)
            {
                nLog.Error("GetRootDirFromSettsFile() - " + exc.Message);
                MessageBox.Show("GetRootDirFromSettsFile() - " + exc.ToString());
            }
            return stringsSetts;
        }

        // USEFUL CODESNIPPET :)
        //
        // try{ }
        // catch (Exception exc) { 
        //     nLog.Error(currentMethodName! + " - " + exc.Message);
        //     MessageBox.Show(currentMethodName! + " - " + exc.ToString());
        // }



        // START THE NAIN JOB :
        private void StartJob()
        {
            try
            {

                MessageBox.Show(rootDir);

                // RUN TCPFOSS :

                // IF OUTGOING FILES EXISTS - BKP & RENAME !

                // RUN MAIL3.BAT :

                // IF RECEIVE CORR. FOR SPRUSNBU RUN CORRSPR.BAT
                // WAIT !
                // UPGRADE SPRUSNBU$ IN DB :

                // IF TODAY INCOME DIR EXISTS :
                //      READ ALL ENVELOPES :
                //      FOREACH ENVELOPE :
                //          GET FULL INFO :
                //          CHECK SMTH...
                //          WRITE INTO DB :
                //          REPORT ! :

                // RUN MAIL3.BAT :
                // RUN TCPFOSS :
            }
            catch (Exception exc)
            {
                nLog.Error("StartJob() - " + exc.Message);
                MessageBox.Show("StartJob() - " + exc.ToString());
            }
        }


    }
}
