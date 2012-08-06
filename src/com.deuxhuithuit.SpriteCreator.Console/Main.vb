﻿Imports System.Console
Imports System.IO

Module Main

    Private targetFolder As String = "E:\github\SpriteCreator\test\"
    Private file As String = "sprite.png"
    Private fileFilter As String = "*.png"

    Sub Main()
        parseArgs()

        WriteLine("Welcome in Deux Huit Huit's SpriteCreator")
        WriteLine()
        WriteLine("Taget Folder: {0}", targetFolder)
        WriteLine("Output File: {0}", file)
        WriteLine()
        'WriteLine()
        'Threading.Thread.Sleep(1000)
        'Write(" -> 3 -> ")
        'Threading.Thread.Sleep(1000)
        'Write("2 -> ")
        'Threading.Thread.Sleep(1000)
        'Write("1 -> ")
        'Threading.Thread.Sleep(1000)
        'WriteLine(" GO!")
        'WriteLine()

        ' Assure we have a folder
        AssureFolder()

        Dim start As Date = Now
        Dim dirInfo As IO.DirectoryInfo = FileIO.FileSystem.GetDirectoryInfo(targetFolder)

        If dirInfo IsNot Nothing AndAlso dirInfo.Exists Then
            Dim sc As New SpriteCreator.Core.SpriteCreator(targetFolder, file, fileFilter)
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
                    Else
                        WriteLine("Argument '{0}' not valid.", s)
                    End If
            End Select
        Next
    End Sub

End Module
