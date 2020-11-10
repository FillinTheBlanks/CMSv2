Public Class DeliveredSupplies
    Dim suppid As String = ""

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

    Private Sub SearchPatientwithLast(ByVal strdesc As String)
        Try

            Dim y As Integer = 0
            opencon()
            lvSearchSupplies.Items.Clear()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@pdesc", strdesc & "%")

            Cmd.CommandText = "SELECT SuppID, SuppDesc FROM supplies WHERE SuppDesc LIKE @pdesc"
            Dr = Cmd.ExecuteReader

            If Dr.HasRows Then
                While Dr.Read
                    lvSearchSupplies.Items.Add(Dr(0))
                    lvSearchSupplies.Items(y).SubItems.Add(Dr(1))
                    y += 1
                End While
                y = 23 * y
                With lvSearchSupplies
                    .Columns(0).Width = 114
                    .Columns(1).Width = 278
                    
                    .SetBounds(246, 260, 395, 26 + y)
                    .Visible = True
                    .Focus()
                    .TopItem.Selected = True
                End With
            Else
                lvSearchSupplies.Visible = False
                txtSuppDesc.Focus()
                txtSuppDesc.SelectAll()
            End If

            Dr.Close()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AddDeliveryRecord()
        Try
            If Not lvDelivery.Items.Count = 0 Then


                If txtDRNo.Text = "" Then
                    txtDRNo.Text = "0"
                End If
                opencon()
                Dim cnt, cntt, getqty, getnqty As Integer
                cntt = 0
                cnt = 0
                For x As Integer = 0 To lvDelivery.Items.Count - 1


                    With Cmd.Parameters
                        .Clear()

                        .AddWithValue("@drno", txtDRNo.Text)
                        .AddWithValue("@suppid", lvDelivery.Items(x).Text)
                        .AddWithValue("@qty", lvDelivery.Items(x).SubItems(2).Text)
                        .AddWithValue("@listprice", lvDelivery.Items(x).SubItems(3).Text)
                        .AddWithValue("@supprid", lvDelivery.Items(x).SubItems(5).Text)
                        .AddWithValue("@date", dtpDate.Value.ToString("yyyy-MM-dd"))
                        .AddWithValue("@empid", global_empid)
                        .AddWithValue("@status", "In")
                    End With

                    Cmd.CommandText = "INSERT INTO delivery(DRNo, SuppID, Qty, ListPrice, SupprID, Date, EmpID) VALUES(@drno, @suppid, @qty, @listprice, @supprid, @date, @empid)"
                    cnt = Cmd.ExecuteNonQuery()
                    cntt += cnt
                    Cmd.CommandText = "INSERT INTO stock_trans(SuppID, Qty, Status, EmpID, Date) VALUES(@suppid, @qty, @status, @empid, @date)"
                    Cmd.ExecuteNonQuery()

                    Cmd.CommandText = "SELECT Qty FROM supplies WHERE SuppID=@suppid"
                    Dr = Cmd.ExecuteReader

                    If Dr.Read Then
                        getqty = Dr(0)
                    End If
                    Dr.Close()
                    getnqty = Integer.Parse(lvDelivery.Items(x).SubItems(2).Text)
                    getnqty += getqty

                    Cmd.Parameters.AddWithValue("@nqty", getnqty)
                    Cmd.CommandText = "UPDATE supplies SET Qty=@nqty WHERE SuppID=@suppid"
                    Cmd.ExecuteNonQuery()

                Next
                closecon()
                MsgBox(cntt & " record has been added.")
                btnPay.Enabled = True

                SuppliesPayment.txtDRNo.AutoCompleteCustomSource.Add(txtDRNo.Text)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ComputeTotalAmount()
        Dim totamt, dAmt As Double
        totamt = 0

        For x As Integer = 0 To lvDelivery.Items.Count - 1
            dAmt = Double.Parse(lvDelivery.Items(x).SubItems(4).Text)
            totamt = totamt + dAmt
        Next

        txtTotalAmt.Text = totamt.ToString("0.00")
    End Sub

    Private Sub txtDRNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDRNo.KeyPress
        NumberValidate10(e, txtDRNo)
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress, txtTotalAmt.KeyPress
        NumberValidate(e, txtQty)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        DisplaySupplier()
        btnPay.Enabled = False
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtDRNo.Text = ""
        txtSuppDesc.Text = ""
        txtQty.Text = "0"
        txtAmount.Text = "0.00"
        txtDRNo.Focus()
        suppid = ""
        lvDelivery.Items.Clear()

    End Sub

    Private Sub txtAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown
        If e.KeyData = Keys.Enter Then
            btnAddCart.PerformClick()
        End If
    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        NumberValidatewithDecimal(e, txtAmount)
    End Sub

    Private Sub DeliveredSupplies_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lvSearchSupplies_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvSearchSupplies.KeyDown
        If e.KeyData = Keys.Enter Then
            suppid = lvSearchSupplies.SelectedItems.Item(0).Text
            txtSuppDesc.Text = lvSearchSupplies.SelectedItems.Item(0).SubItems(1).Text

            lvSearchSupplies.Visible = False
        End If
    End Sub

    Private Sub lvSearchSupplies_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvSearchSupplies.LostFocus
        lvSearchSupplies.Visible = False
    End Sub

    Private Sub txtSuppDesc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSuppDesc.KeyDown
        If e.KeyData = Keys.Enter Then
            SearchPatientwithLast(txtSuppDesc.Text)
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Not txtSuppDesc.Text = "" Then
            AddDeliveryRecord()
        End If
    End Sub

    Private Sub btnAddCart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCart.Click
        If Not txtSuppDesc.Text = "" And (Not txtQty.Text = "" Or Not txtQty.Text = "0") And (Not txtAmount.Text = "" Or Not txtAmount.Text = "0.00" Or Not txtAmount.Text = "0") Then
            Dim itemcnt As Integer = lvDelivery.Items.Count
            Dim nrmal As Boolean = False
            Dim iQty, iQty1 As Integer
            Dim dAmt, dAmt1 As Double

            For x As Integer = 0 To itemcnt - 1
                If suppid = lvDelivery.Items(x).Text And txtAmount.Text = lvDelivery.Items(x).SubItems(3).Text Then

                    iQty = Integer.Parse(txtQty.Text)
                    iQty1 = Integer.Parse(lvDelivery.Items(x).SubItems(2).Text)
                    dAmt = Double.Parse(txtAmount.Text)
                    iQty += iQty1
                    dAmt1 = iQty * dAmt
                    lvDelivery.Items(x).SubItems(2).Text = iQty.ToString
                    lvDelivery.Items(x).SubItems(4).Text = dAmt1.ToString
                    nrmal = True
                End If
            Next

            If nrmal = False Then
                lvDelivery.Items.Add(suppid)
                lvDelivery.Items(itemcnt).SubItems.Add(txtSuppDesc.Text)
                lvDelivery.Items(itemcnt).SubItems.Add(txtQty.Text)
                lvDelivery.Items(itemcnt).SubItems.Add(txtAmount.Text)
                iQty = Integer.Parse(txtQty.Text)
                dAmt = Double.Parse(txtAmount.Text)
                dAmt *= iQty
                lvDelivery.Items(itemcnt).SubItems.Add(dAmt.ToString)
                lvDelivery.Items(itemcnt).SubItems.Add(cmbSupplier.SelectedItem)
            End If



            ComputeTotalAmount()

            txtSuppDesc.Focus()
            txtSuppDesc.SelectAll()
        End If
    End Sub

    Private Sub btnPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPay.Click
        callforms(SuppliesPayment, MainModule.pnlMain)
        With SuppliesPayment
            .txtDRNo.Text = txtDRNo.Text
            .cmbSupplier.SelectedItem = cmbSupplier.SelectedItem
            .SearchDRNo()
            .txtORNo.Focus()
        End With
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Supplier.Show()
        Supplier.BringToFront()
    End Sub

    Private Sub lvDelivery_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvDelivery.MouseDoubleClick
        If lvDelivery.SelectedItems.Count > 0 Then
            If MsgBox("Remove this?", MsgBoxStyle.Critical + MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                lvDelivery.Items(lvDelivery.SelectedItems.Item(0).Index).Remove()
            End If
        End If
    End Sub
End Class