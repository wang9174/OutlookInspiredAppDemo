Imports DevExpress.Mvvm.UI
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Xpf.Map
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks

Namespace DevExpress.DevAV.ViewModels
    Public Interface IMapRouteService
        Function CalculateRouteAsync(ByVal waypoints As IEnumerable(Of RouteWaypoint), ByVal unit As DistanceMeasureUnit, ByVal optimization As BingRouteOptimization, ByVal mode As BingTravelMode) As Task(Of RouteCalculationResult)
    End Interface
End Namespace

Namespace DevExpress.DevAV
    Public Class MapRouteService
        Inherits ServiceBase
        Implements IMapRouteService

        Private ReadOnly Property Layer() As InformationLayer
            Get
                Return CType(AssociatedObject, InformationLayer)
            End Get
        End Property
        Private ReadOnly Property Provider() As BingRouteDataProvider
            Get
                Return CType(Layer.DataProvider, BingRouteDataProvider)
            End Get
        End Property
        Private taskSource As TaskCompletionSource(Of RouteCalculationResult)
        Public Function CalculateRouteAsync(ByVal waypoints As IEnumerable(Of RouteWaypoint), ByVal unit As DistanceMeasureUnit, ByVal optimization As BingRouteOptimization, ByVal mode As BingTravelMode) As Task(Of RouteCalculationResult) Implements IMapRouteService.CalculateRouteAsync
            taskSource = New TaskCompletionSource(Of RouteCalculationResult)()
            Provider.RouteOptions = New BingRouteOptions With {.Mode = mode, .DistanceUnit = unit, .RouteOptimization = optimization}
            Provider.CalculateRoute(waypoints.ToList())
            AddHandler Provider.RouteCalculated, AddressOf Provider_RouteCalculated
            Return taskSource.Task
        End Function
        Private Sub Provider_RouteCalculated(ByVal sender As Object, ByVal e As BingRouteCalculatedEventArgs)
            RemoveHandler Provider.RouteCalculated, AddressOf Provider_RouteCalculated
            If e.Cancelled Then
                taskSource.SetCanceled()
            ElseIf e.Error IsNot Nothing Then
                taskSource.SetException(e.Error)
            Else
                taskSource.SetResult(e.CalculationResult)
            End If

        End Sub
    End Class
End Namespace
