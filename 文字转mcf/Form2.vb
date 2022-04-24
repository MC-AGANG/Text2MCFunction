Imports System.ComponentModel
Imports System.IO
Public Class Form2
	Dim firstload As Boolean = True
	Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
		Dim Installfont As New System.Drawing.Text.InstalledFontCollection
		For Each ff As FontFamily In Installfont.Families
			ComboBox1.Items.Add(ff.Name)
		Next
		ComboBox1.Text = "宋体"
		LinkLabel1.Text = Environment.CurrentDirectory
		Panel1.BackColor = Color.FromArgb(255, 255, 255)                               '程序依赖此bug运行，请勿删除
		Panel2.BackColor = Color.FromArgb(0, 0, 0)
	End Sub


	Private Function TextToBitmap2(text As String, f As Font, rect As Rectangle, fontcolor As Color, backColor As Color) As Bitmap
		Dim g As Graphics
		Dim bmp As Bitmap
		Dim format As New StringFormat(StringFormatFlags.NoClip)
		If rect = Rectangle.Empty Then
			bmp = New Bitmap(1, 1)
			g = Graphics.FromImage(bmp)
			Dim sizef As SizeF = g.MeasureString(text, f, PointF.Empty, format)

			Dim width As Integer = CInt(sizef.Width + 1)
			Dim height As Integer = CInt(sizef.Height + 1)
			rect = New Rectangle(0, 0, width, height)
			bmp.Dispose()

			bmp = New Bitmap(width, height)
		Else
			bmp = New Bitmap(rect.Width, rect.Height)
		End If

		g = Graphics.FromImage(bmp)
		g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
		g.FillRectangle(New SolidBrush(backColor), rect)
		g.DrawString(text, f, New SolidBrush(fontcolor), rect, format)
		Return bmp
	End Function

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Randomize()
		Dim pic As Bitmap
		Dim writer As StreamWriter
		Dim text(32767) As String
		Dim n As Integer = 0
		Dim m As Long = 1
		Dim px, py As Integer
		Dim command As String
		Dim fadetime As Integer = Val(TextBox8.Text) - Val(TextBox7.Text)
		Dim life As Integer = Val(TextBox8.Text)
		Dim md As Integer = TrackBar1.Value
		Dim pretime As Integer = Val(TextBox9.Text)
		Dim rx, ry As Single
		Dim term As Integer
		Do Until Mid(TextBox1.Text, m, 1) = ""
		If Mid(TextBox1.Text, m, 1) = vbLf Then
				n += 1
			Else
				text(n) += Mid(TextBox1.Text, m, 1)
			End If
			m += 1
		Loop
		For i = 0 To n
			writer = New StreamWriter(LinkLabel1.Text + "\line" + CStr(i + 1) + ".mcfunction")
			pic = TextToBitmap2(text(i), New Font(ComboBox1.Text, Val(TextBox2.Text)), Rectangle.Empty, Panel1.BackColor, Panel2.BackColor)
			If RadioButton3.Checked Then
				px = 0 : py = 0
			Else
				px = pic.Width \ 2 : py = pic.Height \ 2
			End If
			For x = 0 To pic.Width - 1
				For y = 0 To pic.Height - 1
					If pic.GetPixel(x, y) <> Panel2.BackColor Then
						rx = Rnd() * TrackBar2.Value / 20
						ry = Rnd() * TrackBar2.Value / 20
						term = Int(Rnd() * pretime)
						If RadioButton1.Checked And Not CheckBox2.Checked Then
							command = "particle soy:best 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((x - px) / md) + "," + CStr((x - px) / md) + "+" + CStr((x - px) / md / 10 + rx) + "*(t-" + CStr(fadetime) + ")))' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((py - y) / md) + "," + CStr((py - y) / md) + "+" + CStr((py - y) / md / 10 + ry) + "*(t-" + CStr(fadetime) + ")))' '0' " + CStr(life) + " 1 '" + CStr(Convert.ToInt32(Mid(ColorTranslator.ToHtml(Panel1.BackColor), 2), 16)) + "' " + TextBox3.Text + " " + TextBox4.Text + " " + TextBox5.Text + " 0 0 0 0 0 force"
						ElseIf RadioButton1.Checked And CheckBox2.Checked Then
							command = "particle soy:best 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((px - x) / md) + "," + CStr((px - x) / md) + "+" + CStr((px - x) / md / 10 + rx) + "*(t-" + CStr(fadetime) + ")))' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((py - y) / md) + "," + CStr((py - y) / md) + "+" + CStr((py - y) / md / 10 + ry) + "*(t-" + CStr(fadetime) + ")))' '0' " + CStr(life) + " 1 '" + CStr(Convert.ToInt32(Mid(ColorTranslator.ToHtml(Panel1.BackColor), 2), 16)) + "' " + TextBox3.Text + " " + TextBox4.Text + " " + TextBox5.Text + " 0 0 0 0 0 force"
						ElseIf RadioButton2.Checked And Not CheckBox2.Checked Then
							command = "particle soy:best '0' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((py - y) / md) + "," + CStr((py - y) / md) + "+" + CStr((py - y) / md / 10 + ry) + "*(t-" + CStr(fadetime) + ")))' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((x - px) / md) + "," + CStr((x - px) / md) + "+" + CStr((x - px) / md / 10 + rx) + "*(t-" + CStr(fadetime) + ")))' " + CStr(life) + " 1 '" + CStr(Convert.ToInt32(Mid(ColorTranslator.ToHtml(Panel1.BackColor), 2), 16)) + "' " + TextBox3.Text + " " + TextBox4.Text + " " + TextBox5.Text + " 0 0 0 0 0 force"
						Else
							command = "particle soy:best '0' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((py - y) / md) + "," + CStr((py - y) / md) + "+" + CStr((py - y) / md / 10 + ry) + "*(t-" + CStr(fadetime) + ")))' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((px - x) / md) + "," + CStr((px - x) / md) + "+" + CStr((px - x) / md / 10 + rx) + "*(t-" + CStr(fadetime) + ")))' " + CStr(life) + " 1 '" + CStr(Convert.ToInt32(Mid(ColorTranslator.ToHtml(Panel1.BackColor), 2), 16)) + "' " + TextBox3.Text + " " + TextBox4.Text + " " + TextBox5.Text + " 0 0 0 0 0 force"
						End If
						writer.WriteLine(command)
					ElseIf Not CheckBox1.Checked And pic.GetPixel(x, y) <> Panel2.BackColor Then
						rx = Rnd() * TrackBar2.Value / 20
						ry = Rnd() * TrackBar2.Value / 20
						term = Int(Rnd() * pretime)
						If RadioButton1.Checked And Not CheckBox2.Checked Then
							command = "particle soy:best 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((x - px) / md) + "," + CStr((x - px) / md) + "+" + CStr((x - px) / md / 10 + rx) + "*(t-" + CStr(fadetime) + ")))' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((py - y) / md) + "," + CStr((py - y) / md) + "+" + CStr((py - y) / md / 10 + ry) + "*(t-" + CStr(fadetime) + ")))' '0' " + CStr(life) + " 1 '" + CStr(Convert.ToInt32(Mid(ColorTranslator.ToHtml(Panel2.BackColor), 2), 16)) + "' " + TextBox3.Text + " " + TextBox4.Text + " " + TextBox5.Text + " 0 0 0 0 0 force"
						ElseIf RadioButton1.Checked And CheckBox2.Checked Then
							command = "particle soy:best 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((px - x) / md) + "," + CStr((px - x) / md) + "+" + CStr((px - x) / md / 10 + rx) + "*(t-" + CStr(fadetime) + ")))' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((py - y) / md) + "," + CStr((py - y) / md) + "+" + CStr((py - y) / md / 10 + ry) + "*(t-" + CStr(fadetime) + ")))' '0' " + CStr(life) + " 1 '" + CStr(Convert.ToInt32(Mid(ColorTranslator.ToHtml(Panel2.BackColor), 2), 16)) + "' " + TextBox3.Text + " " + TextBox4.Text + " " + TextBox5.Text + " 0 0 0 0 0 force"
						ElseIf RadioButton2.Checked And Not CheckBox2.Checked Then
							command = "particle soy:best '0' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((py - y) / md) + "," + CStr((py - y) / md) + "+" + CStr((py - y) / md / 10 + ry) + "*(t-" + CStr(fadetime) + ")))' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((x - px) / md) + "," + CStr((x - px) / md) + "+" + CStr((x - px) / md / 10 + rx) + "*(t-" + CStr(fadetime) + ")))' " + CStr(life) + " 1 '" + CStr(Convert.ToInt32(Mid(ColorTranslator.ToHtml(Panel2.BackColor), 2), 16)) + "' " + TextBox3.Text + " " + TextBox4.Text + " " + TextBox5.Text + " 0 0 0 0 0 force"
						Else
							command = "particle soy:best '0' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((py - y) / md) + "," + CStr((py - y) / md) + "+" + CStr((py - y) / md / 10 + ry) + "*(t-" + CStr(fadetime) + ")))' 'if(t<" + CStr(term) + ",0,if(t<" + CStr(fadetime) + "," + CStr((px - x) / md) + "," + CStr((px - x) / md) + "+" + CStr((px - x) / md / 10 + rx) + "*(t-" + CStr(fadetime) + ")))' " + CStr(life) + " 1 '" + CStr(Convert.ToInt32(Mid(ColorTranslator.ToHtml(Panel2.BackColor), 2), 16)) + "' " + TextBox3.Text + " " + TextBox4.Text + " " + TextBox5.Text + " 0 0 0 0 0 force"
						End If
						writer.WriteLine(command)
					End If
				Next
			Next
			writer.Flush()
			writer.Close()
		Next
	End Sub

	Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
		PictureBox1.Image = TextToBitmap2(TextBox1.Text, New Font(ComboBox1.Text, Val(TextBox2.Text)), Rectangle.Empty, Panel1.BackColor, Panel2.BackColor)
	End Sub

	Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
		If TextBox2.Text <> "" Then
			PictureBox1.Image = TextToBitmap2(TextBox1.Text, New Font(ComboBox1.Text, Val(TextBox2.Text)), Rectangle.Empty, Panel1.BackColor, Panel2.BackColor)
		End If
	End Sub

	Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles Panel1.Click
		If ColorDialog1.ShowDialog = DialogResult.OK Then
			Panel1.BackColor = ColorDialog1.Color
			PictureBox1.Image = TextToBitmap2(TextBox1.Text, New Font(ComboBox1.Text, Val(TextBox2.Text)), Rectangle.Empty, Panel1.BackColor, Panel2.BackColor)
		End If
	End Sub

	Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

	End Sub

	Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
		If CheckBox1.Checked Then
			Panel2.Enabled = False
			Panel2.BackColor = Color.Black
		Else
			Panel2.Enabled = True
		End If
	End Sub

	Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
		If ColorDialog1.ShowDialog = DialogResult.OK Then
			Panel2.BackColor = ColorDialog1.Color
			PictureBox1.Image = TextToBitmap2(TextBox1.Text, New Font(ComboBox1.Text, Val(TextBox2.Text)), Rectangle.Empty, Panel1.BackColor, Panel2.BackColor)
		End If
	End Sub

	Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
		If TextBox6.Text <> "" AndAlso Val(TextBox6.Text) > 0 AndAlso Val(TextBox6.Text) <= 32 Then
			TrackBar1.Value = Val(TextBox6.Text)
		End If
	End Sub

	Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
		TextBox6.Text = CStr(TrackBar1.Value)
	End Sub

	Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
		If firstload = False Then
			PictureBox1.Image = TextToBitmap2(TextBox1.Text, New Font(ComboBox1.Text, Val(TextBox2.Text)), Rectangle.Empty, Panel1.BackColor, Panel2.BackColor)
		End If
		firstload = False
	End Sub

	Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
		Process.Start("explorer.exe", LinkLabel1.Text)
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
			LinkLabel1.Text = FolderBrowserDialog1.SelectedPath
		End If
	End Sub

	Private Sub Form2_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
		Form1.Close()
	End Sub

	Private Sub Panel3_Click(sender As Object, e As EventArgs)
		If ColorDialog1.ShowDialog = DialogResult.OK Then
			Panel2.BackColor = ColorDialog1.Color
		End If
	End Sub
End Class