Public Class SearchAccount

    Private Sub AcceptAccountInfo()
        Try

            opencon()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@pid", lvSearch.SelectedItems.Item(0).Text)
            Cmd.CommandText = "SELECT Diagnosis, DateEnded, Comments FROM patient WHERE PatientID = @pid"
            Dr = Cmd.ExecuteReader



            If Dr.Read Then
                With PatientAccntForm
                    .txtAccntNo.Text = lvSearch.SelectedItems.Item(0).Text
                    .txtPatientID.Text = lvSearch.SelectedItems.Item(0).SubItems(1).Text
                    .txtLastName.Text = lvSearch.SelectedItems.Item(0).SubItems(2).Text
                    .txtFirstName.Text = lvSearch.SelectedItems.Item(0).SubItems(3).Text
                    .txtMI.Text = lvSearch.SelectedItems.Item(0).SubItems(4).Text
                    .cmbType.SelectedItem = lvSearch.SelectedItems.Item(0).SubItems(5).Text
                    .cmbStatus.SelectedItem = lvSearch.SelectedItems.Item(0).SubItems(6).Text
                    Dim date3 As Date = lvSearch.SelectedItems.Item(0).SubItems(7).Text
                    .dtpDateStarted.Value = date3

                    .txtDiagnosis.Text = Dr(0)

                    If Not Dr(1).ToString = "1753-01-01" Then
                        Dim date2 As Date = Date.Parse(Dr(1).ToString)
                        .dtpDateEnded.Value = date2
                    Else
                        .dtpDateEnded.Value = Now
                        .chkDisable.Checked = True
                    End If

                    .txtComment.Text = Dr(2)
                    Me.Close()
                    Me.Dispose()
                    Timer1.Stop()
                End With
            End If

            Dr.Close()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
            closecon()
        End Try

    End Sub

    Private Sub SearchAccntinfo()
        Try
            opencon()
            lvSearch.Items.Clear()
            Dim y As Integer = 0
            Cmd.CommandText = "SELECT p.PatientID, p.InfoID, pi.LName, pi.FName, pi.MName, p.Type, p.Status, p.DateStarted FROM patient p, patient_info pi WHERE p.InfoID = pi.InfoID"
            Dr = Cmd.ExecuteReader

            While Dr.Read

                lvSearch.Items.Add(Dr(0))

                For x As Integer = 1 To 7
                    lvSearch.Items(y).SubItems.Add(Dr(x))
                Next

                y += 1
            End While
            Dr.Close()
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub SearchAccntInfowithLast()
        Try
            opencon()
            lvSearch.Items.Clear()
            Dim y As Integer = 0
            With Cmd.Parameters
                .Clear()
                .AddWithValue("@last", txtLast.Text & "%")
            End With
            Cmd.CommandText = "SELECT p.PatientID, p.InfoID, pi.LName, pi.FName, pi.MName, p.Type, p.Status, p.DateStarted FROM patient p, patient_info pi WHERE p.InfoID = pi.InfoID AND pi.LName LIKE @last"
            Dr = Cmd.ExecuteReader

            While Dr.Read

                lvSearch.Items.Add(Dr(0))

                For x As Integer = 1 To 7
                    lvSearch.Items(y).SubItems.Add(Dr(x))
                Next

                y += 1
            End While
            Dr.Close()
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub SearchAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SearchAccntinfo()
        'Timer1.Start()
        Timer1.Interval = 1000
    End Sub

    Private Sub SearchAccount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        Me.TopMost = True
        Me.Focus()
        Me.BringToFront()
    End Sub

    Private Sub txtLast_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLast.KeyUp
        If txtLast.Text = "" Then
            SearchAccntinfo()
        Else
            SearchAccntInfowithLast()
        End If
    End Sub

    Private Sub lvSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvSearch.KeyDown
        If e.KeyData = Keys.Enter Then
            AcceptAccountInfo()
        End If
    End Sub

    Private Sub lvSearch_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvSearch.MouseClick
        AcceptAccountInfo()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.Focused = False Then
            Me.BringToFront()
            Me.Focus()
            Me.TopMost = True
        End If
    End Sub
End Class