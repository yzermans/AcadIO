Option Strict Off
Option Explicit On
Module modPurge
	
	Public Sub PurgeLineTypes()
		Dim objLTs As AutoCAD.AcadLineTypes
		Dim objLT As AutoCAD.AcadLineType
		On Error GoTo Err_Control
		objLTs = AcadDOC.Linetypes
		For	Each objLT In objLTs
			objLT.Delete()
		Next objLT
Exit_Here: 
		Exit Sub
Err_Control: 
		If Err.Number = -2145320931 Then
			Resume Next
		Else
			'MsgBox Err.Description
			Resume Exit_Here
		End If
	End Sub
	
	Public Sub PurgeLayers()
		Dim objLyrs As AutoCAD.AcadLayers
		Dim objLyr As AutoCAD.AcadLayer
		On Error GoTo Err_Control
		objLyrs = AcadDOC.Layers
		For	Each objLyr In objLyrs
			objLyr.Delete()
		Next objLyr
Exit_Here: 
		Exit Sub
Err_Control: 
		If Err.Number = -2145320931 Then
			Resume Next
		Else
			'MsgBox Err.Description
			Resume Exit_Here
		End If
	End Sub
	
	Public Sub PurgeTextStyles()
		Dim objStyles As AutoCAD.AcadTextStyles
		Dim objStyle As AutoCAD.AcadTextStyle
		On Error GoTo Err_Control
		objStyles = AcadDOC.TextStyles
		For	Each objStyle In objStyles
			objStyle.Delete()
		Next objStyle
Exit_Here: 
		Exit Sub
Err_Control: 
		If Err.Number = -2145320931 Then
			Resume Next
		Else
			'MsgBox Err.Description
			Resume Exit_Here
		End If
	End Sub
	
	Public Sub PurgeDimStyles()
		Dim objStyles As AutoCAD.AcadDimStyles
		Dim objStyle As AutoCAD.AcadDimStyle
		On Error GoTo Err_Control
		objStyles = AcadDOC.DimStyles
		For	Each objStyle In objStyles
			objStyle.Delete()
		Next objStyle
Exit_Here: 
		Exit Sub
Err_Control: 
		If Err.Number = -2145320931 Then
			Resume Next
		Else
			'MsgBox Err.Description
			Resume Exit_Here
		End If
	End Sub
	Public Sub PurgeBlockDefs()
		Dim objBDefs As AutoCAD.AcadBlocks
		Dim objBDef As AutoCAD.AcadBlock
		On Error GoTo Err_Control
		objBDefs = AcadDOC.Blocks
		For	Each objBDef In objBDefs
			'exclude unamed blocks
			If InStr(1, objBDef.Name, "*") = 0 Then
				objBDefs.Item(objBDef.Name).Delete()
			End If
		Next objBDef
Exit_Here: 
		Exit Sub
Err_Control: 
		Select Case Err.Number
			Case -2145320931, -2145386395, -2145386239
				Resume Next
			Case Else
				Resume Next
		End Select
	End Sub
	
	
	'Purge Zero Length Lines
	Public Sub PurgeNullLines()
		Dim objSelSet As AutoCAD.AcadSelectionSet
		Dim objSelSets As AutoCAD.AcadSelectionSets
		Dim objLine As AutoCAD.AcadLine
		Dim intType(0) As Short
		Dim varData(0) As Object
		Dim varStart As Object
		Dim varEnd As Object
		objSelSets = AcadDOC.SelectionSets
		For	Each objSelSet In objSelSets
			If objSelSet.Name = "PurgeNullLines" Then
				objSelSet.Delete()
				Exit For
			End If
		Next objSelSet
		objSelSet = objSelSets.Add("PurgeNullLines")
		intType(0) = 0
		'UPGRADE_WARNING: Couldn't resolve default property of object varData(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		varData(0) = "LINE"
		objSelSet.Select(AutoCAD.AcSelect.acSelectionSetAll, filtertype:=intType, filterdata:=varData)
		For	Each objLine In objSelSet
			'UPGRADE_WARNING: Couldn't resolve default property of object objLine.StartPoint. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object varStart. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			varStart = objLine.StartPoint
			'UPGRADE_WARNING: Couldn't resolve default property of object objLine.EndPoint. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object varEnd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			varEnd = objLine.EndPoint
			'UPGRADE_WARNING: Couldn't resolve default property of object varEnd(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object varStart(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			If varStart(0) = varEnd(0) Then
				'UPGRADE_WARNING: Couldn't resolve default property of object varEnd(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object varStart(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If varStart(1) = varEnd(1) Then
					'UPGRADE_WARNING: Couldn't resolve default property of object varEnd(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					'UPGRADE_WARNING: Couldn't resolve default property of object varStart(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					If varStart(2) = varEnd(2) Then
						objLine.Delete()
					End If
				End If
			End If
		Next objLine
	End Sub
	
	Public Function ssGet(ByRef layName As String, ByRef eName As String) As AutoCAD.AcadSelectionSet
		Dim grpFilter, valFilter As Object
		Dim vpCnt As AutoCAD.AcadEntity
		Dim xdataType, xdataValue As Object
		Dim grpX(3) As Short
		Dim datX(3) As Object
		'set filter parameters
		grpX(0) = -4
		'UPGRADE_WARNING: Couldn't resolve default property of object datX(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		datX(0) = "<AND"
		grpX(1) = 8
		'UPGRADE_WARNING: Couldn't resolve default property of object datX(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		datX(1) = layName
		grpX(2) = 0
		'UPGRADE_WARNING: Couldn't resolve default property of object datX(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		datX(2) = UCase(eName)
		grpX(3) = -4
		'UPGRADE_WARNING: Couldn't resolve default property of object datX(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		datX(3) = "AND>"
		' Create a new selection set
		On Error Resume Next
		ssGet = AcadDOC.SelectionSets.Add("xSS")
		If Err.Number Then ssGet = AcadDOC.SelectionSets.item("xSS")
		Err.Clear()
		ssGet.Clear()
		'assign to variants
		'UPGRADE_WARNING: Couldn't resolve default property of object grpFilter. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        grpFilter = (grpX)
		'UPGRADE_WARNING: Couldn't resolve default property of object valFilter. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        valFilter = (datX)
		ssGet.Select(AutoCAD.AcSelect.acSelectionSetAll,  ,  , grpFilter, valFilter)
		'Debug.Print ssGet.Count
	End Function
End Module