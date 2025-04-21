using System;
using System.Collections.Generic;
using my_mvc_app.Models;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace my_mvc_app.Models{
    [Table("tblChucVu")]
    public partial class TblChucVu
    {
        [Key]
        public string SMaChucVu { get; set; }= null!;

        public string? STenChucVu { get; set; }

        public virtual ICollection<TblNhanVien> TblNhanViens { get; set; } = new List<TblNhanVien>();
    }
}


