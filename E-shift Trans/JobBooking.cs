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
    public partial class JobBooking : Form
    {
        private string _customerName;
        public JobBooking(string customName)
        {
            InitializeComponent();
        }

        private void JobBooking_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(txtPickupLocation.Text) ||
                string.IsNullOrWhiteSpace(txtDeliveryLocation.Text) ||
                comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all the required information before continuing.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Pass the entered data to SwiftHive
                SwiftHive swiftHive = new SwiftHive(
                    txtPickupLocation.Text,
                    txtDeliveryLocation.Text,
                    dateTimePicker1.Value,
                    comboBox1.SelectedItem.ToString()
                );

                swiftHive.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ParcelSwift parcelSwift = new ParcelSwift(_customerName);
            parcelSwift.Show();
            this.Hide();

        }
    }
}