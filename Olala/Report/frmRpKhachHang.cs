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
    public partial class frmRpKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public frmRpKhachHang()
        {
            InitializeComponent();
        }

        private void frmRpKhachHang_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nGANHANGDataSet.KhachHang' table. You can move, or remove it, as needed.
            this.khachHangTableAdapter.Fill(this.nGANHANGDataSet.KhachHang);

        }
    }
}