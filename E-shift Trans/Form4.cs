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
    public partial class Form4 : Form
    {
        private string connectionString = @"Data Source=DESKTOP-86QTNGL;Initial Catalog=Admin;Integrated Security=True";
        private int loggedInEmployeeID;
        private string customerName;
        private string CustomerEmail;
        public Form4(int employeeID, string customerEmail)
        {
            InitializeComponent();
            loggedInEmployeeID = employeeID;
            LoadPendingBookings();
            CustomerEmail = customerEmail;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
        private void LoadPendingBookings()
        {
            string query = @"SELECT BookingID, PickupLocation, DeliveryLocation, PickupDate, PreferredTime, Goods, Vehicle, TotalCost 
                     FROM CustomerBookings 
                     WHERE BookingStatus = 'Pending'";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, con))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                if (!dataGridView1.Columns.Contains("Accept"))
                {
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.HeaderText = "Action";
                    btn.Text = "Accept";
                    btn.UseColumnTextForButtonValue = true;
                    btn.Name = "Accept";
                    dataGridView1.Columns.Add(btn);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Accept"].Index && e.RowIndex >= 0)
            {
                int bookingID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["BookingID"].Value);

                AssignBookingToEmployee(bookingID, loggedInEmployeeID); // Replace with actual logged-in employee ID
            }
        }

        private void AssignBookingToEmployee(int bookingId, int employeeId)
        {
            string query = @"UPDATE CustomerBookings
                     SET AssignedEmployeeID = @empId, BookingStatus = 'Accepted'
                     WHERE BookingID = @bid";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@empId", employeeId);
                cmd.Parameters.AddWithValue("@bid", bookingId);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Booking accepted successfully!");
                    LoadPendingBookings(); // Refresh grid
                }
                else
                {
                    MessageBox.Show("Failed to accept booking.");
                }
            }
        }

        private void cuiButton1_Click(object sender, EventArgs e)
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

        private void cuiButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
