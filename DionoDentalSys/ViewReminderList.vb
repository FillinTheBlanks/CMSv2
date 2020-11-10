Public Class ViewReminderList

    Private Sub SelectReminder()
        With OrthoReminders
            .lvChargeDesc.Items.Clear()
            .txtReminder.Text = lvReminder.SelectedItems.Item(0).Text
            .lvChargeDesc.Items.Add(lvReminder.SelectedItems.Item(0).SubItems(1).Text)
            .dtpDate.Value = Date.Parse(lvReminder.SelectedItems.Item(0).SubItems(2).Text)
            .dtpTime.Value = Date.Parse(lvReminder.SelectedItems.Item(0).SubItems(3).Text)

            .updateno = Integer.Parse(lvReminder.SelectedItems.Item(0).SubItems(5).Text)

           

        End With

        
        Me.Close()
        Me.Dispose()
    End Sub

  
    Private Sub lvReminder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvReminder.KeyDown
        If e.KeyData = Keys.Enter Then
            SelectReminder()
        End If
    End Sub

    Private Sub ViewReminderList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'With lvReminder
        '    .Columns(0).Width = 511
        '    .Columns(1).Width = 207
        '    .Columns(2).Width = 125
        '    .Columns(3).Width = 129
        '    .Columns(4).Width = 107
        '    .Columns(5).Width = 60
        'End With
    End Sub

End Class