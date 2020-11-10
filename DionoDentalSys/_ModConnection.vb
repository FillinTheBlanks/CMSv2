
Imports MySql.Data
Imports MySql.Data.MySqlClient

Module _ModConnection
    Public Con As New MySqlConnection
    Public Cmd As New MySqlCommand
    Public Dr As MySqlDataReader
    Public Da As New MySqlDataAdapter
    Public Ds As New DataSet
    Public Con1 As New MySqlConnection
    Public Cmd1 As New MySqlCommand
    Public Dr1 As MySqlDataReader
    Public Da1 As New MySqlDataAdapter
    Public Ds1 As New DataSet


    Public strcomm As String = "drop view if exists January;" _
                             & "drop view if exists February;" _
                             & "drop view if exists March;" _
                             & "drop view if exists April;" _
                             & "drop view if exists May;" _
                             & "drop view if exists June;" _
                             & "drop view if exists July;" _
                             & "drop view if exists August;" _
                             & "drop view if exists September;" _
                             & "drop view if exists October;" _
                             & "drop view if exists November;" _
                             & "drop view if exists December;" _
                            & "CREATE VIEW January AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')')  As jan, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=1;" _
                            & "CREATE VIEW February AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')') As feb, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=2;" _
                            & "CREATE VIEW March AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')') As march, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=3;" _
                            & "CREATE VIEW April AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')') As april, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=4;" _
                            & "CREATE VIEW May AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')') As may, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=5;" _
                            & "CREATE VIEW June AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')') As june, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=6;" _
                            & "CREATE VIEW July AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')') As july, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=7;" _
                            & "CREATE VIEW August AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')') As aug, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=8;" _
                            & "CREATE VIEW September AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')') As sept, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=9;" _
                            & "CREATE VIEW October AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')') As oct, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=10;" _
                            & "CREATE VIEW November AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')') As nov, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=11;" _
                            & "CREATE VIEW December AS " _
                            & "SELECT CONCAT(i.LName,', ',i.FName,' ', i.MName,'.(',DAY(p.DateStarted),')') As decem, p.Type as type, YEAR(p.DateStarted) as year FROM patient_info i, patient p WHERE p.InfoID = i.InfoID AND MONTH(p.DateStarted)=12;"

    Public Constring As String = "server=localhost;User Id=root;database=dentaldb"
    'Public Constring As String = "server=172.16.12.2;User Id=walkin;Password=kevsys;database=dentaldb"

    Public Sub opencon()
        Con.ConnectionString = Constring
        Con.Open()
        Cmd.Connection = Con
    End Sub

    Public Sub closecon()
        Con.Close()
        Con.ConnectionString = Nothing
        Cmd.CommandText = Nothing
    End Sub

    Public Sub callforms(ByVal frmname As System.Windows.Forms.Form, ByVal pnlname As System.Windows.Forms.Panel)
        frmname.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        frmname.TopLevel = False
        frmname.ShowInTaskbar = False
        frmname.Show()
        frmname.Dock = DockStyle.Fill
        pnlname.Controls.Clear()
        pnlname.Controls.Add(frmname)
    End Sub

    Public Sub suggestiontext(ByVal Qry As String, ByVal Txtactive As System.Windows.Forms.TextBox)
        Try
            opencon()
            Dim dt As New DataTable
            Dim nds As New DataSet
            nds.Tables.Add(dt)
            Dim nDA As New MySqlDataAdapter(Qry, Con)
            Dim r As DataRow

            nDA.Fill(dt)
            Txtactive.AutoCompleteCustomSource.Clear()
            For Each r In dt.Rows
                Txtactive.AutoCompleteCustomSource.Add(r.Item(0).ToString)
            Next

            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Sub NumberValidate(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txtNumVal As System.Windows.Forms.TextBox)
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
        ElseIf Char.IsDigit(keyChar) Then
            Dim text = txtNumVal.Text
            Dim selectionStart = txtNumVal.SelectionStart
            Dim selectionLength = txtNumVal.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 4 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Public Sub NumberValidate10(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txtNumVal As System.Windows.Forms.TextBox)
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
        ElseIf Char.IsDigit(keyChar) Then
            Dim text = txtNumVal.Text
            Dim selectionStart = txtNumVal.SelectionStart
            Dim selectionLength = txtNumVal.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 10 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Public Sub NumberValidatewithDecimal(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txtNumVal As System.Windows.Forms.TextBox)
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
        ElseIf Char.IsDigit(keyChar) OrElse keyChar = "."c Then
            Dim text = txtNumVal.Text
            Dim selectionStart = txtNumVal.SelectionStart
            Dim selectionLength = txtNumVal.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 9 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub
End Module

