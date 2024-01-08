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
    public partial class Form2 : Form
    {
        SqlConnection con;
        public Form2()
        {
            InitializeComponent();
            con =DatabaseConn.GetConnection();
        }

        //login button
        private void login_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the username and password from the TextBoxes
                string username = txtUname.Text;
                string password = txtPass.Text;

                // Check if the username and password are not empty
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    // Use parameterized query to prevent SQL injection
                    string query = "SELECT * FROM Users WHERE username=@Username AND password=@Password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the query
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if there is a matching user
                            if (reader.Read())
                            {
                                // Successful login, perform further actions if needed
                            MessageBox.Show("Login successful!");

                                /*Form4 form4 = new Form4();

                                // Show Form2
                                form4.Show();

                                // Optionally, you can hide Form1
                                this.Hide();*/


                                Form3 form3 = new Form3();
                                form3.gridview();

                                // Show Form2
                                form3.Show();

                                // Optionally, you can hide Form1
                                this.Hide();


                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password");
                            }

                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("Please enter both username and password");
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

       
        //Regtrat ion button
        private void btnReg_Click(object sender, EventArgs e)
        {

            // Create an instance of Form2
            Form1 form1 = new Form1();

            // Show Form2
            form1.Show();

            // Optionally, you can hide Form1
            this.Hide();
        }
    }
}
