Public Class ExpenseForm
   

    Private Sub btnBatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatch.Click
        If Not txtDetails.Text = "" And Not txtAmount.Text = "" And Not txtQty.Text = "" Then
            Dim cnt As Integer = lvBatch.Items.Count

            lvBatch.Items.Add(txtDetails.Text)
            lvBatch.Items(cnt).SubItems.Add(txtQty.Text)
            lvBatch.Items(cnt).SubItems.Add(txtAmount.Text)
            lvBatch.Items(cnt).SubItems.Add(dtpDate.Value.ToString("yyyy-MM-dd"))
        End If
    End Sub

    Private Sub txtAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown
        If e.KeyData = Keys.Enter Then
            btnBatch.PerformClick()
        End If
    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        NumberValidatewithDecimal(e, txtAmount)
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        NumberValidate(e, txtQty)
    End Sub

    
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim cntt, cnt As Integer
            cntt = 0
            opencon()
            If lvBatch.Items.Count > 0 Then
                For x As Integer = 0 To lvBatch.Items.Count - 1
                    With Cmd.Parameters
                        .Clear()
                        .AddWithValue("@detail", lvBatch.Items(x).Text)
                        .AddWithValue("@qty", lvBatch.Items(x).SubItems(1).Text)
                        .AddWithValue("@amt", lvBatch.Items(x).SubItems(2).Text)
                        .AddWithValue("@date", lvBatch.Items(x).SubItems(3).Text)
                        .AddWithValue("@empid", global_empid)
                    End With

                    Cmd.CommandText = "INSERT INTO expense(Details,Qty,Amount,Date,EmpID) VALUES(@detail,@qty,@amt,@date,@empid)"
                    cnt = Cmd.ExecuteNonQuery
                    cntt += cnt

                    Cmd.CommandText = "SELECT * FROM details WHERE Details = @detail"
                    Dr = Cmd.ExecuteReader

                    If Dr.Read Then
                        If Dr.HasRows = False Then
                            Dr.Close()

                            Cmd.CommandText = "INSERT INTO details VALUES(@detail)"
                            Cmd.ExecuteNonQuery()

                        End If
                    Else
                        Dr.Close()

                        Cmd.CommandText = "INSERT INTO details VALUES(@detail)"
                        Cmd.ExecuteNonQuery()
                    End If
                    Dr.Close()

                Next
                MsgBox(cntt & " record has been added.")
            Else
                If Not txtDetails.Text = "" And Not txtQty.Text = "" And Not txtAmount.Text = "" Then
                    With Cmd.Parameters
                        .Clear()
                        .AddWithValue("@detail", txtDetails.Text)
                        .AddWithValue("@qty", txtQty.Text)
                        .AddWithValue("@amt", txtAmount.Text)
                        .AddWithValue("@date", dtpDate.Value.ToString("yyyy-MM-dd"))
                        .AddWithValue("@empid", global_empid)
                    End With

                    Cmd.CommandText = "INSERT INTO expense(Details,Qty,Amount,Date,EmpID) VALUES(@detail,@qty,@amt,@date,@empid)"
                    cntt = Cmd.ExecuteNonQuery
                    MsgBox(cntt & " record has been added.")
                End If
            End If

            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        
    End Sub

    Private Sub ExpenseForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        suggestiontext("SELECT Details FROM details", txtDetails)
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtDetails.Text = ""
        txtQty.Text = "1"
        txtAmount.Text = "0.00"
        lvBatch.Items.Clear()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        txtDetails.Focus()
        txtDetails.SelectAll()
        btnUpdate.Enabled = False
        txtAmount.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtAmount.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.ExpenseForm_Load(sender, e)
    End Sub

End Class