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
Imports System.Threading
Public Class evrakgeldi
    'farklı yerlerde klllanılmak üzere  veriler tanımlandı
    Dim okunan As String
    Dim ileti As Byte()
    Dim boyut As String
    Dim m As Integer
    Public dinleyici As TcpListener
    Dim port0 As Integer = 5050
    Dim port1 As Integer = 5055
    Dim ipadresim As IPAddress = IPAddress.Parse("79.123.155.120")
    Public thread1 As Thread
    Dim thread2 As Thread
    Public istemci As TcpClient
    Public Function doldurload()
        Try
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = False 'kanal kullanılırken hata oluşmaması için
            dinleyici = New TcpListener(port1)
            'port0 üzerinden dinleme yapılıyor
            dinleyici.Start()
            'dinleme başladı
            istemci = dinleyici.AcceptTcpClient()
            label1.Text = "istemci istek yolladı"
            'gelen bilginin aprçalarla alınması
            Dim bilgiokuyucu As New StreamReader(istemci.GetStream())
            okunan = bilgiokuyucu.ReadLine()
            boyut = okunan.Substring(okunan.LastIndexOf("."c) + 1)
            m = Integer.Parse(boyut)


            Dim veri As Stream = istemci.GetStream()
            ileti = New Byte(m - 1) {}
            veri.Read(ileti, 0, m)

            Dim dosyaismi As String
            Dim bas As Integer, son As Integer
            bas = okunan.LastIndexOf("\")
            son = okunan.LastIndexOf("."c)
            dosyaismi = okunan.Substring(bas + 1, son - bas - 1)

            Select Case File.Exists("C:\Dosyalar")
                'gelen verinin  cde dosyalar klasöründe tutulması kısmı eğer cde klasör yosa olusturur varsa bişe yapmaz kaydeder
                'bakınız bu bölümde aynı tarih isimli dosya var mı ona bakıyoruz.
                Case True

                Case False
                    My.Computer.FileSystem.CreateDirectory("C:\Dosyalar") 'C DE dosya oluşturma komutu
                Case Else
                    MsgBox("İşlemi tekrar deneyiniz.", MsgBoxStyle.Information, Me.Text)
            End Select
            textBox1.Text = "C:\Dosyalar\" & dosyaismi 'dosyamızın kaydedilme aşaması

            File.WriteAllBytes(textBox1.Text, ileti)

            dinleyici.[Stop]()
            istemci.Close()
            label1.Text = "dosya kaydedildi"
        Catch
            MessageBox.Show("Bir Hata Oluştu lütfen tekrar Deneyiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Function

    


    Private Sub evrakgeldi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' 2 formun aynı anda 2 işlemin aynı anda kllanılabilmesi için  kanallar belirlendi

        thread1 = New Thread(AddressOf doldurload)
        thread1.Start()

        Me.Close() ' bu formu kapat


    End Sub
   
    

End Class