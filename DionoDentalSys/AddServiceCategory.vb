Public Class AddServiceCategory

    Private Sub AddServiceCategory()
        Try
            opencon()
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@cat", txtCategory.Text)
            Cmd.CommandText = "INSERT INTO service_type VALUES(@cat)"
            Cmd.ExecuteNonQuery()

            closecon()

            ViewCategoryList()

            txtCategory.Focus()
            txtCategory.SelectAll()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ViewCategoryList()
        Try
            opencon()
            lvCategory.Items.Clear()
            Cmd.CommandText = "SELECT * FROM service_type"
            Dr = Cmd.ExecuteReader
            While Dr.Read
                lvCategory.Items.Add(Dr(0))
            End While
            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AddServiceCategory_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ServiceForm.ListCategory()
    End Sub

    Private Sub AddServiceCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ViewCategoryList()
    End Sub

    Private Sub txtCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyDown
        If e.KeyData = Keys.Enter Then
            AddServiceCategory()
        End If
    End Sub
End Class