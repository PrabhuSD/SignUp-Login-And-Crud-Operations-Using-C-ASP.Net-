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
    public partial class Form1 : Form
    {
        SqlConnection con;
        public Form1()
        {
            InitializeComponent();
            con = DatabaseConn.GetConnection();
        }

        //Registrtion form
        private void register_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateRegistration())
                {
                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Users] ([name], [email], [phone], [username], [password]) VALUES (@Name, @Email, @Phone, @Username, @Password)", con))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Username", txtUname.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPass.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registration successful!");
                        txtName.Clear();
                        txtEmail.Clear();
                        txtPhone.Clear();
                        txtUname.Clear();
                        txtPass.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }


        }


        //login page button
        private void login_Click(object sender, EventArgs e)

        {
            // Create an instance of Form2
            Form2 form2 = new Form2();

            // Show Form2
            form2.Show();

            // Optionally, you can hide Form1
            this.Hide();
        }


        //email validation
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        //form validation
        private bool ValidateRegistration()
        {
            // Check if name is not empty
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please enter your name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return false;
            }

            // Check if email is a valid format
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return false;
            }

            // Check if phone is not empty and contains only digits
            if (string.IsNullOrEmpty(txtPhone.Text) || !txtPhone.Text.All(char.IsDigit))
            {
                MessageBox.Show("Please enter a valid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhone.Focus();
                return false;
            }

            // Check if username is not empty
            if (string.IsNullOrEmpty(txtUname.Text))
            {
                MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUname.Focus();
                return false;
            }

            // Check if password is not empty
            if (string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Focus();
                return false;
            }

            return true;
        }



    }
}