Public Class PatientMonitoring

    Public Sub DisplayPatientRecord()
        Try
            With lvPatientRecord
                .Items.Clear()
                .Columns(0).Width = 110
                .Columns(1).Width = 208
                .Columns(2).Width = 130
                .Columns(3).Width = 354
                .Columns(4).Width = 132
            End With

            Dim y As Integer = 0
            opencon()
            Cmd.CommandText = "SELECT p.PatientID, i.LName, i.FName, i.MName, p.Type, p.Diagnosis, p.DateStarted " _
                & "FROM patient p, patient_info i WHERE p.InfoID = i.InfoID"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                With lvPatientRecord
                    .Items.Add(Dr(0))
                    .Items(y).SubItems.Add(Dr(1) & ", " & Dr(2) & " " & Dr(3) & ".")
                    For x As Integer = 4 To 6
                        .Items(y).SubItems.Add(Dr(x))
                    Next
                End With
                y += 1
            End While
            Dr.Close()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub DisplayPatientRecordbySearch(ByVal strSearch As String)
        Try
            Dim qry As String = ""
            With lvPatientRecord
                .Items.Clear()
                .Columns(0).Width = 110
                .Columns(1).Width = 208
                .Columns(2).Width = 130
                .Columns(3).Width = 354
                .Columns(4).Width = 132
            End With

            Dim y As Integer = 0
            opencon()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@search", strSearch & "%")

            If rdoAccount.Checked = True Then
                qry = "SELECT p.PatientID, i.LName, i.FName, i.MName, p.Type, p.Diagnosis, p.DateStarted " _
                & "FROM patient p, patient_info i WHERE p.PatientID LIKE @search AND p.InfoID = i.InfoID"
            Else
                qry = "SELECT p.PatientID, i.LName, i.FName, i.MName, p.Type, p.Diagnosis, p.DateStarted " _
               & "FROM patient p, patient_info i WHERE i.LName LIKE @search AND p.InfoID = i.InfoID"
            End If
            Cmd.CommandText = qry
            Dr = Cmd.ExecuteReader

            While Dr.Read
                With lvPatientRecord
                    .Items.Add(Dr(0))
                    .Items(y).SubItems.Add(Dr(1) & ", " & Dr(2) & " " & Dr(3) & ".")
                    For x As Integer = 4 To 6
                        .Items(y).SubItems.Add(Dr(x))
                    Next
                End With
                y += 1
            End While
            Dr.Close()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ViewPatientPayment()
        If lvPatientRecord.SelectedItems.Count > 0 Then
            If ViewPatientPayments.ShowInTaskbar = True Then
                ViewPatientPayments.Close()
            End If

            With ViewPatientPayments
                .lbAccnt.Text = lvPatientRecord.SelectedItems(0).Text
                .lbName.Text = lvPatientRecord.SelectedItems(0).SubItems(1).Text
                .lbType.Text = lvPatientRecord.SelectedItems(0).SubItems(2).Text
                .lvPayment.Columns(0).Width = 110
                .lvPayment.Columns(1).Width = 346
                .lvPayment.Columns(2).Width = 131
                .lvPayment.Columns(3).Width = 127
                .lvPayment.Columns(4).Width = 120
            End With
            opencon()

            With Cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@accnt", lvPatientRecord.SelectedItems(0).Text)
                .CommandText = "SELECT p.Date, p.ServiceDesc, r.Amount, p.AmountPaid, (r.Amount - p.AmountPaid) FROM payments p RIGHT JOIN request r ON p.ServiceDesc = r.ServiceDesc AND p.ReqID=r.ReqID AND p.Date=r.Date WHERE p.PatientID=@accnt"
                Dr = .ExecuteReader
                Dim y As Integer = 0
                Dim amt, amtc, amtp As Double
                Dim totamt, totamtc, totamtp As Double
                totamt = 0
                totamtc = 0
                totamtp = 0
                While Dr.Read
                    ViewPatientPayments.lvPayment.Items.Add(Dr(0))
                    For x As Integer = 1 To 4
                        ViewPatientPayments.lvPayment.Items(y).SubItems.Add(Dr(x))
                    Next
                    y += 1

                    amtc = Dr(2)
                    totamtc += amtc
                    amtp = Dr(3)
                    totamtp += amtp
                    amt = Dr(4)
                    totamt += amt

                End While

                ViewPatientPayments.lvPayment.Items.Add(" ")
                ViewPatientPayments.lvPayment.Items(y).SubItems.Add("Sum Total")
                ViewPatientPayments.lvPayment.Items(y).SubItems.Add(totamtc)
                ViewPatientPayments.lvPayment.Items(y).SubItems.Add(totamtp)
                ViewPatientPayments.lvPayment.Items(y).SubItems.Add(totamt)

                ViewPatientPayments.txtTotal.Text = totamt.ToString("0.00")
                Dr.Close()
            End With

            closecon()

            ViewPatientPayments.Show()
            ViewPatientPayments.BringToFront()
        End If
    End Sub


    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        DisplayPatientRecord()
    End Sub

    Private Sub lvPatientRecord_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvPatientRecord.KeyDown
        If e.KeyData = Keys.Enter Then
            ViewPatientPayment()
        End If
    End Sub

    Private Sub lvPatientRecord_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvPatientRecord.MouseDoubleClick
        ViewPatientPayment()
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyData = Keys.Enter Then
            DisplayPatientRecordbySearch(txtSearch.Text)
        End If
        
    End Sub

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        If txtSearch.Text = "" Then
            DisplayPatientRecord()
        End If
    End Sub
End Class