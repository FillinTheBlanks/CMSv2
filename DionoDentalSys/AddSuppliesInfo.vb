Public Class AddSuppliesInfo
    Dim gldesc As String = ""
    Private Sub ViewSuppliesRecord()
        Try
            opencon()
            lvSupplies.Items.Clear()
            Dim y As Integer = 0
            Cmd.CommandText = "SELECT SuppID, SuppDesc, Qty, Unit FROM supplies WHERE Status = 1"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                lvSupplies.Items.Add(Dr(0))

                For x As Integer = 1 To 3
                    lvSupplies.Items(y).SubItems.Add(Dr(x))
                Next

                y += 1
            End While
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ViewSuppliesRecordwithDesc()
        Try
            opencon()
            lvSupplies.Items.Clear()
            Dim y As Integer = 0
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@desc", txtDesc1.Text & "%")
            Cmd.CommandText = "SELECT SuppID, SuppDesc, Qty, Unit FROM supplies WHERE SuppDesc LIKE @desc AND Status = 1"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                lvSupplies.Items.Add(Dr(0))

                For x As Integer = 1 To 3
                    lvSupplies.Items(y).SubItems.Add(Dr(x))
                Next

                y += 1
            End While
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AddSuppliesRecord()
        If Not txtDesc.Text = "" And Not txtUnit.Text = "" Then
            Try
                Dim booladd As Boolean = True
                Dim cnt As Integer = 0
                opencon()
                With Cmd.Parameters
                    .Clear()
                    .AddWithValue("@suppno", txtSuppNo.Text)
                    .AddWithValue("@desc", txtDesc.Text)
                    .AddWithValue("@qty", 0)
                    .AddWithValue("@unit", txtUnit.Text)
                    If txtSalesPrice.Text = "" Then
                        txtSalesPrice.Text = "0"
                    End If
                    .AddWithValue("@price", txtSalesPrice.Text)
                    .AddWithValue("@stat", 1)
                End With

                Cmd.CommandText = "SELECT * FROM supplies WHERE SuppDesc = @desc AND Unit = @unit AND SalesPrice = @price AND Status = 0"
                Dr = Cmd.ExecuteReader

                If Dr.Read Then
                    booladd = False
                    With Cmd.Parameters
                        .Clear()
                        .AddWithValue("@suppno", Dr(0))
                    End With

                End If

                Dr.Close()

                If booladd = True Then
                    Cmd.CommandText = "INSERT INTO supplies VALUES(@suppno, @desc, @qty, @unit, @price, @stat)"
                Else
                    Cmd.CommandText = "UPDATE supplies SET Status = 1 WHERE SuppID = @suppno"
                End If
                cnt = Cmd.ExecuteNonQuery()
                MsgBox(cnt & " supplies has been added.")
                closecon()
                gldesc = ""
                AddPatientCharges.txtChargeDesc.AutoCompleteCustomSource.Add(txtDesc.Text)

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub SelectItemonSupplies()
        If lvSupplies.SelectedItems.Count > 0 Then
            Try
                opencon()
                With Cmd.Parameters
                    .Clear()
                    .AddWithValue("@suppno", lvSupplies.SelectedItems.Item(0).Text)
                End With

                Cmd.CommandText = "SELECT SalesPrice FROM supplies WHERE SuppID = @suppno"
                Dr = Cmd.ExecuteReader

                txtSuppNo.Text = lvSupplies.SelectedItems.Item(0).Text
                txtDesc.Text = lvSupplies.SelectedItems.Item(0).SubItems(1).Text
                txtUnit.Text = lvSupplies.SelectedItems.Item(0).SubItems(3).Text
                gldesc = lvSupplies.SelectedItems.Item(0).SubItems(1).Text
                If Dr.Read Then
                    txtSalesPrice.Text = Dr(0).ToString
                End If

                Dr.Close()
                closecon()

                btnAdd.Enabled = False
                btnUpdate.Enabled = True
                btnDelete.Enabled = True

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub UpdateSuppliesRecord()
        If Not txtSuppNo.Text = "" Then
            Try
                opencon()
                With Cmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@suppno", txtSuppNo.Text)
                    .Parameters.AddWithValue("@desc", txtDesc.Text)
                    .Parameters.AddWithValue("@unit", txtUnit.Text)
                    .Parameters.AddWithValue("@price", txtSalesPrice.Text)
                    .CommandText = "UPDATE supplies SET SuppDesc = @desc, Unit = @unit, SalesPrice = @price WHERE SuppID = @suppno"
                    Dim cnt As Integer = Cmd.ExecuteNonQuery()

                    MsgBox(cnt & " record has been updated.")
                End With
                closecon()

                AddPatientCharges.txtChargeDesc.AutoCompleteCustomSource.Remove(gldesc)
                AddPatientCharges.txtChargeDesc.AutoCompleteCustomSource.Add(txtDesc.Text)

                btnRefresh.PerformClick()
                btnClear.PerformClick()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub DeleteSuppliesRecord()
        If Not txtSuppNo.Text = "" Then
            Try
                opencon()
                With Cmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@suppno", txtSuppNo.Text)
                    .CommandText = "UPDATE supplies SET Status = 0 WHERE SuppID = @suppno"
                    Dim cnt As Integer = Cmd.ExecuteNonQuery()

                    MsgBox(cnt & " record has been deleted.")
                End With
                closecon()

                AddPatientCharges.txtChargeDesc.AutoCompleteCustomSource.Remove(gldesc)

                btnRefresh.PerformClick()
                btnClear.PerformClick()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        supplies_no()
        ViewSuppliesRecord()
        btnAdd.Enabled = True
        btnUpdate.Enabled = False
        btnDelete.Enabled = False
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSalesPrice.Text = ""
        txtSuppNo.Text = ""
        txtDesc.Text = ""
        txtUnit.Text = ""
        gldesc = ""
        ViewSuppliesRecord()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        AddSuppliesRecord()
        btnRefresh.PerformClick()
    End Sub

    Private Sub AddSuppliesInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        suggestiontext("SELECT SuppDesc FROM supplies", txtDesc)
    End Sub

    Private Sub txtDesc1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDesc1.KeyUp
        If Not txtDesc1.Text = "" Then
            ViewSuppliesRecordwithDesc()
        Else
            ViewSuppliesRecord()
        End If
    End Sub

    Private Sub lvSupplies_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvSupplies.MouseClick
        SelectItemonSupplies()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        UpdateSuppliesRecord()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteSuppliesRecord()
    End Sub

    Private Sub txtSalesPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSalesPrice.KeyPress
        NumberValidatewithDecimal(e, txtSalesPrice)
    End Sub
End Class