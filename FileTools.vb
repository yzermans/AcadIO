Imports System
Imports System.IO
Imports System.Diagnostics
Imports Microsoft.Win32
Imports System.Security
Imports System.Collections


Public Class FileTools

#Region "Properties"

    Public Cnt As Int32
    Private m_FilesList As Object
    Private m_FileExt As String

    Public Overridable Property FileExt() As String
        Get
            Return m_FileExt
        End Get
        Set(ByVal Value As String)
            m_FileExt = Value
        End Set
    End Property

    Public Overridable Property FilesList() As Object
        Get
            Return m_FilesList
        End Get
        Set(ByVal Value As Object)
            m_FilesList = Value
        End Set
    End Property

#End Region

#Region "Recurse Files"

    Public Sub FindFolders(ByVal strPath As String)
        Dim path As String
        ' reset values
        Cnt = 0
        FilesList = Nothing
        On Error Resume Next
        'Dim siteResponds As Boolean = False
        'siteResponds = My.Computer.Network.Ping("strPath")
        'If siteResponds Then
        For Each path In Directory.GetDirectories(strPath)
            If File.Exists(path) Then
                ' This path is a file
                ProcessFile(path)
            Else
                If Directory.Exists(path) Then
                    ' This path is a directory
                    ProcessDirectory(path)
                Else
                    'Debug.WriteLine("{0} is not a valid file or directory.", path)
                End If
            End If
        Next path
        'End If
    End Sub

    ' Process all files in the directory passed in, and recurse on any directories 
    ' that are found to process the files they contain
    Private Sub ProcessDirectory(ByVal targetDirectory As String)
        Dim fileEntries As String() = Directory.GetFiles(targetDirectory)
        ' Process the list of files found in the directory
        Dim fileName As String
        For Each fileName In fileEntries
            ProcessFile(fileName)
        Next fileName
        Dim subdirectoryEntries As String() = Directory.GetDirectories(targetDirectory)
        ' Recurse into subdirectories of this directory
        Dim subdirectory As String
        For Each subdirectory In subdirectoryEntries
            ProcessDirectory(subdirectory)
        Next subdirectory

    End Sub 'ProcessDirectory

    ' Real logic for processing found files would go here.
    Private Sub ProcessFile(ByVal path As String)
        If FileExt <> "" Then
            If InStr(UCase(path), UCase(FileExt)) > 0 Then
                ReDim Preserve FilesList(Cnt)
                FilesList(Cnt) = path
                Cnt = Cnt + 1
            End If
        End If

        If FileExt = "*" Then
            ReDim Preserve FilesList(Cnt)
            FilesList(Cnt) = path
            Cnt = Cnt + 1
        End If
        ''Debug.WriteLine(path)
    End Sub 'ProcessFile

