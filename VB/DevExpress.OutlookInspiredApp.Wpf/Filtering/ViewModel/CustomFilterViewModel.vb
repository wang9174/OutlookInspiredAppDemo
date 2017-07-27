Imports System
Imports DevExpress.Mvvm.POCO
Imports System.Collections.Generic
Imports DevExpress.Data.Filtering

Namespace DevExpress.DevAV
    Public Class CustomFilterViewModel
        Public Shared Function Create(ByVal entityType As Type, ByVal hiddenProperties As IEnumerable(Of String), ByVal additionalProperties As IEnumerable(Of String)) As CustomFilterViewModel
            Return ViewModelSource.Create(Function() New CustomFilterViewModel(entityType, hiddenProperties, additionalProperties))
        End Function

        Protected Sub New(ByVal entityType As Type, ByVal hiddenProperties As IEnumerable(Of String), ByVal additionalProperties As IEnumerable(Of String))
            Me.EntityType = entityType
            Me.HiddenProperties = hiddenProperties
            Me.AdditionalProperties = additionalProperties
        End Sub

        Private privateEntityType As Type
        Public Property EntityType() As Type
            Get
                Return privateEntityType
            End Get
            Private Set(ByVal value As Type)
                privateEntityType = value
            End Set
        End Property
        Private privateHiddenProperties As IEnumerable(Of String)
        Public Property HiddenProperties() As IEnumerable(Of String)
            Get
                Return privateHiddenProperties
            End Get
            Private Set(ByVal value As IEnumerable(Of String))
                privateHiddenProperties = value
            End Set
        End Property
        Private privateAdditionalProperties As IEnumerable(Of String)
        Public Property AdditionalProperties() As IEnumerable(Of String)
            Get
                Return privateAdditionalProperties
            End Get
            Private Set(ByVal value As IEnumerable(Of String))
                privateAdditionalProperties = value
            End Set
        End Property
        Public Overridable Property Save() As Boolean
        Public Overridable Property FilterCriteria() As CriteriaOperator
        Public Overridable Property FilterName() As String
    End Class
End Namespace
