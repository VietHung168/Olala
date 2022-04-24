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

namespace Olala
{
  
    public partial class frmTaiKhoan : DevExpress.XtraEditors.XtraForm
    {  
        int vitri = 0;
    Boolean themTK = false;
    String maCN;
    DateTime now = DateTime.Now;
        public frmTaiKhoan()
        {
            InitializeComponent();
        }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DS.ChiNhanh' table. You can move, or remove it, as needed.
            this.chiNhanhTableAdapter.Fill(this.DS.ChiNhanh);
            // TODO: This line of code loads data into the 'DS.ChiNhanh' table. You can move, or remove it, as needed.
            this.chiNhanhTableAdapter.Fill(this.DS.ChiNhanh);
            // TODO: This line of code loads data into the 'nGANHANGDataSet.GD_GOIRUT' table. You can move, or remove it, as needed.
            DS.EnforceConstraints = false; //không cần ktra các ràng buộc
            this.chiNhanhTableAdapter.Connection.ConnectionString = Program.connstr;
            this.chiNhanhTableAdapter.Fill(this.DS.ChiNhanh);

            this.khachHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khachHangTableAdapter.Fill(this.DS.KhachHang);

            this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr;
            this.taiKhoanTableAdapter.Fill(this.DS.TaiKhoan);

            this.gD_CHUYENTIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gD_CHUYENTIENTableAdapter.Fill(this.DS.GD_CHUYENTIEN);

            this.gD_GOIRUTTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gD_GOIRUTTableAdapter.Fill(this.DS.GD_GOIRUT);

            //đoạn code này vẫn tiềm ẩn lỗi, tự fix thầy sẽ thả vào lổi lúc chạy demo
            // có khi nào nên lấy theo bdsCN sẽ đúng 
            maCN = ((DataRowView)bdsCN[0])["MACN"].ToString().Trim();
            txtMACN.Text = maCN;
            cmbChiNhanh.DataSource = Program.bdsDSPM;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mChinhanh;

            if (Program.mGroup.Trim() == "NGANHANG")
            {
                //Program.bdsDSPM.Filter = "TENCN <> 'TRA CUU'";
                cmbChiNhanh.Enabled = true;

                //nhóm ngan hang chỉ đc xem tra cứu
                btnThem.Enabled = btnGhi.Enabled = btnSua.Enabled = btnXoa.Enabled = btnUndo.Enabled = false;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
                btnThem.Enabled = btnGhi.Enabled = btnSua.Enabled = btnXoa.Enabled = btnUndo.Enabled = true;
            }
            //   txtCMND.ReadOnly = true;
            txtMACN.ReadOnly = true;
            //    txtSOTK.ReadOnly = true; // có được đóng Số TK chỉ khi thêm mới mới đc mở kh ????
            panelControl2.Enabled = false;
            // gcDSKH.Enabled = false; //????????

        }

        private void btnThem_ItemClick(object sender, ItemClickEventArgs e)
        {
            themTK = true; //??? để khi thêm ktra trùng CMND
            vitri = bdsTK.Position;
            panelControl2.Enabled = true;
            bdsTK.AddNew();
            gcTK.Enabled = false; //cho cái lưới enable = false
            gcDSKH.Enabled = false;
            txtMACN.Text = maCN; // test đỡ 1 TH khi chưa có form đăng nhập
            txtCMND.Text = "";
            //    txtCMND.ReadOnly = false; //?????
            txtCMND.Focus(); // kiểu đưa con trỏ chuột về đây đầu tiên 
            txtCMND.Text = ((DataRowView)bdsDSKH[bdsDSKH.Position])["CMND"].ToString().Trim();
            ngayMoTK.EditValue = now;
            //khi them thì chỉ có 2 nút hoạt động là ghi, undo
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnUndo.Enabled = true;
        }

        private void btnSua_ItemClick(object sender, ItemClickEventArgs e)
        {
            vitri = bdsTK.Position;
            panelControl2.Enabled = true;
            gcTK.Enabled = false;
            gcDSKH.Enabled = false;
            // khi sửa chỉ có thể ghi hoặc undo
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnUndo.Enabled = true;
        }

