Imports System.IO

Public Class clsFolder

    Public Function CreateFolder(ByVal strPath As String) As Boolean
        Dim objDir As New DirectoryInfo(strPath)

        Try
            objDir.Create()
            Return True
        Catch
            Return False
        End Try
    End Function

    Public Function DeleteFolder(ByVal strPath As String) As Boolean
        Dim objDir As New DirectoryInfo(strPath)

        Try
            objDir.Delete(True)
            Return True
        Catch
            Return False
        End Try
    End Function

    'Reading and writing text files in VB.NET:

    Public Function GetFileContents(ByVal FullPath As String, _
           Optional ByRef ErrInfo As String = "") As String
        Dim strContents As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
        Catch Ex As Exception
            ErrInfo = Ex.Message
            Return Nothing
        End Try
    End Function

    Public Function SaveTextToFile(ByVal strData As String, _
     ByVal FullPath As String, _
       Optional ByVal ErrInfo As String = "") As Boolean
        Dim bAns As Boolean = False
        Dim objReader As StreamWriter
        Try
            objReader = New StreamWriter(FullPath)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
        Return bAns
    End Function

End Class
