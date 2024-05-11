using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCToolsKidForJava
{
    public partial class NccEnCoding : Form
    {
        public NccEnCoding()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String str = textBox2.Text;
            String key = textBox1.Text;
            String res = AesDecryptor_Base64(str, key);
            textBox3.Text = res;
        }

        /// <summary>
        /// AES 算法解密(CBC模式) 先base64解码再解密，返回明文
        /// </summary>
        /// <param name="DecryptStr">密文</param>
        /// <param name="Key">密钥</param>
        /// <returns>明文</returns>
        public string AesDecryptor_Base64(string DecryptStr, string Key)
        {
            try
            {
                byte[] keyArray = encodeKey(Key);
                byte[] decryptArray = Convert.FromBase64String(DecryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.CBC;
                rDel.Padding = PaddingMode.PKCS7;
                rDel.IV = new byte[16];

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] result = cTransform.TransformFinalBlock(decryptArray, 0, decryptArray.Length);

                return Encoding.UTF8.GetString(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        private byte[] encodeKey(String password)
        {
            byte[] result = new byte[16]; for (int i = 0; i < 16; i++)
            {
                result[i] = 0;
            }
            byte[] passByte = Encoding.UTF8.GetBytes(password);
            for (int i = 0; i < passByte.Length; i++)
            {
                if (i >= 16)
                {
                    break;
                }
                result[i] = passByte[i];
            }
            return result;
        }
    }
}
