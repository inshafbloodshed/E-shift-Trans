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
    public partial class AddEmployee : Form
    {
            
        string connectionString = "Data Source=DESKTOP-86QTNGL;Initial Catalog=Admin;Integrated Security=True";
        int currentEmployeeId = 0;
        public AddEmployee()
        {
            InitializeComponent();
            txtpassword.Enabled = false;
            checkBox1.CheckedChanged += checkbox_CheckedChanged_1;

        }

        private void checkbox_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (!string.IsNullOrWhiteSpace(txtfname.Text) && !string.IsNullOrWhiteSpace(txtnic.Text))
                {
                    txtpassword.Text = "E" + txtfname.Text.Trim() + txtnic.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Please fill Full Name and NIC Number first.");
                    checkBox1.Checked = false;
                }
            }
            else
            {
                txtpassword.Text = string.Empty;
            }
        }

        private void cuiTextBox1_ContentChanged(object sender, EventArgs e)
        {

        }

        private void cuiButton1_Click(object sender, EventArgs e)
        {
            
            
                Employee();
            
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtfname.Text) ||
                string.IsNullOrWhiteSpace(txtcontact.Text) ||
                string.IsNullOrWhiteSpace(txtemail.Text) ||
                string.IsNullOrWhiteSpace(cRole.Text) ||
                string.IsNullOrWhiteSpace(txtnic.Text) ||
                string.IsNullOrWhiteSpace(txtaddress.Text) ||
                string.IsNullOrWhiteSpace(txtpassword.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return false;
            }
            return true;
        }
        private void ClearFields()
        {
            txtfname.Text = "";
            txtcontact.Text = "";
            txtemail.Text = "";
            cRole.Text = "";
            txtnic.Text = "";
            txtaddress.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            txtpassword.Text = "";
        }

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            if (currentEmployeeId == 0)
            {
                Employee();
            }
            else
            {
                UpdateEmployee();
            }
        }
        private void UpdateEmployee()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Employees SET 
                                FullName=@FullName, ContactNumber=@Contact, Email=@Email, Role=@Role, 
                                NICNumber=@NIC, Address=@Address, DateOfJoining=@DOJ, Password=@Password
                                WHERE EmployeeID=@EmployeeID";

                SqlCommand cmd = new SqlCommand(query, conn);
                SetParameters(cmd);
                cmd.Parameters.AddWithValue("@EmployeeID", currentEmployeeId);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Employee Updated.");
                ClearFields();
            }
        }

       



        private void Employee()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Employees 
                               (FullName, ContactNumber, Email, Role, NICNumber, Address, DateOfJoining, Password)VALUES
                               (@FullName, @ContactNumber, @Email, @Role, @NICNumber, @Address, @DateOfJoining, @Password)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", txtfname.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactNumber", txtcontact.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtemail.Text.Trim());
                cmd.Parameters.AddWithValue("@Role", cRole.Text.Trim());
                cmd.Parameters.AddWithValue("@NICNumber", txtnic.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtaddress.Text.Trim());
                cmd.Parameters.AddWithValue("@DateOfJoining", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Password", txtpassword.Text.Trim());

                cmd.ExecuteNonQuery();

                MessageBox.Show("Employee Added Successfully.");
                ClearFields();
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {

            if (currentEmployeeId == 0)
            {
                Employee();
            }
            else
            {
                UpdateEmployee();
            }
        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {
            AutoFillEmployeeData(txtemail.Text.Trim());
        }

        private void txtfname_TextChanged(object sender, EventArgs e)
        {
            AutoFillEmployeeData(txtfname.Text.Trim());
        }

        private void txtnic_TextChanged(object sender, EventArgs e)
        {
            AutoFillEmployeeData(txtnic.Text.Trim());
        }

        private void txtaddress_TextChanged(object sender, EventArgs e)
        {
            AutoFillEmployeeData(txtaddress.Text.Trim());
        }

        private void cRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoFillEmployeeData(cRole.Text.Trim());
        }
        private void AutoFillEmployeeData(string value)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Employees WHERE Email = @value OR NICNumber = @value";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@value", value);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    currentEmployeeId = Convert.ToInt32(reader["EmployeeID"]);
                    txtfname.Text = reader["FullName"].ToString();
                    txtcontact.Text = reader["ContactNumber"].ToString();
                    txtemail.Text = reader["Email"].ToString();
                    cRole.Text = reader["Role"].ToString();
                    txtnic.Text = reader["NICNumber"].ToString();
                    txtaddress.Text = reader["Address"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["DateOfJoining"]);
                    txtpassword.Text = reader["Password"].ToString();
                    checkBox1.Checked = false;

                    // You might want to remove this to avoid annoying message boxes:
                    // MessageBox.Show("Employee exists. You can update or delete.");
                }
                else
                {
                    currentEmployeeId = 0;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (!string.IsNullOrWhiteSpace(txtfname.Text) && !string.IsNullOrWhiteSpace(txtnic.Text))
                {
                    txtpassword.Text = "E" + txtfname.Text + txtnic.Text;
                }
                else
                {
                    MessageBox.Show("Please fill Full Name and NIC Number first.");
                    checkBox1.Checked = false;
                }
            }
            else
            {
                txtpassword.Text = string.Empty;
            }
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {

        }

        private void SetParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@FullName", txtfname.Text.Trim());
            cmd.Parameters.AddWithValue("@Contact", txtcontact.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtemail.Text.Trim());
            cmd.Parameters.AddWithValue("@Role", cRole.Text.Trim());
            cmd.Parameters.AddWithValue("@NIC", txtnic.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtaddress.Text.Trim());
            cmd.Parameters.AddWithValue("@DOJ", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@Password", txtpassword.Text);
        }



        private void btndelet_Click(object sender, EventArgs e)
        {
            if (currentEmployeeId == 0)
            {
                MessageBox.Show("No employee selected to delete.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", currentEmployeeId);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Employee Deleted.");
                ClearFields();
            }
        }

        private void cuiButton8_Click(object sender, EventArgs e)
        {
            if (currentEmployeeId == 0)
            {
                Employee();
            }
            else
            {
                UpdateEmployee();
            }
        }

        private void cuiButton3_Click(object sender, EventArgs e)
        {
            if (currentEmployeeId == 0)
            {
                MessageBox.Show("No employee selected to delete.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", currentEmployeeId);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Employee Deleted.");
                ClearFields();
            }
        }
    }

}

