Public Class PassChanger
    Dim pass As String = ""

    Public Sub GetCurrentPassword(ByVal strpass As String)
        pass = strpass
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not txtCurrent.Text = "" And Not txtNew.Text = "" And Not txtReenter.Text = "" Then
            If txtNew.Text = txtReenter.Text Then
                If txtCurrent.Text = pass Then
                    Try
                        opencon()
                        With Cmd
                            .Parameters.Clear()
                            .Parameters.AddWithValue("@empid", global_empid)
                            .Parameters.AddWithValue("@newpass", txtNew.Text)
                            .CommandText = "UPDATE `employee` SET `PWord` = @newpass WHERE `EmpID` = @empid"
                            Dim cnt As Integer = .ExecuteNonQuery()
                            MsgBox(cnt & " new password has been updated.")
                        End With

                        closecon()
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                Else
                    MsgBox("Invalid current password.")
                    txtCurrent.Focus()
                    txtCurrent.SelectAll()
                End If
            Else
                MsgBox("Password not Matched.")
                txtNew.Focus()
                txtNew.SelectAll()
            End If
            
        End If
        
    End Sub

    Private Sub txtReenter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtReenter.KeyDown
        If e.KeyData = Keys.Enter Then
            btnSave.PerformClick()
        End If
    End Sub

    Private Sub PassChanger_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        Me.Dispose()
        Me.Close()
    End Sub
End Class