#End Region

    Public Sub KillProcess(ByVal pName As String)
        Dim running() As Process = Process.GetProcesses()
        If running.Length > 0 Then
            Dim Cnt As Integer
            For Cnt = 0 To running.Length - 1
                If UCase(pName) = UCase(running(Cnt).ProcessName) Then
                    running(Cnt).Kill()
                    running(Cnt).Dispose()
                    Exit For
                End If
            Next Cnt
        End If
    End Sub

    Public Sub RunExe(ByVal ExeName As String)
        System.Diagnostics.Process.Start(ExeName)
    End Sub

    Public Function CreateFolder(ByVal strPath As String) As Boolean
        Try
            Directory.CreateDirectory(strPath)
            Return True
        Catch
            Return False
        End Try
    End Function

    Private Sub KillToolbar()
        ' Get all instances of Toolbar running on the specifiec
        ' computer.
        ' 1. Using the computer alias (do not precede with "\\").
        Dim remoteByName As Process() = Process.GetProcessesByName("ToolBar", "myComputer")
        'remoteByName.
    End Sub

    'is object dimensioned
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

    Public Function DeleteFolder(ByVal strPath As String) As Boolean
        Try
            Directory.Delete(True)
            Return True
        Catch
            Return False
        End Try
    End Function

    Public Function FileExists(ByVal strPath As String) As Boolean
        Try
            If Directory.Exists(strPath) Then
                Return True
            Else
                Return False
            End If
        Catch
            Return False
        End Try
    End Function

    Public Function FileDelete(ByVal strPath As String) As Boolean
        Try
            Directory.Delete(strPath)
            Return True
        Catch
            Return False
        End Try
    End Function

    Public Function FileFind(ByVal strPath As String, ByVal ext As String, _
    Optional ByVal none As Boolean = False) As Array
        Dim objFileList() As String = Directory.GetFiles(strPath)

        Dim i As Int32
        ext = "." & UCase(ext)
        ext = Replace(ext, "..", ".")
        Try
            objFileList = Directory.GetFiles(strPath)

            If dimVar(objFileList) Then
                For i = 0 To UBound(objFileList)
                    objFileList(i) = UCase(objFileList(i))
                Next
                objFileList = Filter(objFileList, ext, True)
                If objFileList Is Nothing Then
                    If none Then
                        ReDim Preserve objFileList(0)
                        objFileList(0) = "NONE"
                    Else
                        objFileList = Nothing
                    End If
                End If

                If none Then
                    ReDim Preserve objFileList(UBound(objFileList) + 1)
                    objFileList(UBound(objFileList)) = "NONE"
                End If
            Else
                If none Then
                    ReDim Preserve objFileList(0)
                    objFileList(0) = "NONE"
                Else
                    objFileList = Nothing
                End If
            End If
            For i = 0 To UBound(objFileList)
                objFileList(i) = UCase(objFileList(i))
            Next
            Return objFileList
        Catch
            Return Nothing
        End Try
    End Function

    Public Function FileFindName(ByVal strPath As String, ByVal nam As String, _
  Optional ByVal none As Boolean = False) As Array
        Dim objFileList() As String = Nothing
        Try
            objFileList = Directory.GetFiles(strPath)
            If Not objFileList Is Nothing Then
                objFileList = Filter(objFileList, nam, True)
                If objFileList Is Nothing Then
                    If none Then
                        ReDim Preserve objFileList(0)
                        objFileList(0) = "NONE"
                    Else
                        objFileList = Nothing
                    End If
                End If
                ReDim Preserve objFileList(UBound(objFileList) + 1)
                objFileList(UBound(objFileList)) = "NONE"
            Else
                If none Then
                    ReDim Preserve objFileList(0)
                    objFileList(0) = "NONE"
                Else
                    objFileList = Nothing
                End If
            End If
            Return objFileList
        Catch
            Return Nothing
        End Try
    End Function

    Public Function FindDwg(ByVal strPath As String, ByVal ext As String, _
 Optional ByVal none As Boolean = False) As Array
        Dim objFileList() As String = Nothing
        ext = "." & ext
        Try
            objFileList = Directory.GetFiles(strPath)
            If Not objFileList Is Nothing Then
                If objFileList Is Nothing Then
                    If none Then
                        ReDim Preserve objFileList(0)
                        objFileList(0) = "NONE"
                    Else
                        objFileList = Nothing
                    End If
                End If
                ReDim Preserve objFileList(UBound(objFileList) + 1)
                objFileList(UBound(objFileList)) = "NONE"
            Else
                If none Then
                    ReDim Preserve objFileList(0)
                    objFileList(0) = "NONE"
                Else
                    objFileList = Nothing
                End If
            End If
            Return objFileList
        Catch
            Return Nothing
        End Try
    End Function

    Public Function FileNameWoExt(ByVal FullPath _
        As String) As String
        ' Example: FileNameWithoutExtension("C:\winnt\system32\kernel.dll") 
        'returns("kernel")
        Return System.IO.Path.GetFileNameWithoutExtension(FullPath)
    End Function

    Public Function FolderFromFileName _
  (ByVal FileFullPath As String) As String
        'EXAMPLE: input ="C:\winnt\system32\kernel.dll, 
        'output = C:\winnt\system32\
        Dim intPos As Integer
        intPos = FileFullPath.LastIndexOfAny("\")
        intPos += 1
        Return FileFullPath.Substring(0, intPos)
    End Function

    Public Function NameOnlyFromFullPath _
      (ByVal FileFullPath As String) As String
        'EXAMPLE: input ="C:\winnt\system32\kernel.dll, 
        'output = kernel.dll
        Dim intPos As Integer
        Try
            intPos = FileFullPath.LastIndexOfAny("\")
            intPos += 1
            Return FileFullPath.Substring(intPos, _
                (Len(FileFullPath) - intPos))
        Catch ex As Exception
            Return ""
        End Try
    End Function


End Class
