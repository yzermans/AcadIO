<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.pnlNavigate = New System.Windows.Forms.Panel()
        Me.pnlDirectory = New System.Windows.Forms.Panel()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.lvwFiles = New System.Windows.Forms.ListView()
        Me.cmsEdit = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiUnselectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tvwFolders = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pbPicturePreview = New System.Windows.Forms.PictureBox()
        Me.pnlSelected = New System.Windows.Forms.Panel()
        Me.txtEditList = New System.Windows.Forms.TextBox()
        Me.dtpPlotDate = New System.Windows.Forms.DateTimePicker()
        Me.cboEditList = New System.Windows.Forms.ComboBox()
        Me.lvwACAD = New System.Windows.Forms.ListView()
        Me.ssCompile = New System.Windows.Forms.StatusStrip()
        Me.tssPlots1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.prgPlots = New System.Windows.Forms.ToolStripProgressBar()
        Me.pnlSelectedControls = New System.Windows.Forms.Panel()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.chkCT = New System.Windows.Forms.CheckBox()
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.chkOverWriteBlock = New System.Windows.Forms.CheckBox()
        Me.chkAddAttSize = New System.Windows.Forms.CheckBox()
        Me.chkLayer = New System.Windows.Forms.CheckBox()
        Me.chkInsert = New System.Windows.Forms.CheckBox()
        Me.chkAtt = New System.Windows.Forms.CheckBox()
        Me.grpPlot = New System.Windows.Forms.GroupBox()
        Me.chkRotate = New System.Windows.Forms.CheckBox()
        Me.chkBmp = New System.Windows.Forms.CheckBox()
        Me.chkPost = New System.Windows.Forms.CheckBox()
        Me.fraFormats = New System.Windows.Forms.GroupBox()
        Me.chk2000 = New System.Windows.Forms.CheckBox()
        Me.chkDWF = New System.Windows.Forms.CheckBox()
        Me.chkPreACAD = New System.Windows.Forms.CheckBox()
        Me.chkPNG = New System.Windows.Forms.CheckBox()
        Me.pnlNavigate.SuspendLayout()
        Me.pnlDirectory.SuspendLayout()
        Me.cmsEdit.SuspendLayout()
        CType(Me.pbPicturePreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSelected.SuspendLayout()
        Me.ssCompile.SuspendLayout()
        Me.pnlSelectedControls.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.grpPlot.SuspendLayout()
        Me.fraFormats.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlNavigate
        '
        Me.pnlNavigate.Controls.Add(Me.pnlDirectory)
        Me.pnlNavigate.Controls.Add(Me.pnlSelected)
        Me.pnlNavigate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNavigate.Location = New System.Drawing.Point(0, 0)
        Me.pnlNavigate.Name = "pnlNavigate"
        Me.pnlNavigate.Size = New System.Drawing.Size(725, 572)
        Me.pnlNavigate.TabIndex = 13
        '
        'pnlDirectory
        '
        Me.pnlDirectory.BackColor = System.Drawing.SystemColors.Control
        Me.pnlDirectory.Controls.Add(Me.Splitter2)
        Me.pnlDirectory.Controls.Add(Me.lvwFiles)
        Me.pnlDirectory.Controls.Add(Me.tvwFolders)
        Me.pnlDirectory.Controls.Add(Me.pbPicturePreview)
        Me.pnlDirectory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDirectory.Location = New System.Drawing.Point(0, 0)
        Me.pnlDirectory.Name = "pnlDirectory"
        Me.pnlDirectory.Size = New System.Drawing.Size(725, 412)
        Me.pnlDirectory.TabIndex = 10
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(240, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 412)
        Me.Splitter2.TabIndex = 9
        Me.Splitter2.TabStop = False
        '
        'lvwFiles
        '
        Me.lvwFiles.ContextMenuStrip = Me.cmsEdit
        Me.lvwFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwFiles.Location = New System.Drawing.Point(240, 0)
        Me.lvwFiles.MultiSelect = False
        Me.lvwFiles.Name = "lvwFiles"
        Me.lvwFiles.Size = New System.Drawing.Size(485, 412)
        Me.lvwFiles.TabIndex = 8
        Me.lvwFiles.UseCompatibleStateImageBehavior = False
        '
        'cmsEdit
        '
        Me.cmsEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiSelectAll, Me.tsmiUnselectAll})
        Me.cmsEdit.Name = "cmsEdit"
        Me.cmsEdit.Size = New System.Drawing.Size(130, 48)
        '
        'tsmiSelectAll
        '
        Me.tsmiSelectAll.Name = "tsmiSelectAll"
        Me.tsmiSelectAll.Size = New System.Drawing.Size(129, 22)
        Me.tsmiSelectAll.Text = "Select All"
        '
        'tsmiUnselectAll
        '
        Me.tsmiUnselectAll.Name = "tsmiUnselectAll"
        Me.tsmiUnselectAll.Size = New System.Drawing.Size(129, 22)
        Me.tsmiUnselectAll.Text = "Unselect All"
        '
        'tvwFolders
        '
        Me.tvwFolders.Dock = System.Windows.Forms.DockStyle.Left
        Me.tvwFolders.ImageIndex = 0
        Me.tvwFolders.ImageList = Me.ImageList1
        Me.tvwFolders.Location = New System.Drawing.Point(0, 0)
        Me.tvwFolders.Name = "tvwFolders"
        Me.tvwFolders.SelectedImageIndex = 0
        Me.tvwFolders.Size = New System.Drawing.Size(240, 412)
        Me.tvwFolders.TabIndex = 7
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        '
        'pbPicturePreview
        '
        Me.pbPicturePreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pbPicturePreview.Location = New System.Drawing.Point(337, 199)
        Me.pbPicturePreview.Name = "pbPicturePreview"
        Me.pbPicturePreview.Size = New System.Drawing.Size(160, 100)
        Me.pbPicturePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbPicturePreview.TabIndex = 10
        Me.pbPicturePreview.TabStop = False
        '
        'pnlSelected
        '
        Me.pnlSelected.Controls.Add(Me.txtEditList)
        Me.pnlSelected.Controls.Add(Me.dtpPlotDate)
        Me.pnlSelected.Controls.Add(Me.cboEditList)
        Me.pnlSelected.Controls.Add(Me.lvwACAD)
        Me.pnlSelected.Controls.Add(Me.ssCompile)
        Me.pnlSelected.Controls.Add(Me.pnlSelectedControls)
        Me.pnlSelected.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSelected.Location = New System.Drawing.Point(0, 412)
        Me.pnlSelected.Name = "pnlSelected"
        Me.pnlSelected.Size = New System.Drawing.Size(725, 160)
        Me.pnlSelected.TabIndex = 10
        '
        'txtEditList
        '
        Me.txtEditList.Location = New System.Drawing.Point(120, 112)
        Me.txtEditList.Name = "txtEditList"
        Me.txtEditList.Size = New System.Drawing.Size(68, 20)
        Me.txtEditList.TabIndex = 5
        Me.txtEditList.Visible = False
        '
        'dtpPlotDate
        '
        Me.dtpPlotDate.CustomFormat = ""
        Me.dtpPlotDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpPlotDate.Location = New System.Drawing.Point(120, 88)
        Me.dtpPlotDate.Name = "dtpPlotDate"
        Me.dtpPlotDate.Size = New System.Drawing.Size(72, 20)
        Me.dtpPlotDate.TabIndex = 7
        Me.dtpPlotDate.Value = New Date(2004, 9, 7, 6, 20, 50, 816)
        Me.dtpPlotDate.Visible = False
        '
        'cboEditList
        '
        Me.cboEditList.ItemHeight = 13
        Me.cboEditList.Location = New System.Drawing.Point(120, 48)
        Me.cboEditList.Name = "cboEditList"
        Me.cboEditList.Size = New System.Drawing.Size(72, 21)
        Me.cboEditList.TabIndex = 6
        Me.cboEditList.Visible = False
        '
        'lvwACAD
        '
        Me.lvwACAD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwACAD.Location = New System.Drawing.Point(0, 22)
        Me.lvwACAD.Name = "lvwACAD"
        Me.lvwACAD.Size = New System.Drawing.Size(725, 138)
        Me.lvwACAD.TabIndex = 10
        Me.lvwACAD.UseCompatibleStateImageBehavior = False
        '
        'ssCompile
        '
        Me.ssCompile.Dock = System.Windows.Forms.DockStyle.Top
        Me.ssCompile.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssPlots1, Me.prgPlots})
        Me.ssCompile.Location = New System.Drawing.Point(0, 0)
        Me.ssCompile.MinimumSize = New System.Drawing.Size(0, 22)
        Me.ssCompile.Name = "ssCompile"
        Me.ssCompile.Size = New System.Drawing.Size(725, 22)
        Me.ssCompile.TabIndex = 9
        '
        'tssPlots1
        '
        Me.tssPlots1.AutoSize = False
        Me.tssPlots1.Name = "tssPlots1"
        Me.tssPlots1.Size = New System.Drawing.Size(200, 17)
        Me.tssPlots1.Text = "Ready"
        '
        'prgPlots
        '
        Me.prgPlots.Name = "prgPlots"
        Me.prgPlots.Size = New System.Drawing.Size(100, 16)
        '
        'pnlSelectedControls
        '
        Me.pnlSelectedControls.BackColor = System.Drawing.SystemColors.Control
        Me.pnlSelectedControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSelectedControls.Controls.Add(Me.btnUp)
        Me.pnlSelectedControls.Controls.Add(Me.btnDown)
        Me.pnlSelectedControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSelectedControls.Location = New System.Drawing.Point(0, 0)
        Me.pnlSelectedControls.Name = "pnlSelectedControls"
        Me.pnlSelectedControls.Size = New System.Drawing.Size(725, 160)
        Me.pnlSelectedControls.TabIndex = 8
        '
        'btnUp
        '
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(8, 8)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(32, 28)
        Me.btnUp.TabIndex = 30
        '
        'btnDown
        '
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(48, 8)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(32, 28)
        Me.btnDown.TabIndex = 31
        '
        'scMain
        '
        Me.scMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scMain.Location = New System.Drawing.Point(0, 0)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.chkPNG)
        Me.scMain.Panel1.Controls.Add(Me.chkCT)
        Me.scMain.Panel1.Controls.Add(Me.cmdSelect)
        Me.scMain.Panel1.Controls.Add(Me.cmdSearch)
        Me.scMain.Panel1.Controls.Add(Me.cmdExit)
        Me.scMain.Panel1.Controls.Add(Me.chkOverWriteBlock)
        Me.scMain.Panel1.Controls.Add(Me.chkAddAttSize)
        Me.scMain.Panel1.Controls.Add(Me.chkLayer)
        Me.scMain.Panel1.Controls.Add(Me.chkInsert)
        Me.scMain.Panel1.Controls.Add(Me.chkAtt)
        Me.scMain.Panel1.Controls.Add(Me.grpPlot)
        Me.scMain.Panel1.Controls.Add(Me.fraFormats)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.pnlNavigate)
        Me.scMain.Size = New System.Drawing.Size(914, 572)
        Me.scMain.SplitterDistance = 185
        Me.scMain.TabIndex = 14
        '
        'chkCT
        '
        Me.chkCT.BackColor = System.Drawing.SystemColors.Control
        Me.chkCT.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCT.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCT.Location = New System.Drawing.Point(11, 386)
        Me.chkCT.Name = "chkCT"
        Me.chkCT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCT.Size = New System.Drawing.Size(109, 21)
        Me.chkCT.TabIndex = 37
        Me.chkCT.Text = "Copy Files To:"
        Me.chkCT.UseVisualStyleBackColor = False
        '
        'cmdSelect
        '
        Me.cmdSelect.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSelect.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelect.Location = New System.Drawing.Point(3, 467)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSelect.Size = New System.Drawing.Size(177, 23)
        Me.cmdSelect.TabIndex = 36
        Me.cmdSelect.Text = "Run ACAD"
        Me.cmdSelect.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Location = New System.Drawing.Point(3, 438)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(177, 23)
        Me.cmdSearch.TabIndex = 34
        Me.cmdSearch.Text = "&Recurse Folders"
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(3, 496)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(177, 23)
        Me.cmdExit.TabIndex = 35
        Me.cmdExit.Text = "E&xit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'chkOverWriteBlock
        '
        Me.chkOverWriteBlock.BackColor = System.Drawing.SystemColors.Control
        Me.chkOverWriteBlock.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOverWriteBlock.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOverWriteBlock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOverWriteBlock.Location = New System.Drawing.Point(11, 361)
        Me.chkOverWriteBlock.Name = "chkOverWriteBlock"
        Me.chkOverWriteBlock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOverWriteBlock.Size = New System.Drawing.Size(133, 19)
        Me.chkOverWriteBlock.TabIndex = 33
        Me.chkOverWriteBlock.Text = "Over Write Block"
        Me.chkOverWriteBlock.UseVisualStyleBackColor = False
        '
        'chkAddAttSize
        '
        Me.chkAddAttSize.BackColor = System.Drawing.SystemColors.Control
        Me.chkAddAttSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAddAttSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAddAttSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAddAttSize.Location = New System.Drawing.Point(11, 340)
        Me.chkAddAttSize.Name = "chkAddAttSize"
        Me.chkAddAttSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAddAttSize.Size = New System.Drawing.Size(133, 15)
        Me.chkAddAttSize.TabIndex = 32
        Me.chkAddAttSize.Text = "Add AttDef Size"
        Me.chkAddAttSize.UseVisualStyleBackColor = False
        '
        'chkLayer
        '
        Me.chkLayer.BackColor = System.Drawing.SystemColors.Control
        Me.chkLayer.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLayer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLayer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLayer.Location = New System.Drawing.Point(11, 312)
        Me.chkLayer.Name = "chkLayer"
        Me.chkLayer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLayer.Size = New System.Drawing.Size(111, 21)
        Me.chkLayer.TabIndex = 31
        Me.chkLayer.Text = "Layers To 0"
        Me.chkLayer.UseVisualStyleBackColor = False
        '
        'chkInsert
        '
        Me.chkInsert.BackColor = System.Drawing.SystemColors.Control
        Me.chkInsert.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInsert.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInsert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInsert.Location = New System.Drawing.Point(11, 288)
        Me.chkInsert.Name = "chkInsert"
        Me.chkInsert.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInsert.Size = New System.Drawing.Size(109, 19)
        Me.chkInsert.TabIndex = 30
        Me.chkInsert.Text = "Insert As Block"
        Me.chkInsert.UseVisualStyleBackColor = False
        '
        'chkAtt
        '
        Me.chkAtt.BackColor = System.Drawing.SystemColors.Control
        Me.chkAtt.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAtt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAtt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAtt.Location = New System.Drawing.Point(11, 266)
        Me.chkAtt.Name = "chkAtt"
        Me.chkAtt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAtt.Size = New System.Drawing.Size(109, 15)
        Me.chkAtt.TabIndex = 29
        Me.chkAtt.Text = "Hide AttDef"
        Me.chkAtt.UseVisualStyleBackColor = False
        '
        'grpPlot
        '
        Me.grpPlot.BackColor = System.Drawing.SystemColors.Control
        Me.grpPlot.Controls.Add(Me.chkRotate)
        Me.grpPlot.Controls.Add(Me.chkBmp)
        Me.grpPlot.Controls.Add(Me.chkPost)
        Me.grpPlot.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpPlot.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpPlot.Location = New System.Drawing.Point(12, 142)
        Me.grpPlot.Name = "grpPlot"
        Me.grpPlot.Padding = New System.Windows.Forms.Padding(0)
        Me.grpPlot.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grpPlot.Size = New System.Drawing.Size(118, 102)
        Me.grpPlot.TabIndex = 20
        Me.grpPlot.TabStop = False
        Me.grpPlot.Text = "Plot"
        '
        'chkRotate
        '
        Me.chkRotate.BackColor = System.Drawing.SystemColors.Control
        Me.chkRotate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRotate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRotate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRotate.Location = New System.Drawing.Point(8, 68)
        Me.chkRotate.Name = "chkRotate"
        Me.chkRotate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRotate.Size = New System.Drawing.Size(91, 20)
        Me.chkRotate.TabIndex = 21
        Me.chkRotate.Text = "Land Scape"
        Me.chkRotate.UseVisualStyleBackColor = False
        '
        'chkBmp
        '
        Me.chkBmp.BackColor = System.Drawing.SystemColors.Control
        Me.chkBmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBmp.Location = New System.Drawing.Point(8, 18)
        Me.chkBmp.Name = "chkBmp"
        Me.chkBmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBmp.Size = New System.Drawing.Size(82, 18)
        Me.chkBmp.TabIndex = 20
        Me.chkBmp.Text = "Bmp"
        Me.chkBmp.UseVisualStyleBackColor = False
        '
        'chkPost
        '
        Me.chkPost.BackColor = System.Drawing.SystemColors.Control
        Me.chkPost.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPost.Location = New System.Drawing.Point(8, 44)
        Me.chkPost.Name = "chkPost"
        Me.chkPost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPost.Size = New System.Drawing.Size(82, 18)
        Me.chkPost.TabIndex = 19
        Me.chkPost.Text = "Postscript"
        Me.chkPost.UseVisualStyleBackColor = False
        '
        'fraFormats
        '
        Me.fraFormats.BackColor = System.Drawing.SystemColors.Control
        Me.fraFormats.Controls.Add(Me.chk2000)
        Me.fraFormats.Controls.Add(Me.chkDWF)
        Me.fraFormats.Controls.Add(Me.chkPreACAD)
        Me.fraFormats.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFormats.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraFormats.Location = New System.Drawing.Point(12, 24)
        Me.fraFormats.Name = "fraFormats"
        Me.fraFormats.Padding = New System.Windows.Forms.Padding(0)
        Me.fraFormats.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraFormats.Size = New System.Drawing.Size(118, 102)
        Me.fraFormats.TabIndex = 19
        Me.fraFormats.TabStop = False
        Me.fraFormats.Text = "Create Formats"
        '
        'chk2000
        '
        Me.chk2000.BackColor = System.Drawing.SystemColors.Control
        Me.chk2000.Checked = True
        Me.chk2000.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk2000.Cursor = System.Windows.Forms.Cursors.Default
        Me.chk2000.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk2000.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chk2000.Location = New System.Drawing.Point(12, 16)
        Me.chk2000.Name = "chk2000"
        Me.chk2000.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chk2000.Size = New System.Drawing.Size(125, 17)
        Me.chk2000.TabIndex = 16
        Me.chk2000.Text = "AutoCAD 2000 DWG"
        Me.chk2000.UseVisualStyleBackColor = False
        '
        'chkDWF
        '
        Me.chkDWF.BackColor = System.Drawing.SystemColors.Control
        Me.chkDWF.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDWF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDWF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDWF.Location = New System.Drawing.Point(12, 39)
        Me.chkDWF.Name = "chkDWF"
        Me.chkDWF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDWF.Size = New System.Drawing.Size(97, 17)
        Me.chkDWF.TabIndex = 15
        Me.chkDWF.Text = "AutoCAD DWF"
        Me.chkDWF.UseVisualStyleBackColor = False
        '
        'chkPreACAD
        '
        Me.chkPreACAD.BackColor = System.Drawing.SystemColors.Control
        Me.chkPreACAD.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPreACAD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPreACAD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPreACAD.Location = New System.Drawing.Point(12, 62)
        Me.chkPreACAD.Name = "chkPreACAD"
        Me.chkPreACAD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPreACAD.Size = New System.Drawing.Size(93, 17)
        Me.chkPreACAD.TabIndex = 14
        Me.chkPreACAD.Text = "AutoCAD DXF"
        Me.chkPreACAD.UseVisualStyleBackColor = False
        '
        'chkPNG
        '
        Me.chkPNG.BackColor = System.Drawing.SystemColors.Control
        Me.chkPNG.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPNG.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPNG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPNG.Location = New System.Drawing.Point(11, 411)
        Me.chkPNG.Name = "chkPNG"
        Me.chkPNG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPNG.Size = New System.Drawing.Size(149, 21)
        Me.chkPNG.TabIndex = 38
        Me.chkPNG.Text = "Convert Images To PNG:"
        Me.chkPNG.UseVisualStyleBackColor = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(914, 572)
        Me.Controls.Add(Me.scMain)
        Me.Name = "frmMain"
        Me.Text = "Form1"
        Me.pnlNavigate.ResumeLayout(False)
        Me.pnlDirectory.ResumeLayout(False)
        Me.cmsEdit.ResumeLayout(False)
        CType(Me.pbPicturePreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSelected.ResumeLayout(False)
        Me.pnlSelected.PerformLayout()
        Me.ssCompile.ResumeLayout(False)
        Me.ssCompile.PerformLayout()
        Me.pnlSelectedControls.ResumeLayout(False)
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.grpPlot.ResumeLayout(False)
        Me.fraFormats.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlNavigate As System.Windows.Forms.Panel
    Friend WithEvents pnlDirectory As System.Windows.Forms.Panel
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents lvwFiles As System.Windows.Forms.ListView
    Friend WithEvents tvwFolders As System.Windows.Forms.TreeView
    Friend WithEvents pnlSelected As System.Windows.Forms.Panel
    Friend WithEvents txtEditList As System.Windows.Forms.TextBox
    Friend WithEvents dtpPlotDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboEditList As System.Windows.Forms.ComboBox
    Friend WithEvents lvwACAD As System.Windows.Forms.ListView
    Friend WithEvents ssCompile As System.Windows.Forms.StatusStrip
    Friend WithEvents tssPlots1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents prgPlots As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents pnlSelectedControls As System.Windows.Forms.Panel
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
    Friend WithEvents pbPicturePreview As System.Windows.Forms.PictureBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents chkCT As System.Windows.Forms.CheckBox
    Public WithEvents cmdSelect As System.Windows.Forms.Button
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents chkOverWriteBlock As System.Windows.Forms.CheckBox
    Public WithEvents chkAddAttSize As System.Windows.Forms.CheckBox
    Public WithEvents chkLayer As System.Windows.Forms.CheckBox
    Public WithEvents chkInsert As System.Windows.Forms.CheckBox
    Public WithEvents chkAtt As System.Windows.Forms.CheckBox
    Public WithEvents grpPlot As System.Windows.Forms.GroupBox
    Public WithEvents chkRotate As System.Windows.Forms.CheckBox
    Public WithEvents chkBmp As System.Windows.Forms.CheckBox
    Public WithEvents chkPost As System.Windows.Forms.CheckBox
    Public WithEvents fraFormats As System.Windows.Forms.GroupBox
    Public WithEvents chk2000 As System.Windows.Forms.CheckBox
    Public WithEvents chkDWF As System.Windows.Forms.CheckBox
    Public WithEvents chkPreACAD As System.Windows.Forms.CheckBox
    Friend WithEvents cmsEdit As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiSelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiUnselectAll As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents chkPNG As System.Windows.Forms.CheckBox

End Class
