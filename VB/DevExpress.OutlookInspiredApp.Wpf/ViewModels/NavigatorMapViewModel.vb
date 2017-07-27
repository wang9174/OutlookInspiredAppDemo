Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports DevExpress.DevAV
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map
Imports System.Windows

Namespace DevExpress.DevAV.ViewModels
    Public Interface INavigatorMapViewModel
        Sub NewPushpinCreated(ByVal newPushpin As MapPushpin)
    End Interface
    Public Class NavigatorMapViewModel(Of TEntity As Class)
        Inherits MapViewModelBase
        Implements IDocumentContent, INavigatorMapViewModel

        Public Shared Function Create(ByVal displayEntity As TEntity, ByVal startingAddress As String, ByVal startingLocation As GeoPoint, ByVal destinationAddress As String, ByVal destinationLocation As GeoPoint, Optional ByVal applyDestination As Action(Of Address) = Nothing) As NavigatorMapViewModel(Of TEntity)
            Return ViewModelSource.Create(Function() New NavigatorMapViewModel(Of TEntity)(displayEntity, startingAddress, startingLocation, destinationAddress, destinationLocation, applyDestination))
        End Function
        Private Const maximumWalkingDistance As Double = 7.0

        Private linksViewModel_Renamed As LinksViewModel

        Protected Sub New(ByVal displayEntity As TEntity, ByVal startingAddress As String, ByVal startingLocation As GeoPoint, ByVal destinationAddress As String, ByVal destinationLocation As GeoPoint, ByVal applyDestination As Action(Of Address))
            Me.DisplayEntity = displayEntity
            Me.StartingAddress = startingAddress
            Me.StartingLocation = startingLocation
            Me.DestinationAddress = destinationAddress
            Me.DestinationLocation = destinationLocation
            CenterPoint = New GeoPoint((startingLocation.Latitude + destinationLocation.Latitude) / 2, (startingLocation.Longitude + destinationLocation.Longitude) / 2)
            IsWalkingAvailable = False
            Me.applyDestination = applyDestination
        End Sub

        Private privateDisplayEntity As TEntity
        Public Property DisplayEntity() As TEntity
            Get
                Return privateDisplayEntity
            End Get
            Private Set(ByVal value As TEntity)
                privateDisplayEntity = value
            End Set
        End Property
        Public Property Destination() As Address
        Public ReadOnly Property IsEditingMode() As Boolean
            Get
                Return applyDestination IsNot Nothing
            End Get
        End Property
        Public Overridable Property StartingAddress() As String
        Public Overridable Property StartingLocation() As GeoPoint
        Public Overridable Property DestinationAddress() As String
        Public Overridable Property DestinationLocation() As GeoPoint
        Protected ReadOnly Property RouteService() As IMapRouteService
            Get
                Return Me.GetRequiredService(Of IMapRouteService)()
            End Get
        End Property
        Protected ReadOnly Property MapPushpinsService() As IMapPushpinsService
            Get
                Return Me.GetRequiredService(Of IMapPushpinsService)()
            End Get
        End Property
        Public Overridable Property ActiveItinerary() As List(Of ItineraryItemViewModel)
        Public Overridable Property SelectedItineraryItem() As ItineraryItemViewModel
        Public Overridable Property CenterPoint() As GeoPoint
        Protected ReadOnly Property DocumentManagerService() As IDocumentManagerService
            Get
                Return Me.GetService(Of IDocumentManagerService)()
            End Get
        End Property
        Public Overridable Property IsWalkingAvailable() As Boolean

        Private applyDestination As Action(Of Address)

        Public Sub OnSelectedItineraryItemChanged()
            If SelectedItineraryItem IsNot Nothing Then
                CenterPoint = SelectedItineraryItem.Location
            End If
        End Sub

        Public Sub SaveAndClose()
            applyDestination(Destination)
            Close()
        End Sub
        Public Function CanSaveAndClose() As Boolean
            Return applyDestination IsNot Nothing
        End Function

        Public Sub Swap()
            applyDestination = Nothing
            Me.RaisePropertyChanged(Function(x) x.IsEditingMode)

            Dim address_Renamed As String = StartingAddress
            StartingAddress = DestinationAddress
            DestinationAddress = address_Renamed
            Dim location As GeoPoint = StartingLocation
            StartingLocation = DestinationLocation
            DestinationLocation = location
            CalculateRouteDriving()
        End Sub

        Public Sub CalculateRouteDriving()
            CalculateRoute(BingTravelMode.Driving)
        End Sub

        Public Sub CalculateRouteWalking()
            CalculateRoute(BingTravelMode.Walking)
        End Sub

        Private isBusy As Boolean = False
        Private Sub CalculateRoute(ByVal mode As BingTravelMode)
            If isBusy Then
                Return
            End If
            isBusy = True
            Dim waypoints = {
                New RouteWaypoint(StartingAddress, StartingLocation),
                New RouteWaypoint(DestinationAddress, DestinationLocation)
            }
            Dim unit = DistanceMeasureUnit.Mile
            Dim optimization = BingRouteOptimization.MinimizeTime
            RouteService.CalculateRouteAsync(waypoints, unit, optimization, mode).ContinueWith(Sub(t)
                If t.Result.ResultCode = RequestResultCode.Success AndAlso t.Result.RouteResults.Count > 0 Then
                    Dim route As BingRouteResult = t.Result.RouteResults.First()
                    If route.Legs.Count > 0 Then
                        Dim leg As BingRouteLeg = route.Legs.First()
                        IsWalkingAvailable = If(leg.Distance > maximumWalkingDistance, False, True)
                        Dim startLocation As GeoPoint = leg.Itinerary.First().Location
                        Dim endLocation As GeoPoint = leg.Itinerary.Last().Location
                        CenterPoint = New GeoPoint((startLocation.Latitude + endLocation.Latitude) / 2, (startLocation.Longitude + endLocation.Longitude) / 2)
                        ActiveItinerary = leg.Itinerary.Select(Function(item) New ItineraryItemViewModel(item)).ToList()
                    End If
                End If
                isBusy = False
            End Sub, TaskScheduler.FromCurrentSynchronizationContext())
        End Sub

        Public Sub OnLoaded()
            CalculateRouteDriving()
        End Sub

        Private Sub INavigatorMapViewModel_NewPushpinCreated(ByVal newPushpin As MapPushpin) Implements INavigatorMapViewModel.NewPushpinCreated
            AddHandler newPushpin.MouseLeftButtonDown, Sub(s, e)
                Dim pushpin As MapPushpin = TryCast(s, MapPushpin)
                If (pushpin IsNot Nothing) AndAlso (pushpin.State = MapPushpinState.Normal) Then
                    Dim locationInformation As LocationInformation = TryCast(pushpin.Information, LocationInformation)
                    DestinationAddress = locationInformation.Address.FormattedAddress
                    DestinationLocation = CType(pushpin.Location, GeoPoint)
                    pushpin.Text = "A"
                    Dim rx As New Regex("(.*?), (.*?), (.*?) (.*)")
                    Dim match = rx.Match(locationInformation.Address.FormattedAddress)
                    Dim streetLine As String = match.Groups(1).ToString().Trim()
                    Dim city As String = match.Groups(2).ToString().Trim()
                    Dim state As String = match.Groups(3).ToString().Trim()
                    Dim zipcode As String = match.Groups(4).ToString().Trim()
                    Dim stateEnum As StateEnum = StateEnum.WY
                    System.Enum.TryParse(state, stateEnum)
                    Destination = New Address With {.City = city, .Line = streetLine, .State = stateEnum, .ZipCode = zipcode, .Latitude = DestinationLocation.Latitude, .Longitude = DestinationLocation.Longitude}
                    MapPushpinsService.Clear()
                    CalculateRouteDriving()
                End If
            End Sub
        End Sub
        Public ReadOnly Property LinksViewModel() As LinksViewModel
            Get
                If linksViewModel_Renamed Is Nothing Then
                    linksViewModel_Renamed = DevExpress.DevAV.ViewModels.LinksViewModel.Create()
                End If
                Return linksViewModel_Renamed
            End Get
        End Property
        <Command(UseCommandManager := False)>
        Public Sub Close()
            If DocumentManagerService Is Nothing Then
                Return
            End If
            Dim document As IDocument = DocumentManagerService.FindDocument(Me)
            If document IsNot Nothing Then
                document.Close()
            End If
        End Sub
        Private Sub IDocumentContent_OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
        End Sub
        Private Sub IDocumentContent_OnDestroy() Implements IDocumentContent.OnDestroy
        End Sub
        Private Property IDocumentContent_DocumentOwner() As IDocumentOwner Implements IDocumentContent.DocumentOwner
        Private ReadOnly Property IDocumentContent_Title() As Object Implements IDocumentContent.Title
            Get
                Return "DevAV - " & DestinationAddress
            End Get
        End Property
    End Class
    Public Class ItineraryItemViewModel
        Private item As BingItineraryItem
        Public Sub New(ByVal item As BingItineraryItem)
            Me.item = item
        End Sub
        Private Function RemoveTags(ByVal str As String) As String
            Return (New Regex("<.*?>")).Replace(str, "")
        End Function
        Public ReadOnly Property ManeuverInstruction() As String
            Get
                Return RemoveTags(item.ManeuverInstruction)
            End Get
        End Property
        Public ReadOnly Property Distance() As Double
            Get
                Return item.Distance
            End Get
        End Property
        Public ReadOnly Property Location() As GeoPoint
            Get
                Return item.Location
            End Get
        End Property
        Public ReadOnly Property Maneuver() As BingManeuverType
            Get
                Return item.Maneuver
            End Get
        End Property
    End Class
End Namespace
