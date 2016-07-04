namespace Guiet.kQuatre.UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.feuxDartificeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nouveauToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chargerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDesign = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.cbxReceptor = new System.Windows.Forms.ComboBox();
            this.btnNewLigne = new System.Windows.Forms.Button();
            this.gvFireworkConception = new Telerik.WinControls.UI.RadGridView();
            this.tabLaunch = new System.Windows.Forms.TabPage();
            this.pnlFirework = new System.Windows.Forms.Panel();
            this.gvFireworkLaunch = new Telerik.WinControls.UI.RadGridView();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblProchainTir = new System.Windows.Forms.Label();
            this.gbxDureeFeu = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblDureeFeu = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFireNextFirework = new System.Windows.Forms.Button();
            this.btnReinit = new System.Windows.Forms.Button();
            this.lblTransmitterNotConnected = new System.Windows.Forms.Label();
            this.btnTestConnexion = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnFire = new System.Windows.Forms.Button();
            this.gbxDureeTotale = new System.Windows.Forms.GroupBox();
            this.lblDureeTotale = new System.Windows.Forms.Label();
            this.tabControlImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainMenu.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabDesign.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFireworkConception)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFireworkConception.MasterTemplate)).BeginInit();
            this.tabLaunch.SuspendLayout();
            this.pnlFirework.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFireworkLaunch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFireworkLaunch.MasterTemplate)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbxDureeFeu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbxDureeTotale.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.feuxDartificeToolStripMenuItem,
            this.toolStripMenuItem1});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(1477, 28);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // feuxDartificeToolStripMenuItem
            // 
            this.feuxDartificeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nouveauToolStripMenuItem,
            this.chargerToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitterToolStripMenuItem});
            this.feuxDartificeToolStripMenuItem.Name = "feuxDartificeToolStripMenuItem";
            this.feuxDartificeToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.feuxDartificeToolStripMenuItem.Text = "Feux d\'artifice";
            // 
            // nouveauToolStripMenuItem
            // 
            this.nouveauToolStripMenuItem.Name = "nouveauToolStripMenuItem";
            this.nouveauToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.nouveauToolStripMenuItem.Text = "Nouveau...";
            this.nouveauToolStripMenuItem.Click += new System.EventHandler(this.nouveauToolStripMenuItem_Click);
            // 
            // chargerToolStripMenuItem
            // 
            this.chargerToolStripMenuItem.Name = "chargerToolStripMenuItem";
            this.chargerToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.chargerToolStripMenuItem.Text = "Ouvrir...";
            this.chargerToolStripMenuItem.Click += new System.EventHandler(this.chargerToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(172, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.saveToolStripMenuItem.Text = "Enregistrer...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(172, 6);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(28, 24);
            this.toolStripMenuItem1.Text = "?";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDesign);
            this.tabControl.Controls.Add(this.tabLaunch);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.ImageList = this.tabControlImageList;
            this.tabControl.Location = new System.Drawing.Point(0, 28);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1477, 830);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabDesign
            // 
            this.tabDesign.AutoScroll = true;
            this.tabDesign.Controls.Add(this.tableLayoutPanel1);
            this.tabDesign.ImageIndex = 0;
            this.tabDesign.Location = new System.Drawing.Point(4, 26);
            this.tabDesign.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabDesign.Name = "tabDesign";
            this.tabDesign.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabDesign.Size = new System.Drawing.Size(1469, 800);
            this.tabDesign.TabIndex = 0;
            this.tabDesign.Text = "Concevoir";
            this.tabDesign.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlButtons, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gvFireworkConception, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1461, 792);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.button3);
            this.pnlButtons.Controls.Add(this.cbxReceptor);
            this.pnlButtons.Controls.Add(this.btnNewLigne);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Location = new System.Drawing.Point(4, 4);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1453, 37);
            this.pnlButtons.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(1179, 5);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(147, 28);
            this.button3.TabIndex = 6;
            this.button3.Text = "Ajouter un boitier";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // cbxReceptor
            // 
            this.cbxReceptor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxReceptor.FormattingEnabled = true;
            this.cbxReceptor.Location = new System.Drawing.Point(833, 5);
            this.cbxReceptor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxReceptor.Name = "cbxReceptor";
            this.cbxReceptor.Size = new System.Drawing.Size(336, 25);
            this.cbxReceptor.TabIndex = 5;
            // 
            // btnNewLigne
            // 
            this.btnNewLigne.Image = ((System.Drawing.Image)(resources.GetObject("btnNewLigne.Image")));
            this.btnNewLigne.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewLigne.Location = new System.Drawing.Point(4, 4);
            this.btnNewLigne.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNewLigne.Name = "btnNewLigne";
            this.btnNewLigne.Size = new System.Drawing.Size(129, 28);
            this.btnNewLigne.TabIndex = 2;
            this.btnNewLigne.Text = "Nouvelle ligne";
            this.btnNewLigne.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNewLigne.UseVisualStyleBackColor = true;
            this.btnNewLigne.Click += new System.EventHandler(this.btnNewLigne_Click);
            // 
            // gvFireworkConception
            // 
            this.gvFireworkConception.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.gvFireworkConception.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvFireworkConception.Location = new System.Drawing.Point(4, 49);
            this.gvFireworkConception.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gvFireworkConception.Name = "gvFireworkConception";
            // 
            // 
            // 
            this.gvFireworkConception.RootElement.AccessibleDescription = null;
            this.gvFireworkConception.RootElement.AccessibleName = null;
            this.gvFireworkConception.RootElement.ControlBounds = new System.Drawing.Rectangle(3, 39, 300, 187);
            this.gvFireworkConception.Size = new System.Drawing.Size(1453, 739);
            this.gvFireworkConception.TabIndex = 4;
            this.gvFireworkConception.CommandCellClick += new Telerik.WinControls.UI.CommandCellClickEventHandler(this.gvFireworkConception_CommandCellClick);
            // 
            // tabLaunch
            // 
            this.tabLaunch.Controls.Add(this.pnlFirework);
            this.tabLaunch.Controls.Add(this.pnlControl);
            this.tabLaunch.Location = new System.Drawing.Point(4, 26);
            this.tabLaunch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabLaunch.Name = "tabLaunch";
            this.tabLaunch.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabLaunch.Size = new System.Drawing.Size(1469, 800);
            this.tabLaunch.TabIndex = 1;
            this.tabLaunch.Text = "Lancer";
            this.tabLaunch.UseVisualStyleBackColor = true;
            // 
            // pnlFirework
            // 
            this.pnlFirework.Controls.Add(this.gvFireworkLaunch);
            this.pnlFirework.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFirework.Location = new System.Drawing.Point(4, 212);
            this.pnlFirework.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlFirework.Name = "pnlFirework";
            this.pnlFirework.Size = new System.Drawing.Size(1461, 584);
            this.pnlFirework.TabIndex = 3;
            // 
            // gvFireworkLaunch
            // 
            this.gvFireworkLaunch.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.gvFireworkLaunch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvFireworkLaunch.Location = new System.Drawing.Point(0, 0);
            this.gvFireworkLaunch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gvFireworkLaunch.Name = "gvFireworkLaunch";
            // 
            // 
            // 
            this.gvFireworkLaunch.RootElement.AccessibleDescription = null;
            this.gvFireworkLaunch.RootElement.AccessibleName = null;
            this.gvFireworkLaunch.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 300, 187);
            this.gvFireworkLaunch.Size = new System.Drawing.Size(1461, 584);
            this.gvFireworkLaunch.TabIndex = 5;
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.groupBox2);
            this.pnlControl.Controls.Add(this.gbxDureeFeu);
            this.pnlControl.Controls.Add(this.groupBox1);
            this.pnlControl.Controls.Add(this.gbxDureeTotale);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControl.Location = new System.Drawing.Point(4, 4);
            this.pnlControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(1461, 208);
            this.pnlControl.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblProchainTir);
            this.groupBox2.Location = new System.Drawing.Point(7, 145);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1445, 59);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PROCHAIN TIR";
            // 
            // lblProchainTir
            // 
            this.lblProchainTir.AutoSize = true;
            this.lblProchainTir.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProchainTir.Location = new System.Drawing.Point(8, 20);
            this.lblProchainTir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProchainTir.Name = "lblProchainTir";
            this.lblProchainTir.Size = new System.Drawing.Size(0, 29);
            this.lblProchainTir.TabIndex = 0;
            // 
            // gbxDureeFeu
            // 
            this.gbxDureeFeu.Controls.Add(this.button1);
            this.gbxDureeFeu.Controls.Add(this.lblDureeFeu);
            this.gbxDureeFeu.Location = new System.Drawing.Point(7, 4);
            this.gbxDureeFeu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxDureeFeu.Name = "gbxDureeFeu";
            this.gbxDureeFeu.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxDureeFeu.Size = new System.Drawing.Size(233, 133);
            this.gbxDureeFeu.TabIndex = 0;
            this.gbxDureeFeu.TabStop = false;
            this.gbxDureeFeu.Text = "Durée feu d\'artifice";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 23);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblDureeFeu
            // 
            this.lblDureeFeu.AutoSize = true;
            this.lblDureeFeu.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDureeFeu.Location = new System.Drawing.Point(9, 34);
            this.lblDureeFeu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDureeFeu.Name = "lblDureeFeu";
            this.lblDureeFeu.Size = new System.Drawing.Size(200, 76);
            this.lblDureeFeu.TabIndex = 0;
            this.lblDureeFeu.Text = "00:00";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFireNextFirework);
            this.groupBox1.Controls.Add(this.btnReinit);
            this.groupBox1.Controls.Add(this.lblTransmitterNotConnected);
            this.groupBox1.Controls.Add(this.btnTestConnexion);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnPause);
            this.groupBox1.Controls.Add(this.btnFire);
            this.groupBox1.Location = new System.Drawing.Point(491, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(961, 133);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Panneau de commandes";
            // 
            // btnFireNextFirework
            // 
            this.btnFireNextFirework.Location = new System.Drawing.Point(175, 23);
            this.btnFireNextFirework.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFireNextFirework.Name = "btnFireNextFirework";
            this.btnFireNextFirework.Size = new System.Drawing.Size(149, 78);
            this.btnFireNextFirework.TabIndex = 6;
            this.btnFireNextFirework.Text = "Lancement immédiat prochain artifice";
            this.btnFireNextFirework.UseVisualStyleBackColor = true;
            this.btnFireNextFirework.Click += new System.EventHandler(this.btnFireNextFirework_Click);
            // 
            // btnReinit
            // 
            this.btnReinit.Location = new System.Drawing.Point(647, 23);
            this.btnReinit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReinit.Name = "btnReinit";
            this.btnReinit.Size = new System.Drawing.Size(149, 78);
            this.btnReinit.TabIndex = 5;
            this.btnReinit.Text = "Réinitialiser le feu";
            this.btnReinit.UseVisualStyleBackColor = true;
            this.btnReinit.Click += new System.EventHandler(this.btnReinit_Click);
            // 
            // lblTransmitterNotConnected
            // 
            this.lblTransmitterNotConnected.BackColor = System.Drawing.Color.Red;
            this.lblTransmitterNotConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransmitterNotConnected.Location = new System.Drawing.Point(17, 105);
            this.lblTransmitterNotConnected.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTransmitterNotConnected.Name = "lblTransmitterNotConnected";
            this.lblTransmitterNotConnected.Size = new System.Drawing.Size(936, 25);
            this.lblTransmitterNotConnected.TabIndex = 4;
            this.lblTransmitterNotConnected.Text = "Impossible de tirer le feu car il n\'y a pas de transmetteur connecté";
            this.lblTransmitterNotConnected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTestConnexion
            // 
            this.btnTestConnexion.Location = new System.Drawing.Point(804, 23);
            this.btnTestConnexion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTestConnexion.Name = "btnTestConnexion";
            this.btnTestConnexion.Size = new System.Drawing.Size(149, 78);
            this.btnTestConnexion.TabIndex = 3;
            this.btnTestConnexion.Text = "Tests...";
            this.btnTestConnexion.UseVisualStyleBackColor = true;
            this.btnTestConnexion.Click += new System.EventHandler(this.btnTestConnexion_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(489, 23);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(149, 78);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Arrêt";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(332, 23);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(149, 78);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnFire
            // 
            this.btnFire.Location = new System.Drawing.Point(17, 23);
            this.btnFire.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFire.Name = "btnFire";
            this.btnFire.Size = new System.Drawing.Size(149, 78);
            this.btnFire.TabIndex = 0;
            this.btnFire.Text = "Tirer le feu !";
            this.btnFire.UseVisualStyleBackColor = true;
            this.btnFire.Click += new System.EventHandler(this.btnFire_Click);
            // 
            // gbxDureeTotale
            // 
            this.gbxDureeTotale.Controls.Add(this.lblDureeTotale);
            this.gbxDureeTotale.Location = new System.Drawing.Point(249, 4);
            this.gbxDureeTotale.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxDureeTotale.Name = "gbxDureeTotale";
            this.gbxDureeTotale.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxDureeTotale.Size = new System.Drawing.Size(233, 133);
            this.gbxDureeTotale.TabIndex = 1;
            this.gbxDureeTotale.TabStop = false;
            this.gbxDureeTotale.Text = "Durée totale écoulée";
            // 
            // lblDureeTotale
            // 
            this.lblDureeTotale.AutoSize = true;
            this.lblDureeTotale.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDureeTotale.Location = new System.Drawing.Point(9, 34);
            this.lblDureeTotale.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDureeTotale.Name = "lblDureeTotale";
            this.lblDureeTotale.Size = new System.Drawing.Size(200, 76);
            this.lblDureeTotale.TabIndex = 0;
            this.lblDureeTotale.Text = "00:00";
            // 
            // tabControlImageList
            // 
            this.tabControlImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabControlImageList.ImageStream")));
            this.tabControlImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.tabControlImageList.Images.SetKeyName(0, "form_blue.png");
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(99, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1477, 858);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "kQuatre";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabDesign.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvFireworkConception.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFireworkConception)).EndInit();
            this.tabLaunch.ResumeLayout(false);
            this.pnlFirework.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvFireworkLaunch.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFireworkLaunch)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbxDureeFeu.ResumeLayout(false);
            this.gbxDureeFeu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.gbxDureeTotale.ResumeLayout(false);
            this.gbxDureeTotale.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem feuxDartificeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nouveauToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chargerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDesign;
        private System.Windows.Forms.TabPage tabLaunch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ImageList tabControlImageList;
        private System.Windows.Forms.Button btnNewLigne;
        //private Telerik.WinControls.UI.MasterGridViewTemplate gvFireworkConception;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlButtons;
        private Telerik.WinControls.UI.RadGridView gvFireworkConception;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnFire;
        private System.Windows.Forms.GroupBox gbxDureeFeu;
        private System.Windows.Forms.Label lblDureeFeu;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox gbxDureeTotale;
        private System.Windows.Forms.Label lblDureeTotale;
        private System.Windows.Forms.Panel pnlFirework;
        private Telerik.WinControls.UI.RadGridView gvFireworkLaunch;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Button btnTestConnexion;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cbxReceptor;
        private System.Windows.Forms.Label lblTransmitterNotConnected;
        private System.Windows.Forms.Button btnReinit;
        private System.Windows.Forms.Button btnFireNextFirework;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblProchainTir;
        private System.Windows.Forms.Button button1;

    }
}

