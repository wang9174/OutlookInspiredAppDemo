Imports System.Windows
Imports DevExpress.Xpf.NavBar

Namespace DevExpress.DevAV.Controls
    Public Class AdvancedNavBarGroup
        Inherits NavBarGroup

        Public Shared ReadOnly AdvancedDisplayTextProperty As DependencyProperty = DependencyProperty.Register("AdvancedDisplayText", GetType(String), GetType(AdvancedNavBarGroup), New PropertyMetadata(Nothing))
        Public Property AdvancedDisplayText() As String
            Get
                Return CStr(GetValue(AdvancedDisplayTextProperty))
            End Get
            Set(ByVal value As String)
                SetValue(AdvancedDisplayTextProperty, value)
            End Set
        End Property
    End Class
End Namespace
