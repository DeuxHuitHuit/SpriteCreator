Imports System.Console
Imports System.IO

Imports com.deuxhuithuit.SpriteCreator

Module Main

    Private targetFolder As String = ""
    Private file As String = "sprite.png"
    Private fileFilter As String = "*.png"
    Private horizontalLimit As String = "1"

    Sub Main()
        ' Parse command line arguments
        parseArgs()

        ' Assure we have a folder
        AssureFolder()

        WriteLine("Welcome in Deux Huit Huit's SpriteCreator")
        WriteLine()
        WriteLine("Taget folder: {0}", targetFolder)
        WriteLine("File filter: {0}", fileFilter)
        WriteLine("Output file: {0}", file)
        WriteLine("Horizontal limit: {0}", horizontalLimit)
        WriteLine()

        Dim start As Date = Now
        Dim dirInfo As IO.DirectoryInfo = FileIO.FileSystem.GetDirectoryInfo(targetFolder)

        If Not IsNumeric(horizontalLimit) OrElse CType(horizontalLimit, Integer) < 1 Then
            WriteLine("ERROR: '{0}' is not a valid value. Must be a numerical value greater than 0.", horizontalLimit)
        ElseIf dirInfo IsNot Nothing AndAlso dirInfo.Exists Then
            Dim sc As New Core.SpriteCreator(targetFolder, file, fileFilter, CType(horizontalLimit, Integer))
            Dim files As List(Of String) = sc.GetAllImageInFolder

            If files.Count < 1 Then
                WriteLine("No file found. Can not continue.")
            Else
                For Each f As String In files
                    WriteLine(f)
                Next
                WriteLine()
                WriteLine("Creating sprite...")
                sc.CreateSprite()
                WriteLine("Sprite created successfully!")
            End If

        Else
            WriteLine("ERROR: Folder '{0}' does not exists. Can not continue.", dirInfo.FullName)
        End If

        WriteLine()
        WriteLine("Took {0:0.000} minutes", (Now - start).TotalMinutes)
        WriteLine()
        WriteLine("Hit <Enter> to exit...")
        ReadLine()
    End Sub

    Private Sub AssureFolder()
        If String.IsNullOrWhiteSpace(targetFolder) Then
            targetFolder = Directory.GetCurrentDirectory()
        End If
    End Sub

    Private Sub parseArgs()
        For Each s As String In My.Application.CommandLineArgs
            Select Case s

                Case "-v"

                Case Else
                    If s.StartsWith("-f:") Then
                        file = s.Remove(0, 3)
                    ElseIf s.StartsWith("-t:") Then
                        targetFolder = s.Remove(0, 3)
                    ElseIf s.StartsWith("-filter:") Then
                        fileFilter = s.Remove(0, 8)
                    ElseIf s.StartsWith("-hl:") Then
                        horizontalLimit = s.Remove(0, 4)
                    Else
                        WriteLine("Argument '{0}' not valid.", s)
                    End If
            End Select
        Next
    End Sub

End Module
