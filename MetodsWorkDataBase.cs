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


        // Добавлен параметр placeOfVerification и соответствующая колонка INSERT
        public bool SaveNewDevice(string type, string model, string manufacturer, string factoryNumber, string accuracyClass, string numberOfChannels, string range, string units, string dateOfLastCalibr, string calibrInterval, string dateOfNextCalibr, string placeOfVerification, string facility, string position, string nameObject) //добавление в таблицу новой записи или добавление нового прибора
        {
            bool flagResult = false;
            string sqlOuery = "INSERT INTO " + nameObject + " (type, model, manufacturer, factory_number, reg_number_SI, number_of_channels, range, units, date_of_last_calibration, calibration_interval, date_of_next_calibration, place_of_verification, facility, position, file, file_format, file_name) "
                + "VALUES (@type, @model, @manufacturer, @factory_number, @reg_number_SI, @number_of_channels, @range, @units, @date_of_last_calibration, @calibration_interval, @date_of_next_calibration, @place_of_verification, @facility, @position, @file, @file_format, @file_name)";

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
                    dbComm.Parameters.Add(new SQLiteParameter("@reg_number_SI", accuracyClass));
                    dbComm.Parameters.Add(new SQLiteParameter("@number_of_channels", numberOfChannels));
                    dbComm.Parameters.Add(new SQLiteParameter("@range", range));
                    dbComm.Parameters.Add(new SQLiteParameter("@units", units));
                    dbComm.Parameters.Add(new SQLiteParameter("@date_of_last_calibration", dateOfLastCalibr));
                    dbComm.Parameters.Add(new SQLiteParameter("@calibration_interval", calibrInterval));
                    dbComm.Parameters.Add(new SQLiteParameter("@date_of_next_calibration", dateOfNextCalibr));
                    dbComm.Parameters.Add(new SQLiteParameter("@place_of_verification", placeOfVerification ?? "--"));
                    dbComm.Parameters.Add(new SQLiteParameter("@facility", facility));
                    dbComm.Parameters.Add(new SQLiteParameter("@position", position));
                    // placeholders for file fields
                    dbComm.Parameters.Add(new SQLiteParameter("@file", DBNull.Value));
                    dbComm.Parameters.Add(new SQLiteParameter("@file_format", DBNull.Value));
                    dbComm.Parameters.Add(new SQLiteParameter("@file_name", DBNull.Value));

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


        // Добавлен параметр placeOfVerification и соответствующее поле UPDATE
        public bool updateDate(int id, string type, string model, string manufacturer, string factoryNumber, string accuracyClass, string numberOfChannels, string range, string units, string dateOfLastCalibr, string calibrInterval, string dateOfNextCalibr, string placeOfVerification, string facility, string position, string nameObject)//метод обновляет данные в таблице types
        {

            bool flagResult = false;

            string sqlQuery = "UPDATE " + nameObject + " SET type=@type , model=@model, manufacturer=@manufacturer, factory_number=@factory_number, reg_number_SI=@reg_number_SI, number_of_channels=@number_of_channels, range=@range, units=@units, " +
                "date_of_last_calibration=@date_of_last_calibration, calibration_interval=@calibration_interval, date_of_next_calibration=@date_of_next_calibration, place_of_verification=@place_of_verification, facility=@facility, position=@position WHERE id=@id";

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
                    dbComm.Parameters.Add(new SQLiteParameter("@reg_number_SI", accuracyClass));
                    dbComm.Parameters.Add(new SQLiteParameter("@number_of_channels", numberOfChannels));
                    dbComm.Parameters.Add(new SQLiteParameter("@range", range));
                    dbComm.Parameters.Add(new SQLiteParameter("@units", units));
                    dbComm.Parameters.Add(new SQLiteParameter("@date_of_last_calibration", dateOfLastCalibr));
                    dbComm.Parameters.Add(new SQLiteParameter("@calibration_interval", calibrInterval));
                    dbComm.Parameters.Add(new SQLiteParameter("@date_of_next_calibration", dateOfNextCalibr));
                    dbComm.Parameters.Add(new SQLiteParameter("@place_of_verification", placeOfVerification ?? "--"));
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
                    // Добавлена колонка place_of_verification
                    command.CommandText = "CREATE TABLE " + "'" + tableName + "'" + " ([id]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [type] TEXT NOT NULL, model TEXT, [manufacturer] TEXT, " +
                                      "[factory_number] TEXT, [reg_number_SI] TEXT, [number_of_channels]	TEXT, [range] TEXT, [units]	TEXT, " +
                                      "[date_of_last_calibration]	TEXT, [calibration_interval] TEXT, [date_of_next_calibration] TEXT, [place_of_verification] TEXT, [facility] TEXT, [position]	TEXT, [file] BINARY, [file_format] VARCHAR(10), [file_name] NVARCHAR(128))";

                    command.ExecuteNonQuery();


                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error " + ex.Message);

                }


            }

        } 

        
        public void exportDGVtoExcel(DataGridView dataGridView) // экспорт в эксель (устойчив к добавлению колонок)
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
                    worksheet.DefaultColWidth = 30;//минимальная ширина столбца
                    worksheet.Cells.AutoFitColumns(worksheet.DefaultColWidth);

                    // Записываем заголовки по порядку, пропуская бинарные/вспомогательные колонки file, file_format и id, а также невидимые столбцы
                    int col = 1;
                    var visibleColumns = dataGridView.Columns.Cast<DataGridViewColumn>()
                        .Where(c => c.Visible && c.Name != "file" && c.Name != "file_format" && c.Name != "id")
                        .OrderBy(c => c.DisplayIndex)
                        .ToList();

                    foreach (DataGridViewColumn colGrid in visibleColumns)
                    {
                        worksheet.Cells[1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[1, col].Value = colGrid.HeaderText;
                        col++;
                    }

                    // Записываем данные
                    for (int r = 0; r < dataGridView.Rows.Count; r++)
                    {
                        if (dataGridView.Rows[r].IsNewRow) // пропустить строку ввода новой записи
                            continue;

                        col = 1;
                        foreach (DataGridViewColumn colGrid in visibleColumns)
                        {
                            var cellVal = dataGridView.Rows[r].Cells[colGrid.Index].Value;
                            worksheet.Cells[r + 2, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Cells[r + 2, col].Value = cellVal != null ? cellVal.ToString() : "";
                            col++;
                        }
                    }

                    // Если есть содержимое — применяем границы ко всем заполненным ячейкам (включая заголовки).
                    if (worksheet.Dimension != null)
                    {
                        int lastRow = worksheet.Dimension.End.Row;
                        int lastCol = worksheet.Dimension.End.Column;

                        for (int r = 1; r <= lastRow; r++)
                        {
                            for (int c = 1; c <= lastCol; c++)
                            {
                                var cell = worksheet.Cells[r, c];
                                var b = cell.Style.Border;
                                b.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                b.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                b.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                b.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            }
                        }
                    }

                    myExcelPackage.Save(); //сохраняем все сделанные изменения в экселевском файле

                }
            }
        }


        public void printList(DataGridView dataGridView, string nameObject) //вывод на печать содержимого таблицы
        {
            // Сохраняем исходные видимости столбцов и затем временно скрываем file_name, если он видим.
            Dictionary<string, bool> vis = new Dictionary<string, bool>();
            foreach (DataGridViewColumn c in dataGridView.Columns)
            {
                vis[c.Name] = c.Visible;
            }

            if (dataGridView.Columns.Contains("file_name") && dataGridView.Columns["file_name"].Visible)
            {
                dataGridView.Columns["file_name"].Visible = false;
            }

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

            // Сохраним исходные настройки страницы, чтобы восстановить после печати
            var printDoc = dGVPrinter.printDocument;
            var originalPaperSize = printDoc.DefaultPageSettings.PaperSize;
            var originalMargins = printDoc.DefaultPageSettings.Margins;
            var originalLandscape = printDoc.DefaultPageSettings.Landscape;

            try
            {
                // Устанавливаем альбомную ориентацию
                printDoc.DefaultPageSettings.Landscape = true;

                // Попытаемся найти PaperSize A4 в списке поддерживаемых принтером размеров
                System.Drawing.Printing.PaperSize a4Paper = null;
                try
                {
                    foreach (System.Drawing.Printing.PaperSize ps in printDoc.PrinterSettings.PaperSizes)
                    {
                        // PaperKind может быть доступен у некоторых принтеров
                        if (ps.RawKind == (int)System.Drawing.Printing.PaperKind.A4 || ps.PaperName.ToLower().Contains("a4"))
                        {
                            a4Paper = ps;
                            break;
                        }
                    }
                }
                catch
                {
                    a4Paper = null;
                }

                // Если нашли — применим, иначе создадим пользовательский размер A4 (в сотых дюйма)
                // A4: 210 x 297 mm = 8.27 x 11.69 in => *100 => 827 x 1169 (portrait). Для ландшафта — инвертируем.
                if (a4Paper != null)
                {
                    printDoc.DefaultPageSettings.PaperSize = a4Paper;
                }
                else
                {
                    try
                    {
                        // конструкция PaperSize(width,height) принимает значения в сотых дюйма
                        var a4 = new System.Drawing.Printing.PaperSize("A4", 1169, 827); // landscape (ширина>высота)
                        printDoc.DefaultPageSettings.PaperSize = a4;
                    }
                    catch
                    {
                        // если и это не удалось — оставляем текущие настройки
                    }
                }

                // Задаем небольшие поля (в сотых дюйма). Если принтер не позволит — он сам скорректирует.
                try
                {
                    printDoc.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(50, 50, 40, 40); // ~0.5" слева/справа, ~0.4" сверху/снизу
                }
                catch
                {
                    // игнорируем, если не поддерживается
                }

                // Печатаем DataGridView с текущими видимыми колонками; DGVPrinter сам разбивает на страницы
                dGVPrinter.printDocument.DefaultPageSettings.Landscape = true;
                dGVPrinter.PrintDataGridView(dataGridView);
            }
            finally
            {
                // Восстанавливаем исходные видимости столбцов
                foreach (DataGridViewColumn c in dataGridView.Columns)
                {
                    if (vis.ContainsKey(c.Name))
                        c.Visible = vis[c.Name];
                }

                // Восстановим оригинальные настройки страницы
                try
                {
                    printDoc.DefaultPageSettings.PaperSize = originalPaperSize;
                    printDoc.DefaultPageSettings.Margins = originalMargins;
                    printDoc.DefaultPageSettings.Landscape = originalLandscape;
                }
                catch
                {
                    // игнорируем возможные ошибки восстановления
                }
            }
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

        public bool addFile( int id, string nameObject)  //сохраняем файл в базу
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

        public bool deleteFile(int id, string nameObject) //удаляем файл из базы
        {
            bool flag = false;
            byte[] _fileBytes = null;
            string fileFormat = null;
            string fileName = null;

            if (MessageBox.Show("Вы уверены, что хотите удалить файл ?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
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

        public void readingFile(int id, string nameObject) // получаем файл из БД
        {
            saveFileDialog1.Filter = "All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;

            // получаем данные из БД
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
