Public Class OrthoBracket
    Dim brno As Integer = 0

    Private Sub SearchPatientwithLast(ByVal strlast As String)
        Try

            Dim y As Integer = 0
            opencon()
            lvSearchPatient.Items.Clear()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@plast", strlast & "%")

            Cmd.CommandText = "SELECT p.PatientID, i.LName, i.FName, i.MName, i.Bday FROM patient p, patient_info i WHERE i.LName LIKE @plast AND p.Type = 'Orthodontics' AND p.InfoID = i.InfoID"
            Dr = Cmd.ExecuteReader

            If Dr.HasRows Then
                While Dr.Read
                    lvSearchPatient.Items.Add(Dr(0))
                    For x As Integer = 1 To 4
                        lvSearchPatient.Items(y).SubItems.Add(Dr(x))
                    Next
                    y += 1
                End While
                y = 23 * y
                With lvSearchPatient
                    .Columns(0).Width = 116
                    .Columns(1).Width = 132
                    .Columns(2).Width = 125
                    .Columns(3).Width = 60
                    .Columns(4).Width = 112
                    .SetBounds(254, 239, 551, 26 + y)
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

    Private Sub ViewAllOrthoBracket()
        Try
            Dim y As Integer = 0
            lvBracket.Items.Clear()
            opencon()
            Cmd.CommandText = "SELECT b.PatientID, i.LName, i.FName, i.MName, b.BracketUsed, b.UnusedBracket FROM ortho_bracket b, patient_info i, patient p WHERE b.PatientID = p.PatientID AND p.InfoID = i.InfoID"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                With lvBracket
                    .Items.Add(Dr(0))
                    .Items(y).SubItems.Add(Dr(1) & ", " & Dr(2) & " " & Dr(3) & ".")
                    .Items(y).SubItems.Add(Dr(4))
                    .Items(y).SubItems.Add(Dr(5))
                End With

                y += 1
            End While
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub SearchBracket()
        Try
            opencon()
            With Cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@accnt", txtAccntNo.Text)

                .CommandText = "SELECT * FROM ortho_bracket WHERE PatientID = @accnt"
                Dr = Cmd.ExecuteReader
                If Dr.HasRows Then
                    btnAdd.Enabled = False
                    btnUpdate.Enabled = True
                    btnDelete.Enabled = True

                    If Dr.Read Then
                        brno = Dr(0)
                        txtBracketUsed.Text = Dr(2)
                        txtUnusedBracket.Text = Dr(3)
                    End If
                Else
                    btnAdd.Enabled = True
                    btnUpdate.Enabled = False
                    btnDelete.Enabled = False
                End If

                
                Dr.Close()

            End With

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AddNewBracketRecord()
        If Not txtAccntNo.Text = "" Then
            Try
                opencon()
                With Cmd.Parameters
                    .Clear()
                    .AddWithValue("@accnt", txtAccntNo.Text)
                    .AddWithValue("@use", txtBracketUsed.Text)
                    .AddWithValue("@unuse", txtUnusedBracket.Text)
                End With
                Cmd.CommandText = "INSERT INTO ortho_bracket(PatientID, BracketUsed, UnusedBracket) VALUES(@accnt, @use, @unuse)"
                Dim cnt As Integer = Cmd.ExecuteNonQuery()

                MsgBox(cnt & " record has been added.")
                closecon()
                btnRefresh.PerformClick()
                btnClear.PerformClick()


            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        End If
    End Sub

    Private Sub UpdateBracketRecord()
        If Not txtAccntNo.Text = "" Then
            Try
                opencon()
                With Cmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@no", brno)
                    .Parameters.AddWithValue("@accnt", txtAccntNo.Text)
                    .Parameters.AddWithValue("@use", txtBracketUsed.Text)
                    .Parameters.AddWithValue("@unuse", txtUnusedBracket.Text)
                    .CommandText = "UPDATE ortho_bracket SET BracketUsed = @use, UnusedBracket = @unuse WHERE No = @no AND PatientID = @accnt"
                    Dim cnt As Integer = Cmd.ExecuteNonQuery()

                    MsgBox(cnt & " record has been updated.")
                End With
                closecon()
                btnRefresh.PerformClick()
                btnClear.PerformClick()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub DeleteBracketRecord()
        Try
            opencon()
            With Cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@no", brno)
                .Parameters.AddWithValue("@accnt", txtAccntNo.Text)
                
                .CommandText = "DELETE FROM ortho_bracket WHERE No = @no AND PatientID = @accnt"
                Dim cnt As Integer = Cmd.ExecuteNonQuery()

                MsgBox(cnt & " record has been deleted.")
            End With
            closecon()
            btnRefresh.PerformClick()
            btnClear.PerformClick()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub txtLast_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLast.KeyDown
        If e.KeyData = Keys.Enter Then
            SearchPatientwithLast(txtLast.Text)
        End If
    End Sub

    Private Sub lvSearchPatient_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvSearchPatient.KeyDown
        If e.KeyData = Keys.Enter Then
            With lvSearchPatient
                txtAccntNo.Text = .SelectedItems.Item(0).Text
                txtLast.Text = .SelectedItems.Item(0).SubItems(1).Text
                txtFirst.Text = .SelectedItems.Item(0).SubItems(2).Text
                txtMI.Text = .SelectedItems.Item(0).SubItems(3).Text
            End With
            SearchBracket()
            lvSearchPatient.Visible = False
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtAccntNo.Text = ""
        txtFirst.Text = ""
        txtLast.Text = ""
        txtMI.Text = ""
        txtBracketUsed.Text = ""
        txtUnusedBracket.Text = ""
        brno = 0
        txtLast.Focus()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        ViewAllOrthoBracket()

        btnAdd.Enabled = False
        btnUpdate.Enabled = False
        btnDelete.Enabled = False
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        AddNewBracketRecord()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        UpdateBracketRecord()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteBracketRecord()
    End Sub
End Class