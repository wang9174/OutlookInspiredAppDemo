Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.RichEdit
Imports System.Collections
Imports System.IO
Imports DevExpress.XtraRichEdit

Namespace DevExpress.DevAV
    Public Class RichEditControlMailMergeBehavior
        Inherits Behavior(Of RichEditControl)

        Public Property ActiveObject() As Object
            Get
                Return DirectCast(GetValue(ActiveObjectProperty), Object)
            End Get
            Set(ByVal value As Object)
                SetValue(ActiveObjectProperty, value)
            End Set
        End Property
        Public Shared ReadOnly ActiveObjectProperty As DependencyProperty = DependencyProperty.Register("ActiveObject", GetType(Object), GetType(RichEditControlMailMergeBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, RichEditControlMailMergeBehavior).UpdateActiveRecord()))

        Public Property DocumentTemplate() As Stream
            Get
                Return CType(GetValue(DocumentTemplateProperty), Stream)
            End Get
            Set(ByVal value As Stream)
                SetValue(DocumentTemplateProperty, value)
            End Set
        End Property
        Public Shared ReadOnly DocumentTemplateProperty As DependencyProperty = DependencyProperty.Register("DocumentTemplate", GetType(Stream), GetType(RichEditControlMailMergeBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, RichEditControlMailMergeBehavior).UpdateDocumentTemplate()))

        Public Property DataSource() As IEnumerable
            Get
                Return DirectCast(GetValue(DataSourceProperty), IEnumerable)
            End Get
            Set(ByVal value As IEnumerable)
                SetValue(DataSourceProperty, value)
            End Set
        End Property
        Public Shared ReadOnly DataSourceProperty As DependencyProperty = DependencyProperty.Register("DataSource", GetType(IEnumerable), GetType(RichEditControlMailMergeBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, RichEditControlMailMergeBehavior).UpdateDataSource()))


        Public Property RemoveFields() As IEnumerable(Of String)
            Get
                Return DirectCast(GetValue(RemoveFieldsProperty), IEnumerable(Of String))
            End Get
            Set(ByVal value As IEnumerable(Of String))
                SetValue(RemoveFieldsProperty, value)
            End Set
        End Property
        Public Shared ReadOnly RemoveFieldsProperty As DependencyProperty = DependencyProperty.Register("RemoveFields", GetType(IEnumerable(Of String)), GetType(RichEditControlMailMergeBehavior), New PropertyMetadata(Nothing))

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AssociatedObject.ApplyTemplate()
            AssociatedObject.Options.MailMerge.ActiveRecord = -1

            AssociatedObject.Options.MailMerge.ViewMergedData = True
            AddHandler AssociatedObject.CustomizeMergeFields, AddressOf CustomizeMergeFields
            AddHandler AssociatedObject.ActiveRecordChanged, AddressOf ActiveRecordChanged
            UpdateDataSource()
        End Sub

        Private Sub ActiveRecordChanged(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.CustomizeMergeFields, AddressOf CustomizeMergeFields
            RemoveHandler AssociatedObject.ActiveRecordChanged, AddressOf ActiveRecordChanged
        End Sub

        Private Sub CustomizeMergeFields(ByVal sender As Object, ByVal e As XtraRichEdit.CustomizeMergeFieldsEventArgs)
            If RemoveFields IsNot Nothing Then
                e.MergeFieldsNames = e.MergeFieldsNames.Where(Function(fn) (Not RemoveFields.Any(Function(x) x = fn.Name))).ToArray()
            End If
        End Sub

        Private Sub UpdateDataSource()
            If AssociatedObject IsNot Nothing Then
                AssociatedObject.ApplyTemplate()
                AssociatedObject.Options.MailMerge.DataSource = DataSource
                AssociatedObject.Document.Fields.Update()
            End If
            UpdateActiveRecord()
        End Sub

        Private Sub UpdateDocumentTemplate()
            If AssociatedObject IsNot Nothing Then
                AssociatedObject.ApplyTemplate()
                Dim index As Integer = AssociatedObject.Options.MailMerge.ActiveRecord
                AssociatedObject.LoadDocumentTemplate(DocumentTemplate, DocumentFormat.Rtf)
                AssociatedObject.Options.MailMerge.ActiveRecord = index
                AssociatedObject.Document.Fields.Update()
            End If
        End Sub

        Private Sub UpdateActiveRecord()
            If AssociatedObject IsNot Nothing AndAlso DataSource IsNot Nothing Then
                Dim index = DataSource.Cast(Of Object)().Select(Function(x, i) New With {Key .item = x, Key .index = i}).FirstOrDefault(Function(x) x.item Is ActiveObject)
                AssociatedObject.Options.MailMerge.ActiveRecord = If(index IsNot Nothing, index.index, -1)
            End If
        End Sub
    End Class
End Namespace
