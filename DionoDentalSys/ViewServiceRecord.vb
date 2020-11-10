Public Class ViewServiceRecord

    Public Sub RefreshRecord()
        Try
            opencon()
            Dim dt As New DataTable
            dt.Clear()
            Dim dateforw, datemon As Date
            datemon = Now.AddMonths(-3)
            dateforw = New Date(Now.Year, datemon.Month, Now.Day)
            With Cmd.Parameters
                .Clear()
                .AddWithValue("@dfrom", dateforw.ToString("yyyy-MM-dd"))
                .AddWithValue("@dto", Now.ToString("yyyy-MM-dd"))
            End With
            Cmd.CommandText = "SELECT r.ReqID, r.Date, r.Time, r.ReqID as 'ServiceID', r.PatientID as 'Account#', r.ServiceDesc as 'Procedure', r.Qty, r.Amount, p.AmountPaid, p.Doctor, r.EmpID, pt.Type FROM request r LEFT JOIN patient pt ON r.PatientID = pt.PatientID LEFT JOIN payments p ON r.PatientID=p.PatientID AND r.ServiceDesc = p.ServiceDesc AND r.Date=p.Date AND r.ReqID=p.ReqID WHERE r.Date BETWEEN @dfrom AND @dto ORDER BY r.ReqID ASC"
            Da.SelectCommand = Cmd
            Da.Fill(dt)

            DataGridView1.DataSource = dt
            DataGridView1.AllowUserToOrderColumns = True
            DataGridView1.Refresh()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub SelectDateRecord()
        Try
            opencon()
            Dim dt As New DataTable
            dt.Clear()

            With Cmd.Parameters
                .Clear()
                .AddWithValue("@dfrom", dtpFrom.Value.ToString("yyyy-MM-dd"))
                .AddWithValue("@dto", dtpTo.Value.ToString("yyyy-MM-dd"))
            End With

            Cmd.CommandText = "SELECT r.ReqID, r.Date, r.Time, r.ReqID as 'ServiceID', r.PatientID as 'Account#', r.ServiceDesc as 'Procedure', r.Qty, r.Amount, p.AmountPaid, p.Doctor, r.EmpID, pt.Type FROM request r LEFT JOIN patient pt ON r.PatientID = pt.PatientID LEFT JOIN payments p ON r.PatientID=p.PatientID AND r.ServiceDesc = p.ServiceDesc AND r.Date=p.Date AND r.ReqID=p.ReqID WHERE r.Date BETWEEN @dfrom AND @dto ORDER BY r.ReqID ASC"
            Da.SelectCommand = Cmd
            Da.Fill(dt)

            DataGridView1.DataSource = dt
            DataGridView1.AllowUserToOrderColumns = True
            DataGridView1.Refresh()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ViewServiceRecord_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshRecord()
    End Sub


    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyData = Keys.Enter Then
            If UpdateServiceRecord.ShowInTaskbar = True Then
                UpdateServiceRecord.Dispose()
                UpdateServiceRecord.Close()
            End If

            UpdateServiceRecord.Show()
            UpdateServiceRecord.BringToFront()

            With UpdateServiceRecord
                .dtpDate.Value = DataGridView1.SelectedCells.Item(1).Value
                .txtID.Text = DataGridView1.SelectedCells.Item(3).Value
                .txtAccnt.Text = DataGridView1.SelectedCells.Item(4).Value
                .txtDesc.Text = DataGridView1.SelectedCells.Item(5).Value
                .txtQty.Text = DataGridView1.SelectedCells.Item(6).Value
                .txtAmt.Text = DataGridView1.SelectedCells.Item(7).Value
                If DataGridView1.SelectedCells.Item(8).Value.ToString = "" Then
                    .txtAmtPaid.Text = "0.00"
                Else
                    .txtAmtPaid.Text = DataGridView1.SelectedCells.Item(7).Value
                End If

                .txtAccnt.Focus()
                .txtAccnt.SelectAll()
            End With

        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SelectDateRecord()
    End Sub
End Class