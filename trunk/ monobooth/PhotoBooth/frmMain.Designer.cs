namespace PhotoBooth
{
    partial class frmMain
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
            this.cmdStart = new System.Windows.Forms.Button();
            this.imgPrompts = new System.Windows.Forms.PictureBox();
            this.imgStrip4 = new System.Windows.Forms.PictureBox();
            this.imgStrip3 = new System.Windows.Forms.PictureBox();
            this.imgStrip2 = new System.Windows.Forms.PictureBox();
            this.imgStrip1 = new System.Windows.Forms.PictureBox();
            this.lblCompleteMsg = new System.Windows.Forms.Label();
            this.imgQR = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgPrompts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgStrip4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgStrip3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgStrip2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgQR)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(386, 547);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(75, 43);
            this.cmdStart.TabIndex = 5;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // imgPrompts
            // 
            this.imgPrompts.Location = new System.Drawing.Point(194, 133);
            this.imgPrompts.Name = "imgPrompts";
            this.imgPrompts.Size = new System.Drawing.Size(515, 174);
            this.imgPrompts.TabIndex = 4;
            this.imgPrompts.TabStop = false;
            // 
            // imgStrip4
            // 
            this.imgStrip4.BackColor = System.Drawing.Color.Black;
            this.imgStrip4.Location = new System.Drawing.Point(63, 428);
            this.imgStrip4.Name = "imgStrip4";
            this.imgStrip4.Size = new System.Drawing.Size(100, 100);
            this.imgStrip4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgStrip4.TabIndex = 3;
            this.imgStrip4.TabStop = false;
            // 
            // imgStrip3
            // 
            this.imgStrip3.BackColor = System.Drawing.Color.Black;
            this.imgStrip3.Location = new System.Drawing.Point(63, 322);
            this.imgStrip3.Name = "imgStrip3";
            this.imgStrip3.Size = new System.Drawing.Size(100, 100);
            this.imgStrip3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgStrip3.TabIndex = 2;
            this.imgStrip3.TabStop = false;
            // 
            // imgStrip2
            // 
            this.imgStrip2.BackColor = System.Drawing.Color.Black;
            this.imgStrip2.Location = new System.Drawing.Point(63, 216);
            this.imgStrip2.Name = "imgStrip2";
            this.imgStrip2.Size = new System.Drawing.Size(100, 100);
            this.imgStrip2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgStrip2.TabIndex = 1;
            this.imgStrip2.TabStop = false;
            // 
            // imgStrip1
            // 
            this.imgStrip1.BackColor = System.Drawing.Color.Black;
            this.imgStrip1.Location = new System.Drawing.Point(63, 110);
            this.imgStrip1.Name = "imgStrip1";
            this.imgStrip1.Size = new System.Drawing.Size(100, 100);
            this.imgStrip1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgStrip1.TabIndex = 0;
            this.imgStrip1.TabStop = false;
            // 
            // lblCompleteMsg
            // 
            this.lblCompleteMsg.AutoSize = true;
            this.lblCompleteMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompleteMsg.Location = new System.Drawing.Point(126, 9);
            this.lblCompleteMsg.Name = "lblCompleteMsg";
            this.lblCompleteMsg.Size = new System.Drawing.Size(583, 25);
            this.lblCompleteMsg.TabIndex = 6;
            this.lblCompleteMsg.Text = "Your PhotoStrip is complete. Scan this QR code to get them";
            this.lblCompleteMsg.Visible = false;
            // 
            // imgQR
            // 
            this.imgQR.BackColor = System.Drawing.Color.Black;
            this.imgQR.Location = new System.Drawing.Point(386, 34);
            this.imgQR.Name = "imgQR";
            this.imgQR.Size = new System.Drawing.Size(75, 75);
            this.imgQR.TabIndex = 7;
            this.imgQR.TabStop = false;
            this.imgQR.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(325, 408);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(73, 25);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.imgQR);
            this.Controls.Add(this.lblCompleteMsg);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.imgPrompts);
            this.Controls.Add(this.imgStrip4);
            this.Controls.Add(this.imgStrip3);
            this.Controls.Add(this.imgStrip2);
            this.Controls.Add(this.imgStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.Text = "PhotoBooth";
            this.MaximumSizeChanged += new System.EventHandler(this.frmMain_MaximumSizeChanged);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgPrompts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgStrip4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgStrip3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgStrip2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgQR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgStrip1;
        private System.Windows.Forms.PictureBox imgStrip2;
        private System.Windows.Forms.PictureBox imgStrip3;
        private System.Windows.Forms.PictureBox imgStrip4;
        private System.Windows.Forms.PictureBox imgPrompts;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Label lblCompleteMsg;
        private System.Windows.Forms.PictureBox imgQR;
        private System.Windows.Forms.Label lblStatus;
    }
}

