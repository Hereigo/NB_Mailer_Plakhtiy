﻿using System;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Windows.Threading;
using System.Reflection;
using NLog;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

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

        public static int timerHrs = 0, timerMin = 15, timerSec = 0;

        public MainWindow()
        {
            nLogger.Warn("Запустился NBU-Mailer 2015. Версия .Net: {0}", Environment.Version.ToString());

            InitializeComponent();

            // T I M E R !!!!!!!!!

            if (timerMin < 1)
            {
                nLogger.Error("Timer is set for less then 1 minute!!!");
                MessageBox.Show("Timer is set for less then 1 minute!!!");
            }

            labelForTimer_1.Content = "Автоматический запуск каждые: " + timerHrs + " час. " + timerMin + " мин. " + timerSec + " сек.";

            labelForTimer.Content = "Следующий автоматический запуск в " + DateTime.Now.AddHours(timerHrs).AddMinutes(timerMin).AddSeconds(timerSec).ToShortTimeString();

            DispatcherTimer dispchTimer = new DispatcherTimer();
            dispchTimer.Interval = new TimeSpan(timerHrs, timerMin, timerSec);
            dispchTimer.Tick += DispchTimer_Tick;
            dispchTimer.Start();
        }


        private void DispchTimer_Tick(object sender, EventArgs e)
        {
            string todayTempFile = DateTime.Now.ToString("yyyy-MM-dd") + ".day";

            try
            {
                if (!File.Exists(todayTempFile)) File.Create(todayTempFile);
            }
            catch (Exception exc)
            {
                string message = "Can't create TODAY.day FILE! - " + exc.Message;
                nLogger.Error(message);
                MessageBox.Show(message);
            }

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
                //label4messages.Content = DateTime.Now.ToShortTimeString() + " : ReadedSettings Count Is  - " + stringsSetts.Length;
            }
            catch (Exception exc)
            {
                nLogger.Error(methodName + "() - " + exc.Message);
                //label4messages.Content = DateTime.Now.ToShortTimeString() + " : " + methodName + "() - " + exc.ToString();
            }
#if DEBUG
            return stringsSetts[0];
#else
            return stringsSetts[1];
