namespace BasaKIPiA
{
    partial class EditDeviceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDeviceForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Okey = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.btnDeleteFile = new System.Windows.Forms.Button();
            this.btnInsertFile = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nudInterval = new System.Windows.Forms.NumericUpDown();
            this.dtpYearPoverka = new System.Windows.Forms.DateTimePicker();
            this.txbKvString = new System.Windows.Forms.TextBox();
            this.nudKvartal = new System.Windows.Forms.NumericUpDown();
            this.txb_Poziciya = new System.Windows.Forms.TextBox();
            this.txb_Object = new System.Windows.Forms.TextBox();
            this.txb_NextPov = new System.Windows.Forms.TextBox();
            this.txb_EdinIzm = new System.Windows.Forms.TextBox();
            this.txb_Predel = new System.Windows.Forms.TextBox();
            this.txb_KolvoIzmKan = new System.Windows.Forms.TextBox();
            this.txb_KlassTochn = new System.Windows.Forms.TextBox();
            this.cbx_Manufacturer = new System.Windows.Forms.ComboBox();
            this.txb_FabricNumber = new System.Windows.Forms.TextBox();
            this.txb_Model = new System.Windows.Forms.TextBox();
            this.cbx_Type = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKvartal)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.btn_Okey);
            this.panel1.Controls.Add(this.btn_Cancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 626);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(474, 63);
            this.panel1.TabIndex = 0;
            // 
            // btn_Okey
            // 
            this.btn_Okey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btn_Okey.ForeColor = System.Drawing.Color.White;
            this.btn_Okey.Location = new System.Drawing.Point(306, 25);
            this.btn_Okey.Name = "btn_Okey";
            this.btn_Okey.Size = new System.Drawing.Size(75, 34);
            this.btn_Okey.TabIndex = 1;
            this.btn_Okey.Text = "ОК";
            this.btn_Okey.UseVisualStyleBackColor = false;
            this.btn_Okey.Click += new System.EventHandler(this.btn_Okey_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.Red;
            this.btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel.Location = new System.Drawing.Point(387, 25);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 34);
            this.btn_Cancel.TabIndex = 0;
            this.btn_Cancel.Text = "Отмена";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.btnReadFile);
            this.panel2.Controls.Add(this.btnDeleteFile);
            this.panel2.Controls.Add(this.btnInsertFile);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.nudInterval);
            this.panel2.Controls.Add(this.dtpYearPoverka);
            this.panel2.Controls.Add(this.txbKvString);
            this.panel2.Controls.Add(this.nudKvartal);
            this.panel2.Controls.Add(this.txb_Poziciya);
            this.panel2.Controls.Add(this.txb_Object);
            this.panel2.Controls.Add(this.txb_NextPov);
            this.panel2.Controls.Add(this.txb_EdinIzm);
            this.panel2.Controls.Add(this.txb_Predel);
            this.panel2.Controls.Add(this.txb_KolvoIzmKan);
            this.panel2.Controls.Add(this.txb_KlassTochn);
            this.panel2.Controls.Add(this.cbx_Manufacturer);
            this.panel2.Controls.Add(this.txb_FabricNumber);
            this.panel2.Controls.Add(this.txb_Model);
            this.panel2.Controls.Add(this.cbx_Type);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(474, 626);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnReadFile
            // 
            this.btnReadFile.BackColor = System.Drawing.Color.Olive;
            this.btnReadFile.ForeColor = System.Drawing.Color.White;
            this.btnReadFile.Location = new System.Drawing.Point(185, 536);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(75, 33);
            this.btnReadFile.TabIndex = 60;
            this.btnReadFile.Text = "Извлечь";
            this.btnReadFile.UseVisualStyleBackColor = false;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.BackColor = System.Drawing.Color.Red;
            this.btnDeleteFile.ForeColor = System.Drawing.Color.White;
            this.btnDeleteFile.Location = new System.Drawing.Point(185, 497);
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(75, 33);
            this.btnDeleteFile.TabIndex = 59;
            this.btnDeleteFile.Text = "Удалить";
            this.btnDeleteFile.UseVisualStyleBackColor = false;
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            // 
            // btnInsertFile
            // 
            this.btnInsertFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnInsertFile.ForeColor = System.Drawing.Color.White;
            this.btnInsertFile.Location = new System.Drawing.Point(185, 459);
            this.btnInsertFile.Name = "btnInsertFile";
            this.btnInsertFile.Size = new System.Drawing.Size(75, 33);
            this.btnInsertFile.TabIndex = 58;
            this.btnInsertFile.Text = "Добавить";
            this.btnInsertFile.UseVisualStyleBackColor = false;
            this.btnInsertFile.Click += new System.EventHandler(this.btnInsertFile_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 422);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(223, 13);
            this.label14.TabIndex = 57;
            this.label14.Text = "Добавление, удаление, извлечение файла";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(15, 459);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 129);
            this.pictureBox1.TabIndex = 56;
            this.pictureBox1.TabStop = false;
            // 
            // nudInterval
            // 
            this.nudInterval.Increment = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudInterval.Location = new System.Drawing.Point(209, 286);
            this.nudInterval.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.nudInterval.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.ReadOnly = true;
            this.nudInterval.Size = new System.Drawing.Size(56, 20);
            this.nudInterval.TabIndex = 55;
            this.nudInterval.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudInterval.ValueChanged += new System.EventHandler(this.nudInterval_ValueChanged);
            // 
            // dtpYearPoverka
            // 
            this.dtpYearPoverka.CustomFormat = "yyyy";
            this.dtpYearPoverka.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpYearPoverka.Location = new System.Drawing.Point(298, 257);
            this.dtpYearPoverka.MinDate = new System.DateTime(1999, 1, 1, 0, 0, 0, 0);
            this.dtpYearPoverka.Name = "dtpYearPoverka";
            this.dtpYearPoverka.ShowUpDown = true;
            this.dtpYearPoverka.Size = new System.Drawing.Size(83, 20);
            this.dtpYearPoverka.TabIndex = 54;
            this.dtpYearPoverka.ValueChanged += new System.EventHandler(this.dtpYearPoverka_ValueChanged);
            // 
            // txbKvString
            // 
            this.txbKvString.Location = new System.Drawing.Point(254, 257);
            this.txbKvString.Name = "txbKvString";
            this.txbKvString.ReadOnly = true;
            this.txbKvString.Size = new System.Drawing.Size(38, 20);
            this.txbKvString.TabIndex = 53;
            this.txbKvString.Text = "кв";
            // 
            // nudKvartal
            // 
            this.nudKvartal.Location = new System.Drawing.Point(209, 257);
            this.nudKvartal.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudKvartal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudKvartal.Name = "nudKvartal";
            this.nudKvartal.ReadOnly = true;
            this.nudKvartal.Size = new System.Drawing.Size(38, 20);
            this.nudKvartal.TabIndex = 52;
            this.nudKvartal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudKvartal.ValueChanged += new System.EventHandler(this.nudKvartal_ValueChanged);
            // 
            // txb_Poziciya
            // 
            this.txb_Poziciya.Location = new System.Drawing.Point(209, 374);
            this.txb_Poziciya.Name = "txb_Poziciya";
            this.txb_Poziciya.Size = new System.Drawing.Size(237, 20);
            this.txb_Poziciya.TabIndex = 51;
            // 
            // txb_Object
            // 
            this.txb_Object.Location = new System.Drawing.Point(209, 345);
            this.txb_Object.Name = "txb_Object";
            this.txb_Object.ReadOnly = true;
            this.txb_Object.Size = new System.Drawing.Size(237, 20);
            this.txb_Object.TabIndex = 50;
            // 
            // txb_NextPov
            // 
            this.txb_NextPov.Location = new System.Drawing.Point(209, 316);
            this.txb_NextPov.Name = "txb_NextPov";
            this.txb_NextPov.ReadOnly = true;
            this.txb_NextPov.Size = new System.Drawing.Size(134, 20);
            this.txb_NextPov.TabIndex = 49;
            // 
            // txb_EdinIzm
            // 
            this.txb_EdinIzm.Location = new System.Drawing.Point(209, 228);
            this.txb_EdinIzm.Name = "txb_EdinIzm";
            this.txb_EdinIzm.Size = new System.Drawing.Size(100, 20);
            this.txb_EdinIzm.TabIndex = 46;
            // 
            // txb_Predel
            // 
            this.txb_Predel.Location = new System.Drawing.Point(209, 199);
            this.txb_Predel.Name = "txb_Predel";
            this.txb_Predel.Size = new System.Drawing.Size(100, 20);
            this.txb_Predel.TabIndex = 45;
            // 
            // txb_KolvoIzmKan
            // 
            this.txb_KolvoIzmKan.Location = new System.Drawing.Point(209, 170);
            this.txb_KolvoIzmKan.Name = "txb_KolvoIzmKan";
            this.txb_KolvoIzmKan.Size = new System.Drawing.Size(100, 20);
            this.txb_KolvoIzmKan.TabIndex = 44;
            // 
            // txb_KlassTochn
            // 
            this.txb_KlassTochn.Location = new System.Drawing.Point(209, 142);
            this.txb_KlassTochn.Name = "txb_KlassTochn";
            this.txb_KlassTochn.Size = new System.Drawing.Size(100, 20);
            this.txb_KlassTochn.TabIndex = 43;
            // 
            // cbx_Manufacturer
            // 
            this.cbx_Manufacturer.BackColor = System.Drawing.SystemColors.HotTrack;
            this.cbx_Manufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_Manufacturer.FormattingEnabled = true;
            this.cbx_Manufacturer.Location = new System.Drawing.Point(209, 84);
            this.cbx_Manufacturer.Name = "cbx_Manufacturer";
            this.cbx_Manufacturer.Size = new System.Drawing.Size(237, 21);
            this.cbx_Manufacturer.TabIndex = 42;
            // 
            // txb_FabricNumber
            // 
            this.txb_FabricNumber.Location = new System.Drawing.Point(209, 114);
            this.txb_FabricNumber.Name = "txb_FabricNumber";
            this.txb_FabricNumber.Size = new System.Drawing.Size(237, 20);
            this.txb_FabricNumber.TabIndex = 41;
            // 
            // txb_Model
            // 
            this.txb_Model.Location = new System.Drawing.Point(209, 55);
            this.txb_Model.Name = "txb_Model";
            this.txb_Model.Size = new System.Drawing.Size(237, 20);
            this.txb_Model.TabIndex = 40;
            // 
            // cbx_Type
            // 
            this.cbx_Type.BackColor = System.Drawing.SystemColors.HotTrack;
            this.cbx_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_Type.FormattingEnabled = true;
            this.cbx_Type.Location = new System.Drawing.Point(209, 27);
            this.cbx_Type.Name = "cbx_Type";
            this.cbx_Type.Size = new System.Drawing.Size(237, 21);
            this.cbx_Type.TabIndex = 39;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 382);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 13);
            this.label13.TabIndex = 38;
            this.label13.Text = "Место установки, позиция";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 353);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 13);
            this.label12.TabIndex = 37;
            this.label12.Text = "Объект";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 323);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "Дата следующей поверки";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 293);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(170, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "Межповерочный интервал, мес.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 265);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Дата последней поверки";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 235);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Единицы измерения";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Предел (диапазон измерения)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Кол-во измер.каналов";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Класс точности";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Производитель";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Модель";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Тип";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Заводской номер";
            // 
            // EditDeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 689);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(490, 950);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(490, 728);
            this.Name = "EditDeviceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование данных прибора";
            this.Load += new System.EventHandler(this.EditDeviceForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKvartal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txb_Poziciya;
        private System.Windows.Forms.TextBox txb_Object;
        private System.Windows.Forms.TextBox txb_NextPov;
        private System.Windows.Forms.TextBox txb_EdinIzm;
        private System.Windows.Forms.TextBox txb_Predel;
        private System.Windows.Forms.TextBox txb_KolvoIzmKan;
        private System.Windows.Forms.TextBox txb_KlassTochn;
        private System.Windows.Forms.ComboBox cbx_Manufacturer;
        private System.Windows.Forms.TextBox txb_FabricNumber;
        private System.Windows.Forms.TextBox txb_Model;
        private System.Windows.Forms.ComboBox cbx_Type;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Okey;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox txbKvString;
        private System.Windows.Forms.NumericUpDown nudKvartal;
        private System.Windows.Forms.NumericUpDown nudInterval;
        private System.Windows.Forms.DateTimePicker dtpYearPoverka;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Button btnDeleteFile;
        private System.Windows.Forms.Button btnInsertFile;
        private System.Windows.Forms.Label label14;
    }
}