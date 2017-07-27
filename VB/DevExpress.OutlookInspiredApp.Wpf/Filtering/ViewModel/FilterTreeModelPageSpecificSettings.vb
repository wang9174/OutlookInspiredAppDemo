Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Configuration
Imports System.Linq.Expressions

Namespace DevExpress.DevAV.ViewModels
    Public Class FilterTreeModelPageSpecificSettings(Of TSettings As ApplicationSettingsBase)
        Implements IFilterTreeModelPageSpecificSettings


        Private ReadOnly staticFiltersTitle_Renamed As String

        Private ReadOnly customFiltersTitle_Renamed As String
        Private ReadOnly settings As TSettings
        Private ReadOnly customFiltersProperty As PropertyDescriptor
        Private ReadOnly staticFiltersProperty As PropertyDescriptor

        Private ReadOnly hiddenFilterProperties_Renamed As IEnumerable(Of String)

        Private ReadOnly additionalFilterProperties_Renamed As IEnumerable(Of String)

        Public Sub New(ByVal settings As TSettings, ByVal staticFiltersTitle As String, ByVal getStaticFiltersExpression As Expression(Of Func(Of TSettings, FilterInfoList)), ByVal getCustomFiltersExpression As Expression(Of Func(Of TSettings, FilterInfoList)), Optional ByVal hiddenFilterProperties As IEnumerable(Of String) = Nothing, Optional ByVal additionalFilterProperties As IEnumerable(Of String) = Nothing, Optional ByVal customFiltersTitle As String = "Custom Filters")
            Me.settings = settings
            Me.staticFiltersTitle_Renamed = staticFiltersTitle
            Me.customFiltersTitle_Renamed = customFiltersTitle
            staticFiltersProperty = GetProperty(getStaticFiltersExpression)
            customFiltersProperty = GetProperty(getCustomFiltersExpression)
            Me.hiddenFilterProperties_Renamed = hiddenFilterProperties
            Me.additionalFilterProperties_Renamed = additionalFilterProperties
        End Sub
        Private Property IFilterTreeModelPageSpecificSettings_CustomFilters() As FilterInfoList Implements IFilterTreeModelPageSpecificSettings.CustomFilters
            Get
                Return GetFilters(customFiltersProperty)
            End Get
            Set(ByVal value As FilterInfoList)
                SetFilters(customFiltersProperty, value)
            End Set
        End Property
        Private Property IFilterTreeModelPageSpecificSettings_StaticFilters() As FilterInfoList Implements IFilterTreeModelPageSpecificSettings.StaticFilters
            Get
                Return GetFilters(staticFiltersProperty)
            End Get
            Set(ByVal value As FilterInfoList)
                SetFilters(staticFiltersProperty, value)
            End Set
        End Property
        Private ReadOnly Property IFilterTreeModelPageSpecificSettings_StaticFiltersTitle() As String Implements IFilterTreeModelPageSpecificSettings.StaticFiltersTitle
            Get
                Return staticFiltersTitle_Renamed
            End Get
        End Property
        Private ReadOnly Property IFilterTreeModelPageSpecificSettings_CustomFiltersTitle() As String Implements IFilterTreeModelPageSpecificSettings.CustomFiltersTitle
            Get
                Return customFiltersTitle_Renamed
            End Get
        End Property
        Private ReadOnly Property IFilterTreeModelPageSpecificSettings_HiddenFilterProperties() As IEnumerable(Of String) Implements IFilterTreeModelPageSpecificSettings.HiddenFilterProperties
            Get
                Return hiddenFilterProperties_Renamed
            End Get
        End Property
        Private ReadOnly Property IFilterTreeModelPageSpecificSettings_AdditionalFilterProperties() As IEnumerable(Of String) Implements IFilterTreeModelPageSpecificSettings.AdditionalFilterProperties
            Get
                Return additionalFilterProperties_Renamed
            End Get
        End Property
        Private Sub IFilterTreeModelPageSpecificSettings_SaveSettings() Implements IFilterTreeModelPageSpecificSettings.SaveSettings
            settings.Save()
        End Sub

        Private Function GetProperty(ByVal expression As Expression(Of Func(Of TSettings, FilterInfoList))) As PropertyDescriptor
            If expression IsNot Nothing Then
                Return TypeDescriptor.GetProperties(settings)(GetPropertyName(expression))
            End If
            Return Nothing
        End Function
        Private Function GetFilters(ByVal [property] As PropertyDescriptor) As FilterInfoList
            Return If([property] IsNot Nothing, DirectCast([property].GetValue(settings), FilterInfoList), Nothing)
        End Function
        Private Sub SetFilters(ByVal [property] As PropertyDescriptor, ByVal value As FilterInfoList)
            If [property] IsNot Nothing Then
                [property].SetValue(settings, value)
            End If
        End Sub
        Private Shared Function GetPropertyName(ByVal expression As Expression(Of Func(Of TSettings, FilterInfoList))) As String
            Dim memberExpression As MemberExpression = TryCast(expression.Body, MemberExpression)
            If memberExpression Is Nothing Then
                Throw New ArgumentException("expression")
            End If
            Return memberExpression.Member.Name
        End Function
    End Class
End Namespace
