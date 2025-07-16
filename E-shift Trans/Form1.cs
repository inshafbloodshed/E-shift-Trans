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
using System.Diagnostics.Eventing.Reader;

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
            string usernameOrEmail = Usernametxt.Text.Trim();
            string password = Passwordtxt.Text.Trim();

            if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Username/Email and Password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Check Admin Users table
                    string adminQuery = "SELECT PasswordHash FROM Users WHERE Username = @username";
                    using (SqlCommand cmd = new SqlCommand(adminQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@username", usernameOrEmail);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string storedHash = result.ToString();

                            if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                            {
                                MessageBox.Show("Admin Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Adashboard adashboard = new Adashboard();
                                adashboard.Show();
                                this.Hide();
                                return;
                            }
                        }
                    }

                    // If not Admin, check Customer table
                    // Inside the login button click handler (customer check section)
                    string customerQuery = "SELECT name, passwordhash FROM customerR WHERE email = @customerEmail";
                    using (SqlCommand cmd = new SqlCommand(customerQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@customerEmail", usernameOrEmail);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string customerName = reader["name"].ToString(); // Get the name from DB
                                string storedHash = reader["passwordhash"].ToString();

                                if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                                {
                                    MessageBox.Show("Customer Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Pass the CORRECT variable (customerName, not 'name')
                                    Cdashboard cdashboard = new Cdashboard(customerName);
                                    cdashboard.Show();
                                    this.Hide();
                                    return;
                                }
                            }
                        }
                    }

                    // If neither Admin nor Customer matched
                    MessageBox.Show("Username/Email or Password is incorrect.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button1_Click(object sender, EventArgs e)
        {
            string plainPassword = "admin123";  // Or any password you want
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

            MessageBox.Show("Hashed Password: " + hashedPassword);
        }
    }
    }

