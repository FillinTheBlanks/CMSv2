Public Class PatientAccntForm


    Private Sub ListType()
        Try
            opencon()
            cmbType.Items.Clear()

            Cmd.CommandText = "SELECT type FROM patient_type"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                cmbType.Items.Add(Dr(0))
            End While

            cmbType.SelectedIndex = 0
            Dr.Close()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AddAccountInfo()
        If Not txtLastName.Text = "" And Not txtPatientID.Text = "" Then
            Try
                Dim cnt As Integer = 0
                Dim diagnosis As String = ""

                For x As Integer = 0 To lstDiagnosis.Items.Count - 1
                    diagnosis = diagnosis & lstDiagnosis.Items(x).ToString
                    If x < lstDiagnosis.Items.Count - 1 Then
                        diagnosis = diagnosis & ", "
                    End If
                Next
                closecon()

                opencon()

                With Cmd.Parameters
                    .Clear()
                    .AddWithValue("@accntid", txtAccntNo.Text)
                End With

                Cmd.CommandText = "SELECT * FROM patient WHERE PatientID = @accntid"
                Dr = Cmd.ExecuteReader

                If Dr.Read Then
                    If Dr.HasRows = False Then
                        Dr.Close()
                        closecon()
                    Else
                        Dr.Close()
                        closecon()


                        If MsgBox("Are you sure you want to create another account?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            account_no()

                        Else
                            Exit Sub
                            closecon()
                        End If

                    End If
                Else
                    closecon()

                End If

                closecon()

                    opencon()

                    With Cmd.Parameters
                        .Clear()
                        .AddWithValue("@accntid", txtAccntNo.Text)
                        .AddWithValue("@pid", txtPatientID.Text)
                        .AddWithValue("@type", cmbType.SelectedItem)
                        .AddWithValue("@diag", diagnosis)
                        .AddWithValue("@datestart", dtpDateStarted.Value.ToString("yyyy-MM-dd"))
                        .AddWithValue("@status", cmbStatus.SelectedItem)
                        .AddWithValue("@comment", txtComment.Text)
                    End With

                    If chkDisable.Checked = True Then
                        Cmd.CommandText = "INSERT INTO patient VALUES(@accntid, @pid, @type, @diag, @datestart, '1753-01-01', @status, @comment)"
                    Else
                        Cmd.Parameters.AddWithValue("@dateend", dtpDateEnded.Value.ToString("yyyy-MM-dd"))
                        Cmd.CommandText = "INSERT INTO patient VALUES(@accntid, @pid, @type, @diag, @datestart, @dateend, @status, @comment)"
                    End If
                    cnt = Cmd.ExecuteNonQuery()
                    MsgBox(cnt & " record has been successfully added.")

                    closecon()

                'If MsgBox("Do you want to add a reminder for this patient?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                '    MainModule.OrthodonticsToolStripMenuItem.PerformClick()

                'End If
            Catch ex As Exception
                closecon()
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub UpdateAccountRecord()
        Try
            If Not txtPatientID.Text = "" Then
                opencon()
                Dim diagnosis As String = ""
                With Cmd.Parameters
                    .Clear()
                    .AddWithValue("@accntid", txtAccntNo.Text)
                    .AddWithValue("@patientid", txtPatientID.Text)
                End With

                Cmd.CommandText = "SELECT * FROM patient WHERE PatientID = @accntid AND InfoID = @patientid"
                Dr = Cmd.ExecuteReader

                If Dr.Read Then
                    If Dr.HasRows = True Then
                        Dr.Close()

                        If lstDiagnosis.Items.Count = 0 Then
                            diagnosis = txtDiagnosis.Text
                        Else
                            For x As Integer = 0 To lstDiagnosis.Items.Count - 1
                                diagnosis = diagnosis & lstDiagnosis.Items(x).ToString
                                If x < lstDiagnosis.Items.Count - 1 Then
                                    diagnosis = diagnosis & ", "
                                End If
                            Next
                        End If
                        Dim cnt As Integer
                        With Cmd.Parameters
                            .AddWithValue("@type", cmbType.SelectedItem)
                            .AddWithValue("@status", cmbStatus.SelectedItem)
                            .AddWithValue("@start", dtpDateStarted.Value.ToString("yyyy-MM-dd"))
                            .AddWithValue("@diagnosis", diagnosis)
                            .AddWithValue("@comment", txtComment.Text)

                            If chkDisable.Checked = True Then
                                Cmd.CommandText = "UPDATE patient SET Type=@type, Diagnosis=@diagnosis, DateStarted=@start, Status=@status, Comments=@comment WHERE PatientID=@accntid AND InfoID=@patientid"
                                cnt = Cmd.ExecuteNonQuery()
                            Else
                                .AddWithValue("@end", dtpDateEnded.Value.ToString("yyyy-MM-dd"))
                                Cmd.CommandText = "UPDATE patient SET Type=@type, Diagnosis=@diagnosis, DateStarted=@start, DateEnded=@end, Status=@status, Comments=@comment WHERE PatientID=@accntid AND InfoID=@patientid"
                                cnt = Cmd.ExecuteNonQuery()
                            End If
                            MsgBox(cnt & " record has been updated.")
                            closecon()
                            Button1.PerformClick()
                            btnRefresh.PerformClick()

                        End With

                    Else
                        Dr.Close()

                    End If
                End If
                closecon()

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub DeleteAccountRecord()
        Try
            If Not txtPatientID.Text = "" Then
                opencon()
                Dim diagnosis As String = ""
                With Cmd.Parameters
                    .Clear()
                    .AddWithValue("@accntid", txtAccntNo.Text)
                    .AddWithValue("@patientid", txtPatientID.Text)
                End With

                Cmd.CommandText = "SELECT * FROM patient WHERE PatientID = @accntid AND InfoID = @patientid"
                Dr = Cmd.ExecuteReader

                If Dr.Read Then
                    If Dr.HasRows = True Then
                        Dr.Close()
                        If MsgBox("Are you sure you want to delete this account?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            Dim cnt As Integer
                            Cmd.CommandText = "DELETE FROM patient WHERE PatientID=@accntid AND InfoID=@patientid"
                            cnt = Cmd.ExecuteNonQuery

                            Cmd.CommandText = "DELETE FROM ortho_reminder WHERE PatientID=@accntid"
                            cnt = Cmd.ExecuteNonQuery

                            MsgBox(cnt & " record has been deleted.")
                            closecon()

                            Button1.PerformClick()
                            btnRefresh.PerformClick()
                        End If

                    Else
                        Dr.Close()

                    End If
                End If
                closecon()

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        txtAccntNo.Enabled = False
        txtPatientID.Enabled = False
        txtFirstName.Enabled = False
        txtLastName.Enabled = False
        txtMI.Enabled = False
        ListType()
        closecon()

    End Sub

    Private Sub txtDiagnosis_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDiagnosis.KeyDown
        If e.KeyData = Keys.Enter Then
            lstDiagnosis.Items.Add(txtDiagnosis.Text)
            txtDiagnosis.Text = ""
            txtDiagnosis.Focus()
        End If
    End Sub

    Private Sub chkDisable_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDisable.CheckedChanged
        If chkDisable.Checked = True Then
            dtpDateEnded.Enabled = False
        Else
            dtpDateEnded.Enabled = True
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        AddAccountInfo()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        UpdateAccountRecord()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListType()
        account_no()
        txtLastName.Text = ""
        txtFirstName.Text = ""
        txtDiagnosis.Text = ""
        txtComment.Text = ""
        txtMI.Text = ""
        txtPatientID.Text = ""
        lstDiagnosis.Items.Clear()
        dtpDateStarted.Value = Now
        chkDisable.Checked = True
        cmbType.SelectedIndex = 0
        cmbStatus.SelectedIndex = 0
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SearchAccount.Show()
        SearchAccount.BringToFront()

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteAccountRecord()
    End Sub
End Class