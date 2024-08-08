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
using System.IO;

namespace t8
{
    public partial class Form2 : Form
    {
        public Form2()
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



        private void Start_Click(object sender, EventArgs e)
        {
            string firstFolderPath = txt_source.Text;                                           //@"C:\path\to\first\folder";  // Change this to your first folder path
            string secondFolderPath = richTextBox2.Text;                                      //  @"C:\path\to\second\folder"; // Change this to your second folder path
            string csvFilePath = @"D:\temp.csv";                             // Change this to your output CSV file path

            // Collect information from the first folder
            Dictionary<string, (long Size, int FileCount, int DirectoryCount)> firstFolderInfo = new Dictionary<string, (long, int, int)>();
            TraverseFolder(firstFolderPath, firstFolderInfo, firstFolderPath);

            using (StreamWriter writer = new StreamWriter(csvFilePath))
            {

                // first line of the csv file
                writer.WriteLine("RelativePath,FirstFolderSize,FirstFileCount,FirstDirectoryCount,SecondFolderSize,SecondFileCount,SecondDirectoryCount");

                // Compare with the second folder
                foreach (var kvp in firstFolderInfo)
                {
                    string relativePath = kvp.Key; // Already stored as relative path
                    string secondFolderPathEquivalent = Path.Combine(secondFolderPath, relativePath);

                    if (Directory.Exists(secondFolderPathEquivalent))
                    {
                        long secondFolderSize = GetDirectorySize(new DirectoryInfo(secondFolderPathEquivalent));
                        int secondFileCount = Directory.GetFiles(secondFolderPathEquivalent).Length;
                        int secondDirectoryCount = Directory.GetDirectories(secondFolderPathEquivalent).Length;

                        writer.WriteLine($"{relativePath},{kvp.Value.Size},{kvp.Value.FileCount},{kvp.Value.DirectoryCount},{secondFolderSize},{secondFileCount},{secondDirectoryCount}");
                    }
                    else
                    {
                        writer.WriteLine($"{relativePath},{kvp.Value.Size},{kvp.Value.FileCount},{kvp.Value.DirectoryCount},0,0,0");
                    }
                }
            }
            MessageBox.Show("Done");
        }


        static void TraverseFolder(string folderPath, Dictionary<string, (long, int, int)> folderInfo, string rootFolderPath)
        {
            long folderSize = GetDirectorySize(new DirectoryInfo(folderPath));
            int fileCount = Directory.GetFiles(folderPath).Length;
            int directoryCount = Directory.GetDirectories(folderPath).Length;

            string relativePath = folderPath.Substring(rootFolderPath.Length).TrimStart('\\');

            folderInfo[relativePath] = (folderSize, fileCount, directoryCount);

            foreach (string subdirectory in Directory.GetDirectories(folderPath))
            {
                TraverseFolder(subdirectory, folderInfo, rootFolderPath);
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












        //    // Collect information from the first folder
        //    Dictionary<string, (long Size, int FileCount, int DirectoryCount, string s, long Size1, int Filecount1, int DirectoryCount1)> firstFolderInfo = new Dictionary<string, (long, int, int, string, long, int, int)>();
        //    TraverseFolder(firstFolderPath, firstFolderInfo, firstFolderPath);

        //        using (StreamWriter writer = new StreamWriter(csvFilePath))
        //        {
        //            writer.WriteLine("RelativePath,FirstFolderSize,FirstFileCount,FirstDirectoryCount,SecondFolderSize,SecondFileCount,SecondDirectoryCount");

        //            // Compare with the second folder
        //            foreach (var kvp in firstFolderInfo)
        //            {
        //                string relativePath = kvp.Key; // Already stored as relative path
        //string secondFolderPathEquivalent = Path.Combine(secondFolderPath, relativePath);

        //                if (Directory.Exists(secondFolderPathEquivalent))
        //                {
        //                    long secondFolderSize = GetDirectorySize(new DirectoryInfo(secondFolderPathEquivalent));
        //int secondFileCount = Directory.GetFiles(secondFolderPathEquivalent).Length;
        //int secondDirectoryCount = Directory.GetDirectories(secondFolderPathEquivalent).Length;

        //writer.WriteLine($"{relativePath},{kvp.Value.Size},{kvp.Value.FileCount},{kvp.Value.DirectoryCount},{secondFolderSize},{secondFileCount},{secondDirectoryCount}");
        //                }
        //                else
        //                {
        //                    writer.WriteLine($"{relativePath},{kvp.Value.Size},{kvp.Value.FileCount},{kvp.Value.DirectoryCount},0,0,0");
        //                }
        //            }
        //        }

        //        Console.WriteLine("CSV file created successfully.");

        //static void TraverseFolder(string folderPath, Dictionary<string, (long, int, int, string,  long, int, int)> folderInfo, string rootFolderPath)
        //{
        //    long folderSize = GetDirectorySize(new DirectoryInfo(folderPath));
        //    int fileCount = Directory.GetFiles(folderPath).Length;
        //    int directoryCount = Directory.GetDirectories(folderPath).Length;

        //    string transformed = folderPath.Replace(@"D:\", "");

        //    long folderSize1 = GetDirectorySize(new DirectoryInfo(folderPath));
        //    int fileCount1 = Directory.GetFiles(folderPath).Length;
        //    int directoryCount1 = Directory.GetDirectories(folderPath).Length;

        //    string relativePath = folderPath.Substring(rootFolderPath.Length).TrimStart('\\');

        //    folderInfo[relativePath] = (folderSize, fileCount, directoryCount, " ",folderSize1, fileCount1 ,directoryCount1 );

        //    foreach (string subdirectory in Directory.GetDirectories(folderPath))
        //    {
        //        TraverseFolder(subdirectory, folderInfo, rootFolderPath);
        //    }
        //}

        //static long GetDirectorySize(DirectoryInfo directoryInfo)
        //{
        //    long size = 0;

        //    // Add file sizes.
        //    FileInfo[] files = directoryInfo.GetFiles();
        //    foreach (FileInfo file in files)
        //    {
        //        size += file.Length;
        //    }

        //    // Add subdirectory sizes.
        //    DirectoryInfo[] directories = directoryInfo.GetDirectories();
        //    foreach (DirectoryInfo directory in directories)
        //    {
        //        size += GetDirectorySize(directory);
        //    }

        //    return size;
        //}

    }
    }
