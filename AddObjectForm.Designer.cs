namespace BasaKIPiA
{
    partial class AddObjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddObjectForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txb_NameObject = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_AddObject = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название объекта";
            // 
            // txb_NameObject
            // 
            this.txb_NameObject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_NameObject.Location = new System.Drawing.Point(122, 46);
            this.txb_NameObject.Name = "txb_NameObject";
            this.txb_NameObject.Size = new System.Drawing.Size(305, 20);
            this.txb_NameObject.TabIndex = 1;
            this.txb_NameObject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_NameObject_KeyPress);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.Red;
            this.btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel.Location = new System.Drawing.Point(352, 100);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 33);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "Отмена";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_AddObject
            // 
            this.btn_AddObject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btn_AddObject.ForeColor = System.Drawing.Color.White;
            this.btn_AddObject.Location = new System.Drawing.Point(271, 100);
            this.btn_AddObject.Name = "btn_AddObject";
            this.btn_AddObject.Size = new System.Drawing.Size(75, 33);
            this.btn_AddObject.TabIndex = 3;
            this.btn_AddObject.Text = "Добавить";
            this.btn_AddObject.UseVisualStyleBackColor = false;
            this.btn_AddObject.Click += new System.EventHandler(this.btn_AddObject_Click);
            // 
            // AddObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(447, 135);
            this.Controls.Add(this.btn_AddObject);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.txb_NameObject);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddObjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить объект";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_NameObject;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_AddObject;
    }
}