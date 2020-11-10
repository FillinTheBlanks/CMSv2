Public Class QtyBox

    Public Sub UpdateQtyofSupplies(ByVal formname As String)
        Try
            Dim prevqty, presqty As Integer
            presqty = Integer.Parse(txtQty.Text)
            opencon()


            With Cmd
                If formname = "ViewSupplies" Then
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@suppid", lbSelectItem.Text)

                    .CommandText = "SELECT Qty FROM supplies WHERE SuppID = @suppid"
                    Dr = .ExecuteReader

                    If Dr.Read Then
                        prevqty = Integer.Parse(Dr(0))
                    End If

                    Dr.Close()

                    If presqty > prevqty Then
                        MsgBox("The Qty is Greater than the Available Stocks.")
                        txtQty.Focus()
                        txtQty.SelectAll()
                        closecon()
                    Else
                        prevqty = prevqty - presqty
                        .Parameters.AddWithValue("@qty", prevqty)
                        .Parameters.AddWithValue("@qty1", presqty)
                        .Parameters.AddWithValue("@status", "Out")
                        .Parameters.AddWithValue("@emp", global_empid)
                        .Parameters.AddWithValue("@date", Now)

                        .CommandText = "UPDATE supplies SET Qty = @qty WHERE SuppID = @suppid"
                        .ExecuteNonQuery()

                        .CommandText = "INSERT INTO stock_trans(SuppID, Qty, Status, EmpID, Date) VALUES(@suppid, @qty1, @status, @emp, @date)"
                        .ExecuteNonQuery()

                        Me.Dispose()
                        Me.Close()

                        closecon()

                        With ViewSupplies
                            .ViewAvailableStocks()
                            .ViewStockOut()
                        End With
                    End If

                End If

            End With

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
       
    End Sub

    Private Sub txtQty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyDown
        If e.KeyData = Keys.Enter Then
            UpdateQtyofSupplies(lbFormName.Text)
        End If
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        NumberValidate(e, txtQty)
    End Sub

    Private Sub QtyBox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtQty.Text = "1"

    End Sub

    Private Sub QtyBox_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        Me.Dispose()
        Me.Close()

    End Sub
End Class