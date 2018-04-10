#region Using Directives
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Win32;
#endregion

namespace GenClasse
{

    /// <summary>
    /// Description résumée de cClasseGenerator.
    /// </summary>
    public class ClasseGenerator
    {
        #region Private Members

        private static string _XSDFullPath = string.Empty;
        private string _currentPathFolder = string.Empty;
        private string _copieDe = string.Empty;
        private string _updatestringField = string.Empty;
        private string _updateDecimalField = string.Empty;
        private string _fileXSDWithoutExtension = string.Empty;
        private string _pathFicCS = string.Empty;
        private string _directoryFile = string.Empty;
        private string ligne, tmp, classname, enumname, strClasse, strClassName, strDALORBAL = string.Empty;
        private List<string> _lesEnumerations = new List<string>();
        private Dictionary<string, string> _DChampsstring = new Dictionary<string, string>();
        private Dictionary<string, string> _DChampsDecimal = new Dictionary<string, string>();
        private Dictionary<string, string> _DChampsClass = new Dictionary<string, string>();
        private Dictionary<string, string> _DChampsPpteClasse = new Dictionary<string, string>();
        private Dictionary<string, string> _DchampsDefaultValues = new Dictionary<string, string>();
        private bool _estEnumeration = false;
        private StreamReader _ficIn;
        private StreamWriter _ficOut = null;
        private int _acc, _index, _compteurClasse, _compteurEnum, _compteurJauge;

        #endregion

        #region Constantes

        private const string CRLF = "\r\n";

        public struct PathXSD
        {
            public const string XSDSingle = @"xsd.exe";
            public const string strCompileClass = "csc /target:library /out:c: class.cs";
        }

        public struct RegionClasses
        {
            public const string PrivateMemebers = "\r\n#region Private Members ------------\r\n";
            public const string PublicProperties = "\r\n#region Public Properties ----------\r\n";
            public const string PrivateMethod = "\r\n#region Private Method -------------\r\n";
            public const string EndRegion = "\r\n#endregion\r\n";
            public const string IsValid = "\r\n#region Public Method --------------\r\n" +
                "public bool IsValid ()\r\n" +
                "{\r\n" +
                "return true;\r\n" +
                " // Todo Validating\r\n" +
                "}\r\n" +
                "#endregion\r\n";
            public const string Comment = "/// <summary>\r\n/// \r\n/// </summary>";
        }

        #endregion

        #region Constructors

        public ClasseGenerator()
        {
            string sdkInstallRoot = "";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework"))
            {
                //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework[sdkInstallRoot]
                Version rtVersion = System.Environment.Version;
                string sdkInstallRootKey = "sdkInstallRoot";

                sdkInstallRootKey = rtVersion > new Version("1.1") ? "sdkInstallRootv2.0" : "sdkInstallRootv1.1";

                sdkInstallRoot = (string)key.GetValue(sdkInstallRootKey);
            }

            _XSDFullPath = "xsd.exe";

            if (!File.Exists(_XSDFullPath)) 
                throw new ApplicationException("Can't find xsd.exe: " + _XSDFullPath);
        }
       
        #endregion

        #region	Public properties

        public string FileXSDWithoutExtension
        {
            get { return _fileXSDWithoutExtension; }
            set { _fileXSDWithoutExtension = value; }
        }

        public string PathFicCS
        {
            get { return _pathFicCS; }
            set { _pathFicCS = value; }
        }

        public string DirectoryFile
        {
            get { return _directoryFile; }
            set { _directoryFile = value; }
        }
       
        #endregion

        #region Public Method

