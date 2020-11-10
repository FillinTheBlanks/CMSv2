Public Class EmployeeInfo
    Dim password As String = ""
    Public Sub ClearEmployee()
        txtAddress.Text = ""
        txtContactNo.Text = ""
        txtFName.Text = ""
        txtLName.Text = ""
        txtMI.Text = ""
        txtPosition.Text = ""
    End Sub


    Public Sub ViewEmployeeInfo()
        Try
            opencon()
            With Cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@eid", global_empid)
                .CommandText = "SELECT * FROM employee WHERE EmpID = @eid"
                Dr = .ExecuteReader

                If Dr.Read Then
                    txtLName.Text = Dr(1)
                    txtFName.Text = Dr(2)
                    txtMI.Text = Dr(3)
                    Dim date1 As Date = Date.Parse(Dr(4).ToString)
                    dtpBday.Value = date1

                    txtAddress.Text = Dr(5)
                    txtContactNo.Text = Dr(6)
                    txtPosition.Text = Dr(7)
                    Dim date2 As Date = Date.Parse(Dr(9).ToString)
                    dtpDateHire.Value = date2
                    password = Dr(8)
                End If
                Dr.Close()
            End With

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub UpdateEmployeeInfo()
        Try
            Dim cnt As Integer = 0
            Dim empid As Double = Double.Parse(txtEmpID.Text)
            opencon()
            With Cmd.Parameters
                .Clear()
                .AddWithValue("@empid", empid)
                .AddWithValue("@lname", txtLName.Text)
                .AddWithValue("@fname", txtFName.Text)
                .AddWithValue("@mname", txtMI.Text)
                .AddWithValue("@bday", dtpBday.Value.ToString("yyyy-MM-dd"))
                .AddWithValue("@pos", txtPosition.Text)
                .AddWithValue("@contact", txtContactNo.Text)
                .AddWithValue("@address", txtAddress.Text)
                .AddWithValue("@hire", dtpDateHire.Value.ToString("yyyy-MM-dd"))
            End With
            Cmd.CommandText = "UPDATE `employee` SET `LName`=@lname,`FName`=@fname,`MName`=@mname,`Bday`=@bday,`Address`=@address,`Contact#`=@contact,`Position`=@pos,`DHired`=@hire WHERE EmpID = @empid"

            cnt = Cmd.ExecuteNonQuery()


            MsgBox(cnt & " record has been updated.")
            closecon()
            btnRefresh.PerformClick()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnChangePass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangePass.Click
        With PassChanger
            .GetCurrentPassword(password)
            .Show()
            .BringToFront()
            .txtCurrent.Text = ""
            .txtNew.Text = ""
            .txtReenter.Text = ""
            .txtCurrent.Focus()
        End With
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        ClearEmployee()
        Me.Refresh()
        txtEmpID.Text = global_empid
        ViewEmployeeInfo()

        global_emplname = txtLName.Text
        global_empfname = txtFName.Text
        global_empmname = txtMI.Text
        global_pos = txtPosition.Text
        MainModule.tsEmpName.Text = global_emplname & ", " & global_empfname & " " & global_empmname & ". (" & global_pos & ")"
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        UpdateEmployeeInfo()
    End Sub
End Class