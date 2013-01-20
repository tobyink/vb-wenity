Imports System.Text
Imports System.IO

Module Main

    Dim Arguments As List(Of String) _
        = New List(Of String)
    Dim Options As Dictionary(Of String, List(Of String)) _
        = New Dictionary(Of String, List(Of String))

    'Parse command line arguments, populating the Arguments and
    'Options structures.
    Public Sub ParseArgs()
        Dim argv As String() = Environment.GetCommandLineArgs()
        Dim params As New List(Of String)(argv)
        params.RemoveAt(0)

        Dim last As String = ""
        For Each p As String In params
            If Left(p, 2) = "--" Then
                Dim opt As String = Mid(p, 3)
                If Not Options.ContainsKey(opt) Then
                    Options.Add(opt, New List(Of String))
                End If
                last = opt
            ElseIf last = "" Then
                Arguments.Add(p)
            Else
                Options(last).Add(p)
            End If
        Next p
    End Sub

    Public Sub Main()
        'Get command line arguments into a list.
        ParseArgs()

        If Options.ContainsKey("info") Then
            GoInfo(MsgBoxStyle.Information)
        ElseIf Options.ContainsKey("warning") Then
            GoInfo(MsgBoxStyle.Exclamation)
        ElseIf Options.ContainsKey("error") Then
            GoInfo(MsgBoxStyle.Critical)
        ElseIf Options.ContainsKey("question") Then
            If GoInfo(MsgBoxStyle.YesNo) Then
                Console.WriteLine("Y")
            Else
                Console.WriteLine("N")
            End If
        ElseIf Options.ContainsKey("entry") And Options.ContainsKey("hide-text") Then
            Console.WriteLine(GoPassword())
        ElseIf Options.ContainsKey("entry") Then
            Console.WriteLine(GoEntry())
        ElseIf Options.ContainsKey("file-selection") Then
            Console.WriteLine(GoFileSelection())
        ElseIf Options.ContainsKey("list") Then
            Console.WriteLine(GoList())
        Else
            MsgBox("Wenity error!", MsgBoxStyle.Critical)
        End If

        Application.Exit()
    End Sub

    Public Function GoInfo(ByVal msgtype)
        Dim title As String = "Information"
        If Options.ContainsKey("title") Then
            title = Options("title")(0)
        End If

        Dim text As String = "All updates are complete."
        If Options.ContainsKey("text") Then
            text = Options("text")(0)
        End If

        Return MsgBox(text, msgtype, title)
    End Function

    Public Function GoEntry()
        Dim title As String = "Text entry"
        If Options.ContainsKey("title") Then
            title = Options("title")(0)
        End If

        Dim text As String = "All updates are complete."
        If Options.ContainsKey("text") Then
            text = Options("text")(0)
        End If

        Dim entry_text As String = ""
        If Options.ContainsKey("entry-text") Then
            text = Options("entry-text")(0)
        End If

        Return InputBox(text, title, entry_text)
    End Function

    Public Function GoPassword()
        Dim title As String = "Password entry"
        If Options.ContainsKey("title") Then
            title = Options("title")(0)
        End If

        Dim text As String = "Please enter your password."
        If Options.ContainsKey("text") Then
            text = Options("text")(0)
        End If

        Dim entry_text As String = ""
        If Options.ContainsKey("entry-text") Then
            text = Options("entry-text")(0)
        End If

        PasswordDialog.Text = title
        PasswordDialog.lblText.Text = text
        PasswordDialog.txtEntryText.Text = entry_text
        PasswordDialog.ShowDialog()
        Return PasswordDialog.txtEntryText.Text
    End Function

    Public Function GoFileSelection()
        Dim title As String = "File selection"
        If Options.ContainsKey("title") Then
            title = Options("title")(0)
        End If

        Dim text As String = "Please enter the path."
        If Options.ContainsKey("text") Then
            text = Options("text")(0)
        End If

        Dim entry_text As String = ""
        If Options.ContainsKey("entry-text") Then
            text = Options("entry-text")(0)
        End If

        Dim browse_type As String = "open"
        If Options.ContainsKey("directory") Then
            browse_type = "directory"
        ElseIf Options.ContainsKey("save") Then
            browse_type = "save"
        End If

        Dim browse_multi As Boolean = False
        If Options.ContainsKey("multiple") Then
            browse_multi = True
        End If

        FileSelectionDialog.Text = title
        FileSelectionDialog.lblText.Text = text
        FileSelectionDialog.txtEntryText.Text = entry_text
        FileSelectionDialog.browse_multi = browse_multi
        FileSelectionDialog.browse_type = browse_type
        FileSelectionDialog.ShowDialog()
        Return FileSelectionDialog.txtEntryText.Text
    End Function

    Public Function GoList()
        Dim title As String = "Selection"
        If Options.ContainsKey("title") Then
            title = Options("title")(0)
        End If

        Dim text As String = "Please choose."
        If Options.ContainsKey("text") Then
            text = Options("text")(0)
        End If

        ListDialog.Text = title
        ListDialog.lblText.Text = text

        ListDialog.lstList.SelectionMode = SelectionMode.One
        If Options.ContainsKey("multiple") Then
            ListDialog.lstList.SelectionMode = SelectionMode.MultiExtended
        End If

        For Each a As String In Arguments
            ListDialog.lstList.Items.Add(a)
        Next a

        ListDialog.ShowDialog()

        Dim l = ""
        Dim i As Integer
        For i = 0 To ListDialog.lstList.SelectedIndices.Count - 1
            l = l + "|" + ListDialog.lstList.SelectedIndices(i).ToString
        Next i

        Return Mid(l, 2)
    End Function

End Module
