using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;
using DGVPrinterHelper;
using System.Reflection;

namespace BasaKIPiA
{
    
    public partial class MainForm : Form
    {
        MetodsWorkDataBase mwdb = new MetodsWorkDataBase();
        
        private string sqlQueryAll = "SELECT *FROM ";
        public string basePath;
        ArrayList allData;
        string nameObject;
        bool flag = false;
        int id;
        string type;
        string model;
        string manufacturer;
        string factory_number;
        string accuracy_class;
        string number_of_channels;
        string range;
        string units;
        string date_of_last_calibration;
        string calibration_interval;
        string date_of_next_calibration;
        string facility;
        string position;
        string file_format;
        string file_name;
        int indexrow = 0;

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void dgvSettings() //тут задаём настройки внешнего вида для DataGridView
        {
            try
            {
                dgw_ViewMain.Columns[0].Visible = false;
                dgw_ViewMain.Columns["type"].HeaderText = "Тип";
                dgw_ViewMain.Columns["type"].Width = 180;

                dgw_ViewMain.Columns["model"].HeaderText = "Модель";
                //dgw_ViewMain.Columns["model"].Width = 120;

                dgw_ViewMain.Columns["manufacturer"].HeaderText = "Производитель";
                //dgw_ViewMain.Columns["manufacturer"].Width = 150;

                dgw_ViewMain.Columns["factory_number"].HeaderText = "Заводской номер";
                //dgw_ViewMain.Columns["factory_number"].Width = 120;

                dgw_ViewMain.Columns["accuracy_class"].HeaderText = "Класс точности";
                //dgw_ViewMain.Columns["accuracy_class"].Width = 120;

                dgw_ViewMain.Columns["number_of_channels"].HeaderText = "Кол-во измер.каналов";
                //dgw_ViewMain.Columns["number_of_channels"].Width = 150;

                dgw_ViewMain.Columns["range"].HeaderText = "Диапазон измерения";
                //dgw_ViewMain.Columns["range"].Width = 120;

                dgw_ViewMain.Columns["units"].HeaderText = "Единицы измерения";
                //dgw_ViewMain.Columns["units"].Width = 120;

                dgw_ViewMain.Columns["date_of_last_calibration"].HeaderText = "Дата последней поверки";
                //dgw_ViewMain.Columns["date_of_last_calibration"].Width = 120;

                dgw_ViewMain.Columns["calibration_interval"].HeaderText = "Межповерочный интервал, мес.";
                //dgw_ViewMain.Columns["calibration_interval"].Width = 150;

                dgw_ViewMain.Columns["date_of_next_calibration"].HeaderText = "Дата следующей поверки";
                //dgw_ViewMain.Columns["date_of_next_calibration"].Width = 120;

                dgw_ViewMain.Columns["facility"].HeaderText = "Объект";
                dgw_ViewMain.Columns["facility"].Visible = false;
                //dgw_ViewMain.Columns["facility"].Width = 120;

                dgw_ViewMain.Columns["position"].HeaderText = "Место установки, позиция";
                dgw_ViewMain.Columns["position"].Width = 350;

                dgw_ViewMain.Columns["file"].Visible = false;
                dgw_ViewMain.Columns["file_format"].Visible = false;
                //dgw_ViewMain.Columns["file_name"].Visible = false;
                dgw_ViewMain.Columns["file_name"].HeaderText = "Свидетельство";
                dgw_ViewMain.Columns["file_name"].Width = 150;








            }
            catch
            {

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ExtensionMethods.DoubleBuffered(dgw_ViewMain, true);//включаем быструю отрисовку
            OpenCreateForm opcrform = new OpenCreateForm();
            opcrform.ShowDialog();
            allData = mwdb.ReadAllData(sqlQueryAll + "types", D.DATA);
            while (allData.Count <= 0)
            {
                MessageBox.Show("Необходимо заполнить справочник типов приборов");
                AddTypeForm addTypeForm = new AddTypeForm();
                addTypeForm.ShowDialog();
                allData = mwdb.ReadAllData(sqlQueryAll + "types", D.DATA);

            }

            allData.Clear();

            addItemsComboBox();

            
            
            
        }

        private void btn_EditDictionaryTypes_Click(object sender, EventArgs e)
        {
           
            AddTypeForm addTypeForm = new AddTypeForm();
            addTypeForm.ShowDialog();
        }

        private void btn_EditDictionaryManuf_Click(object sender, EventArgs e)
        {
            AddManufForm addManufForm = new AddManufForm();
            addManufForm.ShowDialog();
        }

        

        

        private void button3_Click(object sender, EventArgs e)
        {
            AddObjectForm addObjectForm = new AddObjectForm();
            addObjectForm.ShowDialog();
            addItemsComboBox();
        }


        private void addItemsComboBox()  //заполняем объектами из БД combobox на главной форме.
        {
            string sqlcommand = "select name from sqlite_master where type = 'table' and name != 'types' and name != 'manufacturers' and name != 'sqlite_sequence'";
            cbx_object.Items.Clear();
            SQLiteConnection con = new SQLiteConnection("Data Source=" + D.DATA + ";Version=3;");
            con.Open();
            SQLiteCommand comm = new SQLiteCommand(sqlcommand, con);
            SQLiteDataReader rd = comm.ExecuteReader();
            while(rd.Read())
            {
                cbx_object.Items.Add(rd["name"]);
                
            }
            

            con.Close();
            cbx_object.SelectedItem = nameObject;


        }

        private void cbx_object_SelectedIndexChanged(object sender, EventArgs e)
        {
            flag = true;
            nameObject = cbx_object.SelectedItem.ToString();
            allData = mwdb.ReadAllData(sqlQueryAll + nameObject + " ORDER BY type", D.DATA);                       
            dgw_ViewMain.DataSource = allData;
            dgvSettings();
            proverkaPoverki();
        }

        private void dgw_ViewMain_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dgw_ViewMain.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
            {
                this.dgw_ViewMain.Rows[index].HeaderCell.Value = indexStr;
                dgw_ViewMain.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                lblCount.Text = dgw_ViewMain.Rows.Count.ToString() + " " + "шт.";
            }

            }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if(flag == false || cbx_object.Items.Count == 0)
            {
                MessageBox.Show("Необходимо выбрать объект");
                return;
            }
            AddDeviceForm addDeviceForm = new AddDeviceForm(nameObject);
            if(addDeviceForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Новый прибор добавлен в базу");
                //allData.Clear();
                //allData = mwdb.ReadAllData(sqlQueryAll + nameObject + " ORDER BY type", D.DATA);
                //dgw_ViewMain.DataSource = allData;
                //dgvSettings();
                //proverkaPoverki();
                search();

            }
            else
            {
                MessageBox.Show("Ошибка при добавлении прибора в базу");
            }
        }

