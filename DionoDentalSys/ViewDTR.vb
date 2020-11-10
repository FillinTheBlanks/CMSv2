Public Class ViewDTR

    Private Sub SelectEmployee(ByVal strempid As String)
        Try
            Dim y As Integer = 0
            lvViewDTR.Items.Clear()
            opencon()
            With Cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@empid", strempid)
                .Parameters.AddWithValue("@date", Now.ToString("yyyy-MM") & "-%")
                .CommandText = "SELECT Date,In1,Out1,In2,Out2,In3,Out3 FROM dtr_employee WHERE EmpID=@empid AND Date LIKE @date"
                Dr = .ExecuteReader
            End With
            'MsgBox(Now.ToString("yyyy-MM") & "-%")

            While Dr.Read
                lvViewDTR.Items.Add(Dr(0))
                For x As Integer = 1 To 6
                    If Dr(x).ToString.Equals("") Then
                        lvViewDTR.Items(y).SubItems.Add(" ")
                    Else
                        Dim ts As TimeSpan = Dr.GetTimeSpan(x)
                        Dim dt As DateTime = DateTime.ParseExact(ts.ToString(), "HH:mm:ss", Nothing)
                        lvViewDTR.Items(y).SubItems.Add(dt.ToString("hh:mm tt"))
                    End If

                Next
                y += 1
            End While

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub DisplayAllEmployee()
        Try
            opencon()
            Dim y As Integer = 0
            With Cmd
                .CommandText = "SELECT EmpID,LName,FName,MName,Position FROM employee ORDER BY EmpID ASC"

                Dr = .ExecuteReader

                While Dr.Read
                    lvEmployee.Items.Add(Dr(0))

                    lvEmployee.Items(y).SubItems.Add(Dr(1))
                    lvEmployee.Items(y).SubItems.Add(Dr(2))
                    lvEmployee.Items(y).SubItems.Add(Dr(3))

                    y += 1

                End While
            End With
            closecon()
        Catch ex As Exception
            closecon()
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ViewDTR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DisplayAllEmployee()
        lblEmpID.Text = ""
        lbLast.Text = ""
        lbFirst.Text = ""
        lbMiddle.Text = ""
    End Sub

    Private Sub lvEmployee_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvEmployee.KeyDown
        If e.KeyData = Keys.Enter Then
            If lvEmployee.SelectedItems.Count = 1 Then
                SelectEmployee(lvEmployee.SelectedItems(0).Text)
                lblEmpID.Text = lvEmployee.SelectedItems(0).Text
                lbLast.Text = lvEmployee.SelectedItems(0).SubItems(1).Text
                lbFirst.Text = lvEmployee.SelectedItems(0).SubItems(2).Text
                lbMiddle.Text = lvEmployee.SelectedItems(0).SubItems(3).Text
            End If
        End If
    End Sub

    Private Sub lvEmployee_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvEmployee.MouseClick
        If lvEmployee.SelectedItems.Count = 1 Then
            SelectEmployee(lvEmployee.SelectedItems(0).Text)
            lblEmpID.Text = lvEmployee.SelectedItems(0).Text
            lbLast.Text = lvEmployee.SelectedItems(0).SubItems(1).Text
            lbFirst.Text = lvEmployee.SelectedItems(0).SubItems(2).Text
            lbMiddle.Text = lvEmployee.SelectedItems(0).SubItems(3).Text
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        If Not lblEmpID.Text = "" Then
            With SelectDTRPrint
                .Show()
                .BringToFront()
                .lbEmpID.Text = lblEmpID.Text
            End With
        End If
    End Sub
End Class