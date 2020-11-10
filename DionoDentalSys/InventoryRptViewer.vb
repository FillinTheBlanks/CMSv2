Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class InventoryRptViewer
    Dim rptDoc As ReportDocument
    Dim is1 As InventoryRpt
    Dim WithEvents dt As New System.Data.DataTable()
    Dim stkcnt As Integer = 0

    Public Sub LoadReport(ByVal schema As String, ByVal data As String, ByVal rptParams As List(Of Object))

        Dim paramCTR As Integer = 0
        is1 = New InventoryRpt()

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

    Public Sub PrintDirect()
        Try
            stkcnt = 0
            Dim y As Integer = 0
            Dim getQty As Integer = 0
            opencon()


            Cmd.CommandText = "SELECT * FROM supplies WHERE Status = 1"
            Dr = Cmd.ExecuteReader

            While Dr.Read

                getQty = Integer.Parse(Dr(2))

                stkcnt += getQty
            End While
            Dr.Close()

            dt.Clear()
            dt.TableName = "InventoryReport"

            Da.SelectCommand = Cmd
            Da.SelectCommand.ExecuteNonQuery()
            Da.Fill(dt)

            closecon()

            Dim params As New List(Of Object)

            params.Add(Now.ToString("MMM dd, yyyy hh:mm:ss tt"))
            params.Add(stkcnt)
            params.Add(MainModule.tsEmpName.Text)

            If System.IO.File.Exists(Application.StartupPath + "\DentalSys_InventoryReport.xml") Then
                System.IO.File.Delete(Application.StartupPath + "\DentalSys_InventoryReport.xml")
            End If

            If System.IO.File.Exists(Application.StartupPath + "\DentalSys_InventoryReport.xsd") Then
                System.IO.File.Delete(Application.StartupPath + "\DentalSys_InventoryReport.xsd")
            End If

            dt.WriteXml(Application.StartupPath + "\DentalSys_InventoryReport.xml")
            dt.WriteXmlSchema(Application.StartupPath + "\DentalSys_InventoryReport.xsd")

            Dim rptvwer As New InventoryRptViewer

            rptvwer.LoadReport(Application.StartupPath + "\DentalSys_InventoryReport.xsd", _
                                   Application.StartupPath + "\DentalSys_InventoryReport.xml", _
                                   params)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub InventoryRptViewer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Loading.Dispose()
        Loading.Close()
    End Sub

    Private Sub InventoryRptViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class