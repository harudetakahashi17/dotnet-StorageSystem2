using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorageSystem.Models
{
    public class Login
    {
        public int Kode_user { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nama_lengkap { get; set; }
        public bool Level { get; set; }
    }
}