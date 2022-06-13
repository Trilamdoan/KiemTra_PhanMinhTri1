using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_PhanMinhTri1.Models;

namespace KiemTra_PhanMinhTri1.Controllers
{
    public class SinhVienController : Controller
    {
        DataDataContext data = new DataDataContext();
        // GET: SinhVien
        public ActionResult ListSV()
        {
            var all_sinhvien = from ss in data.SinhViens select ss;
            return View(all_sinhvien);
        }
        //xem thông tin
        public ActionResult Detail(int id)
        {
            var D_sinhvien = data.SinhViens.Where(m => m.MaSV == id).First();
            return View(D_sinhvien);
        }
        //xóa thông tin
        public ActionResult Delete(int id)
        {
            var D_sv = data.SinhViens.First(m => m.MaSV == id);
            return View(D_sv);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_sv = data.SinhViens.Where(m => m.MaSV == id).First();
            data.SinhViens.DeleteOnSubmit(D_sv);
            data.SubmitChanges();
            return RedirectToAction("ListSV");
        }
        //Thêm 
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SinhVien s)
        {
            var E_MaSV = Convert.ToInt32(collection["MaSV"]);
            var E_HoTen = collection["HoTen"];
            var E_GioiTinh = collection["GioiTinh"];         
            var E_NgaySinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_Hinh = collection["Hinh"];
            var E_MaNganh = Convert.ToInt32(collection["MaNganh"]);
            if (string.IsNullOrEmpty(E_HoTen))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.MaSV = E_MaSV;
                s.HoTen = E_HoTen;              
                s.GioiTinh = E_GioiTinh.ToString();
                s.NgaySinh = E_NgaySinh;
                s.Hinh = E_Hinh.ToString();
                s.MaNganh = E_MaNganh;
                data.SinhViens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("ListSV");
            }
            return this.Create();
        }
        //Edit
        public ActionResult Edit(int id)
        {
            var E_sv = data.SinhViens.First(m => m.MaSV == id);
            return View(E_sv);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_sv = data.SinhViens.First(m => m.MaSV == id);
            var E_MaSV = Convert.ToInt32(collection["MaSV"]);
            var E_HoTen = collection["HoTen"];
            var E_GioiTinh = collection["GioiTinh"];
            var E_NgaySinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_Hinh = collection["Hinh"];
            var E_MaNganh = Convert.ToInt32(collection["MaNganh"]);
            E_sv.MaSV = id;
            if (string.IsNullOrEmpty(E_HoTen))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_sv.MaSV = E_MaSV;
                E_sv.HoTen = E_HoTen;
                E_sv.GioiTinh = E_GioiTinh.ToString();
                E_sv.NgaySinh = E_NgaySinh;
                E_sv.Hinh = E_Hinh.ToString();
                E_sv.MaNganh = E_MaNganh;
                UpdateModel(E_sv);
                data.SubmitChanges();
                return RedirectToAction("ListSV");
            }
            return this.Edit(id);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
    }
}