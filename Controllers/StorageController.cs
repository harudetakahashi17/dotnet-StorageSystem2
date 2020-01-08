using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorageSystem.Models;

namespace StorageSystem.Controllers
{
    public class StorageController : Controller
    {
        public ActionResult Index()
        {
            string conne = ConfigurationManager.ConnectionStrings["mysqlConn"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(conne);
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
            return View();
        }

        [HttpPost]
        public ActionResult GetData()
        {
            string conne = ConfigurationManager.ConnectionStrings["mysqlConn"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(conne);
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
            var connString = ConfigurationManager.ConnectionStrings["mysqlConn"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            string query = "INSERT INTO tb_barang (nama_brg, merek, satuan, stok_brg, harga, jml_hrg)" +
                "VALUES ('" + nama_barang + "','" + merek + "','" + satuan + "','" + stok + "','" + harga + "','" + jml_harga + "')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            var res = cmd.ExecuteNonQuery();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditData(string id_barang)
        {
            var connString = ConfigurationManager.ConnectionStrings["mysqlConn"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connString);
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
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateData(string id_barang, string nama_barang, string merek, string satuan, string stok, string harga, string jml_harga)
        {
            var connString = ConfigurationManager.ConnectionStrings["mysqlConn"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            string query = "UPDATE tb_barang " +
                "SET nama_brg='" + nama_barang + "', merek='" + merek + "', satuan='" + satuan + "', stok_brg='" + stok + "', harga='" + harga + "', jml_hrg='" + jml_harga + "' " +
                "WHERE id_barang='" + id_barang + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            var res = cmd.ExecuteNonQuery();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteData(string id_barang)
        {
            var connString = ConfigurationManager.ConnectionStrings["mysqlConn"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            string query = "DELETE FROM tb_barang WHERE id_barang='" + id_barang + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            return Json(true);
        }
    }
}