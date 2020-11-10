Public Class ViewSuppliesPayment
    Private Sub ViewSuppliesPay()
        Try
            opencon()
            Dim cnt As Integer = 0
            lvSuppliesPay.Items.Clear()
            With Cmd
                .CommandText = "SELECT d.DRNo, d.ORNo, d.SupprID, d.Comments, d.Cash, d.Check, d.Date, CONCAT(e.LName,', ',e.FName,' ',e.MName,'.') FROM supplies_payment d, employee e WHERE d.EmpID = e.EmpID GROUP BY d.SupprID"
                Dr = .ExecuteReader

                While Dr.Read
                    lvSuppliesPay.Items.Add(Dr(0))
                    For x As Integer = 1 To 7
                        lvSuppliesPay.Items(cnt).SubItems.Add(Dr(x))
                    Next
                    cnt += 1
                End While
                Dr.Close()
            End With

            closecon()
        Catch ex As Exception
            closecon()
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ViewSuppliesPayment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ViewSuppliesPay()
    End Sub
End Class