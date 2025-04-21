using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblPhieuXuatKho")]
    public partial class TblPhieuXuatKho
    {
    [Key]
    public string SMaPhieuXuat { get; set; } = null!;

    public string? SMaNhanVien { get; set; }

    public string? SMaKhachHang { get; set; }

    public DateTime DNgayXuat { get; set; }

    public virtual TblKhachHang? SMaKhachHangNavigation { get; set; }

    public virtual TblNhanVien? SMaNhanVienNavigation { get; set; }

    public virtual ICollection<TblChiTietPhieuXuatKho> TblChiTietPhieuXuatKhos { get; set; } = new List<TblChiTietPhieuXuatKho>();
    }
}


