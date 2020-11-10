Public Class UpdateServiceRecord

    Private Sub UpdateServiceRecord_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        suggestiontext("SELECT PatientID FROM patient", txtAccnt)
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If MsgBox("Are you sure you want to update?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Try
                opencon()

                With Cmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@reqid", txtID.Text)
                    .Parameters.AddWithValue("@desc", txtDesc.Text)
                    .Parameters.AddWithValue("@accnt", txtAccnt.Text)
                    .Parameters.AddWithValue("@datec", dtpDate.Value.ToString("yyyy-MM-dd"))
                    .Parameters.AddWithValue("@amt", txtAmt.Text)
                    .Parameters.AddWithValue("@amtpd", txtAmtPaid.Text)

                    .CommandText = "UPDATE request SET PatientID=@accnt, Date=@datec, Amount=@amt WHERE ReqID=@reqid AND ServiceDesc=@desc"
                    .ExecuteNonQuery()

                    .CommandText = "SELECT * FROM payments WHERE ReqID=@reqid AND ServiceDesc=@desc AND PatientID=@accnt"
                    Dr = Cmd.ExecuteReader

                    If Dr.Read Then
                        If Dr.HasRows Then
                            Dr.Close()
                            .CommandText = "UPDATE payments SET PatientID=@accnt, Date=@datec, AmountPaid=@amtpd WHERE ReqID=@reqid AND ServiceDesc=@desc "
                            .ExecuteNonQuery()
                        End If
                    Else
                        Dr.Close()
                    End If
                    
                End With

                closecon()

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

            ViewServiceRecord.RefreshRecord()

            Me.Dispose()
            Me.Close()

        End If
    End Sub

    Private Sub txtAmtPaid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmtPaid.KeyPress
        NumberValidatewithDecimal(e, txtAmtPaid)
    End Sub
End Class