using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace Practicals_2014.Pages
{
    /// <summary>
    /// Interaction logic for PatentsFromDB.xaml
    /// </summary>
    public partial class PatentsFromDB : System.Windows.Controls.UserControl
    {
        List<ResultData> resultDB = new List<ResultData>();
        List<string> AllFileList = new List<string>();
        WelcomePage welCmPg = new WelcomePage();

        string saveDir = "";
        string dbDir = "";
        string PN = "";
        string curFile = "";
        double progress = 0;
        int fileCount = 0;

        
        public PatentsFromDB()
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

        private void DBLocation_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            folderDlg.ShowNewFolderButton = true;

            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                dbDir = folderDlg.SelectedPath + "\\";
                dbLocation.Content = "Patents Database Location: " + dbDir;
            }
        }

        private void GetPatent_Click(object sender, RoutedEventArgs e)
        {
            pbStatus.Value = 0;           
            
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
            try
            {
                AllFileList.Clear();

                ProcessDirectory(dbDir, AllFileList);

                fileCount = AllFileList.Count;

                progress = 0;

                for (int i = 0; i < fileCount; i++)
                {
                    curFile = AllFileList.ElementAt(i);
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        StatusLabel.Content = fileCount + " Total Files. " +
                            "Processing: " + curFile + "...";
                    }));

                    PN = welCmPg.GetEnglishRussianClaimsFromDatabase(curFile, saveDir);

                    progress += 100 / (double)fileCount;
                    (sender as BackgroundWorker).ReportProgress((int)progress);

                    resultDB.Add(new ResultData()
                    {
                        Id = i,
                        DocNum = PN,
                        SaveNameEN = (PN != "-1") ? PN + "_EN.txt" : "Not Available;",
                        SaveNameRU = (PN != "-1") ? PN + "_RU.txt" : "Not Available;",
                        SaveFileLocation = saveDir
                    });
                }

                System.Windows.Application.Current.Resources["resultData"] = resultDB;

                this.Dispatcher.Invoke((Action)(() =>
                {
                    StatusLabel.Content = "Processing Completed. ";
                }));

                toggleProcessButton(true);
            }
            catch (Exception x) { };
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage; 
        }

        public void ProcessDirectory(string targetDirectory, List<string> fileList)
        {
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(targetDirectory, "*.xml");
            foreach (string fileName in fileEntries)
                fileList.Add(fileName);

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, fileList);
        }

        // Insert logic for processing found files here. 
        public void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);
        }

    }
}
