Imports System.Data.OleDb
Public Class istek
    Dim baglan As New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" & Application.StartupPath & "\evraktakip.mdb") 'bağlntı nesnesi tanımlama
    Dim komut As New OleDbCommand
    Private Sub istek_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("Yönetici") 'comboboxar dolduruluyor
        ComboBox1.Items.Add("Kullanıcı")
        Dim adaptor As New OleDbDataAdapter("Select * from istek", baglan) 'datadridvieve görüntü çekme işlemi
        Dim ds As New DataSet
        Dim komut As New OleDbCommand
        adaptor.Fill(ds)
        DataGridView1.DataSource = (ds.Tables(0))
        DataGridView1.Columns(0).HeaderText = "Kullanıcı Adı" 'dtagridviev alan adları
        DataGridView1.Columns(1).HeaderText = "Şifre"
        DataGridView1.Columns(2).HeaderText = "İp Adresi"
        DataGridView1.Columns(3).HeaderText = "Bilgisayar Adı"
        DataGridView1.Columns(4).HeaderText = "Kuruluş Adı"
        DataGridView1.Columns(5).HeaderText = "Kurum Adı"


    End Sub


    Private Sub DataGridView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.Click

        TextBox1.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString() 'textboxların datagrid vieve tıklanmasına bağlı olarak doldurulması
        TextBox2.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
        TextBox3.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
        TextBox4.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
        TextBox5.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString()
        TextBox6.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        komut.Connection = baglan ' data gridviewdwn textboxlara aktarılan verinin kaydedilme işlemidir
        komut.CommandText = "insert into yonetim (kullaniciad,sifre,ipadresi,biladres,kurulusadi,kurumadi,rütbe) VALUES ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & ComboBox1.SelectedText & "')"
        baglan.Open()
        komut.ExecuteNonQuery()
        baglan.Close()
        MsgBox("Kayıt Tamamlandı..")
        temizle()
    End Sub
    Public Function temizle() 'text boxların temeizlenme olayı
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close() 'formu kapat
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        temizle() 'temizle olayını çağır
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim sorgu As String 'gelen isteği silme yani iptal etme
        baglan.Open()
        sorgu = "DELETE FROM istek WHERE ipadres = " & TextBox4.Text & " "
        Dim sil As OleDbCommand = New OleDbCommand(sorgu, baglan)
        sil.ExecuteNonQuery()
        baglan.Close()
    End Sub
End Class