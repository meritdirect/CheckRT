Imports System.IO

Module Module1
    'Checks file for row terminator. Returns 0 if error, 1 if terminator is CRLF, 2 if Terminator is LF, 3 of Terminator is CR
    Sub Main()
        Dim strFileName As String = "", intReturn As Integer = 0
        Try
            If Command.Length = 0 Then
                Environment.ExitCode = 0
                Exit Sub
            End If
            strFileName = Command()

            If Not FilenameIsOK(strFileName) Then
                Environment.ExitCode = 0
                Exit Sub
            End If
            If Not File.Exists(strFileName) Then
                Environment.ExitCode = 0
                Exit Sub
            End If

            If New FileInfo(strFileName).Length.Equals(0) Then
                Environment.ExitCode = 0
                Exit Sub
            End If


            Using sr As New StreamReader(strFileName)


                'Check for end of line characters if file ends CrLF or just LF
                sr.BaseStream.Seek(-2, SeekOrigin.End)
                Dim s1 As Integer = sr.Read() 'read the char before last
                Dim s2 As Integer = sr.Read() 'read the last char 

                If s1 = 13 And s2 = 10 Then
                    intReturn = 1
                ElseIf s2 = 13 Then
                    intReturn = 3
                Else
                    intReturn = 2
                End If
            End Using
            Environment.ExitCode = intReturn
        Catch ex As Exception
            Environment.ExitCode = 0
        End Try

    End Sub
    Public Function FilenameIsOK(ByVal fileName As String) As Boolean
        Dim file As String = Path.GetFileName(fileName)
        Dim directory As String = Path.GetDirectoryName(fileName)

        Return Not (file.Intersect(Path.GetInvalidFileNameChars()).Any() _
                    OrElse
                    directory.Intersect(Path.GetInvalidPathChars()).Any())
    End Function

End Module