        /// <summary>
        /// Function launch XSD.exe Command
        /// </summary>
        /// <param name="pStrSerialisation"></param>
        /// <param name="langue"></param>
        /// <param name="cheminFichierXSD"></param>
        /// <param name="cheminFichierOUT"></param>
        /// <returns>true if success, else false</returns>
        public string LaunchCommandeXSD(string pStrSerialisation, string langue, string cheminFichierXSD, string cheminFichierOUT, string pStrImported)
        {
            string cheminFichierCS = string.Empty;

            cheminFichierXSD = cheminFichierXSD.Trim();
            string commandeXSD = "/" + pStrSerialisation + " /l:" + langue + " " + cheminFichierXSD.Replace("\\", "\\\\") + pStrImported.Replace("\\", "\\\\") + " /out:" + cheminFichierOUT.Replace("\\", "\\\\");
            
            ProcessStartInfo ProcessInfo = new ProcessStartInfo(_XSDFullPath, commandeXSD);
            ProcessInfo.CreateNoWindow = false;

            using (var xsdProcess = Process.Start(ProcessInfo))
            {
                xsdProcess.WaitForExit(10000);

                if (xsdProcess.HasExited && xsdProcess.ExitCode == 0)
                {
                    if (!string.IsNullOrEmpty(pStrImported))
                    {
                        _fileXSDWithoutExtension = Path.GetFileNameWithoutExtension(pStrImported);
                    }

                    _fileXSDWithoutExtension = _fileXSDWithoutExtension.Trim();
                    _fileXSDWithoutExtension = _fileXSDWithoutExtension.Replace(".", "_");
                    cheminFichierCS = _directoryFile + "\\" + _fileXSDWithoutExtension + "." + langue;
                }

                xsdProcess.Dispose();
                xsdProcess.Close();
            }

            return cheminFichierCS;
        }
        
