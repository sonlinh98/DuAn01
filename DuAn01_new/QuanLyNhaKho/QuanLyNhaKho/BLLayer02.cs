﻿using QuanLyNhaKho.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaKho
{
    class BLLayer02
    {
        DALayer01 Layer01 = new DALayer01();
        public void CloseConnection()
        {
            SqlConnection connection = Layer01.GetSqlConnection();
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        #region Phần Xuất hàng

        public DataTable LayMaSoPhieuXuatCuoiCung()
        {
            string cmdText = "SELECT TOP 1 * FROM dbo.Phieuxuat ORDER BY MaPX DESC";
            DataTable dataTable = Layer01.GetDataTable(cmdText);
            return dataTable;
        }

        public DataTable LayThongTinDanhSachPhieuXuat()
        {
            string cmdText = "Select Row_number() over(order by dbo.Phieuxuat.MaPX) STT, dbo.Phieuxuat.MaPX AS N'Số phiếu xuất', dbo.Nhakho.TenNK AS N'Kho', dbo.Nhanvien.TenNV as N'Nhân viên', dbo.Phieuxuat.Ngayxuat AS N'Ngày xuất', dbo.Phieuxuat.Nguoinhanhang, dbo.Phieuxuat.Ghichu, dbo.Phieuxuat.Tongtien FROM dbo.Phieuxuat INNER JOIN dbo.Nhanvien ON Nhanvien.MaNV = Phieuxuat.MaNV INNER JOIN dbo.Nhakho ON Nhakho.MaNK = Phieuxuat.MaNK";
            DataTable dataTable = Layer01.GetDataTable(cmdText);
            return dataTable;
        }

        public DataTable LayThongTinChiTietCuaPhieuXuat(String MaPX)
        {
            string cmdText = " Select Row_number() over(order by dbo.CTPhieuxuat.MaHH) STT, dbo.CTPhieuxuat.MaHH,dbo.Hanghoa.TenHH, dbo.CTPhieuxuat.Soluong,dbo.Hanghoa.Dvt, dbo.CTPhieuxuat.Giavon FROM dbo.CTPhieuxuat INNER JOIN dbo.Hanghoa ON Hanghoa.MaHH = CTPhieuxuat.MaHH WHERE dbo.CTPhieuxuat.MaPX ='" + MaPX + "'";
            DataTable dataTable = Layer01.GetDataTable(cmdText);
            return dataTable;
        }

        public DataTable TimKiemPhieuXuat(String MaPX)
        {
            string cmdText = "Select Row_number() over(order by dbo.Phieuxuat.MaPX) STT, dbo.Phieuxuat.MaPX AS N'Số phiếu xuất', dbo.Nhakho.TenNK AS N'Kho', dbo.Nhanvien.TenNV as N'Nhân viên', dbo.Phieuxuat.Ngayxuat AS N'Ngày xuất', dbo.Phieuxuat.Nguoinhanhang, dbo.Phieuxuat.Ghichu, dbo.Phieuxuat.Tongtien FROM dbo.Phieuxuat INNER JOIN dbo.Nhanvien ON Nhanvien.MaNV = Phieuxuat.MaNV INNER JOIN dbo.Nhakho ON Nhakho.MaNK = Phieuxuat.MaNK where dbo.Phieuxuat.MaPX like N'%" + MaPX + "%'";
            DataTable dataTable = Layer01.GetDataTable(cmdText);
            return dataTable;
        }


        public void ThemPhieuNhapHangVaoBangPhieuXuat(string MaPX, string MaNK, string MaNV, DateTime Ngayxuat, string NguoiNhanHang, string GhiChu, double tongTien, List<ChiTietHangHoaDAO> DanhSachHangXuat)
        {
            // Thêm thông tin chung về phiếu nhập
            string cmdText = "INSERT  INTO dbo.Phieuxuat( MaPX, MaNK ,MaNV ,Ngayxuat ,Nguoinhanhang , Ghichu ,Tongtien) VALUES  ( '" + MaPX + "', '" + MaNK + "' , '" + MaNV + "' , '" + Ngayxuat + "' , '" + NguoiNhanHang + "' ,N'" + GhiChu + "' , " + tongTien + ")";
            Layer01.ExecuteNonQuery(cmdText);

            // Thêm Danh sách hàng nhập và cập nhật số lượng
            foreach (ChiTietHangHoaDAO item in DanhSachHangXuat)
            {
                string cmdText1 = "	INSERT INTO dbo.CTPhieuxuat (MaPX, MaHH, Soluong, Giavon ) VALUES  ( '" + MaPX + "', '" + item.MaHH + "', '" + item.SoLuong + "',  '" + item.DonGia + "' )";
                Layer01.ExecuteNonQuery(cmdText1);
                string cmdText2 = "	UPDATE dbo.Hanghoa SET Soluong = Soluong-" + int.Parse(item.SoLuong) + " WHERE MaHH = '" + item.MaHH + "'";
                Layer01.ExecuteNonQuery(cmdText2);
            }
        }

        #endregion


        #region Phần nhập hàng

        public DataTable LayThongTinDanhSachPhieuNhap()
        {
            string cmdText = "Select Row_number() over(order by dbo.Phieunhap.MaPN) STT, dbo.Phieunhap.MaPN AS N'Số phiếu nhập', dbo.Nhakho.TenNK AS N'Kho', dbo.Nhanvien.TenNV as N'Nhân viên', dbo.Phieunhap.Ngaynhap AS N'Ngày nhập', dbo.Phieunhap.Nguoigiaohang, dbo.Phieunhap.Ghichu, dbo.Phieunhap.Tongtien FROM dbo.Phieunhap INNER JOIN dbo.Nhanvien ON Nhanvien.MaNV = Phieunhap.MaNV INNER JOIN dbo.Nhakho ON Nhakho.MaNK = Phieunhap.MaNK";
            DataTable dataTable = Layer01.GetDataTable(cmdText);
            return dataTable;
        }

        public DataTable LayThongTinChiTietCuaPhieuNhap(String MaPX)
        {
            string cmdText = " Select Row_number() over(order by dbo.CTPhieunhap.MaHH) STT, dbo.CTPhieunhap.MaHH,dbo.Hanghoa.TenHH, dbo.CTPhieunhap.Soluong,dbo.Hanghoa.Dvt, dbo.CTPhieunhap.Giavon FROM dbo.CTPhieunhap INNER JOIN dbo.Hanghoa ON Hanghoa.MaHH = CTPhieunhap.MaHH WHERE dbo.CTPhieunhap.MaPN ='" + MaPX + "'";
            DataTable dataTable = Layer01.GetDataTable(cmdText);
            return dataTable;
        }

        public DataTable TimKiemPhieuNhap(String MaPX)
        {
            string cmdText = "Select Row_number() over(order by dbo.Phieunhap.MaPN) STT, dbo.Phieunhap.MaPN AS N'Số phiếu nhập', dbo.Nhakho.TenNK AS N'Kho', dbo.Nhanvien.TenNV as N'Nhân viên', dbo.Phieunhap.Ngaynhap AS N'Ngày nhập', dbo.Phieunhap.Nguoigiaohang, dbo.Phieunhap.Ghichu, dbo.Phieunhap.Tongtien FROM dbo.Phieunhap INNER JOIN dbo.Nhanvien ON Nhanvien.MaNV = Phieunhap.MaNV INNER JOIN dbo.Nhakho ON Nhakho.MaNK = Phieunhap.MaNK where dbo.Phieunhap.MaPN like N'%" + MaPX + "%'";
            DataTable dataTable = Layer01.GetDataTable(cmdText);
            return dataTable;
        }

        public ChiTietHangHoaDAO LayThongTinMotHangHoaNhapTheoMa(string MaHH)
        {
            string cmdText = "SELECT MaHH, TenHH, Dvt, Soluong, Giavon, Mota AS 'Thanhtien' FROM dbo.Hanghoa where MaHH='"+MaHH+"'";
            SqlDataReader dr = Layer01.GetExecuteReader(cmdText);
            ChiTietHangHoaDAO chiTietHH = new ChiTietHangHoaDAO();
            while (dr.Read())
            {
                chiTietHH = new ChiTietHangHoaDAO(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            return chiTietHH;
        }

        public DataTable LayMaSoPhieuNhapCuoiCung()
        {
            string cmdText = "SELECT TOP 1 * FROM dbo.Phieunhap ORDER BY MaPN DESC";
            DataTable dataTable = Layer01.GetDataTable(cmdText);
            return dataTable;
        }


        public void ThemPhieuNhapHangVaoBangPhieuNhap(string MaPN, string MaNV, string MaNK, DateTime Ngaynhap, string NguoiGiaoHang, string GhiChu, double tongTien, List<ChiTietHangHoaDAO> DanhSachHangNhap)
        {
            // Thêm thông tin chung về phiếu nhập
            string cmdText = "INSERT  INTO dbo.Phieunhap( MaPN , MaNV ,MaNK ,Ngaynhap ,Nguoigiaohang , Ghichu ,Tongtien) VALUES  ( '"+MaPN+"', '"+MaNV+"' , '"+MaNK+"' , '"+Ngaynhap+"' , '"+NguoiGiaoHang+"' ,'"+GhiChu+"' , "+tongTien+")";
            Layer01.ExecuteNonQuery(cmdText);

            // Thêm Danh sách hàng nhập và cập nhật số lượng
            foreach (ChiTietHangHoaDAO item in DanhSachHangNhap)
            {
                 string cmdText1 = "	INSERT INTO dbo.CTPhieunhap ( MaPN, MaHH, Soluong, Giavon ) VALUES  ( '"+MaPN+"', '"+item.MaHH+"', '"+item.SoLuong+"',  '"+item.DonGia+"' )";
                 Layer01.ExecuteNonQuery(cmdText1);
                 string cmdText2 = "	UPDATE dbo.Hanghoa SET Soluong = Soluong+"+int.Parse(item.SoLuong)+" WHERE MaHH = '"+item.MaHH+"'";
                 Layer01.ExecuteNonQuery(cmdText2);
            }
        }


        #endregion

        #region Phần chung của phiếu nhập và phiếu xuất

        public DataTable LayDanhSachNhaKho()
        {
            string cmdText = "SELECT * FROM dbo.Nhakho";
            DataTable dataTable = Layer01.GetDataTable(cmdText);
            return dataTable;
        }

        public DataTable LayDanhSachNhaCungCap()
        {
            string cmdText = "SELECT * FROM dbo.Nhacungcap";
            DataTable dataTable = Layer01.GetDataTable(cmdText);
            return dataTable;
        }



        #endregion
    }
}
