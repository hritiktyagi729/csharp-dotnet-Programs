using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loginforms
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            LoadAllUserDetail();

        }
        Form1 f11 = new Form1();
        string column_id = "";
        string updt_u_id = "";
        private void Submit_btn_Click(object sender, EventArgs e)
        {


            if (u_name.Text == "" || u_passwd.Text == "" || u_location.Text == "" || u_status.Text == "" || u_active.Text == "" || u_profile.Text == "" || u_role.Text == "")
            {
                MessageBox.Show("Enter all values");
                return;
            }
            //if (u_name.Text == "")
            //{
            //    MessageBox.Show("Enter username");
            //}
            //if (u_passwd.Text == "")
            //{
            //    MessageBox.Show("Enter Password");
            //}
            //if (u_location.Text == "")
            //{
            //    MessageBox.Show("Enter Location");
            //}
            //if (u_status.Text == "")
            //{
            //    MessageBox.Show("Enter User Status");
            //}
            //if (u_active.Text == "")
            //{
            //    MessageBox.Show("Enter Active status");
            //}
            //if (u_profile.Text == "")
            //{
            //    MessageBox.Show("Enter profile");
            //}
            //if (u_role.Text == "")
            //{
            //    MessageBox.Show("Enter User role");
            //}


            // query if user already exist
            //  bool u_exist = false;
            //   try
            //   {
            string qr = $"Select * from user_master where u_name='{u_name.Text}'";
            DataTable dt = f11.dt(qr);
            string usr_id = "";
            try
            {
                usr_id = dt.Rows[0]["user_id"].ToString();
            }
            catch
            {

            }


            //stusr=dt.Rows[0]["u_name"].ToString();

            if (usr_id == updt_u_id)
            {
                // MessageBox.Show("User Already exist, Try login");
                //Form2 f2 = new Form2();

                //    // hide the current form
                //this.Hide();

                //    // use the `Show()` method to access the new non-modal form
                //f11.Show();
                // if (MessageBox.Show("user already exist with this name. you want to update  ?", "DC", MessageBoxButtons.OKCancel) == DialogResult.OK){}
                string role = u_role.Text;
                string profile = u_profile.Text;
                string query = "update user_master set u_name='" + u_name.Text + "', u_pwd='" + u_passwd.Text + "' ,u_status='" + u_status.Text + "', u_location= '" + u_location.Text + "', u_active='" + u_active.Text + "', u_role='" + u_role.Text + "'  where user_id='" + updt_u_id + "'";
                DataTable dts = f11.dt(query);
                LoadAllUserDetail();
                txt_clear();



            }
            else
            {

                //     string query = "insert into user_master (u_name,u_pwd,u_role,u_active,u_status,u_location,u_profile) values('" + user_nme + "','" + user_pwd + "','" + user_role + "','" + user_active + "','" + user_status + "','" + user_loca + "','" + user_profile + "')";

                string query = "insert into user_master (u_name,u_pwd,u_status,u_location, u_profile,u_active, u_role) values('" + u_name.Text + "','" + u_passwd.Text + "','" + u_status.Text + "','" + u_location.Text + "','" + u_profile.Text + "','" + u_active.Text + "', '" + u_role.Text + "' )";


                //now passing queru intu table
                DataTable dts = f11.dt(query);
                LoadAllUserDetail();
                MessageBox.Show("Succesfully Inserted into datbase");
                txt_clear();
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {

            try
            {
                string query = "update user_master set u_name='" + u_name.Text + "', u_pwd='" + u_passwd.Text + "' ,u_status='" + u_status.Text + "', u_location= '" + u_location.Text + "'  where user_id='" + u_id.Text + "'";

                DataTable dts = f11.dt(query);

                MessageBox.Show("Database updated");

            }
            catch (Exception ex)
            {
                Console.WriteLine("updation failed, error is:", ex);
            }

        }

        private void Button3_Click(object sender, EventArgs e)
        {

            try
            {
                // string query = "update user_master set u_name='" + u_name.Text + "', u_pwd='" + u_passwd.Text + "' ,u_status='" + u_status.Text + "', u_location= '" + u_location.Text + "'  where user_id='" + u_id.Text + "'";
                string query = $"DELETE from user_master where user_id='" + u_id.Text + "'";

                DataTable dts = f11.dt(query);

                MessageBox.Show("User Deleted with id", u_id.Text);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Deletion failed, error is:", ex);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            //    // use the `Show()` method to access the new non-modal form
            f11.Show();

        }

        // Get all users
        public DataTable getusermaster()
        {
            string query = "SELECT * from user_master";
            DataTable dtuser = f11.dt(query);
            return dtuser;

        }
        public void LoadAllUserDetail()
        {
            //DataTable dt = new DataTable();
            string query = "SELECT * from user_master";
            DataTable dtuser = f11.dt(query);

            try
            {

                dataGridView1.Rows.Clear();
                // dt = _blclassobj.getusermaster();
                if (dtuser.Rows.Count > 0)
                {
                    for (int i = 0; i < dtuser.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = dtuser.Rows[i]["user_id"].ToString();
                        dataGridView1.Rows[i].Cells[1].Value = dtuser.Rows[i]["u_name"].ToString();
                        dataGridView1.Rows[i].Cells[2].Value = dtuser.Rows[i]["u_pwd"].ToString();
                        dataGridView1.Rows[i].Cells[3].Value = dtuser.Rows[i]["u_role"].ToString();
                        dataGridView1.Rows[i].Cells[4].Value = dtuser.Rows[i]["u_profile"].ToString();
                        dataGridView1.Rows[i].Cells[5].Value = dtuser.Rows[i]["u_active"].ToString();
                        dataGridView1.Rows[i].Cells[6].Value = dtuser.Rows[i]["u_status"].ToString();
                        dataGridView1.Rows[i].Cells[7].Value = dtuser.Rows[i]["u_location"].ToString();

                    }
                    dataGridView1.Columns[2].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    var senderGrid = (DataGridView)sender;

        //    if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
        //    {
        //        if (e.ColumnIndex == 8)
        //        {
        //            btn_save.Enabled = false;
        //            btn_update.Enabled = true;
        //            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
        //            column_id = row.Cells[0].Value.ToString();
        //            txt_user.Text = row.Cells[1].Value.ToString();
        //            txt_pwd.Text = row.Cells[2].Value.ToString();
        //            cmb_role.Text = row.Cells[3].Value.ToString();
        //            cmb_profile.Text = row.Cells[4].Value.ToString();
        //            cmb_active.Text = row.Cells[5].Value.ToString();
        //            cmb_status.Text = row.Cells[6].Value.ToString();
        //            cmb_location.Text = row.Cells[7].Value.ToString();
        //        }
        //        if (e.ColumnIndex == 9)
        //        {
        //            if (MessageBox.Show("Do you want to delete this User ?", "DC", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //            {

        //                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
        //                if (row.Cells[1].Value.ToString() != GlobalVariable.username)
        //                {
        //                    column_id = row.Cells[0].Value.ToString();
        //                    _blclassobj.deleteuser(column_id);
        //                    txt_clear();
        //                    LoadAllUserDetail();

        //                }
        //                else
        //                {
        //                    MessageBox.Show("You can't delete this user", "Cbsl Workflow", MessageBoxButtons.OK);
        //                    return;
        //                }

        //            }
        //        }
        //    }
        //}
        //private void btn_update_Click(object sender, EventArgs e)
        //{
        //    if (Convert.ToInt32(column_id) != 0)
        //    {
        //        UpdateUserDetail(txt_user.Text, txt_pwd.Text, cmb_role.Text, cmb_profile.Text, cmb_active.Text, cmb_status.Text, cmb_location.Text, column_id);
        //        MessageBox.Show("Data Updated!!");
        //        txt_clear();
        //        btn_update.Enabled = false;
        //        btn_save.Enabled = true;
        //        LoadAllUserDetail();
        //    }
        //}
        //public int UpdateUserDetail(string user_nme, string user_pwd, string user_role, string user_profile, string user_active, string user_status, string user_loca, string user_id)
        //{
        //    return _dlclass.updateuser(user_nme, user_pwd, user_role, user_profile, user_active, user_status, user_loca, user_id);


        //}



        private void txt_clear()
        {
            //u_name.Clear(); txt_pwd.Clear();
            u_role.Text = "";
            u_profile.Text = "";
            u_active.Text = "";
            u_status.Text = "";
            u_location.Text = "";
            u_name.Text = "";
            u_passwd.Text = "";
            u_id.Text = "";


        }

        public void deleteuser(string column_id)
        {
            try
            {
                // string query = "update user_master set u_name='" + u_name.Text + "', u_pwd='" + u_passwd.Text + "' ,u_status='" + u_status.Text + "', u_location= '" + u_location.Text + "'  where user_id='" + u_id.Text + "'";
                string query = $"DELETE from user_master where user_id='" + column_id + "'";

                DataTable dts = f11.dt(query);

                MessageBox.Show("User Deleted with id", u_id.Text);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Deletion failed, error is:", ex);
            }

        }


        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 9)
                {
                    // submit_btn.Enabled = false;
                    // update.Enabled = true;
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    updt_u_id = row.Cells[0].Value.ToString();
                    u_name.Text = row.Cells[1].Value.ToString();
                    u_passwd.Text = row.Cells[2].Value.ToString();
                    u_role.Text = row.Cells[3].Value.ToString();
                    u_profile.Text = row.Cells[4].Value.ToString();
                    u_active.Text = row.Cells[5].Value.ToString();
                    u_status.Text = row.Cells[6].Value.ToString();
                    u_location.Text = row.Cells[7].Value.ToString();
                    u_passwd.ReadOnly = true;
                    

                }
                if (e.ColumnIndex == 8)
                {
                    if (MessageBox.Show("Do you want to delete this User ?", "DC", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {

                        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                        if (row.Cells[1].Value.ToString() != GlobalVariable.username)
                        {
                            column_id = row.Cells[0].Value.ToString();
                            deleteuser(column_id);
                            // txt_clear();
                            LoadAllUserDetail();

                        }
                        else
                        {
                            MessageBox.Show("You can't delete this user", "Cbsl Workflow", MessageBoxButtons.OK);
                            return;
                        }

                    }
                }
            }
        }
        class GlobalVariable
        {
            public static string username = "";
            public static string userid = "";
            public static string password = "";

            public static string localpathscan = "";
            public static string localpathqc = "";
            public static string localpathexport = "";
            public static string localpathindex = "";
            public static string localpath = "";
            public static string projectname = "";
            public static string localip = "";
            public static string drive = "";
            public static string modules = "";
            public static string locationid = "";
            public static string locationdist = "";

            public static string qc_segpath { get; set; }
            public static string deed_barcode { get; set; }
            public static string DeedNumber { get; set; }
            public static string docType { get; set; }

            public static string LocationName { get; set; }

            public static string autometadata { get; set; }
            public static string NationalCode { get; set; }

        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // reset button
        private void Update_Click(object sender, EventArgs e)
        {
            txt_clear();
            u_passwd.ReadOnly = false;
        }
    }
}

