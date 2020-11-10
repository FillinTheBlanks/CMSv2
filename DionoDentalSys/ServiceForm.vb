Public Class ServiceForm
    Dim gldesc As String = ""
    Dim glid As Integer = 0
    Dim bup As Boolean = False
    Private Sub ViewService()
        Try
            lvService.Items.Clear()
            Dim y As Integer = 0
            opencon()
            Cmd.CommandText = "SELECT * FROM services"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                If Not Dr(1) = "" Then
                    lvService.Items.Add(Dr(0))
                    lvService.Items(y).SubItems.Add(Dr(1))
                    lvService.Items(y).SubItems.Add(Dr(2))
                    y += 1
                End If
            End While
            Dr.Close()

            closecon()
            bup = False
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub ListCategory()
        Try
            cmbCategory.Items.Clear()
            opencon()
            Cmd.CommandText = "SELECT * FROM service_type"
            Dr = Cmd.ExecuteReader

            While Dr.Read
                cmbCategory.Items.Add(Dr(0))
            End While
            Dr.Close()

            closecon()
            cmbCategory.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub


    Private Sub AddService()
        Try
            opencon()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@service", txtDesc.Text)
            Cmd.Parameters.AddWithValue("@cat", cmbCategory.SelectedItem)

            Cmd.CommandText = "SELECT MAX(ServiceID) FROM services"
            Dr = Cmd.ExecuteReader
            If Dr.Read Then
                Dim serviceid As Integer = Dr(0) + 1
                Dr.Close()
                Cmd.Parameters.AddWithValue("@serviceid", serviceid)
                Cmd.CommandText = "INSERT INTO services VALUES(@serviceid, @service, @cat)"
                Dim cnt As Integer = Cmd.ExecuteNonQuery()
                MsgBox(cnt & " record has been added.")

            End If
            closecon()

            AddPatientCharges.txtChargeDesc.AutoCompleteCustomSource.Add(txtDesc.Text)

            ViewService()

            txtDesc.Focus()
            txtDesc.SelectAll()
            gldesc = ""
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub UpdateService()
        Try
            opencon()
            With Cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@id", glid)
                .Parameters.AddWithValue("@desc", txtDesc.Text)
                .Parameters.AddWithValue("@cat", cmbCategory.SelectedItem)
                .CommandText = "UPDATE services SET ServiceDesc=@desc, Category=@cat WHERE ServiceID=@id"
                Dim i As Integer = .ExecuteNonQuery()
                MsgBox(i & " record has been updated.")
                bup = False
            End With
            closecon()

            AddPatientCharges.txtChargeDesc.AutoCompleteCustomSource.Remove(gldesc)
            AddPatientCharges.txtChargeDesc.AutoCompleteCustomSource.Add(txtDesc.Text)

            ViewService()

            txtDesc.Text = ""
            cmbCategory.SelectedIndex = 0
            txtDesc.Focus()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub DeleteService()
        Try
            opencon()
            With Cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@id", glid)
                .CommandText = "DELETE FROM services WHERE ServiceID=@id"
                Dim i As Integer = .ExecuteNonQuery()
                MsgBox(i & " record has been deleted.")
                bup = False
            End With
            closecon()

            AddPatientCharges.txtChargeDesc.AutoCompleteCustomSource.Remove(gldesc)

            ViewService()

            txtDesc.Text = ""
            cmbCategory.SelectedIndex = 0
            txtDesc.Focus()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ServiceForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtDesc.Text = ""
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Refresh()
        ViewService()
        ListCategory()
        txtDesc.Text = ""
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Not txtDesc.Text = "" Then
            AddService()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        AddServiceCategory.Show()
        AddServiceCategory.BringToFront()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If Not txtDesc.Text = "" And bup = True Then
            UpdateService()
        End If
    End Sub

    Private Sub lvService_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvService.MouseClick
        If lvService.SelectedItems.Count > 0 Then
            txtDesc.Text = lvService.SelectedItems.Item(0).SubItems(1).Text
            cmbCategory.SelectedItem = lvService.SelectedItems.Item(0).SubItems(2).Text
            gldesc = txtDesc.Text
            glid = lvService.SelectedItems.Item(0).Text
            bup = True
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Not txtDesc.Text = "" And bup = True Then
            DeleteService()
        End If
    End Sub
End Class