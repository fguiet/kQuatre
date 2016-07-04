namespace Guiet.kQuatre.UI.UserControl
{
    partial class FireworkUserControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLigneNumber = new System.Windows.Forms.Label();
            this.lblDesignation = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMiseAFeu = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbStatut = new System.Windows.Forms.PictureBox();
            this.lblStatut = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDuree = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDureeDepuisMiseAFeu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatut)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLigneNumber
            // 
            this.lblLigneNumber.AutoEllipsis = true;
            this.lblLigneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLigneNumber.Location = new System.Drawing.Point(6, 0);
            this.lblLigneNumber.Name = "lblLigneNumber";
            this.lblLigneNumber.Size = new System.Drawing.Size(160, 24);
            this.lblLigneNumber.TabIndex = 1;
            this.lblLigneNumber.Text = "NA";
            this.lblLigneNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDesignation
            // 
            this.lblDesignation.AutoEllipsis = true;
            this.lblDesignation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesignation.Location = new System.Drawing.Point(6, 24);
            this.lblDesignation.Name = "lblDesignation";
            this.lblDesignation.Size = new System.Drawing.Size(160, 44);
            this.lblDesignation.TabIndex = 3;
            this.lblDesignation.Text = "NA";
            this.lblDesignation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mise à feu :";
            // 
            // lblMiseAFeu
            // 
            this.lblMiseAFeu.AutoSize = true;
            this.lblMiseAFeu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMiseAFeu.Location = new System.Drawing.Point(84, 75);
            this.lblMiseAFeu.Name = "lblMiseAFeu";
            this.lblMiseAFeu.Size = new System.Drawing.Size(24, 13);
            this.lblMiseAFeu.TabIndex = 5;
            this.lblMiseAFeu.Text = "NA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Statut :";
            // 
            // pbStatut
            // 
            this.pbStatut.Image = global::Guiet.kQuatre.Properties.Resources.standby;
            this.pbStatut.Location = new System.Drawing.Point(150, 110);
            this.pbStatut.Name = "pbStatut";
            this.pbStatut.Size = new System.Drawing.Size(16, 16);
            this.pbStatut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbStatut.TabIndex = 7;
            this.pbStatut.TabStop = false;
            // 
            // lblStatut
            // 
            this.lblStatut.AutoSize = true;
            this.lblStatut.Location = new System.Drawing.Point(71, 113);
            this.lblStatut.Name = "lblStatut";
            this.lblStatut.Size = new System.Drawing.Size(65, 13);
            this.lblStatut.TabIndex = 8;
            this.lblStatut.Text = "En attente...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Durée :";
            // 
            // lblDuree
            // 
            this.lblDuree.AutoSize = true;
            this.lblDuree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuree.Location = new System.Drawing.Point(84, 88);
            this.lblDuree.Name = "lblDuree";
            this.lblDuree.Size = new System.Drawing.Size(24, 13);
            this.lblDuree.TabIndex = 10;
            this.lblDuree.Text = "NA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Durée écoulée :";
            // 
            // lblDureeDepuisMiseAFeu
            // 
            this.lblDureeDepuisMiseAFeu.AutoSize = true;
            this.lblDureeDepuisMiseAFeu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDureeDepuisMiseAFeu.Location = new System.Drawing.Point(84, 100);
            this.lblDureeDepuisMiseAFeu.Name = "lblDureeDepuisMiseAFeu";
            this.lblDureeDepuisMiseAFeu.Size = new System.Drawing.Size(24, 13);
            this.lblDureeDepuisMiseAFeu.TabIndex = 12;
            this.lblDureeDepuisMiseAFeu.Text = "NA";
            // 
            // FireworkUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblDureeDepuisMiseAFeu);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblDuree);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStatut);
            this.Controls.Add(this.pbStatut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMiseAFeu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDesignation);
            this.Controls.Add(this.lblLigneNumber);
            this.Name = "FireworkUserControl";
            this.Size = new System.Drawing.Size(169, 133);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLigneNumber;
        private System.Windows.Forms.Label lblDesignation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMiseAFeu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbStatut;
        private System.Windows.Forms.Label lblStatut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDuree;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDureeDepuisMiseAFeu;
    }
}
