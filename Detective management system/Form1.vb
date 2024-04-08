Imports MySql.Data.MySqlClient

Public Class Form1
    ' Adjust the connection string to match your database configuration
    Private connectionString As String = "server=localhost;user=root;password=admin;database=dam;"

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        ' Retrieve user input
        Dim username As String = Guna2TextBox1.Text
        Dim password As String = Guna2TextBox2.Text

        ' Check credentials
        If AuthenticateUser(username, password) Then
            MessageBox.Show("Login Successful!")
            Form2.Show()
        Else
            MessageBox.Show("Invalid Username or Password.")
        End If
    End Sub

    Private Function AuthenticateUser(username As String, password As String) As Boolean
        ' Using block ensures that the connection is closed properly
        Using connection As New MySqlConnection(connectionString)
            Try
                ' Open the connection
                connection.Open()

                ' SQL query to check if a user exists with the given username and password
                Dim sql As String = "SELECT COUNT(*) FROM login WHERE username = @username AND password = @password"
                Using cmd As New MySqlCommand(sql, connection)
                    ' Use parameters to prevent SQL Injection
                    cmd.Parameters.AddWithValue("@username", username)
                    cmd.Parameters.AddWithValue("@password", password) ' Consider hashing the password

                    ' Execute the query and get the result
                    Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    ' If the result is 1, a matching user was found
                    If result > 0 Then
                        Return True
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show($"An error occurred: {ex.Message}")
            End Try
        End Using
        Return False
    End Function

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
