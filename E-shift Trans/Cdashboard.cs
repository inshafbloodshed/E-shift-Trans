using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private string _customerName;
        public Cdashboard(string customerName)
        {
            InitializeComponent();
           
            _customerName = customerName;
            DisplayWelcomeMessage();

        }

        private void Cdashboard_Load(object sender, EventArgs e)
        {

        }

        private void DisplayWelcomeMessage()
        {
            label2.Text = $"Hello, {_customerName}!";
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.ForeColor = Color.DarkBlue;
            // Center the label if you want
            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Text = $"welcome, {_customerName}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JobBooking jobBooking = new JobBooking();
            jobBooking.Show();
            this.Hide();    
        }
    }
}
