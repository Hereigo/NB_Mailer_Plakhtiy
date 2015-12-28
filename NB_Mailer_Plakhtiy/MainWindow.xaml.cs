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
using System.Reflection;

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

            string methodName = MethodInfo.GetCurrentMethod().Name;
            try
            {
                StreamReader sr = new StreamReader("Git_Ignore_Strings_Settings.txt");
                stringsSetts[0] = sr.ReadLine();
                stringsSetts[1] = sr.ReadLine();
                sr.Close();
            }
            catch (Exception exc)
            {
                nLogger.Error(methodName + "() - " + exc.Message);
                MessageBox.Show(methodName + "() - " + exc.ToString());
            }
#if DEBUG
            return stringsSetts[0];
#else
            return stringsSetts[1];
#endif
        }


        // - USEFUL CODESNIPPET :)
        //
        // string methodName = MethodInfo.GetCurrentMethod().Name;
        // try{ }
        // catch (Exception exc) { 
        //     nLogger.Error(methodName + "() - " + exc.Message);
        //     MessageBox.Show(methodName + "() - " + exc.ToString());
        // }



        // START THE NAIN JOB :
        private void StartJob()
        {
            string methodName = MethodInfo.GetCurrentMethod().Name;
            try
            {
                //  A L F A   T E S T I N G   O N L Y  !!!!
                //  A L F A   T E S T I N G   O N L Y  !!!!

                Git_Ignore_ALFA_TEST alfa = new Git_Ignore_ALFA_TEST();

                //  A L F A   T E S T I N G   O N L Y  !!!!
                //  A L F A   T E S T I N G   O N L Y  !!!!

                rootDir = GetRootDirFromSettsFile();
#if !DEBUG
                // RUN MAIL3.BAT & TCPFOSS :
                Process.Start(rootDir + "\\MAIL3.BAT").WaitForExit();
#endif
                //  A F T E R  23:00 !!!

                if (DateTime.Now.Hour > 22)
                {
                    // TODO: CALL EVENING LOG-UPLOADER FROM ALFA_TEST !!!
                    // TODO: CALL EVENING LOG-UPLOADER FROM ALFA_TEST !!!
                    alfa.AlfaTest_EveningLogUpload();

                    string todayBackUp = @"C:\NBUMAIL\USERD\Admin\ARH\" + DateTime.Now.ToString("Bkp_yyMMdd") + ".RAR";

                    // TODO : CHECK IF BKP NOT EXISTS & CREATE IT 
                    // TODO : CHECK IF BKP NOT EXISTS & CREATE IT 
                    if (!File.Exists(todayBackUp))
                    {
                        alfa.AlfaTest_TodayBkpCreate(todayBackUp);
                    }

                    // TODO: IMLEMENT TWO BACKUPERS !!!!!
                    // TODO: IMLEMENT TWO BACKUPERS !!!!!


                    // TODO: T E M P O R A R Y !!!!!!!!!!!!
                    // TODO: T E M P O R A R Y !!!!!!!!!!!!
                    // TODO: T E M P O R A R Y !!!!!!!!!!!!

                    Application.Current.Shutdown();

                }
                else
                {
                    // I M P O R T A N T   B E F O R E   S E N D I N G !!!!!!!!!!
                    // I M P O R T A N T   B E F O R E   S E N D I N G !!!!!!!!!!

                    // IF OUTGOING FILES EXISTS - BKP & RENAME !
                    BackUpAndRenameBeforeToBank(rootDir);
#if !DEBUG
                    // RUN MAIL3.BAT & TCPFOSS :
                    Process.Start(rootDir + "\\MAIL3.BAT").WaitForExit();
#endif

                    if (timerMin < 1)
                    {
                        nLogger.Error("Timer is set for less then 1 minute!!!");
                    }
                    else
                    {
                        if ((DateTime.Now.Hour == 10 | DateTime.Now.Hour == 14) && DateTime.Now.Minute < ((timerMin * 2) - 1))
                        {
                            nLogger.Warn(rootDir + "\\CORRSPR3_aaa.bat - Starting...");
                            // Process.Start(rootDir + "\\CORRSPR3_aaa.bat");
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

                // RUN MAIL3.BAT & TCPFOSS :
#if !DEBUG
                Process.Start(rootDir + "\\MAIL3.BAT").WaitForExit();
#endif
            }
            catch (Exception exc)
            {
                nLogger.Error(methodName + "() - " + exc.Message);
                MessageBox.Show(methodName + "() - " + exc.ToString());
            }
        }




        // TODO: MUST BE REFACTORED!!!!!
        // TODO: MUST BE REFACTORED!!!!!

        private void BackUpAndRenameBeforeToBank(string rootPath)
        {
            string methodName = MethodInfo.GetCurrentMethod().Name;
            try
            {
                string dirOutForBanx = rootPath + "\\ARM3\\EP_O";

                string dirOutForSent = rootPath + "\\SENT\\" +
                    DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM-dd");

                if (!Directory.Exists(dirOutForSent)) Directory.CreateDirectory(dirOutForSent);

                DirectoryInfo[] allSubDirs = new DirectoryInfo(dirOutForBanx).GetDirectories();

                foreach (DirectoryInfo dir in allSubDirs)
                {
                    FileInfo[] outgoimgFiles = dir.GetFiles();

                    if (outgoimgFiles.Length > 0)
                    {
                        String newUniqueName = System.IO.Path.GetFileNameWithoutExtension(outgoimgFiles[0].FullName) + "_" + Guid.NewGuid() + ".zip";

                        File.Copy(outgoimgFiles[0].FullName, dirOutForSent + "\\" + newUniqueName);

                        // R E P O R T !!!!!!!!!!!
                        // R E P O R T !!!!!!!!!!!
                        nLogger.Warn(newUniqueName + " - Sent To - " + dir.Name);

                        File.Move(outgoimgFiles[0].FullName, dir.FullName + "\\" + "1od_" + DateTime.Now.ToString("MMdd") + ".zip");
                    }
                }
            }
            catch (Exception exc)
            {
                nLogger.Error(methodName + "() - " + exc.Message);
                MessageBox.Show(methodName + "() - " + exc.ToString());
            }
        }

    }
}
