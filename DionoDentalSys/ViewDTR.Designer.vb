<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewDTR
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
        Me.lvViewDTR = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblEmpID = New System.Windows.Forms.Label()
        Me.lvEmployee = New System.Windows.Forms.ListView()
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lbLast = New System.Windows.Forms.Label()
        Me.lbFirst = New System.Windows.Forms.Label()
        Me.lbMiddle = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lvViewDTR
        '
        Me.lvViewDTR.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7})
        Me.lvViewDTR.GridLines = True
        Me.lvViewDTR.Location = New System.Drawing.Point(18, 210)
        Me.lvViewDTR.Margin = New System.Windows.Forms.Padding(4)
        Me.lvViewDTR.Name = "lvViewDTR"
        Me.lvViewDTR.Size = New System.Drawing.Size(632, 412)
        Me.lvViewDTR.TabIndex = 0
        Me.lvViewDTR.UseCompatibleStateImageBehavior = False
        Me.lvViewDTR.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Date"
        Me.ColumnHeader1.Width = 106
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "In"
        Me.ColumnHeader2.Width = 87
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Out"
        Me.ColumnHeader3.Width = 87
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "In"
        Me.ColumnHeader4.Width = 87
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Out"
        Me.ColumnHeader5.Width = 87
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "In"
        Me.ColumnHeader6.Width = 87
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Out"
        Me.ColumnHeader7.Width = 87
        '
        'lblEmpID
        '
        Me.lblEmpID.AutoSize = True
        Me.lblEmpID.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpID.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblEmpID.Location = New System.Drawing.Point(15, 188)
        Me.lblEmpID.Name = "lblEmpID"
        Me.lblEmpID.Size = New System.Drawing.Size(67, 18)
        Me.lblEmpID.TabIndex = 1
        Me.lblEmpID.Text = "EmpID"
        '
        'lvEmployee
        '
        Me.lvEmployee.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader12})
        Me.lvEmployee.FullRowSelect = True
        Me.lvEmployee.GridLines = True
        Me.lvEmployee.Location = New System.Drawing.Point(18, 24)
        Me.lvEmployee.MultiSelect = False
        Me.lvEmployee.Name = "lvEmployee"
        Me.lvEmployee.Size = New System.Drawing.Size(632, 148)
        Me.lvEmployee.TabIndex = 2
        Me.lvEmployee.UseCompatibleStateImageBehavior = False
        Me.lvEmployee.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "ID"
        Me.ColumnHeader8.Width = 97
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Last Name"
        Me.ColumnHeader9.Width = 170
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "First Name"
        Me.ColumnHeader10.Width = 151
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Middle Name"
        Me.ColumnHeader11.Width = 112
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Position"
        Me.ColumnHeader12.Width = 96
        '
        'lbLast
        '
        Me.lbLast.AutoSize = True
        Me.lbLast.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLast.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lbLast.Location = New System.Drawing.Point(123, 188)
        Me.lbLast.Name = "lbLast"
        Me.lbLast.Size = New System.Drawing.Size(101, 18)
        Me.lbLast.TabIndex = 1
        Me.lbLast.Text = "Last Name"
        '
        'lbFirst
        '
        Me.lbFirst.AutoSize = True
        Me.lbFirst.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFirst.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lbFirst.Location = New System.Drawing.Point(288, 188)
        Me.lbFirst.Name = "lbFirst"
        Me.lbFirst.Size = New System.Drawing.Size(102, 18)
        Me.lbFirst.TabIndex = 1
        Me.lbFirst.Text = "First Name"
        '
        'lbMiddle
        '
        Me.lbMiddle.AutoSize = True
        Me.lbMiddle.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMiddle.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lbMiddle.Location = New System.Drawing.Point(437, 188)
        Me.lbMiddle.Name = "lbMiddle"
        Me.lbMiddle.Size = New System.Drawing.Size(120, 18)
        Me.lbMiddle.TabIndex = 1
        Me.lbMiddle.Text = "Middle Name"
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.White
        Me.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue
        Me.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Location = New System.Drawing.Point(573, 180)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(77, 26)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'ViewDTR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(666, 638)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.lvEmployee)
        Me.Controls.Add(Me.lbMiddle)
        Me.Controls.Add(Me.lbFirst)
        Me.Controls.Add(Me.lbLast)
        Me.Controls.Add(Me.lblEmpID)
        Me.Controls.Add(Me.lvViewDTR)
        Me.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ViewDTR"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View DTR"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvViewDTR As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblEmpID As System.Windows.Forms.Label
    Friend WithEvents lvEmployee As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lbLast As System.Windows.Forms.Label
    Friend WithEvents lbFirst As System.Windows.Forms.Label
    Friend WithEvents lbMiddle As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
End Class
