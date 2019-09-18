namespace NewEventTool
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.restBtn = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(194, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(158, 20);
            this.comboBox1.TabIndex = 0;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(194, 53);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(158, 20);
            this.comboBox2.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("新細明體", 12F);
            this.richTextBox1.Location = new System.Drawing.Point(67, 116);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(285, 415);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(64, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "PORT :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(64, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "BAUD  RATE : ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(379, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "OPEN";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(379, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "CLOSE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 88);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(285, 22);
            this.textBox1.TabIndex = 9;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("新細明體", 12F);
            this.richTextBox2.Location = new System.Drawing.Point(379, 116);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(285, 415);
            this.richTextBox2.TabIndex = 10;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.RichTextBox2_TextChanged_1);
            // 
            // richTextBox3
            // 
            this.richTextBox3.Font = new System.Drawing.Font("新細明體", 12F);
            this.richTextBox3.Location = new System.Drawing.Point(687, 116);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.Size = new System.Drawing.Size(285, 415);
            this.richTextBox3.TabIndex = 11;
            this.richTextBox3.Text = "";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(379, 88);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(76, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "SELECT";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.comboBox3);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(687, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 100);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Attenuator";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("新細明體", 18F);
            this.textBox4.Location = new System.Drawing.Point(212, 11);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(67, 83);
            this.textBox4.TabIndex = 45;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Increase",
            "Decrease",
            "OneRun"});
            this.comboBox3.Location = new System.Drawing.Point(101, 74);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(93, 20);
            this.comboBox3.TabIndex = 14;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(151, 42);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(43, 22);
            this.textBox3.TabIndex = 20;
            this.textBox3.Text = "3";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(151, 13);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(43, 22);
            this.textBox2.TabIndex = 14;
            this.textBox2.Text = "0";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("新細明體", 9F);
            this.label8.Location = new System.Drawing.Point(113, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "Step";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 9F);
            this.label7.Location = new System.Drawing.Point(109, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "Value";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 9F);
            this.label6.Location = new System.Drawing.Point(6, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "Attenuator(NA)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 9F);
            this.label5.Location = new System.Drawing.Point(6, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "Attenuator(NA)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 9F);
            this.label4.Location = new System.Drawing.Point(6, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "Attenuator(NA)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 9F);
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "Attenuator(NA)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Location = new System.Drawing.Point(589, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(75, 100);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Loop";
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("新細明體", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox5.Location = new System.Drawing.Point(6, 40);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(62, 29);
            this.textBox5.TabIndex = 47;
            this.textBox5.Text = "300";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(475, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 100);
            this.button4.TabIndex = 47;
            this.button4.Text = "Run";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // restBtn
            // 
            this.restBtn.Location = new System.Drawing.Point(67, 539);
            this.restBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.restBtn.Name = "restBtn";
            this.restBtn.Size = new System.Drawing.Size(285, 40);
            this.restBtn.TabIndex = 48;
            this.restBtn.Text = "Reset";
            this.restBtn.UseVisualStyleBackColor = true;
            this.restBtn.Click += new System.EventHandler(this.RestBtn_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(379, 539);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(285, 40);
            this.button5.TabIndex = 49;
            this.button5.Text = "Stop";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(686, 539);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 16);
            this.label9.TabIndex = 50;
            this.label9.Text = "Feedback Times";
            this.label9.Click += new System.EventHandler(this.Label9_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(807, 539);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 16);
            this.label10.TabIndex = 51;
            this.label10.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(863, 563);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 16);
            this.label11.TabIndex = 52;
            this.label11.Text = "Error Times";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(956, 563);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 16);
            this.label12.TabIndex = 53;
            this.label12.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 583);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.restBtn);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Get Key Event 2.4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button restBtn;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

