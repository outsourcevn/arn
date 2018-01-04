using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Aries.Models
{
    public class DBContext
    {
        public static string addUpdateUser(user u)
        {
            try
            {
                using (var db = new etronEntities())
                {
                    if (u.id == 0)
                    {
                        using (MD5 md5Hash = MD5.Create())
                        {
                            string hash = GetMd5Hash(md5Hash, u.pass);
                            u.pass = hash;
                            db.users.Add(u);
                        }
                    }
                    else
                    {
                        // Keep old password
                        if (string.IsNullOrEmpty(u.pass))
                        {
                            var existU = db.users.Find(u.id);
                            if (existU == null) return "Thất bại: Không tìm thấy user";
                            existU.name = u.name;
                            db.Entry(existU).State = EntityState.Modified;
                        }
                        else
                        {
                            // change new pass
                            using (MD5 md5Hash = MD5.Create())
                            {
                                string hash = GetMd5Hash(md5Hash, u.pass);
                                u.pass = hash;
                                db.Entry(u).State = EntityState.Modified;
                            }
                        }
                    }
                    db.SaveChanges();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Thất bại: " + ex.Message;
            }
        }

        public static string deleteUser(int uId)
        {
            try
            {
                using (var db = new etronEntities())
                {
                    var u = new user() { id = uId };
                    db.Entry(u).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Thất bại: " + ex.Message;
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static user validateLogin(LoginModel model)
        {
            user u = null;
            try
            {
                using (var db = new etronEntities())
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string hash = GetMd5Hash(md5Hash, model.Password);
                        u = db.users.Where(f => f.name.Equals(model.Username) && f.pass.Equals(hash)).FirstOrDefault();
                    }
                }
                return u;
            }
            catch (Exception ex)
            {
                return u;
            }
        }
    }
}