        private void btnGhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (txtSOTK.Text.Trim() == "")
            {
                MessageBox.Show("Số tài khoản không được bỏ trống ! ", "", MessageBoxButtons.OK);
                txtSOTK.Focus();
                return;
            }
            if (!Program.isValidInputCMNDSTK.IsMatch(txtSOTK.Text.Trim()))
            {
                MessageBox.Show("Số tài khoản không đúng định dạng !", "", MessageBoxButtons.OK);
                txtCMND.Focus();
                return;
            }
            if (txtSoDu.Text.Trim() == "")
            {
                MessageBox.Show("Số dư không được bỏ trống ! ", "", MessageBoxButtons.OK);
                txtSOTK.Focus();
                return;
            }
            if (formatSoDu(txtSoDu.Text.Trim()) < 0)
            {
                MessageBox.Show("Số dư tài khoản phải >= 0 !", "", MessageBoxButtons.OK);
                txtSOTK.Focus();
                return;
            }
            if (ngayMoTK.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn ngày cấp ! ", "", MessageBoxButtons.OK);
                ngayMoTK.Focus();
                return;
            }
            if (ngayMoTK.DateTime.CompareTo(now) == 1)
            {
                MessageBox.Show("Không được chọn ngày cấp quá ngày hiện tại ! ", "", MessageBoxButtons.OK);
                ngayMoTK.Focus();
                return;
            }
            //check cmnd tồn tại
            if (frmKhachHang.kiemtraCMND(txtCMND.Text.Trim()) == 0)
            {
                MessageBox.Show("Chứng minh nhân dân này chưa được đăng ký ! ", "", MessageBoxButtons.OK);
                txtCMND.Focus();
                return;
            }
            //check sotk chưa tồn tại
            if (themTK == true && kiemtraSOTK(txtSOTK.Text.Trim()) == 1)
            {
                MessageBox.Show("Số tài khoản đã tồn tại ! ", "", MessageBoxButtons.OK);
                txtSOTK.Focus();
                return;
            }
            try
            {
            

                bdsTK.EndEdit();
                bdsTK.ResetCurrentItem();
              //  this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr; // cái này đúng
                this.taiKhoanTableAdapter.Update(this.DS.TaiKhoan);
                MessageBox.Show("Ghi tài khoản thành công!", "", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi tài khoản.\n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
            gcTK.Enabled = true;
            gcDSKH.Enabled = true;
            panelControl2.Enabled = false;
            //đã ghi r kh thể ghi và undo
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnUndo.Enabled = false;
            themTK = false;
            //      txtCMND.ReadOnly = true; //đóng ô khóa chính, chỉ mở khi thêm mới
        }

        private void btnXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            String soTK = "";
            if (bdsChuyenTien.Count > 0)
            {
                MessageBox.Show("Không thể xóa tài khoản này vì đã tạo tài khoản này đã giao dịch chuyễn tiền", "",
                    MessageBoxButtons.OK);
                return;
            }
            if (bdsGuiRut.Count > 0)
            {
                MessageBox.Show("Không thể xóa tài khoản này vì đã tạo tài khoản này đã giao dịch gửi hoặc rút tiền", "",
                    MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thật sự muốn xóa khách hàng này ?", "Xác nhận",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    soTK = ((DataRowView)bdsTK[bdsTK.Position])["SOTK"].ToString().Trim();
                    bdsTK.RemoveCurrent(); // xóa trên máy hiện tại trước

                    this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.taiKhoanTableAdapter.Update(this.DS.TaiKhoan); // xóa trên csdl*//*
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa khách hàng. Bạn hãy xóa lại\n" + ex.Message, "",
                        MessageBoxButtons.OK);
                    this.taiKhoanTableAdapter.Fill(this.DS.TaiKhoan);
                    bdsTK.Position = bdsTK.Find("SOTK", soTK); //khi xóa bị lổi phải trả lại tài khoản tại vị trí đang xóa
                    return;
                }
            }
            if (bdsTK.Count == 0) btnXoa.Enabled = false; // kh có tài khoản thì kh thể xóa
        }

        private void btnUndo_ItemClick(object sender, ItemClickEventArgs e)
        {
            themTK = false; //khi bấm thêm sau đó chọn undo thì cho cái nút thêm trở lại trạng thái false
            bdsTK.CancelEdit();
            //thêm thì bỏ thêm, sửa thì bỏ sửa
            if (btnThem.Enabled == false) bdsTK.Position = vitri;
            gcTK.Enabled = true;
            gcDSKH.Enabled = true;
            panelControl2.Enabled = false;
            //bấm undo thì kh bấm ghi và undo đc nữa
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnUndo.Enabled = false;
        }

        private void btnReload_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.khachHangTableAdapter.Fill(this.DS.KhachHang);
                this.taiKhoanTableAdapter.Fill(this.DS.TaiKhoan);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnThoat_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        private int kiemtraSOTK(String check_str)
        {
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            String str_sp = "SP_CHECKSOTK";
            Program.Sqlcmd = Program.conn.CreateCommand();
            Program.Sqlcmd.CommandType = CommandType.StoredProcedure;
            Program.Sqlcmd.CommandText = str_sp;
            Program.Sqlcmd.Parameters.Add("@X", SqlDbType.NChar).Value = check_str;
            Program.Sqlcmd.Parameters.Add("@Ret", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            Program.Sqlcmd.ExecuteNonQuery();
            String ret = Program.Sqlcmd.Parameters["@RET"].Value.ToString();
            if (ret == "1")
            {
                return 1; //đã tồn tại
            }
            return 0;
        }
        private long formatSoDu(String str)
        {
            String soDu = "";
            soDu = str.Replace(",", soDu);
            return long.Parse(soDu);
        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
                return;
            Program.servername = cmbChiNhanh.SelectedValue.ToString();

            if (cmbChiNhanh.SelectedIndex != Program.mChinhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;  //tài khoản là nhân bản thì ở chi nhánh nào cũng v có cần hiện cmb 2 chi nhánh kh
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }
            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            }
            else
            {
                this.khachHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khachHangTableAdapter.Fill(this.DS.KhachHang);

                this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr;
                this.taiKhoanTableAdapter.Fill(this.DS.TaiKhoan);

                this.gD_CHUYENTIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.gD_CHUYENTIENTableAdapter.Fill(this.DS.GD_CHUYENTIEN);

                this.gD_GOIRUTTableAdapter.Connection.ConnectionString = Program.connstr;
                this.gD_GOIRUTTableAdapter.Fill(this.DS.GD_GOIRUT);
                //lệnh này thừa. Vì phân quyền chỉ có nhóm ngân hàng đc rẻ nhánh
                // và nhóm ngân hàng chỉ đc xem, tra cứu 
                // maCN = ((DataRowView)bdsKH[0])["MACN"].ToString().Trim();
            }
        }
    }
}