using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorageSystem.Models
{
    public class Barang
    {
        public int Id_barang { get; set; }
        public string Nama_brg { get; set; }
        public string Merek { get; set; }
        public string Satuan { get; set; }
        public int Stok_brg { get; set; }
        public int Harga { get; set; }
        public int Jml_hrg { get; set; }
    }
}