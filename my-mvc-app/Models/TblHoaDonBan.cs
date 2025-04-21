using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblHoaDonBan")]
    public partial class TblHoaDonBan
    {
        [Key]
        public string SMaHoaDon { get; set; }= null!;

        public string? SMaNhanVien { get; set; }

        public DateOnly? DNgayLap { get; set; }

        public virtual TblNhanVien? SMaNhanVienNavigation { get; set; }

        public virtual ICollection<TblChiTietHoaDonBan> TblChiTietHoaDonBans { get; set; } = new List<TblChiTietHoaDonBan>();
    }

}

