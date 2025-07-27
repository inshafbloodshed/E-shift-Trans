using CuoreUI.Controls;
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

namespace E_shift_Trans
{
    public partial class Employees : Form
    {
        string connectionString = "Data Source=DESKTOP-86QTNGL;Initial Catalog=Admin;Integrated Security=True";
        int currentEmployeeId = 0;
        public Employees()
        {

            InitializeComponent();
            txtpassword.Enabled = true;
        
        }

        private void cuiLabel4_Load(object sender, EventArgs e)
        {

        }

        private void Employees_Load(object sender, EventArgs e)
        {

        }



        private void checkbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtpassword_ContentChanged(object sender, EventArgs e)
        {

        }

        private void txtfullname_ContentChanged(object sender, EventArgs e)
        {

        }

        private void txtNIC_ContentChanged(object sender, EventArgs e)
        {

        }

        private void cuiLabel8_Load(object sender, EventArgs e)
        {

        }

        private void checkbox_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkbox.Checked)
            {
                if (!string.IsNullOrWhiteSpace(txtfullname.Text) && !string.IsNullOrWhiteSpace(txtNIC.Text))
                {
                    txtpassword.Text = "E" + txtfullname.Text + txtNIC.Text;
                }
                else
                {
                    MessageBox.Show("Please fill Full Name and NIC Number first.");
                    checkbox.Checked = false;
                }
            }
            else
            {
                txtpassword.Text = string.Empty;
            }
        }

        private void txtemail_ContentChanged(object sender, EventArgs e)
        {
            AutoFillEmployeeData(txtemail.Text, "Email");
        }

        private void txtNIC_ContentChanged_1(object sender, EventArgs e)
        {
            AutoFillEmployeeData(txtNIC.Text, "NICNumber");
        }

        private void AutoFillEmployeeData(string value, string column)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = $"SELECT * FROM Employees WHERE {column} = @value";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@value", value);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    currentEmployeeId = Convert.ToInt32(reader["EmployeeID"]);
                    txtfullname.Text = reader["FullName"].ToString();
                    txtcontactno.Text = reader["ContactNumber"].ToString();
                    txtemail.Text = reader["Email"].ToString();
                    crole.Text = reader["Role"].ToString();
                    txtNIC.Text = reader["NICNumber"].ToString();
                    txtaddress.Text = reader["Address"].ToString();
                    txtdatetime.Value = Convert.ToDateTime(reader["DateOfJoining"]);
                    txtpassword.Text = reader["Password"].ToString();

                    MessageBox.Show("Employee exists. You can update.");
                }
                else
                {
                    currentEmployeeId = 0;
                }
            }
        }

        private void AddEmployee()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Employees 
                        (FullName, ContactNumber, Email, Role, NICNumber, Address, DateOfJoining, Password)
                        VALUES
                        (@FullName, @ContactNumber, @Email, @Role, @NICNumber, @Address, @DateOfJoining, @Password)";
                SqlCommand cmd = new SqlCommand(query, conn);
                SetParameters(cmd);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Employee Added Successfully.");
                ClearFields();
            }
        }

        private void UpdateEmployee()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Employees SET 
                        FullName = @FullName,
                        ContactNumber = @ContactNumber,
                        Email = @Email,
                        Role = @Role,
                        NICNumber = @NICNumber,
                        Address = @Address,
                        DateOfJoining = @DateOfJoining,
                        Password = @Password
                        WHERE EmployeeID = @EmployeeID";

                SqlCommand cmd = new SqlCommand(query, conn);
                SetParameters(cmd);
                cmd.Parameters.AddWithValue("@EmployeeID", currentEmployeeId);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Employee Updated Successfully.");
                ClearFields();
            }
        }
        private void SetParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@FullName", txtfullname.Text.Trim());
            cmd.Parameters.AddWithValue("@ContactNumber", txtcontactno.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtemail.Text.Trim());
            cmd.Parameters.AddWithValue("@Role", crole.Text.Trim());
            cmd.Parameters.AddWithValue("@NICNumber", txtNIC.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtaddress.Text.Trim());
            cmd.Parameters.AddWithValue("@DateOfJoining", txtdatetime.Value);
            cmd.Parameters.AddWithValue("@Password", txtpassword.Text.Trim());
        }

        private void ClearFields()
        {
            txtfullname.Text = "";
            txtcontactno.Text = "";
            txtemail.Text = "";
            crole.Text = "";
            txtNIC.Text = "";
            txtaddress.Text = "";
            txtdatetime.Value = DateTime.Now;
            txtpassword.Text = "";
            currentEmployeeId = 0;
        }

        private void EnsurePasswordGenerated()
        {
            if (string.IsNullOrWhiteSpace(txtpassword.Text))
            {
                if (!string.IsNullOrWhiteSpace(txtfullname.Text) && !string.IsNullOrWhiteSpace(txtNIC.Text))
                {
                    txtpassword.Text = "E" + txtfullname.Text.Trim() + txtNIC.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Please fill Full Name and NIC Number to generate password.");
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtfullname.Text) || string.IsNullOrWhiteSpace(txtNIC.Text) ||
                string.IsNullOrWhiteSpace(txtemail.Text) || string.IsNullOrWhiteSpace(txtcontactno.Text) ||
                string.IsNullOrWhiteSpace(crole.Text) || string.IsNullOrWhiteSpace(txtaddress.Text) ||
                string.IsNullOrWhiteSpace(txtpassword.Text))
            {
                MessageBox.Show("Please fill in all fields including password.");
                return false;
            }
            return true;
        }






        private void cuiButton1_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                if (currentEmployeeId == 0)
                {
                    AddEmployee();
                }
                else
                {
                    UpdateEmployee();
                }
            }
        }


    }
}