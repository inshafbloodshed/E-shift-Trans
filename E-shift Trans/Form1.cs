using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Windows.Forms;
using BCrypt.Net;

namespace E_shift_Trans
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Data Source=DESKTOP-86QTNGL;Initial Catalog=Admin;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void logbtn_Click(object sender, EventArgs e)
        {

            string username = Usernametxt.Text.Trim();
            string password = Passwordtxt.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Username and Password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // 1. Check Admin Table
                    string adminQuery = "SELECT PasswordHash FROM Users WHERE Username=@username";
                    using (SqlCommand adminCmd = new SqlCommand(adminQuery, con))
                    {
                        adminCmd.Parameters.AddWithValue("@username", username);
                        object adminResult = adminCmd.ExecuteScalar();

                        if (adminResult != null)
                        {
                            string adminHash = adminResult.ToString();
                            if (BCrypt.Net.BCrypt.Verify(password, adminHash))
                            {
                                MessageBox.Show("Admin Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Adashboard adashboard = new Adashboard();
                                adashboard.Show();
                                this.Hide();
                                return;
                            }
                        }
                    }

                    // 2. Check Customer Table
                    string customerQuery = "SELECT passwordhash FROM customerR WHERE email=@username";
                    using (SqlCommand custCmd = new SqlCommand(customerQuery, con))
                    {
                        custCmd.Parameters.AddWithValue("@username", username);
                        object custResult = custCmd.ExecuteScalar();

                        if (custResult != null)
                        {
                            string custHash = custResult.ToString();
                            if (BCrypt.Net.BCrypt.Verify(password, custHash))
                            {

                            }
                        }
                    }

                    // 3. If not found in both tables
                    MessageBox.Show("Username or Password is incorrect.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Usernametxt.Text = "";
                    Passwordtxt.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to database: " + ex.Message);
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (showpass.Checked)
            {
                // Show password
                Passwordtxt.PasswordChar = '\0'; // No masking character
            }
            else
            {
                // Hide password
                Passwordtxt.PasswordChar = '*';
            }
        }
    }
}
