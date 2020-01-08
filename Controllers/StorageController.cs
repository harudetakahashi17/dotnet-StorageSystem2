using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorageSystem.Models;
using System.Data;
using System.IO;

namespace StorageSystem.Controllers
{
    public class StorageController : Controller
    {
        private static readonly string connString = ConfigurationManager.ConnectionStrings["mysqlConn"].ConnectionString;
        private readonly MySqlConnection conn = new MySqlConnection(connString);
        public ActionResult Index()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * From tb_barang", conn);
            var reader = cmd.ExecuteReader();
            var list_brg = new List<Barang>();
            while (reader.Read()) {
                list_brg.Add(new Barang{
                    Id_barang = Convert.ToInt32(reader["id_barang"]),
                    Nama_brg = reader["nama_brg"].ToString(),
                    Merek = reader["merek"].ToString(),
                    Satuan = reader["satuan"].ToString(),
                    Stok_brg = Convert.ToInt32(reader["stok_brg"]),
                    Harga = Convert.ToInt32(reader["harga"]),
                    Jml_hrg = Convert.ToInt32(reader["jml_hrg"])
                });
            }
            ViewBag.listBarang = list_brg;
            conn.Close();
            return View();
        }

        [HttpPost]
        public ActionResult GetData()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * From tb_barang", conn);
            var reader = cmd.ExecuteReader();
            var list_brg = new List<Barang>();
            while (reader.Read())
            {
                list_brg.Add(new Barang
                {
                    Id_barang = Convert.ToInt32(reader["id_barang"]),
                    Nama_brg = reader["nama_brg"].ToString(),
                    Merek = reader["merek"].ToString(),
                    Satuan = reader["satuan"].ToString(),
                    Stok_brg = Convert.ToInt32(reader["stok_brg"]),
                    Harga = Convert.ToInt32(reader["harga"]),
                    Jml_hrg = Convert.ToInt32(reader["jml_hrg"])
                });
            }
            return Json(list_brg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TambahData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InputData(string nama_barang, string merek, string satuan, string stok, string harga, string jml_harga)
        {
            conn.Open();
            string query = "INSERT INTO tb_barang (nama_brg, merek, satuan, stok_brg, harga, jml_hrg)" +
                "VALUES ('" + nama_barang + "','" + merek + "','" + satuan + "','" + stok + "','" + harga + "','" + jml_harga + "')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            var res = cmd.ExecuteNonQuery();
            conn.Close();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditData(string id_barang)
        {
            conn.Open();
            string query = "SELECT * FROM tb_barang WHERE id_barang='" + id_barang + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();
            var res = new Barang();
            while (reader.Read())
            {
                res.Id_barang = Convert.ToInt32(reader["id_barang"]);
                res.Nama_brg = reader["nama_brg"].ToString();
                res.Merek = reader["merek"].ToString();
                res.Satuan = reader["satuan"].ToString();
                res.Stok_brg = Convert.ToInt32(reader["stok_brg"]);
                res.Harga = Convert.ToInt32(reader["harga"]);
                res.Jml_hrg = Convert.ToInt32(reader["jml_hrg"]);
            }
            conn.Close();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateData(string id_barang, string nama_barang, string merek, string satuan, string stok, string harga, string jml_harga)
        {
            conn.Open();
            string query = "UPDATE tb_barang " +
                "SET nama_brg='" + nama_barang + "', merek='" + merek + "', satuan='" + satuan + "', stok_brg='" + stok + "', harga='" + harga + "', jml_hrg='" + jml_harga + "' " +
                "WHERE id_barang='" + id_barang + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            var res = cmd.ExecuteNonQuery();
            conn.Close();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteData(string id_barang)
        {
            conn.Open();
            string query = "DELETE FROM tb_barang WHERE id_barang='" + id_barang + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return Json(true);
        }

        [HttpPost]
        public void ExportPDF()
        {
            var pdf = "";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * From tb_barang", conn);
            var reader = cmd.ExecuteReader();
            var list_brg = new List<Barang>();
            while (reader.Read())
            {
                list_brg.Add(new Barang
                {
                    Id_barang = Convert.ToInt32(reader["id_barang"]),
                    Nama_brg = reader["nama_brg"].ToString(),
                    Merek = reader["merek"].ToString(),
                    Satuan = reader["satuan"].ToString(),
                    Stok_brg = Convert.ToInt32(reader["stok_brg"]),
                    Harga = Convert.ToInt32(reader["harga"]),
                    Jml_hrg = Convert.ToInt32(reader["jml_hrg"])
                });
            }
            if(list_brg.Count() > 0)
            {
                DataTable dtBarang = new DataTable();
                dtBarang.Columns.Add(new DataColumn("No"));
                dtBarang.Columns.Add(new DataColumn("ID Barang"));
                dtBarang.Columns.Add(new DataColumn("Nama Barang"));
                dtBarang.Columns.Add(new DataColumn("Merek"));
                dtBarang.Columns.Add(new DataColumn("Satuan"));
                dtBarang.Columns.Add(new DataColumn("Stock"));
                dtBarang.Columns.Add(new DataColumn("Harga"));
                dtBarang.Columns.Add(new DataColumn("Jumlah Harga"));
                for(var i = 0; i < list_brg.Count(); i++)
                {
                    DataRow drBarang = dtBarang.NewRow();
                    drBarang["No"] = i+1;
                    drBarang["ID Barang"] = list_brg[i].Id_barang;
                    drBarang["Nama Barang"] = list_brg[i].Nama_brg;
                    drBarang["Merek"] = list_brg[i].Merek;
                    drBarang["Satuan"] = list_brg[i].Satuan;
                    drBarang["Stock"] = list_brg[i].Stok_brg;
                    drBarang["Harga"] = list_brg[i].Harga.ToString("Rp#,0");
                    drBarang["Jumlah Harga"] = list_brg[i].Jml_hrg.ToString("Rp#,0");
                    dtBarang.Rows.Add(drBarang);
                }
                PdfHelper pdfHelper = new PdfHelper();
                pdf = pdfHelper.ExportToPdf("DaftarBarang-", dtBarang);
            }
            var file = new FileInfo(pdf);
            conn.Close();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Stok_Barang.pdf");
            Response.Charset = "";
            Response.ContentType = "Application/pdf";
            Response.TransmitFile(file.FullName);
            Response.End();
        }
    }
}