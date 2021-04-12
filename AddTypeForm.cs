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
    public partial class AddTypeForm : Form //в этом классе запускается форма для работы со справочником типов
    {
        
        MetodsWorkDataBase mwdb = new MetodsWorkDataBase();
        private string sqlQueryAll = "SELECT *FROM types ORDER BY type";
        private int id;
        ArrayList allData;

        public AddTypeForm()
        {
            InitializeComponent();
        }

        void dgwSettings()
        {
            try
            {
                dgwTypes.Columns["id"].Visible = false;
                dgwTypes.Columns["type"].HeaderText = "Наименование типа";
            }
            catch
            {

            }
            
        }

        private void AddTypeForm_Load(object sender, EventArgs e)
        {
            allData = mwdb.ReadAllData(sqlQueryAll, D.DATA);
            dgwTypes.DataSource = allData;
            if(allData.Count != 0)
            {
                dgwSettings();
            }

                
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddOneTypeForm addOneTypeForm = new AddOneTypeForm();
            
            if(addOneTypeForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Новый тип добавлен");
                allData.Clear();
                allData = mwdb.ReadAllData(sqlQueryAll, D.DATA);
                dgwTypes.DataSource = allData;
                dgwSettings();


            }
            else
            {
                MessageBox.Show("Ошибка при добавлении");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

      

        private void dgwTypes_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            id = Int32.Parse(dgwTypes.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(mwdb.deleteData(id, "types", D.DATA) == true)
            {
                MessageBox.Show("Запись удалена");
                allData.Clear();
                allData = mwdb.ReadAllData(sqlQueryAll, D.DATA);
                dgwTypes.DataSource = allData;
                dgwSettings();

            }
            else
            {
                MessageBox.Show("Ошибка при удалении записи");
            }
        }

        private void dgwTypes_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e) //нумерация строк
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dgwTypes.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
            {
                this.dgwTypes.Rows[index].HeaderCell.Value = indexStr;
                dgwTypes.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            }
        }
    }
}
