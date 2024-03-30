Public Class Login
    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        If txtusername.Text = "" Or txtpassword.Text = "" Then
            MsgBox("Enter username and password")
        ElseIf txtusername.Text = "Admin" And txtpassword.Text = "Password" Then
            Dim Obj = New Items
            Obj.Show()
            Me.Hide()
        Else
            MsgBox("Wrong Username or Password")
            txtusername.Text = ""
            txtpassword.Text = ""
        End If
    End Sub

    Private Sub lblseller_Click(sender As Object, e As EventArgs) Handles lblseller.Click
        Dim Obj = New Orders
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub
End Class