Module PatientInfo

    Public Sub patient_info()
        Dim x As Integer
        Dim y As String = ""
        Dim hold As String = ""
        Dim holdcat As String = ""

        Try
            opencon()

            Cmd.CommandText = "SELECT MAX(InfoID) FROM patient_info"
            Dr = Cmd.ExecuteReader

            If Dr.Read Then
                hold = Dr(0)
            End If

            holdcat = hold.Substring(1, 7)
            x = Integer.Parse(holdcat)
            x += 1

            If (x < 10) Then
                y = "I000000" & x
            ElseIf (x >= 10 And x < 100) Then
                y = "I00000" & x
            ElseIf (x >= 100 And x < 1000) Then
                y = "I0000" & x
            ElseIf (x >= 1000 And x < 10000) Then
                y = "I000" & x
            ElseIf (x >= 10000 And x < 100000) Then
                y = "I00" & x
            ElseIf (x >= 100000 And x < 1000000) Then
                y = "I0" & x
            ElseIf (x >= 1000000 And x < 10000000) Then
                y = "I" & x
            End If

            PatientRecord.txtPatientID.Text = y

            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub account_no()
        Dim x As Integer = 0
        Dim y As String = ""
        Dim hold As String = ""
        Dim holdcat As String = ""

        Try
            opencon()

            Cmd.CommandText = "SELECT MAX(PatientID) FROM patient"
            Dr = Cmd.ExecuteReader

            If Dr.Read Then
                hold = Dr(0)
            End If
            If hold = "" Then
                x = 0
            Else
                holdcat = hold.Substring(1, 7)
                x = Integer.Parse(holdcat)
            End If

            x += 1

            If (x < 10) Then
                y = "A000000" & x
            ElseIf (x >= 10 And x < 100) Then
                y = "A00000" & x
            ElseIf (x >= 100 And x < 1000) Then
                y = "A0000" & x
            ElseIf (x >= 1000 And x < 10000) Then
                y = "A000" & x
            ElseIf (x >= 10000 And x < 100000) Then
                y = "A00" & x
            ElseIf (x >= 100000 And x < 1000000) Then
                y = "A0" & x
            ElseIf (x >= 1000000 And x < 10000000) Then
                y = "A" & x
            End If

            PatientAccntForm.txtAccntNo.Text = y

            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub supplies_no()
        Dim x As Integer = 0
        Dim y As String = ""
        Dim hold As String = ""
        Dim holdcat As String = ""

        Try
            opencon()

            Cmd.CommandText = "SELECT MAX(SuppID) FROM supplies"
            Dr = Cmd.ExecuteReader
            If Dr.Read Then
                hold = Dr(0)
            End If
            If hold = "" Then
                x = 0
            Else
                holdcat = hold.Substring(1, 7)
                x = Integer.Parse(holdcat)
            End If

            x += 1


            If (x < 10) Then
                y = "S000000" & x
            ElseIf (x >= 10 And x < 100) Then
                y = "S00000" & x
            ElseIf (x >= 100 And x < 1000) Then
                y = "S0000" & x
            ElseIf (x >= 1000 And x < 10000) Then
                y = "S000" & x
            ElseIf (x >= 10000 And x < 100000) Then
                y = "S00" & x
            ElseIf (x >= 100000 And x < 1000000) Then
                y = "S0" & x
            ElseIf (x >= 1000000 And x < 10000000) Then
                y = "S" & x
            End If

            AddSuppliesInfo.txtSuppNo.Text = y

            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub ListofDoctors(ByVal cmbDoctor As System.Windows.Forms.ComboBox)
        Try
            opencon()
            cmbDoctor.Items.Clear()
            Cmd.CommandText = "SELECT * FROM doctor"
            Dr = Cmd.ExecuteReader
            While Dr.Read
                cmbDoctor.Items.Add(Dr(0))
            End While

            Dr.Close()
            closecon()
            cmbDoctor.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

End Module

Module Security
    Public global_empid, global_emplname, global_empfname, global_empmname, global_pos As String

    Public Sub SignIn(ByVal strempid As String, ByVal strpass As String)
        Try
            opencon()
            Cmd.Parameters.Clear()

            With Cmd.Parameters
                .AddWithValue("@empid", strempid)
                .AddWithValue("@pass", strpass)
            End With

            Cmd.CommandText = "SELECT EmpID, LName, FName, MName, Position FROM employee WHERE EmpID = @empid AND PWord LIKE @pass"
            Dr = Cmd.ExecuteReader

            If Dr.Read Then
                global_empid = Dr(0).ToString
                global_emplname = Dr(1)
                global_empfname = Dr(2)
                global_empmname = Dr(3)
                global_pos = Dr(4)
                With MainModule
                    .tsEmpName.Text = global_emplname & ", " & global_empfname & " " & global_empmname & ". (" & global_pos & ")"
                    .PatientToolStripMenuItem.Enabled = True
                    .SignOutToolStripMenuItem.Enabled = True
                    .ServicesToolStripMenuItem.Enabled = True
                    .SuppliesToolStripMenuItem.Enabled = True
                    .EmployeeToolStripMenuItem.Enabled = True
                    .ReportsToolStripMenuItem.Enabled = True
                    .ExpensesToolStripMenuItem.Enabled = True
                    .SignInToolStripMenuItem.Enabled = False
                    .ExitToolStripMenuItem.Enabled = False
                    .Timer2.Start()
                    .Timer2.Interval = 1000
                End With
                closecon()

                callforms(PatientMonitoring, MainModule.pnlMain)
                MainModule.loadForms(MainModule.tsProgressBar)
                PatientMonitoring.btnRefresh.PerformClick()
            Else
                MsgBox("Incorrect Employee ID or Password.")
                MainModule.tsEmpName.Text = ""
                With LoginForm
                    .txtPassword.Focus()
                    .txtPassword.SelectAll()
                End With

            End If

            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

End Module
