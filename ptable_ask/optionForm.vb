Public Class optionForm
    Private Sub optionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim showx = Form1.Location.X + Form1.Size.Width / 2 - Me.Size.Width / 2
        Dim showy = Form1.Location.Y + Form1.Size.Height / 2 - Me.Size.Height / 2
        Me.SetDesktopLocation(showx, showy)
        CheckBox1.Checked = My.Settings.focusmode
        CheckBox2.Checked = My.Settings.ask_aWeight
        NumericUpDown1.Value = My.Settings.numcount
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim needExit As Boolean = False

        If Not (CheckBox1.Checked = My.Settings.focusmode) Then needExit = True

        My.Settings.focusmode = CheckBox1.Checked
        My.Settings.ask_aWeight = CheckBox2.Checked
        My.Settings.numcount = NumericUpDown1.Value
        My.Settings.Save()
        My.Settings.Reload()

        If needExit Then
            MsgBox("첼린지 모드 적용을 위해 프로그램을 재시작합니다.", vbInformation)
            Application.Exit()
        End If

        Me.Close()
    End Sub
End Class