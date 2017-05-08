using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Spring.Net.Example.Dao.Security
{
    /// <summary>
    /// Secure 的摘要说明。
    /// </summary>
    public class Secure
    {
        #region 加密解密
        /// <summary>
        /// 3des加密字符串(默认编码)
        /// </summary>
        /// <param name="srcString">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后并经 base64 编码的字符串</returns>
        //public static string Encrypt3DES(string srcString, string key)
        //{
        //    if (key == "")
        //        key = "sm20070209";

        //    string srcStr;
        //    string rtn = "", str;
        //    int temp = 0;
        //    srcStr = srcString.Replace("-", "");

        //    TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
        //    MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

        //    DES.Key = hashMD5.ComputeHash(Encoding.Default.GetBytes(key));
        //    DES.Mode = CipherMode.ECB;

        //    ICryptoTransform DESEncrypt = DES.CreateEncryptor();

        //    byte[] Buffer = Encoding.Default.GetBytes(srcStr);

        //    Buffer = DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length);

        //    for (int i = 0; i < Buffer.Length; i++)
        //    {
        //        temp = (int)Buffer[i];
        //        str = temp.ToString("X");
        //        if (str.Length < 2) str = "0" + str;
        //        rtn += str;
        //    }

        //    return rtn;
        //}

        /// <summary>
        /// 3des解密字符串（采用默认编码）
        /// </summary>
        /// <param name="srcString">要解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的字符串</returns>
        /// <exception cref="">密钥错误</exception>
        //public static string Decrypt3DES(string srcString, string key)
        //{
        //    if (key == "")
        //        key = "sm20070209";

        //    TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
        //    MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

        //    DES.Key = hashMD5.ComputeHash(Encoding.Default.GetBytes(key));
        //    DES.Mode = CipherMode.ECB;

        //    ICryptoTransform DESDecrypt = DES.CreateDecryptor();

        //    string result = "";
        //    string tmpStr = srcString.Replace("-", "");

        //    try
        //    {
        //        byte[] Buffer = new byte[tmpStr.Length / 2];

        //        int tmp;
        //        int j = 0;
        //        for (int i = 0; i < tmpStr.Length; i = i + 2)
        //        {
        //            tmp = Int32.Parse(tmpStr.Substring(i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
        //            Buffer[j] = System.Convert.ToByte(tmp);
        //            j++;
        //        }

        //        result = Encoding.Default.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// 获取MD5加密后的字符串(不指定编码方式)
        ///// </summary>
        ///// <param name="srcString">加密前的字符串</param>
        ///// <returns>加密后的字符串</returns>
        //public static string MD5Encode(string srcString)
        //{
        //    //缺省用 gb2312 编码
        //    return MD5Encode(srcString, Encoding.GetEncoding("gb2312"));
        //}

        ///// <summary>
        ///// 获取MD5加密后的字符串(指定编码方式)
        ///// </summary>
        ///// <param name="srcString">加密前的字符串</param>
        ///// <param name="encoding">编码方式</param>
        ///// <returns>加密后的字符串</returns>
        //public static string MD5Encode(string srcString, Encoding encoding)
        //{
        //    byte[] data = encoding.GetBytes(srcString);
        //    return System.BitConverter.ToString(
        //        new MD5CryptoServiceProvider().ComputeHash(data)).Replace("-", "").ToLower();
        //}

        ///// <summary>
        ///// 把字符串编码成Base64
        ///// </summary>
        ///// <param name="inStr"></param>
        ///// <returns></returns>
        //public static string ToBase64(string inStr)
        //{
        //    return Convert.ToBase64String(Encoding.UTF8.GetBytes(inStr));
        //}

        ///// <summary>
        ///// 把Base64编码的字符串反编码
        ///// </summary>
        ///// <param name="inStr64"></param>
        ///// <returns></returns>
        //public static string FromBase64(string inStr64)
        //{
        //    return Encoding.UTF8.GetString(Convert.FromBase64String(inStr64));
        //}
        #endregion 加密解密

        #region SHA256加密
         public static string SHA256(string inString)
        {
            try
            {
                byte[] _byte = Encoding.UTF8.GetBytes(inString);
                SHA256 _sha256 = new SHA256Managed();
                byte[] _result = _sha256.ComputeHash(_byte);
                return Convert.ToBase64String(_result, 0, _result.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion 加密

        #region AES加解密
         // 使用AES加密字符串
         /// <summary>
         /// 使用AES加密字符串
         /// </summary>
         /// <param name="encryptString">待加密字符串</param>
         /// <param name="encryptKey">加密密匙</param>
         /// <param name="salt">盐</param>
         /// <returns>加密结果，加密失败则返回源串</returns>
         public static string EncryptAES(string encryptString, string encryptKey, string salt)
         {
             AesManaged aes = null;
             MemoryStream ms = null;
             CryptoStream cs = null;

             try
             {
                 if (encryptKey == "")
                     encryptKey = "CIS20141125";

                 if (salt == "")
                     salt = "CIS20141125";

                 Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(encryptKey, Encoding.UTF8.GetBytes(salt));

                 aes = new AesManaged();
                 aes.Key = rfc2898.GetBytes(aes.KeySize / 8);
                 aes.IV = rfc2898.GetBytes(aes.BlockSize / 8);

                 ms = new MemoryStream();
                 cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);

                 byte[] data = Encoding.UTF8.GetBytes(encryptString);
                 cs.Write(data, 0, data.Length);
                 cs.FlushFinalBlock();

                 return Convert.ToBase64String(ms.ToArray());
             }
             catch
             {
                 return encryptString;
             }
             finally
             {
                 if (cs != null)
                     cs.Close();

                 if (ms != null)
                     ms.Close();

                 if (aes != null)
                     aes.Clear();
             }
         }


         // 使用AES解密字符串
         /// <summary>
         /// 使用AES解密字符串
         /// </summary>
         /// <param name="decryptString">待解密字符串</param>
         /// <param name="decryptKey">解密密匙</param>
         /// <param name="salt">盐</param>
         /// <returns>解密结果，解谜失败则返回源串</returns>
         public static string DecryptAES(string decryptString, string decryptKey, string salt)
         {
             AesManaged aes = null;
             MemoryStream ms = null;
             CryptoStream cs = null;

             try
             {
                 Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(decryptKey, Encoding.UTF8.GetBytes(salt));

                 aes = new AesManaged();
                 aes.Key = rfc2898.GetBytes(aes.KeySize / 8);
                 aes.IV = rfc2898.GetBytes(aes.BlockSize / 8);

                 ms = new MemoryStream();
                 cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);

                 byte[] data = Convert.FromBase64String(decryptString);
                 cs.Write(data, 0, data.Length);
                 cs.FlushFinalBlock();

                 return Encoding.UTF8.GetString(ms.ToArray(), 0, ms.ToArray().Length);
             }
             catch
             {
                 return decryptString;
             }
             finally
             {
                 if (cs != null)
                     cs.Close();

                 if (ms != null)
                     ms.Close();

                 if (aes != null)
                     aes.Clear();
             }
         }
        #endregion
    }
}
