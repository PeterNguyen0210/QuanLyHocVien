﻿// Quản lý Học viên Trung tâm Anh ngữ
// Copyright © 2016, VP2T
// File "frmXemCacLopDay.cs"
// Writing by Nguyễn Lê Hoàng Tuấn (nguyentuanit96@gmail.com)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLogic;
using DataAccess;

namespace QuanLyHocVien
{
    public partial class frmXemCacLopDay : Form
    {
        private KhoaHoc busKhoaHoc = new KhoaHoc();
        private GiangDay busGiangDay = new GiangDay();
        private LopHoc busLopHoc = new LopHoc();

        public frmXemCacLopDay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Nạp lớp lên giao diện
        /// </summary>
        /// <param name="lh"></param>
        public void LoadUI(LOPHOC lh = null)
        {
            if(lh!=null)
            {
                lblTenLop.Text = lh.TenLop;
                lblMaLop.Text = lh.TenLop;
                lblKhoa.Text = lh.KHOAHOC.TenKH;
                lblNgayBatDau.Text = lh.NgayBD.ToString();
                lblNgayKetThuc.Text = lh.NgayKT.ToString();
                lblSiSo.Text = lh.SiSo.ToString();
            }
            else
            {
                lblTenLop.Text = string.Empty;
                lblMaLop.Text = string.Empty;
                lblKhoa.Text = string.Empty;
                lblNgayBatDau.Text = string.Empty;
                lblNgayKetThuc.Text = string.Empty;
                lblSiSo.Text = string.Empty;
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdKhoangThoiGian_CheckedChanged(object sender, EventArgs e)
        {
            dateTuNgay.Enabled = dateDenNgay.Enabled = rdKhoangThoiGian.Checked;
        }

        private void rdKhoaHoc_CheckedChanged(object sender, EventArgs e)
        {
            cboKhoaHoc.Enabled = rdKhoaHoc.Checked;
        }

        private void frmXemCacLopDay_Load(object sender, EventArgs e)
        {
            //load khóa học
            cboKhoaHoc.DataSource = busKhoaHoc.SelectAll();
            cboKhoaHoc.DisplayMember = "TenKH";
            cboKhoaHoc.ValueMember = "MaKH";

            LoadUI();
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            rdKhoangThoiGian.Checked = true;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            gridKetQuaTimKiem.AutoGenerateColumns = false;
            gridKetQuaTimKiem.DataSource = busGiangDay.SelectAll("GV0001", rdKhoangThoiGian.Checked ? (DateTime?)dateTuNgay.Value : null,
                rdKhoangThoiGian.Checked ? (DateTime?)dateDenNgay.Value : null, rdKhoaHoc.Checked ? cboKhoaHoc.SelectedValue.ToString() : null);
        }

        private void gridKetQuaTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadUI(busLopHoc.Select(gridKetQuaTimKiem.SelectedRows[0].Cells["MaLop"].Value.ToString()));
            }
            catch
            {
                LoadUI();
            }
        }

        private void btnXemTatCa_Click(object sender, EventArgs e)
        {
            gridKetQuaTimKiem.AutoGenerateColumns = false;
            gridKetQuaTimKiem.DataSource = busGiangDay.SelectAll("GV0001", null, null, null);
        }
    }
}