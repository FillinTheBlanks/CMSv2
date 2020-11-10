<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QtyBox
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
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.lbFormName = New System.Windows.Forms.Label()
        Me.lbSelectItem = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(115, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(156, 35)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Quantity"
        '
        'txtQty
        '
        Me.txtQty.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQty.Location = New System.Drawing.Point(51, 78)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(284, 43)
        Me.txtQty.TabIndex = 1
        '
        'lbFormName
        '
        Me.lbFormName.AutoSize = True
        Me.lbFormName.Location = New System.Drawing.Point(12, 158)
        Me.lbFormName.Name = "lbFormName"
        Me.lbFormName.Size = New System.Drawing.Size(39, 13)
        Me.lbFormName.TabIndex = 2
        Me.lbFormName.Text = "Label2"
        Me.lbFormName.Visible = False
        '
        'lbSelectItem
        '
        Me.lbSelectItem.AutoSize = True
        Me.lbSelectItem.Location = New System.Drawing.Point(80, 158)
        Me.lbSelectItem.Name = "lbSelectItem"
        Me.lbSelectItem.Size = New System.Drawing.Size(39, 13)
        Me.lbSelectItem.TabIndex = 2
        Me.lbSelectItem.Text = "Label2"
        Me.lbSelectItem.Visible = False
        '
        'QtyBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.RoyalBlue
        Me.ClientSize = New System.Drawing.Size(384, 180)
        Me.Controls.Add(Me.lbSelectItem)
        Me.Controls.Add(Me.lbFormName)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "QtyBox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QtyBox"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents lbFormName As System.Windows.Forms.Label
    Friend WithEvents lbSelectItem As System.Windows.Forms.Label
End Class
