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
    /// Interaction logic for JsonViewer.xaml
    /// </summary>
    public partial class JsonViewer : Window
    {
        private const string filePath = "ProductJSON.json";

        public JsonViewer()
        {
            InitializeComponent();

            LoadJsonFromFile(filePath);
        }

        public void LoadJsonFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);
                    JsonTextBox.Text = jsonContent;
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

        private void SaveJsonToFile()
        {
            try
            {
                File.WriteAllText(filePath, JsonTextBox.Text);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "File Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveJsonToFile();
        }
    }
}
