using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_mvc_app.Models;
using my_mvc_app.ViewModels;    
namespace my_mvc_app.Controllers
{
    public class XuatKhoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<XuatKhoController> _logger;

        public XuatKhoController(AppDbContext context, ILogger<XuatKhoController> logger)
        {
            _context = context;
            _logger = logger;
        }
        private bool ValidateStockRemoval(string productId, int quantity, out TblSanPham? product)
        {
            product = _context.TblSanPham.FirstOrDefault(p => p.SMaSanPham == productId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Sản phẩm không tồn tại.";
                return false;
            }

            if (quantity <= 0)
            {
                TempData["ErrorMessage"] = "Số lượng xuất phải lớn hơn 0.";
                return false;
            }

            if (product.ISoLuongTon < quantity)
            {
                TempData["ErrorMessage"] = "Số lượng tồn không đủ để xuất kho.";
                return false;
            }

            return true;
        }
        private void reduceStock(TblSanPham product, int quantity, double price, string customerId, string employeeId)
        {
            // Tạo phiếu xuất kho
            var phieuXuatKho = new TblPhieuXuatKho
            {
                SMaPhieuXuat = Guid.NewGuid().ToString("N").Substring(0, 10),
                SMaKhachHang = customerId,
                SMaNhanVien = employeeId,
                DNgayXuat = DateTime.Now
            };
            _context.TblPhieuXuatKho.Add(phieuXuatKho);

            // Tạo chi tiết phiếu xuất kho
            var chiTietPhieuXuatKho = new TblChiTietPhieuXuatKho
            {
                SMaPhieuXuat = phieuXuatKho.SMaPhieuXuat,
                SMaSanPham = product.SMaSanPham,
                ISoLuongXuat = quantity,
                FGiaXuat = price
            };
            _context.TblChiTietPhieuXuatKho.Add(chiTietPhieuXuatKho);

            // Cập nhật số lượng tồn của sản phẩm
            product.ISoLuongTon -= quantity;

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();
        }
        [HttpGet]
        [Route("xuatkho")]
        public IActionResult Index()
        {
            var viewModel = new XuatKhoListViewModel();
            
            // Lấy danh sách phiếu nhập kho và chi tiết phiếu nhập kho
            var phieuXuatList = _context.TblPhieuXuatKho
                .Include(p => p.TblChiTietPhieuXuatKhos) // Liên kết với bảng chi tiết phiếu nhập kho
                .ToList();

            foreach (var phieu in phieuXuatList)
            {
                var detailViewModel = new XuatKhoDetailViewModel
                {
                    PhieuXuatKho = phieu,
                    ChiTietPhieuXuatKhoList = phieu.TblChiTietPhieuXuatKhos.ToList() // Lấy danh sách chi tiết phiếu nhập kho cho mỗi phiếu
                };
                
                viewModel.XuatKhoDetails.Add(detailViewModel);
            }

            return View(viewModel); // Return the view with the populated model
        }
        [HttpGet]
        
        public IActionResult XuatKho()
        {
            var viewModel = new XuatKhoListViewModel();
            
            // Lấy danh sách phiếu nhập kho và chi tiết phiếu nhập kho
            var phieuXuatList = _context.TblPhieuXuatKho
                .Include(p => p.TblChiTietPhieuXuatKhos) // Liên kết với bảng chi tiết phiếu nhập kho
                .ToList();

            foreach (var phieu in phieuXuatList)
            {
                var detailViewModel = new XuatKhoDetailViewModel
                {
                    PhieuXuatKho = phieu,
                    ChiTietPhieuXuatKhoList = phieu.TblChiTietPhieuXuatKhos.ToList() // Lấy danh sách chi tiết phiếu nhập kho cho mỗi phiếu
                };
                
                viewModel.XuatKhoDetails.Add(detailViewModel);
            }
            
            return View(viewModel);
        }
        // Xuất kho
        [HttpPost]
        public IActionResult XuatKho(string productId, int quantity, double price, string customerId, string employeeId)
        {
            if (!ValidateStockRemoval(productId, quantity, out var product))
            {
                return RedirectToAction("Index");
            }

            reduceStock(product, quantity, price, customerId, employeeId);

            TempData["SuccessMessage"] = $"Xuất kho thành công! Đã xuất {quantity} sản phẩm.";
            return RedirectToAction("Index");
        }
    }
}