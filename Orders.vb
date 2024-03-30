Imports System.Data.SqlClient
Public Class Orders
    Dim Con = New SqlConnection("Data Source=ADMIN\SQLEXPRESS;Initial Catalog=Cafe;Integrated Security=True")
    Private Sub lbllogout2_Click(sender As Object, e As EventArgs) Handles lbllogout2.Click
        Dim Obj = New Login
        Obj.Show()
        Me.Hide()
    End Sub
    Private Sub UpdateItem()
        Try
            Dim newQty = stock - Convert.ToInt32(txtquantity.Text)
            Con.Open()
            Dim query = "update ItemTbl set ItQty=" & newQty & " where ItId = " & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Item Edited")
            Con.Close()
            'Reset()
            DisplayItem()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
    Private Sub FilterByCategory()
        Con.Open()
        Dim query = "select * from ItemTbl where ItCat='" & cmbcategory.SelectedValue.ToString() & "'"
        Dim cmd = New SqlCommand(query, Con)
        Dim adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        dgvitem.DataSource = ds.Tables(0)
        Con.Close()
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


    Private Sub Orders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayItem()
        FillCategory()
    End Sub

    Private Sub cmbcategory_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbcategory.SelectionChangeCommitted
        FilterByCategory()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        DisplayItem()
    End Sub
    Dim ProdName As String
    Dim i = 0, GrdTotal = 0, price, qty
    Private Sub btnaddbill_Click(sender As Object, e As EventArgs) Handles btnaddbill.Click
        If key = 0 Then
            MsgBox("select a Item")
        ElseIf Convert.ToInt32(txtquantity.text) > stock Then
            MsgBox("No Enough Stock")
        Else
            Dim rnum As Integer = dgvbill.Rows.Add()
            Dim total = Convert.ToInt32(txtquantity.Text) * price
            i = i + 1
            dgvbill.Rows.Item(rnum).Cells("Column1").Value = i
            dgvbill.Rows.Item(rnum).Cells("Column2").Value = ProdName
            dgvbill.Rows.Item(rnum).Cells("Column3").Value = price
            dgvbill.Rows.Item(rnum).Cells("Column4").Value = txtquantity.Text
            dgvbill.Rows.Item(rnum).Cells("Column5").Value = total
            GrdTotal = GrdTotal + total
            lbltotal.Text = "Rs: " + Convert.ToString(GrdTotal)
            UpdateItem()
            txtquantity.Text = ""
            key = 0
        End If
    End Sub
    Private Sub AddBill()
        Con.Open()
        Dim query = "insert into OrderTbl values('" & DateTime.Today.Date & "'," & GrdTotal & ")"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(query, Con)
        cmd.ExecuteNonQuery()
        MsgBox("Bill Added")
        Con.Close()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString("Cafe Shop", New Font("Arial", 22), Brushes.BlueViolet, 335, 35)
        e.Graphics.DrawString("***YourBill***", New Font("Arial", 14), Brushes.BlueViolet, 350, 60)
        Dim bm As New Bitmap(Me.dgvbill.Width, Me.dgvbill.Height)
        dgvbill.DrawToBitmap(bm, New Rectangle(0, 0, Me.dgvbill.Width, Me.dgvbill.Height))
        e.Graphics.DrawImage(bm, 0, 90)
        e.Graphics.DrawString("Total Amount" + GrdTotal.ToString(), New Font("Arial", 15), Brushes.Crimson, 325, 580)
        e.Graphics.DrawString("==========Thank For Buying In Our Cafe==========", New Font("Arial", 15), Brushes.Crimson, 130, 600)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        AddBill()
        PrintPreviewDialog1.Show()
    End Sub

    Private Sub PrintPreviewDialog1_Load(sender As Object, e As EventArgs) Handles PrintPreviewDialog1.Load

    End Sub

    Private Sub btnorder_Click(sender As Object, e As EventArgs) Handles btnorder.Click
        Dim obj = New ViewOrders
        obj.Show()
        Me.Hide()
    End Sub

    Dim key = 0, stock
    Private Sub dgvitem_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvitem.CellMouseClick
        Dim row As DataGridViewRow = dgvitem.Rows(e.RowIndex)
        ProdName = row.Cells(1).Value.ToString()

        If ProdName = "" Then
            key = 0
            stock = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
            stock = Convert.ToInt32(row.Cells(4).Value.ToString)
            price = Convert.ToInt32(row.Cells(3).Value.ToString)

        End If
    End Sub
End Class