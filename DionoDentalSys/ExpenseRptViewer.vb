Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class ExpenseRptViewer
    Dim rptDoc As ReportDocument
    Dim is1 As ExpenseRpt


    Public Sub LoadReport(ByVal schema As String, ByVal data As String, ByVal rptParams As List(Of Object))

        Dim paramCTR As Integer = 0
        is1 = New ExpenseRpt()

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

    Private Sub ExpenseRptViewer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Loading.Dispose()
        Loading.Close()
    End Sub

End Class