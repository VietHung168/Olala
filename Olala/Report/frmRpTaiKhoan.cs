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
    public partial class frmRpTaiKhoan : DevExpress.XtraEditors.XtraForm
    {
        DateTime dateTo;
        DateTime dateFrom;
        DateTime now = DateTime.Now;
        Boolean chontatCa = false;
        String maCN;
        public frmRpTaiKhoan()
        {
            InitializeComponent();
        }

        private void frmRpTaiKhoan_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nGANHANGDataSet.TaiKhoan' table. You can move, or remove it, as needed.
            this.taiKhoanTableAdapter.Fill(this.nGANHANGDataSet.TaiKhoan);
            // TODO: This line of code loads data into the 'nGANHANGDataSet.TaiKhoan' table. You can move, or remove it, as needed.
            //     this.taiKhoanTableAdapter.Fill(this.nGANHANGDataSet.TaiKhoan);
         //   DS.EnforceConstraints = false; //không cần ktra các ràng buộc
          //  this.chiNhanhTableAdapter.Connection.ConnectionString = Program.connstr;
           // this.chiNhanhTableAdapter.Fill(this.DS.ChiNhanh);

            //đoạn code này vẫn tiềm ẩn lỗi, tự fix thầy sẽ thả vào lổi lúc chạy demo
           // maCN = ((DataRowView)bdsCN[0])["MACN"].ToString().Trim();

            cmbChiNhanh.DataSource = Program.bdsDSPM;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mChinhanh;

            if (Program.mGroup.Trim() == "NGANHANG")
            {
                //Program.bdsDSPM.Filter = "TENCN <> 'TRA CUU'";
                //    cmbChiNhanh.Enabled = true;
                //     radioTatCaCN.Enabled = true;  // có cần phân quyền kh vì tài khoản đứng ở đâu cũng xem đc
            }
            else
            {
                //     cmbChiNhanh.Enabled = false;
                //     radioTatCaCN.Enabled = false;
            }
         //   dayfrom.DateTime = now;
           // dayto.DateTime = now;

        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}