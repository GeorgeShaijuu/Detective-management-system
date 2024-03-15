Imports MySql.Data.MySqlClient

Public Class Form3
    ' Connection string to your MySQL database
    Private connectionString As String = "server=localhost;user=root;password=admin;database=dam;"

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load client IDs into Guna2ComboBox1
        LoadClientIDs()
        LoadCasesData()
        ' Load predefined specialization options into Guna2ComboBox2
        Guna2ComboBox2.Items.Add("Surveillance")
        Guna2ComboBox2.Items.Add("Cybercrime")
        Guna2ComboBox2.Items.Add("Fraud Investigation")
        Guna2ComboBox4.Items.Add("Active")
        Guna2ComboBox4.Items.Add("Inactive")
        ' Add event handler to Guna2ComboBox2 selection change
        AddHandler Guna2ComboBox2.SelectedIndexChanged, AddressOf LoadDetectiveNames
    End Sub

    Private Sub LoadClientIDs()
        Try
            ' Establish connection
            Using connection As New MySqlConnection(connectionString)
                ' Open the connection
                connection.Open()

                ' Prepare SQL command to retrieve client IDs
                Dim query As String = "SELECT client_id FROM client"
                Using cmd As New MySqlCommand(query, connection)
                    ' Execute the command and read the data
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        ' Clear existing items
                        Guna2ComboBox1.Items.Clear()

                        ' Add client IDs to Guna2ComboBox1
                        While reader.Read()
                            Guna2ComboBox1.Items.Add(reader("client_id"))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadDetectiveNames(sender As Object, e As EventArgs)
        Try
            ' Establish connection
            Using connection As New MySqlConnection(connectionString)
                ' Open the connection
                connection.Open()

                ' Prepare SQL command to retrieve detective names based on specialization
                Dim query As String = "SELECT name FROM detective WHERE specialization = @specialization"
                Using cmd As New MySqlCommand(query, connection)
                    ' Add specialization parameter
                    cmd.Parameters.AddWithValue("@specialization", Guna2ComboBox2.SelectedItem.ToString())

                    ' Execute the command and read the data
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        ' Clear existing items
                        Guna2ComboBox3.Items.Clear()

                        ' Add detective names to Guna2ComboBox3
                        While reader.Read()
                            Guna2ComboBox3.Items.Add(reader("name"))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        ' Retrieve selected values from combo boxes
        Dim clientID = Convert.ToInt32(Guna2ComboBox1.SelectedItem)
        Dim genre = Guna2ComboBox2.SelectedItem.ToString
        Dim detectiveName = Guna2ComboBox3.SelectedItem.ToString
        Dim status = Guna2ComboBox4.SelectedItem.ToString

        ' Insert case data into the database
        If InsertCaseData(clientID, genre, detectiveName, status) Then
            MessageBox.Show("Case data added successfully.")
        Else
            MessageBox.Show("Failed to add case data.")
        End If
    End Sub

    Private Function InsertCaseData(clientID As Integer, genre As String, detectiveName As String, status As String) As Boolean
        Try
            ' Establish connection
            Using connection As New MySqlConnection(connectionString)
                ' Open the connection
                connection.Open()

                ' Get detective ID based on detective name
                Dim detectiveID As Integer = GetDetectiveIDByName(detectiveName)

                ' Prepare SQL command to insert case data
                Dim query As String = "INSERT INTO cases (clientid, Genre, detectiveid, status) VALUES (@clientID, @genre, @detectiveID, @status)"
                Using cmd As New MySqlCommand(query, connection)
                    ' Add parameters
                    cmd.Parameters.AddWithValue("@clientID", clientID)
                    cmd.Parameters.AddWithValue("@genre", genre)
                    cmd.Parameters.AddWithValue("@detectiveID", detectiveID)
                    cmd.Parameters.AddWithValue("@status", status)

                    ' Execute the command
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True ' Return true if insertion was successful
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return False ' Return false if an exception occurred
        End Try
    End Function

    Private Function GetDetectiveIDByName(name As String) As Integer
        Try
            ' Establish connection
            Using connection As New MySqlConnection(connectionString)
                ' Open the connection
                connection.Open()

                ' Prepare SQL command to retrieve detective ID by name
                Dim query As String = "SELECT decid FROM detective WHERE name = @name"
                Using cmd As New MySqlCommand(query, connection)
                    ' Add name parameter
                    cmd.Parameters.AddWithValue("@name", name)

                    ' Execute the command and return the detective ID
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return -1 ' Return -1 if an exception occurred
        End Try
    End Function
    Private Sub LoadCasesData()
        Dim query As String = "SELECT * FROM cases"
        Using connection As New MySqlConnection(connectionString)
            Using adapter As New MySqlDataAdapter(query, connection)
                Dim dataset As New DataSet()
                adapter.Fill(dataset)
                Guna2DataGridView1.DataSource = dataset.Tables(0)
            End Using
        End Using
    End Sub
End Class
