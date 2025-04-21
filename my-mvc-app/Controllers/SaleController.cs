using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using my_mvc_app.Models;
using my_mvc_app.ViewModels;
using System.Text.Json;

namespace my_mvc_app.Controllers
{
    public class SaleController : Controller
    {
        private readonly AppDbContext _context;
        private const string SessionKey = "HoaDonTam";

        public SaleController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TongTien = 0;
            return View();
        }

        [HttpGet]
        public IActionResult SearchProduct(string keyword)
        {
            var result = _context.TblSanPham
                .Where(p => string.IsNullOrEmpty(keyword) || p.STenSanPham.Contains(keyword))
                .Select(p => new SanPhamChonViewModel
                {
                    MaSanPham = p.SMaSanPham,
                    TenSanPham = p.STenSanPham,
                    Gia = (float)p.FGia,
                    SoLuongTon = (int)p.ISoLuongTon
                }).ToList();

            ViewBag.Keyword = keyword;
            return PartialView("_ProductListPartial", result);
        }


        [HttpPost]
        public IActionResult AddToInvoice(string maSanPham, int soLuong)
        {
            var product = _context.TblSanPham.FirstOrDefault(p => p.SMaSanPham == maSanPham);
            if (product == null || soLuong <= 0) return BadRequest();

            var danhSach = HttpContext.Session.Get<List<SanPhamChonViewModel>>(SessionKey) ?? new List<SanPhamChonViewModel>();

            var existing = danhSach.FirstOrDefault(p => p.MaSanPham == maSanPham);
            if (existing != null)
            {
                existing.SoLuongChon += soLuong;
            }
            else
            {
                danhSach.Add(new SanPhamChonViewModel
                {
                    MaSanPham = product.SMaSanPham,
                    TenSanPham = product.STenSanPham,
                    Gia = (float)product.FGia,
                    SoLuongTon = (int)product.ISoLuongTon,
                    SoLuongChon = soLuong
                });
            }

            HttpContext.Session.Set(SessionKey, danhSach);
            return PartialView("_HoaDonTamPartial", danhSach);
        }

        [HttpPost]
        public IActionResult CalculateTotal()
        {
            var ds = HttpContext.Session.Get<List<SanPhamChonViewModel>>(SessionKey) ?? new List<SanPhamChonViewModel>();
            float total = ds.Sum(p => p.Gia * p.SoLuongChon);
            ViewBag.TongTien = total;
            return PartialView("_HoaDonTamPartial", ds);
        }

        public IActionResult PrintInvoice()
        {
            // Tạm thời giả lập việc in hóa đơn
            return Content(" Hóa đơn đang được in... (chức năng đang phát triển)");
        }

        [HttpPost]
        public IActionResult RemoveFromInvoice(string maSanPham)
        {
            var danhSach = HttpContext.Session.Get<List<SanPhamChonViewModel>>(SessionKey) ?? new List<SanPhamChonViewModel>();

            // Remove sản phẩm
            var item = danhSach.FirstOrDefault(x => x.MaSanPham == maSanPham);
            if (item != null)
            {
                danhSach.Remove(item);
                HttpContext.Session.Set(SessionKey, danhSach);
            }

            return PartialView("_HoaDonTamPartial", danhSach);
        }


    }
}
