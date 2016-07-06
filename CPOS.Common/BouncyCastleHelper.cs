using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPOS.Common
{
    public class BouncyCastleHelper
    {
        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="pContent">要加密的内容</param>
        /// <param name="pPrivateKey">私钥</param>
        /// <returns></returns>
        public static string PrivateKeyEncrypt(string pContent, string pPrivateKey)
        {
            byte[] btPem = Convert.FromBase64String(pPrivateKey);
            Asn1Object pubKeyObj = Asn1Object.FromByteArray(btPem);//这里也可以从流中读取，从本地导入  
            AsymmetricKeyParameter pubKey = PrivateKeyFactory.CreateKey(btPem);
            IAsymmetricBlockCipher eng = new Pkcs1Encoding(new RsaEngine());
            eng.Init(true, pubKey);//true表示加密  

            //解密已加密的数据
            byte[] encryptedData = Encoding.UTF8.GetBytes(pContent);
            encryptedData = eng.ProcessBlock(encryptedData, 0, encryptedData.Length);
            string result = Convert.ToBase64String(encryptedData);
            return result;
        }

        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="pContent">要加密的内容</param>
        /// <param name="pPrivateKey">私钥</param>
        /// <returns></returns>
        public static string PrivateKeyEncrypt(byte[] encryptedData, string pPrivateKey)
        {
            byte[] btPem = Convert.FromBase64String(pPrivateKey);
            Asn1Object pubKeyObj = Asn1Object.FromByteArray(btPem);//这里也可以从流中读取，从本地导入  
            AsymmetricKeyParameter pubKey = PrivateKeyFactory.CreateKey(btPem);
            IAsymmetricBlockCipher eng = new Pkcs1Encoding(new RsaEngine());
            eng.Init(true, pubKey);//true表示加密  


            encryptedData = eng.ProcessBlock(encryptedData, 0, encryptedData.Length);
            string result = Convert.ToBase64String(encryptedData);
            return result;
        }
        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="pContent">要解密的内容</param>
        /// <param name="pPublicKey">公钥</param>
        /// <returns></returns>
        public static string PublicKeyDecrypt(string pContent, string pPublicKey)
        {
            byte[] btPem = Convert.FromBase64String(pPublicKey);
            //加密、解密  
            Asn1Object pubKeyObj = Asn1Object.FromByteArray(btPem);//这里也可以从流中读取，从本地导入  
            AsymmetricKeyParameter pubKey = PublicKeyFactory.CreateKey(SubjectPublicKeyInfo.GetInstance(pubKeyObj));
            IAsymmetricBlockCipher eng = new Pkcs1Encoding(new RsaEngine());
            eng.Init(false, pubKey);
            //解密已加密的数据
            byte[] encryptedData = Convert.FromBase64String(pContent);
            encryptedData = eng.ProcessBlock(encryptedData, 0, encryptedData.Length);
            string result = Encoding.UTF8.GetString(encryptedData, 0, encryptedData.Length);
            return result;
        }
    }
}
