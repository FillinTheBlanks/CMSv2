Public Class GenReminderList

    

    Public Sub DisplayAllReminder()
        Try
            opencon()

            Dim cntortho As Integer = 0
            With Cmd
                .CommandText = "select COUNT(DISTINCT PatientID) FROM patient WHERE Not PatientID=''"
                Dr = .ExecuteReader

                While Dr.Read
                    cntortho = Integer.Parse(Dr(0).ToString)
                End While
                Dr.Close()

                Dim patientID(cntortho) As String
                Dim date1(cntortho) As Date
                Dim diffdate(cntortho) As Integer
                MainModule.tsProgressBar.Maximum = cntortho

                MainModule.tsProgressBar.Step = cntortho / 100
                Dim cnt As Integer = 0
                .CommandText = "select DISTINCT PatientID, MAX(Date) FROM request WHERE Not PatientID='' GROUP BY PatientID"
                Dr = .ExecuteReader

                While Dr.Read
                    patientID(cnt) = Dr(0).ToString
                    date1(cnt) = Dr(1)
                    diffdate(cnt) = DateDiff(DateInterval.Month, date1(cnt), Now)

                  
                    cnt += 1
                End While

                Dr.Close()
                cnt = 0

                .CommandText = "SELECT o.PatientID as 'Account#', CONCAT(p.LName,', ',p.FName,' ',p.MName,'.') as 'Name', o.PrevDate as 'Visit Date', o.Reminders as 'Reminder', o.DateTrigger, o.TimeTrigger, o.Status as 'Status', i.Type FROM ortho_reminder o, patient i,patient_info p WHERE o.PatientID = i.PatientID AND i.InfoID = p.InfoID AND Not i.PatientID=''"

                Dr = .ExecuteReader

                lvRemindList.Items.Clear()

                While Dr.Read

                    lvRemindList.Items.Add(Dr(0))
                    For x As Integer = 1 To 7
                        If x = 5 Then
                            lvRemindList.Items(cnt).SubItems.Add(Dr.GetTimeSpan(x).ToString)
                        ElseIf x = 6 Then
                            If Dr.GetInt32(6) = 0 Then
                                lvRemindList.Items(cnt).SubItems.Add("Undisplay")
                            Else
                                lvRemindList.Items(cnt).SubItems.Add("Display")
                            End If
                        Else
                            lvRemindList.Items(cnt).SubItems.Add(Dr(x))
                        End If
                    Next

                    For y As Integer = 0 To cntortho - 1
                        If Dr(0).Equals(patientID(y)) Then
                            If diffdate(y) <= 1 Then
                                lvRemindList.Items(cnt).BackColor = Color.Gold
                                lvRemindList.Items(cnt).ForeColor = Color.Black
                            ElseIf diffdate(y) <= 5 And diffdate(y) >= 2 Then
                                lvRemindList.Items(cnt).BackColor = Color.SpringGreen
                                lvRemindList.Items(cnt).ForeColor = Color.Black
                            ElseIf diffdate(y) >= 6 Then
                                lvRemindList.Items(cnt).BackColor = Color.Crimson
                                lvRemindList.Items(cnt).ForeColor = Color.White
                            End If
                            Exit For
                        Else
                            lvRemindList.Items(cnt).BackColor = Color.Crimson
                            lvRemindList.Items(cnt).ForeColor = Color.White
                        End If



                    Next

                    cnt += 1
                    MainModule.tsProgressBar.Value = cnt
                    'Console.WriteLine(cnt)
                End While

                Dr.Close()

            End With

            closecon()
            MainModule.tsProgressBar.Value = 0
        Catch ex As Exception
            closecon()
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub GenReminderList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub lvRemindList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvRemindList.DoubleClick
        If lvRemindList.SelectedItems.Count > 0 Then
            With UpdatePatientReminder
                .txtAccnt.Text = lvRemindList.SelectedItems(0).Text
                .txtName.Text = lvRemindList.SelectedItems(0).SubItems(1).Text
                .dtpVisit.Value = Date.Parse(lvRemindList.SelectedItems(0).SubItems(2).Text)
                .txtReminder.Text = lvRemindList.SelectedItems(0).SubItems(3).Text
                .dtpADate.Value = Date.Parse(lvRemindList.SelectedItems(0).SubItems(4).Text)
                .dtpATime.Value = Date.Parse(lvRemindList.SelectedItems(0).SubItems(5).Text)
                .visitdate = Date.Parse(lvRemindList.SelectedItems(0).SubItems(2).Text)
                .reminder = lvRemindList.SelectedItems(0).SubItems(3).Text
                .Show()
                .BringToFront()
                .dtpVisit.Focus()
            End With
        End If
    End Sub

    Private Sub lvRemindList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvRemindList.KeyDown
        If e.KeyData = Keys.Enter Then
            If lvRemindList.SelectedItems.Count > 0 Then
                With UpdatePatientReminder
                    .txtAccnt.Text = lvRemindList.SelectedItems(0).Text
                    .txtName.Text = lvRemindList.SelectedItems(0).SubItems(1).Text
                    .dtpVisit.Value = Date.Parse(lvRemindList.SelectedItems(0).SubItems(2).Text)
                    .txtReminder.Text = lvRemindList.SelectedItems(0).SubItems(3).Text
                    .dtpADate.Value = Date.Parse(lvRemindList.SelectedItems(0).SubItems(4).Text)
                    .dtpATime.Value = Date.Parse(lvRemindList.SelectedItems(0).SubItems(5).Text)
                    .visitdate = Date.Parse(lvRemindList.SelectedItems(0).SubItems(2).Text)
                    .reminder = lvRemindList.SelectedItems(0).SubItems(3).Text
                    .Show()
                    .BringToFront()
                    .dtpVisit.Focus()
                End With
            End If
        Else
            Dim itmX As ListViewItem = lvRemindList.FindItemWithText(e.KeyData, True, 0)
            If Not itmX Is Nothing Then
                lvRemindList.Focus()
                itmX.Selected = True
                lvRemindList.Items(itmX.Index).Selected = True
                itmX.EnsureVisible()
            End If
        End If
    End Sub

    Private Sub txtSrch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSrch.KeyDown
        If e.KeyData = Keys.Enter Then
            Dim itmX As ListViewItem = lvRemindList.FindItemWithText(txtSrch.Text, True, 0)
            If Not itmX Is Nothing Then
                lvRemindList.Focus()
                itmX.Selected = True
                lvRemindList.Items(itmX.Index).Selected = True
                itmX.EnsureVisible()
                txtSrch.Focus()
                txtSrch.SelectAll()
            End If
        End If
    End Sub

End Class