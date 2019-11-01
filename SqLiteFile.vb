Imports System.Data.SQLite

Public Class SqLiteFile
    Public Const DefaultDbFile As String = "Database.s3db"
    Private Connection As SQLiteConnection = Nothing
    Public Function Query(ByVal aQueryString As String) As DataTable
        Try
            If (Connection Is Nothing) Then Open()
            Dim lCommand As New SQLiteCommand(Connection)
            Dim lDataTable As New DataTable
            lCommand.CommandText = aQueryString
            Dim lReader As SQLiteDataReader = lCommand.ExecuteReader()
            lDataTable.Load(lReader)
            Return lDataTable
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)
        End Try
        Return Nothing
    End Function
    Public Sub Open(Optional ByVal aPathToSqliteFile As String = DefaultDbFile)
        If Not (Connection Is Nothing) Then Close()
        Connection = New SQLiteConnection("Data Source=" & aPathToSqliteFile)
        Connection.Open()
        Query("CREATE TABLE IF NOT EXISTS folder_structure (id bigint, name text, type text, parent text)")
        'Query("CREATE TABLE IF NOT EXISTS current_tasks (id bigint, estimated_hours int, cumulative_minutes int, priority int, date_due text, description text, notes text, category text, string_log text)")
        'Query("CREATE TABLE IF NOT EXISTS finished_tasks (id bigint, estimated_hours int, cumulative_minutes int, priority int, date_due text, description text, notes text, category text, string_log text, finished_timestamp text)")
    End Sub
    Public Sub Close()
        Connection.Close()
        Connection = Nothing
    End Sub
    Public Sub New()
        Open()
    End Sub
    Public Sub New(ByVal aPathToSqliteFile As String)
        Open(aPathToSqliteFile)
    End Sub
    Public Function CanWrite() As Boolean
        If Connection Is Nothing Then Return False
        Return True
    End Function
End Class
