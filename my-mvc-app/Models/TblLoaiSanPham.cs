using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblLoaiSanPham")]
    public partial class TblLoaiSanPham
    {
        [Key]
        public string SMaLoaiSanPham { get; set; } = null!;

        public string? STenLoaiSanPham { get; set; }

        public virtual ICollection<TblSanPham> TblSanPhams { get; set; } = new List<TblSanPham>();
    }
}


