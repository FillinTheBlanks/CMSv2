Public Class ReminderInfo
    Public reminderno As Integer = 0
    Public timetrig As TimeSpan

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            opencon()
            Dim cnt As Integer
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@no", reminderno)
            Cmd.CommandText = "UPDATE ortho_reminder SET Status = 1 WHERE No = @no"
            cnt = Cmd.ExecuteNonQuery()
            If cnt = 1 Then
                Me.Dispose()
                Me.Close()
            End If
            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnRemind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemind.Click
        Try
            opencon()
            
            Dim cnt As Integer
            timetrig += New TimeSpan(0, 1, 0, 0, 0)
            
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@no", reminderno)
            Cmd.Parameters.AddWithValue("@time", timetrig)
            Cmd.CommandText = "UPDATE ortho_reminder SET TimeTrigger = @time WHERE No = @no"
            cnt = Cmd.ExecuteNonQuery()
            If cnt = 1 Then
                Me.Dispose()
                Me.Close()
            End If
            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class