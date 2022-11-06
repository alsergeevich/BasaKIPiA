using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections;
using System.Windows.Forms;
using DGVPrinterHelper;
using System.Drawing;
using System.IO;
using OfficeOpenXml;


namespace BasaKIPiA
{
    class MetodsWorkDataBase
    {
        
        private SQLiteConnection dbConn;
        private SQLiteCommand dbComm;
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        
        


        public ArrayList ReadAllData(string sqlQuery, string dbPath)//метод производит чтение из базы
        {
            ArrayList readData = new ArrayList();
            

            using (dbConn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
            {


                dbComm = new SQLiteCommand(sqlQuery, dbConn);
                try
                {
                    dbConn.Open();
                    SQLiteDataReader dr = dbComm.ExecuteReader();

                    if (dr.HasRows)
                    {
                        foreach (var result in dr)
                        {
                            readData.Add(result);
                        }
                    }
                }
                catch
                {

                }

            }
            return readData;
        }


        public bool SaveNewTypes(string type, string dbPath)//метод сохраняет в таблицу types
        {
           
            bool flagResult = false;
            string sqlQuery = string.Format("INSERT INTO types ([type]) VALUES ('{0}')", type);


            using (dbConn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
            {
                dbComm = new SQLiteCommand(sqlQuery, dbConn);
                try
                {
                    dbConn.Open();
                    if (dbComm.ExecuteNonQuery() == 1)
                    {
                        flagResult = true;
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Ошибка " + ex.Message);
                }
            }

            return flagResult;
        }


        public bool SaveNewManufactured(string title, string dbPath)//метод сохраняет в таблицу manufactured
        {

            bool flagResult = false;
            string sqlQuery = string.Format("INSERT INTO manufacturers ([title]) VALUES ('{0}')", title);


            using (dbConn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
            {
                dbComm = new SQLiteCommand(sqlQuery, dbConn);
                try
                {
                    dbConn.Open();
                    if (dbComm.ExecuteNonQuery() == 1)
                    {
                        flagResult = true;
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Ошибка " + ex.Message);
                }
            }

            return flagResult;
        }


        public bool SaveNewDevice(string type, string model, string manufacturer, string factoryNumber, string accuracyClass, string numberOfChannels, string range, string units, string dateOfLastCalibr, string calibrInterval, string dateOfNextCalibr, string facility, string position, string nameObject) //добавление в таблицу новой записи или добавление нового прибора
        {
            bool flagResult = false;
            string sqlOuery = "INSERT INTO " + nameObject + " (type, model, manufacturer, factory_number, accuracy_class, number_of_channels, range, units, date_of_last_calibration, calibration_interval, date_of_next_calibration, facility, position) "
                + "VALUES (@type, @model, @manufacturer, @factory_number, @accuracy_class, @number_of_channels, @range, @units, @date_of_last_calibration, @calibration_interval, @date_of_next_calibration, @facility, @position)";

            using (dbConn = new SQLiteConnection("Data Source=" + D.DATA + ";Version=3;"))
            {
                try
                {
                    dbConn.Open();
                    dbComm = new SQLiteCommand(sqlOuery, dbConn);

                    dbComm.Parameters.Add(new SQLiteParameter("@type", type));
                    dbComm.Parameters.Add(new SQLiteParameter("@model", model));
                    dbComm.Parameters.Add(new SQLiteParameter("@manufacturer", manufacturer));
                    dbComm.Parameters.Add(new SQLiteParameter("@factory_number", factoryNumber));
                    dbComm.Parameters.Add(new SQLiteParameter("@accuracy_class", accuracyClass));
                    dbComm.Parameters.Add(new SQLiteParameter("@number_of_channels", numberOfChannels));
                    dbComm.Parameters.Add(new SQLiteParameter("@range", range));
                    dbComm.Parameters.Add(new SQLiteParameter("@units", units));
                    dbComm.Parameters.Add(new SQLiteParameter("@date_of_last_calibration", dateOfLastCalibr));
                    dbComm.Parameters.Add(new SQLiteParameter("@calibration_interval", calibrInterval));
                    dbComm.Parameters.Add(new SQLiteParameter("@date_of_next_calibration", dateOfNextCalibr));
                    dbComm.Parameters.Add(new SQLiteParameter("@facility", facility));
                    dbComm.Parameters.Add(new SQLiteParameter("@position", position));

                    if(dbComm.ExecuteNonQuery() == 1)
                    {
                        flagResult = true;
                    }
                }
                catch(SQLiteException ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }

            return flagResult;
        }


        public bool updateDate(int id, string type, string model, string manufacturer, string factoryNumber, string accuracyClass, string numberOfChannels, string range, string units, string dateOfLastCalibr, string calibrInterval, string dateOfNextCalibr, string facility, string position, string nameObject)//метод обновляет данные в таблице types
        {

            bool flagResult = false;

            string sqlQuery = "UPDATE " + nameObject + " SET type=@type , model=@model, manufacturer=@manufacturer, factory_number=@factory_number, accuracy_class=@accuracy_class, number_of_channels=@number_of_channels, range=@range, units=@units, " +
                "date_of_last_calibration=@date_of_last_calibration, calibration_interval=@calibration_interval, date_of_next_calibration=@date_of_next_calibration, facility=@facility, position=@position WHERE id=@id";



            using (dbConn = new SQLiteConnection("Data Source=" + D.DATA + ";Version=3;"))
            {
                try
                {
                    dbConn.Open();
                    dbComm = new SQLiteCommand(sqlQuery, dbConn);

                    dbComm.Parameters.Add(new SQLiteParameter("@id", id));
                    dbComm.Parameters.Add(new SQLiteParameter("@type", type));
                    dbComm.Parameters.Add(new SQLiteParameter("@model", model));
                    dbComm.Parameters.Add(new SQLiteParameter("@manufacturer", manufacturer));
                    dbComm.Parameters.Add(new SQLiteParameter("@factory_number", factoryNumber));
                    dbComm.Parameters.Add(new SQLiteParameter("@accuracy_class", accuracyClass));
                    dbComm.Parameters.Add(new SQLiteParameter("@number_of_channels", numberOfChannels));
                    dbComm.Parameters.Add(new SQLiteParameter("@range", range));
                    dbComm.Parameters.Add(new SQLiteParameter("@units", units));
                    dbComm.Parameters.Add(new SQLiteParameter("@date_of_last_calibration", dateOfLastCalibr));
                    dbComm.Parameters.Add(new SQLiteParameter("@calibration_interval", calibrInterval));
                    dbComm.Parameters.Add(new SQLiteParameter("@date_of_next_calibration", dateOfNextCalibr));
                    dbComm.Parameters.Add(new SQLiteParameter("@facility", facility));
                    dbComm.Parameters.Add(new SQLiteParameter("@position", position));

                    if (dbComm.ExecuteNonQuery() == 1)
                    {
                        flagResult = true;
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }

            return flagResult;
        }


        public bool deleteData(int id, string tableName, string dbPath)//метод удаляет контакт из базы
        {
            bool flagResult = false;
            string sqlQuery = string.Format("DELETE FROM " + tableName + " WHERE [id] = {0}", id);

            using (dbConn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
            {
                dbComm = new SQLiteCommand(sqlQuery, dbConn);
                try
                {
                    dbConn.Open();
                    if (dbComm.ExecuteNonQuery() == 1)
                    {
                        flagResult = true;
                    }
                }
                catch
                {

                }
            }
            return flagResult;
        }


        public void createTablesObject(string dbPath, string tableName) //создаём объект или таблицу в базе данных
        {
            SQLiteConnection conn = new SQLiteConnection();
            SQLiteCommand command = new SQLiteCommand();

            using (conn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
            {
                try
                {
                    conn.Open();
                    command.Connection = conn;
                    command.CommandText = "CREATE TABLE " + "'" + tableName + "'" + " ([id]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [type] TEXT NOT NULL, model TEXT, [manufacturer] TEXT, " +
                                      "[factory_number] TEXT, [accuracy_class] TEXT, [number_of_channels]	TEXT, [range] TEXT, [units]	TEXT, " +
                                      "[date_of_last_calibration]	TEXT, [calibration_interval] TEXT, [date_of_next_calibration] TEXT, [facility] TEXT, [position]	TEXT, [file] BINARY, [file_format] VARCHAR(10), [file_name] NVARCHAR(128))";

                    command.ExecuteNonQuery();


                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error " + ex.Message);

                }


            }

        } 

        
        public void exportDGVtoExcel(DataGridView dataGridView) // экспорт в эксель
        {
            var saveFileDialoge = new SaveFileDialog(); // диалог сохранения файла
            saveFileDialoge.FileName = "output";
            saveFileDialoge.DefaultExt = ".xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // устанавливаем некоммерческую лицензию

            if (saveFileDialoge.ShowDialog() == DialogResult.OK) //если сохраняем
            {
                using(ExcelPackage myExcelPackage = new ExcelPackage(saveFileDialoge.FileName)) //создаём экселевский файл
                {
                    ExcelWorksheet worksheet = myExcelPackage.Workbook.Worksheets.Add("Sheet1"); // создаём книгу в файле

                    worksheet.View.ShowGridLines = true;
                    worksheet.Cells[1, 12, 1, 13].Merge = true;
                    worksheet.DefaultColWidth = 30;//минимальная ширина столбца
                    worksheet.Cells.AutoFitColumns(worksheet.DefaultColWidth);

                    for (int a = 2; a < dataGridView.Columns.Count + 1; a++) //переносим заголовки из datagridview
                    {
                        int i = a;
                        if (i == 13)
                        {
                            continue;
                        }

                        if (i > 13)
                        {
                            i = i - 1;
                        }


                        if (a == 15)
                        {
                            break;
                        }
                        worksheet.Cells[1, i - 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//устанавливаем заголовки в центр
                        worksheet.Cells[1, i - 1].Value = dataGridView.Columns[a - 1].HeaderText;

                    }

                    for (int i = 0; i < dataGridView.Rows.Count; i++) //переносим остальные данные
                    {
                        worksheet.Cells[i + 2, 12, i + 2, 13].Merge = true;//объединяем ячейки в последнем столбце
                        

                        for (int a = 1; a < dataGridView.Columns.Count - 3; a++)
                        {
                            int j = a;

                            if (j == 12)
                            {
                                continue;
                            }

                            if (j > 12)
                            {
                                j = j - 1;
                            }
                            worksheet.Cells[i + 2, j].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//по центру
                            worksheet.Cells[i + 2, 12, i + 2, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;//в последних столбцах прижать влево
                            worksheet.Cells[i + 2, j].Value = dataGridView.Rows[i].Cells[a].Value.ToString();
                        }
                    }

                    myExcelPackage.Save(); //сохраняем все сделанные изменения в экселевском файле

                }
            }

            



            

            

            

            

        }


        public void printList(DataGridView dataGridView, string nameObject) //вывод на печать содержимого таблицы
        {
            dataGridView.Columns["file_name"].Visible = false;
            string name = nameObject.Replace("_", " ");
            DGVPrinter dGVPrinter = new DGVPrinter();
            dGVPrinter.Title = name + " список приборов";
            dGVPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dGVPrinter.PageNumbers = false;
            dGVPrinter.PageNumberInHeader = false;
            dGVPrinter.PorportionalColumns = true;
            dGVPrinter.HeaderCellAlignment = StringAlignment.Near;
            dGVPrinter.Footer = DateTime.Now.Year.ToString() + " год.";
            dGVPrinter.FooterSpacing = 15;
            dGVPrinter.printDocument.DefaultPageSettings.Landscape = true;
            dGVPrinter.PrintDataGridView(dataGridView);
            dataGridView.Columns["file_name"].Visible = true;
        }

        public bool deleteObject(string nameObject) //удаление объекта или таблицы из базы данных
        {
            bool flagResult = false;
            string sqlQuery = string.Format("DROP TABLE IF EXISTS '{0}'", nameObject);
            string sqlcommand = "select name from sqlite_master where type = 'table' and name = " + "'" + nameObject + "'";

            using (dbConn = new SQLiteConnection("Data Source=" + D.DATA + ";Version=3;"))
            {
                dbComm = new SQLiteCommand(sqlQuery, dbConn);
                try
                {
                    dbConn.Open();
                    dbComm.ExecuteNonQuery();
                    dbComm = new SQLiteCommand(sqlcommand, dbConn);
                    if (dbComm.ExecuteNonQuery() != 1)
                    {
                        flagResult = true;
                    }
                }
                catch
                {

                }
            }
            return flagResult;

        }

        public bool addFile( int id, string nameObject)
        {
            bool flag = false;
            openFileDialog1.Filter = "All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return flag;
            string filename = openFileDialog1.FileName;
            

            byte[] _fileBytes = null;
            string filePath = filename; // файл
            FileInfo _fileInfo = new FileInfo(filePath);
            long _numBytes = _fileInfo.Length;
            FileStream _fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read); // откроем файл на чтение
            BinaryReader _binReader = new BinaryReader(_fileStream);
            _fileBytes = _binReader.ReadBytes((int)_numBytes); // файл в байтах
            string fileFormat = Path.GetExtension(filePath).Replace(".", "").ToLower(); // запишем в переменную расширение файла в нижнем регистре, не забыв удалить точку перед расширением, получим «mp3»
            string fileName = Path.GetFileName(filePath).Replace(Path.GetExtension(filePath), ""); // запишем в переменную имя файла, не забыв удалить расширение с точкой, получим «music-01»
                                                                                                   // записываем информацию в базу данных
            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + D.DATA + ";Version=3;"))
            {
                try
                {
                    string commandText = "UPDATE " + nameObject + " SET file=@file, file_format=@format, file_name=@name WHERE id=@id";
                    SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                    Command.Parameters.AddWithValue("@id", id);
                    Command.Parameters.AddWithValue("@file", _fileBytes);
                    Command.Parameters.AddWithValue("@format", fileFormat);
                    Command.Parameters.AddWithValue("@name", fileName);
                    
                    Connect.Open();
                   if( Command.ExecuteNonQuery() == 1)
                    {
                        return flag = true;
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);

                }
                Connect.Close();
            }

            return flag;
        }

        public bool deleteFile(int id, string nameObject)
        {
            bool flag = false;
            byte[] _fileBytes = null;
            string fileFormat = null;
            string fileName = null;

            if (MessageBox.Show("Вы уверенны, что хотите удалить файл ?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                return flag;
            
            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + D.DATA + ";Version=3;"))
            {
                try
                {
                    string commandText = "UPDATE " + nameObject + " SET file=@file, file_format=@format, file_name=@name WHERE id=@id";
                    SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                    Command.Parameters.AddWithValue("@id", id);
                    Command.Parameters.AddWithValue("@file", _fileBytes);
                    Command.Parameters.AddWithValue("@format", fileFormat);
                    Command.Parameters.AddWithValue("@name", fileName);

                    Connect.Open();
                    if(Command.ExecuteNonQuery() == 1)
                    {
                        return flag = true;
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);

                }
                Connect.Close();
            }
            return flag;
        }

        public void readingFile(int id, string nameObject)
        {
            saveFileDialog1.Filter = "All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;

            // получаем данные их БД
            List<byte[]> _fileList = new List<byte[]>();
            List<string> _fileFormatList = new List<string>();
            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + D.DATA + ";Version=3;"))
            {
                Connect.Open();
                SQLiteCommand Command = new SQLiteCommand
                {
                    Connection = Connect,
                    CommandText = @"SELECT * FROM " + nameObject + " WHERE [file_format] NOT NULL AND [id] = " + id
                };
                SQLiteDataReader sqlReader = Command.ExecuteReader();
                byte[] _dbFileByte = null;
                string _dbFileFormat = null;
                while (sqlReader.Read())
                {
                    _dbFileByte = (byte[])sqlReader["file"];
                    _fileList.Add(_dbFileByte);
                    _dbFileFormat = sqlReader["file_format"].ToString().TrimStart().TrimEnd();
                    _fileFormatList.Add(_dbFileFormat);
                }
                Connect.Close();
            }
            if (_fileList.Count == 0) // если в базе нет записей с файлами (пустой список), то...
            {
                return; // завершить работу метода
            }
            // сохранить файл на диск
            byte[] _fileBytes = _fileList[0]; // получаем массив байтов файла, который в БД (первый из списка)
            string _fileFormat = _fileFormatList[0]; // получаем расширение файла (первый из списка)
            string _newFileSaved = filename + "." + _fileFormat; // задаём путь сохранения файла с именем и расширение
            FileStream fileStream = new FileStream(_newFileSaved, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter binWriter = new BinaryWriter(fileStream);
            binWriter.Write(_fileBytes);
            binWriter.Close();
        }


    }

    
}
