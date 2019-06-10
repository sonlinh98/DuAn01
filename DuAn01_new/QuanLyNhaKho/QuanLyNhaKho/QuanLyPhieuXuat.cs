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
    public partial class QuanLyPhieuXuat : Form
    {
        BLLayer02 layer02 = new BLLayer02();

        public QuanLyPhieuXuat()
        {
            InitializeComponent();
        }

        private void btnLapPhieuXuat_Click(object sender, EventArgs e)
        {
            ThemMoiPhieuXuat pxMoi = new ThemMoiPhieuXuat();
            pxMoi.ShowDialog();
        }

        private void QuanLyPhieuXuat_Load(object sender, EventArgs e)
        {
            dgvDanhSachPhieuXuat.DataSource = layer02.LayThongTinDanhSachPhieuXuat();
        }

        private void dgvDanhSachPhieuXuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvChiTietPhieuXuat.DataSource = null;
            int rowindex = dgvDanhSachPhieuXuat.CurrentCell.RowIndex;
               string MaPX = dgvDanhSachPhieuXuat.Rows[rowindex].Cells[1].Value.ToString();
               //MessageBox.Show(MaPX);
            dgvChiTietPhieuXuat.DataSource = layer02.LayThongTinChiTietCuaPhieuXuat(MaPX);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvDanhSachPhieuXuat.DataSource = null;
            dgvDanhSachPhieuXuat.DataSource = layer02.LayThongTinDanhSachPhieuXuat();
            dgvChiTietPhieuXuat.DataSource = null;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string MaPX = txtTimKiem.Text;
            dgvDanhSachPhieuXuat.DataSource = layer02.TimKiemPhieuXuat(MaPX);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

        }
    }
}
