Imports System.Windows.Forms

Public Class FileSelectionDialog

    Public browse_type As String = "open"
    Public browse_multi As Boolean = False

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        If browse_type = "directory" Then
            Me.FolderBrowserDialog.ShowDialog()
            Me.txtEntryText.Text = Me.FolderBrowserDialog.SelectedPath
        ElseIf browse_type = "save" Then
            Me.SaveFileDialog.ShowDialog()
            Me.txtEntryText.Text = Me.SaveFileDialog.FileName
        ElseIf browse_multi Then
            Me.OpenFileDialog.Multiselect = True
            Me.OpenFileDialog.ShowDialog()
            Me.txtEntryText.Text = Join(Me.OpenFileDialog.FileNames, "|")
        Else
            Me.OpenFileDialog.ShowDialog()
            Me.txtEntryText.Text = Me.OpenFileDialog.FileName
        End If
    End Sub
End Class
