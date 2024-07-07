using Jewelry.Data.Models;
using Net1814_212_ASM2_SE183491_NguyenDuyTan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Net1814_212_ASM2_SE183491_NguyenDuyTan
{
    public partial class MainWindow : Window
    {
        private List<Orderitem> orderitems;
        private const string XMLfilepath = "OrderItemXML.xml";
        private const string JSONfilepath = "OrderItemJSON.json";


        public MainWindow()
        {
            InitializeComponent();
            orderitems = new List<Orderitem>();
            OrderItems.ItemsSource = orderitems;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (OrderItems.SelectedItem != null)
            {
                MessageBox.Show($"OrderItem ID: {((Orderitem)OrderItems.SelectedItem).OrderItemId}");
            }

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var orderitem = new Orderitem
            {
                OrderItemId = int.Parse(txtOrderItemId.Text),
                OrderId = int.Parse(txtOrderId.Text),
                ProductId = int.Parse(txtProductId.Text),
                Price = double.Parse(txtPrice.Text),
                Quantity = int.Parse(txtQuantity.Text)
            };
            orderitems.Add(orderitem);
            OrderItems.Items.Refresh();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (OrderItems.SelectedItem != null)
            {
                var orderitem = (Orderitem)OrderItems.SelectedItem;
                orderitem.OrderItemId = int.Parse(txtOrderItemId.Text);
                orderitem.OrderId = int.Parse(txtOrderId.Text);
                orderitem.ProductId = int.Parse(txtProductId.Text);
                orderitem.Price = double.Parse(txtPrice.Text);
                orderitem.Quantity = int.Parse(txtQuantity.Text);
                OrderItems.Items.Refresh();
                ClearInputs();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (OrderItems.SelectedItem != null)
            {
                orderitems.Remove((Orderitem)OrderItems.SelectedItem);
                OrderItems.Items.Refresh();
                ClearInputs();
            }
        }

        private void btnSaveJSON_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderitems.Count > 0)
                {
                    var jsonData = JsonSerializer.Serialize(orderitems, new JsonSerializerOptions { WriteIndented = true });

                    File.WriteAllText(JSONfilepath, jsonData);
                    MessageBox.Show("Products saved to JSON file successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving JSON file: {ex.Message}");
            }
        }

        private void btnLoadJSON_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(JSONfilepath))
                {
                    string jsonData = File.ReadAllText(JSONfilepath);
                    orderitems = JsonSerializer.Deserialize<List<Orderitem>>(jsonData);
                    OrderItems.ItemsSource = orderitems;
                    OrderItems.Items.Refresh();
                    MessageBox.Show("OrderItems loaded from JSON file successfully!");
                }
                else
                {
                    MessageBox.Show("JSON File Not Found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading JSON file: {ex.Message}");
            }
        }

        private void OrderItems_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OrderItems_SelectionChanged(sender, null);
        }

        private void OrderItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderItems.SelectedItem != null)
            {
                var orderitem = (Orderitem)OrderItems.SelectedItem;
                txtOrderItemId.Text = orderitem.OrderItemId.ToString();
                txtOrderId.Text = orderitem.OrderId.ToString();
                txtProductId.Text = orderitem.ProductId.ToString();
                txtPrice.Text = orderitem.Price.ToString();
                txtQuantity.Text = orderitem.Quantity.ToString();
            }
        }

        private void btnSaveXML_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderitems.Count > 0)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<SiProduct>));
                    using FileStream f = File.Create(XMLfilepath);
                    serializer.Serialize(f, orderitems);
                    MessageBox.Show("OrderItems saved to XML file successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving XML file: {ex.Message}");
            }
        }

        private void btnLoadXML_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(XMLfilepath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<SiProduct>));
                    using FileStream xmlLoad = File.Open(XMLfilepath, FileMode.Open);
                    var loadedOrderItems = (List<Orderitem>)serializer.Deserialize(xmlLoad);
                    orderitems = loadedOrderItems;
                    OrderItems.ItemsSource = orderitems;
                    OrderItems.Items.Refresh();
                    MessageBox.Show("OrderItems loaded from XML file successfully!");
                }
                else
                {
                    MessageBox.Show("XML File Not Found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading XML file: {ex.Message}");
            }
        }

        private void ClearInputs()
        {
            txtProductId.Text = "";
            txtOrderItemId.Text = "";
            txtOrderId.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
        }
    }
}
