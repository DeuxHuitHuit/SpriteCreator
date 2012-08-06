Imports Microsoft.VisualBasic.FileIO
Imports System.Collections.ObjectModel

Imports System.Drawing
Imports System.Drawing.Imaging

Public Class SpriteCreator

    Private _targetFolder As String
    Private _file As String
    Private _fileFilter As String
    Private _fileList As List(Of String)

    Public Sub New(targetFolder As String, file As String, fileFilter As String)
        _targetFolder = targetFolder
        _file = file
        _fileFilter = fileFilter
    End Sub

    Public Function GetAllImageInFolder() As List(Of String)
        If _fileList Is Nothing Then
            Dim roc As ReadOnlyCollection(Of String) = FileSystem.GetFiles(_targetFolder, FileIO.SearchOption.SearchTopLevelOnly, _fileFilter)
            _fileList = roc.ToList
        End If
        Return _fileList
    End Function

    Public Function CreateSprite() As Boolean
        Dim refImage As New Bitmap(_fileList(0), True)

        Dim height = refImage.Height
        Dim width = refImage.Width
        Dim sprite As New Bitmap(width, height * _fileList.Count, refImage.PixelFormat)
        Dim g As Graphics = Graphics.FromImage(sprite)
        Dim counter As Integer = 0

        ' Dispose our ref image
        refImage.Dispose()

        For Each f As String In _fileList
            Dim bm = New Bitmap(_fileList(counter), True)
            g.DrawImage(bm, 0, counter * height, width, height)
            bm.Dispose()
            counter += 1
        Next

        ' Dispose graphics
        g.Dispose()

        ' Save the image
        sprite.Save(_targetFolder & _file)

        Return True
    End Function

End Class
