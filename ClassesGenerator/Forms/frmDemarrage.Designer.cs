namespace GenClasse
{
    partial class frmDemarrage
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
            if ( disposing && ( components != null ) )
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDemarrage));
            this.grpSelectFichier = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblImport = new System.Windows.Forms.Label();
            this.txtImport = new System.Windows.Forms.TextBox();
            this.txtCheminXSD = new System.Windows.Forms.TextBox();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.btnParcourir = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbDataSet = new System.Windows.Forms.RadioButton();
            this.rdbClasses = new System.Windows.Forms.RadioButton();
            this.grpbLanaguage = new System.Windows.Forms.GroupBox();
            this.rdbCSharp = new System.Windows.Forms.RadioButton();
            this.rdbVB = new System.Windows.Forms.RadioButton();
            this.grpbGeneration = new System.Windows.Forms.GroupBox();
            this.btnRepartition = new System.Windows.Forms.Button();
            this.btnGeneration = new System.Windows.Forms.Button();
            this.prog = new System.Windows.Forms.ProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCopieDe = new System.Windows.Forms.ToolStripStatusLabel();
            this.classnameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.opFiledg = new System.Windows.Forms.OpenFileDialog();
            this.svFiledg = new System.Windows.Forms.SaveFileDialog();
            this.folderDossier = new System.Windows.Forms.FolderBrowserDialog();
            this.errorNamespace = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpSelectFichier.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpbLanaguage.SuspendLayout();
            this.grpbGeneration.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorNamespace)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grpSelectFichier);
            this.panel1.Controls.Add(this.grpbLanaguage);
            this.panel1.Controls.Add(this.prog);
            this.panel1.Controls.Add(this.grpbGeneration);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Size = new System.Drawing.Size(420, 262);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(389, 336);
            this.btCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btCancel.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // label1
            // 
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Size = new System.Drawing.Size(223, 24);
            this.label1.Text = "Générateur de classes";
            // 
            // panel2
            // 
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Size = new System.Drawing.Size(420, 51);
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(12, 336);
            this.btOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btOk.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // grpSelectFichier
            // 
            this.grpSelectFichier.BackColor = System.Drawing.Color.Transparent;
            this.grpSelectFichier.Controls.Add(this.button1);
            this.grpSelectFichier.Controls.Add(this.lblImport);
            this.grpSelectFichier.Controls.Add(this.txtImport);
            this.grpSelectFichier.Controls.Add(this.txtCheminXSD);
            this.grpSelectFichier.Controls.Add(this.lblNamespace);
            this.grpSelectFichier.Controls.Add(this.btnParcourir);
            this.grpSelectFichier.Controls.Add(this.lblDescription);
            this.grpSelectFichier.Controls.Add(this.txtNamespace);
            this.grpSelectFichier.Location = new System.Drawing.Point(9, 3);
            this.grpSelectFichier.Name = "grpSelectFichier";
            this.grpSelectFichier.Size = new System.Drawing.Size(400, 117);
            this.grpSelectFichier.TabIndex = 0;
            this.grpSelectFichier.TabStop = false;
            this.grpSelectFichier.Text = "Schéma xsd";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button1.Image = global::GenClasse.Properties.Resources.Liste;
            this.button1.Location = new System.Drawing.Point(366, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 23);
            this.button1.TabIndex = 7;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblImport
            // 
            this.lblImport.Location = new System.Drawing.Point(9, 43);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(303, 15);
            this.lblImport.TabIndex = 5;
            this.lblImport.Text = "Imports nécessaires au XSD (séparés par des virgules)\r\n";
            // 
            // txtImport
            // 
            this.txtImport.AllowDrop = true;
            this.txtImport.Location = new System.Drawing.Point(8, 62);
            this.txtImport.Name = "txtImport";
            this.txtImport.Size = new System.Drawing.Size(352, 20);
            this.txtImport.TabIndex = 6;
            this.txtImport.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtImport_DragDrop);
            this.txtImport.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtImport_DragEnter);
            // 
            // txtCheminXSD
            // 
            this.txtCheminXSD.AllowDrop = true;
            this.txtCheminXSD.Location = new System.Drawing.Point(60, 16);
            this.txtCheminXSD.Name = "txtCheminXSD";
            this.txtCheminXSD.Size = new System.Drawing.Size(300, 20);
            this.txtCheminXSD.TabIndex = 1;
            this.txtCheminXSD.TextChanged += new System.EventHandler(this.txtCheminXSD_TextChanged);
            this.txtCheminXSD.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtCheminXSD_DragDrop);
            this.txtCheminXSD.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtCheminXSD_DragEnter);
            // 
            // lblNamespace
            // 
            this.lblNamespace.Location = new System.Drawing.Point(6, 89);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(67, 20);
            this.lblNamespace.TabIndex = 3;
            this.lblNamespace.Text = "Namespace";
            // 
            // btnParcourir
            // 
            this.btnParcourir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnParcourir.Image = global::GenClasse.Properties.Resources.Liste;
            this.btnParcourir.Location = new System.Drawing.Point(366, 14);
            this.btnParcourir.Name = "btnParcourir";
            this.btnParcourir.Size = new System.Drawing.Size(29, 23);
            this.btnParcourir.TabIndex = 2;
            this.btnParcourir.Click += new System.EventHandler(this.btnParcourir_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(9, 19);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(68, 16);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "XSD File";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(76, 86);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(284, 20);
            this.txtNamespace.TabIndex = 4;
            this.txtNamespace.TextChanged += new System.EventHandler(this.txtNamespace_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.rdbDataSet);
            this.groupBox2.Controls.Add(this.rdbClasses);
            this.groupBox2.Location = new System.Drawing.Point(9, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 48);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sérialisation";
            // 
            // rdbDataSet
            // 
            this.rdbDataSet.Location = new System.Drawing.Point(96, 16);
            this.rdbDataSet.Name = "rdbDataSet";
            this.rdbDataSet.Size = new System.Drawing.Size(64, 24);
            this.rdbDataSet.TabIndex = 1;
            this.rdbDataSet.Text = "DataSet";
            // 
            // rdbClasses
            // 
            this.rdbClasses.Checked = true;
            this.rdbClasses.Location = new System.Drawing.Point(16, 16);
            this.rdbClasses.Name = "rdbClasses";
            this.rdbClasses.Size = new System.Drawing.Size(72, 24);
            this.rdbClasses.TabIndex = 0;
            this.rdbClasses.TabStop = true;
            this.rdbClasses.Text = "Classes";
            // 
            // grpbLanaguage
            // 
            this.grpbLanaguage.BackColor = System.Drawing.Color.Transparent;
            this.grpbLanaguage.Controls.Add(this.rdbCSharp);
            this.grpbLanaguage.Controls.Add(this.rdbVB);
            this.grpbLanaguage.Location = new System.Drawing.Point(220, 126);
            this.grpbLanaguage.Name = "grpbLanaguage";
            this.grpbLanaguage.Size = new System.Drawing.Size(188, 48);
            this.grpbLanaguage.TabIndex = 2;
            this.grpbLanaguage.TabStop = false;
            this.grpbLanaguage.Text = "Language";
            // 
            // rdbCSharp
            // 
            this.rdbCSharp.Checked = true;
            this.rdbCSharp.Location = new System.Drawing.Point(28, 16);
            this.rdbCSharp.Name = "rdbCSharp";
            this.rdbCSharp.Size = new System.Drawing.Size(56, 24);
            this.rdbCSharp.TabIndex = 0;
            this.rdbCSharp.TabStop = true;
            this.rdbCSharp.Text = "C#";
            // 
            // rdbVB
            // 
            this.rdbVB.Location = new System.Drawing.Point(103, 16);
            this.rdbVB.Name = "rdbVB";
            this.rdbVB.Size = new System.Drawing.Size(48, 24);
            this.rdbVB.TabIndex = 1;
            this.rdbVB.Text = "VB";
            // 
            // grpbGeneration
            // 
            this.grpbGeneration.BackColor = System.Drawing.Color.Transparent;
            this.grpbGeneration.Controls.Add(this.btnRepartition);
            this.grpbGeneration.Controls.Add(this.btnGeneration);
            this.grpbGeneration.Location = new System.Drawing.Point(9, 180);
            this.grpbGeneration.Name = "grpbGeneration";
            this.grpbGeneration.Size = new System.Drawing.Size(400, 48);
            this.grpbGeneration.TabIndex = 3;
            this.grpbGeneration.TabStop = false;
            this.grpbGeneration.Text = "Génération et Partage";
            // 
            // btnRepartition
            // 
            this.btnRepartition.Enabled = false;
            this.btnRepartition.Location = new System.Drawing.Point(208, 16);
            this.btnRepartition.Name = "btnRepartition";
            this.btnRepartition.Size = new System.Drawing.Size(187, 23);
            this.btnRepartition.TabIndex = 1;
            this.btnRepartition.Text = "&Eclater";
            this.btnRepartition.Click += new System.EventHandler(this.btnRepartition_Click);
            // 
            // btnGeneration
            // 
            this.btnGeneration.Enabled = false;
            this.btnGeneration.Location = new System.Drawing.Point(16, 16);
            this.btnGeneration.Name = "btnGeneration";
            this.btnGeneration.Size = new System.Drawing.Size(184, 23);
            this.btnGeneration.TabIndex = 0;
            this.btnGeneration.Text = "&Générer";
            this.btnGeneration.Click += new System.EventHandler(this.btnGeneration_Click);
            // 
            // prog
            // 
            this.prog.Cursor = System.Windows.Forms.Cursors.Default;
            this.prog.Location = new System.Drawing.Point(9, 236);
            this.prog.Name = "prog";
            this.prog.Size = new System.Drawing.Size(400, 16);
            this.prog.Step = 1;
            this.prog.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCopieDe,
            this.classnameLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 379);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(444, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            // 
            // lblCopieDe
            // 
            this.lblCopieDe.Name = "lblCopieDe";
            this.lblCopieDe.Size = new System.Drawing.Size(66, 17);
            this.lblCopieDe.Text = "Copie de ...";
            // 
            // classnameLabel
            // 
            this.classnameLabel.Name = "classnameLabel";
            this.classnameLabel.Size = new System.Drawing.Size(79, 17);
            this.classnameLabel.Text = "List Copies ....";
            // 
            // opFiledg
            // 
            this.opFiledg.Filter = "Schéma XSD |*.xsd";
            // 
            // svFiledg
            // 
            this.svFiledg.Filter = "Schéma XSD |*.xsd";
            // 
            // errorNamespace
            // 
            this.errorNamespace.ContainerControl = this;
            // 
            // frmDemarrage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(444, 401);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "frmDemarrage";
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.Text = "GenApi : GenClasse";
            this.Load += new System.EventHandler(this.frmDemarrage_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmDemarrage_KeyUp);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btCancel, 0);
            this.Controls.SetChildIndex(this.btOk, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpSelectFichier.ResumeLayout(false);
            this.grpSelectFichier.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.grpbLanaguage.ResumeLayout(false);
            this.grpbGeneration.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorNamespace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSelectFichier;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.TextBox txtCheminXSD;
        private System.Windows.Forms.Button btnParcourir;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbDataSet;
        private System.Windows.Forms.RadioButton rdbClasses;
        private System.Windows.Forms.GroupBox grpbLanaguage;
        private System.Windows.Forms.RadioButton rdbCSharp;
        private System.Windows.Forms.RadioButton rdbVB;
        private System.Windows.Forms.GroupBox grpbGeneration;
        private System.Windows.Forms.Button btnRepartition;
        private System.Windows.Forms.Button btnGeneration;
        private System.Windows.Forms.ProgressBar prog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblCopieDe;
        private System.Windows.Forms.OpenFileDialog opFiledg;
        private System.Windows.Forms.SaveFileDialog svFiledg;
        private System.Windows.Forms.FolderBrowserDialog folderDossier;
        private System.Windows.Forms.ToolStripStatusLabel classnameLabel;
        private System.Windows.Forms.ErrorProvider errorNamespace;
        private System.Windows.Forms.Label lblImport;
        private System.Windows.Forms.TextBox txtImport;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
    }
}

