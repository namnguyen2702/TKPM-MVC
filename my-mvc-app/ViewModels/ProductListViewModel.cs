using my_mvc_app.Models; 
namespace my_mvc_app.ViewModels{
    public class ProductListViewModel
    {
        public List<TblSanPham> Products { get; set; }= new List<TblSanPham>();
        public TblSanPham NewProduct { get; set; } = new TblSanPham();
    }
}
