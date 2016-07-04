namespace Guiet.kQuatre.UI
{
    partial class FireworkForm
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
            this.components = new System.ComponentModel.Container();
            this.lblDesignation = new System.Windows.Forms.Label();
            this.tbxDesignation = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.dtpMiseAFeu = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDuree = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxFreeReceptorAdresses = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLineNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDesignation
            // 
            this.lblDesignation.AutoSize = true;
            this.lblDesignation.Location = new System.Drawing.Point(12, 50);
            this.lblDesignation.Name = "lblDesignation";
            this.lblDesignation.Size = new System.Drawing.Size(69, 13);
            this.lblDesignation.TabIndex = 0;
            this.lblDesignation.Text = "Désignation :";
            // 
            // tbxDesignation
            // 
            this.tbxDesignation.Location = new System.Drawing.Point(131, 47);
            this.tbxDesignation.MaxLength = 255;
            this.tbxDesignation.Name = "tbxDesignation";
            this.tbxDesignation.Size = new System.Drawing.Size(364, 20);
            this.tbxDesignation.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(339, 168);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Ok";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(420, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // dtpMiseAFeu
            // 
            this.dtpMiseAFeu.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpMiseAFeu.Location = new System.Drawing.Point(131, 73);
            this.dtpMiseAFeu.Name = "dtpMiseAFeu";
            this.dtpMiseAFeu.ShowUpDown = true;
            this.dtpMiseAFeu.Size = new System.Drawing.Size(79, 20);
            this.dtpMiseAFeu.TabIndex = 3;
            this.dtpMiseAFeu.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mise à feu :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Durée :";
            // 
            // dtpDuree
            // 
            this.dtpDuree.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpDuree.Location = new System.Drawing.Point(131, 99);
            this.dtpDuree.Name = "dtpDuree";
            this.dtpDuree.ShowUpDown = true;
            this.dtpDuree.Size = new System.Drawing.Size(79, 20);
            this.dtpDuree.TabIndex = 5;
            this.dtpDuree.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Boitier et relay associé :";
            // 
            // cbxFreeReceptorAdresses
            // 
            this.cbxFreeReceptorAdresses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFreeReceptorAdresses.FormattingEnabled = true;
            this.cbxFreeReceptorAdresses.Location = new System.Drawing.Point(131, 129);
            this.cbxFreeReceptorAdresses.Name = "cbxFreeReceptorAdresses";
            this.cbxFreeReceptorAdresses.Size = new System.Drawing.Size(207, 21);
            this.cbxFreeReceptorAdresses.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ajout d\'un artifice sur la ligne n° :";
            // 
            // lblLineNumber
            // 
            this.lblLineNumber.AutoSize = true;
            this.lblLineNumber.Location = new System.Drawing.Point(169, 22);
            this.lblLineNumber.Name = "lblLineNumber";
            this.lblLineNumber.Size = new System.Drawing.Size(22, 13);
            this.lblLineNumber.TabIndex = 11;
            this.lblLineNumber.Text = "NA";
            // 
            // FireworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 210);
            this.Controls.Add(this.lblLineNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxFreeReceptorAdresses);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDuree);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpMiseAFeu);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbxDesignation);
            this.Controls.Add(this.lblDesignation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FireworkForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajouter un artifice";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDesignation;
        private System.Windows.Forms.TextBox tbxDesignation;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ComboBox cbxFreeReceptorAdresses;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDuree;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpMiseAFeu;
        private System.Windows.Forms.Label lblLineNumber;
        private System.Windows.Forms.Label label4;
    }
}