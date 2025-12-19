namespace DisKlinikUygulama
{
    partial class RandevuOlustur
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
            label1 = new Label();
            label2 = new Label();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            textBox1 = new TextBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.MistyRose;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(38, 35);
            label1.Name = "label1";
            label1.Size = new Size(113, 43);
            label1.TabIndex = 0;
            label1.Text = "Hasta seç:";
            // 
            // label2
            // 
            label2.BackColor = Color.MistyRose;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.Location = new Point(430, 35);
            label2.Name = "label2";
            label2.Size = new Size(121, 43);
            label2.TabIndex = 1;
            label2.Text = "Doktor seç:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(178, 39);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(128, 28);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(589, 39);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(128, 28);
            comboBox2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(3, 291);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(797, 159);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button1
            // 
            button1.BackColor = Color.IndianRed;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button1.Location = new Point(38, 210);
            button1.Name = "button1";
            button1.Size = new Size(134, 47);
            button1.TabIndex = 5;
            button1.Text = "ekle";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.IndianRed;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button2.Location = new Point(306, 210);
            button2.Name = "button2";
            button2.Size = new Size(134, 47);
            button2.TabIndex = 6;
            button2.Text = "sil";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.IndianRed;
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button3.Location = new Point(583, 210);
            button3.Name = "button3";
            button3.Size = new Size(134, 47);
            button3.TabIndex = 7;
            button3.Text = "güncelle";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(165, 120);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(154, 27);
            dateTimePicker1.TabIndex = 8;
            // 
            // label3
            // 
            label3.BackColor = Color.MistyRose;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label3.Location = new Point(38, 118);
            label3.Name = "label3";
            label3.Size = new Size(113, 43);
            label3.TabIndex = 9;
            label3.Text = "Tarih seç:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(583, 118);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(131, 27);
            textBox1.TabIndex = 10;
            // 
            // label4
            // 
            label4.BackColor = Color.MistyRose;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label4.Location = new Point(430, 120);
            label4.Name = "label4";
            label4.Size = new Size(113, 65);
            label4.TabIndex = 11;
            label4.Text = "Saat giriniz:";
            // 
            // RandevuOlustur
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Resource1.disresmi;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RandevuOlustur";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RandevuOlustur";
            Load += RandevuOlustur_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;
        private Button button3;
        private DateTimePicker dateTimePicker1;
        private Label label3;
        private TextBox textBox1;
        private Label label4;
    }
}