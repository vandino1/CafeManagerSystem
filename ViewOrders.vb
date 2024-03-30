Imports System.Data.SqlClient
Public Class ViewOrders
    Dim Con = New SqlConnection("Data Source=ADMIN\SQLEXPRESS;Initial Catalog=Cafe;Integrated Security=True")
    Private Sub DisplayBill()
        Con.Open()
        Dim query = "select * from OrderTbl"
        Dim cmd = New SqlCommand(query, Con)
        Dim adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        dgvorderlist.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub ViewOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayBill()
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Dim obj = New Orders
        obj.Show()
        Me.Hide()
    End Sub
End Class