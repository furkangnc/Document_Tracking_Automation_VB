Imports System.Data.OleDb
Public Class kullaniciekle
    Dim baglan As New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" & Application.StartupPath & "\evraktakip.mdb")
    Dim komut As New OleDbCommand 'bağlantı komut tanımlamaları
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'kullanıcılardan gelen idtekleri  tutulduğu yer 
        'veri tabanındaki istek tablosuna ekleme ksmı
        komut.Connection = baglan
        komut.CommandText = "insert into istek (kullaniciadi,sifre,ipadresi,biladres,kurulusadi,kurumadi) VALUES ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "')"
        baglan.Open()
        komut.ExecuteNonQuery()
        baglan.Close()
        MsgBox("Kayıt Tamamlandı..")
        temizle() 'temizleme olayı
    End Sub
    Public Function temizle() 'temizleme fonsiyonu
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
    End Function
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        temizle() 'temizle fonsiyonu
    End Sub

    Private Sub kullaniciekle_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close() 'bu formu kapat
    End Sub
End Class