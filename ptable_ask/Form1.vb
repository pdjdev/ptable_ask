Public Class Form1
    Dim Question
    Dim Answer
    Dim target

    Dim count = 3
    Dim prevans
    Dim loc As Point

    Dim 원소기호() = {"H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne", "Na", "Mg", "Al", "Si", "P", "S", "Cl", "Ar", "K", "Ca"}
    Dim 원자번호() = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20"}
    Dim 원소명() = {"수소", "헬륨", "리튬", "베릴륨", "붕소", "탄소", "질소", "산소", "플루오린", "네온", "나트륨", "마그네슘", "알루미늄", "규소", "인", "황", "염소", "아르곤", "칼륨", "칼슘"}
    Dim 주기() = {1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4}
    Dim 족() = {1, 18, 1, 2, 13, 14, 15, 16, 17, 18, 1, 2, 13, 14, 15, 16, 17, 18, 1, 2}
    Dim 표에서_족() = {1, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2}
    Dim 원자량() = {1.0, 4.0, 6.93, 9.01, 10.81, 12.01, 14.0, 15.99, 18.99,
        20.17, 22.98, 24.31, 26.98, 28.08, 30.97, 32.06, 35.45, 39.94, 39.09, 40.08}

    Declare Function Wow64DisableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Declare Function Wow64EnableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Private osk As String = "C:\Windows\System32\osk.exe"

    Private Sub FormDrag_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            loc = e.Location
        End If
    End Sub

    Private Sub FormDrag_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += e.Location - loc
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles EnterBT.Click
        If TextBox1.Text = Answer Then

            If My.Settings.focusmode Then
                If count = 1 Then
                    QuestionLabel.Text = "좋습니다! 환경 설정 중..."
                    Me.Refresh()
                    Threading.Thread.Sleep(500)
                    Solved()
                Else
                    TextBox1.Text = Nothing
                    count -= 1
                    Dim c As Control = Me.TableLayoutPanel1.GetControlFromPosition(표에서_족(target), 주기(target))
                    c.BackgroundImage = Nothing
                    question_gen()
                End If
            Else
                TextBox1.Text = Nothing
                Dim c As Control = Me.TableLayoutPanel1.GetControlFromPosition(표에서_족(target), 주기(target))
                c.BackgroundImage = Nothing
                question_gen()
            End If

        Else
            MsgBox("오답, 다시 시도해 주세요")
        End If

    End Sub

    Public Sub question_gen()
genagain:
        CountLabel.Text = "남은 질문: " + count.ToString

        Dim r As Random = New Random

        target = r.Next(0, 20)

        Dim r2 As Random = New Random

        Dim q_type

        If My.Settings.ask_aWeight Then
            q_type = r2.Next(1, 5)
        Else
            q_type = r2.Next(1, 4)
        End If

        Dim c As Control = Me.TableLayoutPanel1.GetControlFromPosition(표에서_족(target), 주기(target))
        c.BackgroundImage = My.Resources.check




        Select Case q_type
            Case 1 '원소기호묻기
                Question = "위 주기율표에 표시된 곳에 위치한 원소의 기호는?"
                Answer = 원소기호(target)
            Case 2 '원자번호묻기
                Question = "위 주기율표에 표시된 곳에 위치한 원소의 원자번호는?"
                Answer = 원자번호(target)
            Case 3 '원소명묻기
                Question = "위 주기율표에 표시된 곳에 위치한 원소의 원소명은?"
                Answer = 원소명(target)
            Case 4 '원자량묻기
                Question = "위 주기율표에 표시된 곳에 위치한 원소의 원자량은? (소수점 아래 반올림)"
                Answer = Math.Round(원자량(target), 0).ToString
        End Select

        If Answer = prevans Then
            c.BackgroundImage = Nothing
            GoTo genagain
        End If

        prevans = Answer

        QuestionLabel.Text = Question

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles SwitchBT.Click
        Dim c As Control = Me.TableLayoutPanel1.GetControlFromPosition(표에서_족(target), 주기(target))
        c.BackgroundImage = Nothing
        question_gen()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        QuestionLabel.Hide()

        If My.Settings.focusmode Then
            count = My.Settings.numcount
            Shell("taskkill /f /im explorer.exe")
            Timer1.Start()
            CloseBT.Visible = False
        Else
            msgLabel.Visible = False
            CountLabel.Visible = False
        End If

    End Sub

    Private Sub QuestionLabel_Paint(sender As Object, e As PaintEventArgs) Handles QuestionLabel.Paint
        QuestionLabel.Location = New Point((Panel47.Width - QuestionLabel.Width) / 2, (Panel47.Height - QuestionLabel.Height) / 2)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            For Each prog As Process In Process.GetProcesses
                If prog.ProcessName = "Taskmgr" Then
                    prog.Kill()
                ElseIf prog.ProcessName = "explorer" Then
                    prog.Kill()
                ElseIf prog.ProcessName = "iexplore" Then
                    prog.Kill()
                ElseIf prog.ProcessName = "firefox" Then
                    prog.Kill()
                ElseIf prog.ProcessName = "chrome" Then
                    prog.Kill()
                ElseIf prog.ProcessName = "cmd" Then
                    prog.Kill()
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Refresh()
        FadeIn(Me)
        question_gen()

        QuestionLabel.Show()
        QuestionLabel.Refresh()
    End Sub

    Private Sub Solved()
        Timer1.Stop()
        Dim ExProcess = New Process()
        ExProcess.StartInfo.UseShellExecute = True
        ExProcess.StartInfo.CreateNoWindow = True
        ExProcess.StartInfo.FileName = "c:\windows\explorer.exe"
        ExProcess.StartInfo.WorkingDirectory = Application.StartupPath
        ExProcess.Start()

        For Each prog As Process In Process.GetProcesses
            If prog.ProcessName = "osk" Then
                prog.Kill()
            End If
        Next

        FadeOut(Me)
        Me.Close()
    End Sub

    Private Sub KeyboardBT_Click(sender As Object, e As EventArgs) Handles KeyboardBT.Click
        Try
            Dim old As Long
            If Environment.Is64BitOperatingSystem Then
                If Wow64DisableWow64FsRedirection(old) Then
                    Process.Start(osk)
                    Wow64EnableWow64FsRedirection(old)
                End If
            Else
                Process.Start(osk)
            End If
        Catch ex As Exception
            MsgBox("Windows에서 화상 키보드를 실행하는 도중 문제가 발생하셨습니다. 설치 여부를 확인해 주세요.", vbExclamation)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles ConfigBT.Click
        optionForm.Show()
    End Sub

    Private Sub CloseBT_Click(sender As Object, e As EventArgs) Handles CloseBT.Click
        Me.Close()
    End Sub
End Class
