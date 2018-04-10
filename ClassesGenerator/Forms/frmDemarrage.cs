using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Genapi.Teleacte.Controls.Forms;

namespace GenClasse
{
    public partial class frmDemarrage : CommonForm
    {
        #region Membres

        private ClasseGenerator _generator = null;
        private string _cheminDossier = string.Empty;

        #endregion

        #region Constructors

        public frmDemarrage()
        {
            InitializeComponent();

            _generator = new ClasseGenerator();
        }

        #endregion

        #region Events

        private void frmDemarrage_Load(object sender, EventArgs e)
        {
            //this.Height = 390;
            lblCopieDe.Text = string.Empty;
            classnameLabel.Text = string.Empty;
        }

        private void frmDemarrage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void btnParcourir_Click(object sender, EventArgs e)
        {
            opFiledg.Title = "Séléctionner un schéma XSD Valide";
            opFiledg.Filter = "*.xsd (schéma XSD)|*.xsd|*.xml (Fichiers XML) | *.xml";
            if (opFiledg.ShowDialog() == DialogResult.OK)
            {
                svFiledg.FileName = opFiledg.FileName;
                txtCheminXSD.Text = opFiledg.FileName;
                //string strExtensionFile = Path.GetExtension(opFiledg.FileName);
                _generator.DirectoryFile = Path.GetDirectoryName(opFiledg.FileName);
                _generator.FileXSDWithoutExtension = Path.GetFileNameWithoutExtension(opFiledg.FileName);
                btnGeneration.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            opFiledg.Title = "Séléctionner un schéma XSD Valide";
            opFiledg.Filter = "*.xsd (schéma XSD)|*.xsd|*.xml (Fichiers XML) | *.xml";
            opFiledg.Multiselect = true;
            if (opFiledg.ShowDialog() == DialogResult.OK)
            {
                //svFiledg.FileName = opFiledg.FileName;
                txtImport.Text = string.Join(",", opFiledg.FileNames);
                //string strExtensionFile = Path.GetExtension(opFiledg.FileName);
                //_generator.DirectoryFile = Path.GetDirectoryName(opFiledg.FileName);
                //_generator.FileXSDWithoutExtension = Path.GetFileNameWithoutExtension(opFiledg.FileName);
                //btnGeneration.Enabled = true;
            }
        }

        private void btnGeneration_Click(object sender, EventArgs e)
        {
            if (txtCheminXSD.Text != null)
            {
                try
                {
                    string imported = string.Empty;
                    if (!string.IsNullOrEmpty(txtImport.Text))
                    {
                        string[] lesURI = txtImport.Text.Split(',');
                        foreach (string strValue in lesURI)
                        {
                            imported += string.Format(" /URI: {0}", strValue);
                        }
                    }

                    _generator.PathFicCS = _generator.LaunchCommandeXSD(_generator.GetSerialisation(rdbClasses.Checked), _generator.GetLanguage(rdbCSharp.Checked), txtCheminXSD.Text, _generator.DirectoryFile, imported);
                    btnRepartition.Enabled = true;
                    statusStrip1.Text = "Génération réussie." + _generator.PathFicCS;

                    if (!string.IsNullOrEmpty(_generator.PathFicCS))
                    {
                        btnGeneration.Enabled = false;
                        statusStrip1.Visible = true;
                        lblCopieDe.Text = "Génération réussie: " + _generator.PathFicCS.Replace("\\\\", "\\");

                        //string repertoireSortie = !string.IsNullOrEmpty(txtNamespace.Text) ? txtNamespace.Text : _generator.FileXSDWithoutExtension;
                        Directory.CreateDirectory(string.Format(@"{0}\GenClasse_{1}\DAL", _generator.DirectoryFile, _generator.FileXSDWithoutExtension));
                        Directory.CreateDirectory(string.Format(@"{0}\GenClasse_{1}\BAL", _generator.DirectoryFile, _generator.FileXSDWithoutExtension));
                    }
                    else
                    {
                        lblCopieDe.Text = "La génération a échoué !";
                        MessageBox.Show(string.Format("Schéma non valide ! \nUne erreur s'est produite pendant la génération du fichier."), "Erreur Génération", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Erreur Innatendue !\n{0} ", ex.Message), "Erreur de génération", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRepartition_Click(object sender, EventArgs e)
        {
            btnRepartition.Enabled = false;
            errorNamespace.Clear();

            if (File.Exists(_generator.PathFicCS))
            {
                if (!string.IsNullOrEmpty(txtNamespace.Text))
                {
                    try
                    {
                        _cheminDossier = string.Format(@"{0}\GenClasse_{1}", _generator.DirectoryFile, _generator.FileXSDWithoutExtension);
                        LaunchDistribution();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Une erreur est survenue lors de la répartition des classes,\nSource : {0}\n{1}", ex.Message, ex.StackTrace), "Erreur : Fichier endommagé", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    errorNamespace.SetError(txtNamespace, "Spécifier un espace de nom pour les classes.");
                }
            }
            else
            {
                MessageBox.Show("Le fichier est introuvable.", "Erreur Format Fichier", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            btnRepartition.Enabled = true;
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCheminXSD_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txtCheminXSD.Text))
            {
                //string strExtensionFile = Path.GetExtension(txtCheminXSD.Text);
                _generator.DirectoryFile = Path.GetDirectoryName(txtCheminXSD.Text);
                _generator.FileXSDWithoutExtension = Path.GetFileNameWithoutExtension(txtCheminXSD.Text);
                btnGeneration.Enabled = true;
            }
        }

        private void txtCheminXSD_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void txtCheminXSD_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                txtCheminXSD.Text = string.Empty;
                string strFilePath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                txtCheminXSD.Text = strFilePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Erreur {0}\n{1}", ex.Message, ex.InnerException));
            }
        }

        private void txtImport_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                txtImport.Text = string.Empty;
                string[] cheminFichier = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (cheminFichier.Length > 1)
                {
                    foreach (string strValue in cheminFichier)
                    {
                        if (!string.IsNullOrEmpty(txtImport.Text))
                        {
                            txtImport.Text += string.Format(",{0}", strValue);
                        }
                        else
                        {
                            txtImport.Text += string.Format("{0}", strValue);
                        }
                    }
                }
                else
                {
                    txtImport.Text = cheminFichier[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Erreur {0}\n{1}", ex.Message, ex.InnerException));
            }
        }

        private void txtImport_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        #endregion

        #region Méthodes

        private void LaunchDistribution()
        {
            //this.Height = 416;
            lblCopieDe.Text = string.Format("Copie des classes ...");
            _generator.RepartirClasses(txtNamespace.Text, _generator.PathFicCS, prog, classnameLabel);
            _generator.PathFicCS = _generator.PathFicCS.Replace("\\\\", "\\");
            lblCopieDe.Text = string.Format("Classes générées : {0}\\GenClasse", _generator.PathFicCS);
            classnameLabel.Text = string.Empty;
            //this.Height = 390;

            try
            {
                File.Copy(txtCheminXSD.Text, string.Format(@"{0}\{1}", _cheminDossier, Path.GetFileName(txtCheminXSD.Text)), true);

                string fichierAbouger = string.Format(@"{0}\{1}", _cheminDossier, Path.GetFileName(_generator.PathFicCS));
                if (File.Exists(fichierAbouger))
                    File.Delete(fichierAbouger);

                File.Move(_generator.PathFicCS, string.Format(@"{0}\{1}", _cheminDossier, Path.GetFileName(_generator.PathFicCS)));
                Process.Start(_cheminDossier);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        #endregion

        private void txtNamespace_TextChanged(object sender, EventArgs e)
        {

        }

    }
}