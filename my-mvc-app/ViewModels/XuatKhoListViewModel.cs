using System;
using System.Collections.Generic;
using my_mvc_app.Models;

namespace my_mvc_app.ViewModels
{
    public class XuatKhoListViewModel
    {
        // Danh sách các phiếu nhập kho với thông tin chi tiết
        public List<XuatKhoDetailViewModel> XuatKhoDetails { get; set; } = new List<XuatKhoDetailViewModel>();
    }

    public class XuatKhoDetailViewModel
    {
        // Thông tin phiếu nhập kho
        public TblPhieuXuatKho PhieuXuatKho { get; set; }
        
        // Danh sách chi tiết phiếu nhập kho
        public List<TblChiTietPhieuXuatKho> ChiTietPhieuXuatKhoList { get; set; } = new List<TblChiTietPhieuXuatKho>();
    }
}
