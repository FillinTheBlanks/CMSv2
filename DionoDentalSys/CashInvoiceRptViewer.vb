Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class CashInvoiceRptViewer
    Dim rptDoc As ReportDocument
    Dim ca1 As CashInvoice


    Public Sub LoadReport(ByVal schema As String, ByVal data As String, ByVal rptParams As List(Of Object))

        Dim paramCTR As Integer = 0
        ca1 = New CashInvoice()

        Dim dt As DataTable = New DataTable()

        dt.ReadXmlSchema(schema)
        dt.ReadXml(data)

        ca1.SetDataSource(dt)

        For Each obj As Object In rptParams
            ca1.SetParameterValue(paramCTR, obj)
            paramCTR += 1
        Next

        CrystalReportViewer1.ReportSource = ca1
        CrystalReportViewer1.Refresh()

        Me.Show()
    End Sub

    Private Sub CashInvoiceRptViewer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If MsgBox("New Transaction?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        With AddPatientCharges
            .btnRefresh.PerformClick()
            .btnClear.PerformClick()
        End With
        'End If
    End Sub

    Private Sub CashInvoiceRptViewer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Loading.Dispose()
        Loading.Close()
    End Sub
End Class