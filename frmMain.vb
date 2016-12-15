Imports System
Imports System.IO
Imports System.Diagnostics
Imports Microsoft.Win32
Imports System.Resources
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Public Class frmMain
    Public StatusBar As New ProgressStatus
    Private CurrentFolder As String = Nothing
    'Need variables to hold the selected Font and FontColor
    'for the TextGraphic options panel
    Dim addTextFont As Font = Me.Font
    Dim addTextFontColor As Color = Me.ForeColor
    Dim fw As New clsWatcher
    Dim lvwArray() As String = Nothing
    Dim tvwArray() As String = Nothing
    Dim regKey As RegistryKey
    Dim PdfViewer As String
    Dim pdfCnt As Long
    Dim SelectedArray() As String = Nothing
    Dim clItem As ListViewItem
    Dim row As Integer
    Dim col As Integer
    Dim LastDist As Double = 0.35
    Dim State As String
    Dim ErrorString As String
    Dim FilesLoaded As Boolean


#Region "Treeview"

    Private Sub tvwSetup()
        Dim i As Int32
        Dim d() As String = Nothing
        d = System.IO.Directory.GetLogicalDrives()
        If dimVar(d) Then
            For i = 0 To UBound(d)
                Debug.WriteLine(d(i))
                Dim oNode As New System.Windows.Forms.TreeNode
                Try
                    oNode.ImageIndex = 0    ' Closed folder
                    oNode.SelectedImageIndex = 0
                    oNode.Text = d(i) 'Replace(d(i), "\", "") '"C:"
                    tvwFolders.Nodes.Add(oNode)
                    oNode.Nodes.Add("")
                Catch ex As Exception
                    MsgBox("Cannot create initial node:" & ex.ToString)
                    End
                End Try
            Next
        End If
    End Sub

    Private Sub tvwFolders_BeforeExpand(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvwFolders.BeforeExpand
        If e.Node.ImageIndex = 2 Then Exit Sub
        Try
            If e.Node.GetNodeCount(False) = 1 And e.Node.Nodes(0).Text = "" Then
                e.Node.Nodes(0).Remove()
                EnumerateDirs(e.Node)
                CurrentFolder = e.Node.FullPath
            End If
        Catch ex As Exception
            MsgBox("Unable to expand node for " & e.Node.FullPath & ":" & ex.ToString)
            CurrentFolder = Nothing
        End Try

        If e.Node.GetNodeCount(False) > 0 Then
            e.Node.ImageIndex = 1
            e.Node.SelectedImageIndex = 1
        End If
    End Sub

    Private Sub tvwFolders_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvwFolders.BeforeSelect
        lvwFiles.Clear()
        Erase lvwArray
    End Sub

    Private Sub tvwFolders_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwFolders.AfterSelect
        On Error Resume Next
        'Debug.WriteLine(e.Node.FullPath)
        e.Node.Nodes.Clear()
        EnumerateDirs(e.Node)
        CurrentFolder = e.Node.FullPath
    End Sub

    Private Sub tvwFolders_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwFolders.AfterCollapse
        e.Node.ImageIndex = 0
        e.Node.SelectedImageIndex = 0
    End Sub

    Private Sub EnumerateDrvs(ByVal oParent As System.Windows.Forms.TreeNode)

        Dim d() As String = Nothing

        d = System.IO.Directory.GetLogicalDrives()

        Dim en As System.Collections.IEnumerator

        en = d.GetEnumerator
        'While en.MoveNext
        'Console.WriteLine(CStr(en.Current))
        'End While
    End Sub

    Private Sub EnumerateDirs(ByVal oParent As System.Windows.Forms.TreeNode)
        Dim oFS As New DirectoryInfo(oParent.FullPath & "\")
        Dim oDir As DirectoryInfo
        Dim oFile As FileInfo
        Dim fCnt As Long
        'Dim tst() As String = Nothing
        'Dim tstnode As System.Windows.Forms.TreeNode
        'For Each tstnode In oParent
        'lvwFiles.Clear()
        Try
            'Debug.WriteLine(oParent.Text)
            For Each oDir In oFS.GetDirectories()
                Dim oNode As New System.Windows.Forms.TreeNode
                oNode.Text = oDir.Name
                oNode.ImageIndex = 0
                oNode.SelectedImageIndex = 0
                oParent.Nodes.Add(oNode)
                'oNode.Nodes.Add("")
            Next
        Catch ex As Exception
            MsgBox("Unable to enumerate subfolders of " & oParent.FullPath & ":" & ex.ToString)
        End Try

        Try
            'Erase lvwArray
            For Each oFile In oFS.GetFiles()
                Dim ext As String
                If chkPNG.CheckState = 1 Then
                    ext = ".JPG"
                Else
                    ext = ".DWG"
                End If
                If InStr(UCase(oFile.Name), ext) > 0 Then
                    Dim oNode As New System.Windows.Forms.TreeNode
                    ReDim Preserve lvwArray(fCnt)
                    lvwArray(fCnt) = oFile.Name & "," & oFile.Length & "," & oFile.LastWriteTime & "," & oFile.FullName
                    fCnt = fCnt + 1
                    'oNode.Text = oFile.Name '& " (" & oFile.Length & " bytes)"
                    'oNode.ImageIndex = 2
                    'oNode.SelectedImageIndex = 2
                    'oParent.Nodes.Add(oNode)
                End If
            Next
        Catch ex As Exception
            MsgBox("Unable to enumerate files in " & oParent.FullPath & ":" & ex.ToString)
        End Try
        If dimVar(lvwArray) Then
            PopulateLVW()
        End If

    End Sub
#End Region

#Region "ListView"

    Private Sub PopulateLVW()
        FilesLoaded = False
        prg()
        lvwFiles.Clear()
        Dim i As Int32
        Dim cw As Int32
        cw = (lvwFiles.Width - 216)
        'lvwFiles.Clear()
        lvwFiles.Scrollable = True
        ' Set the view to show details.
        lvwFiles.View = View.Details
        ' Allow the user to edit item text.
        lvwFiles.LabelEdit = False
        ' Allow the user to rearrange columns.
        lvwFiles.AllowColumnReorder = True
        ' Display check boxes.
        lvwFiles.CheckBoxes = True
        ' Select the item and subitems when selection is made.
        lvwFiles.FullRowSelect = False
        ' Display grid lines.
        lvwFiles.GridLines = False
        ' Sort the items in the list in ascending order.
        lvwFiles.Sorting = SortOrder.Ascending

        ' Create columns for the items and subitems.
        lvwFiles.Columns.Add("Name", 144, HorizontalAlignment.Left)
        lvwFiles.Columns.Add("Size", 72, HorizontalAlignment.Left)
        lvwFiles.Columns.Add("Modified", cw, HorizontalAlignment.Left)
        On Error Resume Next

        If dimVar(lvwArray) Then
            For i = 0 To UBound(lvwArray)
                Dim item1 As New ListViewItem
                item1.Checked = False
                item1.Text = Split(lvwArray(i), ",")(0) 'i + 1
                'item1.SubItems.Add(Split(lvwArray(i), ",")(0))
                item1.SubItems.Add(Split(lvwArray(i), ",")(1))
                item1.SubItems.Add(Split(lvwArray(i), ",")(2))
                item1.Tag = Split(lvwArray(i), ",")(3)
                'Add the item to the ListView.
                lvwFiles.Items.Add(item1)
            Next
        End If
        FilesLoaded = True
        lvwFiles.Invalidate()
    End Sub

#End Region

#Region "Standard Subs/Functions"

    'is variant dimensioned
    Function dimVar(ByVal ary As Object) As Boolean
        Dim elements As Integer
        elements = 0
        On Error GoTo MemberExit
        If Not ary Is Nothing Then
            'If VarType(ary) >= 1892 Then
            elements = UBound(ary)
            If elements >= 0 Then
                dimVar = True
            Else
                dimVar = False
            End If
        Else
            dimVar = False
        End If
        Err.Clear()
        Exit Function
MemberExit:
        dimVar = False
        Err.Clear()
    End Function

#End Region

#Region "Listview Selected"

    Private Sub lvwSelected_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwACAD.Click
        Dim itmx As ListViewItem
        Dim subStr As String
        Dim i As Int32
        'clObj = Nothing
        clItem = Nothing
        If txtEditList.Visible Then txtEditList.Visible = False
        If cboEditList.Visible Then cboEditList.Visible = False
        For i = 0 To lvwACAD.Items.Count - 1
            On Error Resume Next
            itmx = lvwACAD.Items(i)
            If itmx.Selected = True Then
                Debug.WriteLine(itmx.SubItems(5).Text)
                clItem = itmx
                Exit For
            End If
        Next
    End Sub

    Private Sub lvwSelected_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvwACAD.ColumnClick
        lvwACAD.ListViewItemSorter = New ListViewItemComparer(e.Column)
        lvwACAD.Sort()

        lvwACAD.ListViewItemSorter = New ListViewItemComparer(e.Column)
        lvwACAD.Sort()

        If SortOrder.Ascending = SortOrder.Ascending Then
            lvwACAD.Sorting = SortOrder.Descending
        Else
            lvwACAD.Sorting = SortOrder.Ascending
        End If

        Select Case e.Column
            Case 0
                lvwACAD.ListViewItemSorter = New ListViewItemComparer(e.Column)
                lvwACAD.Sort()
            Case 1
                lvwACAD.ListViewItemSorter = New ListViewItemComparer(e.Column)
                lvwACAD.Sort()
            Case 2
                lvwACAD.ListViewItemSorter = New ListViewItemComparer(e.Column)
                lvwACAD.Sort()
            Case 3
                lvwACAD.ListViewItemSorter = New ListViewItemComparer(e.Column)
                lvwACAD.Sort()
            Case Else
        End Select
    End Sub

    ''' <summary>
    ''' Move selected listview item up or down based on moveUp= True/false.
    ''' </summary>
    ''' <param name = "moveUp"></param>

    Private Sub MoveListViewItem(ByRef lv As ListView, ByVal moveUp As Boolean)
        ' Move selected listview item up or down based on moveUp= True/false.
        Dim i As Integer
        Dim cache As String
        Dim selIdx As Integer


        With lv
            If .SelectedItems.Count = 0 Then
                Exit Sub
            End If
            selIdx = .SelectedItems.Item(0).Index()

            If moveUp Then
                ' ignore moveup of row(0)
                If selIdx = 0 Then
                    Exit Sub
                End If
                ' move the subitems for the previous row
                ' to cache so we can move the selected row up
                For i = 0 To .Items(selIdx).SubItems.Count - 1
                    cache = .Items(selIdx - 1).SubItems(i).Text
                    .Items(selIdx - 1).SubItems(i).Text = _
                       .Items(selIdx).SubItems(i).Text
                    .Items(selIdx).SubItems(i).Text = cache
                Next
                .Items(selIdx - 1).Selected = False
                .Items(selIdx).Selected = False
                .Refresh()
                .Focus()
                .Items(selIdx - 1).Selected = True
            Else
                ' ignore move down of last row
                If selIdx = .Items.Count - 1 Then
                    Exit Sub
                End If
                ' move the subitems for the next row
                ' to cache so we can move the selected row down
                For i = 0 To .Items(selIdx).SubItems.Count - 1
                    cache = .Items(selIdx + 1).SubItems(i).Text
                    .Items(selIdx + 1).SubItems(i).Text = _
                       .Items(selIdx).SubItems(i).Text
                    .Items(selIdx).SubItems(i).Text = cache
                Next
                .Items(selIdx + 1).Selected = False
                .Items(selIdx).Selected = False
                .Refresh()
                .Focus()
                .Items(selIdx + 1).Selected = True

            End If
        End With
    End Sub

    Private Sub PopulateRecursed()
        prg()
        FilesLoaded = False
        lvwACAD.Clear()
        Dim i As Int32
        Dim cw As Int32
        cw = lvwACAD.Width / 6
        'lvwSelected.Clear()
        lvwACAD.Scrollable = True
        ' Set the view to show details.
        lvwACAD.View = View.Details
        ' Allow the user to edit item text.
        lvwACAD.LabelEdit = False
        ' Allow the user to rearrange columns.
        lvwACAD.AllowColumnReorder = True
        ' Display check boxes.
        lvwACAD.CheckBoxes = False
        ' Select the item and subitems when selection is made.
        lvwACAD.FullRowSelect = True
        ' Display grid lines.
        lvwACAD.GridLines = True
        '' Sort the items in the list in ascending order.
        'lvwSelected.Sorting = SortOrder.Ascending
        lvwACAD.Sorting = SortOrder.None
        ' Create columns for the items and subitems.
        lvwACAD.Columns.Add("Selected Files", lvwACAD.Width, HorizontalAlignment.Left)
        lvwACAD.Update()
        On Error Resume Next
        Dim fTest As New clsSharingViolation
        Dim item1 As New ListViewItem
        Dim renFile As String
        Dim cnt As Long


        Dim tStr As String
        Dim ft As New FileTools
        tStr = Replace(CurrentFolder, "\\", "\")
        Dim fg As Object
        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'get all dwg files in sub folders 
        ft.FileExt = ".DWG" 'add period to distinguish type
        ft.FindFolders(tStr)
        fg = ft.FilesList
        If dimVar(fg) Then
            For i = 0 To UBound(fg)
                Dim item2 As New ListViewItem
                item2.Tag = fg(i)
                item2.Text = fg(i)
                lvwACAD.Items.Add(item2)
                'Debug.WriteLine(tFiles(i))
            Next
        End If

        lvwACAD.Invalidate()
        FilesLoaded = True
        tssPlots1.Text = lvwACAD.Items.Count & " Files"
        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Sub PopulateSelected()
        FilesLoaded = False
        lvwACAD.Clear()
        Dim i As Int32
        Dim cw As Int32
        cw = lvwACAD.Width / 6
        'lvwSelected.Clear()
        lvwACAD.Scrollable = True
        ' Set the view to show details.
        lvwACAD.View = View.Details
        ' Allow the user to edit item text.
        lvwACAD.LabelEdit = False
        ' Allow the user to rearrange columns.
        lvwACAD.AllowColumnReorder = True
        ' Display check boxes.
        lvwACAD.CheckBoxes = False
        ' Select the item and subitems when selection is made.
        lvwACAD.FullRowSelect = True
        ' Display grid lines.
        lvwACAD.GridLines = True
        '' Sort the items in the list in ascending order.
        'lvwSelected.Sorting = SortOrder.Ascending
        lvwACAD.Sorting = SortOrder.None
        ' Create columns for the items and subitems.
        lvwACAD.Columns.Add("Selected Files", lvwACAD.Width, HorizontalAlignment.Left)
        lvwACAD.Update()
        On Error Resume Next
        Dim fTest As New clsSharingViolation
        Dim item1 As New ListViewItem
        Dim renFile As String
        Dim cnt As Long
        For Each item1 In lvwFiles.Items
            If item1.Checked = True Then
                'Debug.WriteLine(item1.Tag)
                Dim item2 As New ListViewItem
                item2.Tag = item1.Tag
                item2.Text = item1.Tag
                'item2.SubItems.Add(System.DateTime.Now.ToShortDateString)
                'item2.SubItems.Add(UCase(Format(System.DateTime.Now, "MMMM dd, yyyy")))
                lvwACAD.Items.Add(item2)
                cnt = cnt + 1
            End If
        Next
        lvwACAD.Invalidate()
        FilesLoaded = True
    End Sub

    <STAThread()> _
     Shared Sub Main()
        Application.Run(New frmMain)
    End Sub 'Main

    <StructLayout(LayoutKind.Sequential)> _
    Structure RECT
        Public left As Integer
        Public top As Integer
        Public right As Integer
        Public bottom As Integer
    End Structure 'RECT

    <DllImport("user32.dll")> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByRef lParam As RECT) As Integer
    End Function

    Public Function GetListViewSubItem(ByVal listView1 As ListView, ByVal pt As Point) As Integer
        Const LVM_FIRST As Integer = &H1000
        Const LVM_GETSUBITEMRECT As Integer = LVM_FIRST + 56
        Const LVIR_BOUNDS As Integer = 0
        col = 0
        row = 0
        Dim myrect As RECT
        Dim lvitem As ListViewItem = listView1.GetItemAt(pt.X, pt.Y)
        If lvitem Is Nothing AndAlso listView1.SelectedItems.Count > 0 Then
            lvitem = listView1.SelectedItems(0)
        End If
        Dim intLVSubItemIndex As Integer = -1
        Dim LVSubItem As ListViewItem.ListViewSubItem = Nothing
        If Not (lvitem Is Nothing) Then
            row = lvitem.Index + 1
            Dim intSendMessage As Integer
            Dim i As Integer
            cboEditList.Tag = ""
            txtEditList.Tag = ""
            dtpPlotDate.Tag = ""
            For i = 1 To lvitem.SubItems.Count - 1
                LVSubItem = lvitem.SubItems(i)
                myrect = New RECT
                myrect.top = i
                myrect.left = LVIR_BOUNDS
                intSendMessage = SendMessage(listView1.Handle, LVM_GETSUBITEMRECT, lvitem.Index, myrect)
                If pt.X < myrect.left Then
                    LVSubItem = lvitem.SubItems(0)
                    intLVSubItemIndex = 0
                    Exit For
                ElseIf pt.X >= myrect.left And pt.X <= myrect.right Then
                    intLVSubItemIndex = i
                    col = intLVSubItemIndex + 1
                    If i = 1 Then
                        cboEditList.Visible = True
                        cboEditList.Top = myrect.top + ssCompile.Height
                        cboEditList.Left = myrect.left
                        cboEditList.Width = (myrect.right - myrect.left)
                        cboEditList.Height = (myrect.bottom - myrect.top)
                        cboEditList.Tag = lvwACAD.Columns.Item(i).Text
                    ElseIf i = 2 Then
                        dtpPlotDate.Visible = True
                        dtpPlotDate.Top = myrect.top
                        dtpPlotDate.Left = myrect.left
                        dtpPlotDate.Width = (myrect.right - myrect.left)
                        dtpPlotDate.Height = (myrect.bottom - myrect.top)
                        dtpPlotDate.Tag = lvwACAD.Columns.Item(i).Text
                    Else
                        txtEditList.Visible = True
                        txtEditList.Top = myrect.top
                        txtEditList.Left = myrect.left
                        txtEditList.Width = (myrect.right - myrect.left)
                        txtEditList.Height = (myrect.bottom - myrect.top)
                        txtEditList.Tag = lvwACAD.Columns.Item(i).Text
                        txtEditList.Focus()
                    End If
                    Exit For
                Else
                    LVSubItem = Nothing
                    txtEditList.Visible = False
                    cboEditList.Visible = False
                    dtpPlotDate.Visible = False
                End If
            Next i
        End If
        If LVSubItem Is Nothing OrElse lvitem Is Nothing Then
            intLVSubItemIndex = -1
            txtEditList.Visible = False
            cboEditList.Visible = False
            dtpPlotDate.Visible = False
        End If
        Return intLVSubItemIndex
    End Function

    Private Sub eList(ByVal clItem As ListViewItem, ByVal subNo As Int32)
        On Error Resume Next
        If subNo = 1 Then
            'Me.dtpPlotDate.Visible = False
            Me.cboEditList.Items.Clear()
            Me.cboEditList.Items.AddRange(New Object() {"NONE", "OUT FOR APPROVAL", "DEADMAN LAYOUT", "DISTRIBUTION SET"})
            Me.cboEditList.Visible = True
        End If
        If subNo = 2 Then
            'Me.cboEditList.Visible = False
            'Me.dtpPlotDate.Visible = True
        End If
    End Sub

    Private Sub lvwSelected_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        On Error Resume Next

        Select Case e.KeyData
            Case Keys.Delete
                'PopulateRevisions()
                txtEditList.Text = ""
                txtEditList.Visible = False
                cboEditList.Visible = False
                dtpPlotDate.Visible = False
                lvwACAD.Scrollable = True
            Case Keys.Escape
                txtEditList.Text = ""
                txtEditList.Visible = False
                cboEditList.Visible = False
                dtpPlotDate.Visible = False
                clItem.Selected = False
                lvwACAD.Scrollable = True
            Case Keys.Down, Keys.Up
                txtEditList.Text = ""
                txtEditList.Visible = False
                cboEditList.Visible = False
                dtpPlotDate.Visible = False
                lvwACAD.Scrollable = True
                Dim i As Int32
                If e.KeyData = Keys.Down Then
                    i = clItem.Index + 1
                    If i > lvwACAD.Items.Count Then i = lvwACAD.Items.Count
                Else
                    i = clItem.Index - 1
                    If i < 0 Then i = 0
                End If
                lvwACAD.Focus()
                clItem.Selected = False
                clItem = lvwACAD.Items(i)
                'clObj = Nothing
                If txtEditList.Visible Then txtEditList.Visible = False
        End Select
    End Sub

    Private Sub txtEditList_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditList.Enter
        If col = 2 Then
            txtEditList.Text = Split(dtpPlotDate.Value.ToString(), " ")(0)
        End If
    End Sub

    Private Sub txtEditList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEditList.KeyDown
        On Error Resume Next
        Select Case e.KeyData
            Case Keys.Delete
                'PopulateRevisions()
                'txtEditList.Text = ""
                'txtEditList.Visible = False
                'cboEditList.Visible = False
                'lvObjects.Scrollable = True
            Case Keys.Escape
                txtEditList.Text = ""
                txtEditList.Visible = False
                cboEditList.Visible = False
                clItem.Selected = False
                lvwACAD.Scrollable = True
                lvwACAD.Focus()
        End Select
    End Sub

    Private Sub txtEditList_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditList.Leave
        If txtEditList.Visible Then
            If col = 1 Then
                Debug.WriteLine(row)
            End If
        End If
        'PopulateRevisions()
    End Sub

    Private Sub cboEditList_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEditList.Leave
        If cboEditList.Visible Then
            If col = 2 Then
                Dim item2 As New ListViewItem
                item2 = lvwACAD.Items(row - 1)
                Debug.WriteLine(item2.SubItems(0).Text)
                Debug.WriteLine(item2.SubItems(1).Text)
                Debug.WriteLine(item2.SubItems(2).Text)
                item2.SubItems(1).Text = cboEditList.Text
                lvwACAD.Items(row - 1) = item2
                lvwACAD.Invalidate()
            End If
            cboEditList.Visible = False
        End If
    End Sub

    Private Sub dtpPlotDate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpPlotDate.Leave
        If dtpPlotDate.Visible Then
            If col = 3 Then
                Dim item2 As New ListViewItem
                item2 = lvwACAD.Items(row - 1)
                Debug.WriteLine(dtpPlotDate.Text)
                Debug.WriteLine(item2.SubItems(1).Text)
                Debug.WriteLine(item2.SubItems(2).Text)
                item2.SubItems(2).Text = dtpPlotDate.Value.ToShortDateString
                lvwACAD.Items(row - 1) = item2
                lvwACAD.Invalidate()
            End If
            dtpPlotDate.Visible = False
        End If
    End Sub

    Private Sub lvwFiles_ItemChecked(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles lvwFiles.ItemChecked
        If FilesLoaded Then
            PopulateSelected()
        End If
    End Sub

    Private Sub lvwSelected_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvwACAD.MouseUp
        Dim mStr As String
        Dim selectedSubItem As Integer = Me.GetListViewSubItem(Me.lvwACAD, New Point(e.X, e.Y))
        If selectedSubItem = -1 Then
            mStr = "No SubItem clicked"
            txtEditList.Visible = False
            cboEditList.Visible = False
            lvwACAD.Scrollable = True
        Else
            If Not clItem Is Nothing Then
                eList(clItem, selectedSubItem)
            End If
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Me.MoveListViewItem(Me.lvwACAD, moveUp:=True)
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Me.MoveListViewItem(Me.lvwACAD, moveUp:=False)
    End Sub


#End Region

#Region "Auto Updates"
    Private Sub CleanUp()
        Dim tStr As String
        Dim ft As New FileTools
        Dim tFiles() As String = Nothing
        Dim i As Int32
        tStr = Application.StartupPath & "\"
        tFiles = ft.FileFind(tStr, "tmp")
        If Not tFiles Is Nothing Then
            For i = 0 To UBound(tFiles)
                'Debug.WriteLine(tFiles(i))
                ft.FileDelete("" & tFiles(i))
            Next
        End If
    End Sub


#End Region

#Region "Acad Support Files"

    Private Sub acPMP()
        Dim acReg As String = Nothing
        'regKey = Registry.CurrentUser.OpenSubKey("Software\Autodesk\AutoCAD\R15.0\ACAD-1:409\Profiles\Standard\General", True)
        regKey = Registry.CurrentUser.OpenSubKey("Software\Autodesk\AutoCAD\R15.0\ACAD-1:409\Profiles\Fabcad\General", True)
        If regKey Is Nothing Then
            ' Key doesn't exist
            'regKey.Close()
            MsgBox("Autocad Profile: Fabcad PrinterDescDir Error", MsgBoxStyle.Critical, "Fabcad Plot Request Error")
            Exit Sub
        End If
        Dim int As Integer = 0
        If (Not regKey Is Nothing) Then
            acReg = regKey.GetValue("PrinterDescDir", 0)
            regKey.Close()
        End If
        acReg = acReg & "\TIFF.pmp"
        'If Not File.Exists(acReg) Then
        Dim rm As New ResourceManager("Fabcon.Plot.DesignSurface.Acad", _
            [Assembly].GetExecutingAssembly())
        Dim rdr As Object
        rdr = rm.GetObject("TIFF.pmp")
        Dim fs As New FileStream(acReg, FileMode.Create)
        Dim w As New BinaryWriter(fs)
        Dim i As Integer
        For i = 0 To UBound(rdr)
            w.Write(rdr(i))
        Next i
        w.Close()
        fs.Close()
        'End If
    End Sub

    Private Sub acPC3()
        Dim acReg As String = Nothing
        regKey = Registry.CurrentUser.OpenSubKey("Software\Autodesk\AutoCAD\R15.0\ACAD-1:409\Profiles\Fabcad\General", True)
        If regKey Is Nothing Then
            ' Key doesn't exist
            'regKey.Close()
            MsgBox("Autocad Profile: Fabcad PrinterConfigDir Error", MsgBoxStyle.Critical, "Fabcad Plot Request Error")
            Exit Sub
        End If
        Dim int As Integer = 0
        If (Not regKey Is Nothing) Then
            acReg = regKey.GetValue("PrinterConfigDir", 0)
            regKey.Close()
        End If
        acReg = acReg & "\TIFF.pc3"
        'If Not File.Exists(acReg) Then
        Dim rm As New ResourceManager("Fabcon.Plot.DesignSurface.Acad", _
            [Assembly].GetExecutingAssembly())
        Dim rdr As Object
        rdr = rm.GetObject("TIFF.pc3")
        Dim fs As New FileStream(acReg, FileMode.Create)
        Dim w As New BinaryWriter(fs)
        Dim i As Integer
        For i = 0 To UBound(rdr)
            w.Write(rdr(i))
        Next i
        w.Close()
        fs.Close()
        'End If
    End Sub

    Private Sub acCTB()
        Dim acReg As String = Nothing
        regKey = Registry.CurrentUser.OpenSubKey("Software\Autodesk\AutoCAD\R15.0\ACAD-1:409\Profiles\Fabcad\General", True)
        If regKey Is Nothing Then
            ' Key doesn't exist
            'regKey.Close()
            MsgBox("Autocad Profile: Fabcad PrinterStyleSheetDir Error", MsgBoxStyle.Critical, "Fabcad Plot Request Error")
            Exit Sub
        End If
        Dim int As Integer = 0
        If (Not regKey Is Nothing) Then
            acReg = regKey.GetValue("PrinterStyleSheetDir", 0)
            regKey.Close()
        End If
        acReg = acReg & "\Tiff.ctb"
        'If Not File.Exists(acReg) Then
        Dim rm As New ResourceManager("Fabcon.Plot.DesignSurface.Acad", _
            [Assembly].GetExecutingAssembly())
        Dim rdr As Object
        rdr = rm.GetObject("Tiff.ctb")
        Dim fs As New FileStream(acReg, FileMode.Create)
        Dim w As New BinaryWriter(fs)
        Dim i As Integer
        For i = 0 To UBound(rdr)
            w.Write(rdr(i))
        Next i
        w.Close()
        fs.Close()
        'End If
    End Sub

#End Region

#Region "Progress Bar"

    Private Sub prg()
        Dim item1 As New ListViewItem
        pdfCnt = 0
        For Each item1 In lvwACAD.Items
            pdfCnt = pdfCnt + 1
        Next
        If pdfCnt = 0 Then
            prgPlots.Value = 0
            prgPlots.Maximum = 0
            prgPlots.Minimum = 0
        Else
            prgPlots.Maximum = pdfCnt
            prgPlots.Minimum = 0
        End If
    End Sub

    Private Sub prgReset()
        prgPlots.Maximum = 0
        prgPlots.Minimum = 0
        prgPlots.Value = 0
        tssPlots1.Text = "Ready: " & lvwACAD.Items.Count & " Files"
    End Sub


#End Region

#Region "ACAD"

    Sub LoadAutoCAD()
        On Error Resume Next
        AcadAPP = GetObject(, "AutoCAD.application")
        If Err.Number Then
            AcadAPP = CreateObject("AutoCAD.Application")
            Err.Clear()
        End If
        On Error Resume Next
        AcadDOC = AcadAPP.ActiveDocument

        If Err.Number Then
            Err.Clear()
            AcadAPP.Documents.Add("Drawing1.DWG")
            If Err.Number Then
                MsgBox("AutoCAD not found!")
                AcadDOC = Nothing
                AcadAPP = Nothing
            End If
        Else
            'Set paSpace = AcadDoc.PaperSpace
            'Set moSpace = AcadDoc.ModelSpace
            AcadAPP.Visible = True

        End If
    End Sub

    Private Sub runBatch(ByRef fname As String)
        Dim tst As String
        Dim ssx As AutoCAD.AcadSelectionSet
        Dim grpX(0) As Short
        Dim datX(0) As Object
        Dim UpdEnt As AutoCAD.AcadEntity
        Dim basDbl(2) As Double
        Dim basPt As Object
        Dim ssAtts As AutoCAD.AcadSelectionSet
        Dim att As AutoCAD.AcadEntity
        Dim acblk As AutoCAD.AcadBlockReference
        Dim iPt(2) As Double
        Dim clr As Integer
        Dim X As Object
        Dim y As Object
        Dim insertionPoint(2) As Double
        Dim attributeObj As AutoCAD.AcadAttribute
        Dim bpt As Object
        Dim ll As Object
        Dim ur As Object
        Dim dn As String
        Dim pos1 As Single
        Dim pos2 As Single
        Dim pos3 As Single
        Dim pos4 As Single
        Dim dwgName As String
        Dim dwgPath As String
        Dim wd As Double
        Dim ht As Double
        Dim szWid As String
        Dim szHt As String
        Dim sysVarName As String = Nothing
        Dim minData As Object
        Dim maxData As Object
        If chkInsert.CheckState = 0 Then
            If chkOverWriteBlock.CheckState > 0 Then
                AcadAPP.Documents.Add()
                AcadDOC = AcadAPP.ActiveDocument
            Else
                AcadAPP.Documents.Open(fname)
                AcadDOC = AcadAPP.ActiveDocument
                If AcadDOC.FullName = "" Then GoTo byPass
            End If
        Else
            LoadAutoCAD()
            If AcadDOC.FullName <> "" Then GoTo byPass
        End If

        If AcadDOC.ActiveSpace = AutoCAD.AcActiveSpace.acPaperSpace Then
            AcadAPP.ZoomExtents()
            y = AcadDOC.GetVariable("extmin")
            X = AcadDOC.GetVariable("extmax")
            X = Format(X(0), "0.0000")
            y = Format(y(1), "0.0000")

            'AcadAPP.ZoomScaled 0.95, acZoomScaledRelative
            On Error Resume Next
            ''base point
            ''basPt = AcadDOC.GetVariable("INSBASE")
            ''X = 112.68309939     Y = 97.12201524     Z = 0.00000000
            ''basDbl(0) = 0: basDbl(1) = 0: basDbl(2) = 0
            ''AcadDOC.SetVariable "INSBASE", basDbl
            AcadDOC.SetVariable("CLAYER", "0")
            '----------------------------------------------------------
            'Set ssx = AcadDOC.SelectionSets.Add("xSS")
            'If Err Then Set ssx = AcadDOC.SelectionSets.item("xSS")
            'Err.Clear
            'ssx.Clear
            'grpX(0) = 8
            'datX(0) = "*"
            'ssx.Select acSelectionSetAll, , , grpX, datX
            'If ssx.Count > 0 Then
            '    For Each UpdEnt In ssx
            '        UpdEnt.Layer = acByLayer
            '        UpdEnt.Color = acByLayer
            '        UpdEnt.Update
            '    Next
            'End If
            'WFR
            '-----------------------------------------------------------
            If chkLayer.CheckState = 1 Then

                ssAtts = ssGet("*", "*")
                If ssAtts.Count > 0 Then
                    For Each att In ssAtts
                        clr = att.Color
                        att.Layer = "0"
                    Next att
                End If
            End If

            'add Attdef size
            If chkAddAttSize.CheckState = 1 Then
                insertionPoint(1) = -0.125
                attributeObj = AcadDOC.PaperSpace.AddAttribute(0.125, AutoCAD.AcAttributeMode.acAttributeModeInvisible + AutoCAD.AcAttributeMode.acAttributeModeConstant, "", insertionPoint, "SIZE", X & "," & y)
                attributeObj.Color = AutoCAD.AcColor.acWhite
                attributeObj.Layer = "0"
                attributeObj.Visible = False
            End If

            'Hide Attdefs
            If chkAtt.CheckState = 1 Then
                ssAtts = ssGet("*", "*")
                If ssAtts.Count > 0 Then
                    For Each att In ssAtts
                        If att.ObjectName = "AcDbAttributeDefinition" Then
                            att.Visible = False
                            AcadAPP.ZoomExtents()
                            AcadDOC.SetVariable("lunits", 2)
                            AcadDOC.SetVariable("luprec", 5)
                            sysVarName = "EXTMIN"
                            minData = AcadDOC.GetVariable(sysVarName)
                            sysVarName = "EXTMAX"
                            maxData = AcadDOC.GetVariable(sysVarName)
                            ht = maxData(1) - minData(1)
                            wd = maxData(0) - minData(0)
                            'UPGRADE_WARNING: Couldn't resolve default property of object AcadDOC.GetVariable(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                            dwgName = AcadDOC.GetVariable("DWGNAME")
                            sysVarName = Replace(UCase(dwgName), ".DWG", "")
                            Debug.Print(sysVarName & "|" & System.Math.Round(wd, 2) & "," & System.Math.Round(ht, 2)) '& Chr(10)
                        End If
                    Next att
                    If sysVarName = "" Then
                        AcadAPP.ZoomExtents()
                        AcadDOC.SetVariable("lunits", 2)
                        AcadDOC.SetVariable("luprec", 5)
                        sysVarName = "EXTMIN"
                        minData = AcadDOC.GetVariable(sysVarName)
                        sysVarName = "EXTMAX"
                        maxData = AcadDOC.GetVariable(sysVarName)
                        ht = maxData(1) - minData(1)
                        wd = maxData(0) - minData(0)
                        'UPGRADE_WARNING: Couldn't resolve default property of object AcadDOC.GetVariable(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        dwgName = AcadDOC.GetVariable("DWGNAME")
                        sysVarName = Replace(UCase(dwgName), ".DWG", "")
                        Debug.Print(sysVarName & "|" & System.Math.Round(wd, 2) & "," & System.Math.Round(ht, 2)) '& Chr(10)
                    End If
                End If
            Else
                'Set ssAtts = ssGet("*", "AttributeReference")
                'If ssAtts.Count > 0 Then
                '    For Each att In ssAtts
                '        att.Visible = True
                '    Next
                'End If
            End If
            If chkOverWriteBlock.CheckState > 0 Then
                dwgName = AcadDOC.GetVariable("DWGNAME")
                dwgPath = AcadDOC.GetVariable("DWGPREFIX")
                dwgName = dwgPath & dwgName
                iPt(0) = 0 : iPt(1) = 0 : iPt(2) = 0
                acblk = AcadDOC.ModelSpace.InsertBlock(iPt, fname, 1.0#, 1.0#, 1.0#, 0)
                AcadAPP.ZoomExtents()
                acblk.Explode()
                acblk.Delete()
                AcadDOC.SaveAs(dwgName)
            End If
            If chkInsert.CheckState = 0 Then
                'fix any problems found
                AcadDOC.AuditInfo(True)
                If chk2000.Checked Then
                    AcadDOC.PurgeAll()
                    savDWG(fname)
                End If
                If chkDWF.Checked Then
                    xrxPS() 'dwf modelspace
                End If
                If chkPreACAD.Checked Then
                    expDXF(fname)
                End If
            Else
                iPt(0) = 0 : iPt(1) = 0 : iPt(2) = 0
                acblk = AcadDOC.PaperSpace.InsertBlock(iPt, fname, 1.0#, 1.0#, 1.0#, 0)
                AcadAPP.ZoomExtents()
                tst = Replace(UCase(fname), ".DWG", ".BMP")
                tst = Mid(tst, InStrRev(tst, "\") + 1)
                PlotToBmp(tst, (chkRotate.CheckState))
                acblk.Delete()
            End If
            If chkPost.CheckState = 1 Then
                tst = Replace(UCase(fname), ".DWG", ".EPS")
                WindowToPlot(tst, (chkRotate.CheckState))
            End If
            If chkBmp.CheckState = 1 Then
                tst = Replace(UCase(fname), ".DWG", "")
                tst = Mid(tst, InStrRev(tst, "\") + 1)
                PlotToBmp(tst, (chkRotate.CheckState))
            End If

        Else
            AcadAPP.ZoomExtents()
            'UPGRADE_WARNING: Couldn't resolve default property of object AcadDOC.GetVariable(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object y. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            y = AcadDOC.GetVariable("extmin")
            'UPGRADE_WARNING: Couldn't resolve default property of object AcadDOC.GetVariable(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object X. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            X = AcadDOC.GetVariable("extmax")
            'UPGRADE_WARNING: Couldn't resolve default property of object X(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object X. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            X = Format(X(0), "0.0000")
            'UPGRADE_WARNING: Couldn't resolve default property of object y(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object y. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            y = Format(y(1), "0.0000")

            'AcadAPP.ZoomScaled 0.95, acZoomScaledRelative
            On Error Resume Next
            ''base point
            ''basPt = AcadDOC.GetVariable("INSBASE")
            ''X = 112.68309939     Y = 97.12201524     Z = 0.00000000
            ''basDbl(0) = 0: basDbl(1) = 0: basDbl(2) = 0
            ''AcadDOC.SetVariable "INSBASE", basDbl
            AcadDOC.SetVariable("CLAYER", "0")
            '----------------------------------------------------------
            'Set ssx = AcadDOC.SelectionSets.Add("xSS")
            'If Err Then Set ssx = AcadDOC.SelectionSets.item("xSS")
            'Err.Clear
            'ssx.Clear
            'grpX(0) = 8
            'datX(0) = "*"
            'ssx.Select acSelectionSetAll, , , grpX, datX
            'If ssx.Count > 0 Then
            '    For Each UpdEnt In ssx
            '        UpdEnt.Layer = acByLayer
            '        UpdEnt.Color = acByLayer
            '        UpdEnt.Update
            '    Next
            'End If
            'WFR
            '-----------------------------------------------------------
            If chkLayer.CheckState = 1 Then
                ssAtts = ssGet("*", "*")
                If ssAtts.Count > 0 Then
                    For Each att In ssAtts
                        clr = att.Color
                        att.Layer = "0"
                        att.Color = clr
                        'If att.Layer <> "0" Then
                        '    'Debug.Print ""
                        '    Debug.Print "File Name = " & fname
                        '    'Debug.Print "Layer Name = " & att.Layer & "    Color = " & clr
                        'End If
                    Next att
                End If
            End If

            'add Attdef size
            If chkAddAttSize.CheckState = 1 Then
                insertionPoint(1) = -0.125
                attributeObj = AcadDOC.ModelSpace.AddAttribute(0.125, AutoCAD.AcAttributeMode.acAttributeModeInvisible + AutoCAD.AcAttributeMode.acAttributeModeConstant, "", insertionPoint, "SIZE", X & "," & y)
                attributeObj.Color = AutoCAD.AcColor.acWhite
                attributeObj.Layer = "0"
                attributeObj.Visible = False
            End If


            'Hide Attdefs
            If chkAtt.CheckState = 1 Then
                ssAtts = ssGet("*", "*")
                If ssAtts.Count > 0 Then
                    For Each att In ssAtts
                        If att.ObjectName = "AcDbAttributeDefinition" Then
                            att.Visible = False
                            AcadAPP.ZoomExtents()
                            AcadDOC.SetVariable("lunits", 2)
                            AcadDOC.SetVariable("luprec", 5)
                            sysVarName = "EXTMIN"
                            minData = AcadDOC.GetVariable(sysVarName)
                            sysVarName = "EXTMAX"
                            maxData = AcadDOC.GetVariable(sysVarName)
                            ht = maxData(1) - minData(1)
                            wd = maxData(0) - minData(0)
                            dwgName = AcadDOC.GetVariable("DWGNAME")
                            sysVarName = Replace(UCase(dwgName), ".DWG", "")
                            'Debug.Print(sysVarName & "|" & System.Math.Round(wd, 2) & "," & System.Math.Round(ht, 2)) '& Chr(10)
                            'AcadAPP.ZoomScaled 0.95, acZoomScaledRelative
                        End If
                    Next att
                    If sysVarName = "" Then
                        AcadAPP.ZoomExtents()
                        AcadDOC.SetVariable("lunits", 2)
                        AcadDOC.SetVariable("luprec", 5)
                        sysVarName = "EXTMIN"
                        minData = AcadDOC.GetVariable(sysVarName)
                        sysVarName = "EXTMAX"
                        maxData = AcadDOC.GetVariable(sysVarName)
                        ht = maxData(1) - minData(1)
                        wd = maxData(0) - minData(0)
                        dwgName = AcadDOC.GetVariable("DWGNAME")
                        sysVarName = Replace(UCase(dwgName), ".DWG", "")
                        Debug.Print(sysVarName & "|" & System.Math.Round(wd, 2) & "," & System.Math.Round(ht, 2)) '& Chr(10)
                    End If
                End If
            Else
                'Set ssAtts = ssGet("*", "AttributeReference")
                'If ssAtts.Count > 0 Then
                '    For Each att In ssAtts
                '        att.Visible = True
                '    Next
                'End If

            End If
            If chkOverWriteBlock.CheckState > 0 Then
                dwgName = AcadDOC.GetVariable("DWGNAME")
                dwgPath = AcadDOC.GetVariable("DWGPREFIX")
                dwgName = dwgPath & dwgName
                iPt(0) = 0 : iPt(1) = 0 : iPt(2) = 0
                acblk = AcadDOC.ModelSpace.InsertBlock(iPt, fname, 1.0#, 1.0#, 1.0#, 0)
                AcadAPP.ZoomExtents()
                acblk.Explode()
                acblk.Delete()
                AcadDOC.SaveAs(dwgName)
            End If


            If chkInsert.CheckState = 0 Then
                'fix any problems found
                AcadDOC.AuditInfo(True)
                If chk2000.Checked Then
                    AcadDOC.PurgeAll()
                    savDWG(fname)
                End If
                If chkDWF.Checked Then
                    xrxMS() 'dwf modelspace
                End If
                If chkPreACAD.Checked Then
                    expDXF(fname)
                End If
            Else
                iPt(0) = 0 : iPt(1) = 0 : iPt(2) = 0
                acblk = AcadDOC.ModelSpace.InsertBlock(iPt, fname, 1.0#, 1.0#, 1.0#, 0)
                AcadAPP.ZoomExtents()
                tst = Replace(UCase(fname), ".DWG", ".PNG")
                tst = Mid(tst, InStrRev(tst, "\") + 1)
                PlotToBmp(tst, (chkRotate.CheckState))
                acblk.Delete()
            End If

            'postscript plot
            If chkPost.CheckState = 1 Then
                tst = Replace(UCase(fname), ".DWG", ".EPS")
                WindowToPlot(tst, (chkRotate.CheckState))
            End If
            'png plot
            If chkBmp.CheckState = 1 Then
                tst = Replace(UCase(fname), ".DWG", ".PNG")
                'tst = Mid(tst, (InStrRev(tst, "\") + 1))
                'PlotToBmp(tst, (chkRotate.CheckState))
                PlotEndviewToBmp(tst, (chkRotate.CheckState))
            End If

        End If

        'Extents file
        AcadAPP.ZoomExtents()
        dn = ""
        dn = AcadDOC.GetVariable("DWGNAME")
        dn = Replace(UCase(dn), ".DWG", "")
        ll = AcadDOC.GetVariable("EXTMIN")
        ur = AcadDOC.GetVariable("EXTMAX")
        If dimVar(Arr) Then
            ReDim Preserve Arr(UBound(Arr) + 1)
        Else
            ReDim Preserve Arr(0)
        End If
        pos1 = CSng(Format(ur(0) - ll(0), "0.0"))
        If pos1 = 0 Then pos1 = 0.015
        pos2 = CSng(Format(ur(1) - ll(1), "0.0"))
        If pos2 = 0 Then pos2 = 0.015
        Arr(UBound(Arr)) = (dn & "=" & pos1 & "," & pos2)
byPass:
        'close up shop
        If chkInsert.CheckState = 0 Then
            AcadDOC.Close(False, AcadDOC.Name)
        End If
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub dwfExport()
        Dim tst As String
        Dim exportFile As String
        Dim sset As AutoCAD.AcadSelectionSet

        tst = "d:\fdrive\" & Mid(CurrentFolder, 4)
        'Find current tilemode and zoom extents
        Dim existPViewport As AutoCAD.AcadPViewport
        If AcadDOC.ActiveSpace = 0 Then
            ' Set acad to paperspace
            AcadDOC.ActiveSpace = AutoCAD.AcActiveSpace.acPaperSpace
            existPViewport = AcadDOC.ActivePViewport
            existPViewport.Display(True)
            AcadDOC.MSpace = False

            ' Create an empty selection set
            sset = AcadDOC.SelectionSets.Add("TEST")
            AcadDOC.ActivePViewport.ZoomExtents()
        Else
            sset = AcadDOC.SelectionSets.Add("TEST")
            'Zoom Extents
            AcadDOC.ActiveViewport.ZoomExtents()
        End If

        If File.Exists(tst) Then
            ' This example exports the current drawing to DXF format.
            ' Note that a valid selection set must be provided, even
            ' though the contents of the selection set are ignored.
            ' Define the name for the exported file
            ' Adjust path to match your system
            exportFile = tst & "\" & AcadDwg
            ' Export the current drawing to the file specified above.
            AcadDOC.Export(exportFile, "DWF", sset)
        Else
            Directory.CreateDirectory(tst)
            exportFile = tst & "\" & AcadDwg
            AcadDOC.Export(exportFile, "DWF", sset)
        End If
    End Sub

    Sub Active_PV()
        Dim newPViewport As AutoCAD.AcadPViewport
        Dim centerPoint(2) As Double
        Dim height_Renamed As Double
        Dim width_Renamed As Double
        height_Renamed = 21.0#
        width_Renamed = 15.5549
        centerPoint(0) = 9.4113 : centerPoint(1) = 10.7072 : centerPoint(2) = 0.0#

        ' Create a paperspace Viewport object
        AcadDOC.ActiveSpace = AutoCAD.AcActiveSpace.acPaperSpace

        newPViewport = AcadDOC.PaperSpace.AddPViewport(centerPoint, width_Renamed, height_Renamed)
        AcadDOC.ActivePViewport.ZoomAll()
        newPViewport.Display((True))

        ' Before making a pViewport active,
        ' the mspace property needs to be True
        AcadDOC.MSpace = True
        'frmDrawTank.acadDOC.paSpace.ActivePViewport = newPViewport
        AcadDOC.ActivePViewport.ZoomExtents()
    End Sub

    Private Sub filArray()
        LoadAutoCAD()
        'Dim selFile As Integer
        AcadPath = CurrentFolder
        If lvwACAD.Items.Count > 0 Then
            prg()
            Dim cnt As Int32 = 1
            Dim item1 As New ListViewItem
            For Each item1 In lvwACAD.Items
                If chkPNG.CheckState = 1 Then
                    Dim bm As Bitmap = Bitmap.FromFile(item1.Tag.ToString)
                    Dim ft As New FileTools
                    Dim strDN As String = ft.FolderFromFileName(item1.Tag.ToString)
                    Dim strFN As String = ft.NameOnlyFromFullPath(item1.Tag.ToString)
                    strFN = ft.FileNameWoExt(strFN)
                    Dim fn As String = UCase(strDN & strFN & ".PNG")
                    bm.Save(fn, System.Drawing.Imaging.ImageFormat.Png)
                    bm.Dispose()
                    prgPlots.Value = cnt
                Else
                    tssPlots1.Text = "Processing: " & cnt & " Of " & pdfCnt
                    runBatch(item1.Tag)
                    prgPlots.Value = cnt
                End If
                cnt = (cnt + 1)
            Next
            lvwACAD.Invalidate()
        End If
        prgReset()
    End Sub

    Sub style_Item()
        Dim tStyle As Short
        Dim TextStyle As AutoCAD.AcadTextStyle
        Dim TextColl As Object
        TextColl = AcadDOC.TextStyles
        For Each TextStyle In TextColl
            TextStyle = TextColl.item(tStyle)
            TextStyle.fontFile = "fabcad.shx"
            tStyle = tStyle + 1
        Next TextStyle
    End Sub

    Sub xrxMS()
        ' Verify that the active space is model space
        AcadDOC.ActiveSpace = AutoCAD.AcActiveSpace.acModelSpace

        ' Set the extents and scale of the plot area
        AcadDOC.ModelSpace.Layout.PlotType = AutoCAD.AcPlotType.acExtents
        AcadDOC.ModelSpace.Layout.StandardScale = AutoCAD.AcPlotScale.acScaleToFit
        ' Set the number of copies to one
        AcadDOC.Plot.NumberOfCopies = 1
        Dim currentPlot As AutoCAD.AcadPlot
        Dim plotFileName As String
        plotFileName = Replace(AcadDOC.GetVariable("dwgname"), ".DWG", ".DWF")
        plotFileName = AcadDOC.GetVariable("dwgprefix") & plotFileName
        currentPlot = AcadDOC.Plot

        ' Send Plot To Window
        'AcadDoc.Plot.DisplayPlotPreview acFullPreview
        Dim tst As Boolean
        ' Initiate the plot
        tst = AcadDOC.Plot.PlotToFile(plotFileName, "d:\acad2000\plotters\DWF ePlot.pc3")
        Threading.Thread.Sleep(1000)
    End Sub

    Sub xrxPS()
        Dim lItem As AutoCAD.AcadLayout
        Dim point1(1) As Double
        'Dim point2(0 To 1) As Double
        lItem = AcadDOC.Layouts.item("Layout1")
        'Dim extMin As Variant
        'Dim extMax As Variant

        AcadDOC.ActiveSpace = AutoCAD.AcActiveSpace.acPaperSpace 'acPaperSpace
        AcadDOC.Application.ZoomExtents()
        'extMin = AcadDoc.GetVariable("extMin")
        'extMax = AcadDoc.GetVariable("extMax")
        point1(0) = 0 : point1(1) = 0
        'point1(0) = extMin(0): point1(1) = extMin(1)
        'point2(0) = extMax(0): point2(1) = extMax(1)
        Dim mediaNames As Object
        'Set Configuration
        lItem.ConfigName = "DWF ePlot.pc3"
        mediaNames = lItem.GetCanonicalMediaNames()
        'acaddoc.ActiveLayout.PlotType = acWindow
        With lItem
            .ConfigName = "DWF ePlot.pc3"
            .StyleSheet = "hplaser.ctb"
            .CanonicalMediaName = mediaNames(UBound(mediaNames))
            .PlotWithLineweights = True
            .PlotOrigin = (point1)
            '    '.PlotRotation = ac90degrees
            '    '.SetWindowToPlot point1, point2
            '    'Plot type must be run after sewindowtoplot or viewtoplot
            '    .PlotType = acExtents:
            .CenterPlot = True
            .PlotRotation = AutoCAD.AcPlotRotation.ac0degrees
            '    ' Read back window information
            '    '.GetWindowToPlot point1, point2
        End With
        AcadDOC.ActiveSpace = AutoCAD.AcActiveSpace.acPaperSpace 'acPaperSpace
        ' Make sure we tell the drawing to plot a view, not some other plot style
        AcadDOC.ActiveLayout.PlotType = AutoCAD.AcPlotType.acExtents
        'Dim currentPlot As AcadPlot
        Dim plotFileName As String
        plotFileName = Replace(AcadDOC.GetVariable("dwgname"), ".DWG", ".DWF")
        plotFileName = AcadDOC.GetVariable("dwgprefix") & plotFileName
        'Set currentPlot = AcadDoc.Plot
        Dim tst As Boolean
        ' Initiate the plot
        tst = AcadDOC.Plot.PlotToFile(plotFileName, "d:\acad2000\plotters\DWF ePlot.pc3")
        Threading.Thread.Sleep(1000)
    End Sub

    Public Sub expDXF(ByRef savFile As String)
        If AcadDOC.FullName <> "" Then
            On Error Resume Next
            If AcadDOC.ActiveSpace = AutoCAD.AcActiveSpace.acPaperSpace Then
                AcadAPP.ZoomExtents()

                If chkCT.CheckState = 1 Then
                    savFile = Replace(UCase(savFile), ".DWG", ".DXF")
                    savFile = Replace(UCase(savFile), UCase(AcadPath), UCase(""))
                    AcadDOC.SaveAs(savFile, AutoCAD.AcSaveAsType.acR12_dxf)
                Else
                    savFile = Replace(UCase(savFile), ".DWG", ".DXF")
                    AcadDOC.SaveAs(savFile, AutoCAD.AcSaveAsType.acR12_dxf)
                End If

            Else
                AcadAPP.ZoomExtents()
                If chkCT.CheckState = 1 Then
                    savFile = Replace(UCase(savFile), ".DWG", ".DXF")
                    savFile = Replace(UCase(savFile), UCase(AcadPath), UCase(""))
                    AcadDOC.SaveAs(savFile, AutoCAD.AcSaveAsType.acR12_dxf)
                Else
                    savFile = Replace(UCase(savFile), ".DWG", ".DXF")
                    AcadDOC.SaveAs(savFile, AutoCAD.AcSaveAsType.acR12_dxf)
                End If
            End If
        Else
            'Debug.Print("Error File: " & savFile)
        End If
        Threading.Thread.Sleep(1000)
    End Sub

    Public Sub savDWG(ByRef savFile As String)
        If AcadDOC.FullName <> "" Then
            On Error Resume Next
            If AcadDOC.ActiveSpace = AutoCAD.AcActiveSpace.acPaperSpace Then
                AcadAPP.ZoomExtents()
                AcadDOC.PurgeAll()
                WFR()
                AcadDOC.SaveAs(savFile, AutoCAD.AcSaveAsType.acR15_dwg)
            Else
                AcadAPP.ZoomExtents()
                AcadDOC.PurgeAll()
                WFR()
                AcadDOC.SaveAs(savFile, AutoCAD.AcSaveAsType.acR15_dwg)
            End If
        Else
            Debug.Print("Error File: " & savFile)
        End If
        'WFR
        'epsIt
        WFR()
    End Sub

    'Private Sub epsIt()
    '    Dim lNum As AcadLayout
    '    Dim lLL(0 To 2) As Double
    '    Dim lUR(0 To 2) As Double
    '    Dim ind As Long
    '    Dim cmdStr As String
    '    Dim epsFile As String
    '    HoldStr = ""
    '    frmWinSeek.Tag = AcadDoc.GetVariable("dwgprefix")
    '    'turn filedia off
    '    AcadDoc.SetVariable "filedia", 0
    '    On Error GoTo epsErr
    '        Set lNum = AcadDoc.Layouts.item("model")
    '        AcadDoc.ActiveLayout = lNum
    '        AcadDoc.MSpace = False
    '        AcadAPP.ZoomExtents
    '        WFR
    '        If Mid(lNum.Name, 1, 2) = "PD" Then
    '            'standard pd window
    '            epsFile = Form1.Tag & lNum.Name & ".eps"
    '            'delet existing
    '            If FileExists(epsFile) Then DeleteFile epsFile
    '            WFR
    '            cmdStr = "psout" & Chr(13) & epsFile & Chr(13) _
    ''            & "W" & Chr(13) & "0,0" & Chr(13) _
    ''            & "9.25,13.49" & Chr(13) & "EPS" & Chr(13) _
    ''            & "128" & Chr(13) & "I" & Chr(13) & "F" _
    ''            & Chr(13) & "9.25,13.49" & Chr(13)
    '            'run psout
    '            Debug.Print cmdStr
    '            AcadDoc.SendCommand cmdStr
    '            WFR
    '        Else
    '            'Elevation window
    '            epsFile = Form1.Tag & lNum.Name & ".eps"
    '            'delet existing
    '            If FileExists(epsFile) Then DeleteFile epsFile
    '            WFR
    '            cmdStr = "psout" & Chr(13) & epsFile & Chr(13) _
    ''            & "W" & Chr(13) & "0,0" & Chr(13) _
    ''            & "33.75,22.75" & Chr(13) & "EPS" & Chr(13) _
    ''            & "128" & Chr(13) & "I" & Chr(13) & "F" _
    ''            & Chr(13) & "33.75,22.75" & Chr(13)
    '            'run psout
    '            AcadDoc.SendCommand cmdStr
    '            WFR
    '        End If
    '    End If
    '
    'epsErr:
    '    prgbar.Caption = ""
    '    'turn filedia on
    '    AcadDoc.SetVariable "filedia", 1
    'End Sub

    Private Sub WriFile(ByRef fname As String, ByRef ArrVals As Object)
        Dim FileNoW As Short
        Dim cnt As Integer
        If dimVar(ArrVals) Then
            'Debug.Print UBound(ArrVals)
            FileNoW = FreeFile
            FileOpen(FileNoW, fname, OpenMode.Append)
            For cnt = 0 To UBound(ArrVals)
                PrintLine(FileNoW, ArrVals(cnt))
            Next
            FileClose(FileNoW)
        End If
    End Sub
#End Region



    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'PreviewToolStripMenuItem.Enabled = False

        'browser
        tvwSetup()
        PopulateLVW()
        'status

        fw.Directory = "C:\TEMP"
        fw.FileName = "TEMP.EPS"
        fw.WatchIt()

        PopulateSelected()

    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        lvwACAD.Items.Clear()
        prgReset()
        PopulateRecursed()
    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        filArray()
    End Sub


#Region "lvobjects context menu"
    Private Sub tsmiSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiSelectAll.Click
        Dim itm As ListViewItem
        For Each itm In lvwFiles.Items
            If Not itm.Checked Then itm.Checked = True
        Next
        lvwFiles.Invalidate()
    End Sub

    Private Sub tsmiUnselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiUnselectAll.Click
        Dim itm As ListViewItem
        For Each itm In lvwFiles.Items
            If itm.Checked Then itm.Checked = False
        Next
        lvwFiles.Invalidate()
    End Sub

#End Region
End Class
