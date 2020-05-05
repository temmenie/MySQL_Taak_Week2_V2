using MySql.Data.MySqlClient;
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

namespace MySQL_Taak_Week2_V2
{
    public partial class Form1 : Form
    {
        ConnectionStringSettingsCollection connectionStringSettings = new ConnectionStringSettingsCollection();
        Dictionary<string, string> connStringsDict = new Dictionary<string, string>();

        string mySqlConnStr = null;
        MySqlConnection mySqlConn;
        MySqlCommand mySqlComm;

        public Form1()
        {
            InitializeComponent();
            connectionStringSettings = GetConnectionStrings();
            connStringsDict = UpdateConnectionsComboBox(cmbMySQLConnecties, connectionStringSettings);
            mySqlConnStr = cmbMySQLConnecties.SelectedValue.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private Dictionary<string, string> UpdateConnectionsComboBox(ComboBox cb, ConnectionStringSettingsCollection cssc)
        {
            Dictionary<string, string> csd = new Dictionary<string, string>();

            if (cssc != null)
            {
                foreach (ConnectionStringSettings cs in cssc)
                {
                    csd.Add(cs.Name, cs.ConnectionString);
                }

                cb.DataSource = new BindingSource(csd, null);
                cb.DisplayMember = "Key";
                cb.ValueMember = "Value";
            }
            else
            {
                cb.Enabled = false;
            }
            return csd;
        }
        private ConnectionStringSettingsCollection GetConnectionStrings()
        {
            ConnectionStringSettingsCollection settings = new ConnectionStringSettingsCollection();

            try
            {
                settings = ConfigurationManager.ConnectionStrings;
            }
            catch (ConfigurationErrorsException err)
            {
                MessageBox.Show(err.Message, "Configuratie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return settings;
        }

        private void BtnOpenMySql_Click(object sender, EventArgs e)
        {
            try
            {
                mySqlConn = OpenMySQLverbinding(mySqlConnStr);

                try
                {
                    if (mySqlConn.State == ConnectionState.Open)
                    {
                        SetConnectionLabelTextAndColor("VERBONDEN", Color.Green);
                    }
                    else
                    {
                        SetConnectionLabelTextAndColor("NIET VERBONDEN", Color.Red);
                    }
                }
                catch (NullReferenceException err)
                {
                    SetConnectionLabelTextAndColor("NIET VERBONDEN", Color.Red);
                    MessageBox.Show(err.Message, "MySQL Connectie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySqlException)
            {
                SetConnectionLabelTextAndColor("NIET VERBONDEN", Color.Red);
            }

        }
        private void SetConnectionLabelTextAndColor(string tekst, Color color)
        {
            ConnectionStatusLabel.Text = "CONNECTION STATUS: " + tekst;
            ConnectionStatusLabel.BackColor = color;
        }

        private MySqlConnection OpenMySQLverbinding(string connectieString)
        {
            MySqlConnection mijnVerbinding = null;

            try
            {
                mijnVerbinding = new MySqlConnection(connectieString);

                try
                {
                    mijnVerbinding.Open();
                }
                catch (MySqlException)
                {
                    MessageBox.Show("Fout bij het maken van verbinding met database", "MySQL Connectie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (mijnVerbinding != null)
                    {
                        mijnVerbinding.Dispose();
                    }
                }
            }
            catch (ArgumentException err)
            {
                MessageBox.Show(err.Message, "SQL-verbinding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (mijnVerbinding != null)
                {
                    mijnVerbinding.Dispose();
                }
            }

            return mijnVerbinding;
        }

        private void cmbMySQLConnecties_SelectedIndexChanged(object sender, EventArgs e)
        {
            mySqlConnStr = cmbMySQLConnecties.SelectedValue.ToString();
            String[] strlist = mySqlConnStr.Split(';');
            string str = string.Empty;

            foreach (String s in strlist)
            {
                if (!s.Contains("password"))
                {
                    str += s + " , ";
                }
            }

        }

        private void BtnSluitMySql_Click(object sender, EventArgs e)
        {
            if (SluitMySQLverbinding(mySqlConn))
            {
                SetConnectionLabelTextAndColor("NIET VERBONDEN", Color.Red);
            }
        }

        private bool SluitMySQLverbinding(MySqlConnection mijnVerbinding)
        {
            bool succes = false;

            if (mijnVerbinding.State == ConnectionState.Open)
            {
                try
                {
                    mijnVerbinding.Close();
                    succes = true;
                }
                catch (MySqlException)
                {
                    MessageBox.Show("Fout bij het sluiten van verbinding met database", "MySQL Connectie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return succes;
        }

        private void ConnectionStatusLabel_Click(object sender, EventArgs e)
        {

        }

        private void LaadBestellingen_click(object sender, EventArgs e)
        {
            try
            {
                using (mySqlConn = OpenMySQLverbinding(mySqlConnStr))
                {
                    if (mySqlConn.State == ConnectionState.Open)
                    {
                        mySqlComm = new MySqlCommand();
                        mySqlComm.Connection = mySqlConn;
                        mySqlComm.CommandText = "select * from orders;";
                        mySqlComm.CommandType = CommandType.Text;

                        SetConnectionLabelTextAndColor("VERBONDEN", Color.Green);

                        using (MySqlDataReader mySqlDr = mySqlComm.ExecuteReader())
                        {
                            while (mySqlDr.Read())
                            {
                                textBox1.Text += (int)mySqlDr[0] + "\t" 
                                    + Convert.ToString((DateTime)mySqlDr[1]) + "\t" 
                                    + (int)mySqlDr[2] + "\t" 
                                    + Convert.ToByte(mySqlDr[3]) + "\t" 
                                    + (int)mySqlDr[4] + "\t" 
                                    + mySqlDr[5] + "\r\n";
                            }

                            mySqlDr.Close();
                        }

                        mySqlConn.Close();
                    }
                    else
                    {
                        SetConnectionLabelTextAndColor("NIET VERBONDEN", Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException || ex is MySqlException)
                {
                    MessageBox.Show(ex.Message, "MySQL Connectie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else throw;
            }
        }

        private void VoegProductToe_Click(object sender, EventArgs e)
        {
            if (ProductNaamTextBox.Text != string.Empty && ProductStockTextBox.Text != string.Empty)
            {
                try
                {
                    using (mySqlConn = OpenMySQLverbinding(mySqlConnStr))
                    {
                        if (mySqlConn.State == ConnectionState.Open)
                        {
                            mySqlComm = new MySqlCommand();
                            mySqlComm.Connection = mySqlConn;
                            mySqlComm.Parameters.AddWithValue("productNaam", ProductNaamTextBox.Text);
                            mySqlComm.Parameters.AddWithValue("ProductStock", ProductStockTextBox.Text);
                            mySqlComm.CommandText = "INSERT INTO producten(productNaam, productStock) VALUES(@productNaam, @ProductStock)";
                            mySqlComm.CommandType = CommandType.Text;

                            SetConnectionLabelTextAndColor("VERBONDEN", Color.Green);

                            try
                            {
                                mySqlComm.ExecuteNonQuery();
                                MessageBox.Show("PRODUCT: " + ProductNaamTextBox.Text.ToUpper() + " IS TOEGEVOEGD", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (MySqlException)
                            {
                                MessageBox.Show("Fout bij schrijven naar database", "MySQL Connectie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            mySqlConn.Close();
                        }
                        else
                        {
                            SetConnectionLabelTextAndColor("NIET VERBONDEN", Color.Red);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex is NullReferenceException || ex is MySqlException)
                    {
                        MessageBox.Show(ex.Message, "MySQL Connectie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else throw;
                }
            }
            else
                MessageBox.Show("EMPTY INPUT", "INPUT ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LeesTabelProducten_Click(object sender, EventArgs e)
        {
            InlezenEnWegSchrijvenInTabel();
        }
        private void VerwijderProductBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int productID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ProductIDCol"].Value);

                DialogResult boxResult = MessageBox.Show("WILT U HET PRODUCT:" + Convert.ToString(dataGridView1.SelectedRows[0].Cells["ProductNaamCol"].Value).ToUpper() + " VERWIJDEREN?", "INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (boxResult == DialogResult.Yes)
                {
                    dataGridView1.Rows.Clear();
                    try
                    {
                        using (mySqlConn = OpenMySQLverbinding(mySqlConnStr))
                        {
                            if (mySqlConn.State == ConnectionState.Open)
                            {
                                using (mySqlComm = new MySqlCommand())
                                {
                                    mySqlComm.Connection = mySqlConn;
                                    mySqlComm.Parameters.AddWithValue("@productID", productID);
                                    mySqlComm.CommandText = "DELETE FROM producten WHERE productID = @productID";
                                    mySqlComm.CommandType = CommandType.Text;

                                    mySqlComm.ExecuteNonQuery();

                                    mySqlConn.Close();
                                    InlezenEnWegSchrijvenInTabel();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Geen connectie met database ", "MySQL Connectie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is NullReferenceException || ex is MySqlException)
                        {
                            MessageBox.Show(ex.Message, "MySQL Connectie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else throw;
                    }
                }
            }
            else
                if (dataGridView1.SelectedRows.Count == 0)
                MessageBox.Show("U MOET EERST EEN RIJ SELECTEREN", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                if (dataGridView1.SelectedRows.Count > 1)
                MessageBox.Show("VERWIJDER RIJ PER RIJ", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void InlezenEnWegSchrijvenInTabel()
        {
            try
            {
                using (mySqlConn = OpenMySQLverbinding(mySqlConnStr))
                {
                    dataGridView1.Rows.Clear();
                    if (mySqlConn.State == ConnectionState.Open)
                    {
                        mySqlComm = new MySqlCommand();
                        mySqlComm.Connection = mySqlConn;
                        mySqlComm.CommandText = "select * from producten;";
                        mySqlComm.CommandType = CommandType.Text;

                        SetConnectionLabelTextAndColor("VERBONDEN", Color.Green);

                        using (MySqlDataReader mySqlDr = mySqlComm.ExecuteReader())
                        {
                            while (mySqlDr.Read())
                            {
                                dataGridView1.Rows.Add(mySqlDr[0].ToString(), mySqlDr[1].ToString(), mySqlDr[2].ToString(), mySqlDr[3].ToString());
                            }

                            mySqlDr.Close();
                        }
                        dataGridView1.Visible = true;
                        mySqlConn.Close();
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException || ex is MySqlException)
                {
                    MessageBox.Show(ex.Message, "MySQL Connectie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else throw;
            }
        }
    }
}
