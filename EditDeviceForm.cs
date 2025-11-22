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
    public partial class EditDeviceForm : Form
    {
        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataReader dr;
        readonly string sqlQuery1 = "SELECT * FROM types ORDER BY type";
        readonly string sqlQuery2 = "SELECT * FROM manufacturers ORDER BY title";
        MetodsWorkDataBase mwdb = new MetodsWorkDataBase();

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
        string place_of_verification; // добавлено
        string facility;
        string position;
        string file_format;
        string file_name;
        string nameObject;
        int year_dp;
        int day_dp;
        int month_dp;

        string dataPoslPov;
        string intervPoverki;
        string nextPov;
        public EditDeviceForm(int id, string type, string model, string manufacturer, string factory_number, string accuracy_class, string number_of_channels, string range, string units, string date_of_last_calibration, string calibration_interval, string date_of_next_calibration, string place_of_verification, string facility, string position, string file_format, string file_name, string nameObject)
        {
            InitializeComponent();
            this.id = id;
            this.type = type;
            this.model = model;
            this.manufacturer = manufacturer;
            this.factory_number = factory_number;
            this.accuracy_class = accuracy_class;
            this.number_of_channels = number_of_channels;
            this.range = range;
            this.units = units;
            this.date_of_last_calibration = date_of_last_calibration;
            this.calibration_interval = calibration_interval;
            this.date_of_next_calibration = date_of_next_calibration;
            this.place_of_verification = place_of_verification;
            this.facility = facility;
            this.position = position;
            this.file_format = file_format;
            this.file_name = file_name;
            this.nameObject = nameObject;
        }

        private void EditDeviceForm_Load(object sender, EventArgs e)
        {
            populateCbxTypes();
            populateCbxManufacturers();
            txb_Model.Text = model;
            txb_FabricNumber.Text = factory_number;
            txb_KlassTochn.Text = accuracy_class;
            txb_KolvoIzmKan.Text = number_of_channels;
            txb_Predel.Text = range;
            txb_EdinIzm.Text = units;
            if(date_of_last_calibration == "--")
            {
                date_of_last_calibration = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
            }
            string[] dates_split = date_of_last_calibration.Split(new char[] {'.'});//разбиваем дату на элементы по точкам
            try
            {
                day_dp = int.Parse(dates_split[0]); //парсим день, месяц, год из string в int
                month_dp = int.Parse(dates_split[1]);
                year_dp = int.Parse(dates_split[2]);
                dtpYearPoverka.Value = new DateTime(year_dp, month_dp, day_dp); //устанавливаем значения года, месяца, дня в DateTimePicker
            }
            catch
            {

            }
            
            nudInterval.Value = int.Parse(calibration_interval);
            txb_NextPov.Text = date_of_next_calibration;
            // устанавливаем новое поле
            txb_PlacePover.Text = place_of_verification ?? "--";
            txb_Object.Text = facility;
            txb_Poziciya.Text = position;
            settingsFormForFile();
            
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
                    cbx_Type.SelectedItem = type;
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
                    cbx_Manufacturer.SelectedItem = manufacturer;
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }

        }

        private void btn_Okey_Click(object sender, EventArgs e)
        {
            if (poverkaFormat())
            {
                return;
            }

            if (txb_Model.Text.Trim() == "")
                txb_Model.Text = "--";
            if (txb_FabricNumber.Text.Trim() == "--")
                txb_FabricNumber.Text = "--";
            if (txb_KlassTochn.Text.Trim() == "--")
                txb_KlassTochn.Text = "--";
            if (txb_KolvoIzmKan.Text.Trim() == "")
                txb_KolvoIzmKan.Text = "--";
            if (txb_Predel.Text.Trim() == "")
                txb_Predel.Text = "--";
            if (txb_EdinIzm.Text.Trim() == "")
                txb_EdinIzm.Text = "--";
            
            
            if (txb_NextPov.Text.Trim() == "")
                txb_NextPov.Text = "--";
            if (txb_Object.Text.Trim() == "")
                txb_Object.Text = "--";
            if (txb_Poziciya.Text.Trim() == "")
                txb_Poziciya.Text = "--";

            // Передаём txb_PlacePover в updateDate
            if (mwdb.updateDate(id, cbx_Type.SelectedItem.ToString(), txb_Model.Text.Trim(), cbx_Manufacturer.SelectedItem.ToString(), txb_FabricNumber.Text.Trim(), txb_KlassTochn.Text.Trim(), txb_KolvoIzmKan.Text.Trim(), txb_Predel.Text.Trim(), txb_EdinIzm.Text.Trim(), dataPoslPov,
                intervPoverki, txb_NextPov.Text.Trim(), txb_PlacePover.Text.Trim(), txb_Object.Text.Trim(), txb_Poziciya.Text.Trim(), nameObject))
            {
                this.DialogResult = DialogResult.OK;
            }

            else
            {
                this.DialogResult = DialogResult.No;
            }

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string formatStringDatePoverka(DateTimePicker dtpick)//функция где формируется вид даты т.е если значения дня и месяца < 10 то добавляем перед ними 0
        {
            string day = dtpick.Value.Day.ToString();
            string month = dtpick.Value.Month.ToString();

            string fulldate = "";
            if (dtpick.Value.Day < 10)
            {
                day = "0" + day;
            }
            if (dtpick.Value.Month < 10)
            {
                month = "0" + month;
            }

            return fulldate + day + "." + month + ".";
        }

        private bool poverkaFormat()//функция где формируются даты последней поверки и следующей поверки для записи в БД
        {
            bool flag = false;
            if (dtpYearPoverka.Value.Year > DateTime.Now.Year)
            {
                MessageBox.Show("Год предыдущей поверки не может быть больше текущего");
                dtpYearPoverka.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);//если пытаемся указать год выше чем текущий, то сбрасываем на текущую дату
                flag = true;
                return flag;
            }

            nextPov = formatStringDatePoverka(dtpYearPoverka) + (dtpYearPoverka.Value.Year + ((int)nudInterval.Value / 12)).ToString();
            txb_NextPov.Text = nextPov;

            intervPoverki = nudInterval.Value.ToString();

            dataPoslPov = formatStringDatePoverka(dtpYearPoverka) + dtpYearPoverka.Value.Year.ToString();

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

        private void settingsFormForFile()
        {
            if (string.IsNullOrEmpty(file_name))
            {
                pictureBox1.Image = Properties.Resources.noFile;
                btnInsertFile.Visible = true;
                btnDeleteFile.Visible = false;
                btnReadFile.Visible = false;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.yesFile;
                btnInsertFile.Visible = false;
                btnDeleteFile.Visible = true;
                btnReadFile.Visible = true;
            }
        }

        private void btnInsertFile_Click(object sender, EventArgs e)
        {
           if( mwdb.addFile(id, nameObject))
            {
                MessageBox.Show("Файл добавлен");
                pictureBox1.Image = Properties.Resources.yesFile;
                btnInsertFile.Enabled = false;
                btnDeleteFile.Enabled = true;
                btnReadFile.Enabled = true;
            }
            
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
           if( mwdb.deleteFile(id, nameObject) )
            {
                MessageBox.Show("Файл удалён");
                pictureBox1.Image = Properties.Resources.noFile;
                btnInsertFile.Enabled = true;
                btnDeleteFile.Enabled = false;
                btnReadFile.Enabled = false;
            }
            else
            {
                MessageBox.Show("Ошибка удаления");
            }
            
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            mwdb.readingFile(id, nameObject);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
