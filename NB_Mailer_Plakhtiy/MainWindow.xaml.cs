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
        public static Logger nLogger = LogManager.GetCurrentClassLogger();

        // TODO: STATIC CLASS FOR STATIC FIELDS FOR SETTINGS

        public static string rootDir;

        public static int timerHrs = 0, timerMin = 0, timerSec = 10;

        public MainWindow()
        {
            nLogger.Trace("Ver.Net: {0}", Environment.Version.ToString());

            InitializeComponent();
#if DEBUG
            string debugOrRelease = "D E B U G";
#else
            string debugOrRelease = "R E L E A S E";
#endif
            MessageBoxResult mbr = MessageBox.Show(debugOrRelease + " !!! - version." + Environment.NewLine +
            Environment.NewLine + "Close This App.?", "WARNING!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (mbr == MessageBoxResult.Yes) Application.Current.Shutdown();

            // T I M E R !!!!!!!!!

            labelForTimer.Content = "Auto Start Every: " + timerHrs + " Hrs. " + timerMin + " Min. " + timerSec + " Sec.";

            DispatcherTimer dispchTimer = new DispatcherTimer();
            dispchTimer.Interval = new TimeSpan(timerHrs, timerMin, timerSec);
            dispchTimer.Tick += DispchTimer_Tick;
            dispchTimer.Start();
        }

        private void DispchTimer_Tick(object sender, EventArgs e)
        {
            StartJob();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            StartJob();
        }

        // TODO: MUST BE REFACTORED!!!!!!!
        private String GetRootDirFromSettsFile()
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
                nLogger.Error("GetRootDirFromSettsFile() - " + exc.Message);
                MessageBox.Show("GetRootDirFromSettsFile() - " + exc.ToString());
            }
#if DEBUG
            return stringsSetts[0];
#else
            return stringsSetts[1];
#endif
        }


        // USEFUL CODESNIPPET :)
        //
        // try{ }
        // catch (Exception exc) { 
        //     nLogger.Error(currentMethodName - " + exc.Message);
        //     MessageBox.Show(currentMethodName - " + exc.ToString());
        // }



        // START THE NAIN JOB :
        private void StartJob()
        {
            try
            {
                rootDir = GetRootDirFromSettsFile();
#if !DEBUG
                // RUN MAIL3.BAT & TCPFOSS :
                Process.Start(rootDir + "\\MAIL3.BAT").WaitForExit();
#endif
                //  A F T E R  23:00 !!!

                if (DateTime.Now.Hour > 22)
                {
                    // TODO : CHECK IF BKP NOT EXISTS & CREATE IT 
                    // TODO : CHECK IF BKP NOT EXISTS & CREATE IT 

                    // TODO: IMLEMENT TWO BACKUPERS !!!!!
                    // TODO: IMLEMENT TWO BACKUPERS !!!!!

                    // Application.Exit();
                }
                else
                {
                    // I M P O R T A N T   B E F O R E !!!!!!!!!!
                    // I M P O R T A N T   B E F O R E !!!!!!!!!!

                    // PrepareFilesForSendToBanx(); !!!!!!!!!!!!!!

                    // IF OUTGOING FILES EXISTS - BKP & RENAME !

                    Process.Start(rootDir + "\\MAIL3.bat"); // + TCPFOSS INSIDE !!!

                    if (timerMin < 1)
                    {
                        nLogger.Warn("Timer is set for less then 1 minute!!!");
                    }
                    else
                    {
                        if ((DateTime.Now.Hour == 10 | DateTime.Now.Hour == 14) && DateTime.Now.Minute < ((timerMin * 2) - 1))
                        {
                            nLogger.Warn(rootDir + "\\CORRSPR3_aaa.bat - Starting...");
                            Process.Start(rootDir + "\\CORRSPR3_aaa.bat");
                        }
                    }

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

                }
#if !DEBUG
                // RUN MAIL3.BAT & TCPFOSS :
                Process.Start(rootDir + "\\MAIL3.BAT").WaitForExit();
#endif
            }
            catch (Exception exc)
            {
                nLogger.Error("StartJob() - " + exc.Message);
                MessageBox.Show("StartJob() - " + exc.ToString());
            }
        }
    }
}
