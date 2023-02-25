Imports System.Data.OleDb
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Public Class Gidenevrakkayit
    Dim ipm As String
    Dim pcadı As String
    Dim dosyaadı As String
    Dim ileti As Byte()
    Dim adresim As IPAddress = IPAddress.Parse("10.8.0.54")
    Dim baglan As New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" & Application.StartupPath & "\evraktakip.mdb")
    Dim komut As New OleDbCommand
    Dim sayi As String


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close() 'bu formu kapat
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ' Giden  evraklara ekleme  işlemi veri tabanına veri ekleme
        komut.Connection = baglan
        komut.CommandText = "insert into giden (sirano,cikistarihi,cikissaati,cinsi,eki,gittigiyer,konu,belgetarihi,sayino,nereyeait,adi) VALUES ('" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & DateTimePicker3.Text & "','" & TextBox4.Text & "','" & TextBox1.Text & "','" & ComboBox1.SelectedText & "','" & ComboBox2.SelectedText & "','" & DateTimePicker2.Text & "','" & TextBox5.Text & "','" & ComboBox3.SelectedText & "','" & TextBox6.Text & "')"
        baglan.Open()
        komut.ExecuteNonQuery()
        baglan.Close()

        MsgBox("Kayıt Tamamlandı..")


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'buformdaki nesnelerimizi başka formda kullanıyoroz..
        yenievrak.TextBox10.Text = Me.TextBox1.Text
        yenievrak.TextBox3.Text = Me.ComboBox2.Text
        yenievrak.TextBox2.Text = Me.TextBox2.Text
        yenievrak.TextBox9.Text = Me.TextBox6.Text
        yenievrak.Show() 'yeni evrak saayfasını aç
    End Sub
    Public Function doldur()

        Try
            ipm = TextBox7.Text 'verinin gönderime işlemini kapsar
            pcadı = TextBox8.Text

            Dim adresim As IPAddress = IPAddress.Parse(ipm)
            'istemci tanımlanıyor
            OpenFileDialog1.ShowDialog()
            TextBox3.Text = OpenFileDialog1.FileName
            Dim ad As String = OpenFileDialog1.FileName
            Dim dosyabilgisi As New FileInfo(ad)
            'dosya bilgisi alınıyor
            dosyaadı = (ad & ".") & dosyabilgisi.Length
            'dosyaadı oluşturuluyor

            Dim istemci As New TcpClient(pcadı, 5055)
            Dim dosyayazıcı As New StreamWriter(istemci.GetStream())
            dosyayazıcı.WriteLine(dosyaadı)
            'dosya yazılıyor
            dosyayazıcı.Flush()

            'Dim istemci As New TcpClient("casper-pc", 5055)
            Dim bilgi As Stream = istemci.GetStream()
            ileti = File.ReadAllBytes(OpenFileDialog1.FileName)
            'dosya byte lara çevriliyor
            bilgi.Write(ileti, 0, ileti.Length)
            istemci.Close()
        Catch
            MessageBox.Show("Bir Hata Oluştu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Function

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        doldur() 'dosya göderme sonksiyonu çağrılıyor
    End Sub

    Public Function combosıfırla1()
        '!comboboxları temizleyip içlerini  farklı tablolardan doldurur
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()
        ComboBox3.Items.Clear()
        Dim oku1 As OleDbDataReader
        baglan.Open()
        komut.Connection = baglan
        komut.CommandText = "select distinct konu,nereyeait,gittigiyer,yonetim.kurulusadi from giden,yonetim"
        oku1 = komut.ExecuteReader()
        While oku1.Read()
            ComboBox3.Items.Add(oku1(2))
            ComboBox2.Items.Add(oku1(0))
            ComboBox1.Items.Add(oku1(1))
            ComboBox4.Items.Add(oku1(3))

        End While
        baglan.Close()


    End Function

    Private Sub Gidenevrakkayit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' veri gönderme işlemin kolaylaşması için ip otomatik seçme 
        ipm = TextBox7.Text
        pcadı = TextBox8.Text
        combosıfırla1() 'combobozları formonloadında temizleyip doldurma
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        'textboxların veritabanına göre doldurulma işlemi çomboboxtan bir vri seçidiğinde o verinin kayıt indexlerine göre bilgi  çağırır
        komut.Connection = baglan
        komut.CommandText = " select * from yonetim Where kurulusadi  = '" & Convert.ToString(ComboBox4.Text) & "'"
        baglan.Open()
        Dim oku As OleDbDataReader = komut.ExecuteReader()
        If oku.Read() Then
            TextBox7.Text = oku.GetString(7)
            TextBox8.Text = oku.GetString(8)
        End If
        baglan.Close()
    End Sub
End Class
