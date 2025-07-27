using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_shift_Trans
{
    public partial class ParcelSwift : Form
    {
        private string _customerName;
        private int deliverySpeedCost = 0;
       
        public ParcelSwift(string customerName)
        {
            InitializeComponent();
            txtpicup.Text = "Enter Pickup Location";
            txtlocation.Text = "Enter Delivery Location";
            cready.Items.AddRange(new string[] { "Today", "Tomorrow", "Next Week" });
            cready.Text = "None";
            _customerName = customerName;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ParcelSwift_Load(object sender, EventArgs e)
        {

        }

        private void txtpicup_TextChanged(object sender, EventArgs e)
        {
            if (txtlocation.Text == "Enter Pickup Location")
                txtlocation.Text = "";
        }

        private void txtlocation_TextChanged(object sender, EventArgs e)
        {
            if (txtlocation.Text == "Enter Delivery Location")
                txtlocation.Text = "";
        }

        private void txtExpress_Click(object sender, EventArgs e)
        {
            deliverySpeedCost = 3000;
        }

        private void txtSatndard_Click(object sender, EventArgs e)
        {
            deliverySpeedCost = 2000;
        }

        private void txtEconomy_Click(object sender, EventArgs e)
        {
            deliverySpeedCost = 1000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtpicup.Text == "" || txtlocation.Text == "" || cready.SelectedIndex == -1 || deliverySpeedCost == 0)
            {
                MessageBox.Show("Please complete all fields and select delivery speed.");
                return;
            }

            packagedelivery packagedelivery = new packagedelivery(txtpicup.Text,
                txtlocation.Text,
                cready.Text,
                deliverySpeedCost);
            packagedelivery.Show();
            this.Hide();
    
        }

        private void txtNIC_TextChanged(object sender, EventArgs e)
        {

        }

        private void cuiButton5_Click(object sender, EventArgs e)
        {
            deliverySpeedCost = 3000;
        }

        private void cuiButton6_Click(object sender, EventArgs e)
        {
            deliverySpeedCost = 1000;
        }

        private void cuiButton7_Click(object sender, EventArgs e)
        {
            deliverySpeedCost = 500;
        }

        private void cuiButton2_Click(object sender, EventArgs e)
        {

        }

        private void cuiButton3_Click(object sender, EventArgs e)
        {
            JobBooking jobBooking = new JobBooking(_customerName);
            jobBooking.Show();
            this.Hide();
            return;

        }

        private void cuiButton1_Click(object sender, EventArgs e)
        {
            Cdashboard cdashboard = new Cdashboard(_customerName);
            cdashboard.Show();
            this.Hide();

        }

        private void cuiButton8_Click(object sender, EventArgs e)
        {
            if (txtpicup.Text == "" || txtlocation.Text == "" || cready.SelectedIndex == -1 || deliverySpeedCost == 0)
            {
                MessageBox.Show("Please complete all fields and select delivery speed.");
                return;
            }

            packagedelivery packagedelivery = new packagedelivery(txtpicup.Text,
                txtlocation.Text,
                cready.Text,
                deliverySpeedCost);
            packagedelivery.Show();
            this.Hide();

        }

        private void cuiButton4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
            return;
        }
    }
}
