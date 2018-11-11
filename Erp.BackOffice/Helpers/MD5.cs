using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Security.Cryptography;
using System.Text;

public class MD5
{
    public MD5()
    {
        
    }

    // Encrypt bytes to bytes
    public static byte[] EncryptionToBytes(byte[] bSource)
    {
        return new MD5CryptoServiceProvider().ComputeHash(bSource);
    }

    // Encrypt string to bytes
    public byte[] EncryptionToBytes(string strSource)
    {
        return EncryptionToBytes(ASCIIEncoding.ASCII.GetBytes(strSource));
    }

    // Encrypt bytes to string
    public static string EncryptionToString(byte[] bSource)
    {
        return ByteArrayToString(EncryptionToBytes(bSource));
    }

    // Encrypt string to string
    public static string EncryptionToString(string strSource)
    {
        return EncryptionToString(ASCIIEncoding.ASCII.GetBytes(strSource));
    }

    // Compare to hash - any match???
    public static bool CompareHash(byte[] bSource, byte[] bDest)
    {
        // Defaul it's not match
        bool blnEqual = false;
        // Math if same length
        if (bSource.Length == bDest.Length)
        {
            int i = 0;
            while ((i < bSource.Length) && (bSource[i] == bDest[i]))
            {
                i += 1;
            }
            // And match if same value
            if (i == bSource.Length)
            {
                blnEqual = true;
            }
        }
        return blnEqual;
    }

    // Compare to hash - any match???
    public static bool CompareHash(byte[] bSource, string strDest)
    {
        return CompareHash(bSource, ASCIIEncoding.ASCII.GetBytes(strDest));
    }

    // Compare to hash - any match???
    public static bool CompareHash(string strSource, string strDest)
    {
        return CompareHash(ASCIIEncoding.ASCII.GetBytes(strSource), ASCIIEncoding.ASCII.GetBytes(strDest));
    }

    // Compare to hash - any match???
    public static bool CompareHash(string strSource, byte[] bDest)
    {
        return CompareHash(ASCIIEncoding.ASCII.GetBytes(strSource), bDest);
    }

    // Convert byte array to string
    public static string ByteArrayToString(byte[] arrInput)
    {
        int i;
        StringBuilder sOutput = new StringBuilder(arrInput.Length);
        for (i = 0; i <= arrInput.Length - 1; i++)
        {
            sOutput.Append(arrInput[i].ToString("X2"));
        }
        return sOutput.ToString();
    }
}
