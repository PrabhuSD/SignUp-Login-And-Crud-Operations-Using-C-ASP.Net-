using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class EditForm : Form
    {
        private int userId;
        SqlConnection con;

        public EditForm(int userId)
        {
            InitializeComponent();
            con = DatabaseConn.GetConnection();
            this.userId = userId;
            LoadUserData();
        }


        //retriving data from table
        private void LoadUserData()
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM Users WHERE id=@UserId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["name"].ToString();
                            txtEmail.Text = reader["email"].ToString();
                            txtPhone.Text = reader["phone"].ToString();
                            txtUname.Text = reader["username"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("User not found!");
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                this.Close();
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
             
           
        //update user detail button
        private void btnSave_Click_1(object sender, EventArgs e)
        {
                // Handle save/update operation
            try
            {
                con.Open();
                string query = "UPDATE Users SET name=@Name, email=@Email, phone=@Phone, username=@Username WHERE id=@UserId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Username", txtUname.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
    
}
