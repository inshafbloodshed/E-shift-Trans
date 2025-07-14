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
using System.Xml.Linq;
using BCrypt.Net;
namespace E_shift_Trans
{
    public partial class Register : Form
    {
        private string connectionString = @"Data Source=DESKTOP-86QTNGL;Initial Catalog=Admin;Integrated Security=True";
        public Register()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            signup.Enabled = checkBox1.Checked; 
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

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

        private bool IsNumeric(string input)
        {
            return long.TryParse(input, out _);
        }

        private void ClearFields()
        {
            cnametxt.Text = "";
            cphonnumbertxt.Text = "";
            cemailtxt.Text = "";
            cpassword.Text = "";
            ccpassword.Text = "";
            checkBox1.Checked = false;
        }


        private void signup_Click(object sender, EventArgs e)
        {
            {
                string name = cnametxt.Text.Trim();
                string phone = cphonnumbertxt.Text.Trim();
                string email = cemailtxt.Text.Trim();
                string password = cpassword.Text;
                string confirmPassword = ccpassword.Text;

                // Basic Validations
                if (name == "" || phone == "" || email == "" || password == "" || confirmPassword == "")
                {
                    MessageBox.Show("All fields are required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Please enter a valid email address.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsNumeric(phone))
                {
                    MessageBox.Show("Phone number should contain digits only.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (password != confirmPassword)
                {
                    MessageBox.Show("Passwords do not match.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (password.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters long.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "INSERT INTO customerR (name, phonenumber, email, passwordhash) VALUES (@name, @phone, @email, @password)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@phone", phone);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@password", hashedPassword);

                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Registration Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearFields();
                            }
                            else
                            {
                                MessageBox.Show("Registration Failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
