using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace assign3
{
    // This class handles all reading and writing of the CSV file
    // It is static so we don't need to create an object to use it
    internal static class InventoryService
    {
        // Reads the CSV file and returns a list of Product objects
        public static List<Product> LoadFromCSV(string filePath)
        {
            List<Product> products = new List<Product>();

            // If the file doesnt exist, return an empty list instead of crashing
            if (!File.Exists(filePath)) return products;

            // Read every line in the file but skip the first one (the header row)
            var lines = File.ReadAllLines(filePath).Skip(1);

            foreach (string line in lines)
            {
                // Split the line by commas to get each individual value
                string[] parts = line.Split(',');

                // We need at least 4 columns because the csv has 4 columns
                if (parts.Length >= 4)
                {
                    try
                    {
                        int id = int.Parse(parts[0]);
                        string name = parts[1];
                        string brand = parts[2];

                        // Price is stored in cents in the CSV, divide by 100 to get dollars
                        decimal price = decimal.Parse(parts[3]) / 100m;

                        // default quantity is 0
                        int quantity = 0;

                        // Create a Product object and add it to the list
                        products.Add(new Product(id, name, brand, price, quantity));
                    }
                    catch
                    {
                        // Skip any rows that have formatting errors
                    }
                }
            }

            return products;
        }

        // Writes the current product list back to the CSV file
        public static void SaveToCSV(string filePath, List<Product> products)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the header row first so the columns are labelled has to match the header exactly
                writer.WriteLine("ProductID,ProductName,ProductBrand,Price,Quantity");

                foreach (var p in products)
                {
                    // Convert price back to cents before saving, and include quantity
                    string line = $"{p.ProductID},{p.ProductName},{p.ProductBrand},{p.ProductPrice * 100},{p.ProductQuantity}";
                    writer.WriteLine(line);
                }
            }
        }
    }
}