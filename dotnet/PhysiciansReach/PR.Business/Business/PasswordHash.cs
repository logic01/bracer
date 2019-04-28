﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace PR.Business.Business
{
    public sealed class PasswordHash
    {
        private const int SaltSize = 16, HashSize = 20, HashIter = 10000;
        private readonly byte[] _hash;
        private readonly byte[] _salt =  new byte[] { 0, 3, 3, 4, 9, 7, 7, 1, 0, 3, 3, 4, 9, 7, 7, 1 };


    public PasswordHash(string password)
        {
            new RNGCryptoServiceProvider().GetBytes(_salt );
            _hash = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
        }

        public PasswordHash(byte[] hashBytes)
        {
            Array.Copy(hashBytes, 0, _salt = new byte[SaltSize], 0, SaltSize);
            Array.Copy(hashBytes, SaltSize, _hash = new byte[HashSize], 0, HashSize);
        }

        public PasswordHash(byte[] salt, byte[] hash)
        {
            Array.Copy(salt, 0, _salt = new byte[SaltSize], 0, SaltSize);
            Array.Copy(hash, 0, _hash = new byte[HashSize], 0, HashSize);
        }

        public byte[] ToArray()
        {
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(_salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(_hash, 0, hashBytes, SaltSize, HashSize);
            return hashBytes;
        }

        public byte[] Salt => (byte[])_salt.Clone();

        public byte[] Hash => (byte[])_hash.Clone();

        public bool Verify(string password)
        {
            byte[] test = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
            for (int i = 0; i < HashSize; i++)
            {
                if (test[i] != _hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
