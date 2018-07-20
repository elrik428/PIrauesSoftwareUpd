namespace Piraeus_UpdtSoftwareVC
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
            this.textBox_MultTid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listView_TID = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.listView_TID_noCtls = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_MultTid
            // 
            this.textBox_MultTid.Location = new System.Drawing.Point(82, 37);
            this.textBox_MultTid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_MultTid.Name = "textBox_MultTid";
            this.textBox_MultTid.Size = new System.Drawing.Size(355, 23);
            this.textBox_MultTid.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "TID List : ";
            // 
            // listView_TID
            // 
            this.listView_TID.Location = new System.Drawing.Point(82, 84);
            this.listView_TID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listView_TID.Name = "listView_TID";
            this.listView_TID.Size = new System.Drawing.Size(355, 200);
            this.listView_TID.TabIndex = 2;
            this.listView_TID.UseCompatibleStateImageBehavior = false;
            this.listView_TID.View = System.Windows.Forms.View.Details;
            this.listView_TID.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_TID_ColumnClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(462, 37);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(462, 159);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 51);
            this.button2.TabIndex = 4;
            this.button2.Text = "Proceed to Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Details : ";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(481, 231);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(61, 22);
            this.button3.TabIndex = 8;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listView_TID_noCtls
            // 
            this.listView_TID_noCtls.Location = new System.Drawing.Point(82, 319);
            this.listView_TID_noCtls.Name = "listView_TID_noCtls";
            this.listView_TID_noCtls.Size = new System.Drawing.Size(355, 97);
            this.listView_TID_noCtls.TabIndex = 9;
            this.listView_TID_noCtls.UseCompatibleStateImageBehavior = false;
            this.listView_TID_noCtls.View = System.Windows.Forms.View.Details;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "No CTLS :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 461);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView_TID_noCtls);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView_TID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_MultTid);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Update Software version to VeriCentre";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_MultTid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView_TID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListView listView_TID_noCtls;
        private System.Windows.Forms.Label label2;
    }
}