        /// <summary>
        /// Function to disperse classes
        /// </summary>
        /// <param name="pStrNamespace"></param>
        /// <param name="cheminFichierCS"></param>
        public void RepartirClasses(string pStrNamespace, string cheminFichierCS, ProgressBar prog, ToolStripStatusLabel libelle)
        {
            string entete_Classe =
                "using System;\r\n" +
                "using System.Xml;\r\n" +
                "using System.Text;\r\n" +
                "using System.Collections.Generic;\r\n" +
                "using System.Xml.Serialization;\r\n";

            string ajouteDalOrBal = string.Empty;

            _acc = _index = _compteurClasse = _compteurEnum = _compteurJauge = prog.Value = prog.Minimum = 0;
            _lesEnumerations.Clear();
            _DChampsClass.Clear();
            _DChampsClass = FillDictionnaryClasses(cheminFichierCS);
            _DChampsPpteClasse.Clear();
            _ficIn = File.OpenText(cheminFichierCS);
            ligne = _ficIn.ReadLine();            

            while (ligne != null)
            {
                Application.DoEvents();
                if (ligne.Contains("{")) 
                    ligne = ligne.Replace(" {", "");

                libelle.Text = _copieDe;
                _compteurJauge = 0;
                _DChampsstring.Clear();
                _DChampsDecimal.Clear();

                #region ----"Rename classes"-----
                
                if (!ligne.Contains("public enum"))
                {
                    ligne = ligne.Trim();
                    foreach (string nomClassCourant in _DChampsClass.Keys)
                    {
                        if (ligne.Contains(nomClassCourant))
                        {
                            if (ligne.Contains("["))
                            {
                                ligne = ligne.Replace(nomClassCourant, _DChampsClass[nomClassCourant]);
                                break;
                            }

                            int i = 3;
                            if (ligne.Contains("abstract")) i = 4;
                            if (ligne.Contains(":") && ligne.Contains("class"))
                            {
                                string[] strTabClass = ligne.Split(':');
                                if (strTabClass.Length >= 1)
                                {
                                    strTabClass[1] = strTabClass[1].Replace(" ", "");
                                    if (_DChampsClass.ContainsKey(strTabClass[1]))
                                    {
                                        ligne = ligne.Replace(strTabClass[1], _DChampsClass[strTabClass[1]]);
                                    }
                                }
                                strTabClass = ligne.Split(' ');
                                if (_DChampsClass.ContainsKey(strTabClass[i]))
                                {
                                    ligne = ligne.Replace(strTabClass[i], _DChampsClass[strTabClass[i]]);
                                }
                                break;
                            }
                            else if (ligne.Contains("class"))
                            {
                                string[] strTabClass = ligne.Split(' ');
                                if (strTabClass.Length > 2)
                                {
                                    if (_DChampsClass.ContainsKey(strTabClass[i]))
                                    {
                                        ligne = ligne.Replace(strTabClass[i], _DChampsClass[strTabClass[i]]);
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                #endregion

                if (ligne.IndexOf("[") != -1)
                {
                    if (ligne.Contains("System.Diagnostics.DebuggerStepThroughAttribute") ||
                        ligne.Contains("System.ComponentModel.DesignerCategoryAttribute") ||
                        ligne.Contains("System.CodeDom.Compiler.GeneratedCodeAttribute")) ligne = string.Empty;
                    tmp += (!string.IsNullOrEmpty(ligne)) ? "\r\n" + ligne : string.Empty;
                }
                else
                {
                    _estEnumeration = false;
                    if (ligne.Contains("class") || (ligne.Contains("enum")))
                    {
                        ajouteDalOrBal = "using " + pStrNamespace + ".BAL" + ";\r\n\r\n" +
                            "namespace " + pStrNamespace + ".DAL" + "\r\n" + "{\r\n";

                        #region ----"Create file class"----
                        //Test si la classe est héritée                        
                        string strFilePathDAL = string.Format(@"{0}\GenClasse_{1}\DAL", DirectoryFile, FileXSDWithoutExtension);
                        if (ligne.Contains("enum"))
                        {
                            string strFilePathBAL = string.Format(@"{0}\GenClasse_{1}\BAL", DirectoryFile, FileXSDWithoutExtension);
                            enumname = ligne.Substring(ligne.IndexOf("enum") + 5);
                            _ficOut = File.CreateText(string.Format(@"{0}\{1}.cs", strFilePathBAL, enumname));
                            _compteurJauge++;
                            _copieDe = enumname;
                            _estEnumeration = true;
                            ajouteDalOrBal = "\r\n namespace " + pStrNamespace + ".BAL" + "\r\n" + "{\r\n";
                            //StrAddDalOrBal = string.Format("\r\n namespace\r{0}.BAL\r\r\n {\r\n", pStrNamespace);
                        }
                        else
                        {

                            if (ligne.Contains(":"))
                            {
                                classname = ligne.Substring(ligne.IndexOf("class") + 6, (ligne.IndexOf(':')) - (ligne.IndexOf("class") + 7));
                            }
                            else
                            {
                                if (ligne.Contains("class"))
                                {
                                    classname = ligne.Substring(ligne.IndexOf("class") + 6);
                                }
                            }
                            string str = string.Format("{0}\\{1}.cs", strFilePathDAL, classname);
                            _ficOut = File.CreateText(str);// string.Format(@"{0}\{1}.cs", strFilePathDAL, classname));
                            _compteurJauge++;
                            _copieDe = classname;
                        }

                        tmp += "\r\n" + ligne + "\r\n{";
                        tmp = entete_Classe + ajouteDalOrBal + tmp;
                        _ficOut.Write(tmp);
                        _acc++;
                        #endregion

                        #region ----"Copie des lignes"-----
                        int compeurMembersPrivate = 0;
                        int compteurPublicProperties = 0;
                        bool IsMembersPrivate = true;
                        bool IsPublicProperties = true;
                        bool bChanged = false;
                        bool bAccolade = false;

                        if (!_estEnumeration) 
                            _ficOut.WriteLine(RegionClasses.PrivateMemebers);

                        do
                        {
                            bChanged = false;
                            bAccolade = false;
                            ligne = _ficIn.ReadLine();
                            if (ligne != null) 
                                ligne = ligne.Trim();

                            if ((!string.IsNullOrEmpty(ligne)) && (!ligne.Contains("private")) && IsMembersPrivate && compeurMembersPrivate != 0)
                            {
                                if (!_estEnumeration)
                                {
                                    _ficOut.WriteLine(RegionClasses.EndRegion);
                                    IsMembersPrivate = false;
                                }
                            }

                            if (!string.IsNullOrEmpty(ligne))
                            {
                                if (ligne.Contains("private")) compeurMembersPrivate++;
                                if (ligne.IndexOf("{") != -1) _acc++;
                                if (ligne.IndexOf("}") != -1) _acc--;
                                string strtest = ligne;

                                #region ---"Lines contains string"-----
                                if (ligne.Contains("string") && (!ligne.Contains(",")))
                                {
                                    ligne = (ligne.Contains("@")) ? ligne.Replace("@", "@Str") : ligne;
                                    string[] strTabDeclaration = ligne.Split(' ');
                                    if (strTabDeclaration.Length > 2)
                                    {
                                        strTabDeclaration[2] = strTabDeclaration[2].Replace(";", "");
                                        _updatestringField = strTabDeclaration[2];
                                        _updatestringField = _updatestringField.Replace("Field", "");
                                        _updatestringField = _updatestringField[0].ToString().ToUpper() + _updatestringField.Substring(1, (_updatestringField.Length) - 1);
                                        string strPubOrPriv = "_";
                                        if (ligne.Contains("public string"))
                                        {
                                            compteurPublicProperties++;
                                            strPubOrPriv = "";
                                        }
                                        _updatestringField = strPubOrPriv + _updatestringField;
                                        strTabDeclaration[2] = strTabDeclaration[2].Replace(";", "");
                                        if (!_DChampsstring.ContainsKey(strTabDeclaration[2]))
                                        {
                                            _DChampsstring.Add(strTabDeclaration[2], _updatestringField);
                                        }
                                        strtest = strtest.Replace(";", "");
                                        foreach (string strInLine in strtest.Split(' '))
                                        {
                                            if (_DChampsstring.ContainsKey(strInLine))
                                            {
                                                ligne = ligne.Replace(strInLine, _DChampsstring[strInLine]);
                                                bChanged = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (!IsMembersPrivate && IsPublicProperties)
                                    {
                                        _ficOut.WriteLine(RegionClasses.PublicProperties);
                                        IsPublicProperties = false;
                                    }
                                    if (_DChampsstring.Count > 0)
                                    {
                                        // Update lines in Get
                                        strtest = ligne.Replace(";", "");
                                        foreach (string strInLin in strtest.Split('.'))
                                        {
                                            if (_DChampsstring.ContainsKey(strInLin))
                                            {
                                                ligne = ligne.Replace(strInLin, _DChampsstring[strInLin]);
                                                bChanged = true;
                                                break;
                                            }
                                        }
                                        // Update Set ...
                                        if (!bChanged)
                                        {
                                            strtest = strtest.Replace("this.", "");
                                            foreach (string strInLine in strtest.Split(' '))
                                            {
                                                if (_DChampsstring.ContainsKey(strInLine))
                                                {
                                                    ligne = ligne.Replace(strInLine, _DChampsstring[strInLine]);
                                                    bChanged = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region ---"Lines contains Decimal"----
                                if (ligne.Contains("decimal") && !bChanged)
                                {
                                    if (ligne.Contains("private decimal"))
                                    {
                                        string strDecimal = ligne.Substring((ligne.IndexOf("private decimal ") + 16), (ligne.IndexOf(';')) - (ligne.IndexOf("private decimal") + 16));
                                        _updateDecimalField = strDecimal;
                                        strDecimal = strDecimal.Replace("Field", "");
                                        strDecimal = strDecimal[0].ToString().ToUpper() + strDecimal.Substring(1, (strDecimal.Length) - 1);
                                        strDecimal = strDecimal.Replace(strDecimal, "_d" + strDecimal);
                                        _DChampsDecimal.Add(_updateDecimalField, strDecimal);
                                    }
                                    else if (ligne.Contains("public decimal"))
                                    {
                                        string strPublicDecimal = ligne.Substring((ligne.IndexOf("public decimal ") + 15), (ligne.IndexOf(" {")) - (ligne.IndexOf("public decimal") + 15));
                                        string strUpdatePublicDecimal = strPublicDecimal[0].ToString().ToUpper() + strPublicDecimal.Substring(1, (strPublicDecimal.Length) - 1);
                                        ligne = ligne.Replace(strPublicDecimal, "d" + strUpdatePublicDecimal);
                                    }
                                }
                                if (_DChampsDecimal.Count > 0)
                                {
                                    foreach (string strCurrentDecimal in _DChampsDecimal.Keys)
                                    {
                                        if ((!string.IsNullOrEmpty(strCurrentDecimal)) && ligne.Contains(strCurrentDecimal))
                                        {
                                            ligne = ligne.Replace(strCurrentDecimal, _DChampsDecimal[strCurrentDecimal]);
                                        }
                                    }
                                }
                                #endregion

                                #region ----"Lines contains Classe"----
                                // -----"Nommages des classes"------
                                if (!bChanged)
                                {
                                    for (int j = 0; j < ligne.Split(' ').Length; j++)
                                    {
                                        bool IsConstructor = false;
                                        string strLines = (ligne.Split(' '))[j];
                                        if (strLines.Contains("[]"))
                                        {
                                            strLines = strLines.Replace("[]", "");
                                            bAccolade = true;
                                        }
                                        else if (strLines.Contains("()"))
                                        {
                                            strLines = strLines.Replace("()", "");
                                            IsConstructor = true;

                                        }
                                        if (_DChampsClass.ContainsKey(strLines))
                                        {
                                            string[] strTab = ligne.Split(' ');
                                            if (strTab.Length >= 3)
                                            {
                                                // Ne pas renommer les enumération en cEnum...
                                                if (!_lesEnumerations.Contains(strLines))
                                                {
                                                    if (ligne.Contains("private") || ligne.Contains("public"))
                                                    {
                                                        if (!IsConstructor)
                                                        {
                                                            string strChampsClass = strTab[2];
                                                            strChampsClass = strChampsClass.Replace(";", "");
                                                            string strkey = strChampsClass;
                                                            strChampsClass = strChampsClass.Replace("Field", "");

                                                            string strPublicOrPrivatePrpties = string.Empty;
                                                            strPublicOrPrivatePrpties = (ligne.Contains("public")) ? strPublicOrPrivatePrpties = "" :
                                                                strPublicOrPrivatePrpties = "_";

                                                            strChampsClass = strChampsClass.Replace(strChampsClass, strPublicOrPrivatePrpties + strChampsClass[0].ToString().ToUpper() +
                                                                strChampsClass.Substring(1, (strChampsClass.Length) - 1));
                                                            if (!_DChampsPpteClasse.ContainsKey(strkey))
                                                            {
                                                                _DChampsPpteClasse.Add(strkey, strChampsClass);
                                                            }
                                                            ligne = strTab[0] + " " + _DChampsClass[strLines];
                                                            // For Constructors !!!
                                                            if (ligne.Contains("()"))
                                                            {
                                                                ligne = ligne + "() ";
                                                            }
                                                            if (bAccolade) ligne = ligne + "[] ";
                                                            strTab[2] = (!ligne.Contains("private")) ? strChampsClass : strChampsClass + ";";
                                                            for (int i = 2; i < strTab.Length; i++)
                                                            {
                                                                ligne += " " + strTab[i];
                                                            }
                                                            break;
                                                        }
                                                        else ligne = ligne.Replace(strLines, _DChampsClass[strLines]);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (_DChampsPpteClasse.Count > 0)
                                {
                                    // Update lines in Get
                                    strtest = ligne.Replace(";", "");
                                    foreach (string strInLin in strtest.Split('.'))
                                    {
                                        if (_DChampsPpteClasse.ContainsKey(strInLin))
                                        {
                                            ligne = ligne.Replace(strInLin, _DChampsPpteClasse[strInLin]);
                                            bChanged = true;
                                            break;
                                        }
                                    }
                                    // Update Set ...
                                    if (!bChanged)
                                    {
                                        strtest = strtest.Replace("this.", "");
                                        foreach (string strInLine in strtest.Split(' '))
                                        {
                                            if (_DChampsPpteClasse.ContainsKey(strInLine))
                                            {
                                                ligne = ligne.Replace(strInLine, _DChampsPpteClasse[strInLine]);
                                                break;
                                            }
                                        }
                                    }
                                }
                                #endregion

                                // TODO Comment foreach public propreties 
                                if (ligne.Contains("/// <remarks/>") && !bAccolade && !_estEnumeration)
                                {
                                    ligne = string.Format("{0}", RegionClasses.Comment);
                                }

                                ligne = ligne.Replace("/// <remarks/>", string.Empty);

                                // For Tag contains : [System.xmlAttribute... (typeof(clientpmType)) ...  
                                if (ligne.Contains("[") && !bAccolade)
                                {
                                    foreach (string strLinesClassName in _DChampsClass.Keys)
                                    {
                                        if (ligne.Contains(strLinesClassName))
                                        {
                                            ligne = (ligne.Contains(strLinesClassName)) ? ligne.Replace(strLinesClassName, _DChampsClass[strLinesClassName]) : ligne;
                                            break;
                                        }
                                    }
                                }
                                //  -----------------    //
                                #region ----"Default Lines"----

                                if (!bChanged && ligne.Contains("Field"))
                                {
                                    // Members private
                                    if (!ligne.Contains(","))
                                    {
                                        if (ligne.Contains("private"))
                                        {
                                            ligne = ligne.Replace("  ", " ");
                                            string[] strDefaultValues = ligne.Split(' ');
                                            if (strDefaultValues.Length > 2)
                                            {
                                                string strArrobase = string.Empty;
                                                string strKey = strDefaultValues[2].Replace(";", "");
                                                string strValues = strKey;
                                                if (!strDefaultValues[1].Contains("Field"))
                                                    strValues = strKey.Replace("Field", "");
                                                strArrobase = (ligne.Contains("@")) ? "@_" : "_";
                                                strValues = strArrobase + strValues[0].ToString().ToUpper() + strValues.Substring(1, (strValues.Length) - 1);
                                                if (!_DchampsDefaultValues.ContainsKey(strKey))
                                                    _DchampsDefaultValues.Add(strKey, strValues);
                                                ligne = ligne.Replace(strKey, _DchampsDefaultValues[strKey]);
                                            }
                                        }
                                    }
                                    else bChanged = false;
                                }
                                if (!bChanged)
                                {
                                    // "Get" for default Values
                                    string strDefLigTemp = ligne.Replace(";", "");
                                    foreach (string strGet in strDefLigTemp.Split('.'))
                                    {
                                        if (_DchampsDefaultValues.ContainsKey(strGet))
                                        {
                                            ligne = ligne.Replace(strGet, _DchampsDefaultValues[strGet]);
                                            bChanged = true;
                                            break;
                                        }
                                    }
                                    // Update "Set" ...for default Values
                                    if (!bChanged)
                                    {
                                        strDefLigTemp = ligne.Replace("this.", "");
                                        foreach (string strInLine in strDefLigTemp.Split(' '))
                                        {
                                            if (_DchampsDefaultValues.ContainsKey(strInLine))
                                            {
                                                ligne = ligne.Replace(strInLine, _DchampsDefaultValues[strInLine]);
                                                break;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                if (_acc == 0)
                                {
                                    if (!_estEnumeration)
                                    {
                                        _ficOut.WriteLine(RegionClasses.EndRegion);
                                        _ficOut.Write(RegionClasses.IsValid);
                                    }
                                }
                                _ficOut.WriteLine(ligne);

                                _compteurJauge++;
                                // ------"Début Jauge"-------
                                prog.Maximum = _compteurJauge + 1;
                                prog.Value = _compteurJauge;
                                // ------"Fin Jauge"-------
                            }
                        }
                        while (ligne != null && _acc != 0);
                        if (ligne != null && _acc == 0)
                        {
                            _ficOut.Write("}");
                            _ficOut.Close();
                            tmp = string.Empty;
                        }
                        #endregion
                    }
                    ligne = _ficIn.ReadLine();
                    if (ligne == null)
                    {
                        prog.Minimum = 0;
                        prog.Maximum = 0;
                        prog.Value = 0;
                    }
                }
                ligne = _ficIn.ReadLine();
            }
            _ficIn.Close();
        }

        /// <summary>
        /// Fill Dictionnary _DChampsClass with file text
        /// </summary>
        /// <param name="cheminFichierCS"></param>
        /// <returns></returns>
        private Dictionary<string, string> FillDictionnaryClasses(string cheminFichierCS)
        {
            _ficIn = File.OpenText(cheminFichierCS);
            ligne = _ficIn.ReadLine();
            while (ligne != null)
            {
                ligne = ligne.Replace(" {", "");
                if (ligne.Contains("public enum"))
                {
                    enumname = ligne.Substring(ligne.IndexOf("enum") + 5);
                    _lesEnumerations.Add(enumname);
                }
                else if (ligne.Contains("class") && ligne.Contains("public"))
                {
                    //Test si la classe est héritée
                    if (ligne.Contains(":"))
                    {
                        classname = ligne.Substring(ligne.IndexOf("class") + 6, (ligne.IndexOf(':')) - (ligne.IndexOf("class") + 7));
                        strClasse = classname[0].ToString().ToUpper();
                        strClassName = strClasse + classname.Substring(1, (classname.Length - 1));
                        _DChampsClass.Add(classname, strClassName);
                    }
                    else
                    {
                        classname = ligne.Substring(ligne.IndexOf("class") + 6);
                        strClasse = classname[0].ToString().ToUpper();
                        strClassName = strClasse + classname.Substring(1, (classname.Length - 1));
                        _DChampsClass.Add(classname, strClassName);
                    }
                }

                ligne = _ficIn.ReadLine();
            }

            _ficIn.Close();

            return _DChampsClass;
        }

        /// <summary>
        /// Get Language CS or Vb
        /// </summary>
        /// <param name="pBolLangCSChecked"></param>
        /// <returns></returns>
        public string GetLanguage(bool pBolLangCSChecked)
        {
            return pBolLangCSChecked ? "cs" : "vb";
        }
       
        /// <summary>
        /// Get serialisation classes or dataset
        /// </summary>
        /// <param name="pBolSerClasses"></param>
        /// <returns></returns>
        public string GetSerialisation(bool pBolSerClasses)
        {
            return pBolSerClasses ? "classes" : "dataset";
        }
       
        #endregion
    }

}
