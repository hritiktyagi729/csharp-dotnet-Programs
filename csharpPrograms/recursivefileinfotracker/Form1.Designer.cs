namespace t8
{
    partial class Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.browsesource = new System.Windows.Forms.Button();
            this.nasdatapath = new System.Windows.Forms.Button();
            this.txt_source = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.start = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 463);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(219, 150);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // browsesource
            // 
            this.browsesource.Location = new System.Drawing.Point(3, 3);
            this.browsesource.Name = "browsesource";
            this.browsesource.Size = new System.Drawing.Size(600, 69);
            this.browsesource.TabIndex = 0;
            this.browsesource.Text = "Browse Source Recycle Folder";
            this.browsesource.UseVisualStyleBackColor = true;
            this.browsesource.Click += new System.EventHandler(this.Browsesource_Click);
            // 
            // nasdatapath
            // 
            this.nasdatapath.Location = new System.Drawing.Point(3, 153);
            this.nasdatapath.Name = "nasdatapath";
            this.nasdatapath.Size = new System.Drawing.Size(600, 69);
            this.nasdatapath.TabIndex = 1;
            this.nasdatapath.Text = "Main Nasa Data PAth";
            this.nasdatapath.UseVisualStyleBackColor = true;
            this.nasdatapath.Click += new System.EventHandler(this.Nasdatapath_Click);
            // 
            // txt_source
            // 
            this.txt_source.Location = new System.Drawing.Point(3, 78);
            this.txt_source.Name = "txt_source";
            this.txt_source.Size = new System.Drawing.Size(600, 69);
            this.txt_source.TabIndex = 2;
            this.txt_source.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(3, 228);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(600, 71);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(765, 177);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(111, 75);
            this.start.TabIndex = 1;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.Start_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.richTextBox2, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.nasdatapath, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txt_source, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.browsesource, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(66, 24);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(606, 302);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 625);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.start);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button browsesource;
        private System.Windows.Forms.Button nasdatapath;
        private System.Windows.Forms.RichTextBox txt_source;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}

