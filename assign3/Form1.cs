using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assign3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        // Runs when the Home button is clicked - loads the home screen
        private void btnHome_Click(object sender, EventArgs e)
        {
            ShowScreen(new UC_Home());
        }

        // Runs when the program first opens
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Runs when the Inventory button is clicked - loads the inventory screen
        private void btnInv_Click(object sender, EventArgs e)
        {
            ShowScreen(new UC_Inventory());
        }

        // Swaps the current screen inside the content panel with a new one
        private void ShowScreen(UserControl newScreen)
        {
            // Loop through existing controls and dispose them to free memory
            foreach (Control ctrl in pnlContent.Controls)
            {
                ctrl.Dispose();
            }

            // Clear the panel and fill it with the new screen
            pnlContent.Controls.Clear();
            newScreen.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(newScreen);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

}

