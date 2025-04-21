using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_mvc_app.Models;
using my_mvc_app.ViewModels;    
namespace my_mvc_app.Controllers
{
    public class NhapKhoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<NhapKhoController> _logger;

        public NhapKhoController(AppDbContext context, ILogger<NhapKhoController> logger)
        {
            _context = context;
            _logger = logger;
        }
        private void addStock(TblSanPham product, int quantity)
        {
            product.ISoLuongTon += quantity;
            _context.SaveChanges();
        }
        private void addStock(TblSanPham product, int quantity, double price, string supplierId)
        {
            // Tạo phiếu nhập kho
            var phieuNhapKho = new TblPhieuNhapKho
            {
                SMaPhieuNhap = Guid.NewGuid().ToString("N").Substring(0, 10),
                SMaNhaCungCap = supplierId,
                DNgayNhap = DateTime.Now
            };
            _context.TblPhieuNhapKho.Add(phieuNhapKho);

            // Tạo chi tiết phiếu nhập kho
            var chiTietPhieuNhapKho = new TblChiTietPhieuNhapKho
            {
                SMaPhieuNhap = phieuNhapKho.SMaPhieuNhap,
                SMaSanPham = product.SMaSanPham,
                ISoLuongNhap = quantity,
                FGiaNhap = price
            };
            _context.TblChiTietPhieuNhapKho.Add(chiTietPhieuNhapKho);

            // Cập nhật số lượng tồn của sản phẩm
            product.ISoLuongTon += quantity;

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();
        }
        private bool ValidateStockEntry(string productId, int quantity, out TblSanPham? product)
        {
            product = _context.TblSanPham.FirstOrDefault(p => p.SMaSanPham == productId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Sản phẩm không tồn tại.";
                return false;
            }

            if (quantity <= 0)
            {
                TempData["ErrorMessage"] = "Số lượng nhập phải lớn hơn 0.";
                return false;
            }

            return true;
        }
        [HttpGet]
        [Route("nhapkho")]
        public IActionResult Index()
        {
            var viewModel = new NhapKhoListViewModel();
            
            // Lấy danh sách phiếu nhập kho và chi tiết phiếu nhập kho
            var phieuNhapList = _context.TblPhieuNhapKho
                .Include(p => p.TblChiTietPhieuNhapKhos) // Liên kết với bảng chi tiết phiếu nhập kho
                .ToList();

            foreach (var phieu in phieuNhapList)
            {
                var detailViewModel = new NhapKhoDetailViewModel
                {
                    PhieuNhapKho = phieu,
                    ChiTietPhieuNhapKhoList = phieu.TblChiTietPhieuNhapKhos.ToList() // Lấy danh sách chi tiết phiếu nhập kho cho mỗi phiếu
                };
                
                viewModel.NhapKhoDetails.Add(detailViewModel);
            }

            return View(viewModel); // Return the view with the populated model
        }

        [HttpGet]
        
        public IActionResult NhapKho()
        {
            var viewModel = new NhapKhoListViewModel();
            
            // Lấy danh sách phiếu nhập kho và chi tiết phiếu nhập kho
            var phieuNhapList = _context.TblPhieuNhapKho
                .Include(p => p.TblChiTietPhieuNhapKhos) // Liên kết với bảng chi tiết phiếu nhập kho
                .ToList();

            foreach (var phieu in phieuNhapList)
            {
                var detailViewModel = new NhapKhoDetailViewModel
                {
                    PhieuNhapKho = phieu,
                    ChiTietPhieuNhapKhoList = phieu.TblChiTietPhieuNhapKhos.ToList() // Lấy danh sách chi tiết phiếu nhập kho cho mỗi phiếu
                };
                
                viewModel.NhapKhoDetails.Add(detailViewModel);
            }
            
            return View(viewModel);
        }


        // Nhập kho
        [HttpPost]
        public IActionResult NhapKho(string productId, int quantity, double price, string supplierId)
        {
            if (!ValidateStockEntry(productId, quantity, out var product))
            {
                return RedirectToAction("Index");
            }

            addStock(product, quantity, price, supplierId);

            TempData["SuccessMessage"] = $"Nhập kho thành công! Đã thêm {quantity} sản phẩm.";
            return RedirectToAction("Index");
        }
    }
}