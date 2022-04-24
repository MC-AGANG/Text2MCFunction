Public Class Form1
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Form2.Show()
        Me.Visible = False
        Timer1.Enabled = False
    End Sub
End Class
