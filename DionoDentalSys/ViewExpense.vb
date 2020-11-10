Public Class ViewExpense
    Dim WithEvents dt As New System.Data.DataTable()
    Dim strcmd As String = ""
    Dim strdate As String = ""
    Private Sub PrintReport()
        opencon()

        With Cmd
            .Parameters.Clear()
            .Parameters.AddWithValue("@date1", dtpDate.Value.ToString("yyyy-MM-dd"))
            
            dt.Clear()
            dt.TableName = "ExpenseReport"

            Cmd.CommandText = strcmd
            Da.SelectCommand = Cmd
            Da.SelectCommand.ExecuteNonQuery()
            Da.Fill(dt)

            Dim params As New List(Of Object)

            params.Add(strdate)
            params.Add(txtAmount.Text)

            If System.IO.File.Exists(Application.StartupPath + "\DentalSys_ExpenseReport.xml") Then
                System.IO.File.Delete(Application.StartupPath + "\DentalSys_ExpenseReport.xml")
            End If

            If System.IO.File.Exists(Application.StartupPath + "\DentalSys_ExpenseReport.xsd") Then
                System.IO.File.Delete(Application.StartupPath + "\DentalSys_ExpenseReport.xsd")
            End If

            dt.WriteXml(Application.StartupPath + "\DentalSys_ExpenseReport.xml")
            dt.WriteXmlSchema(Application.StartupPath + "\DentalSys_ExpenseReport.xsd")

            Dim rptvwer As New ExpenseRptViewer

            rptvwer.LoadReport(Application.StartupPath + "\DentalSys_ExpenseReport.xsd", _
                                   Application.StartupPath + "\DentalSys_ExpenseReport.xml", _
                                   params)
        End With
        closecon()
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        Try
            opencon()
            lvExpense.Items.Clear()
            Dim y As Integer = 0
            Dim dAmt, dTAmt As Double
            dTAmt = 0
            Cmd.CommandText = "SELECT e.Details, e.Qty, e.Amount, e.Date, em.LName, em.FName, em.MName FROM expense e, employee em WHERE e.EmpID = em.EmpID"
            Dr = Cmd.ExecuteReader
            strcmd = Cmd.CommandText
            While Dr.Read
                lvExpense.Items.Add(Dr(0))
                lvExpense.Items(y).SubItems.Add(Dr(1))
                lvExpense.Items(y).SubItems.Add(Dr(2))
                lvExpense.Items(y).SubItems.Add(Dr(3))
                lvExpense.Items(y).SubItems.Add(Dr(4) & ", " & Dr(5) & " " & Dr(6) & ".")
                y += 1
                dAmt = Dr(2)
                dTAmt += dAmt
            End While
            Dr.Close()
            Dim datef, datet As String
            datef = ""
            datet = ""
            Cmd.CommandText = "SELECT MIN(Date), MAX(Date) FROM expense"
            Dr = Cmd.ExecuteReader

            If Dr.Read Then
                datef = Dr(0).ToString
                datet = Dr(1).ToString
            End If
            Dr.Close()
            strdate = "From " & datef & " To " & datet

            txtAmount.Text = dTAmt.ToString("0.00")

            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        closecon()

    End Sub

    Private Sub dtpDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDate.ValueChanged
        Try
            opencon()
            Dim y As Integer = 0
            Dim dAmt, dTAmt As Double
            dTAmt = 0
            With Cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@date1", dtpDate.Value.ToString("yyyy-MM-dd"))
                .CommandText = "SELECT e.Details, e.Qty, e.Amount, e.Date, em.LName, em.FName, em.MName FROM expense e, employee em WHERE e.Date = @date1 AND e.EmpID = em.EmpID"

                Dr = .ExecuteReader
                strcmd = .CommandText
                If Dr.HasRows = True Then
                    lvExpense.Items.Clear()
                    While Dr.Read
                        lvExpense.Items.Add(Dr(0))
                        lvExpense.Items(y).SubItems.Add(Dr(1))
                        lvExpense.Items(y).SubItems.Add(Dr(2))
                        lvExpense.Items(y).SubItems.Add(Dr(3))
                        lvExpense.Items(y).SubItems.Add(Dr(4) & ", " & Dr(5) & " " & Dr(6) & ".")
                        y += 1
                        dAmt = Dr(2)
                        dTAmt += dAmt
                    End While

                    txtAmount.Text = dTAmt.ToString("0.00")
                    strdate = dtpDate.Value.ToLongDateString

                Else
                    lvExpense.Items.Clear()
                End If
                
                Dr.Close()
            End With
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If lvExpense.Items.Count > 0 Then
            Loading.Show()
            PrintReport()
        End If
    End Sub
End Class