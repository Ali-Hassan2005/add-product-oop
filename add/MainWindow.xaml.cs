using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace form_addProduct
{
    public partial class MainWindow : Window
    {
        private const string apiUrl = "https://salepoint.onrender.com/products/addProduct";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productName = productNameTextBox.Text;
                decimal productNetPrice = decimal.Parse(productnetPriceTextBox.Text);
                decimal productSellPrice = decimal.Parse(productsellPriceTextBox.Text);
                int productStock = int.Parse(productstockTextBox.Text);
                string productCategory = productcategoryTextBox.Text;

                var newProduct = new
                {
                    name = productName,
                    netPrice = productNetPrice,
                    sellPrice = productSellPrice,
                    stock = productStock,
                    category = productCategory
                };

                await SendProductData(newProduct);
                MessageBox.Show("Product added successfully!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid data for prices and stock.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async Task SendProductData(object product)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonData = JsonConvert.SerializeObject(product);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to add product. Status code: {response.StatusCode}");
                }
            }
        }
    }
}