        private void dgw_ViewMain_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            id = Int32.Parse(dgw_ViewMain.Rows[e.RowIndex].Cells[0].Value.ToString());
            type = dgw_ViewMain.Rows[e.RowIndex].Cells[1].Value.ToString();
            model = dgw_ViewMain.Rows[e.RowIndex].Cells[2].Value.ToString();
            manufacturer = dgw_ViewMain.Rows[e.RowIndex].Cells[3].Value.ToString();
            factory_number = dgw_ViewMain.Rows[e.RowIndex].Cells[4].Value.ToString();
            accuracy_class = dgw_ViewMain.Rows[e.RowIndex].Cells[5].Value.ToString();
            number_of_channels = dgw_ViewMain.Rows[e.RowIndex].Cells[6].Value.ToString();
            range = dgw_ViewMain.Rows[e.RowIndex].Cells[7].Value.ToString();
            units = dgw_ViewMain.Rows[e.RowIndex].Cells[8].Value.ToString();
            date_of_last_calibration = dgw_ViewMain.Rows[e.RowIndex].Cells[9].Value.ToString();
            calibration_interval = dgw_ViewMain.Rows[e.RowIndex].Cells[10].Value.ToString();
            date_of_next_calibration = dgw_ViewMain.Rows[e.RowIndex].Cells[11].Value.ToString();
            facility = dgw_ViewMain.Rows[e.RowIndex].Cells[12].Value.ToString();
            position = dgw_ViewMain.Rows[e.RowIndex].Cells[13].Value.ToString();
            file_format = dgw_ViewMain.Rows[e.RowIndex].Cells[15].Value.ToString();
            file_name = dgw_ViewMain.Rows[e.RowIndex].Cells[16].Value.ToString();
            

        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if(flag == false || id == 0 || allData.Count == 0)
            {
                MessageBox.Show("Выберите объект или прибор");
                return;
            }

            EditDeviceForm editDeviceForm = new EditDeviceForm(id, type, model, manufacturer, factory_number, accuracy_class, number_of_channels, range, units, date_of_last_calibration, calibration_interval, date_of_next_calibration, facility, position, file_format, file_name, nameObject);
            if(editDeviceForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Данные прибора изменены");
                search();
                //allData.Clear();
                //allData = mwdb.ReadAllData(sqlQueryAll + nameObject + " ORDER BY type", D.DATA);
                //dgw_ViewMain.DataSource = allData;
                //dgvSettings();
                //proverkaPoverki();
                if (dgw_ViewMain.Rows.Count > 0)
                {
                    dgw_ViewMain.CurrentCell = dgw_ViewMain.Rows[indexrow].Cells[1];
                }
                
                
                
            }
            else
            {
                search();
                if (dgw_ViewMain.Rows.Count > 0)
                {
                    dgw_ViewMain.CurrentCell = dgw_ViewMain.Rows[indexrow].Cells[1];
                }
            }

            
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if(dgw_ViewMain.Rows.Count <= 0)
            {
                MessageBox.Show("Нет приборов для удаления");
                return;
            }
            DialogResult result = MessageBox.Show("Удалить прибор ?", "Удаление прибора", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if(result == DialogResult.No)
            {
                return;
            }
            if(mwdb.deleteData(id, nameObject, D.DATA) == true)
            {
                
                MessageBox.Show("Прибор удалён из базы");
                //allData.Clear();
                //allData = mwdb.ReadAllData(sqlQueryAll + nameObject + " ORDER BY type", D.DATA);
                //dgw_ViewMain.DataSource = allData;

                search();

                if(allData.Count != 0)
                {
                    dgvSettings();
                    proverkaPoverki();
                    search();

                }
                    
            }
            else
            {
                MessageBox.Show("Ошибка при удалении");
            }
        }

        
        

