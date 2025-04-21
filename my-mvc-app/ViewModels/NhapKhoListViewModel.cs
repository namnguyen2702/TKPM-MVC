using System;
using System.Collections.Generic;
using my_mvc_app.Models;

namespace my_mvc_app.ViewModels
{
    public class NhapKhoListViewModel
    {
        // Danh sách các phiếu nhập kho với thông tin chi tiết
        public List<NhapKhoDetailViewModel> NhapKhoDetails { get; set; } = new List<NhapKhoDetailViewModel>();
    }

    public class NhapKhoDetailViewModel
    {
        // Thông tin phiếu nhập kho
        public TblPhieuNhapKho PhieuNhapKho { get; set; }
        
        // Danh sách chi tiết phiếu nhập kho
        public List<TblChiTietPhieuNhapKho> ChiTietPhieuNhapKhoList { get; set; } = new List<TblChiTietPhieuNhapKho>();
    }
}
