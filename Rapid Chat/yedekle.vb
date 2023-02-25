Imports System.IO
Public Class yedekle

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'veritabanı yedekle
        Dim zaman As New Date  'burada zaman değişkenimizi atadık
        Dim uzanti As String 'burada ise  uzantısını atadık "Kayıt.mdb" gibi
        zaman = DateTime.Today 'zaman isimli değişkenimize "today" ile bugünün tarihini verdik
        uzanti = (".mdb") 'uzanti isimli değişkenimizde ise Uzantımızı 
        Select Case File.Exists("D:\Evrak Takip Yedek\")
            Case True
            Case False
                My.Computer.FileSystem.CreateDirectory("D:\Evrak Takip Yedek")
        End Select
        Select Case File.Exists("D:\Evrak Takip Yedek\" & (zaman) & (uzanti))

            'bakınız bu bölümde aynı tarih isimli dosya var mı ona bakıyoruz.
            Case True
                MsgBox("Bugün Zaten yedek Alınmış", MsgBoxStyle.Information, "Dikkat !")
                'eğer aynı gün aldığımız yedek varsa yedeği tanıdı ve uyarı verdi
            Case False
                'bu bölümde ise eğer aynı gün yedek yoksa dosyayı kopyalayacak.
                FileCopy("evraktakip.mdb", "D:\Evrak Takip Yedek\veritabanı" & Path.GetFileName(zaman) & uzanti)
                'buradaki "zaman" ve "uzanti" değişkenlerine dikkat edin zaman dosya adını değiştirdi.
                'uzantı ise dosya uzantımızı atadı.
                MsgBox("Veritabanı Yedekleme İşlemi Tamamlanmıştır, Veritabanı D:\Evrak Takip Yedek\veritabanı Klasörünün İçindedir", MsgBoxStyle.Information, "İşlem")
                'işlem bitti ve dosyamız ydeklendi mesajını aldık.
            Case Else
                MsgBox("İşlemi tekrar deneyiniz.", MsgBoxStyle.Information, Me.Text)
        End Select
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        My.Computer.FileSystem.DeleteDirectory("D:\Evrak Takip Yedek", FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
        Select Case File.Exists("D:\Evrak Takip Yedek")
            'bakınız bu bölümde aynı tarih isimli dosya var mı ona bakıyoruz.
            Case True

            Case False
                My.Computer.FileSystem.CreateDirectory("D:\Evrak Takip Yedek")
            Case Else
                MsgBox("İşlemi tekrar deneyiniz.", MsgBoxStyle.Information, Me.Text)
        End Select
        My.Computer.FileSystem.CopyDirectory(Application.StartupPath & "\\raporlar ", "D:\Evrak Takip Yedek\raporlar")
        My.Computer.FileSystem.CopyDirectory(Application.StartupPath & "\\resimler ", "D:\Evrak Takip Yedek\resimler")
        MsgBox("Yedek Alındı.")

    End Sub

    Private Sub yedekle_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class