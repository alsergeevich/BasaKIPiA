using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasaKIPiA
{
    public partial class AddOneManufForm : Form
    {

        MetodsWorkDataBase mwdb = new MetodsWorkDataBase();
        public AddOneManufForm()
        {
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {

            if (txb_Manuf.Text.Trim() == "")
            {
                MessageBox.Show("Заполните поле");
                return;
            }

            if (mwdb.SaveNewManufactured(txb_Manuf.Text.Trim(), D.DATA))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.No;
            }

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
