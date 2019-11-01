Imports System.Windows.Forms

Public Class NewItem

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub checkEnableOk()
        If TxtName.Text <> "" AndAlso CmbType.Text <> "" Then
            OK_Button.Enabled = True
        Else
            OK_Button.Enabled = False
        End If
    End Sub


    Private Sub TxtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtName.TextChanged
        checkEnableOk()
    End Sub

    Private Sub CmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbType.SelectedIndexChanged
        checkEnableOk()
    End Sub

    Private Sub NewItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For lItem As FolderStructure.ItemType = FolderStructure.ItemType.Folder To FolderStructure.ItemType.Document
            CmbType.Items.Add(lItem.ToString)
        Next
    End Sub
End Class
