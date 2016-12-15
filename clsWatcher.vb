Imports System
Imports System.IO
Imports System.Diagnostics

Public Class clsWatcher
    Private _FileName As String
    Private _Directory As String
    Private _Status As Long
    Public watcher As New FileSystemWatcher

    Public Property FileName() As String
        Get
            FileName = _FileName
        End Get
        Set(ByVal Value As String)
            _FileName = Value
        End Set
    End Property

    Public Property Directory() As String
        Get
            Directory = _Directory
        End Get
        Set(ByVal Value As String)
            _Directory = Value
        End Set
    End Property

    Public ReadOnly Property Status() As Long
        Get
            Status = _Status
        End Get
    End Property

    Public Sub WatchIt()
        ' Create a new FileSystemWatcher and set its properties.
        watcher.Path = Directory
        ' Watch for changes in LastAccess and LastWrite times, and
        ' the renaming of files or directories. 
        watcher.NotifyFilter = (NotifyFilters.LastAccess Or NotifyFilters.LastWrite Or NotifyFilters.FileName Or NotifyFilters.DirectoryName)
        ' Only watch text files.
        watcher.Filter = FileName

        ' Add event handlers.
        AddHandler watcher.Changed, AddressOf OnChanged
        AddHandler watcher.Created, AddressOf OnChanged
        AddHandler watcher.Deleted, AddressOf OnChanged
        AddHandler watcher.Renamed, AddressOf OnRenamed

        ' Begin watching.
        watcher.EnableRaisingEvents = True
    End Sub

    ' Define the event handlers.
    Private Sub OnChanged(ByVal source As Object, ByVal e As FileSystemEventArgs)
        ' Specify what is done when a file is changed, created, or deleted.
        'Select Case e.ChangeType
        'Case WatcherChangeTypes.Created
        '_Status = "CREATED"
        'Case WatcherChangeTypes.Renamed
        '_Status = "RENAMED"
        'Case WatcherChangeTypes.Deleted
        '_Status = "DELETED"
        'Case WatcherChangeTypes.All
        '_Status = "ALL"
        'Case Else
        '_Status = "CHANGED"
        'End Select
        _Status = e.ChangeType

        Debug.WriteLine(_Status)
    End Sub

    Private Sub OnRenamed(ByVal source As Object, ByVal e As RenamedEventArgs)
        ' Specify what is done when a file is renamed.
        _Status = e.ChangeType
        'Debug.WriteLine(_Status)
    End Sub
End Class
