Public Class SelectDTRPrint
    Dim WithEvents dt As New System.Data.DataTable()
    
    Private Sub GetMonth()
        Try
            opencon()
            cmbMonth.Items.Clear()
            With Cmd
                .CommandText = "SELECT DISTINCT DATE_FORMAT(Date, '%M') FROM dtr_employee"
                Dr = .ExecuteReader

                While Dr.Read
                    cmbMonth.Items.Add(Dr(0))
                End While
            End With
            cmbMonth.SelectedIndex = 0
            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub GetYear()
        Try
            opencon()
            cmbYear.Items.Clear()
            With Cmd
                .CommandText = "SELECT DISTINCT DATE_FORMAT(Date, '%Y') FROM dtr_employee"
                Dr = .ExecuteReader

                While Dr.Read
                    cmbYear.Items.Add(Dr(0))
                End While
            End With
            cmbYear.SelectedIndex = 0
            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub PrintReport()
        Try
            Dim strcmd As String = ""
            Dim strname As String = ""
            Dim strtwh = ""
            opencon()
            dt.Clear()
            dt.TableName = "DTRecord"
            Dim monthnum As Integer
            monthnum = DateTime.ParseExact(cmbMonth.SelectedItem, "MMMM", Nothing).Month

            With Cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@empid", lbEmpID.Text)
                .Parameters.AddWithValue("@date", cmbYear.SelectedItem & "-" & monthnum.ToString("00") & "-%")

                .CommandText = "SELECT Day,DATE_FORMAT(In1,'%H:%i') As 'In',DATE_FORMAT(Out1,'%H:%i') As 'Out',DATE_FORMAT(In2,'%H:%i') As 'In',DATE_FORMAT(Out2,'%H:%i') As 'Out',DATE_FORMAT(In3,'%H:%i') As 'In',DATE_FORMAT(Out3,'%H:%i') As 'Out', CONCAT(HOUR(ADDTIME(ADDTIME(IF(In1 IS NOT NULL AND Out1 IS NOT NULL,(TIMEDIFF(Out1,In1)),0), IF(In2 IS NOT NULL AND Out2 IS NOT NULL,(TIMEDIFF(Out2,In2)),0)),IF(In3 IS NOT NULL AND Out3 IS NOT NULL,(TIMEDIFF(Out3,In3)),0))),'h&',MINUTE(ADDTIME(ADDTIME(IF(In1 IS NOT NULL AND Out1 IS NOT NULL,(TIMEDIFF(Out1,In1)),0), IF(In2 IS NOT NULL AND Out2 IS NOT NULL,(TIMEDIFF(Out2,In2)),0)),IF(In3 IS NOT NULL AND Out3 IS NOT NULL,(TIMEDIFF(Out3,In3)),0))),'m') AS 'Total',Remarks FROM dtr_employee WHERE EmpID = @empid AND Date LIKE @date"
                Dr = .ExecuteReader

                If Dr.Read Then
                    If Dr.HasRows = True Then
                        Dr.Close()
                        strcmd = .CommandText

                        .CommandText = "SELECT LName,FName,MName FROM employee WHERE EmpID=@empid"
                        Dr = .ExecuteReader
                        If Dr.Read Then
                            strname = Dr(0) & ", " & Dr(1) & " " & Dr(2) & "."
                        End If

                        Dr.Close()

                        .CommandText = "SELECT CONCAT(HOUR(SEC_TO_TIME(SUM(TIME_TO_SEC(ADDTIME(ADDTIME(IF(In1 IS NOT NULL AND Out1 IS NOT NULL,(TIMEDIFF(Out1,In1)),0), IF(In2 IS NOT NULL AND Out2 IS NOT NULL,(TIMEDIFF(Out2,In2)),0)),IF(In3 IS NOT NULL AND Out3 IS NOT NULL,(TIMEDIFF(Out3,In3)),0)))))),'h&',MINUTE(SEC_TO_TIME(SUM(TIME_TO_SEC(ADDTIME(ADDTIME(IF(In1 IS NOT NULL AND Out1 IS NOT NULL,(TIMEDIFF(Out1,In1)),0), IF(In2 IS NOT NULL AND Out2 IS NOT NULL,(TIMEDIFF(Out2,In2)),0)),IF(In3 IS NOT NULL AND Out3 IS NOT NULL,(TIMEDIFF(Out3,In3)),0)))))),'m') FROM dtr_employee WHERE EmpID = @empid AND Date LIKE @date"
                        Dr = .ExecuteReader
                        If Dr.Read Then
                            strtwh = Dr(0)
                        End If
                        Dr.Close()

                        .CommandText = strcmd
                        Da.SelectCommand = Cmd
                        Da.SelectCommand.ExecuteNonQuery()
                        Da.Fill(dt)

                        closecon()

                        Dim params As New List(Of Object)

                        params.Add(strname)
                        params.Add(cmbMonth.SelectedItem & " " & cmbYear.SelectedItem)
                        params.Add(strtwh)

                        If System.IO.File.Exists(Application.StartupPath + "\DentalSys_DTReport1.xml") Then
                            System.IO.File.Delete(Application.StartupPath + "\DentalSys_DTReport1.xml")
                        End If

                        If System.IO.File.Exists(Application.StartupPath + "\DentalSys_DTReport1.xsd") Then
                            System.IO.File.Delete(Application.StartupPath + "\DentalSys_DTReport1.xsd")
                        End If

                        dt.WriteXml(Application.StartupPath + "\DentalSys_DTReport1.xml")
                        dt.WriteXmlSchema(Application.StartupPath + "\DentalSys_DTReport1.xsd")

                        Dim rptvwer As New DTRptViewer

                        rptvwer.LoadReport(Application.StartupPath + "\DentalSys_DTReport1.xsd", _
                                               Application.StartupPath + "\DentalSys_DTReport1.xml", _
                                               params)
                    Else
                        Dr.Close()
                        closecon()
                    End If
                Else
                    Dr.Close()
                    MsgBox("No records available.")
                    closecon()
                End If
            End With

            
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        
    End Sub

    Private Sub SelectDTRPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetMonth()
        GetYear()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintReport()
    End Sub

End Class