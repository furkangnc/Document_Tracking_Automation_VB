Imports System.Data.OleDb
Imports System.Data
Public Class Gelenevrakkayit
    Dim baglan As New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" & Application.StartupPath & "\evraktakip.mdb")
    Dim komut As New OleDbCommand 'bağlantı tanımlamaları

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close() 'formu kapat
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'gelen veri tabanına ekleme yapma işlemi'
        baglan.Close()
        komut.Connection = baglan
        komut.CommandText = "insert into gelen (sirano,giristarihi,girissaati,cinsi,eki,geldigiyer,konu,belgetarihi,sayino,nereyeait,adi) VALUES ('" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & DateTimePicker3.Text & "','" & TextBox4.Text & "','" & TextBox1.Text & "','" & ComboBox1.SelectedText & "','" & ComboBox2.SelectedText & "','" & DateTimePicker2.Text & "','" & TextBox5.Text & "','" & ComboBox3.SelectedText & "','" & TextBox6.Text & "')"
        baglan.Open()
        komut.ExecuteNonQuery()
        baglan.Close()
        MsgBox("Kayıt Tamamlandı")
    End Sub

    
   
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'bulununa konumdaki dosyayi  aç

        System.Diagnostics.Process.Start(Application.StartupPath & "\raporlar\" + TextBox6.Text + ".docx")
    End Sub
    Public Function combosıfırla()
        ComboBox1.Items.Clear() 'combobox olayları ve doldurma işlemleri
        ComboBox2.Items.Clear()
        ComboBox3.Items.Clear()
        Dim oku1 As OleDbDataReader
        baglan.Open()
        komut.Connection = baglan
        komut.CommandText = "select konu,nereyeait,geldigiyer from gelen"
        oku1 = komut.ExecuteReader()
        While oku1.Read()
            ComboBox3.Items.Add(oku1(2))
            ComboBox2.Items.Add(oku1(0))
            ComboBox1.Items.Add(oku1(1))
        End While
    End Function
    Private Sub Gelenevrakkayit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        combosıfırla()
    End Sub
End Class