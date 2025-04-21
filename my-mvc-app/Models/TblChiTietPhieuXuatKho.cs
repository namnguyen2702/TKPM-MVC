using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblChiTietPhieuXuatKho")]
    public partial class TblChiTietPhieuXuatKho
    {
        [Key]
        public string SMaPhieuXuat { get; set; }= null!;

        public string SMaSanPham { get; set; } = null!;

        public int? ISoLuongXuat { get; set; }

        public double? FGiaXuat { get; set; }

        public virtual TblPhieuXuatKho SMaPhieuXuatNavigation { get; set; } = null!;

        public virtual TblSanPham SMaSanPhamNavigation { get; set; } = null!;
    }
}


