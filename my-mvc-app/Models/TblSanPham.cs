using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblSanPham")]
    public partial class TblSanPham
    {
        [Key]
        public string SMaSanPham { get; set; } = null!;

        public string? STenSanPham { get; set; }

        public string? SDonVi { get; set; }

        public double? FGia { get; set; }

        public DateTime? DHanSuDung { get; set; }

        public int? ISoLuongTon { get; set; }

        public string? SMaLoaiSanPham { get; set; }

        public string? SMaNhaCungCap { get; set; }

        public virtual TblLoaiSanPham? SMaLoaiSanPhamNavigation { get; set; }

        public virtual TblNhaCungCap? SMaNhaCungCapNavigation { get; set; }

        public virtual ICollection<TblChiTietHoaDonBan> TblChiTietHoaDonBans { get; set; } = new List<TblChiTietHoaDonBan>();

        public virtual ICollection<TblChiTietPhieuNhapKho> TblChiTietPhieuNhapKhos { get; set; } = new List<TblChiTietPhieuNhapKho>();

        public virtual ICollection<TblChiTietPhieuXuatKho> TblChiTietPhieuXuatKhos { get; set; } = new List<TblChiTietPhieuXuatKho>();
    }
    
}


