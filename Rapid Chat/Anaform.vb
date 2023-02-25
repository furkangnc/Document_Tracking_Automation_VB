Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data.Common
Imports System.Data
Imports System.Xml
Imports System.IO




Public Class Anaform
    Dim baglanti, baglanti1, sorgu, sorgu1, yol, sql As String 'çeşitli yerlerde kullanılmak üzere bağlantı nesneleri oluşturulmuşi
    Public baglan As New OleDbConnection("Provider = Microsoft.JET.OLEDB.4.0;data source=" & Application.StartupPath & "\evraktakip.mdb")
    Public komut As OleDbCommand
    Dim komut1 As New OleDbCommand
    Public dr As OleDbDataReader
    Dim DosyaAdi, Dosyayolu As String
    Dim verial As OleDbDataAdapter
    Dim a1 As DataSet
    Dim baglan1 As OleDbConnection
    Dim verial1 As OleDbDataAdapter
    Dim a2 As DataSet
    Dim baglan2 As OleDbConnection



    Public Function veri_oku()
        'Datagridwiev1 doldurulma işlemi
        Dim adaptor As New OleDbDataAdapter("Select * from gelen", baglan)
        Dim ds As New DataSet
        Dim komut As New OleDbCommand
        adaptor.Fill(ds)
        DataGridView1.DataSource = (ds.Tables(0))
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).HeaderText = "Sıra No"
        DataGridView1.Columns(2).HeaderText = "Giriş Tarih"
        DataGridView1.Columns(3).HeaderText = "Giriş Saati"
        DataGridView1.Columns(4).HeaderText = "Cinsi"
        DataGridView1.Columns(5).HeaderText = "Eki"
        DataGridView1.Columns(6).HeaderText = "Geldiği Yer"
        DataGridView1.Columns(7).HeaderText = "Konu"
        DataGridView1.Columns(8).HeaderText = "Belge Tarihi"
        DataGridView1.Columns(9).HeaderText = "Sayı No"
        DataGridView1.Columns(10).HeaderText = "Nereye Ait"
    End Function

    Public Function veri_oku1()
        'Datagridwiev2nin doldurulma işlemi
        Dim adaptor As New OleDbDataAdapter("Select * from giden", baglan)
        Dim ds As New DataSet
        Dim komut As New OleDbCommand
        adaptor.Fill(ds)
        DataGridView2.DataSource = (ds.Tables(0))
        DataGridView2.Columns(0).Visible = False
        DataGridView2.Columns(1).HeaderText = "Sırano"
        DataGridView2.Columns(2).HeaderText = "Çıkış Tarihi"
        DataGridView2.Columns(3).HeaderText = "Çıkış Saati"
        DataGridView2.Columns(4).HeaderText = "Cinsi"
        DataGridView2.Columns(5).HeaderText = "Eki"
        DataGridView2.Columns(6).HeaderText = "Gittiği Yer"
        DataGridView2.Columns(7).HeaderText = "Konu"
        DataGridView2.Columns(8).HeaderText = "Belge Tarihi"
        DataGridView2.Columns(9).HeaderText = "Sayı No"
        DataGridView2.Columns(10).HeaderText = "Ait Olduğu Yer"
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        veri_oku() 'buton1e baglı 'Datagridwiev1 doldurulma işlemi
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        veri_oku1() 'buton2yee baglı 'Datagridwiev2 doldurulma işlemi

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles evrakkayitbtn.Click
        Gelenevrakkayit.Show() 'gelen evrak sayfasını aç

    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Gidenevrakkayit.Show() 'gidenevrak sayfasını aç

    End Sub

    Public Sub okuyucu_oku()
        Dim ds As New DataSet

        'DataGridViewe Veritabanında Bulunan giden Tablosundan Verileri Çekiyoruz...
        If baglan.State = ConnectionState.Closed Then
            baglan.Open()
        End If
        ds.Clear()
        Dim adaptor = New OleDbDataAdapter("Select * From giden ", baglan)
        adaptor.Fill(ds, "giden")
        DataGridView2.DataSource = ds.Tables("giden")
        baglan.Close()
    End Sub
    Public gdurum As Integer

    Private Sub Button3_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        evrakgeldi.Show()
    End Sub


    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

        veri_oku() 'tab page 1 sayfasını dolldurma işmeli (datagridvievi)
    End Sub

    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click

        veri_oku1() 'tab page 2 sayfasını dolldurma işmeli (datagridvievi)
    End Sub
    
    Private Sub Anaform_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        ComboBox1.Items.Clear() 'comboboxun içini temizler
        ComboBox1.Text = ""

        ComboBox1.Items.Add("Sıra Numarasına Göre") 'comboboxun içini doldurur
        ComboBox1.Items.Add("Belge Tarihine Göre")
        ComboBox2.Items.Clear()
        ComboBox2.Text = ""

        ComboBox2.Items.Add("Sıra Numarasına Göre")
        ComboBox2.Items.Add("Belge Tarihine Göre")
        okuyucu_oku()
        veri_oku1() 'datalistview 1i doldurur
        veri_oku() 'datalistview 2yi doldurur
        evrakgeldi.Show()
        Button1.Visible = False
        Button2.Visible = False
    End Sub


    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        'aramakriterleri textbox1e harfe basıldığında şı işlemi yap
        'comboboxlardan seçilme drumuna göre arama 
        Dim ds As New DataSet
        If ComboBox1.Text = "" Then

            MessageBox.Show("Öncelikle Arama Türü Seçmelisiniz...", "Evrak Takip Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Else

            If DataGridView2.Visible = True Then

                If ComboBox1.Text = "Sıra Numarasına Göre" Then
                    If baglan.State = ConnectionState.Closed Then
                        baglan.Open()
                    End If
                    ds.Clear()
                    Dim adaptor = New OleDbDataAdapter("Select * from giden where sirano Like'" + TextBox1.Text & "%'", baglan)
                    adaptor.Fill(ds, "giden")
                    DataGridView2.DataSource = ds.Tables("giden")
                    baglan.Close()
                Else
                End If
                If ComboBox1.Text = "Belge Tarihine Göre" Then
                    If baglan.State = ConnectionState.Closed Then
                        baglan.Open()
                    End If
                    ds.Clear()
                    Dim adaptor = New OleDbDataAdapter("Select * from giden where belgetarihi Like'" + TextBox1.Text & "%'", baglan)
                    adaptor.Fill(ds, "giden")
                    DataGridView2.DataSource = ds.Tables("giden")
                    baglan.Close()
                Else
                End If
            End If
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        yedekle.Show() 'yedekle sayfasını açar
    End Sub


    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        'aramakriterleri textbox1e harfe basıldığında şı işlemi yap
        'comboboxlardan seçilme drumuna göre arama
        Dim ds As New DataSet
        If ComboBox2.Text = "" Then

            MessageBox.Show("Öncelikle Arama Türü Seçmelisiniz...", "Evrak Takip Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Else

            If DataGridView1.Visible = True Then

                If ComboBox2.Text = "Sıra Numarasına Göre" Then
                    If baglan.State = ConnectionState.Closed Then
                        baglan.Open()
                    End If
                    ds.Clear()
                    Dim adaptor = New OleDbDataAdapter("Select * from gelen where sirano Like'" + TextBox2.Text & "%'", baglan)
                    adaptor.Fill(ds, "gelen")
                    DataGridView1.DataSource = ds.Tables("gelen")
                    baglan.Close()
                Else
                End If
                If ComboBox2.Text = "Belge Tarihine Göre" Then
                    If baglan.State = ConnectionState.Closed Then
                        baglan.Open()
                    End If
                    ds.Clear()
                    Dim adaptor = New OleDbDataAdapter("Select * from gelen where belgetarihi Like'" + TextBox2.Text & "%'", baglan)
                    adaptor.Fill(ds, "gelen")
                    DataGridView1.DataSource = ds.Tables("gelen")
                    baglan.Close()
                Else
                End If
            End If
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If Button1.Visible = True Then 'buton1 görünüyosa buton1 ve 2 yi gizle 
            Button1.Visible = False
            Button2.Visible = False
        Else
            Button1.Visible = True 'görünmüyorsa gizliliği kaldır
            Button2.Visible = True
        End If


    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        istek.Show() 'istek sayfasını aç
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Application.Exit() 'çıkış komutu
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' Gelenevrakkayit evrakları raporlama işleminin yapıldığı yer ve gösterilmesini sağlayan komut
        baglanti = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\evraktakip.mdb"
        sorgu = "Select * from gelen"
        baglan1 = New OleDbConnection(baglanti)
        verial = New OleDbDataAdapter(sorgu, baglan1)
        a1 = New DataSet
        If (verial.Fill(a1, "gelen") = False) Then
            MessageBox.Show("Böyle Bir Kayıt Yok")
            Me.Hide()
        Else
            Dim rapor As New GelenRapor1
            rapor.SetDataSource(a1)
            gelenraporlama.CrystalReportViewer1.ReportSource = rapor
            gelenraporlama.Show()
            baglan1.Close()
        End If

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' giden evrakların raporlama işleminin yapıldığı yer ve gösterilmesini sağlayan kom
        baglanti = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\evraktakip.mdb"
        sorgu = "Select * from giden"
        baglan2 = New OleDbConnection(baglanti)
        verial1 = New OleDbDataAdapter(sorgu, baglan2)
        a2 = New DataSet
        If (verial1.Fill(a2, "giden") = False) Then
            MessageBox.Show("Böyle Bir Kayıt Yok")
            Me.Hide()
        Else
            Dim rapor1 As New GidenRapor1
            rapor1.SetDataSource(a2)
            gidenraporlama.CrystalReportViewer1.ReportSource = rapor1
            gidenraporlama.Show()
            baglan2.Close()
        End If

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Form1.Show()
    End Sub
End Class