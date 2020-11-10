Public Class SelectSupplierPaymentReport

    Private Sub SelectSupplierPaymentReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            opencon()
            cmbSupplier.Items.Clear()
            cmbSupplier.Items.Add("All")
            Cmd.CommandText = "SELECT SupprID FROM supplier ORDER BY SupprID ASC"
            Dr = Cmd.ExecuteReader
            While Dr.Read
                cmbSupplier.Items.Add(Dr(0))
            End While
            Dr.Close()
            closecon()
            cmbSupplier.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Timer1.Start()
        Timer1.Interval = 1000
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If cmbSupplier.Focused = False Then
            Me.Focus()
            Me.BringToFront()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        SuppliesPaymentRptViewer.PrintDirect(cmbSupplier.SelectedItem)
    End Sub

End Class