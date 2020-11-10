Public Class Supplier
    Dim boolupdate As Boolean = False
    Dim strsupp As String = ""
    Private Sub DisplayAllSupplier()
        Try
            lvSupplier.Items.Clear()
            Dim y As Integer = 0
            opencon()
            Cmd.CommandText = "SELECT * FROM supplier"
            Dr = Cmd.ExecuteReader
            While Dr.Read
                lvSupplier.Items.Add(Dr(0))
                For x As Integer = 1 To 4
                    lvSupplier.Items(y).SubItems.Add(Dr(x))
                Next
                y += 1
            End While
            Dr.Close()

            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Supplier_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        DeliveredSupplies.DisplaySupplier()
    End Sub

    Private Sub Supplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DisplayAllSupplier()
        txtAddress.Text = ""
        txtAgent.Text = ""
        txtContact.Text = ""
        txtName.Text = ""
        txtSupplier.Text = ""
        boolupdate = False
        strsupp = ""
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Not txtSupplier.Text = "" Then
            If boolupdate = False And strsupp = "" Then
                Try
                    Dim cnt As Integer
                    opencon()
                    With Cmd.Parameters
                        .Clear()
                        .AddWithValue("@suppid", txtSupplier.Text)
                        .AddWithValue("@name", txtName.Text)
                        .AddWithValue("@agent", txtAgent.Text)
                        .AddWithValue("@contact", txtContact.Text)
                        .AddWithValue("@addr", txtAddress.Text)
                    End With

                    Cmd.CommandText = "INSERT INTO supplier VALUES(@suppid,@name,@agent,@contact,@addr)"
                    cnt = Cmd.ExecuteNonQuery()
                    MsgBox(cnt & " record has been added.")

                    closecon()
                    Supplier_Load(sender, e)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            ElseIf boolupdate = True Then
                Try
                    Dim cnt As Integer
                    opencon()
                    With Cmd.Parameters
                        .Clear()
                        .AddWithValue("@psuppid", strsupp)
                        .AddWithValue("@suppid", txtSupplier.Text)
                        .AddWithValue("@name", txtName.Text)
                        .AddWithValue("@agent", txtAgent.Text)
                        .AddWithValue("@contact", txtContact.Text)
                        .AddWithValue("@addr", txtAddress.Text)
                    End With

                    Cmd.CommandText = "UPDATE supplier SET  `SupprID`=@suppid, `Name`=@name, `Agent`=@agent, `Contact#`=@contact, `Address`=@addr WHERE `SupprID`=@psuppid"
                    cnt = Cmd.ExecuteNonQuery()
                    MsgBox(cnt & " record has been updated.")

                    closecon()
                    Supplier_Load(sender, e)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If

        End If


    End Sub

    Private Sub lvSupplier_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvSupplier.MouseClick
        If lvSupplier.SelectedItems.Count > 0 Then
            txtSupplier.Text = lvSupplier.SelectedItems.Item(0).Text
            txtName.Text = lvSupplier.SelectedItems.Item(0).SubItems(1).Text
            txtAgent.Text = lvSupplier.SelectedItems.Item(0).SubItems(2).Text
            txtContact.Text = lvSupplier.SelectedItems.Item(0).SubItems(3).Text
            txtAddress.Text = lvSupplier.SelectedItems.Item(0).SubItems(4).Text
            boolupdate = True
            strsupp = lvSupplier.SelectedItems.Item(0).Text
        End If
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If boolupdate = True Then

            Try
                opencon()
                With Cmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@supprid", strsupp)
                    .CommandText = "DELETE FROM supplier WHERE SupprID=@supprid"
                    .ExecuteNonQuery()

                End With
                closecon()
                Supplier_Load(sender, e)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub
End Class