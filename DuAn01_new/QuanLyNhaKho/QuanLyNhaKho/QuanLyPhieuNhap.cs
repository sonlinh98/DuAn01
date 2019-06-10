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
    public partial class QuanLyPhieuNhap : Form
    {
        BLLayer02 layer02 = new BLLayer02();
        public QuanLyPhieuNhap()
        {
            InitializeComponent();
        }

        private void btnLapPhieuNhap_Click(object sender, EventArgs e)
        {
            ThemMoiPhieuNhap pnMoi = new ThemMoiPhieuNhap();
            pnMoi.ShowDialog();
        }

        private void QuanLyPhieuNhap_Load(object sender, EventArgs e)
        {
            dgvDanhSachPhieuNhap.DataSource = layer02.LayThongTinDanhSachPhieuNhap();

        }

        private void dgvDanhSachPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dgvDanhSachPhieuNhap.CurrentCell.RowIndex;
            string MaPN = dgvDanhSachPhieuNhap.Rows[rowindex].Cells[1].Value.ToString();
            dgvChiTietPhieuNhap.DataSource = layer02.LayThongTinChiTietCuaPhieuNhap(MaPN);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvDanhSachPhieuNhap.DataSource = null;
            dgvDanhSachPhieuNhap.DataSource = layer02.LayThongTinDanhSachPhieuNhap();
            dgvChiTietPhieuNhap.DataSource = null;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string MaPX = txtTimKiem.Text;
            dgvDanhSachPhieuNhap.DataSource = layer02.TimKiemPhieuNhap(MaPX);
        }

    }
}
