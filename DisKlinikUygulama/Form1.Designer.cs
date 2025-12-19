namespace DisKlinikUygulama
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.MistyRose;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(218, 82);
            label1.Name = "label1";
            label1.Size = new Size(352, 53);
            label1.TabIndex = 0;
            label1.Text = "Lütfen Şİfre giriniz.";
            // 
            // label2
            // 
            label2.BackColor = Color.MistyRose;
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.Location = new Point(218, 228);
            label2.Name = "label2";
            label2.Size = new Size(352, 54);
            label2.TabIndex = 1;
            label2.Text = "Lütfen TC No giriniz.";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Linen;
            textBox1.Location = new Point(218, 154);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(352, 27);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Linen;
            textBox2.Location = new Point(218, 314);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(352, 27);
            textBox2.TabIndex = 3;
            // 
            // button1
            // 
            button1.BackColor = Color.RosyBrown;
            button1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button1.Location = new Point(414, 366);
            button1.Name = "button1";
            button1.Size = new Size(156, 48);
            button1.TabIndex = 4;
            button1.Text = "Giriş";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Resource1.indir;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
    }
}
