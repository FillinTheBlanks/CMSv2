<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainModule
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
        Me.components = New System.ComponentModel.Container()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SignInToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SignOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmployeeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmployeeInformationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExpensesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddExpensesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewExpensesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewRecordsSchedulesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientReminderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DentalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OrthodonticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientBracketsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewReminderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAndCreateReminderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ServicesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientChargesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddServiceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewServiceRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SuppliesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewSuppliesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddSuppliesRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeliveredSuppliesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SuppliesPaymentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExpenseReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IncomeStatementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InventoryReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientRecordToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SuppliesPaymentReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientStatisticalReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutUsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.tsEmpName = New System.Windows.Forms.ToolStripLabel()
        Me.tsDateTime = New System.Windows.Forms.ToolStripLabel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnViewDTR = New System.Windows.Forms.Button()
        Me.lvDTR = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtEmpID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbEmpName = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.Label7 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Location = New System.Drawing.Point(0, 25)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1075, 588)
        Me.pnlMain.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.RoyalBlue
        Me.MenuStrip1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EmployeeToolStripMenuItem, Me.ExpensesToolStripMenuItem, Me.PatientToolStripMenuItem, Me.ServicesToolStripMenuItem, Me.SuppliesToolStripMenuItem, Me.ReportsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1366, 26)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SignInToolStripMenuItem, Me.SignOutToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(40, 22)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'SignInToolStripMenuItem
        '
        Me.SignInToolStripMenuItem.Name = "SignInToolStripMenuItem"
        Me.SignInToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.SignInToolStripMenuItem.Text = "Sign In"
        '
        'SignOutToolStripMenuItem
        '
        Me.SignOutToolStripMenuItem.Name = "SignOutToolStripMenuItem"
        Me.SignOutToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.SignOutToolStripMenuItem.Text = "Sign Out"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'EmployeeToolStripMenuItem
        '
        Me.EmployeeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EmployeeInformationToolStripMenuItem})
        Me.EmployeeToolStripMenuItem.Name = "EmployeeToolStripMenuItem"
        Me.EmployeeToolStripMenuItem.Size = New System.Drawing.Size(83, 22)
        Me.EmployeeToolStripMenuItem.Text = "Employee"
        '
        'EmployeeInformationToolStripMenuItem
        '
        Me.EmployeeInformationToolStripMenuItem.Name = "EmployeeInformationToolStripMenuItem"
        Me.EmployeeInformationToolStripMenuItem.Size = New System.Drawing.Size(220, 22)
        Me.EmployeeInformationToolStripMenuItem.Text = "Employee Information"
        '
        'ExpensesToolStripMenuItem
        '
        Me.ExpensesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddExpensesToolStripMenuItem, Me.ViewExpensesToolStripMenuItem})
        Me.ExpensesToolStripMenuItem.Name = "ExpensesToolStripMenuItem"
        Me.ExpensesToolStripMenuItem.Size = New System.Drawing.Size(82, 22)
        Me.ExpensesToolStripMenuItem.Text = "Expenses"
        '
        'AddExpensesToolStripMenuItem
        '
        Me.AddExpensesToolStripMenuItem.Name = "AddExpensesToolStripMenuItem"
        Me.AddExpensesToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.AddExpensesToolStripMenuItem.Text = "Add Expenses"
        '
        'ViewExpensesToolStripMenuItem
        '
        Me.ViewExpensesToolStripMenuItem.Name = "ViewExpensesToolStripMenuItem"
        Me.ViewExpensesToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.ViewExpensesToolStripMenuItem.Text = "View Expenses"
        '
        'PatientToolStripMenuItem
        '
        Me.PatientToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewRecordsSchedulesToolStripMenuItem, Me.PatientRecordToolStripMenuItem, Me.PatientReminderToolStripMenuItem, Me.PatientBracketsToolStripMenuItem, Me.PatientAccountToolStripMenuItem, Me.ViewReminderToolStripMenuItem, Me.DeleteAndCreateReminderToolStripMenuItem})
        Me.PatientToolStripMenuItem.Name = "PatientToolStripMenuItem"
        Me.PatientToolStripMenuItem.Size = New System.Drawing.Size(64, 22)
        Me.PatientToolStripMenuItem.Text = "Patient"
        '
        'ViewRecordsSchedulesToolStripMenuItem
        '
        Me.ViewRecordsSchedulesToolStripMenuItem.Name = "ViewRecordsSchedulesToolStripMenuItem"
        Me.ViewRecordsSchedulesToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.ViewRecordsSchedulesToolStripMenuItem.Text = "View Patient Records"
        '
        'PatientRecordToolStripMenuItem
        '
        Me.PatientRecordToolStripMenuItem.Name = "PatientRecordToolStripMenuItem"
        Me.PatientRecordToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.PatientRecordToolStripMenuItem.Text = "Patient Record"
        '
        'PatientReminderToolStripMenuItem
        '
        Me.PatientReminderToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DentalToolStripMenuItem, Me.OrthodonticsToolStripMenuItem})
        Me.PatientReminderToolStripMenuItem.Name = "PatientReminderToolStripMenuItem"
        Me.PatientReminderToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.PatientReminderToolStripMenuItem.Text = "Patient Reminder"
        '
        'DentalToolStripMenuItem
        '
        Me.DentalToolStripMenuItem.Name = "DentalToolStripMenuItem"
        Me.DentalToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.DentalToolStripMenuItem.Text = "Dental"
        Me.DentalToolStripMenuItem.Visible = False
        '
        'OrthodonticsToolStripMenuItem
        '
        Me.OrthodonticsToolStripMenuItem.Name = "OrthodonticsToolStripMenuItem"
        Me.OrthodonticsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.OrthodonticsToolStripMenuItem.Text = "Orthodontics"
        '
        'PatientBracketsToolStripMenuItem
        '
        Me.PatientBracketsToolStripMenuItem.Name = "PatientBracketsToolStripMenuItem"
        Me.PatientBracketsToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.PatientBracketsToolStripMenuItem.Text = "Patient Brackets"
        '
        'PatientAccountToolStripMenuItem
        '
        Me.PatientAccountToolStripMenuItem.Name = "PatientAccountToolStripMenuItem"
        Me.PatientAccountToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.PatientAccountToolStripMenuItem.Text = "Patient Account"
        '
        'ViewReminderToolStripMenuItem
        '
        Me.ViewReminderToolStripMenuItem.Name = "ViewReminderToolStripMenuItem"
        Me.ViewReminderToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.ViewReminderToolStripMenuItem.Text = "View Reminder"
        '
        'DeleteAndCreateReminderToolStripMenuItem
        '
        Me.DeleteAndCreateReminderToolStripMenuItem.Name = "DeleteAndCreateReminderToolStripMenuItem"
        Me.DeleteAndCreateReminderToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.DeleteAndCreateReminderToolStripMenuItem.Text = "Delete and Create Reminder"
        '
        'ServicesToolStripMenuItem
        '
        Me.ServicesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PatientChargesToolStripMenuItem, Me.AddServiceToolStripMenuItem, Me.ViewServiceRecordToolStripMenuItem})
        Me.ServicesToolStripMenuItem.Name = "ServicesToolStripMenuItem"
        Me.ServicesToolStripMenuItem.Size = New System.Drawing.Size(73, 22)
        Me.ServicesToolStripMenuItem.Text = "Services"
        '
        'PatientChargesToolStripMenuItem
        '
        Me.PatientChargesToolStripMenuItem.Name = "PatientChargesToolStripMenuItem"
        Me.PatientChargesToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.PatientChargesToolStripMenuItem.Text = "Patient Service Charge"
        '
        'AddServiceToolStripMenuItem
        '
        Me.AddServiceToolStripMenuItem.Name = "AddServiceToolStripMenuItem"
        Me.AddServiceToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.AddServiceToolStripMenuItem.Text = "Add Service Details"
        '
        'ViewServiceRecordToolStripMenuItem
        '
        Me.ViewServiceRecordToolStripMenuItem.Name = "ViewServiceRecordToolStripMenuItem"
        Me.ViewServiceRecordToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.ViewServiceRecordToolStripMenuItem.Text = "View Service Record"
        '
        'SuppliesToolStripMenuItem
        '
        Me.SuppliesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewSuppliesToolStripMenuItem, Me.AddSuppliesRecordToolStripMenuItem, Me.DeliveredSuppliesToolStripMenuItem, Me.SuppliesPaymentToolStripMenuItem})
        Me.SuppliesToolStripMenuItem.Name = "SuppliesToolStripMenuItem"
        Me.SuppliesToolStripMenuItem.Size = New System.Drawing.Size(71, 22)
        Me.SuppliesToolStripMenuItem.Text = "Supplies"
        '
        'ViewSuppliesToolStripMenuItem
        '
        Me.ViewSuppliesToolStripMenuItem.Name = "ViewSuppliesToolStripMenuItem"
        Me.ViewSuppliesToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.ViewSuppliesToolStripMenuItem.Text = "View Supplies"
        '
        'AddSuppliesRecordToolStripMenuItem
        '
        Me.AddSuppliesRecordToolStripMenuItem.Name = "AddSuppliesRecordToolStripMenuItem"
        Me.AddSuppliesRecordToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.AddSuppliesRecordToolStripMenuItem.Text = "Add Supplies Record"
        '
        'DeliveredSuppliesToolStripMenuItem
        '
        Me.DeliveredSuppliesToolStripMenuItem.Name = "DeliveredSuppliesToolStripMenuItem"
        Me.DeliveredSuppliesToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.DeliveredSuppliesToolStripMenuItem.Text = "Delivered Supplies"
        '
        'SuppliesPaymentToolStripMenuItem
        '
        Me.SuppliesPaymentToolStripMenuItem.Name = "SuppliesPaymentToolStripMenuItem"
        Me.SuppliesPaymentToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.SuppliesPaymentToolStripMenuItem.Text = "Supplies Payment"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExpenseReportToolStripMenuItem, Me.IncomeStatementToolStripMenuItem, Me.InventoryReportToolStripMenuItem, Me.PatientRecordToolStripMenuItem1, Me.SuppliesPaymentReportToolStripMenuItem, Me.PatientStatisticalReportToolStripMenuItem})
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(70, 22)
        Me.ReportsToolStripMenuItem.Text = "Reports"
        '
        'ExpenseReportToolStripMenuItem
        '
        Me.ExpenseReportToolStripMenuItem.Name = "ExpenseReportToolStripMenuItem"
        Me.ExpenseReportToolStripMenuItem.Size = New System.Drawing.Size(238, 22)
        Me.ExpenseReportToolStripMenuItem.Text = "Expense Report"
        '
        'IncomeStatementToolStripMenuItem
        '
        Me.IncomeStatementToolStripMenuItem.Name = "IncomeStatementToolStripMenuItem"
        Me.IncomeStatementToolStripMenuItem.Size = New System.Drawing.Size(238, 22)
        Me.IncomeStatementToolStripMenuItem.Text = "Income Statement"
        '
        'InventoryReportToolStripMenuItem
        '
        Me.InventoryReportToolStripMenuItem.Name = "InventoryReportToolStripMenuItem"
        Me.InventoryReportToolStripMenuItem.Size = New System.Drawing.Size(238, 22)
        Me.InventoryReportToolStripMenuItem.Text = "Inventory Report"
        '
        'PatientRecordToolStripMenuItem1
        '
        Me.PatientRecordToolStripMenuItem1.Name = "PatientRecordToolStripMenuItem1"
        Me.PatientRecordToolStripMenuItem1.Size = New System.Drawing.Size(238, 22)
        Me.PatientRecordToolStripMenuItem1.Text = "Patient Record"
        '
        'SuppliesPaymentReportToolStripMenuItem
        '
        Me.SuppliesPaymentReportToolStripMenuItem.Name = "SuppliesPaymentReportToolStripMenuItem"
        Me.SuppliesPaymentReportToolStripMenuItem.Size = New System.Drawing.Size(238, 22)
        Me.SuppliesPaymentReportToolStripMenuItem.Text = "Supplies Payment Report"
        '
        'PatientStatisticalReportToolStripMenuItem
        '
        Me.PatientStatisticalReportToolStripMenuItem.Name = "PatientStatisticalReportToolStripMenuItem"
        Me.PatientStatisticalReportToolStripMenuItem.Size = New System.Drawing.Size(238, 22)
        Me.PatientStatisticalReportToolStripMenuItem.Text = "Patient Statistical Report"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutUsToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(48, 22)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutUsToolStripMenuItem
        '
        Me.AboutUsToolStripMenuItem.Name = "AboutUsToolStripMenuItem"
        Me.AboutUsToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.AboutUsToolStripMenuItem.Text = "About Us"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.RoyalBlue
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsProgressBar, Me.tsEmpName, Me.tsDateTime})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 616)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1366, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsProgressBar
        '
        Me.tsProgressBar.BackColor = System.Drawing.Color.White
        Me.tsProgressBar.Name = "tsProgressBar"
        Me.tsProgressBar.Size = New System.Drawing.Size(100, 22)
        '
        'tsEmpName
        '
        Me.tsEmpName.Name = "tsEmpName"
        Me.tsEmpName.Size = New System.Drawing.Size(0, 22)
        '
        'tsDateTime
        '
        Me.tsDateTime.Name = "tsDateTime"
        Me.tsDateTime.Size = New System.Drawing.Size(72, 22)
        Me.tsDateTime.Text = "Loading..."
        '
        'Timer1
        '
        '
        'Timer2
        '
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DodgerBlue
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.btnViewDTR)
        Me.Panel1.Controls.Add(Me.lvDTR)
        Me.Panel1.Controls.Add(Me.txtEmpID)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lbEmpName)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(1081, 29)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(285, 584)
        Me.Panel1.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(3, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(167, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Press ENTER Key Once. "
        '
        'btnViewDTR
        '
        Me.btnViewDTR.BackColor = System.Drawing.Color.White
        Me.btnViewDTR.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnViewDTR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue
        Me.btnViewDTR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue
        Me.btnViewDTR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewDTR.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewDTR.Location = New System.Drawing.Point(212, 65)
        Me.btnViewDTR.Name = "btnViewDTR"
        Me.btnViewDTR.Size = New System.Drawing.Size(52, 23)
        Me.btnViewDTR.TabIndex = 3
        Me.btnViewDTR.Text = "View"
        Me.btnViewDTR.UseVisualStyleBackColor = False
        '
        'lvDTR
        '
        Me.lvDTR.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7})
        Me.lvDTR.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvDTR.GridLines = True
        Me.lvDTR.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvDTR.HideSelection = False
        Me.lvDTR.Location = New System.Drawing.Point(3, 187)
        Me.lvDTR.MultiSelect = False
        Me.lvDTR.Name = "lvDTR"
        Me.lvDTR.Size = New System.Drawing.Size(279, 394)
        Me.lvDTR.TabIndex = 2
        Me.lvDTR.UseCompatibleStateImageBehavior = False
        Me.lvDTR.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "    "
        Me.ColumnHeader1.Width = 81
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "In"
        Me.ColumnHeader2.Width = 80
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Out"
        Me.ColumnHeader3.Width = 80
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "In"
        Me.ColumnHeader4.Width = 80
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Out"
        Me.ColumnHeader5.Width = 80
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "In"
        Me.ColumnHeader6.Width = 80
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Out"
        Me.ColumnHeader7.Width = 80
        '
        'txtEmpID
        '
        Me.txtEmpID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtEmpID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtEmpID.Location = New System.Drawing.Point(6, 63)
        Me.txtEmpID.Name = "txtEmpID"
        Me.txtEmpID.Size = New System.Drawing.Size(200, 27)
        Me.txtEmpID.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(43, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(206, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Daily Time Record"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(79, 169)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(15, 18)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "|"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(3, 169)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Date"
        '
        'lbEmpName
        '
        Me.lbEmpName.AutoSize = True
        Me.lbEmpName.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbEmpName.ForeColor = System.Drawing.Color.White
        Me.lbEmpName.Location = New System.Drawing.Point(3, 139)
        Me.lbEmpName.Name = "lbEmpName"
        Me.lbEmpName.Size = New System.Drawing.Size(149, 18)
        Me.lbEmpName.TabIndex = 0
        Me.lbEmpName.Text = "Employee Name"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(3, 116)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 18)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Name :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(3, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Employee ID"
        '
        'Timer3
        '
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.RoyalBlue
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(1167, 620)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(192, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "LLITech Co. Version 1.2.0"
        '
        'MainModule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1366, 641)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainModule"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ampodia - Diono Dental System"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SignInToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SignOutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatientToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents tsDateTime As System.Windows.Forms.ToolStripLabel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents tsEmpName As System.Windows.Forms.ToolStripLabel
    Friend WithEvents PatientRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatientReminderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewRecordsSchedulesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ServicesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddServiceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SuppliesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatientChargesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewSuppliesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddSuppliesRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DentalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrthodonticsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatientBracketsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeliveredSuppliesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SuppliesPaymentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatientAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmployeeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmployeeInformationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExpenseReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IncomeStatementToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InventoryReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatientRecordToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SuppliesPaymentReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutUsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExpensesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddExpensesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewExpensesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents ViewReminderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewServiceRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtEmpID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lvDTR As System.Windows.Forms.ListView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents btnViewDTR As System.Windows.Forms.Button
    Friend WithEvents PatientStatisticalReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbEmpName As System.Windows.Forms.Label
    Friend WithEvents DeleteAndCreateReminderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label7 As System.Windows.Forms.Label

End Class
