using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Olala.Report
{
    public partial class frmRpDaoDich : DevExpress.XtraEditors.XtraForm
    {
        public frmRpDaoDich()
        {
            InitializeComponent();
        }

        private void frmRpDaoDich_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nGANHANGDataSet.TaiKhoan' table. You can move, or remove it, as needed.
            this.taiKhoanTableAdapter.Fill(this.nGANHANGDataSet.TaiKhoan);
            // TODO: This line of code loads data into the 'nGANHANGDataSet.NhanVien' table. You can move, or remove it, as needed.
            this.nhanVienTableAdapter.Fill(this.nGANHANGDataSet.NhanVien);

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {

        }

        private void btnManHinh_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

        }

        private void ngayBatDau_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ngayKetThuc_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtSOTK_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}