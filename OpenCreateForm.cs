using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;

namespace BasaKIPiA
{
    public partial class OpenCreateForm : Form
    {
        
        
        public OpenCreateForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Data base(*.dbs)|*.dbs";
            if(openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            D.DATA = openFileDialog1.FileName;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Data base(*.dbs)|*.dbs";
            if(saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            D.DATA = saveFileDialog1.FileName;
            StreamWriter sw = new StreamWriter(D.DATA);
            sw.Close();
            SQLiteConnection conn = new SQLiteConnection();
            SQLiteCommand command = new SQLiteCommand();
            if (!File.Exists(D.DATA))
                SQLiteConnection.CreateFile(D.DATA);

            try
            {
                conn = new SQLiteConnection("Data Source=" + D.DATA + ";Version=3;");
                conn.Open();
                command.Connection = conn;

               
                command.CommandText = "CREATE TABLE manufacturers ([id]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [title]	TEXT NOT NULL)";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE types ([id]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [type] TEXT NOT NULL)";
                command.ExecuteNonQuery();

                conn.Close();
            }
            catch(SQLiteException ex)
            {
                MessageBox.Show("Error " + ex.Message);

            }

            this.Close();
        }
    }
}
