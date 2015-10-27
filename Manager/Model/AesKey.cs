using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Manager
{
    class AesKey
    {
        public AesKey(string rawKey)
        {
            byte[] keybinary = Encoding.UTF8.GetBytes(rawKey);

            Key = CreateKey(keybinary);
            IV = CreateIV(keybinary);
        }

        private byte[] CreateKey(byte[] keybinary)
        {
            SHA256Managed sha256 = new SHA256Managed();
            return sha256.ComputeHash(keybinary);
        }

        private static byte[] CreateIV(byte[] keybinary)
        {
            SHA1Managed sha1 = new SHA1Managed();
            byte[] iv = new byte[16];
            MemoryStream ms = new MemoryStream(sha1.ComputeHash(keybinary));
            ms.Read(iv, 0, 16);
            ms.Close();
            return iv;
        }

        public byte[] Key { get; private set; }

        public byte[] IV { get; private set; }
    }
}
