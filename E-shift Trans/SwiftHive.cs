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
    public partial class SwiftHive : Form
    {
        string connectionString = "Data Source=DESKTOP-86QTNGL;Initial Catalog=Admin;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        private string pickupLocation;
        private string deliveryLocation;
        private DateTime pickupDate;
        private string preferredTime;
        private string specialInstruction = "";
        

        public SwiftHive(string pickupLocation, string deliveryLocation, DateTime pickupDate, string preferredTime)
        {
            InitializeComponent();
           

            this.pickupLocation = pickupLocation;
            this.deliveryLocation = deliveryLocation;
            this.pickupDate = pickupDate;
            this.preferredTime = preferredTime;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void SwiftHive_Load(object sender, EventArgs e)
        {
        }
        private void button6_Click(object sender, EventArgs e)
        { 
        
        }
            
    
        private string selectedGood = "";
        private void txtfurniture_Click(object sender, EventArgs e)
        {
            selectedGood = "Furniture";
        }

        private void txtboxes_Click(object sender, EventArgs e)
        {
            selectedGood = "Boxes";
        }

        private void txtelectronics_Click(object sender, EventArgs e)
        {
            selectedGood = "Electronics";
        }

        private void txtvechicles_Click(object sender, EventArgs e)
        {
            selectedGood = "Vechicles";
        }

        private void txtoffiecemove_Click(object sender, EventArgs e)
        {
            selectedGood = "Offiece Move";
        }

        private string selectedVehicle = "";
        private int vehicleCost = 0;
        private void button8_Click(object sender, EventArgs e)
        {
            selectedVehicle = "lorry";
            vehicleCost = 10000;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            selectedVehicle = "heavy truck";
            vehicleCost = 8000;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            selectedVehicle = "medium Truck";
            vehicleCost = 5000;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            selectedVehicle = "Mini Truck";
            vehicleCost = 3000;

        }

        private string selectedPackage = "";
        private int packageCost = 0;
        private void Rpremium_CheckedChanged(object sender, EventArgs e)
        {
            if (Rpremium.Checked)
            {
                selectedPackage = "Premium";
                packageCost = 6000;
            }
        }

        private void Rspecial_CheckedChanged(object sender, EventArgs e)
        {
            if (Rspecial.Checked)
            {
                selectedPackage = "Premium";
                packageCost = 3000;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (Rnopackage.Checked)
            {
                selectedPackage = "Premium";
                packageCost = 00;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void cuiButton6_Click(object sender, EventArgs e)
        {
            int totalCost = vehicleCost + packageCost;

            string query = "INSERT INTO CustomerBookings (PickupLocation, DeliveryLocation, PickupDate, PreferredTime, Goods, Vehicle, VehicleCost, Package, PackageCost, SpecialInstruction, TotalCost) " +
                           "VALUES (@Pickup, @Delivery, @PickupDate, @Time, @Goods, @Vehicle, @VehicleCost, @Package, @PackageCost, @Instruction, @TotalCost)";

            SqlConnection con = new SqlConnection(connectionString);

            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Pickup", pickupLocation);
                    cmd.Parameters.AddWithValue("@Delivery", deliveryLocation);
                    cmd.Parameters.AddWithValue("@PickupDate", pickupDate);
                    cmd.Parameters.AddWithValue("@Time", preferredTime);
                    cmd.Parameters.AddWithValue("@Goods", selectedGood);
                    cmd.Parameters.AddWithValue("@Vehicle", selectedVehicle);
                    cmd.Parameters.AddWithValue("@VehicleCost", vehicleCost);
                    cmd.Parameters.AddWithValue("@Package", selectedPackage);
                    cmd.Parameters.AddWithValue("@PackageCost", packageCost);
                    cmd.Parameters.AddWithValue("@Instruction", specialInstruction);
                    cmd.Parameters.AddWithValue("@TotalCost", totalCost);

                    cmd.ExecuteNonQuery();
                }

                // Fetch and display data in DataGridView
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CustomerBookings", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();
            }

            MessageBox.Show("Booking saved successfully and summary loaded!");
        }

        private void cuiPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cuiButton4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
            return;
        }

        private void cuiPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

