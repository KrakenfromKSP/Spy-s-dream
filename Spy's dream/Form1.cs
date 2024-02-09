using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Spy_s_dream
{
    public partial class Form1 : Form
    {
        // Об'єкти RSA для зберігання ключів
        private RSAParameters publicKey;
        private RSAParameters privateKey;

        public Form1()
        {
            InitializeComponent();

            // Ініціалізація ключів при створенні форми
            InitializeKeys();
        }

        private void InitializeKeys()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Отримуємо публічний і приватний ключі
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Зашифрування тексту
            string plaintext = textBox1.Text;
            if (!string.IsNullOrWhiteSpace(plaintext))
            {
                string encryptedText = Encrypt(plaintext, publicKey);
                textBox2.Text = encryptedText;
                textBox1.Clear(); // Очищення TextBox1 після шифрування
            }
            else
            {
                MessageBox.Show("Введіть текст для шифрування!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Декодування тексту
            string encryptedText = textBox2.Text;
            if (!string.IsNullOrWhiteSpace(encryptedText))
            {
                string decryptedText = Decrypt(encryptedText, privateKey);
                textBox1.Text = decryptedText;
            }
            else
            {
                MessageBox.Show("Введіть текст для декодування!");
            }
        }

        private string Encrypt(string plaintext, RSAParameters key)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(key);

                byte[] data = Encoding.UTF8.GetBytes(plaintext);
                byte[] encryptedData = rsa.Encrypt(data, false);
                return Convert.ToBase64String(encryptedData);
            }
        }

        private string Decrypt(string encryptedText, RSAParameters key)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(key);

                byte[] encryptedData = Convert.FromBase64String(encryptedText);
                byte[] decryptedData = rsa.Decrypt(encryptedData, false);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }
    }
}