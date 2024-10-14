using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace De02
{
    public partial class frmSanpham : Form
    {
        public frmSanpham()
        {
            InitializeComponent();
        }
        private void frmSanpham_Load(object sender, EventArgs e)
        {
            LoadSanpham();
            LoadLoaiSP();
        }

        private void LoadSanpham()
        {
            string query = "SELECT * FROM Sanpham";
            DataTable dt = ExecuteQuery(query);
            // Lấy dữ liệu sản phẩm từ database
          
            lvSanpham.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row["MaSP"].ToString());
                item.SubItems.Add(row["TenSP"].ToString());
                item.SubItems.Add(Convert.ToDateTime(row["NgayNhap"]).ToString("dd/MM/yyyy"));
                item.SubItems.Add(row["MaLoai"].ToString());
                lvSanpham.Items.Add(item);
            }
        }

        private DataTable ExecuteQuery(string query)
        {
            throw new NotImplementedException();
        }

        private void LoadLoaiSP()
        {
            // Lấy dữ liệu loại sản phẩm từ database
            string query = "SELECT * FROM LoaiSP";
            DataTable dt = ExecuteQuery(query);

            cboLoaiSP.DataSource = dt;
            cboLoaiSP.DisplayMember = "TenLoai";
            cboLoaiSP.ValueMember = "MaLoai";
        }

        private void lvSanpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSanpham.SelectedItems.Count > 0)
            {
                ListViewItem item = lvSanpham.SelectedItems[0];
                txtMaSP.Text = item.SubItems[0].Text;
                txtTenSP.Text = item.SubItems[1].Text;
                dtNgaynhap.Value = DateTime.Parse(item.SubItems[2].Text);
                cboLoaiSP.SelectedValue = item.SubItems[3].Text;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btThem_Click(object sender, EventArgs e)
        {
           
         string query = "INSERT INTO Sanpham (MaSP, TenSP, NgayNhap, MaLoai) " +
                               "VALUES (@MaSP, @TenSP, @NgayNhap, @MaLoai)";
                SqlParameter[] parameters = {
        new SqlParameter("@MaSP", txtMaSP.Text),
        new SqlParameter("@TenSP", txtTenSP.Text),
        new SqlParameter("@NgayNhap", dtNgaynhap.Value),
        new SqlParameter("@MaLoai", cboLoaiSP.SelectedValue)
    };

            ExecuteQuery(query, parameters);
                LoadSanpham();
            }

        private void ExecuteQuery(string query, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
           
         string query = "DELETE FROM Sanpham WHERE MaSP = @MaSP";
                SqlParameter[] parameters = {
        new SqlParameter("@MaSP", txtMaSP.Text)
    };

            ExecuteQuery(query, parameters);
                LoadSanpham();
            }

        private void btThoat_Click(object sender, EventArgs e)
        {
        
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }

        private void btSua_Click(object sender, EventArgs e)
        {
                string query = "UPDATE Sanpham SET TenSP = @TenSP, NgayNhap = @NgayNhap, MaLoai = @MaLoai " +
                               "WHERE MaSP = @MaSP";
                SqlParameter[] parameters = {
        new SqlParameter("@MaSP", txtMaSP.Text),
        new SqlParameter("@TenSP", txtTenSP.Text),
        new SqlParameter("@NgayNhap", dtNgaynhap.Value),
        new SqlParameter("@MaLoai", cboLoaiSP.SelectedValue)
    };

            ExecuteQuery(query, parameters);
                LoadSanpham();
            }

        private void ClearForm()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            dtNgaynhap.Value = DateTime.Now;
            cboLoaiSP.SelectedIndex = -1;
        }

    }

}




