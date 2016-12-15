'need a reference to System.Design.DLL (Project -> Addreference)

Imports System.Windows.Forms.Design
Imports System.ComponentModel.Component


Public Class DirBrowse
    Inherits FolderNameEditor

    Dim fb As New FolderBrowser()
    Dim rPath As String
    Dim dPath As String ' default path
    Dim rDesc As String
    Dim dr As DialogResult

    Public Property [Description]() As String
        Get
            Description = rDesc
        End Get
        Set(ByVal Value As String)
            rDesc = Description
        End Set
    End Property

    Public ReadOnly Property ShowDialog() As DialogResult
        Get

            fb.Description = rDesc
            fb.StartLocation = FolderBrowserFolder.MyComputer
            dr = fb.ShowDialog

            If dr = DialogResult.OK Then
                rPath = fb.DirectoryPath
            Else
                rPath = Nothing
            End If
            ShowDialog = dr
        End Get
    End Property

    Public Property [Path]() As String
        Get
            [Path] = rPath
            If Not (rPath Is Nothing) Then
                dPath = rPath
            End If
        End Get
        Set(ByVal Value As String)
            dPath = Value
        End Set
    End Property
End Class
