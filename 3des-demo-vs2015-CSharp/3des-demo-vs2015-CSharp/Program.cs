﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System;
public class Des3
{
    #region CBC模式**

    /// <summary>
    /// DES3 CBC模式加密
    /// </summary>
    /// <param name="key">密钥</param>
    /// <param name="iv">IV</param>
    /// <param name="data">明文的byte数组</param>
    /// <returns>密文的byte数组</returns>
    public static byte[] Des3EncodeCBC(byte[] key, byte[] iv, byte[] data)
    {
        //复制于MSDN

        try
        {
            // Create a MemoryStream.
            MemoryStream mStream = new MemoryStream();

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.CBC;             //默认值
            tdsp.Padding = PaddingMode.PKCS7;       //默认值

            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream cStream = new CryptoStream(mStream,
                tdsp.CreateEncryptor(key, iv),
                CryptoStreamMode.Write);

            // Write the byte array to the crypto stream and flush it.
            cStream.Write(data, 0, data.Length);
            cStream.FlushFinalBlock();

            // Get an array of bytes from the 
            // MemoryStream that holds the 
            // encrypted data.
            byte[] ret = mStream.ToArray();

            // Close the streams.
            cStream.Close();
            mStream.Close();

            // Return the encrypted buffer.
            return ret;
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            return null;
        }
    }

    /// <summary>
    /// DES3 CBC模式解密
    /// </summary>
    /// <param name="key">密钥</param>
    /// <param name="iv">IV</param>
    /// <param name="data">密文的byte数组</param>
    /// <returns>明文的byte数组</returns>
    public static byte[] Des3DecodeCBC(byte[] key, byte[] iv, byte[] data)
    {
        try
        {
            // Create a new MemoryStream using the passed 
            // array of encrypted data.
            MemoryStream msDecrypt = new MemoryStream(data);

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.CBC;
            tdsp.Padding = PaddingMode.PKCS7;

            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                tdsp.CreateDecryptor(key, iv),
                CryptoStreamMode.Read);

            // Create buffer to hold the decrypted data.
            byte[] fromEncrypt = new byte[data.Length];

            // Read the decrypted data out of the crypto stream
            // and place it into the temporary buffer.
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

            //Convert the buffer into a string and return it.
            return fromEncrypt;
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            return null;
        }
    }

    #endregion

    #region ECB模式

    /// <summary>
    /// DES3 ECB模式加密
    /// </summary>
    /// <param name="key">密钥</param>
    /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
    /// <param name="str">明文的byte数组</param>
    /// <returns>密文的byte数组</returns>
    public static byte[] Des3EncodeECB(byte[] key, byte[] iv, byte[] data)
    {
        try
        {
            // Create a MemoryStream.
            MemoryStream mStream = new MemoryStream();

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.ECB;
            tdsp.Padding = PaddingMode.PKCS7;
            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream cStream = new CryptoStream(mStream,
                tdsp.CreateEncryptor(key, iv),
                CryptoStreamMode.Write);

            // Write the byte array to the crypto stream and flush it.
            cStream.Write(data, 0, data.Length);
            cStream.FlushFinalBlock();

            // Get an array of bytes from the 
            // MemoryStream that holds the 
            // encrypted data.
            byte[] ret = mStream.ToArray();

            // Close the streams.
            cStream.Close();
            mStream.Close();

            // Return the encrypted buffer.
            return ret;
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            return null;
        }

    }

    /// <summary>
    /// DES3 ECB模式解密
    /// </summary>
    /// <param name="key">密钥</param>
    /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
    /// <param name="str">密文的byte数组</param>
    /// <returns>明文的byte数组</returns>
    public static byte[] Des3DecodeECB(byte[] key, byte[] iv, byte[] data)
    {
        try
        {
            // Create a new MemoryStream using the passed 
            // array of encrypted data.
            MemoryStream msDecrypt = new MemoryStream(data);

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.ECB;
            tdsp.Padding = PaddingMode.PKCS7;

            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                tdsp.CreateDecryptor(key, iv),
                CryptoStreamMode.Read);

            // Create buffer to hold the decrypted data.
            byte[] fromEncrypt = new byte[data.Length];

            // Read the decrypted data out of the crypto stream
            // and place it into the temporary buffer.
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

            //Convert the buffer into a string and return it.
            return fromEncrypt;
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            return null;
        }
    }

    #endregion

    /// <summary>
    /// 类测试
    /// </summary>
    public static void Test()
    {
        System.Text.Encoding utf8 = System.Text.Encoding.UTF8;

        //key为abcdefghijklmnopqrstuvwx的Base64编码
        string ydata = "00068900002||20180927095839|||3002800173|0200000777|01|0155129175|||";
        byte[] key = Convert.FromBase64String("YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4");
        byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };      //当模式为ECB时，IV无用

        byte[] data = utf8.GetBytes(ydata);
        System.Console.WriteLine("原始报文:");
        System.Console.WriteLine(ydata);
        System.Console.WriteLine();

        byte[] str1 = Des3.Des3EncodeECB(key, iv, data);
        byte[] str2 = Des3.Des3DecodeECB(key, iv, str1);
        System.Console.WriteLine("ECB加密报文:");
        System.Console.WriteLine(Convert.ToBase64String(str1));
        System.Console.WriteLine("ECB解密报文:");
        System.Console.WriteLine(System.Text.Encoding.UTF8.GetString(str2));

        System.Console.WriteLine();

        
        byte[] str3 = Des3.Des3EncodeCBC(key, iv, data);
        byte[] str4 = Des3.Des3DecodeCBC(key, iv, str3);
        System.Console.WriteLine("CBC加密报文:");
        System.Console.WriteLine(Convert.ToBase64String(str3));
        System.Console.WriteLine("CBC解密报文:");
        System.Console.WriteLine(utf8.GetString(str4));

        System.Console.WriteLine();

        Console.ReadLine();
    }

}

namespace _3des_demo_vs2015_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Des3.Test();
        }
    }
}
