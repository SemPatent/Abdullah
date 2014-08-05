using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practicals_2014.Pages
{
    /// <summary>
    /// Interaction logic for GetPatentsResult.xaml
    /// </summary>
    public partial class GetPatentsResult : System.Windows.Controls.UserControl
    {       
        public List<ResultData> resultDB = new List<ResultData>();

        public GetPatentsResult()
        {           
            InitializeComponent();

            resultDB = System.Windows.Application.Current.Resources["resultData"] as List<ResultData>;
            dgUsers.ItemsSource = resultDB;
        }       
    }

    public class ResultData
    {
        public int Id { get; set; }
        public string DocNum { get; set; }
        public string SaveNameEN { get; set; }
        public string SaveNameRU { get; set; }
        public string SaveFileLocation { get; set; }
    }
}
