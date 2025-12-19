namespace DisKlinikUygulama
{
    partial class Doktor
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
            dataGridView1 = new DataGridView();
            label1 = new Label();
            button1 = new Button();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            comboBox1 = new ComboBox();
            textBox2 = new TextBox();
            label4 = new Label();
            button2 = new Button();
            panel1 = new Panel();
            button3 = new Button();
            textBox4 = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            textBox3 = new TextBox();
            comboBox2 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(1, 88);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(799, 115);
            dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.BackColor = Color.MistyRose;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(1, 9);
            label1.Name = "label1";
            label1.Size = new Size(799, 67);
            label1.TabIndex = 1;
            label1.Text = "Bugün için randevularınız.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.BackColor = Color.IndianRed;
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button1.ForeColor = SystemColors.ActiveCaptionText;
            button1.Location = new Point(676, 209);
            button1.Name = "button1";
            button1.Size = new Size(112, 37);
            button1.TabIndex = 2;
            button1.Text = "Seç";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.MistyRose;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.Location = new Point(14, 12);
            label2.Name = "label2";
            label2.Size = new Size(63, 23);
            label2.TabIndex = 3;
            label2.Text = "Diş No";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(14, 38);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(90, 27);
            textBox1.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.MistyRose;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label3.Location = new Point(157, 12);
            label3.Name = "label3";
            label3.Size = new Size(114, 23);
            label3.TabIndex = 5;
            label3.Text = "Yapılan İşlem";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(157, 39);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(112, 28);
            comboBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(344, 38);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(237, 27);
            textBox2.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.MistyRose;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label4.Location = new Point(344, 12);
            label4.Name = "label4";
            label4.Size = new Size(112, 23);
            label4.TabIndex = 8;
            label4.Text = "Doktor Notu";
            // 
            // button2
            // 
            button2.BackColor = Color.IndianRed;
            button2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button2.ForeColor = SystemColors.ActiveCaptionText;
            button2.Location = new Point(675, 39);
            button2.Name = "button2";
            button2.Size = new Size(112, 37);
            button2.TabIndex = 9;
            button2.Text = "Kaydet";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(button3);
            panel1.Controls.Add(textBox4);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(1, 252);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 199);
            panel1.TabIndex = 10;
            // 
            // button3
            // 
            button3.BackColor = Color.IndianRed;
            button3.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button3.Location = new Point(672, 132);
            button3.Name = "button3";
            button3.Size = new Size(108, 35);
            button3.TabIndex = 16;
            button3.Text = "İlaç ekle";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(428, 146);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(111, 27);
            textBox4.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.MistyRose;
            label7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label7.Location = new Point(428, 101);
            label7.Name = "label7";
            label7.Size = new Size(125, 23);
            label7.TabIndex = 14;
            label7.Text = "Kullanım Şekli";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.MistyRose;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label6.Location = new Point(222, 101);
            label6.Name = "label6";
            label6.Size = new Size(49, 23);
            label6.TabIndex = 13;
            label6.Text = "Adet";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.MistyRose;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label5.Location = new Point(14, 101);
            label5.Name = "label5";
            label5.Size = new Size(58, 23);
            label5.TabIndex = 12;
            label5.Text = "İlaçlar";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(222, 145);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(111, 27);
            textBox3.TabIndex = 11;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(14, 145);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(132, 28);
            comboBox2.TabIndex = 10;
            // 
            // Doktor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Name = "Doktor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Doktor";
            Load += Doktor_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Button button1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private ComboBox comboBox1;
        private TextBox textBox2;
        private Label label4;
        private Button button2;
        private Panel panel1;
        private TextBox textBox3;
        private ComboBox comboBox2;
        private Button button3;
        private TextBox textBox4;
        private Label label7;
        private Label label6;
        private Label label5;
    }
}