        private void search()
        {
            string sqlQuery = "SELECT * FROM " + nameObject + " WHERE [type] LIKE '%" + txb_Search.Text + "%' OR [model] LIKE '%" +
                txb_Search.Text + "%' OR [manufacturer] LIKE '%" + txb_Search.Text + "%' OR [range] LIKE '%"  + txb_Search.Text + "%' OR [calibration_interval] LIKE '%" +
                txb_Search.Text + "%' OR [date_of_next_calibration] LIKE '%" + txb_Search.Text + "%' OR [position] LIKE '%" + txb_Search.Text + "%' ORDER BY type";

            if(txb_Search.Text == "")
            {
                allData.Clear();
                allData = mwdb.ReadAllData(sqlQueryAll + nameObject + " ORDER BY type", D.DATA);
                dgw_ViewMain.DataSource = allData;
                dgvSettings();
                proverkaPoverki();
            }
            else
            {
                allData.Clear();
                allData = mwdb.ReadAllData(sqlQuery, D.DATA);
                dgw_ViewMain.DataSource = allData;
                if (allData.Count != 0)
                {
                    dgvSettings();
                    proverkaPoverki();
                    
                }
                    
            }
        }

        private void txb_Search_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        

        

        

        private void dgw_ViewMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            indexrow = dgw_ViewMain.CurrentRow.Index;
            
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            txb_Search.Clear();
        }



        private void proverkaPoverki()
        {
            int yearNow = DateTime.Now.Year;
            int yearNext;
            int kvartalNow;
            int kvartalNext;
            foreach(DataGridViewRow row in dgw_ViewMain.Rows)
            {

                if(row.Cells[9].Value.ToString() == "--" || row.Cells[11].Value.ToString() == "--")
                {
                    continue;
                }

                kvartalNow = ((int)(DateTime.Now.Month) + 2) / 3;
                kvartalNext = int.Parse(row.Cells[11].Value.ToString().Substring(0, 1));
                yearNext = int.Parse(row.Cells[11].Value.ToString().Substring(row.Cells[11].Value.ToString().Length - 4));
                if (kvartalNow >= kvartalNext && yearNow >= yearNext || yearNow > yearNext)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }


        //настройка меню
        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Разработано: Инженер КИПиА \"ООО НОВАТЭК - Энерго\", Заскотченко Александ Сергеевич.\nEmail для связи: ( ingenerkipovec@gmail.com )");
        }

        private void добавитьОбъектToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddObjectForm addObjectForm = new AddObjectForm();
            addObjectForm.ShowDialog();
            addItemsComboBox();
        }

        private void удалитьОбъектToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cbx_object.SelectedItem == null)
            {
                MessageBox.Show("Объект не выбран");
                return;
            }
            string name = nameObject.Replace("_", " ");
            if (MessageBox.Show("Вы действительно хотите удалить объект " + name + " ?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                allData.Clear();
                dgw_ViewMain.DataSource = null;

                if (mwdb.deleteObject(nameObject) == true)
                {
                    MessageBox.Show("Объект удалён");
                    addItemsComboBox();

                    if (cbx_object.Items.Count > 0)
                        cbx_object.SelectedIndex = 0;



                }
            }
        }

        private void экспортВExcelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mwdb.exportDGVtoExcel(dgw_ViewMain);
        }

        private void печатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mwdb.printList(dgw_ViewMain, nameObject);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (dgw_ViewMain.Rows.Count <= 0)
                {
                    MessageBox.Show("Нет приборов для удаления");
                    return;
                }
                DialogResult result = MessageBox.Show("Удалить прибор ?", "Удаление прибора", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.No)
                {
                    return;
                }
                if (mwdb.deleteData(id, nameObject, D.DATA) == true)
                {

                    MessageBox.Show("Прибор удалён из базы");
                    allData.Clear();
                    allData = mwdb.ReadAllData(sqlQueryAll + nameObject + " ORDER BY type", D.DATA);
                    dgw_ViewMain.DataSource = allData;
                    if (allData.Count != 0)
                    {
                        dgvSettings();
                        proverkaPoverki();
                        search();

                    }

                }
                else
                {
                    MessageBox.Show("Ошибка при удалении");
                }
            }
        }
    }

    public static class D
    {
        public static string DATA { get; set; }

        public static AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
    }


    public static class ExtensionMethods  //класс в котором включаем быструю отрисовку в датагриде
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }

    

}
