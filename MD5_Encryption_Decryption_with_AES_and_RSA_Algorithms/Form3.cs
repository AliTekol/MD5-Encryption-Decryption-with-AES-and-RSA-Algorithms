using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Numerics;

namespace MD5_Encryption_Decryption_with_AES_and_RSA_Algorithms
{
    public partial class Form3 : Form
    {
        private static string M;
        private static double KEY_IN_ASCII_KARSILIGI;
        private int ASCII_BOYUT = 0;
        private int UNICODE_BOYUT = 0;    
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox10.Text = Form1.unHashed;
            richTextBox1.Text = Form2.str3;
            richTextBox2.Text = Form2.str2;
            richTextBox3.Text = Form2.str3+ Form1.unHashed;
            M = richTextBox3.Text;
            int unicodeBoyut = ASCIIEncoding.Unicode.GetByteCount(M);
            int asciiBoyut = ASCIIEncoding.ASCII.GetByteCount(M);
            UNICODE_BOYUT = unicodeBoyut;
            ASCII_BOYUT = asciiBoyut;     
            MessageBox.Show(unicodeBoyut.ToString()+" UNICODE SIZE");
            MessageBox.Show(asciiBoyut.ToString()+" ASCII SIZE");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            AesCryptoServiceProvider crypto = new AesCryptoServiceProvider();
            crypto.KeySize = 256;
            crypto.BlockSize = 128;          
            crypto.GenerateKey();
            byte[] keyGenerated = crypto.Key;
            string Key = Convert.ToBase64String(keyGenerated);
            string encrypted = EncryptText(M, Key);
            string decrypted = DecryptText(encrypted, Key);
            richTextBox8.Text = encrypted.ToString();
            richTextBox9.Text = decrypted.ToString();           
            richTextBox4.Text = Key;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(richTextBox4.Text);
            for(int i = 0; i < richTextBox4.Text.Length; i++)
            {
                richTextBox5.Text += asciiBytes[i].ToString();             
            }      
            KEY_IN_ASCII_KARSILIGI = Convert.ToDouble(richTextBox5.Text);
            BigInteger sonuc1 = BigInteger.Pow((BigInteger)KEY_IN_ASCII_KARSILIGI, (int)Form1.kalicin);
            BigInteger sonuc2 = BigInteger.Parse((sonuc1 % (BigInteger)Form1.kalicin).ToString());
            richTextBox6.Text = sonuc2.ToString();
            richTextBox7.Text = richTextBox8.Text + richTextBox6.Text;
            //AES KULANARAK ŞİFRELENMİŞ MESAJ VE RSA İLE ŞİFRELENEN MESAJI BOBA GÖNDERİN
        }
        public string EncryptText(string input, string password)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesEncrypted);
            return result;
        }
        public string DecryptText(string input, string password)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
            string result = Encoding.UTF8.GetString(bytesDecrypted);
            return result;
        }
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using(MemoryStream ms = new MemoryStream())
            {
                using(RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using(var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }
        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using(MemoryStream ms = new MemoryStream())
            {
                using(RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using(var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }
    }
}
