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

namespace E_shift_Trans
{
    public partial class Cdashboard : Form
    {
       
        private string connectionString = @"Data Source=DESKTOP-86QTNGL;Initial Catalog=Admin;Integrated Security=True";
        private string _customerName;
       

        public Cdashboard(string  customerName )
        {
            InitializeComponent();
            _customerName = customerName;
            DisplayWelcomeMessage();
            
        }


        private void Cdashboard_Load(object sender, EventArgs e)
        {
            LoadCustomerBookings();
            LoadCustomerParcelBooking();
        }

        private void LoadCustomerBookings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT BookingID, BookingStatus, PickupLocation, DeliveryLocation, PickupDate, PreferredTime, Goods, Vehicle, VehicleCost, Package, PackageCost, SpecialInstruction, TotalCost FROM CustomerBookings";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }

        }

        private void LoadCustomerParcelBooking()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM ParcelBookings";  // Removed WHERE filter
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;  // Use second grid
            }
        }


        private void DisplayWelcomeMessage()
        {
            label2.Text = $"Hello, {_customerName}!";
            label2.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            label2.ForeColor = Color.White;
            // Center the label if you want
            // Use panel1 width instead of form
            label2.Left = (cuiPanel1.ClientSize.Width - label2.PreferredWidth) / 2;

            

            label2.BringToFront(); // In case it's hidden behind anything
            label2.Refresh();
        }


        private void label2_Click(object sender, EventArgs e)
        {
            label2.Text = $"Hello, {_customerName}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JobBooking jobBooking = new JobBooking(_customerName);
            jobBooking.Show();
            this.Hide();    
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cuiButton1_Click(object sender, EventArgs e)
        {
            JobBooking jobBooking = new JobBooking(_customerName);
            jobBooking.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cuiButton2_Click(object sender, EventArgs e)
        {

        }

        private void cuiButton3_Click(object sender, EventArgs e)
        {
            ParcelSwift parcelSwift = new ParcelSwift(_customerName);   
            parcelSwift.Show();
            this.Hide();
        }

        private void cuiPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cuiPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cuiPictureBox2_Load(object sender, EventArgs e)
        {
            
        }

        private void cuiLabel3_Load(object sender, EventArgs e)
        {

        }

        private void cuiLabel4_Load(object sender, EventArgs e)
        {
            
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();   
            this.Hide();
            return;
        }

        private void cuiButton4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide(); return;
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            
        }
    }
}
