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
        private int loggedInEmployeeID = 0;
        private string loggedInEmployeeName = "";
        private string CustomerEmail;

        public Form1()
        {
            InitializeComponent();
        }


        public Form1(string CustomerEmail)
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cuiButton1.Text = "Continue";


            comboBox1.Visible = false;
        }

        private void logbtn_Click(object sender, EventArgs e)
        {
        }




        private void label5_Click(object sender, EventArgs e)
        {
            Register register = new Register(CustomerEmail);
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

        private void Usernametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Passwordtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void cuiButton1_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = Usernametxt.Text.Trim();
            string password = Passwordtxt.Text.Trim();

            // Step 1: If role is selected already, skip login validation and redirect based on role
            if (comboBox1.Visible && comboBox1.SelectedItem != null)
            {
                string selectedRole = comboBox1.SelectedItem.ToString();

                if (selectedRole == "Driver")
                {
                    Form4 form4 = new Form4(loggedInEmployeeID, CustomerEmail);
                    form4.Show();
                }
                else if (selectedRole == "Admin Staff")
                {
                    Form3 form3 = new Form3();
                    form3.Show();
                }
                else if (selectedRole == "Delivery Manager")
                {
                    MessageBox.Show("Delivery Manager page not implemented yet."); // Or redirect to a form
                }
                else
                {
                    MessageBox.Show("Unknown role selected.");
                    return;
                }

                this.Hide();
                return;
            }

            // Step 2: Continue with normal login process if no role is selected
            if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Username/Email and Password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (usernameOrEmail == "admin" && password == "admin123")
            {
                MessageBox.Show("Admin Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Admindashboard admindashboard = new Admindashboard(CustomerEmail);
                admindashboard.Show();
                this.Hide();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Customer login
                    string customerQuery = "SELECT name, passwordhash FROM customerR WHERE email = @customerEmail";
                    using (SqlCommand cmd = new SqlCommand(customerQuery, con))
                    {
                       
                        cmd.Parameters.AddWithValue("@customerEmail", usernameOrEmail);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                string customerName = reader["name"].ToString();
                                string storedHash = reader["passwordhash"].ToString();

                                if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                                {
                                    MessageBox.Show("Customer Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Cdashboard cdashboard = new Cdashboard(customerName);
                                    cdashboard.Show();
                                    this.Hide();
                                    return;
                                }
                            }
                        }
                    }


                    // Employee login
                    string employeeQuery = "SELECT EmployeeID, FullName, Password, Role FROM Employees WHERE Email = @employeeEmail";
                    using (SqlCommand cmd = new SqlCommand(employeeQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@employeeEmail", usernameOrEmail);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int employeeID = Convert.ToInt32(reader["EmployeeID"]);
                                string employeeName = reader["FullName"].ToString();
                                string storedPassword = reader["Password"].ToString();
                                string roleFromDB = reader["Role"].ToString();

                                if (password == storedPassword)
                                {
                                    loggedInEmployeeID = employeeID;
                                    loggedInEmployeeName = employeeName;

                                    comboBox1.Items.Clear();
                                    comboBox1.Items.Add("Driver");
                                    comboBox1.Items.Add("Admin Staff");
                                    comboBox1.Items.Add("Delivery Manager");
                                    comboBox1.Visible = true;
                                    comboBox1.SelectedItem = roleFromDB;

                                    MessageBox.Show("Employee login successful! Please select your role and click Login again.");
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect employee password.");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("No employee found with that email.");
                                return;
                            }
                        }
                    }

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

        private void cuiTextBox1_ContentChanged(object sender, EventArgs e)
        {

        }

        private void cuiPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cuiPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
    

