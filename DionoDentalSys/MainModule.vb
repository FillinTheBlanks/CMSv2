Imports Microsoft.Office.Interop
'Imports Excel = Microsoft.Office.Interop.Excel

Public Class MainModule
    Dim xlApp As New Excel.Application
    Dim xlWorkBook As Excel.Workbook
    Dim sectoclear As Integer = 0

    Private Function AlreadyRunning() As Boolean
        Dim my_proc As Process = Process.GetCurrentProcess
        Dim my_name As String = my_proc.ProcessName
        Dim procs() As Process = Process.GetProcessesByName(my_name)

        If procs.Length = 1 Then Return False

        Dim i As Integer
        For i = 0 To procs.Length - 1
            If procs(i).StartTime < my_proc.StartTime Then _
                Return False
        Next i

        Return False
    End Function

    Public Sub loadForms(ByVal toolStripProgressBar As ToolStripProgressBar)
        With toolStripProgressBar
            .Value = 0

            Threading.Thread.Sleep(10) '// FOR TESTING.
            .Value = 10

            Threading.Thread.Sleep(10) '// FOR TESTING.
            .Value = 50

            Threading.Thread.Sleep(10) '// FOR TESTING.
            .Value = 70

            Threading.Thread.Sleep(10) '// FOR TESTING.

            .Value = 100
            Threading.Thread.Sleep(10)

            .Value = 0
        End With
    End Sub

    Public Sub DeletePreviousReminder()
        opencon()

        Cmd.Parameters.Clear()
        Cmd.Parameters.AddWithValue("@date", Now.ToString("yyyy-MM-dd"))
        Cmd.CommandText = "DELETE FROM ortho_reminder WHERE DateTrigger < @date"
        Cmd.ExecuteNonQuery()

        Dim cntortho As Integer = 0
        With Cmd
            .CommandText = "SELECT COUNT(PatientID) FROM patient WHERE Type='Orthodontics'"
            Dr = .ExecuteReader

            If Dr.Read Then
                cntortho = Dr(0)
            End If
            Dr.Close()

            Dim patientID(cntortho) As String
            Dim datestart(cntortho) As Date

            Dim cnt As Integer = 0
            .CommandText = "SELECT PatientID,DateStarted FROM patient WHERE Type='Orthodontics'"
            Dr = .ExecuteReader
            Dim daypat As Integer = 0
            Dim yearpat As Integer = 0
            Dim monpat As Integer = 0
            While Dr.Read
                patientID(cnt) = Dr(0).ToString
                datestart(cnt) = Dr(1)
                cnt += 1
            End While

            'MsgBox(cntortho.ToString & " " & cnt.ToString)

            Dr.Close()
            Dim dateforw As Date
            'GENERATE REMINDERS FOR ORTHODONTICS PATIENT

            For cnt = 0 To cntortho - 1 Step 1
                Dim datenew, datesub, datemon, dateyear As Date

                datemon = Now.AddMonths(1)

                If datemon.Month = 1 Then
                    dateyear = Now.AddYears(1)
                ElseIf dateyear.Year < Now.Year Then
                    dateyear = Now
                End If
                daypat = datestart(cnt).Day
                yearpat = dateyear.Year
                monpat = datemon.Month

                If monpat = 4 Or 6 Or 9 Or 11 Then
                    If daypat > 30 Then
                        daypat = 30
                    End If
                End If


                If monpat = 2 Then
                    If daypat > 28 Then
                        daypat = 28
                    End If
                End If


                If monpat = 1 Or 3 Or 5 Or 7 Or 8 Or 10 Or 12 Then
                    If daypat > 31 Then
                        daypat = 31
                    End If
                End If

                'MsgBox(daypat & "/" & monpat & "/" & yearpat)
                dateforw = New Date(yearpat, monpat, daypat)

                If dateforw.DayOfWeek.ToString().Equals("Sunday") Then
                    datenew = dateforw.AddDays(2)
                ElseIf dateforw.DayOfWeek.ToString().Equals("Monday") Then
                    datenew = dateforw.AddDays(1)
                Else
                    datenew = dateforw
                End If

                datesub = datenew.AddDays(-7)

                .Parameters.Clear()
                .Parameters.AddWithValue("@pid", patientID(cnt))
                .Parameters.AddWithValue("@sdesc", "None")
                .Parameters.AddWithValue("@datestart", datenew)
                .Parameters.AddWithValue("@datet", datesub)
                .Parameters.AddWithValue("@reminder", "Next session is on " & datenew.ToShortDateString)
                .Parameters.AddWithValue("@timet", "09:00:00")
                .Parameters.AddWithValue("@status", 0)
                .Parameters.AddWithValue("@empid", global_empid)
                .Parameters.AddWithValue("@datenow", Now)

                .CommandText = "INSERT INTO ortho_reminder(PatientID,ServiceDesc,PrevDate,Reminders,DateTrigger,TimeTrigger,Status,EmpID,`Date`) SELECT * FROM (SELECT @pid,@sdesc,@datestart,@reminder,@datet,@timet,@status,@empid,@datenow) As tmp WHERE NOT EXISTS (SELECT PatientID FROM ortho_reminder WHERE PatientID=@pid) LIMIT 1"
                .ExecuteNonQuery()

            Next

            'MsgBox(cnt.ToString)
            cnt = 0
            cntortho = 0
            .CommandText = "SELECT COUNT(PatientID) FROM patient WHERE Not Type='Orthodontics'"
            Dr = .ExecuteReader

            If Dr.Read Then
                cntortho = Dr(0)
            End If
            Dr.Close()

            Dim pID(cntortho) As String
            Dim dstart(cntortho) As Date

            .CommandText = "SELECT PatientID,DateStarted FROM patient WHERE Not Type='Orthodontics'"
            Dr = .ExecuteReader

            While Dr.Read
                pID(cnt) = Dr(0).ToString
                dstart(cnt) = Dr(1)
                cnt += 1
            End While
            Dr.Close()

            Dim cntortho1 As Integer = 0
            .CommandText = "select DISTINCT r.PatientID, MAX(r.Date) from request r, patient p WHERE r.PatientID = p.PatientID AND Not p.Type = 'Orthodontics' GROUP BY r.PatientID"
            Dr = .ExecuteReader

            While Dr.Read
                cntortho1 += 1
            End While
            Dr.Close()

            Dim prID(cntortho1) As String
            Dim date1(cntortho1) As Date
            Dim diffdate(cntortho1) As Integer

            cnt = 0
            .CommandText = "select DISTINCT r.PatientID, MAX(r.Date) from request r, patient p WHERE r.PatientID = p.PatientID AND Not p.Type = 'Orthodontics' GROUP BY r.PatientID"
            Dr = .ExecuteReader

            While Dr.Read
                prID(cnt) = Dr(0).ToString
                date1(cnt) = Dr(1)
                diffdate(cnt) = DateDiff(DateInterval.Month, date1(cnt), Now)

                cnt += 1
            End While

            Dr.Close()
            cnt = 0
            'GENERATE REMINDERS FOR NON ORTHODONTICS PATIENT

            For x As Integer = 0 To cntortho - 1
                For y As Integer = 0 To cntortho1 - 1
                    If pID(x).Equals(prID(y)) Then
                        If diffdate(y) >= 6 Then
                            Dim datenew, datesub, datemon, dateyear As Date
                            'Dim dateforw As Date
                            datemon = Now.AddMonths(1)
                            If datemon.Month = 1 Then
                                dateyear = Now.AddYears(1)
                            ElseIf dateyear.Year < Now.Year Then
                                dateyear = Now
                            End If

                            daypat = dstart(x).Day
                            yearpat = dateyear.Year
                            monpat = datemon.Month

                            If monpat = 4 Or 6 Or 9 Or 11 Then
                                If daypat > 30 Then
                                    daypat = 30
                                End If
                            End If


                            If monpat = 2 Then
                                If daypat > 28 Then
                                    daypat = 28
                                End If
                            End If


                            If monpat = 1 Or 3 Or 5 Or 7 Or 8 Or 10 Or 12 Then
                                If daypat > 31 Then
                                    daypat = 31
                                End If
                            End If

                            dateforw = New Date(yearpat, monpat, daypat)

                            If dateforw.DayOfWeek.ToString().Equals("Sunday") Then
                                datenew = dateforw.AddDays(2)
                            ElseIf dateforw.DayOfWeek.ToString().Equals("Monday") Then
                                datenew = dateforw.AddDays(1)
                            Else
                                datenew = dateforw
                            End If

                            datesub = datenew.AddDays(-7)
                            .Parameters.Clear()
                            .Parameters.AddWithValue("@pid", pID(x))
                            .Parameters.AddWithValue("@sdesc", "None")
                            .Parameters.AddWithValue("@datestart", datenew)
                            .Parameters.AddWithValue("@datet", datesub)
                            .Parameters.AddWithValue("@reminder", "Next session is on " & datenew.ToShortDateString)
                            .Parameters.AddWithValue("@timet", "09:00:00")
                            .Parameters.AddWithValue("@status", 0)
                            .Parameters.AddWithValue("@empid", global_empid)
                            .Parameters.AddWithValue("@datenow", Now)

                            .CommandText = "INSERT INTO ortho_reminder(PatientID,ServiceDesc,PrevDate,Reminders,DateTrigger,TimeTrigger,Status,EmpID,`Date`) SELECT * FROM (SELECT @pid,@sdesc,@datestart,@reminder,@datet,@timet,@status,@empid,@datenow) As tmp WHERE NOT EXISTS (SELECT PatientID FROM ortho_reminder WHERE PatientID=@pid) LIMIT 1"
                            .ExecuteNonQuery()
                            Exit For
                        End If
                    End If
                Next
            Next

            cntortho = 0
            cntortho1 = 0
            Cmd.CommandText = "SELECT PatientID,COUNT(PatientID) FROM ortho_reminder GROUP BY PatientID HAVING COUNT(*) > 1"
            Dr = Cmd.ExecuteReader
            While Dr.Read
                cntortho += 1
            End While
            Dr.Close()

            Dim strPatientID(cntortho) As String
            Dim intCount(cntortho) As Integer

            Cmd.CommandText = "SELECT PatientID,COUNT(PatientID) FROM ortho_reminder GROUP BY PatientID HAVING COUNT(*) > 1"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                strPatientID(cntortho1) = Dr(0)
                intCount(cntortho1) = Dr(1)
                cntortho1 += 1
            End While
            Dr.Close()

            For cntortho1 = 0 To cntortho - 1
                With Cmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@id", strPatientID(cntortho1))
                    .Parameters.AddWithValue("@cnt", intCount(cntortho1) - 1)

                    .CommandText = "DELETE FROM ortho_reminder WHERE PatientID=@id LIMIT @cnt"
                    .ExecuteNonQuery()
                End With
            Next


            closecon()

        End With
    End Sub

    Public Sub DisableMenu1()
        PatientToolStripMenuItem.Enabled = False
        SignOutToolStripMenuItem.Enabled = False
        ServicesToolStripMenuItem.Enabled = False
        SuppliesToolStripMenuItem.Enabled = False
        EmployeeToolStripMenuItem.Enabled = False
        ReportsToolStripMenuItem.Enabled = False
        ExpensesToolStripMenuItem.Enabled = False
    End Sub

    Private Sub CheckReminder()
        Dim tspan As TimeSpan = Now.TimeOfDay
        Try
            opencon()
            'MsgBox(tspan.ToString("hh\:mm"))

            With Cmd.Parameters
                .Clear()
                .AddWithValue("@date", Now.ToString("yyyy-MM-dd"))
                .AddWithValue("@time", tspan.ToString("hh\:mm") & "%")
            End With

            Cmd.CommandText = "SELECT o.No, o.PatientID, i.LName, i.FName, i.MName, o.ServiceDesc, o.Reminders, o.TimeTrigger FROM ortho_reminder o, patient p, patient_info i WHERE o.DateTrigger = @date AND o.TimeTrigger LIKE @time AND o.Status = 0 AND o.PatientID = p.PatientID AND p.InfoID = i.InfoID"
            Dr = Cmd.ExecuteReader
            While Dr.Read
                If Dr.HasRows = True Then

                    With ReminderInfo
                        .reminderno = Dr(0)
                        .lbAccount.Text = Dr(1)
                        .lbName.Text = Dr(2) & ", " & Dr(3) & " " & Dr(4)
                        .lbProcedure.Text = Dr(5)
                        .lbReminder.Text = Dr(6)
                        .timetrig = Dr(7)
                        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Hand)
                        .Show()
                        .BringToFront()
                        .Focus()
                    End With

                Else
                    closecon()
                    Exit Sub
                End If
            End While

            Dr.Close()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub UpdateDTR(ByVal strid As String)
        Try
            lvDTR.Items.Clear()
            opencon()
            Dim sIn1, sOut1, sIn2, sOut2, sIn3, sOut3 As String
            Dim sId As Integer = 0
            With Cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@empid", strid)
                .Parameters.AddWithValue("@date", Now.ToString("yyyy-MM-dd"))
                .Parameters.AddWithValue("@day", Now.ToString("dd"))
                .Parameters.AddWithValue("@time", Now.ToString("HH:mm tt"))
                '.Parameters.AddWithValue("@time", Now.ToString("hh\:mm") & ":00")
                .CommandText = "SELECT * FROM dtr_employee WHERE EmpID=@empid AND Date=@date"
                Dr = .ExecuteReader

                If Dr.Read Then
                    If Dr.HasRows = True Then
                        sId = Dr(0)
                        sIn1 = Dr(4).ToString
                        sOut1 = Dr(5).ToString
                        sIn2 = Dr(6).ToString
                        sOut2 = Dr(7).ToString
                        sIn3 = Dr(8).ToString
                        sOut3 = Dr(9).ToString

                        Dr.Close()
                        .Parameters.AddWithValue("@no", sId)

                        If sIn1.Equals("") Then
                            .CommandText = "UPDATE dtr_employee SET In1=@time WHERE no=@no"
                            .ExecuteNonQuery()
                        ElseIf sOut1.Equals("") Then
                            .CommandText = "UPDATE dtr_employee SET Out1=@time WHERE no=@no"
                            .ExecuteNonQuery()
                        ElseIf sIn2.Equals("") Then
                            .CommandText = "UPDATE dtr_employee SET In2=@time WHERE no=@no"
                            .ExecuteNonQuery()
                        ElseIf sOut2.Equals("") Then
                            .CommandText = "UPDATE dtr_employee SET Out2=@time WHERE no=@no"
                            .ExecuteNonQuery()
                        ElseIf sIn3.Equals("") Then
                            .CommandText = "UPDATE dtr_employee SET In3=@time WHERE no=@no"
                            .ExecuteNonQuery()
                        ElseIf sOut3.Equals("") Then
                            .CommandText = "UPDATE dtr_employee SET Out3=@time WHERE no=@no"
                            .ExecuteNonQuery()
                        End If


                    End If
                Else
                    Dr.Close()
                    .CommandText = "INSERT INTO dtr_employee(EmpID,Date,Day,In1) VALUES(@empid,@date,@day,@time)"
                    .ExecuteNonQuery()
                End If

                'This will display all the dtr records on all the day of the current month
                Dim lastDay As DateTime = New DateTime(Now.Year, Now.Month, 1)
                .Parameters.AddWithValue("@lstday", lastDay.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd"))
                .Parameters.AddWithValue("@frstday", lastDay.ToString("yyyy-MM-dd"))

                .CommandText = "SELECT Date,In1,Out1,In2,Out2,In3,Out3 FROM dtr_employee WHERE EmpID=@empid AND Date BETWEEN @frstday AND @lstday ORDER BY Date ASC"
                Dr = Cmd.ExecuteReader
                Dim y As Integer = 0
                While Dr.Read
                    lvDTR.Items.Add(Dr(0))
                    For x As Integer = 1 To 6
                        If Dr(x).ToString.Equals("") Then
                            lvDTR.Items(y).SubItems.Add(" ")
                        Else
                            Dim ts As TimeSpan = Dr.GetTimeSpan(x)
                            Dim dt As DateTime = DateTime.ParseExact(ts.ToString(), "HH:mm:ss", Nothing)

                            lvDTR.Items(y).SubItems.Add(dt.ToString("hh:mm tt"))
                        End If

                    Next

                    'Dim tIn1 As TimeSpan = TimeSpan.Parse(Dr(1))
                    'Dim tOut1 As TimeSpan = TimeSpan.Parse(Dr(2))
                    'Dim totalTime As TimeSpan = tIn1.Subtract(tOut1)
                    'MsgBox(totalTime.ToString("hh"))
                    y += 1
                End While
                Dr.Close()
                Timer3.Interval = 1000
                Timer3.Start()


            End With

            closecon()
            txtEmpID.Text = ""
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Function GetDayforMonday(ByVal inputDate As Date)
        Dim daysAway As Integer
        daysAway = inputDate.DayOfWeek - DayOfWeek.Monday
        Return inputDate.AddDays(-daysAway).ToString("yyyy-MM-dd")
    End Function

    Private Function GetDayforSunday(ByVal inputDate As Date)
        Dim daysAway As Integer
        daysAway = inputDate.DayOfWeek - DayOfWeek.Sunday
        Return inputDate.AddDays(-daysAway).ToString("yyyy-MM-dd")
    End Function

    Private Sub InsertAllDateofMonth(ByVal empid As String)
        Dim startDay As System.DateTime = New System.DateTime(Now.Year, Now.Month, 1)
        Dim lastDay As System.DateTime = startDay.AddMonths(1).AddDays(-1)
        Dim newValue As System.DateTime = startDay

        Try
            opencon()

            With Cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@empid", empid)
                .Parameters.AddWithValue("@chkdate", startDay.ToString("yyyy-MM") & "-%")


                .CommandText = "SELECT * FROM dtr_employee WHERE Date LIKE @chkdate AND EmpID = @empid"
                Dr = .ExecuteReader

                If Dr.Read Then
                    Dr.Close()


                    While newValue <= lastDay
                        .Parameters.Clear()
                        .Parameters.AddWithValue("@empid", empid)
                        .Parameters.AddWithValue("@date", newValue.ToString("yyyy-MM-dd"))
                        .Parameters.AddWithValue("@day", newValue.ToString("dd"))

                        .CommandText = "SELECT * FROM dtr_employee WHERE Date = @date AND EmpID = @empid"
                        Dr = .ExecuteReader

                        If Dr.Read Then
                            Dr.Close()
                        Else
                            Dr.Close()

                            '.CommandText = "INSERT INTO dtr_employee(EmpID,Date,Day,In1,Out1,In2,Out2,In3,Out3,Remarks) VALUES(@empid,@date,@day,'','','','','','','')"
                            .CommandText = "INSERT INTO dtr_employee(EmpID,Date,Day) VALUES(@empid,@date,@day)"
                            .ExecuteNonQuery()
                        End If
                        newValue = newValue.AddDays(1)
                    End While

                Else
                    Dr.Close()

                    While newValue <= lastDay
                        .Parameters.Clear()
                        .Parameters.AddWithValue("@empid", empid)
                        .Parameters.AddWithValue("@date", newValue.ToString("yyyy-MM-dd"))
                        .Parameters.AddWithValue("@day", newValue.ToString("dd"))
                        '.CommandText = "INSERT INTO dtr_employee(EmpID,Date,Day,In1,Out1,In2,Out2,In3,Out3,Remarks) VALUES(@empid,@date,@day,'','','','','','','')"
                        .CommandText = "INSERT INTO dtr_employee(EmpID,Date,Day) VALUES(@empid,@date,@day)"
                        .ExecuteNonQuery()
                        newValue = newValue.AddDays(1)
                    End While

                End If
            End With



            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        'Put Remarks as Monday
        newValue = startDay

        While newValue <= lastDay
            Try
                opencon()
                With Cmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@empid", empid)
                    .Parameters.AddWithValue("@date", GetDayforMonday(newValue))
                    .CommandText = "SELECT * FROM dtr_employee WHERE EmpID=@empid AND Date=@date"
                    Dr = .ExecuteReader

                    If Dr.Read Then
                        Dr.Close()

                        .CommandText = "UPDATE dtr_employee SET Remarks='Monday' WHERE EmpID=@empid AND Date=@date"
                        .ExecuteNonQuery()
                    Else
                        Dr.Close()
                    End If

                End With

                closecon()

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

            newValue = newValue.AddDays(7)
        End While
        'SELECT In1, TIME_FORMAT(In1, '%h:%i %p') FROM `dtr_employee`
        'Put Remarks as Sunday
        newValue = startDay

        While newValue <= lastDay
            Try
                opencon()
                With Cmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@empid", empid)
                    .Parameters.AddWithValue("@date", GetDayforSunday(newValue))
                    .CommandText = "SELECT * FROM dtr_employee WHERE EmpID=@empid AND Date=@date"
                    Dr = .ExecuteReader

                    If Dr.Read Then
                        Dr.Close()

                        .CommandText = "UPDATE dtr_employee SET Remarks='Sunday' WHERE EmpID=@empid AND Date=@date"
                        .ExecuteNonQuery()
                    Else
                        Dr.Close()
                    End If

                End With

                closecon()

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

            newValue = newValue.AddDays(7)
        End While
    End Sub

    Private Sub MainModule_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Timer1.Stop()
    End Sub

    Private Sub MainModule_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = e.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = e.CloseReason

        If UnloadMode = CloseReason.UserClosing Then
            Cancel = True
        End If
        e.Cancel = Cancel
    End Sub


    Private Sub MainModule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Loading.Show()
        'Loading.BringToFront()
        'Loading.Focus()
        Me.Hide()

        DeletePreviousReminder()

        'Loading.Close()
        'Loading.Dispose()
        Me.Show()

        If AlreadyRunning() Then
            MessageBox.Show( _
                "Another instance is already running.", _
                "Already Running", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Exclamation)
            Me.Close()
        End If

        Timer1.Start()
        Timer1.Interval = 1000
        callforms(LoginForm, pnlMain)
        DisableMenu1()
        suggestiontext("SELECT EmpID FROM employee", txtEmpID)
        LoginForm.txtEmpID.Focus()
        LoginForm.txtEmpID.SelectAll()
        lbEmpName.Text = ""
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        tsDateTime.Text = Now.ToString
    End Sub

    Private Sub SignInToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SignInToolStripMenuItem.Click
        callforms(LoginForm, pnlMain)
        With LoginForm
            .txtEmpID.Text = ""
            .txtPassword.Text = ""
            .txtEmpID.Focus()
        End With
        DisableMenu1()

    End Sub

    Private Sub SignOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SignOutToolStripMenuItem.Click

        loadForms(tsProgressBar)

        global_empid = ""
        global_emplname = ""
        global_empfname = ""
        global_empmname = ""
        global_pos = ""
        tsEmpName.Text = ""
        DisableMenu1()
        ExitToolStripMenuItem.Enabled = True
        SignInToolStripMenuItem_Click(sender, e)
        Timer2.Stop()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        If MsgBox("Are you sure you want to close the session?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "EXIT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub PatientRecordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatientRecordToolStripMenuItem.Click
        callforms(PatientRecord, Me.pnlMain)
        loadForms(tsProgressBar)
    End Sub

    Private Sub ViewRecordsSchedulesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewRecordsSchedulesToolStripMenuItem.Click
        callforms(PatientMonitoring, pnlMain)
        loadForms(tsProgressBar)
        PatientMonitoring.btnRefresh.PerformClick()
    End Sub

    Private Sub AddServiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddServiceToolStripMenuItem.Click
        callforms(ServiceForm, pnlMain)
        loadForms(tsProgressBar)
        ServiceForm.btnRefresh.PerformClick()
    End Sub

    Private Sub PatientChargesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatientChargesToolStripMenuItem.Click
        callforms(AddPatientCharges, pnlMain)
        loadForms(tsProgressBar)
        AddPatientCharges.btnRefresh.PerformClick()
    End Sub

    Private Sub OrthodonticsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrthodonticsToolStripMenuItem.Click
        callforms(OrthoReminders, pnlMain)
        loadForms(tsProgressBar)
        OrthoReminders.btnClear.PerformClick()
        OrthoReminders.btnRefresh.PerformClick()
    End Sub

    Private Sub PatientBracketsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatientBracketsToolStripMenuItem.Click
        callforms(OrthoBracket, pnlMain)
        loadForms(tsProgressBar)
        OrthoBracket.btnClear.PerformClick()
        OrthoBracket.btnRefresh.PerformClick()
    End Sub

    Private Sub AddSuppliesRecordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddSuppliesRecordToolStripMenuItem.Click
        callforms(AddSuppliesInfo, pnlMain)
        loadForms(tsProgressBar)
        AddSuppliesInfo.btnClear.PerformClick()
        AddSuppliesInfo.btnRefresh.PerformClick()
    End Sub

    Private Sub ViewSuppliesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewSuppliesToolStripMenuItem.Click
        callforms(ViewSupplies, pnlMain)
        loadForms(tsProgressBar)
        ViewSupplies.btnRefresh.PerformClick()
    End Sub

    Private Sub DeliveredSuppliesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeliveredSuppliesToolStripMenuItem.Click
        callforms(DeliveredSupplies, pnlMain)
        loadForms(tsProgressBar)
        DeliveredSupplies.btnRefresh.PerformClick()
        DeliveredSupplies.btnClear.PerformClick()
    End Sub

    Private Sub SuppliesPaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SuppliesPaymentToolStripMenuItem.Click
        callforms(SuppliesPayment, pnlMain)
        loadForms(tsProgressBar)
        SuppliesPayment.rdoCash.Checked = True
    End Sub

    Private Sub PatientAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatientAccountToolStripMenuItem.Click
        callforms(PatientAccntForm, pnlMain)
        loadForms(tsProgressBar)
        PatientAccntForm.btnRefresh.PerformClick()
        PatientAccntForm.Button1.PerformClick()
    End Sub

    Private Sub EmployeeInformationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeeInformationToolStripMenuItem.Click
        callforms(EmployeeInfo, pnlMain)
        loadForms(tsProgressBar)
        EmployeeInfo.btnRefresh.PerformClick()
    End Sub

    Private Sub IncomeStatementToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles IncomeStatementToolStripMenuItem.Click
        callforms(IncomeStatement, pnlMain)
        loadForms(tsProgressBar)
        IncomeStatement.btnRefresh.PerformClick()
    End Sub

    Private Sub AddExpensesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddExpensesToolStripMenuItem.Click
        callforms(ExpenseForm, pnlMain)
        loadForms(tsProgressBar)
        ExpenseForm.btnClear.PerformClick()
        ExpenseForm.btnRefresh.PerformClick()
    End Sub

    Private Sub ViewExpensesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewExpensesToolStripMenuItem.Click
        callforms(ViewExpense, pnlMain)
        loadForms(tsProgressBar)
        ViewExpense.btnView.PerformClick()
        ViewExpense.btnRefresh.PerformClick()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If Con.State = ConnectionState.Closed Then
            CheckReminder()
        End If
    End Sub

    Private Sub ExpenseReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpenseReportToolStripMenuItem.Click
        callforms(ViewExpense, pnlMain)
        loadForms(tsProgressBar)
        ViewExpense.btnView.PerformClick()
        ViewExpense.btnRefresh.PerformClick()
    End Sub

    Private Sub InventoryReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InventoryReportToolStripMenuItem.Click
        loadForms(tsProgressBar)
        InventoryRptViewer.PrintDirect()
    End Sub

    Private Sub PatientRecordToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatientRecordToolStripMenuItem1.Click
        SelectPatientReport.Show()
        SelectPatientReport.BringToFront()
    End Sub

    Private Sub SuppliesPaymentReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SuppliesPaymentReportToolStripMenuItem.Click
        SelectSupplierPaymentReport.Show()
        SelectSupplierPaymentReport.BringToFront()
    End Sub

    Private Sub ViewServiceRecordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewServiceRecordToolStripMenuItem.Click
        callforms(ViewServiceRecord, pnlMain)
        'ViewServiceRecord.RefreshRecord()

        loadForms(tsProgressBar)
    End Sub

    Private Sub txtEmpID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEmpID.KeyDown
        If e.KeyData = Keys.Enter Then
            If Not txtEmpID.Text = "" Then
                InsertAllDateofMonth(txtEmpID.Text)

                Try
                    opencon()
                    With Cmd
                        .Parameters.Clear()
                        .Parameters.AddWithValue("@empid", txtEmpID.Text)

                        .CommandText = "SELECT * FROM employee WHERE EmpID = @empid"
                        Dr = .ExecuteReader
                    End With
                    If Dr.Read Then
                        lbEmpName.Text = Dr.GetString("LName") & ", " & Dr.GetString("FName") & " " & Dr.GetString("MName") & "."
                        If Dr.HasRows = True Then
                            closecon()

                            UpdateDTR(txtEmpID.Text)
                        End If
                    Else
                        closecon()
                        MsgBox("Incorrect Employee ID.")
                        txtEmpID.Focus()
                        txtEmpID.SelectAll()
                    End If

                    closecon()

                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        sectoclear += 1
        If sectoclear = 10 Then
            lvDTR.Items.Clear()
            sectoclear = 0
            Timer3.Stop()
            lbEmpName.Text = ""
            txtEmpID.Text = ""
            txtEmpID.Focus()
        End If
    End Sub

    Private Sub btnViewDTR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewDTR.Click
        ViewDTR.Show()
        ViewDTR.BringToFront()

        If ViewDTR.ShowInTaskbar = True Then
            ViewDTR.BringToFront()
            ViewDTR.Focus()
        End If

    End Sub

    Private Sub PatientStatisticalReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatientStatisticalReportToolStripMenuItem.Click
        SelectYear.Show()
        SelectYear.BringToFront()

        'PrintPatientStatistics()
    End Sub

    Public Sub PrintPatientStatistics(ByVal selYear As Integer)
        opencon()
        With Cmd.Parameters
            .Clear()
            .AddWithValue("@year", selYear)
        End With
        Cmd.CommandText = strcomm
        Cmd.ExecuteNonQuery()

        SelectYear.Close()
        SelectYear.Dispose()
        'xlApp = New Excel._ExcelApplication
        Dim cntcol As Integer = 2
        Dim strmonth As String = ""
        Dim xlSheets As Excel.Sheets
        Dim xlSheet, xlSheet1, xlSheet2, xlSheet3 As Excel._Worksheet

        'Dim range As Excel.Range
        '~~> Display Excel

        xlApp = New Excel.Application()
        xlWorkBook = xlApp.Workbooks.Add
        xlSheets = xlWorkBook.Worksheets
        xlSheets.Add()
        xlSheet = xlSheets(1)
        xlSheet.Name = "General Statistical Report"
        xlSheet.Tab.Color = Color.DarkGreen
        With xlSheet.Range("A1", "X1")
            .Font.Bold = True
            .RowHeight = 25

            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
        End With
        '' Add table headers going cell by cell.
        xlSheet.Cells(1, 1).Value = "January"
        xlSheet.Cells(1, 2).Value = "February"
        xlSheet.Cells(1, 3).Value = "March"
        xlSheet.Cells(1, 4).Value = "April"
        xlSheet.Cells(1, 5).Value = "May"
        xlSheet.Cells(1, 6).Value = "June"
        xlSheet.Cells(1, 7).Value = "July"
        xlSheet.Cells(1, 8).Value = "August"
        xlSheet.Cells(1, 9).Value = "September"
        xlSheet.Cells(1, 10).Value = "October"
        xlSheet.Cells(1, 11).Value = "November"
        xlSheet.Cells(1, 12).Value = "December"

        For x As Integer = 1 To 12
            strmonth = MonthName(x)
            Cmd.CommandText = "SELECT * FROM " & strmonth & " WHERE year =@year"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                xlSheet.Cells(cntcol, x).Value = Dr(0)
                cntcol += 1

            End While
            Dr.Close()
            cntcol = 2
        Next


        xlSheet.Range("A1:X1").EntireColumn.AutoFit()

        'Next sheet for orthodontic patients
        xlSheet1 = xlSheets(2)
        xlSheet1.Name = "Orthodontics"
        xlSheet1.Tab.Color = Color.Red

        With xlSheet1.Range("A1", "X1")
            .Font.Bold = True
            .RowHeight = 25

            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
        End With
        '' Add table headers going cell by cell.
        xlSheet1.Cells(1, 1).Value = "January"
        xlSheet1.Cells(1, 2).Value = "February"
        xlSheet1.Cells(1, 3).Value = "March"
        xlSheet1.Cells(1, 4).Value = "April"
        xlSheet1.Cells(1, 5).Value = "May"
        xlSheet1.Cells(1, 6).Value = "June"
        xlSheet1.Cells(1, 7).Value = "July"
        xlSheet1.Cells(1, 8).Value = "August"
        xlSheet1.Cells(1, 9).Value = "September"
        xlSheet1.Cells(1, 10).Value = "October"
        xlSheet1.Cells(1, 11).Value = "November"
        xlSheet1.Cells(1, 12).Value = "December"


        For x As Integer = 1 To 12
            strmonth = MonthName(x)
            Cmd.CommandText = "SELECT * FROM " & strmonth & " WHERE type LIKE 'Orthodontics' AND year =@year"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                xlSheet1.Cells(cntcol, x).Value = Dr(0)
                cntcol += 1

            End While
            Dr.Close()
            cntcol = 2
        Next

        xlSheet1.Range("A1:X1").EntireColumn.AutoFit()


        'Next sheet for walk-in patients
        xlSheet2 = xlSheets(3)
        xlSheet2.Name = "Walk-In"
        xlSheet2.Tab.Color = Color.Gold

        With xlSheet2.Range("A1", "X1")
            .Font.Bold = True
            .RowHeight = 25

            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
        End With
        '' Add table headers going cell by cell.
        xlSheet2.Cells(1, 1).Value = "January"
        xlSheet2.Cells(1, 2).Value = "February"
        xlSheet2.Cells(1, 3).Value = "March"
        xlSheet2.Cells(1, 4).Value = "April"
        xlSheet2.Cells(1, 5).Value = "May"
        xlSheet2.Cells(1, 6).Value = "June"
        xlSheet2.Cells(1, 7).Value = "July"
        xlSheet2.Cells(1, 8).Value = "August"
        xlSheet2.Cells(1, 9).Value = "September"
        xlSheet2.Cells(1, 10).Value = "October"
        xlSheet2.Cells(1, 11).Value = "November"
        xlSheet2.Cells(1, 12).Value = "December"


        For x As Integer = 1 To 12
            strmonth = MonthName(x)
            Cmd.CommandText = "SELECT * FROM " & strmonth & " WHERE type LIKE 'Walk-In' AND year =@year"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                xlSheet2.Cells(cntcol, x).Value = Dr(0)
                cntcol += 1

            End While
            Dr.Close()
            cntcol = 2
        Next

        xlSheet2.Range("A1:X1").EntireColumn.AutoFit()

        'Next sheet for walk-in patients
        xlSheet3 = xlSheets(4)
        xlSheet3.Name = "OFW"
        xlSheet3.Tab.Color = Color.DarkBlue

        With xlSheet3.Range("A1", "X1")
            .Font.Bold = True
            .RowHeight = 25

            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
        End With
        '' Add table headers going cell by cell.
        xlSheet3.Cells(1, 1).Value = "January"
        xlSheet3.Cells(1, 2).Value = "February"
        xlSheet3.Cells(1, 3).Value = "March"
        xlSheet3.Cells(1, 4).Value = "April"
        xlSheet3.Cells(1, 5).Value = "May"
        xlSheet3.Cells(1, 6).Value = "June"
        xlSheet3.Cells(1, 7).Value = "July"
        xlSheet3.Cells(1, 8).Value = "August"
        xlSheet3.Cells(1, 9).Value = "September"
        xlSheet3.Cells(1, 10).Value = "October"
        xlSheet3.Cells(1, 11).Value = "November"
        xlSheet3.Cells(1, 12).Value = "December"


        For x As Integer = 1 To 12
            strmonth = MonthName(x)
            Cmd.CommandText = "SELECT * FROM " & strmonth & " WHERE type LIKE 'OFW' AND year =@year"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                xlSheet3.Cells(cntcol, x).Value = Dr(0)
                cntcol += 1

            End While
            Dr.Close()
            cntcol = 2
        Next

        xlSheet3.Range("A1:X1").EntireColumn.AutoFit()

        'For the viewing and releasing of excel workbook
        xlApp.Visible = True

        Using sfd As New SaveFileDialog
            sfd.FileName = selYear & ".xlsx"
            If sfd.ShowDialog() = DialogResult.OK Then
                xlWorkBook.SaveAs(sfd.FileName)
                MessageBox.Show(sfd.FileName)
            End If
        End Using

        'xlWorkBook.Close()
        'xlApp.Quit()

        ReleaseObject(xlApp)
        ReleaseObject(xlWorkBook)
        ReleaseObject(xlSheet)
        ReleaseObject(xlSheet1)
        ReleaseObject(xlSheet2)
        ReleaseObject(xlSheet3)
        closecon()

    End Sub

    Private Sub ReleaseObject(ByVal o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub

    Private Sub ViewReminderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewReminderToolStripMenuItem.Click
        callforms(GenReminderList, pnlMain)
        GenReminderList.DisplayAllReminder()

    End Sub

    Private Sub DeleteAndCreateReminderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteAndCreateReminderToolStripMenuItem.Click
        If MsgBox("Are you sure you want to DELETE and CREATE Patient Reminder?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Loading.Show()
            Loading.TopMost = True
            DeletePreviousReminder()
            Loading.Close()
        End If
    End Sub

    Private Sub AboutUsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutUsToolStripMenuItem.Click
        AboutUs.Show()
        AboutUs.BringToFront()
        AboutUs.TopMost = True
    End Sub

    Private Sub DentalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DentalToolStripMenuItem.Click

    End Sub
End Class
