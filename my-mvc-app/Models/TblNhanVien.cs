using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblNhanVien")]
    public partial class TblNhanVien
    {
        [Key]
        public string SMaNhanVien { get; set; }

        public string? STenNhanVien { get; set; }

        public string? SMaChucVu { get; set; }

        public string? SMatKhau { get; set; }

        public string? SSdt { get; set; }

        public string? SCccd { get; set; }

        public string? SGmail { get; set; }
       
        public virtual TblChucVu? SMaChucVuNavigation { get; set; }

        public virtual ICollection<TblHoaDonBan> TblHoaDonBans { get; set; } = new List<TblHoaDonBan>();

        public virtual ICollection<TblPhieuNhapKho> TblPhieuNhapKhos { get; set; } = new List<TblPhieuNhapKho>();

        public virtual ICollection<TblPhieuXuatKho> TblPhieuXuatKhos { get; set; } = new List<TblPhieuXuatKho>();
    }
}


