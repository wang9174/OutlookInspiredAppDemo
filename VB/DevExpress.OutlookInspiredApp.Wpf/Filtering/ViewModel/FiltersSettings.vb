Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System

Namespace DevExpress.DevAV.ViewModels
    Friend NotInheritable Class FiltersSettings

        Private Sub New()
        End Sub

        Public Shared Function GetEmployeesFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of Employee, Long)
            Return FilterTreeViewModel(Of Employee, Long).Create(New FilterTreeModelPageSpecificSettings(Of My.Settings)(My.Settings.Default, "Status", Function(x) x.EmployeesStaticFilters, Function(x) x.EmployeesCustomFilters, { BindableBase.GetPropertyName(Function() (New Employee()).FullNameBindable), BindableBase.GetPropertyName(Function() (New Employee()).Id), BindableBase.GetPropertyName(Function() (New Employee()).Picture), BindableBase.GetPropertyName(Function() (New Employee()).PictureId)}, { BindableBase.GetPropertyName(Function() (New Employee()).Address) & "." & BindableBase.GetPropertyName(Function() (New Address()).City), BindableBase.GetPropertyName(Function() (New Employee()).Address) & "." & BindableBase.GetPropertyName(Function() (New Address()).State), BindableBase.GetPropertyName(Function() (New Employee()).Address) & "." & BindableBase.GetPropertyName(Function() (New Address()).ZipCode)}), CreateUnitOfWork().Employees, New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of Employee, Long))).SetParentViewModel(parentViewModel)
        End Function
        Public Shared Function GetTasksFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of EmployeeTask, Long)
            Return FilterTreeViewModel(Of EmployeeTask, Long).Create(New FilterTreeModelPageSpecificSettings(Of My.Settings)(My.Settings.Default, "Tasks", Function(x) x.TasksStaticFilters, Function(x) x.TasksCustomFilters, Nothing, Nothing, "By Employee"), CreateUnitOfWork().Tasks, New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of EmployeeTask, Long))).SetParentViewModel(parentViewModel)
        End Function
        Public Shared Function GetCustomersFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of Customer, Long)
            Return FilterTreeViewModel(Of Customer, Long).Create(New FilterTreeModelPageSpecificSettings(Of My.Settings)(My.Settings.Default, "Favorites", Function(x) x.CustomersStaticFilters, Function(x) x.CustomersCustomFilters, { BindableBase.GetPropertyName(Function() (New Customer()).Id)}, { BindableBase.GetPropertyName(Function() (New Customer()).BillingAddress) & "." & BindableBase.GetPropertyName(Function() (New Address()).City), BindableBase.GetPropertyName(Function() (New Customer()).BillingAddress) & "." & BindableBase.GetPropertyName(Function() (New Address()).State), BindableBase.GetPropertyName(Function() (New Customer()).BillingAddress) & "." & BindableBase.GetPropertyName(Function() (New Address()).ZipCode)}), CreateUnitOfWork().Customers, New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of Customer, Long))).SetParentViewModel(parentViewModel)
        End Function
        Public Shared Function GetProductsFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of Product, Long)
            Return FilterTreeViewModel(Of Product, Long).Create(New FilterTreeModelPageSpecificSettings(Of My.Settings)(My.Settings.Default, "Category", Function(x) x.ProductsStaticFilters, Function(x) x.ProductsCustomFilters, Nothing, { BindableBase.GetPropertyName(Function() (New Product()).Id), BindableBase.GetPropertyName(Function() (New Product()).EngineerId), BindableBase.GetPropertyName(Function() (New Product()).PrimaryImageId), BindableBase.GetPropertyName(Function() (New Product()).SupportId), BindableBase.GetPropertyName(Function() (New Product()).Support)}), CreateUnitOfWork().Products, New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of Product, Long))).SetParentViewModel(parentViewModel)
        End Function
        Public Shared Function GetSalesFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of Order, Long)
            Return FilterTreeViewModel(Of Order, Long).Create(New FilterTreeModelPageSpecificSettings(Of My.Settings)(My.Settings.Default, "Category", Function(x) x.OrdersStaticFilters, Function(x) x.OrdersCustomFilters, { BindableBase.GetPropertyName(Function() (New Order()).Id), BindableBase.GetPropertyName(Function() (New Order()).CustomerId), BindableBase.GetPropertyName(Function() (New Order()).EmployeeId), BindableBase.GetPropertyName(Function() (New Order()).StoreId)}, { BindableBase.GetPropertyName(Function() (New Order()).Customer) & "." & BindableBase.GetPropertyName(Function() (New Customer()).Name)}), CreateUnitOfWork().Orders.ActualOrders(), New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of Order, Long))).SetParentViewModel(parentViewModel)
        End Function
        Public Shared Function GetOpportunitiesFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of Quote, Long)
            Return FilterTreeViewModel(Of Quote, Long).Create(New FilterTreeModelPageSpecificSettings(Of My.Settings)(My.Settings.Default, "Category", Function(x) x.QuotesStaticFilters, Nothing, Nothing), CreateUnitOfWork().Quotes.ActualQuotes(), New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of Quote, Long))).SetParentViewModel(parentViewModel)
        End Function

        Private Shared Function CreateUnitOfWork() As IDevAVDbUnitOfWork
            Return UnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork()
        End Function

        Private Shared Sub RegisterEntityChangedMessageHandler(Of TEntity, TPrimaryKey)(ByVal recipient As Object, ByVal handler As Action)
            Messenger.Default.Register(Of EntityMessage(Of TEntity, TPrimaryKey))(recipient, Sub(message) handler())
        End Sub
    End Class
End Namespace
