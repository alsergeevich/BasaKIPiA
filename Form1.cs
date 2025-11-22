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
        string reg_number_SI;
        string number_of_channels;
        string range;
        string units;
        string date_of_last_calibration;
        string calibration_interval;
        string date_of_next_calibration;
        string place_of_verification; // новое поле
        string facility;
        string position;
        string file_format;
        string file_name;
        int indexrow = 0;

        public MainForm()
        {
            InitializeComponent();
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            // Инициализация контекстного меню для dgw_ViewMain без пунктов "Открыть файл", "Экспорт в Excel", "Печать"
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            var editItem = new ToolStripMenuItem("Редактировать");
            var deleteItem = new ToolStripMenuItem("Удалить");
            

            // Действие "Редактировать"
            editItem.Click += (s, e) =>
            {
                editPriborAndShowWindowEdit();
            };

            // Действие "Удалить"
            deleteItem.Click += (s, e) =>
            {
                if (dgw_ViewMain.Rows.Count <= 0 || dgw_ViewMain.CurrentRow == null)
                {
                    MessageBox.Show("Нет приборов для удаления");
                    return;
                }
                var result = MessageBox.Show("Удалить прибор ?", "Удаление прибора", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.No) return;

                try
                {
                    if (mwdb.deleteData(id, nameObject, D.DATA) == true)
                    {
                        MessageBox.Show("Прибор удалён из базы");
                        search();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении: " + ex.Message);
                }
            };

            contextMenu.Items.AddRange(new ToolStripItem[] { editItem, deleteItem});
            dgw_ViewMain.ContextMenuStrip = contextMenu;

            // При правом клике выделяем строку под курсором и обновляем поля (id и т.д.)
            dgw_ViewMain.MouseDown += (s, e) =>
            {
                if (e.Button != MouseButtons.Right) return;
                var hit = dgw_ViewMain.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    try
                    {
                        dgw_ViewMain.ClearSelection();
                        dgw_ViewMain.Rows[hit.RowIndex].Selected = true;
                        dgw_ViewMain.CurrentCell = dgw_ViewMain.Rows[hit.RowIndex].Cells[1];
                        // Обновляем значения id и полей через существующий обработчик
                        dgw_ViewMain_CellEnter(dgw_ViewMain, new DataGridViewCellEventArgs(1, hit.RowIndex));
                    }
                    catch { }
                }
            };

            // Перед открытием меню включаем/выключаем пункты в зависимости от состояния строки
            contextMenu.Opening += (s, e) =>
            {
                bool hasRow = dgw_ViewMain.CurrentRow != null && dgw_ViewMain.CurrentRow.Index >= 0;
                editItem.Enabled = hasRow;
                deleteItem.Enabled = hasRow;
            };
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

                dgw_ViewMain.Columns["reg_number_SI"].HeaderText = "Регистрационный номер типа СИ";
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

                // новый столбец
                dgw_ViewMain.Columns["place_of_verification"].HeaderText = "Место поверки";
                dgw_ViewMain.Columns["place_of_verification"].Width = 220;

                dgw_ViewMain.Columns["facility"].HeaderText = "Подразделение";
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

        

        // Создаёт чекбоксы для отображения/скрытия столбцов
        private void CreateColumnCheckboxes()
        {
            try
            {
                flpColumns.Controls.Clear();
                if (dgw_ViewMain.Columns.Count == 0) return;

                // Перебираем колонки в том же порядке, что и в DGV
                foreach (DataGridViewColumn col in dgw_ViewMain.Columns)
                {
                    // Пропускаем технические/бинарные поля
                    if (col.Name == "id" || col.Name == "file" || col.Name == "file_format")
                        continue;

                    CheckBox cb = new CheckBox();
                    cb.Text = col.HeaderText ?? col.Name;
                    cb.Tag = col.Name;
                    cb.AutoSize = true;
                    cb.Checked = col.Visible;
                    cb.CheckedChanged += (s, e) =>
                    {
                        try
                        {
                            string colName = cb.Tag.ToString();
                            if (dgw_ViewMain.Columns.Contains(colName))
                                dgw_ViewMain.Columns[colName].Visible = cb.Checked;
                        }
                        catch { }
                    };
                    flpColumns.Controls.Add(cb);
                }
            }
            catch { }
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

            // Сбросим текущее выбранное имя, чтобы при старте не было выбора
            nameObject = null;

            using (SQLiteConnection con = new SQLiteConnection("Data Source=" + D.DATA + ";Version=3;"))
            {
                con.Open();
                using (SQLiteCommand comm = new SQLiteCommand(sqlcommand, con))
                using (SQLiteDataReader rd = comm.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string tableName = rd["name"].ToString();
                        string displayName = tableName.Replace("_", " ");
                        cbx_object.Items.Add(new ComboItem(displayName, tableName));
                    }
                }
            }

            // Оставляем ComboBox без выбранного элемента (никакой элемент не выделен)
            cbx_object.SelectedIndex = -1;
            flag = false;
        }

        private void cbx_object_SelectedIndexChanged(object sender, EventArgs e)
        {
            flag = true;

            // Получаем фактическое имя таблицы из ComboItem.Value
            if (cbx_object.SelectedItem is ComboItem ci)
            {
                nameObject = ci.Value;
            }
            else
            {
                nameObject = cbx_object.SelectedItem?.ToString();
            }

            allData = mwdb.ReadAllData(sqlQueryAll + nameObject + " ORDER BY type", D.DATA);
            dgw_ViewMain.DataSource = allData;
            dgvSettings();
            CreateColumnCheckboxes();
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
            reg_number_SI = dgw_ViewMain.Rows[e.RowIndex].Cells[5].Value.ToString();
            number_of_channels = dgw_ViewMain.Rows[e.RowIndex].Cells[6].Value.ToString();
            range = dgw_ViewMain.Rows[e.RowIndex].Cells[7].Value.ToString();
            units = dgw_ViewMain.Rows[e.RowIndex].Cells[8].Value.ToString();
            date_of_last_calibration = dgw_ViewMain.Rows[e.RowIndex].Cells[9].Value.ToString();
            calibration_interval = dgw_ViewMain.Rows[e.RowIndex].Cells[10].Value.ToString();
            date_of_next_calibration = dgw_ViewMain.Rows[e.RowIndex].Cells[11].Value.ToString();
            place_of_verification = dgw_ViewMain.Rows[e.RowIndex].Cells[12].Value?.ToString() ?? "--";
            facility = dgw_ViewMain.Rows[e.RowIndex].Cells[13].Value.ToString();
            position = dgw_ViewMain.Rows[e.RowIndex].Cells[14].Value.ToString();
            file_format = dgw_ViewMain.Rows[e.RowIndex].Cells[16].Value.ToString();
            file_name = dgw_ViewMain.Rows[e.RowIndex].Cells[17].Value.ToString();
            

        }

        private void editPriborAndShowWindowEdit()
        {
            if (flag == false || id == 0 || allData.Count == 0)
            {
                MessageBox.Show("Выберите объект или прибор");
                return;
            }

            EditDeviceForm editDeviceForm = new EditDeviceForm(id, type, model, manufacturer, factory_number, reg_number_SI, number_of_channels, range, units, date_of_last_calibration, calibration_interval, date_of_next_calibration, place_of_verification, facility, position, file_format, file_name, nameObject);
            if (editDeviceForm.ShowDialog() == DialogResult.OK)
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

        private void btn_Edit_Click(object sender, EventArgs e)
        {

            editPriborAndShowWindowEdit();


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
                CreateColumnCheckboxes();
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
                    CreateColumnCheckboxes();
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
            int monthNow = DateTime.Now.Month;
            int monthNext;
            int yearNext;
            int intervalPoverki;
            //int kvartalNow;
            //int kvartalNext;
            foreach(DataGridViewRow row in dgw_ViewMain.Rows)
            {

                if(row.Cells[9].Value.ToString() == "--" || row.Cells[11].Value.ToString() == "--")
                {
                    continue;
                }

                

                intervalPoverki = int.Parse(row.Cells[10].Value.ToString()); //парсим значение межповерочного интервала
                yearNext = int.Parse(row.Cells[11].Value.ToString().Substring(row.Cells[11].Value.ToString().Length - 4));//парсим значение года
                monthNext = int.Parse(row.Cells[11].Value.ToString().Substring(3, 2));//парсим значение месяца
                int datePoverki = ((yearNext - yearNow) * 12) + monthNext - monthNow; //сколько месяцев осталось до поверки
                if ((datePoverki <= 0) || (yearNow > yearNext)) //если сумма межпов интервала и остатком месяцев до поверки == интервалу поверки
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                else if ((datePoverki >= 1) && (datePoverki <= 2))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
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

        private void dgw_ViewMain_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)//обработка двойного клика по строке для редактирования прибора
        {
            editPriborAndShowWindowEdit();
        }

        private void типыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTypeForm addTypeForm = new AddTypeForm();
            addTypeForm.ShowDialog();
        }

        private void производителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddManufForm addManufForm = new AddManufForm();
            addManufForm.ShowDialog();
        }

        private void сформироватьГрафикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgw_ViewMain.Rows.Count == 0)
            {
                MessageBox.Show("Таблица пуста. Нечего экспортировать.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                FileName = "График_поверки",
                DefaultExt = ".xlsx",
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };

            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                using (var package = new OfficeOpenXml.ExcelPackage())
                {
                    var ws = package.Workbook.Worksheets.Add("График поверки");

                    // Подставляем название подразделения (используем nameObject как источник, если пусто — cbx_object)
                    string subdivision = (!string.IsNullOrWhiteSpace(nameObject) ? nameObject : (cbx_object.SelectedItem?.ToString() ?? ""));
                    subdivision = subdivision.Replace("_", " ");
                    int reportYear = DateTime.Now.Year + 1;

                    // Заголовок (строка 1) — объединяем колонки 1..20
                    string title = $"График поверки {subdivision} на {reportYear} год";
                    ws.Cells[1, 1].Value = title;
                    ws.Cells[1, 1, 1, 20].Merge = true;
                    ws.Cells[1, 1, 1, 20].Style.Font.Bold = true;
                    ws.Cells[1, 1, 1, 20].Style.Font.Size = 14;
                    ws.Cells[1, 1, 1, 20].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    ws.Row(1).Height = 24;

                    // Заголовки (строка 2)
                    var headers = new[]
                    {
                        "№", "Тип", "Модель", "Заводской номер", "Регистрационный номер типа СИ",
                        "Дата последней поверки", "Межповерочный интервал", "Дата следующей поверки"
                    };

                    // Месяцы (колонки 9..20)
                    var months = new[]
                    {
                        "Январь","Февраль","Март","Апрель","Май","Июнь",
                        "Июль","Август","Сентябрь","Октябрь","Ноябрь","Декабрь"
                    };

                    int headerRow = 2;
                    // Пишем заголовки 1..8
                    for (int i = 0; i < headers.Length; i++)
                    {
                        ws.Cells[headerRow, i + 1].Value = headers[i];
                        ws.Cells[headerRow, i + 1].Style.Font.Bold = true;
                        ws.Cells[headerRow, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }

                    // Пишем месяцы в 9..20
                    for (int m = 0; m < months.Length; m++)
                    {
                        ws.Cells[headerRow, 9 + m].Value = months[m];
                        ws.Cells[headerRow, 9 + m].Style.Font.Bold = true;
                        ws.Cells[headerRow, 9 + m].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        ws.Column(9 + m).Width = 12;
                    }

                    int outRow = headerRow + 1; // данные начинаются со строки 3
                    int[] monthCounts = new int[12];

                    // Перебираем строки DataGridView
                    foreach (DataGridViewRow row in dgw_ViewMain.Rows)
                    {
                        if (row.IsNewRow) continue;

                        // Колонки по именам (при наличии)
                        string type = GetCellString(row, "type");
                        string model = GetCellString(row, "model");
                        string factoryNumber = GetCellString(row, "factory_number");
                        string regNumber = GetCellString(row, "reg_number_SI");
                        string dateLast = GetCellString(row, "date_of_last_calibration");
                        string interval = GetCellString(row, "calibration_interval");
                        string dateNext = GetCellString(row, "date_of_next_calibration");

                        ws.Cells[outRow, 1].Value = outRow - headerRow; // порядковый номер
                        ws.Cells[outRow, 2].Value = type;
                        ws.Cells[outRow, 3].Value = model;
                        ws.Cells[outRow, 4].Value = factoryNumber;
                        ws.Cells[outRow, 5].Value = regNumber;
                        ws.Cells[outRow, 6].Value = dateLast;
                        ws.Cells[outRow, 7].Value = interval;
                        ws.Cells[outRow, 8].Value = dateNext;

                        // Попытка распарсить дату следующей поверки и поставить плюс в соответствующем месяце
                        if (!string.IsNullOrWhiteSpace(dateNext) && dateNext != "--")
                        {
                            DateTime dt;
                            bool parsed = DateTime.TryParseExact(dateNext, new[] { "dd.MM.yyyy", "d.M.yyyy", "dd.MM.yy", "d.M.yy" },
                                System.Globalization.CultureInfo.GetCultureInfo("ru-RU"),
                                System.Globalization.DateTimeStyles.None, out dt);

                            if (!parsed)
                            {
                                // как запасной вариант пробуем общую попытки парсинга
                                parsed = DateTime.TryParse(dateNext, out dt);
                            }

                            if (parsed)
                            {
                                int month = dt.Month; // 1..12
                                int monthCol = 8 + month; // 9..20
                                ws.Cells[outRow, monthCol].Value = "+";
                                ws.Cells[outRow, monthCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                monthCounts[month - 1]++;
                            }
                        }

                        outRow++;
                    }

                    int dataEndRow = outRow - 1;

                    // Итоговая строка с суммами по месяцам (строка после данных)
                    int summaryRow = dataEndRow + 2;
                    ws.Cells[summaryRow, 1].Value = "Итого";
                    ws.Cells[summaryRow, 1].Style.Font.Bold = true;
                    ws.Cells[summaryRow, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    for (int m = 0; m < 12; m++)
                    {
                        ws.Cells[summaryRow, 9 + m].Value = monthCounts[m];
                        ws.Cells[summaryRow, 9 + m].Style.Font.Bold = true;
                        ws.Cells[summaryRow, 9 + m].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }

                    // Форматирование: автоширина для первых 8 колонок
                    for (int c = 1; c <= 8; c++)
                        ws.Column(c).AutoFit();

                    // Добавляем границы для области таблицы (заголовки, данные, итог)
                    int lastRowForBorders = summaryRow;
                    var tableRange = ws.Cells[headerRow, 1, lastRowForBorders, 20];
                    var border = tableRange.Style.Border;
                    border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    // Внутренние линии
                    tableRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    tableRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    tableRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    tableRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    // Создаём столбчатую диаграмму по суммам месяцев
                    var chart = ws.Drawings.AddChart("chart_ГрафикПоверки", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered);
                    chart.Title.Text = "График поверок по месяцам";
                    chart.SetPosition(summaryRow, 20, 0, 0); // позиция: ниже итоговой строки
                    chart.SetSize(900, 400);

                    var xRange = ws.Cells[headerRow, 9, headerRow, 20]; // месяцы (заголовки)
                    var yRange = ws.Cells[summaryRow, 9, summaryRow, 20]; // итоги

                    var serie = chart.Series.Add(yRange, xRange);
                    serie.Header = "Количество поверок";

                    // Сохраняем файл
                    var fi = new System.IO.FileInfo(saveFileDialog.FileName);
                    package.SaveAs(fi);

                    MessageBox.Show("Файл успешно сохранён: " + fi.FullName, "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при формировании файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Локальная вспомогательная функция для безопасного получения значения по имени колонки
            string GetCellString(DataGridViewRow r, string colName)
            {
                try
                {
                    if (dgw_ViewMain.Columns.Contains(colName))
                    {
                        var cell = r.Cells[dgw_ViewMain.Columns[colName].Index].Value;
                        return cell?.ToString() ?? "";
                    }
                }
                catch { }
                return "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            сформироватьГрафикToolStripMenuItem_Click(sender, e);
        }

// вспомогательный тип для хранения элемента ComboBox
private class ComboItem
{
    public string Display { get; }
    public string Value { get; }

    public ComboItem(string display, string value)
    {
        Display = display;
        Value = value;
    }

    // В WinForms ComboBox использует ToString() для вывода текста элемента
    public override string ToString() => Display;
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
