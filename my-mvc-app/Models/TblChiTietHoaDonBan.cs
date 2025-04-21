using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblChiTietHoaDonBan")]
    public partial class TblChiTietHoaDonBan
    {
        [Key]
        public string SMaHoaDon { get; set; } = null!;

        public string SMaSanPham { get; set; } = null!;

        public int? ISoLuongBan { get; set; }

        public double? FGia { get; set; }

        public virtual TblHoaDonBan SMaHoaDonNavigation { get; set; } = null!;

        public virtual TblSanPham SMaSanPhamNavigation { get; set; } = null!;
    }
}


