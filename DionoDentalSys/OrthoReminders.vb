Public Class OrthoReminders
    Public updateno As Integer = 0

    Private Sub SearchPatientwithLast(ByVal strlast As String)
        Try

            Dim y As Integer = 0
            opencon()
            lvSearchPatient.Items.Clear()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@plast", strlast & "%")

            Cmd.CommandText = "SELECT p.PatientID, i.LName, i.FName, i.MName, i.Bday, p.DateStarted FROM patient p, patient_info i WHERE i.LName LIKE @plast AND p.Type = 'Orthodontics' AND p.InfoID = i.InfoID"
            Dr = Cmd.ExecuteReader

            If Dr.HasRows Then
                While Dr.Read
                    lvSearchPatient.Items.Add(Dr(0))
                    For x As Integer = 1 To 5
                        lvSearchPatient.Items(y).SubItems.Add(Dr(x))
                    Next
                    y += 1
                End While
                y = 23 * y
                With lvSearchPatient
                    .Columns(0).Width = 120
                    .Columns(1).Width = 132
                    .Columns(2).Width = 129
                    .Columns(3).Width = 53
                    .Columns(4).Width = 109
                    .SetBounds(224, 219, 548, 26 + y)
                    .BringToFront()
                    .Visible = True
                    .Focus()
                    .TopItem.Selected = True
                End With
            Else
                lvSearchPatient.Visible = False
                txtLast.Focus()
                txtLast.SelectAll()
            End If


            Dr.Close()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ViewAllChargeDesc(ByVal accntno As String)
        Try
            opencon()
            lvChargeDesc1.Items.Clear()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@accnt", accntno)
            Cmd.CommandText = "SELECT DISTINCT ServiceDesc FROM request WHERE PatientID = @accnt"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                lvChargeDesc1.Items.Add(Dr(0))
            End While
            Dr.Close()
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AddReminder()
        Try
            updateno = 0
            Dim cntentry, cnt As Integer
            Dim datestart As Date = Date.Parse(txtDate.Text)
            cntentry = 0
            opencon()
            For x As Integer = 0 To lvChargeDesc.Items.Count - 1

                With Cmd.Parameters
                    .Clear()
                    .AddWithValue("@accnt", txtAccntNo.Text)
                    .AddWithValue("@desc", lvChargeDesc.Items(x).Text)
                    .AddWithValue("@datestart", datestart)
                    .AddWithValue("@reminder", txtReminder.Text)
                    .AddWithValue("@datet", dtpDate.Value.ToString("yyyy-MM-dd"))
                    .AddWithValue("@timet", dtpTime.Value)
                    .AddWithValue("@status", 0)
                    .AddWithValue("@empid", global_empid)
                    .AddWithValue("@date", Now)
                End With
                Cmd.CommandText = "INSERT INTO ortho_reminder(PatientID, ServiceDesc, PrevDate, Reminders, DateTrigger, TimeTrigger, " _
                    & "Status, EmpID, Date) VALUES(@accnt, @desc, @datestart, @reminder, @datet, @timet, @status, @empid, @date)"
                cnt = Cmd.ExecuteNonQuery()

                cntentry += cnt

            Next
            MsgBox(cntentry & " reminder has been added.")

            closecon()

            If MsgBox("New Transaction?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                btnClear.PerformClick()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub AddReminderList()
        Try
            Dim y As Integer = 0
            opencon()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@accnt", txtAccntNo.Text)
            Cmd.CommandText = "SELECT DISTINCT Reminders, ServiceDesc, DateTrigger, TimeTrigger, Date, No FROM ortho_reminder WHERE PatientID = @accnt"
            Dr = Cmd.ExecuteReader

            If Dr.HasRows = True Then
                
                With ViewReminderList
                    .lbAccntNo.Text = txtAccntNo.Text
                    .lvReminder.Items.Clear()
                    While Dr.Read
                        .lvReminder.Items.Add(Dr(0))

                        .lvReminder.Items(y).SubItems.Add(Dr(1))
                        .lvReminder.Items(y).SubItems.Add(Dr.GetDateTime(2).ToString("MM/dd/yyyy"))
                        .lvReminder.Items(y).SubItems.Add(Dr(3).ToString)
                        .lvReminder.Items(y).SubItems.Add(Dr.GetDateTime(4).ToString("MM/dd/yyyy"))
                        .lvReminder.Items(y).SubItems.Add(Dr(5).ToString)

                        y += 1
                    End While
                    .Show()
                    .BringToFront()
                End With
            Else
                MsgBox("No previous record to be displayed.")
            End If
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub UpdateReminder()
        Try
            opencon()

            Dim cnt, cnt1 As Integer
            cnt1 = 0
            For x As Integer = 0 To lvChargeDesc.Items.Count - 1
                With Cmd.Parameters
                    .Clear()
                    .AddWithValue("@no", updateno)
                    .AddWithValue("@patientid", txtAccntNo.Text)
                    .AddWithValue("@servicedesc", lvChargeDesc.Items(x).Text)
                    .AddWithValue("@reminder", txtReminder.Text)
                    .AddWithValue("@datet", dtpDate.Value.ToString("yyyy-MM-dd"))
                    .AddWithValue("@timet", dtpTime.Value)
                    .AddWithValue("@empid", global_empid)
                    .AddWithValue("@date", Now)

                    Cmd.CommandText = "UPDATE ortho_reminder SET ServiceDesc=@servicedesc, Reminders=@reminder, DateTrigger=@datet, TimeTrigger=@timet, Status=0, EmpID=@empid, Date=@date WHERE PatientID=@patientid AND No=@no"
                    cnt = Cmd.ExecuteNonQuery

                    cnt1 += cnt

                End With
            Next
            
            closecon()

            MsgBox(cnt1 & " record has been updated.")

            btnClear.PerformClick()
            btnRefresh.PerformClick()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub DeleteReminder()
        Try
            opencon()
            Dim cnt, cnt1 As Integer
            cnt1 = 0
            For x As Integer = 0 To lvChargeDesc.Items.Count - 1
                With Cmd.Parameters
                    .Clear()
                    .AddWithValue("@patientid", txtAccntNo.Text)
                    .AddWithValue("@no", updateno)

                    Cmd.CommandText = "DELETE FROM ortho_reminder WHERE PatientID=@patientid AND No=@no"
                    cnt = Cmd.ExecuteNonQuery
                    cnt1 += cnt
                End With
            Next

            closecon()

            MsgBox(cnt1 & " record has been deleted.")

            btnClear.PerformClick()
            btnRefresh.PerformClick()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        Me.Focus()
        txtLast.Focus()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtAccntNo.Text = ""
        txtLast.Text = ""
        txtFirst.Text = ""
        txtMI.Text = ""
        lvChargeDesc.Items.Clear()
        lvChargeDesc1.Items.Clear()
        txtReminder.Text = ""
        txtLast.Focus()
        lvSearchPatient.Visible = False
        updateno = 0
    End Sub

    Private Sub lvSearchPatient_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvSearchPatient.KeyDown
        If e.KeyData = Keys.Enter Then
            updateno = 0
            With lvSearchPatient
                txtAccntNo.Text = .SelectedItems.Item(0).Text
                txtLast.Text = .SelectedItems.Item(0).SubItems(1).Text
                txtFirst.Text = .SelectedItems.Item(0).SubItems(2).Text
                txtMI.Text = .SelectedItems.Item(0).SubItems(3).Text
                txtDate.Text = .SelectedItems.Item(0).SubItems(5).Text
                Dim date1 As Date = Date.Parse(.SelectedItems.Item(0).SubItems(4).Text)
                Dim ageyear As Integer
                ageyear = DateDiff(DateInterval.Year, date1, Now)
                If Now.Month < date1.Month Then
                    ageyear -= 1
                End If
                txtAge.Text = ageyear
            End With
            lvSearchPatient.Visible = False
            ViewAllChargeDesc(txtAccntNo.Text)
        End If
    End Sub

    Private Sub OrthoReminders_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lvChargeDesc.Columns(0).Width = 625
    End Sub

    Private Sub txtLast_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLast.KeyDown
        If e.KeyData = Keys.Enter Then
            SearchPatientwithLast(txtLast.Text)
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        If Not txtAccntNo.Text = "" And Not lvChargeDesc.Items.Count = 0 And updateno = 0 Then
            If MsgBox("Finish Transaction?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "REMINDERS") = MsgBoxResult.Yes Then
                AddReminder()
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Not txtAccntNo.Text = "" Then
            AddReminderList()
        End If
    End Sub

    Private Sub btnMvRyt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMvRyt.Click

        Dim i As Integer = 0
        If Not lvChargeDesc1.Items.Count = 0 Then
            If Not lvChargeDesc1.SelectedItems.Count = 0 Then
                For x As Integer = 0 To lvChargeDesc.Items.Count - 1
                    If lvChargeDesc1.SelectedItems.Item(0).SubItems(0).Text = lvChargeDesc.Items(x).SubItems(0).Text Then
                        lvChargeDesc1.SelectedItems.Item(0).Remove()
                        x = lvChargeDesc.Items.Count
                    Else
                        i += 1
                    End If
                Next
                If i <= lvChargeDesc1.Items.Count Then
                    lvChargeDesc.Items.Add(lvChargeDesc1.SelectedItems.Item(0).Text)
                    lvChargeDesc1.SelectedItems.Item(0).Remove()
                ElseIf lvChargeDesc.Items.Count = 0 Then
                    'Else
                    lvChargeDesc.Items.Add(lvChargeDesc1.SelectedItems.Item(0).Text)
                    lvChargeDesc1.SelectedItems.Item(0).Remove()
                End If
            Else
                For x As Integer = 0 To lvChargeDesc.Items.Count - 1
                    If lvChargeDesc1.Items(0).SubItems(0).Text = lvChargeDesc.Items(x).SubItems(0).Text Then
                        lvChargeDesc1.Items(0).Remove()
                        x = lvChargeDesc.Items.Count
                    Else
                        i += 1
                    End If
                Next
                If i <= lvChargeDesc1.Items.Count Then
                    lvChargeDesc.Items.Add(lvChargeDesc1.Items(0).Text)
                    lvChargeDesc1.Items(0).Remove()
                ElseIf lvChargeDesc.Items.Count = 0 Then
                    'Else
                    lvChargeDesc.Items.Add(lvChargeDesc1.Items(0).Text)
                    lvChargeDesc1.Items(0).Remove()
                End If
            End If

        End If
    End Sub

    Private Sub btnMvRytAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMvRytAll.Click
        If Not lvChargeDesc1.Items.Count = 0 Then
            For x As Integer = 0 To lvChargeDesc1.Items.Count - 1
                lvChargeDesc.Items.Add(lvChargeDesc1.Items(0).SubItems(0).Text)
                lvChargeDesc1.Items(0).Remove()
            Next
            
        End If
    End Sub

    Private Sub btnMvLft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMvLft.Click
        Dim i As Integer = 0
        If Not lvChargeDesc.Items.Count = 0 Then
            If Not lvChargeDesc.SelectedItems.Count = 0 Then
                For x As Integer = 0 To lvChargeDesc1.Items.Count - 1
                    If lvChargeDesc.SelectedItems.Item(0).Text = lvChargeDesc1.Items(x).Text Then
                        lvChargeDesc.SelectedItems.Item(0).Remove()
                        x = lvChargeDesc1.Items.Count
                    Else
                        i += 1
                    End If
                Next
                If i = lvChargeDesc.Items.Count Then
                    lvChargeDesc1.Items.Add(lvChargeDesc.SelectedItems.Item(0).Text)
                    lvChargeDesc.SelectedItems.Item(0).Remove()
                ElseIf lvChargeDesc1.Items.Count = 0 Then
                    'Else
                    lvChargeDesc1.Items.Add(lvChargeDesc.SelectedItems.Item(0).Text)
                    lvChargeDesc.SelectedItems.Item(0).Remove()
                End If
            Else
                For x As Integer = 0 To lvChargeDesc1.Items.Count - 1
                    If lvChargeDesc.Items(0).Text = lvChargeDesc1.Items(x).Text Then
                        lvChargeDesc.Items(0).Remove()
                        x = lvChargeDesc1.Items.Count
                    Else
                        i += 1
                    End If
                Next
                If i = lvChargeDesc.Items.Count Then
                    lvChargeDesc1.Items.Add(lvChargeDesc.Items(0).Text)
                    lvChargeDesc.Items(0).Remove()
                ElseIf lvChargeDesc1.Items.Count = 0 Then

                    lvChargeDesc1.Items.Add(lvChargeDesc.Items(0).Text)
                    lvChargeDesc.Items(0).Remove()
                End If
            End If

        End If
    End Sub

    Private Sub btnMvLftAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMvLftAll.Click

        If Not lvChargeDesc.Items.Count = 0 Then
            For x As Integer = 0 To lvChargeDesc.Items.Count - 1
                    lvChargeDesc1.Items.Add(lvChargeDesc.Items(0).SubItems(0).Text)
                lvChargeDesc.Items(0).Remove()
            Next
        End If
    End Sub

    Private Sub lvSearchPatient_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvSearchPatient.LostFocus
        lvSearchPatient.Visible = False
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If Not updateno = 0 Then
            If MsgBox("Are you sure you want to update this record?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "UPDATING") = MsgBoxResult.Yes Then
                UpdateReminder()
            End If

        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Not updateno = 0 Then
            If MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "REMOVING") = MsgBoxResult.Yes Then
                DeleteReminder()
            End If
        End If
    End Sub

End Class