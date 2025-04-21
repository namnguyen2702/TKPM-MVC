using Microsoft.EntityFrameworkCore;
using my_mvc_app.Models;
namespace my_mvc_app.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Thêm DbSet cho các bảng trong cơ sở dữ liệu
        public virtual DbSet<TblChiTietHoaDonBan> TblChiTietHoaDonBan{ get; set; }

    public virtual DbSet<TblChiTietPhieuNhapKho> TblChiTietPhieuNhapKho { get; set; }

    public virtual DbSet<TblChiTietPhieuXuatKho> TblChiTietPhieuXuatKho { get; set; }

    public virtual DbSet<TblChucVu> TblChucVu { get; set; }

    public virtual DbSet<TblHoaDonBan> TblHoaDonBan { get; set; }

    public virtual DbSet<TblKhachHang> TblKhachHang { get; set; }

    public virtual DbSet<TblLoaiSanPham> TblLoaiSanPham { get; set; }

    public virtual DbSet<TblNhaCungCap> TblNhaCungCap { get; set; }

    public virtual DbSet<TblNhanVien> TblNhanVien { get; set; }

    public virtual DbSet<TblPhieuNhapKho> TblPhieuNhapKho { get; set; }

    public virtual DbSet<TblPhieuXuatKho> TblPhieuXuatKho { get; set; }

    public virtual DbSet<TblSanPham> TblSanPham { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblChiTietHoaDonBan>(entity =>
        {
            entity.HasKey(e => new { e.SMaHoaDon, e.SMaSanPham }).HasName("PK__tblChiTi__BF40DA44E16D8816");

            entity.ToTable("tblChiTietHoaDonBan");

            entity.Property(e => e.SMaHoaDon)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaHoaDon");
            entity.Property(e => e.SMaSanPham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaSanPham");
            entity.Property(e => e.FGia).HasColumnName("fGia");
            entity.Property(e => e.ISoLuongBan).HasColumnName("iSoLuongBan");

            entity.HasOne(d => d.SMaHoaDonNavigation).WithMany(p => p.TblChiTietHoaDonBans)
                .HasForeignKey(d => d.SMaHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblChiTie__sMaHo__7E37BEF6");

            entity.HasOne(d => d.SMaSanPhamNavigation).WithMany(p => p.TblChiTietHoaDonBans)
                .HasForeignKey(d => d.SMaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblChiTie__sMaSa__7F2BE32F");
        });

        modelBuilder.Entity<TblChiTietPhieuNhapKho>(entity =>
        {
            entity.HasKey(e => new { e.SMaPhieuNhap, e.SMaSanPham }).HasName("PK__tblChiTi__0FD644D06A8F3DDF");

            entity.ToTable("tblChiTietPhieuNhapKho");

            entity.Property(e => e.SMaPhieuNhap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaPhieuNhap");
            entity.Property(e => e.SMaSanPham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaSanPham");
            entity.Property(e => e.FGiaNhap).HasColumnName("fGiaNhap");
            entity.Property(e => e.ISoLuongNhap).HasColumnName("iSoLuongNhap");

            entity.HasOne(d => d.SMaPhieuNhapNavigation).WithMany(p => p.TblChiTietPhieuNhapKhos)
                .HasForeignKey(d => d.SMaPhieuNhap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblChiTie__sMaPh__6FE99F9F");

            entity.HasOne(d => d.SMaSanPhamNavigation).WithMany(p => p.TblChiTietPhieuNhapKhos)
                .HasForeignKey(d => d.SMaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblChiTie__sMaSa__70DDC3D8");
        });

        modelBuilder.Entity<TblChiTietPhieuXuatKho>(entity =>
        {
            entity.HasKey(e => new { e.SMaPhieuXuat, e.SMaSanPham }).HasName("PK__tblChiTi__0D92B29846B5B5E3");

            entity.ToTable("tblChiTietPhieuXuatKho");

            entity.Property(e => e.SMaPhieuXuat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaPhieuXuat");
            entity.Property(e => e.SMaSanPham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaSanPham");
            entity.Property(e => e.FGiaXuat).HasColumnName("fGiaXuat");
            entity.Property(e => e.ISoLuongXuat).HasColumnName("iSoLuongXuat");

            entity.HasOne(d => d.SMaPhieuXuatNavigation).WithMany(p => p.TblChiTietPhieuXuatKhos)
                .HasForeignKey(d => d.SMaPhieuXuat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblChiTie__sMaPh__778AC167");

            entity.HasOne(d => d.SMaSanPhamNavigation).WithMany(p => p.TblChiTietPhieuXuatKhos)
                .HasForeignKey(d => d.SMaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblChiTie__sMaSa__787EE5A0");
        });

        modelBuilder.Entity<TblChucVu>(entity =>
        {
            entity.HasKey(e => e.SMaChucVu).HasName("PK__tblChucV__AC4190CC371EA491");

            entity.ToTable("tblChucVu");

            entity.Property(e => e.SMaChucVu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaChucVu");
            entity.Property(e => e.STenChucVu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sTenChucVu");
        });

        modelBuilder.Entity<TblHoaDonBan>(entity =>
        {
            entity.HasKey(e => e.SMaHoaDon).HasName("PK__tblHoaDo__63EEEA918180E08F");

            entity.ToTable("tblHoaDonBan");

            entity.Property(e => e.SMaHoaDon)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaHoaDon");
            entity.Property(e => e.DNgayLap).HasColumnName("dNgayLap");
            entity.Property(e => e.SMaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaNhanVien");

            entity.HasOne(d => d.SMaNhanVienNavigation).WithMany(p => p.TblHoaDonBans)
                .HasForeignKey(d => d.SMaNhanVien)
                .HasConstraintName("FK__tblHoaDon__sMaNh__7B5B524B");
        });

        modelBuilder.Entity<TblKhachHang>(entity =>
        {
            entity.HasKey(e => e.SMaKhachHang).HasName("PK__tblKhach__911A139BB6BCE191");

            entity.ToTable("tblKhachHang");

            entity.Property(e => e.SMaKhachHang)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaKhachHang");
            entity.Property(e => e.SDiaChi)
                .HasMaxLength(225)
                .HasColumnName("sDiaChi");
            entity.Property(e => e.SSdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sSDT");
            entity.Property(e => e.STenKhachHang)
                .HasMaxLength(225)
                .HasColumnName("sTenKhachHang");
        });

        modelBuilder.Entity<TblLoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.SMaLoaiSanPham).HasName("PK__tblLoaiS__84C280F03E43D9E8");

            entity.ToTable("tblLoaiSanPham");

            entity.Property(e => e.SMaLoaiSanPham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaLoaiSanPham");
            entity.Property(e => e.STenLoaiSanPham)
                .HasMaxLength(225)
                .HasColumnName("sTenLoaiSanPham");
        });

        modelBuilder.Entity<TblNhaCungCap>(entity =>
        {
            entity.HasKey(e => e.SMaNhaCungCap).HasName("PK__tblNhaCu__614004045AEB024A");

            entity.ToTable("tblNhaCungCap");

            entity.Property(e => e.SMaNhaCungCap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaNhaCungCap");
            entity.Property(e => e.SDiaChi)
                .HasMaxLength(225)
                .HasColumnName("sDiaChi");
            entity.Property(e => e.SSdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sSDT");
            entity.Property(e => e.STenCongTy)
                .HasMaxLength(225)
                .HasColumnName("sTenCongTy");
        });

        modelBuilder.Entity<TblNhanVien>(entity =>
        {
            entity.HasKey(e => e.SMaNhanVien).HasName("PK__tblNhanV__B7B44507B93E778A");

            entity.ToTable("tblNhanVien");

            entity.Property(e => e.SMaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaNhanVien");
            entity.Property(e => e.SCccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("sCCCD");
            entity.Property(e => e.SGmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sGmail");
            entity.Property(e => e.SMaChucVu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaChucVu");
            entity.Property(e => e.SMatKhau)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMatKhau");
            entity.Property(e => e.SSdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sSDT");
            entity.Property(e => e.STenNhanVien)
                .HasMaxLength(225)
                .HasColumnName("sTenNhanVien");

            entity.HasOne(d => d.SMaChucVuNavigation).WithMany(p => p.TblNhanViens)
                .HasForeignKey(d => d.SMaChucVu)
                .HasConstraintName("FK__tblNhanVi__sMaCh__5FB337D6");
        });

        modelBuilder.Entity<TblPhieuNhapKho>(entity =>
        {
            entity.HasKey(e => e.SMaPhieuNhap).HasName("PK__tblPhieu__D37874057699954B");

            entity.ToTable("tblPhieuNhapKho");

            entity.Property(e => e.SMaPhieuNhap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaPhieuNhap");
            entity.Property(e => e.DNgayNhap).HasColumnName("dNgayNhap");
            entity.Property(e => e.SMaNhaCungCap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaNhaCungCap");
            entity.Property(e => e.SMaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaNhanVien");

            entity.HasOne(d => d.SMaNhaCungCapNavigation).WithMany(p => p.TblPhieuNhapKhos)
                .HasForeignKey(d => d.SMaNhaCungCap)
                .HasConstraintName("FK__tblPhieuN__sMaNh__6D0D32F4");

            entity.HasOne(d => d.SMaNhanVienNavigation).WithMany(p => p.TblPhieuNhapKhos)
                .HasForeignKey(d => d.SMaNhanVien)
                .HasConstraintName("FK__tblPhieuN__sMaNh__6C190EBB");
        });

        modelBuilder.Entity<TblPhieuXuatKho>(entity =>
        {
            entity.HasKey(e => e.SMaPhieuXuat).HasName("PK__tblPhieu__D13C824D5F61A36D");

            entity.ToTable("tblPhieuXuatKho");

            entity.Property(e => e.SMaPhieuXuat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaPhieuXuat");
            entity.Property(e => e.DNgayXuat).HasColumnName("dNgayXuat");
            entity.Property(e => e.SMaKhachHang)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaKhachHang");
            entity.Property(e => e.SMaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaNhanVien");

            entity.HasOne(d => d.SMaKhachHangNavigation).WithMany(p => p.TblPhieuXuatKhos)
                .HasForeignKey(d => d.SMaKhachHang)
                .HasConstraintName("FK__tblPhieuX__sMaKh__74AE54BC");

            entity.HasOne(d => d.SMaNhanVienNavigation).WithMany(p => p.TblPhieuXuatKhos)
                .HasForeignKey(d => d.SMaNhanVien)
                .HasConstraintName("FK__tblPhieuX__sMaNh__73BA3083");
        });

        modelBuilder.Entity<TblSanPham>(entity =>
        {
            entity.HasKey(e => e.SMaSanPham).HasName("PK__tblSanPh__CAE30D5C9D710162");

            entity.ToTable("tblSanPham");

            entity.Property(e => e.SMaSanPham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaSanPham");
            entity.Property(e => e.DHanSuDung).HasColumnName("dHanSuDung");
            entity.Property(e => e.FGia).HasColumnName("fGia");
            entity.Property(e => e.ISoLuongTon).HasColumnName("iSoLuongTon");
            entity.Property(e => e.SDonVi)
                .HasMaxLength(25)
                .HasColumnName("sDonVi");
            entity.Property(e => e.SMaLoaiSanPham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaLoaiSanPham");
            entity.Property(e => e.SMaNhaCungCap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sMaNhaCungCap");
            entity.Property(e => e.STenSanPham)
                .HasMaxLength(225)
                .HasColumnName("sTenSanPham");

            entity.HasOne(d => d.SMaLoaiSanPhamNavigation).WithMany(p => p.TblSanPhams)
                .HasForeignKey(d => d.SMaLoaiSanPham)
                .HasConstraintName("FK__tblSanPha__sMaLo__68487DD7");

            entity.HasOne(d => d.SMaNhaCungCapNavigation).WithMany(p => p.TblSanPhams)
                .HasForeignKey(d => d.SMaNhaCungCap)
                .HasConstraintName("FK__tblSanPha__sMaNh__693CA210");
        });

    }
    
    }
}