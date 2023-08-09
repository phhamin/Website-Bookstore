﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLySach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [DisplayName ("Mã đơn hàng ")]
        public string MaDonHang { get; set; }
        [DisplayName("Đã thanh toán ")]
        public Nullable<bool> DaThanhToan { get; set; }
        [DisplayName("Tình trạng giao hàng ")]
        public Nullable<bool> TinhTrangGiaoHang { get; set; }
        [DisplayName("Ngày đặt ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime NgayDat { get; set; }
        [DisplayName("Ngày giao ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> NgayGiao { get; set; }
        [DisplayName("Mã khách hàng ")]
        public string MaKH { get; set; }
        [DisplayName("Tổng tiền ")]
        public decimal TongTien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}
