Public Class ViewSupplies
    Dim WithEvents dt As New System.Data.DataTable()
    Dim strcmd As String = ""
    Dim stkcnt As Integer = 0
    Private Sub PrintReportAvail()
        opencon()

        dt.Clear()
        dt.TableName = "InventoryReport"

        Cmd.CommandText = strcmd

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
    End Sub

    Public Sub ViewAvailableStocks()
        Try
            stkcnt = 0
            Dim y As Integer = 0
            Dim getQty As Integer = 0
            opencon()
            lvAvailStock.Items.Clear()

            Cmd.CommandText = "SELECT * FROM supplies WHERE Status = 1"
            Dr = Cmd.ExecuteReader
            strcmd = Cmd.CommandText
            While Dr.Read
                lvAvailStock.Items.Add(Dr(0))

                For x As Integer = 1 To 4
                    lvAvailStock.Items(y).SubItems.Add(Dr(x))
                Next
                getQty = Integer.Parse(Dr(2))

                stkcnt += getQty

                y += 1

            End While
            Dr.Close()
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ViewStockIn()
        Try
            Dim y As Integer = 0
            opencon()
            lvStockIn.Items.Clear()

            Cmd.CommandText = "SELECT t.SuppID, s.SuppDesc, t.Qty, s.Unit, s.SalesPrice FROM stock_trans t, supplies s WHERE t.Status = 'In' AND t.SuppID = s.SuppID AND s.Status = 1"
            Dr = Cmd.ExecuteReader
            strcmd = Cmd.CommandText
            While Dr.Read
                lvStockIn.Items.Add(Dr(0))

                For x As Integer = 1 To 4
                    lvStockIn.Items(y).SubItems.Add(Dr(x))
                Next
                y += 1

            End While
            Dr.Close()
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub ViewStockOut()
        Try
            Dim y As Integer = 0
            opencon()
            lvStockOut.Items.Clear()

            Cmd.CommandText = "SELECT t.SuppID, s.SuppDesc, t.Qty, s.Unit, s.SalesPrice FROM stock_trans t, supplies s WHERE t.Status = 'Out' AND t.SuppID = s.SuppID AND s.Status = 1"
            Dr = Cmd.ExecuteReader
            strcmd = Cmd.CommandText
            While Dr.Read
                lvStockOut.Items.Add(Dr(0))

                For x As Integer = 1 To 4
                    lvStockOut.Items(y).SubItems.Add(Dr(x))
                Next
                y += 1

            End While
            Dr.Close()
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()

        ViewAvailableStocks()
        ViewStockIn()
        ViewStockOut()

    End Sub

    Private Sub lvAvailStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvAvailStock.KeyDown
        If e.KeyData = Keys.Enter Then
            If lvAvailStock.SelectedItems.Count > 0 Then
                Dim suppid As String = lvAvailStock.SelectedItems.Item(0).Text
                With QtyBox
                    .Show()
                    .txtQty.SelectAll()
                    .lbFormName.Text = Me.Name
                    .lbSelectItem.Text = lvAvailStock.SelectedItems.Item(0).Text
                End With
            End If
        End If
    End Sub

   
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If TabPage1.Focus = True Then
            PrintReportAvail()
        End If
    End Sub

    Private Sub TabPage1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub

End Class