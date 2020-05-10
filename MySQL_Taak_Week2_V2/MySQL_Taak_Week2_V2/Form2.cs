using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MySQL_Taak_Week2_V2
{
    public partial class Form2 : Form
    {
        public string klantNaam;
        public string productNaam;
        public int productAantal;

        public Form2()
        {
            InitializeComponent();
        }

        public void VulKlantenComboBox(List<string> klanten)
        {
            foreach (string klant in klanten)
            {
                KlantenComboBox.Items.Add(klant);
            }
        }
        public void VulProductenComboBox(List<string> producten)
        {
            foreach (string product in producten)
            {
                ProductenComboBox.Items.Add(product);
            }
        }
        private void VoegOrderToeBtn_Click(object sender, EventArgs e)
        {
            if (this.productNaam != string.Empty && this.klantNaam != string.Empty && this.productAantal > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("NO EMPTY/ZERO BOXES ARE TOLERATED", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void KlantenComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.klantNaam = KlantenComboBox.SelectedItem.ToString();
        }
        private void ProductenComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.productNaam = ProductenComboBox.SelectedItem.ToString();
        }
        private void AantalNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.productAantal = Convert.ToInt32(AantalNumUpDown.Value);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}