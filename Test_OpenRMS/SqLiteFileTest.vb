Imports System.Collections

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports OpenRMS


'''<summary>
'''This is a test class for SqLiteFileTest and is intended
'''to contain all SqLiteFileTest Unit Tests
'''</summary>
<TestClass()> _
Public Class SqLiteFileTest


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
    '''A test for all functionality of the SqLiteFile class.
    '''</summary>
    <TestMethod()> _
    Public Sub BasicTest()
        ' Dim lPath As String = "C:\Documents and Settings\pntmass\Desktop\Repository\Visual Studio Projects\OpenRMS\Test_OpenRMS\bin\Debug" 'System.Windows.Forms.Application.StartupPath
        Dim lDefaultDbFile As String = "Database.s3db"
        Dim lTestDbFile1 As String = "AuthoritngTests_Db1.s3db"
        Dim lTestDbFile2 As String = "AuthoritngTests_Db2.s3db"
        ' Test using the "Open" 
        If System.IO.File.Exists(lTestDbFile1) Then System.IO.File.Delete(lTestDbFile1)
        If System.IO.File.Exists(lTestDbFile2) Then System.IO.File.Delete(lTestDbFile2)
        Dim target As SqLiteFile = New SqLiteFile
        target.Open(lTestDbFile1)
        If Not System.IO.File.Exists(lDefaultDbFile) Then
            Assert.Fail("File '" & lDefaultDbFile & "' not created as expected on opening.")
        End If
        If Not target.CanWrite Then
            Assert.Fail("Open failed: CanWrite property shows it can't be written to after opening.")
        End If
        target.Close()
        If target.CanWrite Then
            Assert.Fail("Close failed: CanWrite property shows it can be written to after closing.")
        End If
        If Not System.IO.File.Exists(lTestDbFile1) Then
            Assert.Fail("File '" & lTestDbFile1 & "' not created as expected on opening.")
        End If
        target = New SqLiteFile(lTestDbFile2)
        target.Open()
        If Not System.IO.File.Exists(lTestDbFile2) Then
            Assert.Fail("File '" & lTestDbFile2 & "' not created as expected on opening.")
        End If
        ' Test Query
        Dim lDataTable As DataTable
        lDataTable = target.Query("CREATE TABLE IF NOT EXISTS test_table (id_field bigint, text_field text, int_field int)")
        If lDataTable.Rows.Count > 0 Then
            Assert.Fail("Shouldn't return rows")
        End If
        lDataTable = target.Query("INSERT INTO test_table VALUES (1,'this is a test of the table 1234567890',9012)")
        If lDataTable.Rows.Count > 0 Then
            Assert.Fail("Shouldn't return rows")
        End If
        lDataTable = target.Query("SELECT * FROM test_table")
        If lDataTable.Rows.Count <> 1 Then
            Assert.Fail("Should have returned exactly one row, instead returned " & lDataTable.Rows.Count.ToString)
        End If
        Dim lDataRow As DataRow = lDataTable.Rows(0)
        If lDataRow(0) <> 1 Then
            Assert.Fail("id_field value incorrect")
        End If
        If lDataRow(1) <> "this is a test of the table 1234567890" Then
            Assert.Fail("text_field value incorrect")
        End If
        If lDataRow(2) <> 9012 Then
            Assert.Fail("int_field value incorrect")
        End If

        target.Close()
        ' Cleanup
        If System.IO.File.Exists(lTestDbFile1) Then System.IO.File.Delete(lTestDbFile1)
        If System.IO.File.Exists(lTestDbFile2) Then System.IO.File.Delete(lTestDbFile2)
    End Sub
End Class
