using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasaKIPiA
{
    public partial class AddManufForm : Form
    {
        MetodsWorkDataBase mwdb = new MetodsWorkDataBase();
        private string sqlQueryAll = "SELECT *FROM manufacturers ORDER BY title";
        private int id;
        ArrayList allData;

        public AddManufForm()
        {
            InitializeComponent();
        }


        void dgwSettings()
        {
            try
            {
                dgw_Manufactured.Columns["id"].Visible = false;
                dgw_Manufactured.Columns["title"].HeaderText = "Наименование производителя";
            }
            catch
            {

            }

        }

        private void AddManufForm_Load(object sender, EventArgs e)
        {
            allData = mwdb.ReadAllData(sqlQueryAll, D.DATA);
            dgw_Manufactured.DataSource = allData;
            if(allData.Count != 0)
            {
                dgwSettings();
            }
            
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            AddOneManufForm addOneManufForm = new AddOneManufForm();

            if (addOneManufForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Новый производитель добавлен");
                allData.Clear();
                allData = mwdb.ReadAllData(sqlQueryAll, D.DATA);
                dgw_Manufactured.DataSource = allData;
                dgwSettings();


            }
            else
            {
                MessageBox.Show("Ошибка при добавлении");
            }
        }

        private void dgw_Manufactured_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            id = Int32.Parse(dgw_Manufactured.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (mwdb.deleteData(id, "manufacturers", D.DATA) == true)
            {
                MessageBox.Show("Запись удалена");
                allData.Clear();
                allData = mwdb.ReadAllData(sqlQueryAll, D.DATA);
                dgw_Manufactured.DataSource = allData;
                dgwSettings();

            }
            else
            {
                MessageBox.Show("Ошибка при удалении записи");
            }
        }

        private void dgw_Manufactured_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dgw_Manufactured.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
            {
                this.dgw_Manufactured.Rows[index].HeaderCell.Value = indexStr;
                dgw_Manufactured.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            }
        }
    }
}
