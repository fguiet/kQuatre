namespace Guiet.kQuatre.UI
{
    partial class ManuelLaunchForm
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
            this.gbxDureeFeu = new System.Windows.Forms.GroupBox();
            this.lblDureeFeu = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlBoard = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOnOff = new System.Windows.Forms.Button();
            this.gbxDureeFeu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxDureeFeu
            // 
            this.gbxDureeFeu.Controls.Add(this.lblDureeFeu);
            this.gbxDureeFeu.Location = new System.Drawing.Point(12, 12);
            this.gbxDureeFeu.Name = "gbxDureeFeu";
            this.gbxDureeFeu.Size = new System.Drawing.Size(175, 80);
            this.gbxDureeFeu.TabIndex = 1;
            this.gbxDureeFeu.TabStop = false;
            this.gbxDureeFeu.Text = "Durée feu d\'artifice";
            // 
            // lblDureeFeu
            // 
            this.lblDureeFeu.AutoSize = true;
            this.lblDureeFeu.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDureeFeu.Location = new System.Drawing.Point(6, 16);
            this.lblDureeFeu.Name = "lblDureeFeu";
            this.lblDureeFeu.Size = new System.Drawing.Size(151, 59);
            this.lblDureeFeu.TabIndex = 0;
            this.lblDureeFeu.Text = "00:00";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pnlBoard);
            this.groupBox1.Location = new System.Drawing.Point(12, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(934, 546);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Console de tir";
            // 
            // pnlBoard
            // 
            this.pnlBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBoard.AutoScroll = true;
            this.pnlBoard.Location = new System.Drawing.Point(10, 19);
            this.pnlBoard.Name = "pnlBoard";
            this.pnlBoard.Size = new System.Drawing.Size(918, 521);
            this.pnlBoard.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOnOff);
            this.groupBox2.Location = new System.Drawing.Point(193, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(753, 80);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Panneau de commandes";
            // 
            // btnOnOff
            // 
            this.btnOnOff.Location = new System.Drawing.Point(6, 19);
            this.btnOnOff.Name = "btnOnOff";
            this.btnOnOff.Size = new System.Drawing.Size(100, 55);
            this.btnOnOff.TabIndex = 0;
            this.btnOnOff.Text = "OFF";
            this.btnOnOff.UseVisualStyleBackColor = true;
            this.btnOnOff.Click += new System.EventHandler(this.btnOnOff_Click);
            // 
            // ManuelLaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 656);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxDureeFeu);
            this.Name = "ManuelLaunchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lancement du feu en mode manuel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManuelLaunchForm_FormClosing);
            this.gbxDureeFeu.ResumeLayout(false);
            this.gbxDureeFeu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxDureeFeu;
        private System.Windows.Forms.Label lblDureeFeu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlBoard;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOnOff;
    }
}