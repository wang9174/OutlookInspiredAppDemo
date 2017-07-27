Namespace My


    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")>
    Friend NotInheritable Partial Class Settings
        Inherits System.Configuration.ApplicationSettingsBase

        Private Shared defaultInstance As Settings = (CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New Settings()), Settings))

        Public Shared ReadOnly Property [Default]() As Settings
            Get
                Return defaultInstance
            End Get
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>All</Name>" & ControlChars.CrLf & "    <FilterCriteria />" & ControlChars.CrLf & "    <ImageUri />" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Overdue Tasks</Name>" & ControlChars.CrLf & "    <FilterCriteria>(Not IsNull([DueDate]))AND([DueDate] &lt; LocalDateTimeNow())AND(Not([Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Completed#))</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri />" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Incomplete Tasks</Name>" & ControlChars.CrLf & "    <FilterCriteria>Not([Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Completed#)</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri>Resources/Tasks/Deferred.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>High Priority Tasks</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Priority] = ##Enum#DevExpress.DevAV.EmployeeTaskPriority,High#</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri />" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property TasksStaticFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("TasksStaticFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("TasksStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>On probation </Name>" & ControlChars.CrLf & "    <FilterCriteria>Not IsNull([ProbationReason])</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property EmployeesCustomFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("EmployeesCustomFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("EmployeesCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>All</Name>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Salaried</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Salaried#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Commission</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Commission#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Contract</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Contract#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Terminated</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Terminated#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>On Leave</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,OnLeave#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property EmployeesStaticFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("EmployeesStaticFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("EmployeesStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Active</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.CustomerStatus,Active#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Suspended</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.CustomerStatus,Suspended#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property CustomersCustomFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("CustomersCustomFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("CustomersCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>All</Name>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Stores &gt; 10 Locations</Name>" & ControlChars.CrLf & "    <FilterCriteria>[TotalStores] &gt; 10</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Revenues &gt; 100 Billion</Name>" & ControlChars.CrLf & "    <FilterCriteria>[AnnualRevenue] &gt; 100000000000.0m</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Employees &gt; 10000</Name>" & ControlChars.CrLf & "    <FilterCriteria>[TotalEmployees] &gt; 10000</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property CustomersStaticFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("CustomersStaticFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("CustomersStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Discontinued</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Available] = False</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Out of Stock</Name>" & ControlChars.CrLf & "    <FilterCriteria>[CurrentInventory] = 0</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property ProductsCustomFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("ProductsCustomFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("ProductsCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>All</Name>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Video Players</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,VideoPlayers#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Automation</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Automation#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Monitors</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Monitors#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Projectors</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Projectors#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Televisions</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Televisions#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property ProductsStaticFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("ProductsStaticFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("ProductsStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Sales &gt; $5000</Name>" & ControlChars.CrLf & "    <FilterCriteria>[TotalAmount] &gt; 5000.0m</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Sales &lt; $5000</Name>" & ControlChars.CrLf & "    <FilterCriteria>[TotalAmount] &lt; 5000.0m</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property OrdersCustomFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("OrdersCustomFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("OrdersCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>All</Name>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Today</Name>" & ControlChars.CrLf & "    <FilterCriteria>IsOutlookIntervalToday([OrderDate])</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Yesterday</Name>" & ControlChars.CrLf & "    <FilterCriteria>IsOutlookIntervalYesterday([OrderDate])</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>This Month</Name>" & ControlChars.CrLf & "    <FilterCriteria>[OrderDate] &gt;= #2014-09-01# And [OrderDate] &lt;= #2014-09-04 14:47:53.71590#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>This Year</Name>" & ControlChars.CrLf & "    <FilterCriteria>IsOutlookIntervalEarlierThisYear([OrderDate])</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property OrdersStaticFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("OrdersStaticFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("OrdersStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>All</Name>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Last Year</Name>" & ControlChars.CrLf & "    <FilterCriteria>GetYear([Date])=(GetYear(LocalDateTimeNow())-1)</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>This Year</Name>" & ControlChars.CrLf & "    <FilterCriteria>IsOutlookIntervalEarlierThisYear([Date])</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property QuotesStaticFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("QuotesStaticFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("QuotesStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>All</Name>" & ControlChars.CrLf & "    <FilterCriteria />" & ControlChars.CrLf & "    <ImageUri />" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property TasksCustomFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("TasksCustomFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("TasksCustomFilters") = value
            End Set
        End Property
    End Class
End Namespace
