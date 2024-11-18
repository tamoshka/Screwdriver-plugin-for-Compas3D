namespace ScrewdriverPlugin
{
    partial class MainForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TextBoxRodLength = new System.Windows.Forms.TextBox();
            this.TextBoxRodWidth = new System.Windows.Forms.TextBox();
            this.TextBoxHandleWidth = new System.Windows.Forms.TextBox();
            this.TextBoxHandleLength = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ButtonCreate = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ComboBoxShapeOfHandle = new System.Windows.Forms.ComboBox();
            this.ComboBoxShapeOfRod = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Длина ручки";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Длина наконечника";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Диаметр ручки";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Диаметра наконечника";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Форма ручки";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Форма наконечника";
            // 
            // TextBoxRodLength
            // 
            this.TextBoxRodLength.BackColor = System.Drawing.SystemColors.Window;
            this.TextBoxRodLength.Location = new System.Drawing.Point(228, 189);
            this.TextBoxRodLength.Name = "TextBoxRodLength";
            this.TextBoxRodLength.Size = new System.Drawing.Size(133, 26);
            this.TextBoxRodLength.TabIndex = 10;
            this.TextBoxRodLength.Leave += new System.EventHandler(this.TextBoxRodLength_Leave);
            // 
            // TextBoxRodWidth
            // 
            this.TextBoxRodWidth.BackColor = System.Drawing.SystemColors.Window;
            this.TextBoxRodWidth.Location = new System.Drawing.Point(228, 229);
            this.TextBoxRodWidth.Name = "TextBoxRodWidth";
            this.TextBoxRodWidth.Size = new System.Drawing.Size(133, 26);
            this.TextBoxRodWidth.TabIndex = 11;
            this.TextBoxRodWidth.Leave += new System.EventHandler(this.TextBoxRodWidth_Leave);
            // 
            // TextBoxHandleWidth
            // 
            this.TextBoxHandleWidth.BackColor = System.Drawing.SystemColors.Window;
            this.TextBoxHandleWidth.Location = new System.Drawing.Point(228, 89);
            this.TextBoxHandleWidth.Name = "TextBoxHandleWidth";
            this.TextBoxHandleWidth.Size = new System.Drawing.Size(133, 26);
            this.TextBoxHandleWidth.TabIndex = 12;
            this.TextBoxHandleWidth.Leave += new System.EventHandler(this.TextBoxHandleWidth_Leave);
            // 
            // TextBoxHandleLength
            // 
            this.TextBoxHandleLength.BackColor = System.Drawing.SystemColors.Window;
            this.TextBoxHandleLength.Location = new System.Drawing.Point(228, 49);
            this.TextBoxHandleLength.Name = "TextBoxHandleLength";
            this.TextBoxHandleLength.Size = new System.Drawing.Size(133, 26);
            this.TextBoxHandleLength.TabIndex = 13;
            this.TextBoxHandleLength.Leave += new System.EventHandler(this.TextBoxHandleLength_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(390, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "45-150 мм";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(390, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(277, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Четверть от длины ручки (+/- 5 мм)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(390, 192);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(283, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "45-500 мм, больше чем длина ручки";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(390, 232);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(253, 20);
            this.label10.TabIndex = 17;
            this.label10.Text = "1/2 диаметра ручки или меньше";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // ButtonCreate
            // 
            this.ButtonCreate.Location = new System.Drawing.Point(330, 285);
            this.ButtonCreate.Name = "ButtonCreate";
            this.ButtonCreate.Size = new System.Drawing.Size(140, 35);
            this.ButtonCreate.TabIndex = 18;
            this.ButtonCreate.Text = "Создать";
            this.ButtonCreate.UseVisualStyleBackColor = true;
            this.ButtonCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            // 
            // ComboBoxShapeOfHandle
            // 
            this.ComboBoxShapeOfHandle.FormattingEnabled = true;
            this.ComboBoxShapeOfHandle.Items.AddRange(new object[] {
            "Цилиндрическая",
            "Шестиугольная призма"});
            this.ComboBoxShapeOfHandle.Location = new System.Drawing.Point(228, 9);
            this.ComboBoxShapeOfHandle.Name = "ComboBoxShapeOfHandle";
            this.ComboBoxShapeOfHandle.Size = new System.Drawing.Size(133, 28);
            this.ComboBoxShapeOfHandle.TabIndex = 20;
            this.ComboBoxShapeOfHandle.SelectedIndexChanged += new System.EventHandler(this.ComboBoxShapeOfHandle_SelectedIndexChanged);
            // 
            // ComboBoxShapeOfRod
            // 
            this.ComboBoxShapeOfRod.FormattingEnabled = true;
            this.ComboBoxShapeOfRod.Items.AddRange(new object[] {
            "Крестообразная",
            "Плоская"});
            this.ComboBoxShapeOfRod.Location = new System.Drawing.Point(228, 149);
            this.ComboBoxShapeOfRod.Name = "ComboBoxShapeOfRod";
            this.ComboBoxShapeOfRod.Size = new System.Drawing.Size(133, 28);
            this.ComboBoxShapeOfRod.TabIndex = 21;
            this.ComboBoxShapeOfRod.SelectedIndexChanged += new System.EventHandler(this.ComboBoxShapeOfRod_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(390, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(319, 20);
            this.label11.TabIndex = 22;
            this.label11.Text = "Шестиугольная призма/Цилиндрическая";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(390, 152);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(203, 20);
            this.label12.TabIndex = 23;
            this.label12.Text = "Крестообразная/Плоская";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 364);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ComboBoxShapeOfRod);
            this.Controls.Add(this.ComboBoxShapeOfHandle);
            this.Controls.Add(this.ButtonCreate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TextBoxHandleLength);
            this.Controls.Add(this.TextBoxHandleWidth);
            this.Controls.Add(this.TextBoxRodWidth);
            this.Controls.Add(this.TextBoxRodLength);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Отвёртка";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBoxRodLength;
        private System.Windows.Forms.TextBox TextBoxRodWidth;
        private System.Windows.Forms.TextBox TextBoxHandleWidth;
        private System.Windows.Forms.TextBox TextBoxHandleLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button ButtonCreate;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox ComboBoxShapeOfHandle;
        private System.Windows.Forms.ComboBox ComboBoxShapeOfRod;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}

