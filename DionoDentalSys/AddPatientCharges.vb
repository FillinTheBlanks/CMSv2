Public Class AddPatientCharges
    Dim WithEvents dt As New System.Data.DataTable()
    Dim ownername As String = ""
    Private Sub getOwnerName()
        Try
            opencon()
            Cmd.CommandText = "SELECT * FROM owner"
            Dr = Cmd.ExecuteReader
            If Dr.Read Then
                ownername = Dr(0)
            End If
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ReturnDefault()
        GenerateRequestNo()
        txtAccountNo.Text = ""
        txtAge.Text = ""
        txtAmount.Text = ""
        txtCash.Text = ""
        txtChange.Text = "0.00"
        txtChargeDesc.Text = ""
        txtDiagnosis.Text = ""
        txtFirst.Text = ""
        txtLast.Text = ""
        txtMI.Text = ""
        txtPercentage.Text = ""
        txtQty.Text = ""
        txtType.Text = ""
        lbSubTotal.Text = "0.00"
        lbTax.Text = "0.00"
        lbTotal.Text = "0.00"
        lvChargeList.Items.Clear()
        txtLast.Focus()
    End Sub

    Private Sub GenerateRequestNo()
        Try
            opencon()
            Dim reqid As Integer = 0

            Cmd.CommandText = "SELECT MAX(ReqID) FROM request"
            Dr = Cmd.ExecuteReader

            If Dr.HasRows = True Then
                If Dr.Read Then
                    reqid = Dr(0)
                    reqid += 1
                End If
            Else
                reqid = 100001
            End If
            Dr.Close()

            lbTransNo.Text = reqid.ToString

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AutoGetChangewhenInputtingCash(ByVal cash As String)
        If Not cash = "" Then
            Dim dtCash As Double = 0
            If lvChargeList.Items.Count > 0 Then
                For x As Integer = 0 To lvChargeList.Items.Count - 1
                    dtCash += Double.Parse(lvChargeList.Items(x).SubItems(5).Text)
                Next
            End If

            Dim dCash As Double = Double.Parse(cash) + dtCash
            Dim dTotal As Double = Double.Parse(lbTotal.Text)
            Dim change As Double

            change = dCash - dTotal
            txtChange.Text = change.ToString("0.00")
        ElseIf cash = "" Then
            txtChange.Text = lbTotal.Text
        End If

    End Sub

    Private Sub SearchPatientwithLast(ByVal strlast As String)
        Try

            Dim y As Integer = 0
            opencon()
            lvSearchPatient.Items.Clear()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@plast", strlast & "%")

            Cmd.CommandText = "SELECT p.PatientID, i.LName, i.FName, i.MName, i.Bday, p.Diagnosis, p.Type FROM patient p, patient_info i WHERE i.LName LIKE @plast AND p.InfoID = i.InfoID"
            Dr = Cmd.ExecuteReader

            If Dr.HasRows Then
                While Dr.Read
                    lvSearchPatient.Items.Add(Dr(0))
                    For x As Integer = 1 To 6
                        lvSearchPatient.Items(y).SubItems.Add(Dr(x))
                    Next
                    y += 1
                End While
                y = 23 * y
                With lvSearchPatient
                    .Columns(0).Width = 107
                    .Columns(1).Width = 119
                    .Columns(2).Width = 119
                    .Columns(3).Width = 60
                    .Columns(4).Width = 109
                    .SetBounds(0, -6, 521, 26 + y)

                    .Visible = True
                    .Focus()
                    .TopItem.Selected = True
                End With
            Else
                lvSearchPatient.Visible = False
                txtLast.Focus()
                txtLast.SelectAll()
            End If

            Dr.Close()
            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ComputeTotalAmount()
        Dim total As Double = 0
        Dim tax As Double = 0
        Dim subtotal As Double = 0
        For x As Integer = 0 To lvChargeList.Items.Count - 1
            total += lvChargeList.Items(x).SubItems(4).Text
        Next
        tax = total * 0.12
        subtotal = total - tax

        lbTax.Text = tax.ToString("#,000.00")
        lbSubTotal.Text = subtotal.ToString("#,000.00")
        lbTotal.Text = total.ToString("#,000.00")
    End Sub

    Private Sub PayService()
        Try
            Dim reqid As Integer = 0
            Dim y As Integer = 0
            Dim z As Integer = 0
            opencon()
            With Cmd


                For x As Integer = 0 To lvChargeList.Items.Count - 1
                    .CommandText = "SELECT MAX(No) FROM request"
                    Dr = .ExecuteReader

                    If Dr.HasRows = True Then
                        If Dr.Read Then
                            reqid = Dr(0)
                            reqid += 1
                        End If
                    End If
                    Dr.Close()

                    .Parameters.Clear()
                    .Parameters.AddWithValue("@no", reqid)
                    .Parameters.AddWithValue("@reqid", lbTransNo.Text)
                    .Parameters.AddWithValue("@pid", txtAccountNo.Text)
                    .Parameters.AddWithValue("@desc", lvChargeList.Items(x).SubItems(1).Text)
                    .Parameters.AddWithValue("@qty", lvChargeList.Items(x).SubItems(2).Text)
                    .Parameters.AddWithValue("@amt", lvChargeList.Items(x).SubItems(4).Text)
                    .Parameters.AddWithValue("@emp", global_empid)
                    .Parameters.AddWithValue("@date", dtpDateCharge.Value.ToString("yyyy-MM-dd"))
                    .Parameters.AddWithValue("@time", Now)
                    .Parameters.AddWithValue("@doctor", cmbDoctor.SelectedItem)
                    .CommandText = "INSERT INTO request(No, ReqID, PatientID, ServiceDesc, Qty, Amount, EmpID, Date, Time) VALUES(@no, @reqid, @pid, @desc, @qty, @amt, @emp, @date, @time)"
                    z = .ExecuteNonQuery()

                    If Not lvChargeList.Items(x).SubItems(5).Text = "0.00" Or Not lvChargeList.Items(x).SubItems(5).Text = "" Then
                        With Cmd
                            .Parameters.AddWithValue("@cash", lvChargeList.Items(x).SubItems(5).Text)
                            .CommandText = "INSERT INTO payments(ReqID, ServiceDesc, PatientID, AmountPaid, Doctor, EmpID, Date) VALUES(@reqid, @desc, @pid, @cash, @doctor, @emp, @date)"
                            .ExecuteNonQuery()

                        End With
                    End If

                    y += z
                Next
            End With

            closecon()
            MsgBox(y & " charge/s has been added.")
            btnPrint.Enabled = True
            'ReturnDefault()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub PayServicewithotherDoctor()
        Try
            Dim prcnt As Double = Double.Parse(txtPercentage.Text) / 100

            Dim reqid As Integer = 0
            Dim y As Integer = 0
            Dim z As Integer = 0
            opencon()
            With Cmd


                For x As Integer = 0 To lvChargeList.Items.Count - 1
                    .CommandText = "SELECT MAX(No) FROM request"
                    Dr = .ExecuteReader

                    If Dr.HasRows = True Then
                        If Dr.Read Then
                            reqid = Dr(0)
                            reqid += 1
                        End If
                    End If
                    Dr.Close()

                    .Parameters.Clear()
                    .Parameters.AddWithValue("@no", reqid)
                    .Parameters.AddWithValue("@reqid", lbTransNo.Text)
                    .Parameters.AddWithValue("@pid", txtAccountNo.Text)
                    .Parameters.AddWithValue("@desc", lvChargeList.Items(x).SubItems(1).Text)
                    .Parameters.AddWithValue("@qty", lvChargeList.Items(x).SubItems(2).Text)
                    .Parameters.AddWithValue("@amt", lvChargeList.Items(x).SubItems(4).Text)
                    .Parameters.AddWithValue("@emp", global_empid)
                    .Parameters.AddWithValue("@date", dtpDateCharge.Value.ToString("yyyy-MM-dd"))
                    .Parameters.AddWithValue("@time", Now)
                    .Parameters.AddWithValue("@doctor", cmbDoctor.SelectedItem)
                    .Parameters.AddWithValue("@owner", ownername)
                    .CommandText = "INSERT INTO request(No, ReqID, PatientID, ServiceDesc, Qty, Amount, EmpID, Date, Time) VALUES(@no, @reqid, @pid, @desc, @qty, @amt, @emp, @date, @time)"
                    z = .ExecuteNonQuery()

                    If Not lvChargeList.Items(x).SubItems(5).Text = "0.00" Or Not lvChargeList.Items(x).SubItems(5).Text = "" Then
                        With Cmd
                            Dim cash As Double = Double.Parse(lvChargeList.Items(x).SubItems(5).Text)
                            Dim getshare As Double = cash * prcnt

                            .Parameters.AddWithValue("@cash", getshare)
                            .CommandText = "INSERT INTO payments(ReqID, ServiceDesc, PatientID, AmountPaid, Doctor, EmpID, Date) VALUES(@reqid, @desc, @pid, @cash, @doctor, @emp, @date)"
                            .ExecuteNonQuery()

                            Dim getshareforowner As Double = cash - getshare
                            .Parameters.AddWithValue("@cashowner", getshareforowner)
                            .CommandText = "INSERT INTO payments(ReqID, ServiceDesc, PatientID, AmountPaid, Doctor, EmpID, Date) VALUES(@reqid, @desc, @pid, @cashowner, @owner, @emp, @date)"

                            .ExecuteNonQuery()

                        End With
                    End If

                    y += z
                Next
            End With



            closecon()
            MsgBox(y & " charge/s has been added.")
            btnPrint.Enabled = True
            'ReturnDefault()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub PrintCashInvoice()
        Dim bal, cash, total, change As Double
        opencon()
        Dim transno As Integer = Integer.Parse(lbTransNo.Text) - 1
        dt.Clear()
        dt.TableName = "CashInvoice"
        Cmd.Parameters.Clear()
        Cmd.Parameters.AddWithValue("@orno", transno)
        Cmd.CommandText = "SELECT ServiceDesc as 'Description', Qty as 'Quantity', Amount as 'Amount' FROM request WHERE ReqID = @orno"
        Da.SelectCommand = Cmd
        Da.SelectCommand.ExecuteNonQuery()
        Da.Fill(dt)

        closecon()
        If txtChange.Text = "" Then
            txtChange.Text = "0.00"
        End If
        cash = 0

        If lvChargeList.Items.Count > 0 Then
            For x As Integer = 0 To lvChargeList.Items.Count - 1
                cash += Double.Parse(lvChargeList.Items(x).SubItems(5).Text)
            Next
        End If
        change = Double.Parse(txtChange.Text)
        'cash = Double.Parse(txtCash.Text)
        total = Double.Parse(lbTotal.Text)
        bal = total - cash

        If change < 0 Then
            change = 0
        End If

        Dim params As New List(Of Object)
        params.Add(txtAccountNo.Text)
        params.Add(lbTransNo.Text)
        params.Add(Now.ToShortDateString)
        params.Add(Me.txtLast.Text + ", " + Me.txtFirst.Text + " " + Me.txtMI.Text)
        params.Add(global_emplname & ", " & global_empfname & " " & global_empmname & ".")

        params.Add(bal)
        params.Add(cash)
        params.Add(change)
        params.Add(total)
        params.Add(cmbDoctor.SelectedItem)

        If System.IO.File.Exists(Application.StartupPath + "\DentalSys_CashInvoice.xml") Then
            System.IO.File.Delete(Application.StartupPath + "\DentalSys_CashInvoice.xml")
        End If

        If System.IO.File.Exists(Application.StartupPath + "\DentalSys_CashInvoice.xsd") Then
            System.IO.File.Delete(Application.StartupPath + "\DentalSys_CashInvoice.xsd")
        End If

        dt.WriteXml(Application.StartupPath + "\DentalSys_CashInvoice.xml")
        dt.WriteXmlSchema(Application.StartupPath + "\DentalSys_CashInvoice.xsd")

        Dim caslipv As New CashInvoiceRptViewer

        caslipv.LoadReport(Application.StartupPath + "\DentalSys_CashInvoice.xsd", _
                               Application.StartupPath + "\DentalSys_CashInvoice.xml", _
                               params)
        btnPrint.Enabled = False
    End Sub

    Private Sub txtCash_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCash.KeyDown
        If e.KeyData = Keys.Enter Then
            If Not txtChargeDesc.Text = "" And Not txtQty.Text = "0" And Not txtAmount.Text = "0" Then
                Dim itemcnt As Integer = lvChargeList.Items.Count
                Dim nrmal As Boolean = False
                Dim qty, qty1 As Integer
                Dim damt, dcash As Double
                dcash = Double.Parse(txtCash.Text)
                qty1 = Integer.Parse(txtQty.Text)
                For x As Integer = 0 To itemcnt - 1
                    If txtChargeDesc.Text = lvChargeList.Items(x).SubItems(1).Text And txtAmount.Text = lvChargeList.Items(x).SubItems(3).Text Then
                        damt = Double.Parse(lvChargeList.Items(x).SubItems(3).Text)
                        qty = Integer.Parse(lvChargeList.Items(x).SubItems(2).Text)
                        qty1 += qty
                        damt *= qty1
                        lvChargeList.Items(x).SubItems(2).Text = qty1
                        lvChargeList.Items(x).SubItems(4).Text = damt.ToString("0.00")
                        nrmal = True
                    End If
                Next

                If nrmal = False Then
                    damt = Double.Parse(txtAmount.Text)
                    damt *= qty1
                    With lvChargeList
                        Dim cnt As Integer = .Items.Count
                        .Items.Add(cnt + 1)
                        .Items(cnt).SubItems.Add(txtChargeDesc.Text)
                        .Items(cnt).SubItems.Add(txtQty.Text)
                        .Items(cnt).SubItems.Add(txtAmount.Text)
                        .Items(cnt).SubItems.Add(damt.ToString("0.00"))
                        .Items(cnt).SubItems.Add(dcash.ToString("0.00"))
                    End With
                End If


                ComputeTotalAmount()
            End If
            txtChargeDesc.Focus()
            txtChargeDesc.SelectAll()
        End If
    End Sub

    Private Sub AddSuppliesDesc()
        Try
            opencon()

            Cmd.CommandText = "SELECT SuppDesc FROM supplies"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                txtChargeDesc.AutoCompleteCustomSource.Add(Dr(0))
            End While
            Dr.Close()
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub CheckServiceDesc()
        Try
            closecon()

            opencon()
            For x As Integer = 0 To lvChargeList.Items.Count - 1
                With Cmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@desc", lvChargeList.Items(x).SubItems(1).Text)

                    .CommandText = "SELECT SuppID, Qty FROM supplies WHERE SuppDesc LIKE @desc"
                    Dr = .ExecuteReader

                    If Dr.Read Then
                        If Dr.HasRows = True Then

                            Dim getcurrqty As Integer = Dr(1)
                            Dim getsuppid As String = Dr(0)

                            Dr.Close()

                            Dim newqty As Integer = Integer.Parse(lvChargeList.Items(x).SubItems(2).Text)
                            getcurrqty -= newqty

                            .Parameters.AddWithValue("@prevqty", getcurrqty)
                            .Parameters.AddWithValue("@suppid", getsuppid)
                            .Parameters.AddWithValue("@empid", global_empid)
                            .Parameters.AddWithValue("@date", dtpDateCharge.Value.ToString("yyyy-MM-dd"))
                            .Parameters.AddWithValue("@accnt", txtAccountNo.Text)
                            .Parameters.AddWithValue("@newqty", lvChargeList.Items(x).SubItems(2).Text)
                            .Parameters.AddWithValue("@reqid", lbTransNo.Text)

                            .CommandText = "INSERT INTO stock_trans(SuppID,Qty,Status,EmpID,Date) VALUES(@suppid,@newqty,'Out',@empid,@date)"
                            .ExecuteNonQuery()

                            .CommandText = "INSERT INTO get_supplies(ReqID,PatientID,SuppID,Qty,Date,EmpID) VALUES(@reqid,@accnt,@suppid,@newqty,@date,@empid)"
                            .ExecuteNonQuery()

                            .CommandText = "UPDATE supplies SET Qty=@prevqty WHERE SuppID=@suppid"
                            .ExecuteNonQuery()

                        Else
                            Dr.Close()

                        End If
                        Dr.Close()

                    End If
                    Dr.Close()

                End With

            Next

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub


    Private Sub txtCash_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCash.KeyPress
        NumberValidatewithDecimal(e, txtCash)
    End Sub

    Private Sub txtCash_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCash.KeyUp
        'AutoGetChangewhenInputtingCash(txtCash.Text)
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        NumberValidate(e, txtQty)
    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        NumberValidatewithDecimal(e, txtAmount)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        GenerateRequestNo()
        txtLast.Focus()
        txtLast.SelectAll()
        btnPrint.Enabled = False
        ListofDoctors(cmbDoctor)
        getOwnerName()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtAccountNo.Text = ""
        txtLast.Text = ""
        txtFirst.Text = ""
        txtMI.Text = ""
        txtDiagnosis.Text = ""
        txtChargeDesc.Text = ""
        txtQty.Text = "1"
        txtAmount.Text = "0.00"
        txtCash.Text = "0.00"
        lbTotal.Text = "0.00"
        lbTax.Text = "0.00"
        lbSubTotal.Text = "0.00"
        lvChargeList.Items.Clear()
        cmbDoctor.SelectedIndex = 0
        txtPercentage.Text = "0"
        txtPercentage.Enabled = False
        txtChange.Text = "0.00"
    End Sub

    Private Sub AddPatientCharges_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        suggestiontext("SELECT ServiceDesc FROM services", txtChargeDesc)
        AddSuppliesDesc()
        txtQty.Text = "1"
        txtAmount.Text = "0.00"
        txtCash.Text = "0.00"
        btnPrint.Enabled = False
    End Sub

    Private Sub txtLast_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLast.KeyDown
        If e.KeyData = Keys.Enter Then
            SearchPatientwithLast(txtLast.Text)
        End If
    End Sub

    Private Sub lvSearchPatient_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvSearchPatient.KeyDown
        If e.KeyData = Keys.Enter Then
            With lvSearchPatient
                txtAccountNo.Text = .SelectedItems.Item(0).Text
                txtLast.Text = .SelectedItems.Item(0).SubItems(1).Text
                txtFirst.Text = .SelectedItems.Item(0).SubItems(2).Text
                txtMI.Text = .SelectedItems.Item(0).SubItems(3).Text
                txtDiagnosis.Text = .SelectedItems.Item(0).SubItems(5).Text
                txtType.Text = .SelectedItems.Item(0).SubItems(6).Text
                Dim date1 As Date = Date.Parse(.SelectedItems.Item(0).SubItems(4).Text)
                Dim ageyear As Integer
                ageyear = DateDiff(DateInterval.Year, date1, Now)

                If Now.Month < date1.Month Then
                    ageyear -= 1
                End If

                txtAge.Text = ageyear

            End With
            lvSearchPatient.Visible = False
            txtChargeDesc.Focus()
        End If
    End Sub

    Private Sub lvSearchPatient_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvSearchPatient.LostFocus
        lvSearchPatient.Visible = False
    End Sub

    Private Sub btnPay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPay.Click
        If Not lvChargeList.Items.Count = 0 Then
            If MsgBox("Finish Transaction?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "CHARGES") = MsgBoxResult.Yes Then
                If Not txtAccountNo.Text = "" Then
                    CheckServiceDesc()
                End If
                Dim prcnt As Integer = 0
                If txtPercentage.Text = "" Then
                    prcnt = 0
                Else
                    prcnt = Integer.Parse(txtPercentage.Text)
                End If

                If txtPercentage.Enabled = False Then
                    PayService()
                ElseIf txtPercentage.Enabled = True And prcnt > 0 Then
                    PayServicewithotherDoctor()
                ElseIf txtPercentage.Enabled = True And prcnt = 0 Then
                    MsgBox("Please indicate percentage.")
                    txtPercentage.Focus()
                    txtPercentage.SelectAll()
                End If

            End If
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Not lvChargeList.Items.Count = 0 Then
            Loading.Show()
            Loading.BringToFront()
            PrintCashInvoice()
        Else
            MsgBox("No records to be printed.")
        End If
    End Sub

    Private Sub cmbDoctor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDoctor.SelectedIndexChanged

        Try
            opencon()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@doc", cmbDoctor.SelectedItem)
            Cmd.CommandText = "SELECT * FROM owner WHERE Name=@doc"
            Dr = Cmd.ExecuteReader

            If Dr.HasRows Then
                Dr.Close()
                txtPercentage.Text = "0"
                txtPercentage.Enabled = False
            Else
                txtPercentage.Enabled = True
                txtPercentage.Focus()
                txtPercentage.SelectAll()
            End If
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub txtPercentage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPercentage.KeyPress
        NumberValidate(e, txtPercentage)
    End Sub

    Private Sub lvChargeList_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvChargeList.MouseDoubleClick
        If lvChargeList.SelectedItems.Count > 0 Then
            If MsgBox("Remove this?", MsgBoxStyle.Critical + MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                lvChargeList.Items(lvChargeList.SelectedItems.Item(0).Index).Remove()
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ReturnDefault()
    End Sub
End Class