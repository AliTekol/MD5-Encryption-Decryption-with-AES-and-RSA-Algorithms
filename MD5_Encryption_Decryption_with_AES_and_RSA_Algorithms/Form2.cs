using System;
using System.Text;
using System.Windows.Forms;
using System.Numerics;

namespace MD5_Encryption_Decryption_with_AES_and_RSA_Algorithms
{
    public partial class Form2 : Form
    {
        Form1 frm1 = new Form1();
        Form3 frm3 = new Form3();
        private static int d_diger = 0; //D -> ALICE'S PRIVATE KEY
        public static string str1 = "";
        public static string str2 = "";
        public static string str3 = "";
        public Form2()
        {     
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            d_diger = (int)frm1.dyibul();
            richTextBox5.Text = d_diger.ToString();
            richTextBox4.Text = Form1.kalicisifrelimesaj;           
            byte[] asciiBytes = Encoding.ASCII.GetBytes(richTextBox4.Text);
            for(int i=0;i< richTextBox4.Text.Length;i++)
            {
                richTextBox1.Text += asciiBytes[i].ToString();      
            }
            double ascii = Convert.ToDouble(richTextBox1.Text);
            MessageBox.Show(ascii.ToString() + "ASCII NUMBER");      
            BigInteger ascii_ussu_d = BigInteger.Pow((BigInteger)ascii, (int)Form1.kalicig);         
            BigInteger mod = BigInteger.Parse((ascii_ussu_d % (BigInteger)Form1.kalicin).ToString());
            richTextBox2.Text = mod.ToString();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void Form2_Load(object sender, EventArgs e)
        {                
        }
        private void button2_Click(object sender, EventArgs e)
        {          
            BigInteger modlu_sonuc = BigInteger.Parse(richTextBox2.Text);
            str1 = richTextBox2.Text; 
            BigInteger mesajin_ascii_formu = BigInteger.Pow(modlu_sonuc, d_diger);
            BigInteger mod2 = BigInteger.Parse((mesajin_ascii_formu % (BigInteger)Form1.kalicin).ToString());
            richTextBox6.Text = mod2.ToString();
            str3 = richTextBox6.Text;
            byte[] asciiBytes2 = Encoding.ASCII.GetBytes(richTextBox6.Text);                 
            for(int i = 0; i < richTextBox6.Text.Length; i++)
            {
                richTextBox3.Text += asciiBytes2[i].ToString();
            }
            str2 = richTextBox3.Text;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm3.Show();
            this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}