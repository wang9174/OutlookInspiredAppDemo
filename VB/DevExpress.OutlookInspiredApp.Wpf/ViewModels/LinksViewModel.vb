Imports DevExpress.Xpf.Core
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Public Class LinksViewModel
        Public Shared Function Create() As LinksViewModel
            Return ViewModelSource.Create(Function() New LinksViewModel())
        End Function
        Protected Sub New()
        End Sub

        Public Sub GettingStarted()
            DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_GetStarted_WPF.aspx")
        End Sub
        Public Sub GetFreeSupport()
            DocumentPresenter.OpenLink(AssemblyInfo.DXLinkGetSupport)
        End Sub
        Public Sub BuyNow()
            DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_Subscriptions_Buy.aspx")
        End Sub
        Public Sub UniversalSubscription()
            DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_UniversalSubscription.aspx")
        End Sub
        Public Sub About()
            DevExpress.Xpf.About.ShowAbout(DevExpress.Utils.About.ProductKind.DXperienceWPF)
        End Sub
    End Class
End Namespace
