Public Class AboutUs

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Label3_KeyDown(sender As Object, e As KeyEventArgs) Handles Label3.KeyDown, Me.KeyDown, Label1.KeyDown, Label2.KeyDown, Label4.KeyDown, Label5.KeyDown,
            Label6.KeyDown, Label7.KeyDown, Label8.KeyDown, Label9.KeyDown
        If e.KeyData = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class