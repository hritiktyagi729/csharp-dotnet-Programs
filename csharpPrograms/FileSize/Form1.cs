using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySqlConnector;
using System.Configuration;
using System.IO;

namespace filesize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       // MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["connec"].ToString());
        public static DataTable dt(string query)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["connec"].ToString());
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            try
            {
                //conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                string s = ex.Message.ToString();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // FolderBrowserDialog fd = new FolderBrowserDialog();

            string folderpath = BrowseFolder();// @"D:\OTHER2\pdf";
            string key = "scan";
            Dictionary<string, object> myDict =
           new Dictionary<string, object>();

            // Adding key/value pairs in myDict 
            myDict.Add("ID", 1);
            myDict.Add("LocationID", 2407);
            myDict.Add("LotNo",232132);
            myDict.Add("FileBarcode", "A12B34324");
            myDict.Add("PageCount", 15);
            myDict.Add("PageSize", "140*200");
            myDict.Add("ModuleName", "module");
            myDict.Add("LogDate", "today");
            CalculateFolderSize(folderpath, key,myDict);
            

        }
        public static string BrowseFolder()
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select the folder to calculate size";
                folderDialog.ShowNewFolderButton = false;
                 
                DialogResult result = folderDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    return folderDialog.SelectedPath;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
      //  Dictionary<string, int> s_dict = new Dictionary<string, int>();
       // s_dict.Add ("ID", 1);
        
        public static void CalculateFolderSize(string folderPath, string key, Dictionary<string, object> myDict)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException("Directory does not exist: " + folderPath);

           // long totalSize = 0;
            string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
            string csvFilePath = Path.GetFileNameWithoutExtension(folderPath)+".csv";
            var writer = new StreamWriter(csvFilePath);
            

            foreach (string f in files)
            {
                try
                {
                    writer.WriteLine($"calculating the size of the file {f}");
                    FileInfo fileInfo = new FileInfo(f);
                    double s = (fileInfo.Length) / (1024.0 * 1024.0);
                   
                }
                catch(Exception e)
                {
                    writer.WriteLine($"unable to get the file size of file {f}. Error: {e}");
                }

                // s = s / 1024 * 1024

                // Console.WriteLine("file size of the file", f , "is ", fileInfo.Length);
                //string query = $"Insert into my_table (key1, key2) values('{key}', '{s}mb')";
                // string query = $"Insert into test2 (ID, LocationID, LotNo, FileBarcode, PageCount, PgeSize, ModuleName, LogDate ) values" +
                //      $" ('{myDict.TryGetValue("ID", out var ID)}', '{myDict.TryGetValue("LocationId", out var locationId)}', '{myDict.TryGetValue("LotNo", out var LotNo)}'," +
                //      $" '{myDict.TryGetValue("FileBarcode", out var FileBarcode)}', '{myDict.TryGetValue("PageCount", out var PageCount)}', '{myDict.TryGetValue("PageSize", out var PageSize)}'," +
                //        $" '{myDict.TryGetValue("ModuleName", out var ModuleName)}', {myDict.TryGetValue("LogDate", out var LogDate)}";
                try
                {
                    writer.WriteLine($"entering the value in the database {f}");
                    string query = $"Insert into test2 (ID, LocationID, LotNo, FileBarcode, PageCount, PageSize, ModuleName, LogDate ) values" +
                       $" ({myDict["ID"]}, '{myDict["LocationID"]}', '{myDict["LotNo"]}'," +
                       $" '{myDict["FileBarcode"]}', '{myDict["PageCount"]}', '{myDict["PageSize"]}'," +
                       $" '{myDict["ModuleName"]}', '{myDict["LogDate"]}')";
                    writer.WriteLine($"Entering the data into the datatable {f}");
                    dt(query);
                    writer.WriteLine($"succesfully process done on  {f}");
                }
                catch (Exception e)
                {
                    writer.WriteLine($"unable to append the values in the database of file {f}. Error: {e}");
                }


            //   MessageBox.Show(s.ToString());
               // totalSize += fileInfo.Length;
            }
            writer.Close();

          //  return totalSize;
        }
    }
}
