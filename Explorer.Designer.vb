<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Explorer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Explorer))
        Me.TreeViewDirectoryStructure = New System.Windows.Forms.TreeView
        Me.ListViewCurDir = New System.Windows.Forms.ListView
        Me.ImageListIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.MenuStrip = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NewItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'TreeViewDirectoryStructure
        '
        Me.TreeViewDirectoryStructure.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeViewDirectoryStructure.HideSelection = False
        Me.TreeViewDirectoryStructure.HotTracking = True
        Me.TreeViewDirectoryStructure.Location = New System.Drawing.Point(-1, 3)
        Me.TreeViewDirectoryStructure.Name = "TreeViewDirectoryStructure"
        Me.TreeViewDirectoryStructure.Size = New System.Drawing.Size(168, 282)
        Me.TreeViewDirectoryStructure.TabIndex = 0
        '
        'ListViewCurDir
        '
        Me.ListViewCurDir.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListViewCurDir.Location = New System.Drawing.Point(3, 3)
        Me.ListViewCurDir.Name = "ListViewCurDir"
        Me.ListViewCurDir.Size = New System.Drawing.Size(338, 282)
        Me.ListViewCurDir.TabIndex = 1
        Me.ListViewCurDir.UseCompatibleStateImageBehavior = False
        '
        'ImageListIcons
        '
        Me.ImageListIcons.ImageStream = CType(resources.GetObject("ImageListIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListIcons.Images.SetKeyName(0, "sqlite_database.png")
        Me.ImageListIcons.Images.SetKeyName(1, "xp_folder.png")
        Me.ImageListIcons.Images.SetKeyName(2, "Document.png")
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 27)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeViewDirectoryStructure)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ListViewCurDir)
        Me.SplitContainer1.Size = New System.Drawing.Size(518, 288)
        Me.SplitContainer1.SplitterDistance = 170
        Me.SplitContainer1.TabIndex = 2
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(518, 24)
        Me.MenuStrip.TabIndex = 3
        Me.MenuStrip.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewItemToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewItemToolStripMenuItem
        '
        Me.NewItemToolStripMenuItem.Name = "NewItemToolStripMenuItem"
        Me.NewItemToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.NewItemToolStripMenuItem.Text = "New Item"
        '
        'Explorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(518, 315)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip)
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "Explorer"
        Me.Text = "OpenRMS Explorer"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TreeViewDirectoryStructure As System.Windows.Forms.TreeView
    Friend WithEvents ListViewCurDir As System.Windows.Forms.ListView
    Friend WithEvents ImageListIcons As System.Windows.Forms.ImageList
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
