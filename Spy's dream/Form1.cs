using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Spy_s_dream
{
    public partial class Form1 : Form
    {
        // ��'���� RSA ��� ��������� ������
        private RSAParameters publicKey;
        private RSAParameters privateKey;

        public Form1()
        {
            InitializeComponent();

            // ����������� ������ ��� �������� �����
            InitializeKeys();
        }

        private void InitializeKeys()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // �������� �������� � ��������� �����
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ������������ ������
            string plaintext = textBox1.Text;
            if (!string.IsNullOrWhiteSpace(plaintext))
            {
                string encryptedText = Encrypt(plaintext, publicKey);
                textBox2.Text = encryptedText;
                textBox1.Clear(); // �������� TextBox1 ���� ����������
            }
            else
            {
                MessageBox.Show("������ ����� ��� ����������!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // ����������� ������
            string encryptedText = textBox2.Text;
            if (!string.IsNullOrWhiteSpace(encryptedText))
            {
                string decryptedText = Decrypt(encryptedText, privateKey);
                textBox1.Text = decryptedText;
            }
            else
            {
                MessageBox.Show("������ ����� ��� �����������!");
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