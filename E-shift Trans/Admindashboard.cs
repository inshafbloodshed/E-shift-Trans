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
    public partial class Admindashboard : Form
    {
        private string connectionString = @"Data Source=DESKTOP-86QTNGL;Initial Catalog=Admin;Integrated Security=True";
        private string customerName;
        private String CustomerEmail;
        public Admindashboard(string customerEmail)
        {
            InitializeComponent();
            CustomerEmail = customerEmail;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cuiButton1_Click(object sender, EventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.Show();
            this.Hide();
        }



        private void LoadEmployees()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT EmployeeID, FullName, ContactNumber, Email, Role, NICNumber, Address, DateOfJoining, Password FROM Employees";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void Admindashboard_Load(object sender, EventArgs e)
        {
            LoadEmployees();
            LoadCustomer();
        }

        private void LoadCustomer()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT name, phonenumber, email FROM customerR";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt1 = new DataTable();
                adapter.Fill(dt1);
                dataGridView2.DataSource = dt1;
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cuiButton5_Click(object sender, EventArgs e)
        {
            Cdashboard cdashboard = new Cdashboard(customerName);
            cdashboard.Show();
            this.Hide();
        }

        private void cuiButton4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(CustomerEmail);
            form1.Show();
            this.Hide();
            return;
        }
    }
}
