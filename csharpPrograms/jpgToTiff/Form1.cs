using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace t2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            // Create an instance of FolderBrowserDialog
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                // Set a description to guide the user
                folderBrowserDialog.Description = "Select a folder";

                // Show the FolderBrowserDialog and check if the user selected a folder
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected folder path
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;
                    input_path.Text = selectedFolderPath;

                    // Clear the existing nodes in the TreeView
                    treeViewDirectories.Nodes.Clear();

                    // Load the directory structure into the TreeView
                    LoadDirectoryTree(selectedFolderPath, treeViewDirectories.Nodes);
                }

            }

           
        
        }
        private void LoadDirectoryTree(string dirPath, TreeNodeCollection nodeCollection)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
                TreeNode rootNode = new TreeNode(directoryInfo.Name)
                {
                    Tag = directoryInfo
                };
                nodeCollection.Add(rootNode);
                LoadSubDirectories(directoryInfo, rootNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSubDirectories(DirectoryInfo dirInfo, TreeNode treeNode)
        {
            try
            {
                foreach (var directory in dirInfo.GetDirectories())
                {
                    TreeNode dirNode = new TreeNode(directory.Name)
                    {
                        Tag = directory
                    };
                    treeNode.Nodes.Add(dirNode);
                    LoadSubDirectories(directory, dirNode);
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    TreeNode fileNode = new TreeNode(file.Name)
                    {
                        Tag = file
                    };
                    treeNode.Nodes.Add(fileNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
