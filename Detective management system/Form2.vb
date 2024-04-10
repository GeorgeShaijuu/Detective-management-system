Imports MySql.Data.MySqlClient

Public Class Form2
    Private connectionString As String = "server=localhost;user=root;password=admin;database=dam;"
    Private Function IsValidName(name As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(name, "^[A-Za-z ]+$")
    End Function

    Private Function IsValidPhoneNumber(number As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(number, "^\d{10}$")
    End Function

    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim mail = New System.Net.Mail.MailAddress(email)
            Return mail.Address = email
        Catch
            Return False
        End Try
    End Function
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
        If Not IsValidName(Guna2TextBox1.Text) Then
            MessageBox.Show("Please enter a valid name.")
            Return
        End If

        If Not IsValidPhoneNumber(Guna2TextBox2.Text) Then
            MessageBox.Show("Please enter a valid 10-digit phone number.")
            Return
        End If

        If Not IsValidEmail(Guna2TextBox3.Text) Then
            MessageBox.Show("Please enter a valid email address.")
            Return
        End If
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
        If Not IsValidName(Guna2TextBox8.Text) Then
            MessageBox.Show("Please enter a valid name.")
            Return
        End If

        If Not IsValidPhoneNumber(Guna2TextBox7.Text) Then
            MessageBox.Show("Please enter a valid 10-digit phone number.")
            Return
        End If

        If Not IsValidEmail(Guna2TextBox6.Text) Then
            MessageBox.Show("Please enter a valid email address.")
            Return
        End If
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

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        ' Connection string to connect to your database
        Dim connectionString As String = "server=localhost; user=root; password=admin; database=dam;"

        ' SQL query to select data from the report table
        Dim reportQuery As String = "SELECT * FROM report"
        ' SQL query to select data from the payments table
        Dim paymentQuery As String = "SELECT * FROM payments"

        ' Variable to hold the message that will be displayed in the message box
        Dim message As String = "Report Table Data:" & Environment.NewLine

        Using connection As New MySqlConnection(connectionString)
            ' Open the connection to the database
            connection.Open()

            ' Create a new command with the report query and connection
            Using reportCommand As New MySqlCommand(reportQuery, connection)
                ' Execute the report query and obtain a reader to read the results
                Using reportReader As MySqlDataReader = reportCommand.ExecuteReader()
                    ' Check if there are rows in the report table
                    If reportReader.HasRows Then
                        ' Loop through all the rows in the report table
                        While reportReader.Read()
                            ' Append each row's data to the message
                            ' Assuming the report table has columns caseid, client_id, decid
                            ' Adjust the column names as per your actual table structure
                            message &= $"Case ID: {reportReader("caseid")}, Client ID: {reportReader("client_id")}, Detective ID: {reportReader("decid")}" & Environment.NewLine
                        End While
                    Else
                        message &= "No data found in the report table." & Environment.NewLine
                    End If
                End Using
            End Using

            ' Append a separator between the report and payment table data
            message &= Environment.NewLine & "Payment Table Data:" & Environment.NewLine

            ' Create a new command with the payment query and connection
            Using paymentCommand As New MySqlCommand(paymentQuery, connection)
                ' Execute the payment query and obtain a reader to read the results
                Using paymentReader As MySqlDataReader = paymentCommand.ExecuteReader()
                    ' Check if there are rows in the payment table
                    If paymentReader.HasRows Then
                        ' Loop through all the rows in the payment table
                        While paymentReader.Read()
                            ' Append each row's data to the message
                            ' Assuming the payments table has columns caseid, paymethod, payamt
                            ' Adjust the column names as per your actual table structure
                            message &= $"Case ID: {paymentReader("caseid")}, Payment Method: {paymentReader("paymethod")}, Payment Amount: {paymentReader("payamt")}" & Environment.NewLine
                        End While
                    Else
                        message &= "No data found in the payment table." & Environment.NewLine
                    End If
                End Using
            End Using
        End Using

        ' Display the combined message in a message box
        MessageBox.Show(message, "Report and Payment Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class
