﻿using QuanLyNhaKho.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaKho
{
    public partial class ThemMoiPhieuNhap : Form
    {
        BLLayer02 layer02 = new BLLayer02();
        List<ChiTietHangHoaDAO> DanhSachHangHoaNhap = new List<ChiTietHangHoaDAO>();
        private double TongTien = 0;



        public ThemMoiPhieuNhap()
        {
            InitializeComponent();
        }

        private void ThemMoiPhieuNhap_Load(object sender, EventArgs e)
        {
            cboKho.DataSource = layer02.LayDanhSachNhaKho();
            cboKho.DisplayMember = "Tenkho";
            cboKho.ValueMember = "MaNK";
            cboKho.SelectedItem = null;
            cboKho.SelectedText = "--Chọn nhà kho--";

            cboNhaCungCap.DataSource = layer02.LayDanhSachNhaCungCap();
            cboNhaCungCap.DisplayMember = "TenNCC";
            cboNhaCungCap.ValueMember = "MaNCC";
            cboNhaCungCap.SelectedItem = null;
            cboNhaCungCap.SelectedText = "--Chọn nhà cung cấp--";

            // Khởi tạo danh sách hàng nhập
            //ChiTietHangHoaDAO chiTietMotSanPham = layer02.LayThongTinMotHangHoaNhapTheoMa(txtMaHangHoa.Text);
            //DanhSachHangHoaNhap.Add(chiTietMotSanPham);

            //dgvDanhSachHangMua.DataSource = DanhSachHangHoaNhap;

            //dgvDanhSachHangMua.Columns["MaHH"].HeaderText = "Mã hàng hóa";
            //dgvDanhSachHangMua.Columns["TenHH"].HeaderText = "Tên hàng hóa";
            //dgvDanhSachHangMua.Columns["DVT"].HeaderText = "DVT";
            //dgvDanhSachHangMua.Columns["SoLuongNhap"].HeaderText = "Số lượng";
            //dgvDanhSachHangMua.Columns["DonGia"].HeaderText = "Giá nhập";
            //dgvDanhSachHangMua.Columns["ThanhTien"].HeaderText = "Thành tiền";


            // Sinh ngẫu nhiên số phiếu nhập
            txtSoPhieuNhap.Text = SinhMaSoPhieuNhapTuDong();
        }

        private void btnThemMatHang_Click(object sender, EventArgs e)
        {

            // Xóa bỏ danh sách hàng hóa cũ
            dgvDanhSachHangMua.DataSource = null;

            // Cập nhật danh sách mới
            ChiTietHangHoaDAO chiTietMotSanPham = layer02.LayThongTinMotHangHoaNhapTheoMa(txtMaHangHoa.Text);

            // Lấy dữ liệu số lượng nhập, giá nhập, thành tiền vào danh sách
            chiTietMotSanPham.DonGia = txtGiaNhap.Text;
            chiTietMotSanPham.SoLuong = txtSoLuong.Text;
            chiTietMotSanPham.ThanhTien = (int.Parse(txtGiaNhap.Text) * int.Parse(txtSoLuong.Text)).ToString();

            DanhSachHangHoaNhap.Add(chiTietMotSanPham);

            dgvDanhSachHangMua.DataSource = DanhSachHangHoaNhap;

            dgvDanhSachHangMua.Columns["MaHH"].HeaderText = "Mã hàng hóa";
            dgvDanhSachHangMua.Columns["TenHH"].HeaderText = "Tên hàng hóa";
            dgvDanhSachHangMua.Columns["DVT"].HeaderText = "DVT";
            dgvDanhSachHangMua.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvDanhSachHangMua.Columns["DonGia"].HeaderText = "Giá nhập";
            dgvDanhSachHangMua.Columns["ThanhTien"].HeaderText = "Thành tiền";

            // Tính tổng tiền mỗi khi thêm sản phẩm
            TongTien += int.Parse(txtSoLuong.Text) * int.Parse(txtGiaNhap.Text);
            txtTongTien.Text = TongTien.ToString();

        }

        //private int CheckDuplicatesHangHoa(string MaHH)
        //{
        //    for (int i = 0; i < DanhSachHangHoaNhap.Rows.Count - 1; i++)
        //    {
        //        string myname = DanhSachHangHoaNhap.Rows[i][0].ToString();

        //        if (myname.Equals(MaHH))
        //        {
        //            return i;
        //        }

        //    }
        //    return -1;
        //}

        private string SinhMaSoPhieuNhapTuDong()
        {
            string maHangHoaGoc = layer02.LayMaSoPhieuNhapCuoiCung().Rows[0][0].ToString();// lấy ô đầu tiên của bảng kết quả trả về
            string kyTuDau = maHangHoaGoc.Substring(0, 2);
            int soCanTang = Convert.ToInt32(maHangHoaGoc.Substring(2)) + 1;

            string maHangHoa = "";
            if (soCanTang >= 0 && soCanTang < 10)
                maHangHoa = kyTuDau + "0000" + soCanTang;
            if (soCanTang >= 10 && soCanTang < 100)
                maHangHoa = kyTuDau + "000" + soCanTang;
            if (soCanTang >= 100 && soCanTang < 1000)
                maHangHoa = kyTuDau + "00" + soCanTang;
            if (soCanTang >= 1000 && soCanTang < 10000)
                maHangHoa = kyTuDau + "0" + soCanTang;
            if (soCanTang >= 10000 && soCanTang < 100000)
                maHangHoa = kyTuDau + soCanTang;
            if (soCanTang >= 100000)
                maHangHoa = "Không thể tăng hơn nữa!";


            return maHangHoa;

        }


        private void ResetThemMoiPhieuNhap()
        {
            txtSoPhieuNhap.Text = SinhMaSoPhieuNhapTuDong();
            txtNguoiGiaoHang.Text = "";

            if (cboKho.SelectedItem != null)
            {
                cboKho.SelectedItem = null;
                cboKho.SelectedText = "--Chọn nhà kho--";
            }

            if (cboNhaCungCap.SelectedItem != null)
            {
                cboNhaCungCap.SelectedItem = null;
                cboNhaCungCap.SelectedText = "--Chọn nhà cung cấp";
            }

            txtGhiChu.Text = "";
            txtTongTien.Text = "0";

            TongTien = 0;

            txtMaHangHoa.Text = "";
            txtSoLuong.Text = "0";
            txtGiaNhap.Text = "0";

            dgvDanhSachHangMua.DataSource = null;
            DanhSachHangHoaNhap.Clear();

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetThemMoiPhieuNhap();
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            layer02.ThemPhieuNhapHangVaoBangPhieuNhap(txtSoPhieuNhap.Text, txtMaNV.Text, cboKho.SelectedValue.ToString(), dtpNgayNhap.Value, txtNguoiGiaoHang.Text, txtGhiChu.Text, double.Parse(txtTongTien.Text), DanhSachHangHoaNhap);

            ResetThemMoiPhieuNhap();

            MessageBox.Show("Thêm phiếu nhập hàng thàng công");
        }
    }
}
