using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SkladApp
{
    public partial class Form1 : Form
    {
        string connStr = @"Data Source=DESKTOP-N5QN3JE\SQLEXPRESS;Initial Catalog=sklad;Integrated Security=True;";
        SqlConnection conn;
        SqlDataAdapter adapter;
        DataTable table;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connStr);
            table = new DataTable();

            button3_Click(this, EventArgs.Empty);

            adapter = new SqlDataAdapter("SELECT DISTINCT name FROM products", conn);
            DataTable names = new DataTable();
            adapter.Fill(names);

            foreach (DataRow r in names.Rows)
                comboBox1.Items.Add(r[0].ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("SELECT * FROM products", conn);
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
                comboBox1.Items.Add(textBox1.Text);

            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int newId = 1;

            SqlCommand getMax = new SqlCommand("SELECT ISNULL(MAX(id),0)+1 FROM products", conn);
            conn.Open();
            newId = (int)getMax.ExecuteScalar();
            conn.Close();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO products(id,name,stillage,cell,quantity) VALUES(@id,@n,@s,@c,@q)",
                conn);

            cmd.Parameters.AddWithValue("@id", newId);
            cmd.Parameters.AddWithValue("@n", comboBox1.Text);
            cmd.Parameters.AddWithValue("@s", (int)numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@c", (int)numericUpDown2.Value);
            cmd.Parameters.AddWithValue("@q", (int)numericUpDown3.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Товар добавлен в базу!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку!");
                return;
            }

            int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;

            SqlCommand cmd = new SqlCommand("DELETE FROM products WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Запись удалена!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter(
                "SELECT * FROM products WHERE name LIKE '%' + @n + '%'", conn);

            adapter.SelectCommand.Parameters.AddWithValue("@n", textBox2.Text);

            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter(
                "SELECT * FROM products WHERE stillage=@s AND cell=@c", conn);

            adapter.SelectCommand.Parameters.AddWithValue("@s", (int)numericUpDown4.Value);
            adapter.SelectCommand.Parameters.AddWithValue("@c", (int)numericUpDown5.Value);

            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("products.txt"))
            {
                foreach (DataRow r in table.Rows)
                    sw.WriteLine($"{r[0]} | {r[1]} | {r[2]} | {r[3]} | {r[4]}");
            }
                    MessageBox.Show("Сохранено в products.txt!");
        }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}