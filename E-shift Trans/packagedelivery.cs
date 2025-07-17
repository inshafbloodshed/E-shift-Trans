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
    public partial class packagedelivery : Form
    {
        private string pickuploaction, deliveryLocation, readyForPickup;
        private int deliverySpeedCost = 0;

        private string selectedItem = "";
        private string selectedVehicle = "";
        private int vehicleCost = 0;
        

        private readonly string connectionString = "Data Source=DESKTOP-86QTNGL;Initial Catalog=Admin;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";


        public packagedelivery(string pickup, string delivery, string readyPickup, int speedCost)

        {
            InitializeComponent();
            pickuploaction = pickup;
            deliveryLocation = delivery;
            readyForPickup = readyPickup;
            deliverySpeedCost = speedCost;
            

        }


        private void button1_Click(object sender, EventArgs e)
        {
            selectedItem = "Document";
        }

        private void motorcycle_Click(object sender, EventArgs e)
        {
            selectedVehicle = "motorcycle";
            vehicleCost = 1000;
        }

        private void threewheeler_Click(object sender, EventArgs e)
        {
            selectedVehicle = "threewheeler";
            vehicleCost = 2000;
        }

        private void minivan_Click(object sender, EventArgs e)
        {
            selectedVehicle = "minivan";
            vehicleCost = 3000;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedItem = "small package";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectedItem = "food";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            selectedItem = "Gift";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            selectedItem = "Medicine";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            selectedItem = "Package";
        }

        private void txtLength_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtwidth_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtweight_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtinstruction_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (selectedItem == "" || selectedVehicle == "" || txtLength.Text == "" || txtwidth.Text == "" || txtHeight.Text == "" || txtweight.Text == "")
            {
                MessageBox.Show("Please fill all package details and selections.");
                return;
            }

            if (!double.TryParse(txtLength.Text, out double length) || length <= 0 ||
                !double.TryParse(txtwidth.Text, out double width) || width <= 0 ||
                !double.TryParse(txtHeight.Text, out double height) || height <= 0 ||
                !double.TryParse(txtweight.Text, out double weight) || weight <= 0)
            {
                MessageBox.Show("Please enter valid positive numbers for dimensions and weight.");
                return;
            }

            string specialInstruction = txtinstruction.Text;

            double volume = length * width * height;
            int totalCost = deliverySpeedCost + vehicleCost ;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "INSERT INTO ParcelBookings (PickupLocation, DeliveryLocation, ReadyForPickup, ItemType, Vehicle, VehicleCost, Length, Width, Height, WeightKg, SpecialInstruction, EstimatedCost, CreatedDate) " +
                               "VALUES (@Pickup, @Delivery, @ReadyForPickup, @ItemType, @Vehicle, @VehicleCost, @Length, @Width, @Height, @WeightKg, @Instruction, @EstimatedCost, @CreatedDate)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Pickup", pickuploaction);
                cmd.Parameters.AddWithValue("@Delivery", deliveryLocation);
                cmd.Parameters.AddWithValue("@ReadyForPickup", readyForPickup);
                cmd.Parameters.AddWithValue("@ItemType", selectedItem);
                cmd.Parameters.AddWithValue("@Vehicle", selectedVehicle);
                cmd.Parameters.AddWithValue("@VehicleCost", vehicleCost);
                cmd.Parameters.AddWithValue("@Length", length);
                cmd.Parameters.AddWithValue("@Width", width);
                cmd.Parameters.AddWithValue("@Height", height);
                cmd.Parameters.AddWithValue("@WeightKg", weight);
                cmd.Parameters.AddWithValue("@Instruction", specialInstruction);
                cmd.Parameters.AddWithValue("@EstimatedCost", totalCost);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                cmd.ExecuteNonQuery();


                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ParcelBookings", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                MessageBox.Show("Parcel booked! Total Cost: " + totalCost);
            }
        }

           
        
    
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void packagedelivery_Load(object sender, EventArgs e)
        {

        }
    }
}
