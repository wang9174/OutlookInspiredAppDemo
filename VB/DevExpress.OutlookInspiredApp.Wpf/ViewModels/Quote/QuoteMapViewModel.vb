Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports DevExpress.DevAV
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common.DataModel

Namespace DevExpress.DevAV.ViewModels
    Public Class QuoteMapViewModel
        Inherits CollectionViewModel(Of Quote, Long, IDevAVDbUnitOfWork)

        Private privateQuotes As QuoteCollectionViewModel
        Public Property Quotes() As QuoteCollectionViewModel
            Get
                Return privateQuotes
            End Get
            Private Set(ByVal value As QuoteCollectionViewModel)
                privateQuotes = value
            End Set
        End Property
        Public Overridable Property GrouppedMapItems() As IEnumerable(Of Object)
        Public Overridable Property Stage() As Stage
        Public Overridable Property SelectedItem() As Object
        Public Overridable Property IsHighStage() As Boolean
        Public Overridable Property IsMediumStage() As Boolean
        Public Overridable Property IsLowStage() As Boolean
        Public Overridable Property IsUnlikelyStage() As Boolean

        Public ReadOnly Property LinksViewModel() As LinksViewModel
            Get
                If linksViewModel_Renamed Is Nothing Then
                    linksViewModel_Renamed = DevExpress.DevAV.ViewModels.LinksViewModel.Create()
                End If
                Return linksViewModel_Renamed
            End Get
        End Property

        Public Sub New()
            MyBase.New(UnitOfWorkSource.GetUnitOfWorkFactory(), Function(x) x.Quotes)
            IsHighStage = True
            UpdateMapItems()
        End Sub
        Protected Overrides Sub OnFilterExpressionChanged()
            MyBase.OnFilterExpressionChanged()
            UpdateMapItems()
        End Sub
        Protected Overridable Sub OnStageChanged()
            IsHighStage = Object.Equals(Stage, Stage.High)
            IsMediumStage = Object.Equals(Stage, Stage.Medium)
            IsLowStage = Object.Equals(Stage, Stage.Low)
            IsUnlikelyStage = Object.Equals(Stage, Stage.Unlikely)
            UpdateMapItems()
        End Sub
        Protected Overridable Sub OnIsHighStageChanged()
            If IsHighStage Then
                Stage = Stage.High
            End If
        End Sub
        Protected Overridable Sub OnIsMediumStageChanged()
            If IsMediumStage Then
                Stage = Stage.Medium
            End If
        End Sub
        Protected Overridable Sub OnIsLowStageChanged()
            If IsLowStage Then
                Stage = Stage.Low
            End If
        End Sub
        Protected Overridable Sub OnIsUnlikelyStageChanged()
            If IsUnlikelyStage Then
                Stage = Stage.Unlikely
            End If
        End Sub

        Private Sub UpdateMapItems()
            Dim items = GetOpportunities(Stage, FilterExpression).GroupBy(Function(item) item.Address, New AddressComparer())
            If items.Count() > 0 Then
                Dim minValue As Decimal = items.Min(Function(group) group.CustomSum(Function(quote) quote.Value))
                Dim maxValue As Decimal = items.Max(Function(group) group.CustomSum(Function(quote) quote.Value))
                Dim dif As Decimal = maxValue - minValue
                If dif < CDec(0.01) Then
                    dif = 1
                End If
                GrouppedMapItems = items.Select(Function(group) New With {Key .Address = group.Key, Key .Total = group.CustomSum(Function(item) item.Value), Key .AbsSize = CDbl((group.CustomSum(Function(item) item.Value) - minValue) / dif)})
            Else
                GrouppedMapItems = Enumerable.Empty(Of Object)()
            End If
            SelectedItem = GrouppedMapItems.LastOrDefault()
        End Sub
        Public Function GetOpportunities(ByVal stage As Stage, Optional ByVal filterExpression As Expression(Of Func(Of Quote, Boolean)) = Nothing) As IList(Of QuoteMapItem)
            Dim unitOfWork = CreateUnitOfWork()

            Dim quotes_Renamed = unitOfWork.Quotes.GetFilteredEntities(filterExpression).ActualQuotes()
            Dim customers = unitOfWork.Customers
            Return QueriesHelper.GetOpportunities(quotes_Renamed, customers, stage).ToList()
        End Function

        Private linksViewModel_Renamed As LinksViewModel
    End Class
    Friend Class AddressComparer
        Implements IEqualityComparer(Of Address)

        Public Overloads Function Equals(ByVal x As Address, ByVal y As Address) As Boolean Implements IEqualityComparer(Of Address).Equals
            Return x.State.Equals(y.State) AndAlso x.City.Equals(y.City)
        End Function

        Public Overloads Function GetHashCode(ByVal obj As Address) As Integer Implements IEqualityComparer(Of Address).GetHashCode
            Return obj.State.GetHashCode() Xor obj.City.GetHashCode()
        End Function
    End Class
End Namespace
