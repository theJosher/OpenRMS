Module FolderStructure
    Public Enum ItemOrderBy As Integer
        id = 0
        name = 1
        type = 3
        parent = 4
    End Enum
    ' IMPORTANT!: Item type must reflect ImageIndex (for icons)
    Public Enum ItemType As Integer
        Database = 0 ' Not used within Database, only for specifying database
        Folder = 1
        Document = 2
    End Enum
    Public Class ItemRecord
        Public ReadOnly Id As Long
        Public ReadOnly Name As String
        Public ReadOnly Type As ItemType
        Public ReadOnly Parent As String
        Public Sub New(ByVal aId As Long, ByVal aName As String, ByVal aType As ItemType, ByVal aParent As String)
            Id = aId
            Name = aName
            Type = aType
            Parent = aParent
        End Sub
    End Class

    ' If item already exists, returns a negative number
    Public Function createItem(ByRef aSqliteFile As SqLiteFile, ByVal aName As String, ByVal aParent As String, ByVal aType As ItemType, ByRef aError As String) As String
        aError = ""
        If Not aSqliteFile.CanWrite Then
            aError = "Cannot communicate with database."
            Return -99
        End If
        Dim lData As DataTable = aSqliteFile.Query("SELECT * FROM folder_structure where name='" & aName & "' AND parent='" & aParent & "'")
        If lData.Rows.Count <> 0 Then
            aError = "Cannot create file; Duplicate name in folder."
            Return -1
        End If
        lData = aSqliteFile.Query("SELECT max(id) FROM folder_structure")
        Dim lId As Long = 1
        If (lData.Rows.Count <> 0) AndAlso Not lData.Rows(0).IsNull(0) Then
            Dim lRow As DataRow = lData.Rows(0)
            lId = lRow(0)
            lId += 1
        End If
        aSqliteFile.Query("INSERT INTO folder_structure VALUES (" & lId & ", '" & aName & "', '" & aType & "', '" & aParent & "')")
        Return lId
    End Function
    Public Function listItems(ByRef aSqliteFile As SqLiteFile, ByVal aParent As String, ByVal aOrderBy As ItemOrderBy, ByRef aError As String) As ItemRecord()
        aError = ""
        If Not aSqliteFile.CanWrite Then
            aError = "Cannot communicate with database."
            Return Nothing
        End If
        Dim lData As DataTable = aSqliteFile.Query("SELECT * FROM folder_structure where parent='" & aParent & "' ORDER BY " & aOrderBy.ToString)
        Dim lReturnArray(0 To lData.Rows.Count - 1) As ItemRecord
        For lIdx As Integer = 0 To lData.Rows.Count - 1
            Dim lRow As DataRow = lData.Rows(lIdx)
            Dim lItemRecord As New ItemRecord(lRow(0), lRow(1), lRow(2), lRow(3))
            lReturnArray(lIdx) = lItemRecord
        Next
        Return lReturnArray
    End Function

    Public Sub applyFolderStructure(ByRef aSqliteFile As SqLiteFile, ByRef aTreeNode As TreeNodeCollection, ByVal aParent As String, ByRef aOrderby As ItemOrderBy, ByRef aError As String)
        aTreeNode.Clear()
        Dim lItemRecord() As ItemRecord = listItems(aSqliteFile, aParent, aOrderby, aError)
        If (aError <> "") Then Exit Sub
        For Each lItem As ItemRecord In lItemRecord
            Dim lPath As String = aParent
            If Not lPath.EndsWith("/") Then lPath = lPath & "/"
            Dim lAddedNode As TreeNode = aTreeNode.Add(lItem.Name, lItem.Name, lItem.Type, lItem.Type)
            If (lItem.Type = ItemType.Folder) Then
                applyFolderStructure(aSqliteFile, lAddedNode.Nodes, lPath & lItem.Name, aOrderby, aError)
                If (aError <> "") Then Exit Sub
            End If
        Next
    End Sub

    Public Sub applyFolderContents(ByRef aSqliteFile As SqLiteFile, ByRef aListView As ListView, ByVal aParent As String, ByRef aOrderby As ItemOrderBy, ByRef aError As String)
        aListView.Clear()
        Dim lItemRecord() As ItemRecord = listItems(aSqliteFile, aParent, aOrderby, aError)
        If (aError <> "") Then Exit Sub
        For Each lItem As ItemRecord In lItemRecord
            Dim lListViewItem As ListViewItem = aListView.Items.Add(lItem.Name)
            'lListViewItem.SubItems.Add(lItem.Name.ToString)
            lListViewItem.SubItems.Add(lItem.Type.ToString)
            lListViewItem.SubItems.Add(lItem.Parent.ToString)
            lListViewItem.SubItems.Add(lItem.Id.ToString)
            lListViewItem.ImageIndex = lItem.Type
        Next
    End Sub
End Module
