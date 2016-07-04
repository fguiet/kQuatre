namespace Guiet.kQuatre.UI
{
    partial class TestForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.cbxReceptor = new System.Windows.Forms.ComboBox();
            this.lblBoitier = new System.Windows.Forms.Label();
            this.btnLaunchTest = new System.Windows.Forms.Button();
            this.lblResultat = new System.Windows.Forms.Label();
            this.lblPuissance = new System.Windows.Forms.Label();
            this.lblLastKnownPower = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gvResistance = new Telerik.WinControls.UI.RadGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvResistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvResistance.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(513, 591);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(119, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Femer la fenêtre";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbxReceptor
            // 
            this.cbxReceptor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxReceptor.FormattingEnabled = true;
            this.cbxReceptor.Location = new System.Drawing.Point(137, 22);
            this.cbxReceptor.Name = "cbxReceptor";
            this.cbxReceptor.Size = new System.Drawing.Size(293, 21);
            this.cbxReceptor.TabIndex = 11;
            this.cbxReceptor.SelectedIndexChanged += new System.EventHandler(this.cbxReceptor_SelectedIndexChanged);
            // 
            // lblBoitier
            // 
            this.lblBoitier.AutoSize = true;
            this.lblBoitier.Location = new System.Drawing.Point(13, 25);
            this.lblBoitier.Name = "lblBoitier";
            this.lblBoitier.Size = new System.Drawing.Size(118, 13);
            this.lblBoitier.TabIndex = 12;
            this.lblBoitier.Text = "Sélectionner un boitier :";
            // 
            // btnLaunchTest
            // 
            this.btnLaunchTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLaunchTest.Location = new System.Drawing.Point(501, 89);
            this.btnLaunchTest.Name = "btnLaunchTest";
            this.btnLaunchTest.Size = new System.Drawing.Size(119, 23);
            this.btnLaunchTest.TabIndex = 13;
            this.btnLaunchTest.Text = "Lancer le test";
            this.btnLaunchTest.UseVisualStyleBackColor = true;
            this.btnLaunchTest.Click += new System.EventHandler(this.btnLaunchTest_Click);
            // 
            // lblResultat
            // 
            this.lblResultat.AutoSize = true;
            this.lblResultat.Location = new System.Drawing.Point(13, 29);
            this.lblResultat.Name = "lblResultat";
            this.lblResultat.Size = new System.Drawing.Size(213, 13);
            this.lblResultat.TabIndex = 14;
            this.lblResultat.Text = "Messages envoyés : 0, reçus : 0, perdus : 0";
            // 
            // lblPuissance
            // 
            this.lblPuissance.AutoSize = true;
            this.lblPuissance.Location = new System.Drawing.Point(13, 61);
            this.lblPuissance.Name = "lblPuissance";
            this.lblPuissance.Size = new System.Drawing.Size(125, 13);
            this.lblPuissance.TabIndex = 15;
            this.lblPuissance.Text = "Puissance du signal : NA";
            // 
            // lblLastKnownPower
            // 
            this.lblLastKnownPower.AutoSize = true;
            this.lblLastKnownPower.Location = new System.Drawing.Point(209, 61);
            this.lblLastKnownPower.Name = "lblLastKnownPower";
            this.lblLastKnownPower.Size = new System.Drawing.Size(206, 13);
            this.lblLastKnownPower.TabIndex = 16;
            this.lblLastKnownPower.Text = "Dernière puissance connue du signal : NA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Plus le signal est faible moins c\'est bon";
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(481, 61);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(111, 13);
            this.lblDistance.TabIndex = 19;
            this.lblDistance.Text = "Distance approx. : NA";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBoitier);
            this.groupBox1.Controls.Add(this.cbxReceptor);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 58);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Boitiers à tester";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblResultat);
            this.groupBox2.Controls.Add(this.lblPuissance);
            this.groupBox2.Controls.Add(this.lblDistance);
            this.groupBox2.Controls.Add(this.btnLaunchTest);
            this.groupBox2.Controls.Add(this.lblLastKnownPower);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(626, 119);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test de connexion";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gvResistance);
            this.groupBox3.Location = new System.Drawing.Point(12, 201);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(626, 384);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Test de résistance des lignes";
            // 
            // gvResistance
            // 
            this.gvResistance.Location = new System.Drawing.Point(6, 19);
            this.gvResistance.Name = "gvResistance";
            this.gvResistance.Size = new System.Drawing.Size(614, 359);
            this.gvResistance.TabIndex = 0;
            this.gvResistance.Text = "Résistance";
            this.gvResistance.CommandCellClick += new Telerik.WinControls.UI.CommandCellClickEventHandler(this.gvResistance_CommandCellClick);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 626);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tests (connexion, résistance...)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnexionForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvResistance.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvResistance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbxReceptor;
        private System.Windows.Forms.Label lblBoitier;
        private System.Windows.Forms.Button btnLaunchTest;
        private System.Windows.Forms.Label lblResultat;
        private System.Windows.Forms.Label lblPuissance;
        private System.Windows.Forms.Label lblLastKnownPower;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Telerik.WinControls.UI.RadGridView gvResistance;

    }
}