using System;
using System.Collections.Generic;
using System.Xml;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.IO;


namespace Practicals_2014.Pages
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : System.Windows.Controls.UserControl
    {      
        public List<ResultData> resultDB = new List<ResultData>();

        public WelcomePage()
        {            
            InitializeComponent();
            //using (StreamReader sr = new StreamReader(FirstFile))
            //{
            //    FirstFileTxt = sr.ReadToEnd();
            //}

            //using (StreamReader sr = new StreamReader(SecondFile))
            //{
            //    SecondFileTxt = sr.ReadToEnd();
            //}
            //TrimFiles(@"C:\Users\Bilal\Desktop\Claims2\RU2012149441_EN.txt",
            //    @"C:\Users\Bilal\Desktop\Claims2\RU2012149441_RU.txt");

            //using (StreamWriter sr = new StreamWriter(FirstFile + "xxx"))
            //{
            //    sr.Write(FirstFileTxt);
            //}

            //using (StreamWriter sr = new StreamWriter(SecondFile + "xxx"))
            //{
            //    sr.Write(SecondFileTxt);
            //}
        }

        public string GetEnglishRussianClaimsFromDatabase(string filename, string fSavePath)
        {
            string chkTxtReg = @"^[a-zA-ZА-Яа-я0-9\.\s,\-\n\e\\:;\(\)&\?\]\[\}\{/]+$";
            string EnglishExstract;
            string RussianExstract;
            string PN;   
         
            XmlReaderSettings settings = new XmlReaderSettings();
            XmlDocument doc = new XmlDocument();
 
            settings.XmlResolver = null;
            settings.DtdProcessing = DtdProcessing.Ignore;
           
            XmlReader reader = XmlTextReader.Create(filename, settings);           
            doc.Load(reader);

            XmlNodeList item = doc.GetElementsByTagName("ru-patent-document");
            XmlAttribute attr = item.Item(0).Attributes["doc-number"];

            PN = attr.Value;
            PN = "RU" + PN.Substring(1);
            
            item = doc.GetElementsByTagName("abstract");

            RussianExstract = item.Item(0).InnerText;
            EnglishExstract = item.Item(1).InnerText;

            if (Regex.IsMatch(EnglishExstract, chkTxtReg))
            {
                saveToFile(EnglishExstract, fSavePath + PN + "_EN.txt");
                saveToFile(RussianExstract, fSavePath + PN + "_RU.txt");
            }
            else
            {
                return "-1";
            }

            return PN;          
        }
        

        ~WelcomePage()
        {
           
        }
        
        public string GetEnglishRussianClaimsFromWeb(string docNum, string pSavePath)
        {
            string RussianUrl = string.Format("http://patentscope.wipo.int/search/en/detail.jsf?docId={0}", docNum) +
                "&recNum=1&tab=PCTClaims&maxRec=1&office=&prevFilter=&sortOption=Pub+Date+Desc&queryString=";
            string chkTxtReg = @"^[a-zA-ZА-Яа-я0-9\.\s,\-\n\e\\:;\(\)&\?\]\[\}\{/]+$";
            string EnglishUrl = "";
            string RussianClaims = "";
            string EnglishClaims = "";
            string PublNum = "";
            string publN = "";

            try
            {     
                //get russian claims
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                int tryCount = 0;
                do
                {
                    htmlDocument = htmlWeb.Load(RussianUrl);
                    System.Threading.Thread.Sleep(1000);
                    tryCount++;
                }
                while (htmlDocument == null && tryCount < 5);
               
                string pubNumStr = htmlDocument.GetElementbyId("resultPanel1").InnerText;
                PublNum = Regex.Match(pubNumStr, @"RU\d+").Value;
                HtmlNodeCollection AllClaimTxt = htmlDocument.DocumentNode.SelectNodes("//*[@class=\"claimp\"]");
                foreach (HtmlNode node in AllClaimTxt)
                {
                    RussianClaims += Environment.NewLine + node.InnerText;
                }

                //get english claims
                publN = PublNum.Substring(2);
                EnglishUrl = string.Format("http://worldwide.espacenet.com/publicationDetails/claims?CC=RU&NR={0}", publN) +
                    "A&KC=A&FT=D&ND=3&date=20140527&DB=EPODOC&locale=en_EP";
                htmlDocument = htmlWeb.Load(EnglishUrl);
                System.Threading.Thread.Sleep(1000);

                AllClaimTxt = htmlDocument.DocumentNode.SelectNodes("//*[@class=\"application article clearfix printTableText\"]");
                
                EnglishClaims = htmlDocument.GetElementbyId("claims").InnerText.TrimStart('\n');
                EnglishClaims = EnglishClaims.Substring(Regex.Match(EnglishClaims, @"\w+").Index);
                EnglishClaims = EnglishClaims.Replace("1. What is claimed is:", "")
                    .Replace("What is claimed is:", "").Replace("CLAIMS", "");                               
                               
                //check that claims are made up of only words
                if (Regex.IsMatch(EnglishClaims, chkTxtReg) && Regex.IsMatch(EnglishClaims, @"\w+")
                    && Regex.IsMatch(RussianClaims, @"\w+") && TrimFiles(ref EnglishClaims, ref RussianClaims))
                {
                    saveToFile(EnglishClaims, pSavePath + PublNum + "_EN.txt");
                    saveToFile(RussianClaims, pSavePath + PublNum + "_RU.txt");
                }
                else
                {
                    return "-1";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "-1";
            }
           
            return PublNum;
        }

        private void saveToFile(string text, string fileName)
        {
            System.IO.File.WriteAllText(fileName, text);
        }        

        private bool TrimFiles(ref string FirstFileTxt, ref string SecondFileTxt)
        {

            string parNumTxt1 = "", parNumTxt2 = "";
            int parNum1 = 0, parNum2 = 0;
            int[,] bothNum = new int[2, 100];
            List<int> pNum1 = new List<int>();
            List<int> pNum2 = new List<int>();

            try
            {                                            
                string[] str1 = FirstFileTxt.Split(new[] { Environment.NewLine },
                    StringSplitOptions.RemoveEmptyEntries);
                string[] str2 = SecondFileTxt.Split(new[] { Environment.NewLine },
                    StringSplitOptions.RemoveEmptyEntries);

                foreach(string par in str1)
                {
                    string temp = par.Substring(Regex.Match(par, @"\w+").Index);
                    temp = Regex.Match(temp, @"^\d+").Value;
                    if(temp != "")
                    {
                        pNum1.Add(Int16.Parse(temp));
                        parNum1 = Int16.Parse(temp);
                        parNumTxt1 = par;
                    }
                }

                foreach (string par in str2)
                {
                    string temp = par.Substring(Regex.Match(par, @"\w+").Index);
                    temp = Regex.Match(temp, @"^\d+").Value;
                    if (temp != "")
                    {
                        pNum2.Add(Int16.Parse(temp));
                        parNum2 = Int16.Parse(temp);
                        parNumTxt2 = par;
                    }
                }

                if (checkParag(pNum1, pNum2))
                {
                    if (pNum1[pNum1.Count - 1] > pNum2[pNum2.Count - 1])
                    {
                        string tmpStr = FirstFileTxt.Remove(FirstFileTxt.IndexOf(
                            cutString(str1, pNum2[pNum2.Count - 1])));
                        FirstFileTxt = tmpStr.Length > 50 ? tmpStr : FirstFileTxt;
                    }
                    else if (pNum2[pNum2.Count - 1] > pNum1[pNum1.Count - 1])
                    {
                        string tmpStr = SecondFileTxt.Remove(SecondFileTxt.IndexOf(
                            cutString(str2, pNum1[pNum1.Count - 1])));
                        SecondFileTxt = tmpStr.Length > 50 ? tmpStr : SecondFileTxt;
                    }
                }
                else
                {
                    return false;
                }                           
                
            }
            catch (Exception e)
            {       
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        bool checkParag(List<int> para1, List<int> para2)
        {
            int cnt = para1.Count < para2.Count ? para1.Count : para2.Count;
            
            for(int i = 0; i < cnt; i++)
            {
                if(para1[i] != para2[i])
                    return false;
            }

            return true;
        }

        string cutString(string[] text, int lastPar)
        {
            foreach (string par in text)
            {
                string temp = par.Substring(Regex.Match(par, @"\w+").Index);
                temp = Regex.Match(temp, @"^\d+").Value;
                if (temp != "")
                {
                    if (Int16.Parse(temp) > lastPar)
                    {
                        return par;
                    }
                }
            }

            return "";
        }
    }

}
