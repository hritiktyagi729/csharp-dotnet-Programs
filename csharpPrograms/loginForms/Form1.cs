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

namespace Loginforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["connec"].ToString());
        public DataTable dt(string query)
        {
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
        private void Login_Click(object sender, EventArgs e)
        {
            string name =usr_name.Text;
            string pass = usr_passwd.Text;
            string query = $"Select * from user_master where u_name='{name}'";
            DataTable dts = dt(query);
            if (dts.Rows.Count > 0)
            {
                string pas = dts.Rows[0]["u_pwd"].ToString();
                //MessageBox.Show(pas);

                if (dts.Rows[0]["u_pwd"].ToString()== pass.Trim())
                {
                    MessageBox.Show("Login successful");
                }
                else
                {
                    MessageBox.Show("Wrong password");
                    
                }
            }
            else
            {

                MessageBox.Show("Wrong username, Try Signup");
                Form2 f2 = new Form2();

                //    // hide the current form
                this.Hide();

                //    // use the `Show()` method to access the new non-modal form
                f2.Show();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //    // create an object of `Form2` form in the current form
            Form2 f2 = new Form2();

            //    // hide the current form
            this.Hide();

            //    // use the `Show()` method to access the new non-modal form
            f2.Show();
        }

    }
   
}
