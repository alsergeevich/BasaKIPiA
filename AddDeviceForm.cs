using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasaKIPiA
{
    public partial class AddDeviceForm : Form
    {
        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataReader dr;
        string sqlQuery1 = "SELECT * FROM types ORDER BY type";
        string sqlQuery2 = "SELECT * FROM manufacturers ORDER BY title";
        string nameObject;
        string nameObjectNonEdit;
        MetodsWorkDataBase mwdb = new MetodsWorkDataBase();
        string dataPoslPov;
        string intervPoverki;
        string nextPov;

        public AddDeviceForm(string nameObject)
        {
            InitializeComponent();
            this.nameObject = nameObject;
            nameObjectNonEdit = nameObject;
            txb_Model.AutoCompleteCustomSource = D.collection;
            txb_KlassTochn.AutoCompleteCustomSource = D.collection;
            txb_Predel.AutoCompleteCustomSource = D.collection;
            txb_EdinIzm.AutoCompleteCustomSource = D.collection;
            
            txb_NextPov.AutoCompleteCustomSource = D.collection;
        }

        private void populateCbxTypes()
        {
            using (conn = new SQLiteConnection("Data Source=" + D.DATA + ";Version=3;"))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = sqlQuery1;
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    cbx_Type.Items.Add("--");
                    while (dr.Read())
                    {
                        cbx_Type.Items.Add(dr["type"]);
                    }
                    cbx_Type.SelectedIndex = 0;
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }


        }


        private void populateCbxManufacturers()
        {
            using (conn = new SQLiteConnection("Data Source=" + D.DATA + ";Version=3;"))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = sqlQuery2;
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    cbx_Manufacturer.Items.Add("--");
                    while (dr.Read())
                    {
                        cbx_Manufacturer.Items.Add(dr["title"]);
                    }
                    cbx_Manufacturer.SelectedIndex = 0;
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }

        }

        private void AddDeviceForm_Load(object sender, EventArgs e)
        {
            populateCbxTypes();
            populateCbxManufacturers();
            nameObject = nameObject.Replace("_", " ");
            txb_Object.Text = nameObject;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (poverkaFormat())
            {
                return;
            }

            for(int i = 0; i < nudKolichestvo.Value; i++)
            {
                if (mwdb.SaveNewDevice(cbx_Type.SelectedItem.ToString(), txb_Model.Text.Trim(), cbx_Manufacturer.SelectedItem.ToString(), txb_FabricNumber.Text.Trim(), txb_KlassTochn.Text.Trim(), txb_KolvoIzmKan.Text.Trim(), txb_Predel.Text.Trim(), txb_EdinIzm.Text.Trim(), dataPoslPov,
                intervPoverki, txb_NextPov.Text.Trim(), txb_Object.Text.Trim(), txb_Poziciya.Text.Trim(), nameObjectNonEdit))
                {
                    this.DialogResult = DialogResult.OK;

                }
                else
                {
                    this.DialogResult = DialogResult.No;
                }
            }
        }


        private void txb_Model_LostFocus(object sender, System.EventArgs e)
        {
            if (txb_Model.Text != "" || !D.collection.Contains(txb_Model.Text.Trim()))
            {
                D.collection.Add(txb_Model.Text.Trim());
            }
        }



        private void txb_KlassTochn_LostFocus(object sender, System.EventArgs e)
        {
            if (txb_KlassTochn.Text != "" || !D.collection.Contains(txb_KlassTochn.Text.Trim()))
            {
                D.collection.Add(txb_KlassTochn.Text.Trim());
            }
        }

        private void txb_Predel_LostFocus(object sender, System.EventArgs e)
        {
            if(txb_Predel.Text != "" || !D.collection.Contains(txb_Predel.Text.Trim()))
            {
                D.collection.Add(txb_Predel.Text.Trim());
            }
        }

        private void txb_EdinIzm_LostFocus(object sender, System.EventArgs e)
        {
            if(txb_EdinIzm.Text != "" || !D.collection.Contains(txb_EdinIzm.Text.Trim()))
            {
                D.collection.Add(txb_EdinIzm.Text.Trim());
            }
        }

        

        private bool poverkaFormat()
        {
            bool flag = false;
            if(dtpYearPoverka.Value.Year > DateTime.Now.Year)
            {
                MessageBox.Show("Год предыдущей поверки не может быть больше текущего");
                flag = true;
                return flag;
            }

            nextPov = nudKvartal.Value.ToString() + " " + txbKvString.Text + " " + (dtpYearPoverka.Value.Year + ((int)nudInterval.Value / 12)).ToString();
            txb_NextPov.Text = nextPov;

            intervPoverki = nudInterval.Value.ToString();

            dataPoslPov = nudKvartal.Value.ToString() + " " + txbKvString.Text + " " + (dtpYearPoverka.Value.Year).ToString();

            return flag;

        }

        private void nudKvartal_ValueChanged(object sender, EventArgs e)
        {
            poverkaFormat();
        }

        private void dtpYearPoverka_ValueChanged(object sender, EventArgs e)
        {
            poverkaFormat();
        }

        private void nudInterval_ValueChanged(object sender, EventArgs e)
        {
            poverkaFormat();
        }
    }
}
