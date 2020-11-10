Public Class IncomeStatement
    Dim WithEvents dt As New System.Data.DataTable()
    Dim strcmd As String = ""
    Dim dAmt, dTAmt As Double


    Private Sub GenerateIncomeStatement()
        Try
            dTAmt = 0
            opencon()
            Dim y As Integer = 0
            lvIncome.Items.Clear()

            With Cmd.Parameters
                .Clear()
                .AddWithValue("@from", dtpFrom.Value.ToString("yyyy-MM-dd"))
                .AddWithValue("@to", dtpTo.Value.ToString("yyyy-MM-dd"))
                .AddWithValue("@doctor", cmbDoctor.SelectedItem)
            End With

            If dtpFrom.Value.Equals(dtpTo.Value) Then
                Cmd.CommandText = "SELECT pt.PatientID, p.LName, p.FName, p.MName, py.ServiceDesc, py.AmountPaid FROM payments py LEFT JOIN patient pt ON py.PatientID = pt.PatientID LEFT JOIN patient_info p ON pt.InfoID = p.InfoID WHERE py.Date = @from AND py.Doctor = @doctor"
            Else
                Cmd.CommandText = "SELECT pt.PatientID, p.LName, p.FName, p.MName, py.ServiceDesc, py.AmountPaid FROM payments py LEFT JOIN patient pt ON py.PatientID = pt.PatientID LEFT JOIN patient_info p ON pt.InfoID = p.InfoID WHERE py.Date BETWEEN @from AND @to AND py.Doctor = @doctor"
            End If
            Dr = Cmd.ExecuteReader
            strcmd = Cmd.CommandText

            While Dr.Read
                lvIncome.Items.Add(Dr(1).ToString)

                For x As Integer = 2 To 5
                    If Not Dr(x).ToString = "" Then
                        lvIncome.Items(y).SubItems.Add(Dr(x).ToString)
                    Else
                        lvIncome.Items(y).SubItems.Add(" ")
                    End If

                Next
                dAmt = Dr(5)
                dTAmt += dAmt
                y += 1
            End While
            txtTAmt.Text = dTAmt.ToString("0.00")

            Dr.Close()
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub PrintCashInvoice()

        opencon()

        dt.Clear()
        dt.TableName = "IncomeStatement"
        With Cmd.Parameters
            .Clear()
            .AddWithValue("@from", dtpFrom.Value.ToString("yyyy-MM-dd"))
            .AddWithValue("@to", dtpTo.Value.ToString("yyyy-MM-dd"))
            .AddWithValue("@doctor", cmbDoctor.SelectedItem)
        End With

        Cmd.CommandText = strcmd

        Da.SelectCommand = Cmd
        Da.SelectCommand.ExecuteNonQuery()
        Da.Fill(dt)

        closecon()

        Dim params As New List(Of Object)

        If dtpFrom.Value.Equals(dtpTo.Value) Then
            params.Add(dtpFrom.Value.ToLongDateString)
        Else
            params.Add("From " & dtpFrom.Value.ToString("MM/dd/yyyy") & " To " & dtpTo.Value.ToString("MM/dd/yyyy"))
        End If

        params.Add(txtTAmt.Text)
        params.Add(cmbDoctor.SelectedItem)

        If System.IO.File.Exists(Application.StartupPath + "\DentalSys_IncomeStatement.xml") Then
            System.IO.File.Delete(Application.StartupPath + "\DentalSys_IncomeStatement.xml")
        End If

        If System.IO.File.Exists(Application.StartupPath + "\DentalSys_IncomeStatement.xsd") Then
            System.IO.File.Delete(Application.StartupPath + "\DentalSys_IncomeStatement.xsd")
        End If

        dt.WriteXml(Application.StartupPath + "\DentalSys_IncomeStatement.xml")
        dt.WriteXmlSchema(Application.StartupPath + "\DentalSys_IncomeStatement.xsd")

        Dim rptvwer As New ISRptViewer

        rptvwer.LoadReport(Application.StartupPath + "\DentalSys_IncomeStatement.xsd", _
                               Application.StartupPath + "\DentalSys_IncomeStatement.xml", _
                               params)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        GenerateIncomeStatement()
        ListofDoctors(cmbDoctor)
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        GenerateIncomeStatement()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If lvIncome.Items.Count > 0 Then
            Loading.Show()
            Loading.BringToFront()
            PrintCashInvoice()
        End If
    End Sub

End Class