Imports Microsoft.Office.Interop.Word

Imports Word = Microsoft.Office.Interop.Word 'micrisof dosyaları için 
Imports System.IO


Public Class yenievrak

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim oWord As Word.Application 'vord dosyası tanımlama
        Dim oDoc As Word.Document 'döküman tanımlama
        Dim oPara1 As Word.Paragraph, oPara2 As Word.Paragraph 'paragraf tanımlama
        Dim oPara3 As Word.Paragraph, oPara4 As Word.Paragraph 'paragraf tanımlama
        Dim oRng As Word.Range 'satır tanımlama


        'Kelime başlatın ve belge şablonu açın.
        oWord = CreateObject("Word.Application")
        oWord.Visible = True
        oDoc = oWord.Documents.Add





        'Belgenin başlangıcında bir paragraf ekleme

        oPara1 = oDoc.Content.Paragraphs.Add 'paragraf ekle
        oPara1.Range.Font.Bold = True

        oPara1.Range.Text = TextBox4.Text
        oPara1.Format.SpaceAfter = 0   'Paragraftan sonra 24 pt aralık
        oPara1.Alignment = LeftRightAlignment.Right 'hizlama
        oPara1.Range.InsertParagraphAfter() 'satırdan sonra

        oPara1 = oDoc.Content.Paragraphs.Add
        oPara1.Range.Font.Bold = True

        oPara1.Range.Text = TextBox5.Text
        oPara1.Format.SpaceAfter = 0    'Paragraftan sonra 24 pt aralık
        oPara1.Alignment = LeftRightAlignment.Right 'hizlama
        oPara1.Range.InsertParagraphAfter() 'paragraftan sonra

        oPara1 = oDoc.Content.Paragraphs.Add 'paragraf ekle
        oPara1.Range.Font.Bold = True

        oPara1.Range.Text = TextBox6.Text
        oPara1.Format.SpaceAfter = 0    'Paragraftan sonra 24 pt aralık
        oPara1.Alignment = LeftRightAlignment.Right
        oPara1.Range.InsertParagraphAfter()

        oPara1 = oDoc.Content.Paragraphs.Add
        oPara1.Range.Font.Bold = True

        oPara1.Range.Text = TextBox7.Text
        oPara1.Format.SpaceAfter = 50    'Paragraftan sonra 24 pt aralık
        oPara1.Alignment = LeftRightAlignment.Right
        oPara1.Range.InsertParagraphAfter()


        'Belgenin sonunda bir paragraf ekleme
        'Belgenin sonunda önceden tanımlanmış bir yer işareti
        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        'sayı tarih saat alanı
        oPara2.Range.Text = TextBox2.Text + "                                                                                " + DateTimePicker1.Text
        oPara2.Format.SpaceAfter = 6
        oPara2.Alignment = LeftRightAlignment.Left
        oPara2.Range.InsertParagraphAfter()



        'Başka bir paragraf ekleme
        oPara3 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara3.Range.Text = "Konu :" + TextBox3.Text 'konu alanı
        oPara3.Range.Font.Bold = False
        oPara3.Format.SpaceAfter = 0
        oPara3.Range.InsertParagraphAfter()



        'Tablodan sonra bir metin ekleyin
        'oTable.Range.InsertParagraphAfter()
        oPara4 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara4.Range.InsertParagraphBefore()
        oPara4.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft
        'içerik alanı yazı kısmı
        oPara4.Range.Text = RichTextBox2.Text
        oPara4.Format.SpaceAfter = 6
        oPara4.Range.InsertParagraphAfter()

        'Belgenin sonunda bir paragraf ekleme
        'Belgenin sonunda önceden tanımlanmış bir yer işareti
        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        'mühür imza kısmı
        oPara2.Range.Text = TextBox8.Text
        oPara2.Format.SpaceAfter = 0
        oPara4.Format.Alignment = WdParagraphAlignment.wdAlignParagraphRight
        oPara2.Range.InsertParagraphAfter()

        'Belgenin sonunda bir paragraf ekleme
        'Belgenin sonunda önceden tanımlanmış bir yer işareti
        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        'adres kısmı
        oPara2.Range.Text = TextBox1.Text
        oPara2.Format.SpaceAfter = 24
        oPara2.Alignment = LeftRightAlignment.Left
        oPara2.Range.InsertParagraphAfter()


        ''Add text after the chart.
        oRng = oDoc.Bookmarks.Item("\endofdoc").Range
        'ek kısmı
        oRng.InsertAfter(TextBox10.Text)
        oRng.InsertParagraphAfter()
        Dim a
        a = Directory.GetCurrentDirectory
        oDoc.SaveAs(a & "\\raporlar\\ " + TextBox9.Text + ".docx") 'dosyayı kaydetme işlemi
        'All done. Close this form.
        Me.Close()

    End Sub

    
End Class