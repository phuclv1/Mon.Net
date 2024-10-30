using System;
using System.Collections.Generic;

namespace phuc_585.dulieu;

public partial class SanPham
{
    public string MaSp { get; set; } = null!;

    public string? TenSp { get; set; }

    public int? DonGia { get; set; }

    public int? SoLuongCo { get; set; }

    public string? MaLoai { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual LoaiSp? MaLoaiNavigation { get; set; }
}