#endif
        }

        private void button1_test_Click(object sender, RoutedEventArgs e)
        {
            // ONLY FOR DEBUGGING
            Git_Ignore_ALFA_TEST tst = new Git_Ignore_ALFA_TEST();
            tst.SendFileToAdmin("C:\\NBUMAIL\\USERD\\Admin\\APPL\\NBU-Packets.xml");
        }



        // - USEFUL CODE-SNIPPET :)
        //
        //
        // string methodName = MethodInfo.GetCurrentMethod().Name;
        // try{ }
        // catch (Exception exc) { 
        //     nLogger.Error(methodName + "() - " + exc.Message);
        //     label4messages.Content = DateTime.Now.ToShortTimeString() + " : " + methodName + "() - " + exc.ToString());
        // }



        // START THE NAIN JOB :
        private void StartJob()
        {
            labelForTimer.Content = "Следующий автоматический запуск в " + 
                DateTime.Now.AddHours(timerHrs).AddMinutes(timerMin).AddSeconds(timerSec).ToShortTimeString();

            // TODO: RUN EACH TASK IF CERTAIN FILES ARE EXISTS !!!
            // TODO: RUN EACH TASK IF CERTAIN FILES ARE EXISTS !!!
            // TODO: RUN EACH TASK IF CERTAIN FILES ARE EXISTS !!!

            string methodName = MethodInfo.GetCurrentMethod().Name;
            try
            {
                //  A L F A   T E S T I N G   O N L Y  !!!!
                //  A L F A   T E S T I N G   O N L Y  !!!!

                Git_Ignore_ALFA_TEST alfa = new Git_Ignore_ALFA_TEST();
                
                //  A L F A   T E S T I N G   O N L Y  !!!!
                //  A L F A   T E S T I N G   O N L Y  !!!!

                rootDir = GetRootDirFromSettsFile();

                #region  A F T E R  23:00 !!!

                //  A F T E R  23:00 !!!

                if (DateTime.Now.Hour > 22)
                {
                    string todayBackUp = @"C:\NBUMAIL\USERD\Admin\ARH\" + DateTime.Now.ToString("Bkp_yyMMdd") + ".RAR";

                    // CHECK IF BKP NOT EXISTS & CREATE IT 

                    if (!File.Exists(todayBackUp))
                    {
                        alfa.AlfaTest_EveningLogUpload();

                        alfa.AlfaTest_TodayBkpCreate(todayBackUp);

                        // TODO: IMLEMENT TWO BACKUPERS !!!!!
                        // TODO: IMLEMENT TWO BACKUPERS !!!!!

                    }
                    else
                    {
                        string todayBackFullUp = "Z:\\" + DateTime.Now.ToString("yyyy-MM") + ".RAR";

                        DateTime lastBkpTime = new FileInfo(todayBackFullUp).LastWriteTime;

                        DateTime lastBkpMustBe = DateTime.Now.AddHours(-24);

                        if (DateTime.Compare(lastBkpMustBe, lastBkpTime) < 0)
                        {
                            nLogger.Warn("Last Bkp Not Needed Because Its Time is " + lastBkpTime);
                        }
                        else
                        {
                            alfa.AlfaTest_TodayFullBkpCreate(todayBackFullUp);

                            nLogger.Warn("NBU-Mailer 2015 Закончил все задачи на сегодня " + DateTime.Now);

                            // TODO: T E M P O R A R Y !!!!!!!!!!!!
                            // TODO: T E M P O R A R Y !!!!!!!!!!!!
                            // TODO: T E M P O R A R Y !!!!!!!!!!!!
                        }
                    }

                    // Application.Current.Shutdown();

                }
                #endregion
                else
                {
                    // I M P O R T A N T   B E F O R E   S E N D I N G !!!!!!!!!!
                    // IF OUTGOING FILES EXISTS - BKP & RENAME !
                    BackUpAndRenameBeforeToBank(rootDir);

                    // RUN MAIL3.BAT - NOT TCPFOSS !!!
                    //label4messages.Content = "Trying To Run Mail3.bat ...";

                    Process.Start(rootDir + "\\MAIL3.BAT").WaitForExit();

                    #region  C O R R S P R 

                    if ((DateTime.Now.Hour == 10 | DateTime.Now.Hour == 14) && DateTime.Now.Minute < (timerMin + 4))
                    {
                        //label4messages.Content = "Trying To Run CORRSPR3_aaa.bat ...";

                        Process.Start(rootDir + "\\CORRSPR3_aaa.bat");

                        nLogger.Warn("Запустился CorrSpr3_aaa.bat для корректировки справочника SPRUSNBU.DBF");
                    }
                    // IF RECEIVE CORR. FOR SPRUSNBU RUN CORRSPR.BAT
                    // WAIT !
                    // UPGRADE SPRUSNBU$ IN DB :
                    #endregion

                    //label4messages.Content = "Trying To CheckEnvelopesAndUploadInDB()...";

                    //label4messages.Content = 

                    alfa.AlfaTest_CheckEnvelopesAndUploadInDB(rootDir);

                    // TODO: R E F A C T O R   A L F A _ T E S T  !!!
                    // TODO: R E F A C T O R   A L F A _ T E S T  !!!
                    // TODO: R E F A C T O R   A L F A _ T E S T  !!!

                    // IF TODAY INCOME DIR EXISTS :
                    //      READ ALL ENVELOPES :
                    //      FOREACH ENVELOPE :
                    //          GET FULL INFO :
                    //          CHECK SMTH...
                    //          WRITE INTO DB :
                    //          REPORT ! :

                }


                // RUN MAIL3.BAT - NOT TCPFOSS !!!
                //label4messages.Content = "Trying To Run Mail3.bat ...";
                Process.Start(rootDir + "\\MAIL3.BAT").WaitForExit();


                // RUN TCPFOSS ONLY !!!
                //label4messages.Content = "Trying To Run TCPFOSS.bat ...";
                Process.Start(rootDir + "\\TCPFOSS.BAT").WaitForExit();


            }
            catch (Exception exc)
            {
                nLogger.Error(methodName + "() - " + exc.Message);
                //label4messages.Content = DateTime.Now.ToShortTimeString() + " : " + methodName + "() - " + exc.Message;
            }
        }


        // TODO: MUST BE REFACTORED!!!!!
        // TODO: MUST BE REFACTORED!!!!!

        private void BackUpAndRenameBeforeToBank(string rootPath)
        {
            string methodName = MethodInfo.GetCurrentMethod().Name;
            try
            {
                string doNotRenameFilesFromHere = "_SPISOK";

                string dirOutForBanx = rootPath + "\\ARM3\\EP_O";

                string dirOutForSent = rootPath + "\\SENT\\" +
                    DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM-dd");

                if (!Directory.Exists(dirOutForSent)) Directory.CreateDirectory(dirOutForSent);

                DirectoryInfo[] allSubDirs = new DirectoryInfo(dirOutForBanx).GetDirectories();

                int filesForSend = 0;

                foreach (DirectoryInfo dir in allSubDirs)
                {
                    if (!dir.Name.Contains(doNotRenameFilesFromHere))
                    {
                        FileInfo[] outgoimgFiles = dir.GetFiles();

                        if (outgoimgFiles.Length > 0)
                        {
                            filesForSend++;

                            string currentExten = Path.GetExtension(outgoimgFiles[0].FullName);

                            String newUniqueName = System.IO.Path.GetFileNameWithoutExtension(outgoimgFiles[0].FullName) + "_" + Guid.NewGuid() + currentExten;

                            File.Copy(outgoimgFiles[0].FullName, dirOutForSent + "\\" + newUniqueName);

                            string destination = dir.Name;

                            // TODO: MUST BE REFACTORED !!!!!!
                            // TODO: MUST BE REFACTORED !!!!!!
                            // TODO: MUST BE REFACTORED !!!!!!
                            if (dir.Name.Contains("FILE")) destination = "Несколько Банков.";

                            nLogger.Warn(newUniqueName.Substring(0, 16) + currentExten +" - Отправлен в - " + dir.Name);

                            File.Move(outgoimgFiles[0].FullName, dir.FullName + "\\" + "1od_" + DateTime.Now.ToString("MMdd") + currentExten);
                        } 
                    }
                }

                //label4messages.Content = DateTime.Now.ToShortTimeString() + " : Отправлено файлов - " + filesForSend;

            }
            catch (Exception exc)
            {
                nLogger.Error(methodName + "() - " + exc.Message);
                //label4messages.Content = DateTime.Now.ToShortTimeString() + " : " + methodName + "() - " + exc.Message;
            }
        }
    }
}
