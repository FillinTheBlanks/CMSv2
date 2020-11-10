Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class SuppliesPaymentRptViewer
    Dim rptDoc As ReportDocument
    Dim is1 As SuppliesPaymentRpt
    Dim WithEvents dt As New System.Data.DataTable()
    Dim stkcnt As Integer = 0

    Public Sub LoadReport(ByVal schema As String, ByVal data As String, ByVal rptParams As List(Of Object))

        Dim paramCTR As Integer = 0
        is1 = New SuppliesPaymentRpt()

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
                Cmd.CommandText = "SELECT p.`SupprID` as 'Supplier', p.`Comments` as 'Description', p.`Cash`, p.`Check`, p.`Date`, s.`Name`, s.`Contact#`, s.`Address` FROM `supplies_payment` p, supplier s WHERE p.`SupprID` = s.`SupprID` ORDER BY p.`SupprID` ASC"
            Else
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@supplier", strtype)
                Cmd.CommandText = "SELECT p.`SupprID` as 'Supplier', p.`Comments` as 'Description', p.`Cash`, p.`Check`, p.`Date`, s.`Name`, s.`Contact#`, s.`Address` FROM `supplies_payment` p, supplier s WHERE p.`SupprID` =@supplier AND p.`SupprID` = s.`SupprID` ORDER BY p.`SupprID` ASC"
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
                SelectSupplierPaymentReport.Dispose()
                SelectSupplierPaymentReport.Close()
            Else
                SelectSupplierPaymentReport.Dispose()
                SelectSupplierPaymentReport.Close()
            End If
            dt.Clear()
            dt.TableName = "SuppliesPaymentReport"

            Da.SelectCommand = Cmd
            Da.SelectCommand.ExecuteNonQuery()
            Da.Fill(dt)

            closecon()

            Dim params As New List(Of Object)

            params.Add(Now.ToString("MMM dd, yyyy hh:mm:ss tt"))
            'params.Add(MainModule.tsEmpName.Text)
            'params.Add(strtype)
            'params.Add(stkcnt)

            If System.IO.File.Exists(Application.StartupPath + "\DentalSys_SuppliesPaymentReport.xml") Then
                System.IO.File.Delete(Application.StartupPath + "\DentalSys_SuppliesPaymentReport.xml")
            End If

            If System.IO.File.Exists(Application.StartupPath + "\DentalSys_SuppliesPaymentReport.xsd") Then
                System.IO.File.Delete(Application.StartupPath + "\DentalSys_SuppliesPaymentReport.xsd")
            End If

            dt.WriteXml(Application.StartupPath + "\DentalSys_SuppliesPaymentReport.xml")
            dt.WriteXmlSchema(Application.StartupPath + "\DentalSys_SuppliesPaymentReport.xsd")

            Dim rptvwer As New SuppliesPaymentRptViewer

            rptvwer.LoadReport(Application.StartupPath + "\DentalSys_SuppliesPaymentReport.xsd", _
                                   Application.StartupPath + "\DentalSys_SuppliesPaymentReport.xml", _
                                   params)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub SuppliesPaymentRptViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class