using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblPhieuNhapKho")]
    public partial class TblPhieuNhapKho
    {
        [Key]
        public string SMaPhieuNhap { get; set; } = null!;

        public string? SMaNhanVien { get; set; }

        public string? SMaNhaCungCap { get; set; }

        public DateTime DNgayNhap { get; set; }

        public virtual TblNhaCungCap? SMaNhaCungCapNavigation { get; set; }

        public virtual TblNhanVien? SMaNhanVienNavigation { get; set; }

        public virtual ICollection<TblChiTietPhieuNhapKho> TblChiTietPhieuNhapKhos { get; set; } = new List<TblChiTietPhieuNhapKho>();
    }
}


