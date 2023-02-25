Imports System.Data
Imports System.Data.OleDb
Imports System.Xml
Imports System.IO
Public Class Giris
    Public Sub giris()
        'veritabanındaki kullanici adi şifre kısmına göre giriş yaplmasını sağlayan kısım
        Dim baglanti As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath.ToString() & "\evraktakip.mdb")
        Dim sorgu As New OleDbCommand("select * from yonetim Where kullaniciad=@Kullanici and sifre=@Sifre", baglanti)

        sorgu.Parameters.Add("@Kullanici", t1.Text) 'sorgu satırlarımı  t1 nesnesinin texti @kullanıcıya bağlama
        sorgu.Parameters.Add("@Sifre", t2.Text)
        baglanti.Open()
        Dim dr As OleDbDataReader = sorgu.ExecuteReader()
        If dr.Read() Then
            Me.Hide() 'kullanıcı adı şifre doğr ise şunları yap 
            Anaform.l8.Text = Me.t1.Text 'anaformun  label8 ini bu formun t1ine eşitle
            Anaform.Show()

        Else 'değilse uyar
            Label3.Text = "Yanlış kullanıcı adı veya şifre!"
        End If
    End Sub


    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

        giris() 'buton 2 ye basıldığında giriş fonsiyonunu çalıştır
    End Sub


    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Application.Exit() 'çıkış komutu

    End Sub

    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
        kullaniciekle.Show() 'kayıt olmak isteyen kullanıcıların kayıt kısmı
    End Sub

    Private Sub t2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles t2.KeyDown
        't2 nin  üzerinde entere basıldığında  aşağıdaki kodları çalıştırır
        If e.KeyData.ToString() = "Return" Then

            giris() 'giriş fonksiyonu
            Me.ActiveControl = Me.t1
            t1.Focus() 'odaklama işlemi


            t2.Clear() 'temizleme işlemi
        End If

    End Sub

    Private Sub t1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles t1.KeyDown
        't2 nin  üzerinde entere basıldığında  aşağıdaki kodları çalıştırır
        If e.KeyData.ToString() = "Return" Then

            giris() 'giriş fonksiyonu
            Me.ActiveControl = Me.t1
            t2.Focus() 'odaklama işlemi


            t2.Clear() 'temizleme işlemi
        End If
    End Sub

    Private Sub Giris_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
