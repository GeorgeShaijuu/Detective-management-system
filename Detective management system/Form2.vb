Imports MySql.Data.MySqlClient

Public Class Form2
    Private connectionString As String = "server=localhost;user=root;password=admin;database=dam;"

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2ComboBox1.Items.Add("Surveillance")
        Guna2ComboBox1.Items.Add("Cybercrime")
        Guna2ComboBox1.Items.Add("Fraud Investigation")
        Guna2GroupBox2.Visible = False
        Guna2GroupBox3.Visible = False
    End Sub
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Guna2GroupBox2.Visible = True
        Guna2GroupBox3.Visible = False

    End Sub
    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Guna2GroupBox2.Visible = False
        Guna2GroupBox3.Visible = True
    End Sub
    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        ' Retrieve data from textboxes
        Dim fullname As String = Guna2TextBox1.Text
        Dim contactNo As String = Guna2TextBox2.Text
        Dim email As String = Guna2TextBox3.Text
        Dim address As String = Guna2TextBox4.Text

        ' Insert client data into the database
        If InsertClientData(fullname, contactNo, email, address) Then
            MessageBox.Show("Client data added successfully.")
        Else
            MessageBox.Show("Failed to add client data.")
        End If
    End Sub

    Private Function InsertClientData(fullname As String, contactNo As String, email As String, address As String) As Boolean
        Try
            ' Establish connection
            Using connection As New MySqlConnection(connectionString)
                ' Open the connection
                connection.Open()

                ' Prepare SQL command
                Dim query As String = "INSERT INTO client (fullname, contact_no, email, address) VALUES (@fullname, @contactNo, @email, @address)"
                Using cmd As New MySqlCommand(query, connection)
                    ' Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@fullname", fullname)
                    cmd.Parameters.AddWithValue("@contactNo", contactNo)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@address", address)

                    ' Execute the command
                    cmd.ExecuteNonQuery()
                End Using

                ' Close the connection
                connection.Close()

                Return True ' Return true if insertion was successful
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return False ' Return false if an exception occurred
        End Try
    End Function
    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        ' Retrieve data from textboxes and combobox
        Dim name As String = Guna2TextBox8.Text
        Dim contactNo As String = Guna2TextBox7.Text
        Dim email As String = Guna2TextBox6.Text
        Dim specialization As String = Guna2ComboBox1.SelectedItem.ToString()

        ' Insert detective data into the database
        If InsertDetectiveData(name, contactNo, email, specialization) Then
            MessageBox.Show("Detective data added successfully.")
        Else
            MessageBox.Show("Failed to add detective data.")
        End If
    End Sub

    Private Function InsertDetectiveData(name As String, contactNo As String, email As String, specialization As String) As Boolean
        Try
            ' Establish connection
            Using connection As New MySqlConnection(connectionString)
                ' Open the connection
                connection.Open()

                ' Prepare SQL command
                Dim query As String = "INSERT INTO detective (name, contactno, email, specialization) VALUES (@name, @contactNo, @email, @specialization)"
                Using cmd As New MySqlCommand(query, connection)
                    ' Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@name", name)
                    cmd.Parameters.AddWithValue("@contactNo", contactNo)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@specialization", specialization)

                    ' Execute the command
                    cmd.ExecuteNonQuery()
                End Using

                ' Close the connection
                connection.Close()

                Return True ' Return true if insertion was successful
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return False ' Return false if an exception occurred
        End Try
    End Function

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Form3.Show()
    End Sub
End Class
