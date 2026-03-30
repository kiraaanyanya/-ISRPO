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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string word = ""; 
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            LoadWord();
        }
        void LoadWord()
        {
            string cs = @"Data Source=DESKTOP-N5QN3JE\SQLEXPRESS;Initial Catalog=WordsDB;Integrated Security=True";

            SqlConnection con = new SqlConnection(cs);
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Word FROM Words ORDER BY NEWID()", con);

            var result = cmd.ExecuteScalar();

            con.Close();

            char[] a = word.ToCharArray();

            for (int i = 0; i < a.Length; i++)
            {
                int j = rnd.Next(a.Length);
                char t = a[i];
                a[i] = a[j];
                a[j] = t;
            }

            label1.Text = new string(a);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            LoadWord();
            textBox1.Text = "";
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToUpper() == word)
                MessageBox.Show("Правильно");
            else
                MessageBox.Show("Неправильно");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
