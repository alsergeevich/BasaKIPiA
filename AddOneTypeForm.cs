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
    public partial class AddOneTypeForm : Form
    {
        MetodsWorkDataBase mwdb = new MetodsWorkDataBase();
        public string dbPath;
        public AddOneTypeForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txbType.Text.Trim() == "")
            {
                MessageBox.Show("Заполните поле");
                return;
            }

            if (mwdb.SaveNewTypes(txbType.Text.Trim(), D.DATA))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.No;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
