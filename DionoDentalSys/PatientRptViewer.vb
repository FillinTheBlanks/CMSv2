Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class PatientRptViewer
    Dim rptDoc As ReportDocument
    Dim is1 As PatientRpt
    Dim WithEvents dt As New System.Data.DataTable()
    Dim stkcnt As Integer = 0

    Public Sub LoadReport(ByVal schema As String, ByVal data As String, ByVal rptParams As List(Of Object))

        Dim paramCTR As Integer = 0
        is1 = New PatientRpt()

        Dim dt As DataTable = New DataTable()

        dt.ReadXmlSchema(schema)
        dt.ReadXml(data)

        is1.SetDataSource(dt)

        For Each obj As Object In rptParams
            is1.SetParameterValue(paramCTR, obj)
            paramCTR += 1
        Next

        CrystalReportViewer1.ReportSource = is1
        CrystalReportViewer1.Refresh()

        Me.Show()
    End Sub

    Public Sub PrintDirect(ByVal strtype As String)
        Try
            stkcnt = 0
            Dim y As Integer = 0
            Dim getQty As Integer = 0
            opencon()
            If strtype = "All" Then
                Cmd.CommandText = "SELECT p.PatientID as 'Account No', CONCAT(i.LName,', ', i.FName, ' ', i.MName) as 'Patient Name', p.Type as 'Type', p.Diagnosis as 'Diagnosis', p.DateStarted as 'Start Date', p.Status as 'Status' FROM patient p, patient_info i WHERE p.InfoID = i.InfoID"
            Else
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@type", strtype)
                Cmd.CommandText = "SELECT p.PatientID as 'Account No', CONCAT(i.LName,', ', i.FName, ' ', i.MName) as 'Patient Name', p.Type as 'Type', p.Diagnosis as 'Diagnosis', p.DateStarted as 'Start Date', p.Status as 'Status' FROM patient p, patient_info i WHERE p.InfoID = i.InfoID AND p.Type = @type"
            End If

            Dr = Cmd.ExecuteReader

            While Dr.Read
                stkcnt += 1
            End While

            Dr.Close()
            If stkcnt = 0 Then
                closecon()
                Exit Sub
                MsgBox("No reports to be printed.")
                SelectPatientReport.Dispose()
                SelectPatientReport.Close()
            Else
                SelectPatientReport.Dispose()
                SelectPatientReport.Close()
            End If
            dt.Clear()
            dt.TableName = "PatientReport"

            Da.SelectCommand = Cmd
            Da.SelectCommand.ExecuteNonQuery()
            Da.Fill(dt)

            closecon()

            Dim params As New List(Of Object)

            params.Add(Now.ToString("MMM dd, yyyy hh:mm:ss tt"))
            params.Add(MainModule.tsEmpName.Text)
            params.Add(strtype)
            params.Add(stkcnt)

            If System.IO.File.Exists(Application.StartupPath + "\DentalSys_PatientReport.xml") Then
                System.IO.File.Delete(Application.StartupPath + "\DentalSys_PatientReport.xml")
            End If

            If System.IO.File.Exists(Application.StartupPath + "\DentalSys_PatientReport.xsd") Then
                System.IO.File.Delete(Application.StartupPath + "\DentalSys_PatientReport.xsd")
            End If

            dt.WriteXml(Application.StartupPath + "\DentalSys_PatientReport.xml")
            dt.WriteXmlSchema(Application.StartupPath + "\DentalSys_PatientReport.xsd")

            Dim rptvwer As New PatientRptViewer

            rptvwer.LoadReport(Application.StartupPath + "\DentalSys_PatientReport.xsd", _
                                   Application.StartupPath + "\DentalSys_PatientReport.xml", _
                                   params)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub PatientRptViewer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("Do you want to print another patient record?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            SelectPatientReport.Show()
            SelectPatientReport.BringToFront()
        End If
    End Sub

    Private Sub PatientRptViewer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Loading.Dispose()
        Loading.Close()
    End Sub
End Class