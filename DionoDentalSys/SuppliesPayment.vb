Public Class SuppliesPayment

    Public Sub DisplaySupplier()
        Try
            opencon()
            cmbSupplier.Items.Clear()

            Cmd.CommandText = "SELECT SupprID FROM supplier"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                cmbSupplier.Items.Add(Dr(0))
            End While
            cmbSupplier.SelectedIndex = 0
            Dr.Close()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub SearchDRNo()
        Try
            opencon()
            lvDelivery.Items.Clear()
            With Cmd
                Dim y As Integer = 0
                .Parameters.Clear()
                .Parameters.AddWithValue("@drno", txtDRNo.Text)

                .CommandText = "SELECT d.SuppID, s.SuppDesc, d.Qty, d.ListPrice, (d.Qty * d.ListPrice), d.SupprID FROM delivery d, supplies s WHERE d.DRNo = @drno AND d.SuppID = s.SuppID"
                Dr = Cmd.ExecuteReader

                If Dr.HasRows Then
                    While Dr.Read
                        lvDelivery.Items.Add(Dr(0))
                        For x As Integer = 1 To 5
                            lvDelivery.Items(y).SubItems.Add(Dr(x))
                        Next
                        y += 1
                    End While
                End If
                Dr.Close()
            End With
            

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AddSuppliesPayment()
        Try
            Dim cnt As Integer = 0
            opencon()
            With Cmd.Parameters
                .Clear()
                .AddWithValue("@orno", txtORNo.Text)
                .AddWithValue("@drno", txtDRNo.Text)
                .AddWithValue("@suppr", cmbSupplier.SelectedItem)
                .AddWithValue("@comment", txtDesc.Text)
                If rdoCash.Checked = True Then
                    .AddWithValue("@cash", txtPayment.Text)
                    .AddWithValue("@check", 0)
                Else
                    .AddWithValue("@cash", 0)
                    .AddWithValue("@check", txtPayment.Text)
                End If
                .AddWithValue("@date", Now)
                .AddWithValue("@emp", global_empid)
            End With

            Cmd.CommandText = "INSERT INTO supplies_payment(ORNo, DRNo, SupprID, Comments, Cash, `Check`, `Date`, EmpID) VALUES(@orno, @drno, @suppr, @comment, @cash, @check, @date, @emp)"
            cnt = Cmd.ExecuteNonQuery()

            MsgBox(cnt & " record has been added.")
            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
   
    Private Sub SuppliesPayment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DisplaySupplier()
        suggestiontext("SELECT DISTINCT DRNo FROM delivery", txtDRNo)
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtDRNo.Text = ""
        txtORNo.Text = ""
        txtDesc.Text = ""
        txtPayment.Text = ""
        lvDelivery.Items.Clear()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        DisplaySupplier()
        txtDRNo.Focus()
        txtDRNo.SelectAll()
    End Sub

    Private Sub txtDRNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDRNo.KeyDown
        If e.KeyData = Keys.Enter Then
            If Not txtDRNo.Text = "" Then
                SearchDRNo()
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Not txtDesc.Text = "" And Not txtPayment.Text = "" Then
            AddSuppliesPayment()
        End If
    End Sub

    Private Sub rdoCash_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoCash.CheckedChanged
        If rdoCash.Checked = False Then
            rdoCheck.Checked = True
        End If
    End Sub

    Private Sub rdoCheck_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoCheck.CheckedChanged
        If rdoCheck.Checked = False Then
            rdoCash.Checked = True
        End If
    End Sub

    Private Sub txtPayment_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPayment.KeyPress
        NumberValidatewithDecimal(e, txtPayment)
    End Sub
    
    Private Sub btnViewAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewAll.Click
        With ViewSuppliesPayment
            .Show()
            .BringToFront()
            .Focus()
        End With
    End Sub
End Class