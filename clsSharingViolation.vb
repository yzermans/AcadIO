Imports System
Imports System.IO
Imports System.Reflection
Public Class clsSharingViolation

    Private _FileName As String

    Public Property FileName() As String
        Get
            FileName = _FileName
        End Get
        Set(ByVal Value As String)
            _FileName = Value
        End Set
    End Property

    Public Function Run() As Int32
        Dim _inStream As FileStream = Nothing
        Try
            _inStream = File.OpenRead(FileName)
        Catch _ioe As IOException
            Return 0
        End Try
        If Not _inStream Is Nothing Then
            _inStream.Close()
            Return 1
        End If
    End Function

End Class
