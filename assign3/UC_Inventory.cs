using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace assign3
{
    public partial class UC_Inventory : UserControl
    {
        public UC_Inventory()
        {
            InitializeComponent();
        }

        // BindingList and BindingSource allow the grid to update automatically when data changes
        private BindingList<Product> _inventoryList = new BindingList<Product>();
        private BindingSource _bindingSource = new BindingSource();

        // Relative path - the csv file must be in the same folder as the program
        string filePath = "./products.csv";

        // Runs when this screen is loaded
        private void UC_Inventory_Load(object sender, EventArgs e)
        {
            string path = filePath;

            // 1. Load the data into a temporary list
            var tempData = InventoryService.LoadFromCSV(path);

            // 2. Clear the BindingList and add the loaded data
            _inventoryList.Clear();
            foreach (var item in tempData)
            {
                _inventoryList.Add(item);
            }

            // 3. Bind the BindingList to the grid
            dgvInventory.DataSource = _inventoryList;
        }

        // Runs when the Add button is clicked
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. Validation
            if (!ValidateInputs()) return;

            // 2. Create the new product using the textbox values
            int newId = _inventoryList.Count + 1000;
            string name = txtName.Text;
            string brand = txtBrand.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            int quantity = int.Parse(txtQuantity.Text);

            // Use the constructor to build a Product object and add it to the list
            Product newProduct = new Product(newId, name, brand, price, quantity);
            _inventoryList.Add(newProduct);

            // 4. Refresh the grid to show the new item
            _bindingSource.ResetBindings(false);

            // 5. Clear fields for the next entry
            ClearFields();
        }

        // Checks all inputs are valid before adding or updating - returns true if everything is fine
        private bool ValidateInputs()
        {
            // Name can only have letters, numbers and spaces - no special characters like #, $, @
            if (!Regex.IsMatch(txtName.Text, @"^[a-zA-Z0-9 ]+$"))
            {
                MessageBox.Show("Product Name contains invalid characters.");
                return false;
            }

            // Price must be a positive decimal number
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Please enter a valid positive price.");
                return false;
            }

            // Quantity must be a positive whole number
            if (!int.TryParse(txtQuantity.Text, out int qty) || qty < 0)
            {
                MessageBox.Show("Please enter a valid positive quantity.");
                return false;
            }

            return true;
        }

        // Clears all input fields so they are ready for the next entry
        private void ClearFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtBrand.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
        }

        // Runs when the Update button is clicked
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1. Check if a Product ID is present to identify the record
            if (!int.TryParse(txtID.Text, out int idToUpdate))
            {
                MessageBox.Show("Please select a product from the grid to update.");
                return;
            }

            // 2. Find the product in the BindingList using the ID
            var productToUpdate = _inventoryList.FirstOrDefault(p => p.ProductID == idToUpdate);

            if (productToUpdate != null)
            {
                // 3. Validate the new inputs before applying changes
                if (ValidateInputs())
                {
                    // 4. Update the object properties (excluding ProductID)
                    productToUpdate.ProductName = txtName.Text;
                    productToUpdate.ProductBrand = txtBrand.Text;
                    productToUpdate.ProductPrice = decimal.Parse(txtPrice.Text);
                    productToUpdate.ProductQuantity = int.Parse(txtQuantity.Text);

                    // 5. Refresh the grid to show the updated data
                    _bindingSource.ResetBindings(false);
                    dgvInventory.Refresh();

                    // 6. Clear input fields
                    ClearFields();
                    MessageBox.Show("Product updated successfully in the list.");
                }
            }
            else
            {
                MessageBox.Show("Product ID not found in inventory.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Runs when the Delete button is clicked
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string searchTerm = txtDelete.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a Product ID or Name to delete.");
                return;
            }

            // Find the product in the BindingList
            // This checks if the ID matches OR if the Name matches (ignoring case)
            Product productToDelete = _inventoryList.FirstOrDefault(p =>
                p.ProductID.ToString() == searchTerm ||
                p.ProductName.Equals(searchTerm, StringComparison.OrdinalIgnoreCase));

            if (productToDelete != null)
            {
                // Ask the user to confirm before deleting
                DialogResult result = MessageBox.Show($"Are you sure you want to delete {productToDelete.ProductName}?",
                    "Confirm Delete", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Remove from the BindingList - the grid updates automatically
                    _inventoryList.Remove(productToDelete);

                    txtDelete.Clear();
                    MessageBox.Show("Product deleted successfully.");
                }
            }
            else
            {
                MessageBox.Show("No product found matching that ID or Name.");
            }
        }

        // Runs when the Save button is clicked - writes the list back to the CSV file
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string path = filePath;

                // Convert the BindingList to a standard List to pass to the service
                List<Product> listToSave = _inventoryList.ToList();

                // Call the save method
                InventoryService.SaveToCSV(path, listToSave);

                MessageBox.Show("Changes saved to CSV successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
        }

        private void btnClearProduct_Click(object sender, EventArgs e)
        {

        }
    }
}