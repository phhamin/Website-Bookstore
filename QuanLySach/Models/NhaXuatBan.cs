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

    public partial class NhaXuatBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaXuatBan()
        {
            this.Saches = new HashSet<Sach>();
        }
        [DisplayName("Mã nhà xuất bản")]
        public string MaNXB { get; set; }

        [DisplayName("Tên nhà xuất bản")]

        public string TenNXB { get; set; }

        [DisplayName("Địa chỉ")]

        public string DiaChi { get; set; }

        [DisplayName("Điện thoại")]

        public string DienThoai { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
