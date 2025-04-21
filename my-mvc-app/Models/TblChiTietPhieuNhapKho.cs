using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblChiTietPhieuNhapKho")]
    public partial class TblChiTietPhieuNhapKho
    {
        [Key]
        public string SMaPhieuNhap { get; set; }= null!;

        public string SMaSanPham { get; set; } = null!;

        public int? ISoLuongNhap { get; set; }

        public double? FGiaNhap { get; set; }

        public virtual TblPhieuNhapKho SMaPhieuNhapNavigation { get; set; } = null!;

        public virtual TblSanPham SMaSanPhamNavigation { get; set; } = null!;
    }
}


