using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Net1814_212_ASM2_SE183491_Nguyen_Duy_Tan
{
    /// <summary>
    /// Interaction logic for XMLViewer.xaml
    /// </summary>
    public partial class XMLViewer : Window
    {
        private const string filePath = "ProductXML.xml";

        public XMLViewer()
        {
            InitializeComponent();

            LoadXmlFromFile(filePath);
        }

        public void LoadXmlFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string xmlContent = File.ReadAllText(filePath);
                    XmlTextBox.Text = xmlContent;
                }
                else
                {
                    MessageBox.Show($"File {filePath} not found.", "File Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}", "File Read Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveXmlToFile()
        {
            try
            {
                File.WriteAllText(filePath, XmlTextBox.Text);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "File Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveXmlToFile();
        }
    }
}
