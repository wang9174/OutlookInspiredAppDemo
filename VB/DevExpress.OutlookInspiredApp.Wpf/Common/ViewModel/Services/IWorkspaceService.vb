Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.Text

Namespace DevExpress.DevAV.Common.ViewModel
    Public Class Workspace

        Private regions_Renamed As New Dictionary(Of String, String)()

        Public Sub AddRegion(ByVal regionId As String, ByVal regionLayout As String)
            regions_Renamed.Add(regionId, regionLayout)
        End Sub
        Public Function FindRegionLayout(ByVal regionId As String) As String
            Dim regionLayout As String = Nothing
            Return If(regions_Renamed.TryGetValue(regionId, regionLayout), regionLayout, Nothing)
        End Function
        Public ReadOnly Property Regions() As IEnumerable(Of KeyValuePair(Of String, String))
            Get
                Return regions_Renamed
            End Get
        End Property
    End Class
    Public Interface IWorkspaceService
        Function SaveWorkspace() As Workspace
        Sub RestoreWorkspace(ByVal workspace As Workspace)
    End Interface
End Namespace
