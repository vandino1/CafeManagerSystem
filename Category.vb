Imports System.Data.SqlClient
Public Class Category
    Dim Con = New SqlConnection("Data Source=ADMIN\SQLEXPRESS;Initial Catalog=Cafe;Integrated Security=True")
    Private Sub DisplayItem()
        Con.Open()
        Dim query = "select * from CategoryTbl"
        Dim cmd = New SqlCommand(query, Con)
        Dim adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        dgvcategory.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Dim key = 0
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If key = 0 Then
            MsgBox("Select The Cate To Delete")
        Else
            Con.Open()
            Dim query = "delete from CategoryTbl where CatId=" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Item Deleted")
            Con.Close()
            txtnamecate.Text = ""
            DisplayItem()
        End If
    End Sub

    Private Sub Category_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayItem()
    End Sub

    Private Sub dgvcategory_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvcategory.CellMouseClick
        Dim row As DataGridViewRow = dgvcategory.Rows(e.RowIndex)
        txtnamecate.Text = row.Cells(1).Value.ToString
        If txtnamecate.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Application.Exit()
    End Sub

    Private Sub lbllogout1_Click(sender As Object, e As EventArgs) Handles lbllogout1.Click
        Dim Obj = New Login
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Dim Obj = New Items
        Obj.Show()
        Me.Hide()
    End Sub
End Class