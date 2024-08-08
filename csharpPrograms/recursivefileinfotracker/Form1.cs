using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;

namespace t8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Browsesource_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;
                    txt_source.Text = selectedPath;
                  //  MessageBox.Show("Selected Folder: " + selectedPath);
                }
            }
        }

        private void Nasdatapath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;
                    richTextBox2.Text = selectedPath;
                 //   MessageBox.Show("Selected Folder: " + selectedPath);
                }
            }
        }

        public static void WriteCsvFile<T>(string filePath, List<T> records)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            string csvFilePath = @"D:\temp.csv";
            using (StreamWriter writer = new StreamWriter(csvFilePath))
            {
                writer.WriteLine("FolderPath,FolderSize,FileCount,DirectoryCount");
                TraverseFolder(txt_source.Text, writer);
                MessageBox.Show("Done");
            }



        }

        static void TraverseFolder(string folderPath, StreamWriter writer)
        {
            // Calculate folder size
            long folderSize = GetDirectorySize(new DirectoryInfo(folderPath));

            // Get file and directory counts
            int fileCount = Directory.GetFiles(folderPath).Length;
            int directoryCount = Directory.GetDirectories(folderPath).Length;

            // Write the current folder data to the CSV
            writer.WriteLine($"{folderPath},{folderSize},{fileCount},{directoryCount}");

            // Recursively traverse each subdirectory
            foreach (string subdirectory in Directory.GetDirectories(folderPath))
            {
                TraverseFolder(subdirectory, writer);
            }
        }

        static long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            long size = 0;

            // Add file sizes.
            FileInfo[] files = directoryInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }

            // Add subdirectory sizes.
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            foreach (DirectoryInfo directory in directories)
            {
                size += GetDirectorySize(directory);
            }

            return size;
        }



        ////  Extra code to traverse through and get directory size
        ////List<string> folders = GetFolders(txt_source.Text);

        ////    foreach (string sub_folder in folders)
        ////    {
        ////        List<string> s_sub_folders = GetFolders(sub_folder);

        ////        foreach(string s_sub_file in s_sub_folders)
        ////        {
        ////            //////  List<string> filelist = GetFiles(s_sub_file);
        ////            ////  FileInfo fileInfo = new FileInfo(s_sub_file);
        ////            ////  double s = (fileInfo.Length) / (1024.0 * 1024.0);
        ////            ///
        ////            getdirectorySize(s_sub_file);




    }
    //public static List<string> GetFolders(string path)
    //{
    //    List<string> folders = new List<string>();

    //    try
    //    {
    //        // Get all subdirectories in the specified path
    //        string[] subdirectories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

    //        // Add each subdirectory to the list
    //        folders.AddRange(subdirectories);
    //    }
    //    catch (UnauthorizedAccessException ex)
    //    {
    //        Console.WriteLine("Access to the path is denied: " + ex.Message);
    //    }
    //    catch (DirectoryNotFoundException ex)
    //    {
    //        Console.WriteLine("The specified path is invalid: " + ex.Message);
    //    }

    //    return folders;
    //}


    //public static List<string> GetFiles(string path)
    //{
    //    List<string> files = new List<string>();

    //    try
    //    {
    //        // Get all files in the specified path
    //        string[] fileEntries = Directory.GetFiles(path);

    //        // Add each file to the list
    //        files.AddRange(fileEntries);
    //    }
    //    catch (UnauthorizedAccessException ex)
    //    {
    //        Console.WriteLine("Access to the path is denied: " + ex.Message);
    //    }
    //    catch (DirectoryNotFoundException ex)
    //    {
    //        Console.WriteLine("The specified path is invalid: " + ex.Message);
    //    }

    //    return files;
    //}

    //public static long getdirectorySize(string s_sub_file)
    //{
    //    DirectoryInfo di = new DirectoryInfo(s_sub_file);
    //    long s = di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
    //    MessageBox.Show(s.ToString());
    //    return s;
    //}

}
