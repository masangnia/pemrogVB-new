Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement


Public Class DataProduksi
    Dim conn As OleDbConnection
    Dim da As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand
    Dim rd As OleDbDataReader
    Dim str As String

    'Method Koneksi
    Sub Koneksi()
        ' Ganti path str sesuai dengan lokasi Connection String database.
        str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= C:\Users\HP\OneDrive\Desktop\PemrogramanVB\dbbarang (1).accdb"
        conn = New OleDbConnection(str)
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    'Method TextMati
    Sub TextMati()
        Me.tbKodeBarang.Enabled = False
        Me.tbNamaBarang.Enabled = False
        Me.cbSatuan.Enabled = False
        Me.tbJumlah.Enabled = False
        Me.tbHarga.Enabled = False
    End Sub

    'Method TextHidup
    Sub TextHidup()
        Me.tbKodeBarang.Enabled = True
        Me.tbNamaBarang.Enabled = True
        Me.cbSatuan.Enabled = True
        Me.tbJumlah.Enabled = True
        Me.tbHarga.Enabled = True
    End Sub

    'Method Kosong
    Sub Kosong()
        tbKodeBarang.Clear()
        tbNamaBarang.Clear()
        tbJumlah.Clear()
        tbHarga.Clear()
        tbKodeBarang.Focus()
    End Sub
    Private Sub DataProduksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call TampilGrid()
        Call Kosong()
        Call TextMati()
        'dgvDataBarang.ReadOnly = True   
        Me.btnTambah.Enabled = True
        Me.btnSimpan.Enabled = False
        Me.btnEdit.Enabled = False
        Me.btnUpdate.Enabled = False
        Me.btnBatal.Enabled = False
        Me.btnHapus.Enabled = False
        Me.btnKeluar.Enabled = True
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        Call Kosong()
        Call TextHidup()
        Me.btnTambah.Enabled = False
        Me.btnSimpan.Enabled = True
        Me.btnEdit.Enabled = True
        Me.btnUpdate.Enabled = True
        Me.btnBatal.Enabled = False
        Me.btnHapus.Enabled = True
        Me.btnKeluar.Enabled = True
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If tbKodeBarang.Text = "" Or tbNamaBarang.Text = "" Or cbSatuan.Text = "" Then
            MsgBox("Data belum lengkap, pastikan semua form terisi")
            Exit Sub
        Else
            Call Koneksi()
            Dim simpan As String = "insert into tbproduksi (id_barang, nama_barang, jumlah, id_produksi, tgl_produksi)" & "values ('" & tbKodeBarang.Text & "','" & tbNamaBarang.Text & "','" & cbSatuan.Text & "','" & tbJumlah.Text & "','" & tbHarga.Text & "')"
            cmd = New OleDbCommand(simpan, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Data berhasil di input", MsgBoxStyle.Information, "Information")
            Me.OleDbConnection1.Close()
            Call TampilGrid()
            dgvDataBarang.Refresh()
            Call Koneksi()
            Call Kosong()
            Call TextMati()
            Me.btnTambah.Enabled = True
            Me.btnSimpan.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnUpdate.Enabled = False
            Me.btnBatal.Enabled = False
            Me.btnHapus.Enabled = False
            Me.btnKeluar.Enabled = True
        End If
    End Sub
    Sub TampilGrid()
        da = New OleDbDataAdapter("SELECT * FROM tbproduksi", conn)
        ds = New DataSet
        da.Fill(ds, "tbproduksi")
        dgvDataBarang.DataSource = ds.Tables("tbproduksi")
    End Sub
    Private Sub dgvDataBarang_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDataBarang.CellContentClick

    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Close()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Call TextHidup()
        Call TextMati()
        Me.tbKodeBarang.Enabled = False
        Me.btnTambah.Enabled = False
        Me.btnSimpan.Enabled = False
        Me.btnEdit.Enabled = False
        Me.btnUpdate.Enabled = True
        Me.btnBatal.Enabled = True
        Me.btnHapus.Enabled = True
        Me.btnKeluar.Enabled = True
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim sql As String

        If MsgBox("Do you want save again ?", MsgBoxStyle.YesNo, "Message") = vbYes Then
            sql = "UPDATE tbproduksi SET nama_barang= '" & tbNamaBarang.Text & "', jumlah= '" & cbSatuan.Text & "', id_produksi= '" & tbJumlah.Text & "', tgl_produksi= '" & tbHarga.Text & "' where id_barang='" & tbKodeBarang.Text & "'"
            cmd = New OleDbCommand(sql, conn)
            cmd.ExecuteNonQuery()
            dgvDataBarang.Refresh()
            Me.OleDbConnection1.Close()
            Call TextMati()
            Call Kosong()
            Me.btnTambah.Enabled = True
            Me.btnSimpan.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnUpdate.Enabled = False
            Me.btnBatal.Enabled = False
            Me.btnHapus.Enabled = False
            Me.btnKeluar.Enabled = True
            dgvDataBarang.Refresh()
            Call TampilGrid()
        End If
    End Sub

    Private Sub tbKodeBarang_TextChanged(sender As Object, e As EventArgs) Handles tbKodeBarang.TextChanged

    End Sub

    Private Sub tbKodeBarang_LostFocus(sender As Object, e As EventArgs) Handles tbKodeBarang.LostFocus
        str = "SELECT * FROM tbbarang Where id_barang = '" & tbKodeBarang.Text & "'"
        cmd = New OleDbCommand(str, conn)
        rd = cmd.ExecuteReader
        Try
            While rd.Read
                tbNamaBarang.Text = rd.GetString(1)
                cbSatuan.Text = rd.GetString(2)
                tbJumlah.Text = rd.GetValue(3)
                tbHarga.Text = rd.GetValue(4)
                TextMati()
                Me.btnTambah.Enabled = False
                Me.btnSimpan.Enabled = False
                Me.btnEdit.Enabled = True
                Me.btnUpdate.Enabled = False
                Me.btnBatal.Enabled = False
                Me.btnHapus.Enabled = False
                Me.btnKeluar.Enabled = True
            End While
        Finally
            rd.Close()
        End Try
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Call Kosong()
        Call TextMati()
        Me.btnTambah.Enabled = True
        Me.btnSimpan.Enabled = False
        Me.btnEdit.Enabled = False
        Me.btnUpdate.Enabled = False
        Me.btnBatal.Enabled = False
        Me.btnHapus.Enabled = False
        Me.btnKeluar.Enabled = True
    End Sub

    Private Sub tbKodeBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbKodeBarang.KeyPress
        tbKodeBarang.MaxLength = 6
        If e.KeyChar = Chr(13) Then tbNamaBarang.Focus()
    End Sub

    Private Sub tbNamaBarang_TextChanged(sender As Object, e As EventArgs) Handles tbNamaBarang.TextChanged

    End Sub

    Private Sub tbNamaBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbNamaBarang.KeyPress
        tbNamaBarang.MaxLength = 25
        If e.KeyChar = Chr(13) Then cbSatuan.Focus()
    End Sub

    Private Sub cbSatuan_TextChanged(sender As Object, e As EventArgs) Handles cbSatuan.TextChanged

    End Sub

    Private Sub cbSatuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbSatuan.KeyPress
        cbSatuan.MaxLength = 10
        If e.KeyChar = Chr(13) Then tbJumlah.Focus()
    End Sub

    Private Sub tbJumlah_TextChanged(sender As Object, e As EventArgs) Handles tbJumlah.TextChanged

    End Sub

    Private Sub tbJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbJumlah.KeyPress
        If e.KeyChar = Chr(13) Then tbHarga.Focus()
    End Sub

    Private Sub tbHarga_TextChanged(sender As Object, e As EventArgs) Handles tbHarga.TextChanged

    End Sub

    Private Sub tbHarga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbHarga.KeyPress
        If e.KeyChar = Chr(13) Then btnSimpan.Focus()
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If tbKodeBarang.Text = "" Then
            MsgBox("Kode belum diisi")
            tbKodeBarang.Focus()
            Exit Sub
        Else
            If MessageBox.Show("Yakin akan dihapus..?", "",
           MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim hapus As String = "Delete * from tbproduksi where id_barang='" & tbKodeBarang.Text & "'"
                cmd = New OleDbCommand(hapus, conn)
                cmd.ExecuteNonQuery()
                Call TampilGrid()
                Call Kosong()
                Me.btnTambah.Enabled = True
                Me.btnSimpan.Enabled = False
                Me.btnEdit.Enabled = False
                Me.btnUpdate.Enabled = False
                Me.btnBatal.Enabled = False
                Me.btnHapus.Enabled = False
                Me.btnKeluar.Enabled = True
            Else
                Call TextMati()
            End If
        End If
    End Sub
End Class