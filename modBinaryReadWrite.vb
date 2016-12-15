Option Strict Off
Option Explicit On
Imports System.IO
Module modBinaryReadWrite
	Public Arr() As String
	Public acnt As Integer
	Public HoldStr As String
	Public PDF As String
	
	' File to string
	'
	' This function reads a text or a binary
	' file and returns its contents as
	' a string. It is a practical way for in
	' putting files because this
	' function reads the file in a single Get,
	' and it avoids locking the
	' file for long periods of time. However,
	' your code is responsible for
	' parsing the file into lines or other text units.
	' Newline delimiters
	' are left in text files by this function.
	
	Public Function file2String(ByVal strFileid As String) As String
		Dim bytFileNumber As Byte
		Dim strInputArea As String
		On Error GoTo file2String_Lbl1_freeFileError
		bytFileNumber = FreeFile
		On Error GoTo file2String_Lbl2_openError
		FileOpen(bytFileNumber, strFileid, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
		On Error GoTo file2String_Lbl3_storageError
		strInputArea = New String(" ", FileLen(strFileid))
		On Error GoTo file2String_Lbl4_getError
		'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(bytFileNumber, strInputArea)
		On Error GoTo 0
		file2String = strInputArea
		On Error GoTo file2String_Lbl5_closeError
		FileClose(bytFileNumber)
		
		If file2String = "" Then
			MsgBox("No data found in the specified file: " & strFileid)
		End If
		
		On Error GoTo 0
file2String_Lbl9_exit: 
		Exit Function
file2String_Lbl1_freeFileError: 
		MsgBox("No free file numbers are available")
		GoTo file2String_Lbl9_exit
file2String_Lbl2_openError: 
		MsgBox("Can't open file " & UCase(strFileid))
		GoTo file2String_Lbl9_exit
file2String_Lbl3_storageError: 
		MsgBox("Insufficient storage")
		GoTo file2String_Lbl9_exit
file2String_Lbl4_getError: 
		MsgBox("Error In Get: " & Err.Number & " " & Err.Description)
		GoTo file2String_Lbl9_exit
file2String_Lbl5_closeError: 
		MsgBox("Error In Close: " & Err.Number & " " & Err.Description)
		GoTo file2String_Lbl9_exit
	End Function
	
	' String to file: return True (file written ok)
	' or False (file not written ok)
	'
	'
	' This function writes strOutstring to a
	' file. It is a practical way
	' for inputting files because this function
	' writes the file in a single
	' Put, and it avoids locking the file for
	' long periods of time.
	' However, your code is responsible for
	' separating the lines of text
	' files with vbCrLfs or doing other item
	' separation.
	'
	' varFile may be a string or a byte:
	'
	'
	' * If varFile is a string then the
	' identified file is opened,
	' replaced by strOutstring, and closed
	'
	' * If varFile is a byte then strOutstring
	' is appended to the end
	' of the open file identified by varFile.
	' The file is neither
	' opened nor closed. Note that the out
	' string is appended to
	' the last line of the file when it is
	' not prefixed by a carriage
	' return and a line feed.
	'
	'
	' If the optional reference parameter
	' varFileNumber is present it is
	' set to the file number used to open the
	' file (when varFile is a string)
	' or it is set to varFile
	' (when varFile is a byte.)
	'
	' If the optional parameter booClose is
	' present and True, or absent,
	' then the file is CLOSED after output:
	' if booClose is present and
	' False, then the file remains open.
	' Note that if you use varFileNumber
	' to obtain the file number you normally
	' should pass booClose:=False so
	' that the file number as returned is
	' useful the next time you call
	' this routine.
	'
	' If the optional parameter booAppend is
	' present and True then the string is
	' appended to the end of the existing
	' contents of the file: otherwise the
	' string replaces the old contents of
	' the file.
	
	Public Function string2File(ByVal strOutstring As String, ByVal varFile As Object, Optional ByRef varFileNumber As Object = Nothing, Optional ByVal booClose As Boolean = True) As Boolean
		Dim booOpenClose As Boolean
		Dim bytFileNumber As Byte
		string2File = False
		'UPGRADE_WARNING: VarType has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		Select Case VarType(varFile)
			Case VariantType.String : booOpenClose = True
			Case VariantType.Byte : booOpenClose = False
			Case Else
				MsgBox("varFile parameter has an unexpected varType")
				GoTo string2File_Lbl9_exit
		End Select
		
		
		If booOpenClose Then
			On Error GoTo string2File_Lbl1_freeFileError
			bytFileNumber = FreeFile
			On Error GoTo 0
			
			
			'UPGRADE_WARNING: Couldn't resolve default property of object varFile. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            If File.Exists(CStr(varFile)) Then
                On Error GoTo string2File_Lbl6_killFileError
                'UPGRADE_WARNING: Couldn't resolve default property of object varFile. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                File.Delete(varFile)
                On Error GoTo 0
            End If
			On Error GoTo string2File_Lbl2_openError
			'UPGRADE_WARNING: Couldn't resolve default property of object varFile. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			FileOpen(bytFileNumber, varFile, OpenMode.Binary, OpenAccess.Write, OpenShare.LockWrite)
			On Error GoTo 0
		Else
			'UPGRADE_WARNING: Couldn't resolve default property of object varFile. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			bytFileNumber = varFile
		End If
		On Error GoTo string2File_Lbl4_putError
		'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FilePut(bytFileNumber, strOutstring)
		On Error GoTo 0
		
		
		If booOpenClose And booClose Then
			On Error GoTo string2File_Lbl5_closeError
			FileClose(bytFileNumber)
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
		'UPGRADE_WARNING: Couldn't resolve default property of object varFileNumber. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If Not IsNothing(varFileNumber) Then varFileNumber = bytFileNumber
		string2File = True
string2File_Lbl9_exit: 
		Exit Function
string2File_Lbl1_freeFileError: 
		MsgBox("No free file numbers are available")
		GoTo string2File_Lbl9_exit
string2File_Lbl2_openError: 
		MsgBox("Can't open file " & UCase(strOutstring))
		GoTo string2File_Lbl9_exit
string2File_Lbl4_putError: 
		MsgBox("Error In Put: " & Err.Number & " " & Err.Description)
		GoTo string2File_Lbl9_exit
string2File_Lbl5_closeError: 
		MsgBox("Error In Close: " & Err.Number & " " & Err.Description)
		GoTo string2File_Lbl9_exit
string2File_Lbl6_killFileError: 
		MsgBox("Error In Kill: " & Err.Number & " " & Err.Description)
		GoTo string2File_Lbl9_exit
	End Function
End Module