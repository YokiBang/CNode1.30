using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.Common
{
    /// <summary>
    /// 微信解密方法
    /// </summary>
    public class WXAESDecrypt
    {
        /// <summary>
        /// 微信解密方法
        /// </summary>
        /// <param name="encryptedDataStr">包括敏感数据在内的完整用户信息的加密数据</param>
        /// <param name="key">sessionKey</param>
        /// <param name="iv">加密算法的初始向量</param>
        /// <returns></returns>
        public  static string AES_decrypt(string encryptedDataStr, string iv, string sessionKey)
        {
            //设置 cipher 格式 AES-128-CBC  
            RijndaelManaged rijalg = new RijndaelManaged();            
            rijalg.KeySize = 128;
            rijalg.Padding = PaddingMode.PKCS7;
            rijalg.Mode = CipherMode.CBC;
            rijalg.Key = Convert.FromBase64String(sessionKey);
            rijalg.IV = Convert.FromBase64String(iv);

            byte[] encryptedData = Convert.FromBase64String(encryptedDataStr);
            //解密    
            ICryptoTransform decryptor = rijalg.CreateDecryptor(rijalg.Key, rijalg.IV);

            string result;
            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        result = srDecrypt.ReadToEnd();
                    }
                }
            }
            return result;
        }

    }
}
