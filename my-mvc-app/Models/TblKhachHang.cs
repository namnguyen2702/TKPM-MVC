using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblKhahcHang")]
    public partial class TblKhachHang
    {
        [Key]
        public string SMaKhachHang { get; set; }= null!;

        public string? STenKhachHang { get; set; }

        public string? SSdt { get; set; }

        public string? SDiaChi { get; set; }

        public virtual ICollection<TblPhieuXuatKho> TblPhieuXuatKhos { get; set; } = new List<TblPhieuXuatKho>();
    }
}


