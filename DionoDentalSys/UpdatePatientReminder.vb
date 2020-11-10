Public Class UpdatePatientReminder
    Public visitdate As Date
    Public reminder As String
  
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
        Me.Close()
        GenReminderList.Focus()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If Not txtReminder.Text = "" Then
            Try
                opencon()
                With Cmd.Parameters
                    .Clear()
                    .AddWithValue("@accnt", txtAccnt.Text)
                    .AddWithValue("@vdate", visitdate)
                    .AddWithValue("@reminder", reminder)
                    .AddWithValue("@nvdate", dtpVisit.Value.ToString("yyyy-MM-dd"))
                    .AddWithValue("@nreminder", txtReminder.Text)
                    .AddWithValue("@adate", dtpADate.Value.ToString("yyyy-MM-dd"))
                    .AddWithValue("@atime", dtpATime.Value)
                    .AddWithValue("@empid", global_empid)
                    .AddWithValue("@date", Now)
                End With
                Cmd.CommandText = "UPDATE ortho_reminder SET PrevDate=@nvdate, Reminders=@nreminder, DateTrigger=@adate, TimeTrigger=@atime, EmpID=@empid, Date=@date WHERE PatientID=@accnt AND PrevDate=@vdate AND Reminders=@reminder"
                Dim cnt As Integer = Cmd.ExecuteNonQuery

                If cnt = 1 Then
                    MsgBox("Reminder Successfully Update.")
                    closecon()
                    GenReminderList.DisplayAllReminder()
                    Me.Close()
                    Me.Dispose()
                    GenReminderList.lvRemindList.Focus()
                ElseIf cnt = 0 Then
                    closecon()
                    MsgBox("Failed to Update.")
                End If

            Catch ex As Exception
                closecon()
                MsgBox(ex.ToString)
            End Try
        Else
            MsgBox("Please Add Information for Reminder Message.")
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MsgBox("Are you sure you want to delete this reminder?", MsgBoxStyle.Critical + MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Try
                opencon()
                With Cmd.Parameters
                    .Clear()
                    .AddWithValue("@accnt", txtAccnt.Text)
                End With
                Cmd.CommandText = "DELETE FROM ortho_reminder WHERE PatientID=@accnt"
                Dim cnt As Integer = Cmd.ExecuteNonQuery

                If cnt = 1 Then
                    MsgBox("Reminder Successfully Deleted.")
                    closecon()
                    GenReminderList.DisplayAllReminder()
                    Me.Close()
                    Me.Dispose()
                    GenReminderList.lvRemindList.Focus()
                ElseIf cnt = 0 Then
                    closecon()
                    MsgBox("Failed to Delete.")
                End If

            Catch ex As Exception
                closecon()
                MsgBox(ex.ToString)
            End Try
        Else
            MsgBox("Please Add Information for Reminder Message.")
        End If
    End Sub
End Class