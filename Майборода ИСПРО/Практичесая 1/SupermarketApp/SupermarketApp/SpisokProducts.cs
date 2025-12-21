using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SupermarketApp
{
    public partial class SpisokProducts : Form
    {
        public SpisokProducts()
        {
            InitializeComponent();
            
            string connectionString = "Data Source=DESKTOP-N5QN3JE\\SQLEXPRESS;Initial Catalog=supermarket;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL-запрос для получения списка продуктов
                string query = "SELECT Name FROM products";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Добавляем название продукта в ComboBox
                        comboBox1.Items.Add(reader["Name"].ToString());
                    }
                }
            }

        }   

        private void buttonCalculate_Click_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                listAddProducts.Items.Add(comboBox1.SelectedItem.ToString());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listAddProducts.Items.Clear(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal total = 0; // Используем decimal, если цена — decimal в БД
            foreach (string productName in listAddProducts.Items)
            {
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-N5QN3JE\\SQLEXPRESS;Initial Catalog=supermarket;Integrated Security=True"))
                {
                    string query = "SELECT ISNULL(Price, 0) FROM Products WHERE Name = @Name";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", productName);
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            total += Convert.ToDecimal(result);
                        }
                    }
                }
            }
            textBox1.Text = total.ToString("C2"); // Выводим сумму с валютным форматом
        }
    }
}
