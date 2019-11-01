Public Class DocumentEditor
    Private Class LinkRecord
        Public ModuleId As Integer
        Public SubsectionId As Integer
    End Class
    Private Class DocumentSubsection
        Public Panel As TableLayoutPanel
        Public Id As Int64
        Public MainHeadingAndText As RichTextBox
        Public Attributes() As RichTextBox
        Public OutLinks() As LinkRecord
        Public InLinks() As LinkRecord
    End Class
    Public Class Document

    End Class
    'Public Sub AdjustHeight(ByVal rtfRichEdit As RichTextBox, ByVal lpMinHeight As Long)
    '    Dim lpMax As Long    ' Max scroll position
    '    Dim lpMin As Long    ' Min scroll position

    '    ' get the scroll range
    '    GetScrollRange(rtfRichEdit.hwnd, SB_VERT, lpMin, lpMax)

    '    ' It may not be necessary to subtract the lpMin value from lpMax because I can't think of anytime that lpMin would be anything but 0... but just in case....
    '    If ((lpMax - lpMin) * Screen.TwipsPerPixelY < lpMinHeight) Then
    '        ' This allows you to set a minimum height for your control
    '        rtfRichEdit.Height = lpMinHeight
    '    Else
    '        ' Else the control height should be equal to the text height - the max scroll value.
    '        rtfRichEdit.Height = (lpMax - lpMin) * Screen.TwipsPerPixelY
    '    End If
    'End Sub
    Private Sub resizeTextBox(ByRef aRichTextBox As RichTextBox)
        Dim lTop As Point = aRichTextBox.GetPositionFromCharIndex(0)
        Dim lBottom As Point = aRichTextBox.GetPositionFromCharIndex(aRichTextBox.Text.Length - 1)
        Dim lDesiredHeight As Integer = (lBottom.Y - lTop.Y) + 10
        Dim lMaxHeight As Integer = Me.Height - 55
        'If (pVisiblePoint < aRichTextBox.Height) Then
        TextBox1.Text = Me.Bottom ' lDesiredHeight
        TextBox2.Text = Me.Height 'aRichTextBox.Height
        If (aRichTextBox.Height <= lMaxHeight) AndAlso (lDesiredHeight > aRichTextBox.Height) Then
            aRichTextBox.Height = Math.Min(lDesiredHeight, lMaxHeight)
        End If

        If (lDesiredHeight > lMaxHeight) Then
            aRichTextBox.ScrollBars = RichTextBoxScrollBars.Both
        Else
            aRichTextBox.ScrollBars = RichTextBoxScrollBars.Horizontal
        End If
    End Sub
    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged
        resizeTextBox(RichTextBox1)
        'End If
    End Sub
End Class