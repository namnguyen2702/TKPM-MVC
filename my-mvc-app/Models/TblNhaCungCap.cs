using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblNhaCungCap")]
    public partial class TblNhaCungCap
    {
        [Key]
        public string SMaNhaCungCap { get; set; } = null!;

        public string? STenCongTy { get; set; }

        public string? SSdt { get; set; }

        public string? SDiaChi { get; set; }

        public virtual ICollection<TblPhieuNhapKho> TblPhieuNhapKhos { get; set; } = new List<TblPhieuNhapKho>();

        public virtual ICollection<TblSanPham> TblSanPhams { get; set; } = new List<TblSanPham>();
    }

}

