using Microsoft.AspNetCore.Mvc;
using my_mvc_app.Models;
using my_mvc_app.ViewModels;    
namespace my_mvc_app.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(AppDbContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpPost]
        public IActionResult AddProduct(TblSanPham product)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Dữ liệu sản phẩm không hợp lệ.";
                return RedirectToAction("Index");
            }

            if (_context.TblSanPham.Any(p => p.SMaSanPham == product.SMaSanPham))
            {
                TempData["ErrorMessage"] = "Mã sản phẩm đã tồn tại.";
                return RedirectToAction("Index");
            }

            // Thêm sản phẩm mới vào cơ sở dữ liệu
            _context.TblSanPham.Add(product);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Thêm sản phẩm mới thành công!";
            return RedirectToAction("Index");
        }
        // Sửa đổi dữ liệu sản phẩm trong cơ sở dữ liệu
        private void updateProduct(TblSanPham product)
        {
            _context.TblSanPham.Update(product);
            _context.SaveChanges();
        }

        // Xóa sản phẩm khỏi cơ sở dữ liệu
        private void removeProduct(TblSanPham product)
        {
            _context.TblSanPham.Remove(product);
            _context.SaveChanges();
        }

        // Validate product update
        private bool ValidateUpdateProduct(TblSanPham product)
        {
            if (product == null)
            {
                TempData["ErrorMessage"] = "Sản phẩm không hợp lệ.";
                return false;
            }

            if (string.IsNullOrEmpty(product.SMaSanPham))
            {
                TempData["ErrorMessage"] = "Mã sản phẩm không được để trống.";
                return false;
            }

            return true;
        }

        // Check if product can be deleted
        private bool CheckIfProductCanBeDeleted(string productId, out TblSanPham? product)
        {
            product = _context.TblSanPham.FirstOrDefault(p => p.SMaSanPham == productId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Sản phẩm không tồn tại.";
                return false;
            }

            // Add any additional checks here if needed
            return true;
        }

        // Validate stock removal (Xuất kho)
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
        [HttpGet]
        public IActionResult searchProduct(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập từ khóa tìm kiếm.";
                return RedirectToAction("Index");
            }

            var products = _context.TblSanPham
                .Where(p => p.STenSanPham.Contains(query) || p.SMaSanPham.Contains(query))
                .ToList();

            if (!products.Any())
            {
                TempData["ErrorMessage"] = "Không tìm thấy sản phẩm nào phù hợp.";
                return RedirectToAction("Index");
            }

            var viewModel = new ProductListViewModel
            {
                Products = products
            };

            TempData["SuccessMessage"] = $"Tìm thấy {products.Count} sản phẩm phù hợp.";
            return View("Index", viewModel);
        }
        [Route("sanpham")]
        public IActionResult Index()
        {
            var products = _context.TblSanPham.ToList();

            var viewModel = new ProductListViewModel
            {
                Products = products
            };

            return View(viewModel);
        }

        // Thêm sản phẩm mới
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TblSanPham product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            if (_context.TblSanPham.Any(p => p.SMaSanPham == product.SMaSanPham))
            {
                ModelState.AddModelError("", "Mã sản phẩm đã tồn tại.");
                return View(product);
            }

            AddProduct(product);

            TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
            return RedirectToAction("Index");
        }

        // Sửa sản phẩm
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var product = _context.TblSanPham.FirstOrDefault(p => p.SMaSanPham == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(TblSanPham product)
        {
            if (!ValidateUpdateProduct(product))
            {
                return View(product);
            }

            updateProduct(product);

            TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
            return RedirectToAction("Index");
        }

        // Xóa sản phẩm
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var product = _context.TblSanPham.FirstOrDefault(p => p.SMaSanPham == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            if (!CheckIfProductCanBeDeleted(id, out var product))
            {
                return RedirectToAction("Index");
            }

            removeProduct(product);

            TempData["SuccessMessage"] = "Xóa sản phẩm thành công!";
            return RedirectToAction("Index");
        }
        
    }
}