using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace assign3
{
    public partial class UC_Inventory : UserControl
    {
        public UC_Inventory()
        {
            InitializeComponent();
        }

        // BindingList updates the grid automatically when data changes
        // BindingSource connects the list to the grid
        private BindingList<Product> _inventoryList = new BindingList<Product>();
        private BindingSource _bindingSource = new BindingSource();

        // Path to the CSV file - must be in the same folder as the program
        string filePath = "./products.csv";

        // Runs when this screen opens
        private void UC_Inventory_Load(object sender, EventArgs e)
        {
            string path = filePath;

            // 1. Load products from the CSV into a temporary list
            var tempData = InventoryService.LoadFromCSV(path);

            // 2. Clear the BindingList then add each product from the temp list
            _inventoryList.Clear();
            foreach (var item in tempData)
            {
                _inventoryList.Add(item);
            }

            // 3. Connect the BindingList to the grid so products show up
            dgvInventory.DataSource = _inventoryList;
        }

        // Runs when the Add button is clicked
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. Stop if any inputs are invalid
            if (!ValidateInputs()) return;

            // 2. Read values from the input fields
            int newId = _inventoryList.Count + 1000;
            string name = txtName.Text;
            string brand = txtBrand.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            int quantity = int.Parse(txtQuantity.Text);

            // 3. Build a new Product object using the constructor and add it to the list
            Product newProduct = new Product(newId, name, brand, price, quantity);
            _inventoryList.Add(newProduct);

            // 4. Refresh the grid to show the new product
            _bindingSource.ResetBindings(false);

            // 5. Clear the input fields ready for the next entry
            ClearFields();
        }

        // Checks all inputs are valid before adding or updating
        // Returns true if everything is fine, false if something is wrong
        private bool ValidateInputs()
        {
            // Name can only have letters, numbers and spaces - no special characters like #, $, @
            if (!Regex.IsMatch(txtName.Text, @"^[a-zA-Z0-9 ]+$"))
            {
                MessageBox.Show("Product Name contains invalid characters.");
                return false;
            }

            // Price must be a positive number with decimals allowed
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

        // Empties all the input fields so they are ready for the next entry
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
            // 1. Check there is a Product ID to identify which product to update
            if (!int.TryParse(txtID.Text, out int idToUpdate))
            {
                MessageBox.Show("Please select a product from the grid to update.");
                return;
            }

            // 2. Search the list for the product with that ID
            var productToUpdate = _inventoryList.FirstOrDefault(p => p.ProductID == idToUpdate);

            if (productToUpdate != null)
            {
                // 3. Check the new inputs are valid before saving changes
                if (ValidateInputs())
                {
                    // 4. Update the product's details (ID stays the same)
                    productToUpdate.ProductName = txtName.Text;
                    productToUpdate.ProductBrand = txtBrand.Text;
                    productToUpdate.ProductPrice = decimal.Parse(txtPrice.Text);
                    productToUpdate.ProductQuantity = int.Parse(txtQuantity.Text);

                    // 5. Refresh the grid to show the updated data
                    _bindingSource.ResetBindings(false);
                    dgvInventory.Refresh();

                    // 6. Clear the input fields
                    ClearFields();
                    MessageBox.Show("Product updated successfully in the list.");
                }
            }
            else
            {
                MessageBox.Show("Product ID not found in inventory.");
            }
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

            // Search the list by ID or Name (ignoring uppercase/lowercase)
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
                    // Remove from the list - the grid updates automatically
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

        // Runs when the Save button is clicked - writes everything back to the CSV file
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string path = filePath;

                // Convert the BindingList to a normal List to pass to InventoryService
                List<Product> listToSave = _inventoryList.ToList();

                // Call the save method which writes each product as a line in the CSV
                InventoryService.SaveToCSV(path, listToSave);

                MessageBox.Show("Changes saved to CSV successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
        }

        // Runs when the Clear button is clicked - empties all input fields
        private void btnClearProduct_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}