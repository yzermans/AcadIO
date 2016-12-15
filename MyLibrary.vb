Option Strict Off
Option Explicit On
Module MyLibrary
	Public AcadAPP As AutoCAD.AcadApplication
	Public AcadDOC As AutoCAD.AcadDocument
	Public AcadDwg As String
	Public AcadPath As String
	Public moSpace As Object
	Public paSpace As Object
	Public acadRun As Boolean
	
	Public Function isFile(ByRef filename As String) As Boolean
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Len(Dir(filename)) > 0 Then
			'filename exists
			isFile = True
		Else
			'Filename doed not exist
			isFile = False
		End If
	End Function
	Public Sub ShowHourglass()
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
	End Sub
	
	Public Sub ShowMousePointer()
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	'Input validation
	
	Public Function ValiText(ByRef KeyIn As Short, ByRef ValidateString As String, ByRef Editable As Boolean) As Short
		
		Dim ValidateList As String
		Dim KeyOut As Short
		'
		If Editable = True Then
			ValidateList = UCase(ValidateString) & Chr(8)
		Else
			ValidateList = UCase(ValidateString)
		End If
		'
		If InStr(1, ValidateList, UCase(Chr(KeyIn)), 1) > 0 Then
			KeyOut = KeyIn
		Else
			KeyOut = 0
			Beep()
		End If
		'
		ValiText = KeyOut
		'
	End Function
	'Then, for each control whose input you wish to validate, just put something
	'like this in the KeyPress event of the control:
	' KeyAscii = ValiText(KeyAscii, "0123456789/-", True)
	
	Public Sub file_prn(ByRef filVal As String, ByRef prnVal As String)
		Dim FileNo As Short
		FileNo = FreeFile
		FileOpen(FileNo, filVal, OpenMode.Append)
		PrintLine(FileNo, prnVal)
		FileClose(FileNo)
	End Sub
	
	Public Function ssx(ByRef layName As String, ByRef blkName As String) As AutoCAD.AcadSelectionSet
		Dim grpX(3) As Short
		Dim datX(3) As Object
		Dim grpFilter, valFilter As Object
		Dim vpCnt As AutoCAD.AcadEntity
		Dim xdataType, xdataValue As Object
		' Create a new selection set
		On Error Resume Next
		ssx = AcadDOC.SelectionSets.Add("xSS")
		If Err.Number Then ssx = AcadDOC.SelectionSets.item("xSS")
		Err.Clear()
		ssx.Clear()
		'set filter parameters for selection for vpSS
		grpX(0) = -4
		'UPGRADE_WARNING: Couldn't resolve default property of object datX(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		datX(0) = "<OR"
		grpX(1) = 8
		'UPGRADE_WARNING: Couldn't resolve default property of object datX(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		datX(1) = layName
		grpX(2) = 2
		'UPGRADE_WARNING: Couldn't resolve default property of object datX(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		datX(2) = blkName
		grpX(3) = -4
		'UPGRADE_WARNING: Couldn't resolve default property of object datX(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		datX(3) = "OR>"
		'assign to variants
		'UPGRADE_WARNING: Couldn't resolve default property of object grpFilter. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        grpFilter = (grpX)
		'UPGRADE_WARNING: Couldn't resolve default property of object valFilter. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        valFilter = (datX)
		ssx.Select(AutoCAD.AcSelect.acSelectionSetAll,  ,  , grpFilter, valFilter)
	End Function
	Function IsQuiet() As Boolean
		' Get the acadState object and check to see if
		' AutoCAD is in a quiescent state.
		Dim state As AutoCAD.AcadState
		state = AcadAPP.GetAcadState
		If state.IsQuiescent Then
			'AutoCAD Is Idle
			IsQuiet = True
		Else
			'AutoCAD Is Busy
			IsQuiet = False
		End If
	End Function
	
	Public Sub WFR()
		Do 
			System.Windows.Forms.Application.DoEvents()
		Loop Until IsQuiet
	End Sub
	Public Sub LoadAutoCAD()
		On Error Resume Next
		AcadAPP = GetObject( , "AutoCAD.Application")
		If Err.Number Then
			MsgBox("Autocad Is Not Running")
			acadRun = False
			Err.Clear()
			GoTo acadErr
			'Set ACADApp = CreateObject("AutoCAD.Application")
			Err.Clear()
		End If
		
		On Error Resume Next
		
		AcadDOC = AcadAPP.ActiveDocument
		If Err.Number Then
			MsgBox("AutoCAD Error")
			GoTo acadErr
		Else
			acadRun = True
			AcadAPP.Visible = True
			'UPGRADE_WARNING: Couldn't resolve default property of object AcadDOC.GetVariable(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'frmWinSeek.Text = "Update - [" & AcadDOC.GetVariable("dwgname") & "]"
			'AcadDOC.SetVariable "TILEMODE", 1
		End If
		Exit Sub
acadErr: 
		acadRun = False
	End Sub
End Module