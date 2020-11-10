Public Class SearchForm

    Private Sub ViewAllPatientInfo()
        Try
            opencon()
            lvViewPatient.Items.Clear()
            Dim x, y As Integer
            Cmd.CommandText = "SELECT InfoID, LName, FName, MName, Bday, Address FROM patient_info"
            Dr = Cmd.ExecuteReader
            y = 0

            While Dr.Read
                lvViewPatient.Items.Add(Dr(0))

                For x = 1 To 5
                    lvViewPatient.Items(y).SubItems.Add(Dr(x))
                Next
                y += 1
            End While
            Dr.Close()

            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ViewAllPatientInfowithLast(ByVal strlast As String)
        Try
            opencon()
            lvViewPatient.Items.Clear()
            Cmd.Parameters.Clear()
            Dim x, y As Integer

            Cmd.Parameters.AddWithValue("@lname", strlast & "%")
            Cmd.CommandText = "SELECT InfoID, LName, FName, MName, Bday, Address FROM patient_info WHERE LName LIKE @lname"
            Dr = Cmd.ExecuteReader
            y = 0

            While Dr.Read
                lvViewPatient.Items.Add(Dr(0))

                For x = 1 To 5
                    lvViewPatient.Items(y).SubItems.Add(Dr(x))
                Next
                y += 1
            End While
            Dr.Close()

            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AcceptPatientInfo()
        If Not lvViewPatient.SelectedItems.Item(0).Text = "" Then
            Try
                opencon()
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@pid", lvViewPatient.SelectedItems.Item(0).SubItems(0).Text)
                Cmd.CommandText = "SELECT Gender, ContactNo, EmailAdd, Occupation FROM patient_info WHERE InfoID = @pid"
                Dr = Cmd.ExecuteReader

                With PatientRecord
                    .txtPatientID.Text = lvViewPatient.SelectedItems.Item(0).SubItems(0).Text
                    .txtLastName.Text = lvViewPatient.SelectedItems.Item(0).SubItems(1).Text
                    .txtFirstName.Text = lvViewPatient.SelectedItems.Item(0).SubItems(2).Text
                    .txtMI.Text = lvViewPatient.SelectedItems.Item(0).SubItems(3).Text
                    .dtpBirthDate.Value = lvViewPatient.SelectedItems.Item(0).SubItems(4).Text
                    .txtAddress.Text = lvViewPatient.SelectedItems.Item(0).SubItems(5).Text
                    If Dr.Read Then
                        If Dr(0) = "M" Then
                            .cmbSex.SelectedIndex = 0
                        Else
                            .cmbSex.SelectedIndex = 1
                        End If
                        .txtContactNo.Text = Dr(1)
                        .txtEmailAdd.Text = Dr(2)
                        .txtOccupation.Text = Dr(3)
                        .btnUpdate.Enabled = True
                        .btnDelete.Enabled = True
                        .updatebool = True
                    End If
                End With

                
                Dr.Close()

                closecon()
                Me.Dispose()
                Me.Close()
                Timer1.Stop()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub SearchForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ViewAllPatientInfo()
        'Timer1.Start()
        Timer1.Interval = 1000
    End Sub

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtSearch.KeyUp
        If txtSearch.Text = "" Then
            ViewAllPatientInfo()
        Else
            ViewAllPatientInfowithLast(txtSearch.Text)
        End If
    End Sub

    Private Sub lvViewPatient_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvViewPatient.KeyDown
        If e.KeyData = Keys.Enter Then
            AcceptPatientInfo()

        End If
    End Sub

    Private Sub lvViewPatient_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvViewPatient.MouseClick
        AcceptPatientInfo()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.Focused = False Then
            Me.BringToFront()
            Me.Focus()
            Me.TopMost = True
        End If
    End Sub
End Class