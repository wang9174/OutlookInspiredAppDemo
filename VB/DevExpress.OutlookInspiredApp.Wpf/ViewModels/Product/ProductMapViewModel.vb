Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm

Namespace DevExpress.DevAV.ViewModels
    Public Class ProductMapViewModel
        Inherits SingleObjectViewModel(Of Product, Long, IDevAVDbUnitOfWork)


        Private citiesMapViewModel_Renamed As CitiesMapViewModel

        Private statisticsViewModel_Renamed As ProductStatisticsViewModel

        Public Sub New()
            MyBase.New(UnitOfWorkSource.GetUnitOfWorkFactory(), Function(x) x.Products)
        End Sub
        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            RemoveHandler StatisticsViewModel.FilterTypeChanged, AddressOf StatisticsViewModel_FilterTypeChanged
            Dim addresses = New HashSet(Of Address)(GetSalesStores().Select(Function(cs) cs.Address))
            Me.CitiesMapViewModel.Cities = CreateCities(addresses)
            StatisticsViewModel.EntityId = PrimaryKey
            StatisticsViewModel.SelectedAddress = Me.CitiesMapViewModel.Cities.FirstOrDefault()
            AddHandler StatisticsViewModel.FilterTypeChanged, AddressOf StatisticsViewModel_FilterTypeChanged
        End Sub
        Private Sub StatisticsViewModel_FilterTypeChanged(ByVal sender As Object, ByVal e As PeriodEventArgs)
            Dim addresses = New HashSet(Of Address)(GetSalesStores().Select(Function(cs) cs.Address))
            Me.CitiesMapViewModel.Cities = CreateCities(addresses, e.Period)
        End Sub
        Public Function GetSalesStores(Optional ByVal period As Period = Period.Lifetime) As IEnumerable(Of CustomerStore)
            Return QueriesHelper.GetSalesStoresForPeriod(UnitOfWork.Orders, period)
        End Function
        Private Function CreateCities(ByVal addresses As HashSet(Of Address), Optional ByVal period As Period = Period.Lifetime) As List(Of CityViewModel)
            Dim newCities = New List(Of CityViewModel)()
            For Each city In addresses.Select(Function(a) CityViewModel.Create(a, UnitOfWork))
                city.Sales = GetSalesByCity(city.Address.City, period).GroupBy(Function(mi) mi.Customer).Select(Function(gr) New SalesViewModel(gr.Key.Name, gr.CustomSum(Function(mi) mi.Total)))
                Dim address As String = city.Address.City
                If Not newCities.Any(Function(c) c.Address.City = address) Then
                    newCities.Add(city)
                End If
            Next city
            Return newCities
        End Function
        Private Function GetSalesByCity(ByVal city As String, Optional ByVal period As Period = Period.Lifetime) As IEnumerable(Of MapItem)
            Return QueriesHelper.GetSaleMapItemsByCity(UnitOfWork.OrderItems, Entity.Id, city, period)
        End Function
        Public ReadOnly Property CitiesMapViewModel() As CitiesMapViewModel
            Get
                If citiesMapViewModel_Renamed Is Nothing Then
                    citiesMapViewModel_Renamed = DevExpress.DevAV.ViewModels.CitiesMapViewModel.Create()
                End If
                Return citiesMapViewModel_Renamed
            End Get
        End Property
        Public ReadOnly Property StatisticsViewModel() As ProductStatisticsViewModel
            Get
                If statisticsViewModel_Renamed Is Nothing Then
                    statisticsViewModel_Renamed = ProductStatisticsViewModel.Create()
                End If
                Return statisticsViewModel_Renamed
            End Get
        End Property
        Public Overrides Sub Delete()

            Dim messageBoxService_Renamed As IMessageBoxService = citiesMapViewModel_Renamed.GetRequiredService(Of IMessageBoxService)()
            messageBoxService_Renamed.ShowMessage("To ensure data integrity, the Products module doesn't allow records to be deleted. Record deletion is supported by the Employees module.", "Delete Product", MessageButton.OK)
        End Sub
        Protected Overrides Function GetTitle() As String
            Return "Product Sales Map - DevAV"
        End Function
    End Class
End Namespace
