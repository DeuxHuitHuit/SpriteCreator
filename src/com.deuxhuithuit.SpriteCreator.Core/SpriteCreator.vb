Imports Microsoft.VisualBasic.FileIO

Imports System.Drawing
Imports System.Drawing.Imaging

Public Class SpriteCreator

    Private _targetFolder As String
    Private _file As String
    Private _fileFilter As String
    Private _fileList As List(Of String)
    Private _filePath As String
    Private _horizontalLimit As Integer

    Public Sub New(targetFolder As String, file As String, fileFilter As String, horizontalLimit As Integer)
        _targetFolder = targetFolder
        If Not _targetFolder.EndsWith("\") Then
            _targetFolder &= "\"
        End If

        _file = file
        _fileFilter = fileFilter
        _filePath = _targetFolder & _file
        _horizontalLimit = horizontalLimit
    End Sub

    Public Function GetAllImageInFolder() As List(Of String)
        If _fileList Is Nothing Then
            CleanUp()
            _fileList = FileSystem.GetFiles(_targetFolder, FileIO.SearchOption.SearchTopLevelOnly, _fileFilter).ToList
        End If
        Return _fileList
    End Function

    Public Sub CleanUp()
        If FileIO.FileSystem.FileExists(_filePath) Then
            FileIO.FileSystem.DeleteFile(_filePath)
        End If
    End Sub

    Public Function CreateSprite() As Boolean
        ' Get info from the first image in list
        Dim refImage As New Bitmap(GetAllImageInFolder()(0), True)
        Dim height = refImage.Height
        Dim width = refImage.Width

        ' Create the new sprite image
        Dim sprite As New Bitmap(width * _horizontalLimit, CType(height * Math.Ceiling(_fileList.Count / _horizontalLimit),Integer), refImage.PixelFormat)
        Dim g As Graphics = Graphics.FromImage(sprite)
        Dim counter As Integer = 0
        Dim x As Integer = 0
        Dim y As Integer = 0

        ' Dispose our ref image
        refImage.Dispose()

        ' Draw each image into the sprite
        For Each f As String In _fileList
            Dim bm = New Bitmap(_fileList(counter), True)
            g.DrawImage(bm, x * width, y * height, width, height)
            bm.Dispose()
            counter += 1
            x += 1

            If x = _horizontalLimit Then
                x = 0
                y += 1
            End If
        Next

        ' Dispose graphics
        g.Dispose()

        ' Save the image on disk
        sprite.Save(_filePath)

        ' Dispose the ressources
        sprite.Dispose()

        Return True
    End Function

End Class
