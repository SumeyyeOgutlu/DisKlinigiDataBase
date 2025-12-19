namespace DisKlinikUygulama
{
    partial class AsistanSecenek
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
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.IndianRed;
            button1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button1.Location = new Point(28, 147);
            button1.Name = "button1";
            button1.Size = new Size(331, 77);
            button1.TabIndex = 0;
            button1.Text = "Ekle / Güncelle";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.IndianRed;
            button2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button2.Location = new Point(28, 278);
            button2.Name = "button2";
            button2.Size = new Size(331, 77);
            button2.TabIndex = 1;
            button2.Text = "Sil / Arama";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.MistyRose;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(123, 25);
            label1.Name = "label1";
            label1.Size = new Size(511, 72);
            label1.TabIndex = 2;
            label1.Text = "Hasta İşlemleri";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            button3.BackColor = Color.IndianRed;
            button3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button3.Location = new Point(424, 147);
            button3.Name = "button3";
            button3.Size = new Size(331, 77);
            button3.TabIndex = 3;
            button3.Text = "Randevu Oluştur";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.IndianRed;
            button4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button4.Location = new Point(424, 278);
            button4.Name = "button4";
            button4.Size = new Size(331, 77);
            button4.TabIndex = 4;
            button4.Text = "Ödeme yap";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // AsistanSecenek
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Resource1.disresmi;
            ClientSize = new Size(800, 450);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "AsistanSecenek";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AsistanSecenek";
            Load += AsistanSecenek_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
        private Button button3;
        private Button button4;
    }
}