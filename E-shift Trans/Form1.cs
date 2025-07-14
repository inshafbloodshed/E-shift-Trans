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

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter both Username and Password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

   
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(1) FROM Users WHERE Username=@username AND Password=@password";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@passwordhash", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Navigate to the next form or main application window

                            Adashboard adashboard = new Adashboard();
                            adashboard.Show();
                            this.Hide();

                        }
                        else
                        {
                            MessageBox.Show("Username or Password is incorrect.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Usernametxt.Text = "";
                            Passwordtxt.Text = "";
                        }
                    }
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
