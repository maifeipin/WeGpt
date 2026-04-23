using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBGPT
{
    internal class DataEntry
    {
        public DataEntry() { }
    }
    public class SiteCookie
    {
        public string name { get; set; }
        public string value { get; set; }
        public string domain { get; set; }
        public string path { get; set; }
        public long? expires { get; set; }  // Unix timestamp, nullable
        public bool httpOnly { get; set; }
        public bool secure { get; set; }
        public int sameSite { get; set; } // 1 for None, 2 for Lax/Strict

        public override string ToString()
        {
            return $"Name: {name}, Value: {value}, Domain: {domain}, Path: {path}, Expires: {expires}, HttpOnly: {httpOnly}, Secure: {secure}, SameSite: {sameSite}";
        }
    }

}
