<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReminderInfo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbReminder = New System.Windows.Forms.Label()
        Me.lbName = New System.Windows.Forms.Label()
        Me.lbAccount = New System.Windows.Forms.Label()
        Me.btnRemind = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbProcedure = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Account No"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 18)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Patient Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 183)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Reminder"
        '
        'lbReminder
        '
        Me.lbReminder.AutoSize = True
        Me.lbReminder.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbReminder.ForeColor = System.Drawing.Color.LimeGreen
        Me.lbReminder.Location = New System.Drawing.Point(32, 212)
        Me.lbReminder.Name = "lbReminder"
        Me.lbReminder.Size = New System.Drawing.Size(108, 18)
        Me.lbReminder.TabIndex = 0
        Me.lbReminder.Text = "lbReminder"
        '
        'lbName
        '
        Me.lbName.AutoSize = True
        Me.lbName.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbName.ForeColor = System.Drawing.Color.LimeGreen
        Me.lbName.Location = New System.Drawing.Point(138, 61)
        Me.lbName.Name = "lbName"
        Me.lbName.Size = New System.Drawing.Size(74, 18)
        Me.lbName.TabIndex = 0
        Me.lbName.Text = "lbName"
        '
        'lbAccount
        '
        Me.lbAccount.AutoSize = True
        Me.lbAccount.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbAccount.ForeColor = System.Drawing.Color.LimeGreen
        Me.lbAccount.Location = New System.Drawing.Point(138, 20)
        Me.lbAccount.Name = "lbAccount"
        Me.lbAccount.Size = New System.Drawing.Size(93, 18)
        Me.lbAccount.TabIndex = 0
        Me.lbAccount.Text = "lbAccount"
        '
        'btnRemind
        '
        Me.btnRemind.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnRemind.FlatAppearance.BorderSize = 0
        Me.btnRemind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemind.Location = New System.Drawing.Point(33, 290)
        Me.btnRemind.Name = "btnRemind"
        Me.btnRemind.Size = New System.Drawing.Size(175, 23)
        Me.btnRemind.TabIndex = 1
        Me.btnRemind.Text = "Remind me Later"
        Me.btnRemind.UseVisualStyleBackColor = False
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnOk.FlatAppearance.BorderSize = 0
        Me.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOk.Location = New System.Drawing.Point(315, 290)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(175, 23)
        Me.btnOk.TabIndex = 1
        Me.btnOk.Text = "Ok"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 18)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Procedure"
        '
        'lbProcedure
        '
        Me.lbProcedure.AutoSize = True
        Me.lbProcedure.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbProcedure.ForeColor = System.Drawing.Color.LimeGreen
        Me.lbProcedure.Location = New System.Drawing.Point(29, 127)
        Me.lbProcedure.Name = "lbProcedure"
        Me.lbProcedure.Size = New System.Drawing.Size(115, 18)
        Me.lbProcedure.TabIndex = 0
        Me.lbProcedure.Text = "lbProcedure"
        '
        'ReminderInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(521, 334)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnRemind)
        Me.Controls.Add(Me.lbProcedure)
        Me.Controls.Add(Me.lbReminder)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbAccount)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Name = "ReminderInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reminder Info"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbReminder As System.Windows.Forms.Label
    Friend WithEvents lbName As System.Windows.Forms.Label
    Friend WithEvents lbAccount As System.Windows.Forms.Label
    Friend WithEvents btnRemind As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbProcedure As System.Windows.Forms.Label
End Class
