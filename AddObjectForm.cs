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
    public partial class AddObjectForm : Form
    {
        MetodsWorkDataBase mwdb = new MetodsWorkDataBase();
        public AddObjectForm()
        {
            InitializeComponent();
            txb_NameObject.ShortcutsEnabled = false;
        }

        private void btn_AddObject_Click(object sender, EventArgs e)
        {
            string nameObject = txb_NameObject.Text.Trim();
            if (nameObject == "")
            {
                MessageBox.Show("Введите название объекта");
                return;
            }

            nameObject = nameObject.Replace(" ", "_");

            mwdb.createTablesObject(D.DATA, nameObject);
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txb_NameObject_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
            char key = e.KeyChar;
            if (txb_NameObject.Text == "" && Char.IsDigit(key))
            {
                e.Handled = true;
                MessageBox.Show("Имя объекта не должно начинаться с цифры");
            }

            if ((key == ',') || (key == '.') || (key == ';') || (key == ':') || (key == '-') || (key == '+') || (key == '=') || key == '?' || key == '!' || key == '"' || key == '@' || key == '\'' || key == '\\' || key == '|' || key == '/' || key == '<' || key == '>' || key == '#' || key == '$' || key == '^' || key == '&' || key == '!' || key == '%' || key == '*' || key == '{' || key == '}' || key == '`' || key == '~' || key == '(' || key == ')')
            {
                e.Handled = true;

            }
        }

        //private void txb_NameObject_TextChanged(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    //    if (txb_NameObject.Focused)
        //    //    {
        //    //        string clipdata = Clipboard.GetText();

        //    //        if (txb_NameObject.Text.Contains(clipdata))
        //    //        {
        //    //            txb_NameObject.Clear();
        //    //        }
        //    //    }
        //    //}
        //    //catch
        //    //{

        //    //}
        //}
    }
}
