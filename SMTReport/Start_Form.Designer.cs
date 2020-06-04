namespace SMTReport
{
    partial class Start_Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.NightBT = new System.Windows.Forms.Button();
            this.DayBT = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ГлавныйТаймер = new System.Windows.Forms.Timer(this.components);
            this.Status = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите режим отчета";
            // 
            // NightBT
            // 
            this.NightBT.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.NightBT.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F);
            this.NightBT.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.NightBT.Location = new System.Drawing.Point(5, 44);
            this.NightBT.Name = "NightBT";
            this.NightBT.Size = new System.Drawing.Size(195, 41);
            this.NightBT.TabIndex = 1;
            this.NightBT.Text = "SMT Ночная карта";
            this.NightBT.UseVisualStyleBackColor = false;
            this.NightBT.Click += new System.EventHandler(this.button1_Click);
            // 
            // DayBT
            // 
            this.DayBT.BackColor = System.Drawing.Color.White;
            this.DayBT.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F);
            this.DayBT.Location = new System.Drawing.Point(206, 43);
            this.DayBT.Name = "DayBT";
            this.DayBT.Size = new System.Drawing.Size(218, 42);
            this.DayBT.TabIndex = 1;
            this.DayBT.Text = "SMT Дневная карта";
            this.DayBT.UseVisualStyleBackColor = false;
            this.DayBT.Click += new System.EventHandler(this.DayBT_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F);
            this.button1.Location = new System.Drawing.Point(430, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "Выход";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PowderBlue;
            this.button2.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F);
            this.button2.Location = new System.Drawing.Point(430, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(218, 42);
            this.button2.TabIndex = 1;
            this.button2.Text = "FAS Отчет";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ГлавныйТаймер
            // 
            this.ГлавныйТаймер.Interval = 1000;
            this.ГлавныйТаймер.Tick += new System.EventHandler(this.ГлавныйТаймер_Tick);
            // 
            // Status
            // 
            this.Status.BackColor = System.Drawing.Color.Lime;
            this.Status.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Status.Location = new System.Drawing.Point(488, 9);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(160, 23);
            this.Status.TabIndex = 2;
            this.Status.Text = "Отчет запущен";
            this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button3.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F);
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Location = new System.Drawing.Point(5, 88);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(195, 41);
            this.button3.TabIndex = 1;
            this.button3.Text = "ESD Report";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Start_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 135);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.DayBT);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.NightBT);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(30, 30);
            this.Name = "Start_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Программа SMT";
            this.Load += new System.EventHandler(this.Start_Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Start_Form_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button NightBT;
        private System.Windows.Forms.Button DayBT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer ГлавныйТаймер;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Button button3;
    }
}

