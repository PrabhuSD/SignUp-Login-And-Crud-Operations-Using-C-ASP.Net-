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
    public partial class Form3 : Form
    {
        SqlConnection con;


        public Form3()
        {
          
            InitializeComponent();

            con = DatabaseConn.GetConnection();
        
            try
            {
                
                gridview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed even if an exception occurs
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }



        //edit update code

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the user clicked on the button column
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                // Handle edit operation (open a new form for editing)
                int userId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                EditForm editForm = new EditForm(userId);
                editForm.ShowDialog();
                // Refresh the DataGridView after editing
                gridview();
            }

            if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                // Handle delete operation
                int userId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                DeleteUser(userId);
                // Refresh the DataGridView after deleting
                gridview();
            }
        }

        //delete user code
        private void DeleteUser(int userId)
        {
            try
            {
                con.Open();
                string query = "DELETE FROM Users WHERE id=@UserId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User deleted successfully!");
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





        //DataGridView

        public void gridview()
        {
            try
            {
                con.Open();

                // Fetch data from the database
                SqlDataAdapter cmd = new SqlDataAdapter("SELECT * FROM [dbo].[Users]", con);
                DataTable dt = new DataTable();
                cmd.Fill(dt);

                // Debug: Check if DataTable has data
                Console.WriteLine("Rows in DataTable: " + dt.Rows.Count);

                // Clear existing rows before populating
                dataGridView1.Rows.Clear();

                // Debug: Check if DataGridView has columns
                Console.WriteLine("Columns in DataGridView: " + dataGridView1.Columns.Count);

                // Debug: Check if DataGridView is bound to the DataTable
                Console.WriteLine("DataGridView DataSource: " + dataGridView1.DataSource);

                // Populate DataGridView with database data
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["id"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item["name"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["email"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item["phone"].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = item["username"].ToString();
                }

               
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

    }
}
