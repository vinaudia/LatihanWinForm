using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace LatihanWinForm
{
    public partial class FrmListMahasiswa : Form
    {
        public FrmListMahasiswa()
        {
            InitializeComponent();

            InisialisasiListView();
        }

        private void InisialisasiListView()
        {
            lvwMahasiswa.View = System.Windows.Forms.View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;

            lvwMahasiswa.Columns.Add("No.", 50, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("NPM", 80, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Nama", 200, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Jenis Kelamin", 100, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Tempat Lahir", 100, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Tgl. Lahir", 100, HorizontalAlignment.Left);
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (!mskNpm.MaskFull)
            {
                MessageBox.Show("NPM Harus Diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mskNpm.Focus();
                return;
            }

            if (!(txtNama.Text.Length > 0))
            {
                MessageBox.Show("Nama Harus Diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNama.Focus();
                return;
            }

            var jenisKelamin = rdoLakilaki.Checked ? "Laki-laki" : "Perempuan";
            var result = MessageBox.Show("Apakah Data Ingin Disimpan?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var noUrut = lvwMahasiswa.Items.Count + 1;

                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(mskNpm.Text);
                item.SubItems.Add(txtNama.Text);
                item.SubItems.Add(jenisKelamin);
                item.SubItems.Add(txtTempatLahir.Text);
                item.SubItems.Add(dtpTanggalLahir.Value.ToString("dd/MM/yyyy"));

                lvwMahasiswa.Items.Add(item);
                ResetForm();
            }
        }

        private void ResetForm()
        {
            mskNpm.Clear();
            txtNama.Clear();
            rdoLakilaki.Checked = true;
            txtTempatLahir.Clear();
            dtpTanggalLahir.Value = DateTime.Today;

            mskNpm.Focus();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                var index = lvwMahasiswa.SelectedIndices[0];
                var nama = lvwMahasiswa.Items[index].SubItems[2].Text;
                var msg = string.Format("Apakah data Mahasiswa '{0}' ingin dihapus?", nama);

                var result = MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    lvwMahasiswa.Items[index].Remove();

                    for (var i = 0; i < lvwMahasiswa.Items.Count; i++)
                    {
                        var noUrut = i + 1;
                        lvwMahasiswa.Items[i].Text = noUrut.ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("Data belum dipilih", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            var msg = "Apakah Anda Yakin?";
            var result = MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void lvwMahasiswa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                var index = lvwMahasiswa.SelectedIndices[0];
                var npm = lvwMahasiswa.Items[index].SubItems[1].Text;
                var nama = lvwMahasiswa.Items[index].SubItems[2].Text;
                var jenisKelamin = lvwMahasiswa.Items[index].SubItems[3].Text;
                string tempatLahir = lvwMahasiswa.Items[index].SubItems[4].Text;
                string tanggalLahir = lvwMahasiswa.Items[index].SubItems[5].Text;
                DateTime dt = DateTime.ParseExact(tanggalLahir, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                mskNpm.Text = npm;
                txtNama.Text = nama;

                txtTempatLahir.Text = tempatLahir;
                if (jenisKelamin == "Laki-laki")
                {
                    rdoLakilaki.Checked = true;
                    rdoPerempuan.Checked = false;
                }
                else
                {
                    rdoLakilaki.Checked = false;
                    rdoPerempuan.Checked = true;
                }

                dtpTanggalLahir.Value = dt;
            }
        }
    }
}
