Public Class SelectPatientReport

    Private Sub SelectPatientReport_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Timer1.Stop()
    End Sub

    Private Sub SelectPatientReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            opencon()
            cmbPType.Items.Clear()
            cmbPType.Items.Add("All")

            Cmd.CommandText = "SELECT * FROM patient_type ORDER BY type ASC"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                cmbPType.Items.Add(Dr(0))
            End While
            Dr.Close()
            closecon()
            cmbPType.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Timer1.Start()
        Timer1.Interval = 1000
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        MainModule.loadForms(MainModule.tsProgressBar)
        PatientRptViewer.PrintDirect(cmbPType.SelectedItem)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If cmbPType.Focus = False Then
            Me.BringToFront()
            Me.TopMost = True
            Me.Focus()
        End If
    End Sub
End Class