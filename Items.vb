Imports System.Data.SqlClient
Public Class Items
    Dim Con = New SqlConnection("Data Source=ADMIN\SQLEXPRESS;Initial Catalog=Cafe;Integrated Security=True")
    Private Sub lbllogout1_Click(sender As Object, e As EventArgs) Handles lbllogout1.Click
        Dim Obj = New Login
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Application.Exit()
    End Sub

    Private Sub btnAddcategory_Click(sender As Object, e As EventArgs) Handles btnAddcategory.Click
        If txtcategory.Text = "" Then
            MsgBox("Enter The Category")
        Else
            Con.Open()
            Dim query = "insert into CategoryTbl values('" & txtcategory.Text & "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Category Added")
            Con.Close()
            txtcategory.Text = ""
            FillCategory()
        End If
    End Sub
    Private Sub Reset()
        txtname.Text = ""
        cmbcategory.SelectedIndex = 0
        txtprice.Text = ""
        txtquantity.Text = ""
    End Sub
    Private Sub FillCategory()
        Con.Open()
        Dim cmd = New SqlCommand("select * from CategoryTbl", Con)
        Dim adapter = New SqlDataAdapter(cmd)
        Dim Tbl = New DataTable()
        adapter.Fill(Tbl)
        cmbcategory.DataSource = Tbl
        cmbcategory.DisplayMember = "CatName"
        cmbcategory.ValueMember = "CatName"
        Con.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub Items_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillCategory()
        DisplayItem()
    End Sub
    Private Sub DisplayItem()
        Con.Open()
        Dim query = "select * from ItemTbl"
        Dim cmd = New SqlCommand(query, Con)
        Dim adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        dgvitem.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        If cmbcategory.SelectedIndex = -1 Or txtname.Text = "" Or txtprice.Text = "" Or txtquantity.Text = "" Then
            MsgBox("Missing Infomation")
        Else
            Con.Open()
            Dim query = "insert into ItemTbl values('" & txtname.Text & "', '" & cmbcategory.SelectedValue.ToString() & "', '" & txtprice.Text & "', '" & txtquantity.Text & "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Item Added")
            Con.Close()
            Reset()
            DisplayItem()
        End If
    End Sub
    Dim key = 0
    Private Sub dgvitem_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvitem.CellMouseClick
        Dim row As DataGridViewRow = dgvitem.Rows(e.RowIndex)
        txtname.Text = row.Cells(1).Value.ToString
        cmbcategory.SelectedValue = row.Cells(2).Value.ToString
        txtprice.Text = row.Cells(3).Value.ToString
        txtquantity.Text = row.Cells(4).Value.ToString
        If txtname.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If key = 0 Then
            MsgBox("Select The Item To Delete")
        Else
            Con.Open()
            Dim query = "delete from ItemTbl where ItId=" & key & ""
            Dim cmd As SqlCommand
        cmd = New SqlCommand(query, Con)
        cmd.ExecuteNonQuery()
            MsgBox("Item Deleted")
            Con.Close()
        Reset()
        DisplayItem()
        End If
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If cmbcategory.SelectedIndex = -1 Or txtname.Text = "" Or txtprice.Text = "" Or txtquantity.Text = "" Then
            MsgBox("Missing Infomation")
        Else
            Try
                Con.Open()
                Dim query = "update ItemTbl set ItName='" & txtname.Text & "', ItCat='" & cmbcategory.SelectedValue.ToString() & "', ItPrice=" & txtprice.Text & ", ItQty=" & txtquantity.Text & " where ItId = " & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Item Edited")
                Con.Close()
                Reset()
                DisplayItem()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnCategory_Click(sender As Object, e As EventArgs) Handles btnCategory.Click
        Dim Obj = New Category
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        TryCast(dgvitem.DataSource, DataTable).DefaultView.RowFilter =
             String.Format("ItName like '%" & txtsearch.Text & "%'")

    End Sub
End Class