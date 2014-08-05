using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;

namespace Practicals_2014.Pages
{
    /// <summary>
    /// Interaction logic for PatentsFromWeb.xaml
    /// </summary>
    public partial class PatentsFromWeb : System.Windows.Controls.UserControl
    {
        List<ResultData> resultDB = new List<ResultData>();
        WelcomePage welCmPg = new WelcomePage();

        string saveDir = "";
        string docNum = "";
        double progress = 0;
        int count;

        public PatentsFromWeb()
        {
            InitializeComponent();
            saveDir = "Save Location: " + AppDomain.CurrentDomain.BaseDirectory;
            saveLocation.Content = saveDir;
        }

        private void SaveLocation_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            folderDlg.ShowNewFolderButton = true;

            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                saveDir = folderDlg.SelectedPath + "\\";
                saveLocation.Content = "Save Location: " + saveDir;
            }
        }

        private void GetPatent_Click(object sender, RoutedEventArgs e)
        {
            pbStatus.Value = 0;
            count = Convert.ToInt32(CntPatent.Value);
            docNum = StartPublNum.Text;
            Window_ContentRendered();
        }

        private void Window_ContentRendered()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
        }

        void toggleProcessButton(bool val)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                GetPatent.IsEnabled = val;
            }));
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {           
            string savePath = saveDir;
            string PN = "";

            //docNum = "RU97508322";

            progress = 0;
            docNum = docNum.Trim();
            Match match = Regex.Match(docNum, @"([A-Za-z]{2})(\d+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                toggleProcessButton(false);
                int cntNum = Convert.ToInt32(docNum.Substring(2));
                docNum = docNum.Substring(0, 2);

                for (int i = 0, j = 0; i < count; j++, cntNum++)
                {
                    string curDocNum = docNum + cntNum.ToString();

                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        StatusLabel.Content = "Processing Patent: " + curDocNum + "...";
                    }));

                    PN = welCmPg.GetEnglishRussianClaimsFromWeb(curDocNum, savePath);

                    if (PN != "-1")
                    {
                        i++;
                        progress += 100 / (double)count;
                        (sender as BackgroundWorker).ReportProgress((int)progress);
                    }

                    resultDB.Add(new ResultData()
                    {
                        Id = j,
                        DocNum = curDocNum,
                        SaveNameEN = (PN != "-1") ? PN + "_EN.txt" : "Not Available;",
                        SaveNameRU = (PN != "-1") ? PN + "_RU.txt" : "Not Available;",
                        SaveFileLocation = savePath
                    });
                }
            }
            else
            {
                var result = System.Windows.Forms.MessageBox.Show("Invalid Publication Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            System.Windows.Application.Current.Resources["resultData"] = resultDB;
            this.Dispatcher.Invoke((Action)(() =>
            {
                StatusLabel.Content = "Processing Completed. ";
            }));

            toggleProcessButton(true);
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }
    }
}
