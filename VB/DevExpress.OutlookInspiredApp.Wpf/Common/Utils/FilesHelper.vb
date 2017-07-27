Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Interop
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace DevExpress.DevAV.Common.Utils
    Public NotInheritable Class FilesHelper

        Private Sub New()
        End Sub

        #Region "CommonWinApi"
        <Flags>
        Public Enum SHGFI As Integer
            Icon = &H100
            DisplayName = &H200
            TypeName = &H400
            Attributes = &H800
            IconLocation = &H1000
            ExeType = &H2000
            SysIconIndex = &H4000
            LinkOverlay = &H8000
            Selected = &H10000
            Attr_Specified = &H20000
            LargeIcon = &H0
            SmallIcon = &H1
            OpenIcon = &H2
            ShellIconSize = &H4
            PIDL = &H8
            UseFileAttributes = &H10
            AddOverlays = &H20
            OverlayIndex = &H40
        End Enum

        <StructLayout(LayoutKind.Sequential)>
        Private Structure SHFILEINFO
            Public hIcon As IntPtr
            Public iIcon As IntPtr
            Public dwAttributes As UInteger
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst := 260)>
            Public szDisplayName As String
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst := 80)>
            Public szTypeName As String
        End Structure

        Private Class Shell32
            <DllImport("shell32.dll")>
            Public Shared Function SHGetFileInfo(ByVal pszPath As String, ByVal dwFileAttributes As UInteger, ByRef psfi As SHFILEINFO, ByVal cbSizeFileInfo As UInteger, ByVal uFlags As UInteger) As IntPtr
            End Function
            <DllImport("User32.dll")>
            Public Shared Function DestroyIcon(ByVal hIcon As IntPtr) As Integer
            End Function
        End Class
        #End Region

        Public Shared MaxAttachedFileSize As Long = 6
        Public Shared Function GetAttachedFileInfo(ByVal name As String, Optional ByVal directoryName As String = "", Optional ByVal id As Long = -1) As AttachedFileInfo
            Return New AttachedFileInfo() With {.Name = name, .DisplayName = Path.GetFileNameWithoutExtension(name), .Extension = Path.GetExtension(name), .Icon = IconToImageSourceConverter(IconFromExtension(Path.GetExtension(name))), .FullPath = Path.Combine(directoryName, name), .Id = id}
        End Function
        Public Shared Function OpenFileFromDb(ByVal name As String, ByVal content() As Byte) As String
            Dim tempFileName As String = Path.GetTempFileName()
            Dim newFileName As String = Path.Combine(Path.GetDirectoryName(tempFileName), Path.GetFileNameWithoutExtension(name) & "_" & Path.GetFileNameWithoutExtension(tempFileName) & Path.GetExtension(name))
            File.Delete(tempFileName)
            File.WriteAllBytes(newFileName, content)
            File.SetAttributes(newFileName, FileAttributes.ReadOnly)
            Process.Start(newFileName)
            Return newFileName
        End Function
        Public Shared Sub OpenFileFromDisc(ByVal fullPath As String)
            Process.Start(fullPath)
        End Sub
        Public Shared Sub DeleteTempFiles(ByRef deletedFilesPath As List(Of String))
            If deletedFilesPath Is Nothing Then
                Return
            End If
            For Each path As String In deletedFilesPath
                Try
                    File.SetAttributes(path, FileAttributes.Normal)
                    File.Delete(path)
                Catch e1 As Exception
                End Try
            Next path
            deletedFilesPath.Clear()
        End Sub
        Private Shared Function IconFromExtension(ByVal extension As String) As Icon
            Dim shFileInfo As New SHFILEINFO()
            Shell32.SHGetFileInfo(extension, &H80, shFileInfo, CUInt(Marshal.SizeOf(shFileInfo)), CUInt(SHGFI.Icon Or SHGFI.LargeIcon Or SHGFI.UseFileAttributes))
            Dim icon As Icon = DirectCast(System.Drawing.Icon.FromHandle(shFileInfo.hIcon).Clone(), Icon)
            Shell32.DestroyIcon(shFileInfo.hIcon)
            Return icon
        End Function
        Private Shared Function IconToImageSourceConverter(ByVal icon As Icon) As BitmapSource
            Return Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
        End Function
    End Class
    Public Class AttachedFileInfo
        Public Property DisplayName() As String
        Public Property Name() As String
        Public Property Extension() As String
        Public Property Icon() As ImageSource
        Public Property FullPath() As String
        Public Property Id() As Long
    End Class

End Namespace
