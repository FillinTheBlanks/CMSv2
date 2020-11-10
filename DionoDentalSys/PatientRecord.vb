Public Class PatientRecord
    Public updatebool As Boolean = False

    Private Sub AddPatientRecord()
        Dim sex As String = ""
        Dim recnt As Integer = 0
        If cmbSex.SelectedIndex = 0 Then
            sex = "M"
        Else
            sex = "F"
        End If
        Try

            opencon()
            Cmd.Parameters.Clear()
            With Cmd.Parameters
                .AddWithValue("@infoid", txtPatientID.Text)
                .AddWithValue("@lname", txtLastName.Text)
                .AddWithValue("@fname", txtFirstName.Text)
                .AddWithValue("@mi", txtMI.Text)
                .AddWithValue("@sex", sex)
                .AddWithValue("@bday", dtpBirthDate.Value.ToString("yyyy-MM-dd"))
                .AddWithValue("@addr", txtAddress.Text)
                .AddWithValue("@contact", txtContactNo.Text)
                .AddWithValue("@email", txtEmailAdd.Text)
                .AddWithValue("@occupy", txtOccupation.Text)
            End With

            Cmd.CommandText = "INSERT patient_info VALUES(@infoid, @lname, @fname, @mi, @sex, @bday, @addr, @contact, @email, @occupy)"
            recnt = Cmd.ExecuteNonQuery()

            MsgBox(recnt & " record has been successfully added.")
            closecon()
            updatebool = True

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        
    End Sub

    Private Sub ValidatePatientInfo()
        Try
            opencon()
            Cmd.Parameters.Clear()
            With Cmd.Parameters
                .AddWithValue("@pid", txtPatientID.Text)
            End With
            Cmd.CommandText = "SELECT * FROM patient_info WHERE InfoID = @pid"
            Dr = Cmd.ExecuteReader

            If Dr.Read Then
                MsgBox("Duplicate Patient ID.")
                Dr.Read()
                closecon()
                btnRefresh.PerformClick()
            Else
                Dr.Read()
                closecon()

                AddPatientRecord()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ValidateAccountInfo()
        Try

            opencon()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@pid", txtPatientID.Text)
            Cmd.CommandText = "SELECT * FROM patient WHERE InfoID = @pid"
            Dr = Cmd.ExecuteReader
            If Dr.HasRows Then


                While Dr.Read
                    With PatientAccntForm

                        .txtAccntNo.Text = Dr(0)
                        .cmbType.SelectedItem = Dr(2)
                        .txtDiagnosis.Text = Dr(3)
                        Dim date1 As Date = Date.Parse(Dr(4).ToString)
                        .dtpDateStarted.Value = date1
                        
                        If Not Dr(5).ToString = "1753-01-01" Then
                            Dim date2 As Date = Date.Parse(Dr(5).ToString)
                            .dtpDateEnded.Value = date2
                        Else
                            .dtpDateEnded.Value = Now

                            .chkDisable.Checked = True

                        End If

                        .cmbStatus.SelectedItem = Dr(6)
                        .txtComment.Text = Dr(7)
                    End With
                End While
                'PatientAccntForm.btnAdd.Enabled = False
            Else
                Exit Sub
                closecon()

            End If
            Dr.Close()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
            closecon()
        End Try

    End Sub

    Private Sub UpdateRecord()
        If Not txtPatientID.Text = "" Then
            Try
                opencon()
                With Cmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@pid", txtPatientID.Text)
                    .CommandText = "SELECT * FROM patient_info WHERE InfoID = @pid"
                    Dr = Cmd.ExecuteReader
                    If Dr.Read Then

                        If Dr.HasRows = True Then
                            Dr.Close()

                            .Parameters.AddWithValue("@last", txtLastName.Text)
                            .Parameters.AddWithValue("@first", txtFirstName.Text)
                            .Parameters.AddWithValue("@mid", txtMI.Text)
                            .Parameters.AddWithValue("@address", txtAddress.Text)
                            .Parameters.AddWithValue("@contact", txtContactNo.Text)
                            .Parameters.AddWithValue("@email", txtEmailAdd.Text)
                            .Parameters.AddWithValue("@occupy", txtOccupation.Text)
                            .Parameters.AddWithValue("@sex", cmbSex.SelectedItem)
                            .Parameters.AddWithValue("@bday", dtpBirthDate.Value.ToString("yyyy-MM-dd"))

                            .CommandText = "UPDATE patient_info SET LName=@last, FName=@first, MName=@mid, Gender=@sex, Bday=@bday, Address=@address, ContactNo=@contact, EmailAdd=@email, Occupation=@occupy WHERE InfoID=@pid"
                            Dim cnt As Integer = .ExecuteNonQuery
                            MsgBox(cnt & " record has been updated.")
                        Else
                            Dr.Close()
                            MsgBox("No record to be updated.")
                        End If
                    End If
                   
                End With


                closecon()

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub DeleteRecord()
        If Not txtPatientID.Text = "" Then
            Try
                opencon()
                With Cmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@pid", txtPatientID.Text)
                    .CommandText = "SELECT * FROM patient_info WHERE InfoID = @pid"
                    Dr = Cmd.ExecuteReader
                    If Dr.Read Then

                        If Dr.HasRows = True Then
                            Dr.Close()
                            If MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                .CommandText = "DELETE FROM patient_info WHERE InfoID=@pid"
                                Dim cnt As Integer = .ExecuteNonQuery
                                MsgBox(cnt & " record has been deleted.")
                                closecon()

                                btnClear.PerformClick()
                                btnRefresh.PerformClick()
                            End If
                            
                        Else
                            Dr.Close()
                            MsgBox("No record to be deleted.")
                        End If
                    End If

                End With
                closecon()

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        patient_info()
        updatebool = False
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtLastName.Text = ""
        txtFirstName.Text = ""
        txtMI.Text = ""
        txtContactNo.Text = ""
        txtAddress.Text = ""
        txtOccupation.Text = ""
        txtEmailAdd.Text = ""
        cmbSex.SelectedIndex = 0
        dtpBirthDate.Value = Now
    End Sub

    Private Sub PatientRecord_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbSex.SelectedIndex = 0
        'txtPatientID.Enabled = False
        suggestiontext("SELECT DISTINCT LName FROM patient_info LIMIT 100", txtLastName)
        suggestiontext("SELECT DISTINCT FName FROM patient_info LIMIT 100", txtFirstName)
        suggestiontext("SELECT Address FROM patient_info LIMIT 50", txtAddress)

        btnClear.PerformClick()
        btnRefresh.PerformClick()
        btnDelete.Enabled = False
        btnUpdate.Enabled = False
        updatebool = False
        txtLastName.Focus()
        txtLastName.SelectAll()
        'btnUpdate.Enabled = False
        'btnDelete.Enabled = False
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Not txtLastName.Text = "" And Not txtFirstName.Text = "" And updatebool = False Then
            ValidatePatientInfo()
        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        UpdateRecord()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteRecord()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SearchForm.Show()
    End Sub

    Private Sub btnAccnt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccnt.Click
        If updatebool = True Then
            callforms(PatientAccntForm, MainModule.pnlMain)
            PatientAccntForm.Button1.PerformClick()
            PatientAccntForm.btnRefresh.PerformClick()

            With PatientAccntForm
                .txtPatientID.Text = txtPatientID.Text
                .txtLastName.Text = txtLastName.Text
                .txtFirstName.Text = txtFirstName.Text
                .txtMI.Text = txtMI.Text

            End With
            btnRefresh.PerformClick()
            closecon()
            ValidateAccountInfo()


            Me.Dispose()
            Me.Close()
        End If
    End Sub

    Private Sub dtpBirthDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpBirthDate.ValueChanged
        Dim ageyear As Integer
        ageyear = DateDiff(DateInterval.Year, dtpBirthDate.Value, Now)
        If Now.Month < dtpBirthDate.Value.Month Then
            ageyear -= 1
        End If
        txtAge.Text = ageyear
    End Sub

End Class