Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class DTRptViewer
    Dim rptDoc As ReportDocument
    Dim is1 As DTReport
    Dim WithEvents dt As New System.Data.DataTable()

    Public Sub LoadReport(ByVal schema As String, ByVal data As String, ByVal rptParams As List(Of Object))

        Dim paramCTR As Integer = 0
        is1 = New DTReport()

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
        SelectDTRPrint.Close()
        SelectDTRPrint.Dispose()
        Me.Show()
    End Sub

End Class