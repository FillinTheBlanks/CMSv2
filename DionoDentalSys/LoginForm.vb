Public Class LoginForm

    Private Sub LoginForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        suggestiontext("SELECT DISTINCT EmpID FROM employee LIMIT 100", txtEmpID)

    End Sub

    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyData = Keys.Enter Then
            SignIn(txtEmpID.Text, txtPassword.Text)
        End If
    End Sub

End Class