Imports System.Data.SqlClient

Public Class login

    Dim tau As New Form1
    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtLogin.Select()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click

        Dim connString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Wintarefa.mdf;
                                    Integrated Security=True;Connect Timeout=30"

        Dim ligacao As SqlConnection = New SqlConnection(connString)

        Try
            ligacao.Open()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        Dim cmd As SqlCommand = New SqlCommand("SELECT * FROM Alunos WHERE login = '" &
                                               txtLogin.Text & "' AND senha = '" & txtSenha.Text & "'", ligacao)

        Dim dr As SqlDataReader = cmd.ExecuteReader
        Dim encontrou As Boolean = False
        Dim strNome As String = ""
        If dr.HasRows Then
            While dr.Read
                encontrou = True
                strNome = dr("nome").ToString
            End While
        End If

        ligacao.Close()

        If encontrou = True Then
            Me.Hide()
            tau.Show()
            tau.Label1.Text = "Bem vindo " & strNome
        Else
            Dim msg As String = "Não encontrado." & vbNewLine & "Utilizador ou senha incorretos."
            Dim titulo As String = "Aviso!"
            Dim botoes = MessageBoxButtons.OK
            Dim icone = MessageBoxIcon.Exclamation
            MessageBox.Show(msg, titulo, botoes, icone)
            txtLogin.Text = ""
            txtSenha.Text = ""
        End If

    End Sub
End Class