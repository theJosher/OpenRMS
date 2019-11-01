Imports System.Windows.Forms

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports OpenRMS



'''<summary>
'''This is a test class for FolderStructureTest and is intended
'''to contain all FolderStructureTest Unit Tests
'''</summary>
<TestClass()> _
Public Class FolderStructureTest


    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    '''<summary>
    '''A test for createItem
    '''</summary>
    <TestMethod()> _
    Public Sub createItemTest()
        ' Initialize
        Dim lTestFile1 As String = "FolderStructureTest_Db1.s3db"
        If System.IO.File.Exists(lTestFile1) Then System.IO.File.Delete(lTestFile1)
        ' Test
        Dim aSqliteFile As New SqLiteFile(lTestFile1)
        Dim aSqliteFileSecondPointer As New SqLiteFile(lTestFile1)
        Dim aName As String = "FolderName"
        Dim aParent As String = "/"
        Dim aType As ItemType = ItemType.Folder
        Dim aError As String = ""
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long

        actual = FolderStructure.createItem(aSqliteFile, aName, aParent, aType, aError)
        If (Not aSqliteFile.CanWrite) Then
            Assert.Fail("SqLite file was closed in call to CreateItem")
        End If
        If actual <> 1 Then
            Assert.Fail("First Item identifier not set to 1")
        End If
        If aError <> "" Then
            Assert.Fail("Error set when it should not be.")
        End If

        actual = FolderStructure.createItem(aSqliteFile, aName, aParent, aType, aError)
        If actual >= 0 Then
            Assert.Fail("Return value >= 0; should be negative for duplicate item path")
        End If
        If aError = "" Then
            Assert.Fail("Error not set; should be set for duplicate item path.")
        End If

        ' create a document below that folder using the second database pointer
        actual = FolderStructure.createItem(aSqliteFileSecondPointer, "DocumentName", aParent & aName, ItemType.Document, aError)
        If (Not aSqliteFileSecondPointer.CanWrite) Then
            Assert.Fail("SqLite file was closed in call to CreateItem")
        End If
        If actual <> 2 Then
            Assert.Fail("Second Item identifier not set to 2")
        End If
        If aError <> "" Then
            Assert.Fail("Error set when it should not be.")
        End If
        aSqliteFileSecondPointer.Close()
        aSqliteFile.Close()

        ' attempt creation of document when database is closed
        actual = FolderStructure.createItem(aSqliteFileSecondPointer, "DocumentName", aParent & aName, ItemType.Document, aError)
        Assert.AreEqual(actual, Convert.ToInt64(-99))

        ' Cleanup
        If System.IO.File.Exists(lTestFile1) Then System.IO.File.Delete(lTestFile1)
    End Sub

    '''<summary>
    '''A test for listItems
    '''</summary>
    <TestMethod()> _
    Public Sub listItemsTest()
        ' Initialize
        Dim lTestFile2 As String = "FolderStructureTest_Db2.s3db"
        If System.IO.File.Exists(lTestFile2) Then System.IO.File.Delete(lTestFile2)
        ' Test
        Dim aSqliteFile As New SqLiteFile(lTestFile2)
        Dim lError As String = ""
        For lIdx As Integer = 1 To 100
            createItem(aSqliteFile, "Folder " & lIdx.ToString("000"), "/", ItemType.Folder, lError)
            createItem(aSqliteFile, "Document " & lIdx.ToString("000"), "/", ItemType.Document, lError)
        Next
        ' test ordering by id
        Dim actual() As ItemRecord = FolderStructure.listItems(aSqliteFile, "/", ItemOrderBy.id, lError)
        Assert.AreEqual(lError, "")
        Assert.AreEqual(200, actual.Count)
        For lIdx As Int64 = 0 To 199
            Assert.AreEqual(lIdx + 1, actual(lIdx).Id)
            If lIdx Mod 2 = 0 Then
                Assert.AreEqual("Folder " & ((lIdx + 2) / 2).ToString("000"), actual(lIdx).Name)
                Assert.AreEqual(ItemType.Folder, actual(lIdx).Type)
                Assert.AreEqual("/", actual(lIdx).Parent)
            Else
                Assert.AreEqual("Document " & ((lIdx + 1) / 2).ToString("000"), actual(lIdx).Name)
                Assert.AreEqual(ItemType.Document, actual(lIdx).Type)
                Assert.AreEqual("/", actual(lIdx).Parent)
            End If
        Next
        ' test ordering by name
        actual = FolderStructure.listItems(aSqliteFile, "/", ItemOrderBy.name, lError)
        For lIdx As Int64 = 0 To 99
            Assert.AreEqual("Document " & (lIdx + 1).ToString("000"), actual(lIdx).Name)
            Assert.AreEqual(ItemType.Document, actual(lIdx).Type)
            Assert.AreEqual("/", actual(lIdx).Parent)
        Next
        For lIdx As Int64 = 100 To 199
            Assert.AreEqual("Folder " & (lIdx - 99).ToString("000"), actual(lIdx).Name)
            Assert.AreEqual(ItemType.Folder, actual(lIdx).Type)
            Assert.AreEqual("/", actual(lIdx).Parent)
        Next
        aSqliteFile.Close()
        actual = FolderStructure.listItems(aSqliteFile, "/", ItemOrderBy.id, lError)
        Assert.AreEqual(lError, "Cannot communicate with database.")
        Assert.AreEqual(actual, Nothing)
        ' cleanup
        If System.IO.File.Exists(lTestFile2) Then System.IO.File.Delete(lTestFile2)
    End Sub

    Private Sub inOrderPrint(ByRef aTreeNodeCollection As TreeNodeCollection, ByRef aString As String)
        For Each lNode As TreeNode In aTreeNodeCollection
            aString = aString & vbNewLine & lNode.Name & "," & lNode.ImageIndex & "," & lNode.SelectedImageIndex
            If lNode.Nodes.Count > 0 Then
                inOrderPrint(lNode.Nodes, aString)
            End If
        Next
    End Sub
    '''<summary>
    '''A test for applyFolderStructure
    '''</summary>
    <TestMethod(), _
     DeploymentItem("OpenRMS.exe")> _
    Public Sub applyFolderStructureContentsTest()
        Const lLoops As Integer = 5
        ' Initialize
        Dim lTestFile3 As String = "FolderStructureTest_Db3.s3db"
        If System.IO.File.Exists(lTestFile3) Then System.IO.File.Delete(lTestFile3)
        Dim aSqliteFile As New SqLiteFile(lTestFile3)
        ' create a folder structure
        Dim aTreeView As New TreeView
        Dim aTreeViewExpected As New TreeView
        Dim aParent As String = "/"
        Dim aError As String = ""
        Dim lFoldersWithContents As New System.Collections.ArrayList
        Dim lFoldersWithoutContents As New System.Collections.ArrayList
        For lIdx As Integer = 0 To lLoops
            ' Folder
            Dim lTopFldrName As String = "Folder " & lIdx.ToString("000")
            Dim lTopFldrPath As String = "/" & lTopFldrName
            createItem(aSqliteFile, lTopFldrName, "/", ItemType.Folder, aError)
            aTreeViewExpected.Nodes.Add(lTopFldrName, lTopFldrName, ItemType.Folder, ItemType.Folder)
            lFoldersWithContents.Add(lTopFldrPath)
            ' Document
            Dim lTopDocName As String = "Document " & lIdx.ToString("000")
            Dim lTopDocPath As String = "/" & lTopDocName
            createItem(aSqliteFile, lTopDocName, "/", ItemType.Document, aError)
            aTreeViewExpected.Nodes.Add(lTopDocName, lTopDocName, ItemType.Document, ItemType.Document)
            For lIdx2 As Integer = 0 To lLoops
                ' Folder
                Dim l2ndFldrName As String = "Folder " & lIdx2.ToString("000")
                Dim l2ndFldrPath As String = "/" & lTopFldrName & "/" & l2ndFldrName
                createItem(aSqliteFile, l2ndFldrName, "/" & lTopFldrName, ItemType.Folder, aError)
                aTreeViewExpected.Nodes(lIdx * 2).Nodes.Add(l2ndFldrName, l2ndFldrName, ItemType.Folder, ItemType.Folder)
                lFoldersWithContents.Add(l2ndFldrPath)
                ' Document
                Dim l2ndDocName As String = "Document " & lIdx2.ToString("000")
                Dim l2ndDocPath As String = "/" & lTopFldrName & "/" & l2ndDocName
                createItem(aSqliteFile, l2ndDocName, "/" & lTopFldrName, ItemType.Document, aError)
                aTreeViewExpected.Nodes(lIdx * 2).Nodes.Add(l2ndDocName, l2ndDocName, ItemType.Document, ItemType.Document)
                For lIdx3 As Integer = 0 To lLoops
                    ' Folder
                    Dim l3rdFldrName As String = "Folder " & lIdx3.ToString("000")
                    Dim l3rdFldrPath As String = "/" & lTopFldrName & "/" & l2ndFldrName & "/" & l3rdFldrName
                    createItem(aSqliteFile, l3rdFldrName, "/" & lTopFldrName & "/" & l2ndFldrName, ItemType.Folder, aError)
                    aTreeViewExpected.Nodes(lIdx * 2).Nodes(lIdx2 * 2).Nodes.Add(l3rdFldrName, l3rdFldrName, ItemType.Folder, ItemType.Folder)
                    lFoldersWithoutContents.Add(l3rdFldrPath)
                    ' Document
                    Dim l3rdDocName As String = "Document " & lIdx3.ToString("000")
                    Dim l3rdDocPath As String = "/" & lTopFldrName & "/" & l2ndFldrName & "/" & l3rdDocName
                    createItem(aSqliteFile, l3rdDocName, "/" & lTopFldrName & "/" & l2ndFldrName, ItemType.Document, aError)
                    aTreeViewExpected.Nodes(lIdx * 2).Nodes(lIdx2 * 2).Nodes.Add(l3rdDocName, l3rdDocName, ItemType.Document, ItemType.Document)
                Next
            Next
        Next
        ' Test applyFolderStructure
        FolderStructure.applyFolderStructure(aSqliteFile, aTreeView.Nodes, aParent, ItemOrderBy.id, aError)
        Dim lExpectedTree As String = ""
        inOrderPrint(aTreeViewExpected.Nodes, lExpectedTree)
        Dim lActualTree As String = ""
        inOrderPrint(aTreeView.Nodes, lActualTree)
        Assert.AreEqual(lExpectedTree, lActualTree)

        ' Test applyFolderContents
        Dim lListView As New ListView
        For Each lFolder As String In lFoldersWithContents
            FolderStructure.applyFolderContents(aSqliteFile, lListView, lFolder, ItemOrderBy.name, aError)

            For lIdx As Integer = 0 To lLoops
                Dim lExpectedName As String = "Document " & lIdx.ToString("000")
                Dim lActualName As String = lListView.Items(lIdx).Text
                Assert.AreEqual(lExpectedName, lActualName)
                Dim lExpectedIcon As Integer = ItemType.Document
                Dim lActualIcon As Integer = lListView.Items(lIdx).ImageIndex
                Assert.AreEqual(lExpectedIcon, lActualIcon)
            Next
            For lIdx As Integer = 0 To lLoops
                Dim lExpectedName As String = "Folder " & lIdx.ToString("000")
                Dim lActualName As String = lListView.Items(lIdx + lLoops + 1).Text
                Assert.AreEqual(lExpectedName, lActualName)
                Dim lExpectedIcon As Integer = ItemType.Folder
                Dim lActualIcon As Integer = lListView.Items(lIdx + lLoops + 1).ImageIndex
                Assert.AreEqual(lExpectedIcon, lActualIcon)
                ' Dim lActualIcon As Integer = aListView.Items(lIdx + lLoops + 1).SubItems(0) = "folder"
            Next
        Next
        For Each lFolder As String In lFoldersWithoutContents
            FolderStructure.applyFolderContents(aSqliteFile, lListView, lFolder, ItemOrderBy.name, aError)
            Assert.AreEqual(lListView.Items.Count, 0)
        Next
        aSqliteFile.Close()
        ' test expected error conditions
        FolderStructure.applyFolderContents(aSqliteFile, lListView, "/", ItemOrderBy.name, aError)
        Assert.AreEqual(aError, "Cannot communicate with database.")
        aError = ""
        FolderStructure.applyFolderStructure(aSqliteFile, aTreeView.Nodes, "/", ItemOrderBy.id, aError)
        Assert.AreEqual(aError, "Cannot communicate with database.")
        ' cleanup
        If System.IO.File.Exists(lTestFile3) Then System.IO.File.Delete(lTestFile3)


        ' stuff I might want to test in the future:

        'Dim lExpectedIndex As Integer = aTreeViewExpected.Nodes.IndexOfKey(lKey)
        'Dim lActualIndex As Integer = aTreeView.Nodes.IndexOfKey(lKey)
        'If (lExpectedIndex = -1) Then
        '    Assert.Fail("Index -1, should have found a value")
        'End If
        'If (lActualIndex = -1) Then
        '    Assert.Fail("Index -1, should have found a value")
        'End If
        'Assert.AreEqual(lExpectedIndex, lActualIndex)
        'Assert.AreEqual(aTreeViewExpected.Nodes, aTreeView.Nodes)
        '''''' Assert.AreEqual(aErrorExpected, aError)
    End Sub
End Class
