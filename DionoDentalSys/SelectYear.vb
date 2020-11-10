Public Class SelectYear
    Private Sub Displayyear()
        Try
            opencon()

            cmbYear.Items.Clear()
            With Cmd
               
                .CommandText = "SELECT DISTINCT YEAR(DateStarted) FROM patient ORDER BY YEAR(DateStarted) ASC"
                Dr = .ExecuteReader
                While Dr.Read
                    cmbYear.Items.Add(Dr(0))
                End While
                Dr.Close()
            End With

            cmbYear.SelectedIndex = 0
            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub SelectYear_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Displayyear()
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        MainModule.PrintPatientStatistics(cmbYear.SelectedItem)
    End Sub

End Class