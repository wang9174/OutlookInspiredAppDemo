using System;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace DevExpress.DevAV.ViewModels {
    /// <summary>
    /// Represents the root POCO view model for the DevAVDb data model.
    /// </summary>
    public partial class DevAVDbViewModel : DocumentsViewModel<DevAVDbModuleDescription, IDevAVDbUnitOfWork> {

  const string TablesGroup = "Tables";

        /// <summary>
        /// Creates a new instance of DevAVDbViewModel as a POCO view model.
        /// </summary>
        public static DevAVDbViewModel Create() {
            return ViewModelSource.Create(() => new DevAVDbViewModel());
        }

        /// <summary>
        /// Initializes a new instance of the DevAVDbViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the DevAVDbViewModel type without the POCO proxy factory.
        /// </summary>
        protected DevAVDbViewModel()
            : base(UnitOfWorkSource.GetUnitOfWorkFactory()) {
                NavigationPaneVisibility = NavigationPaneVisibility.Normal;
        }

        protected override DevAVDbModuleDescription[] CreateModules() {
            return new DevAVDbModuleDescription[] {
                new DevAVDbModuleDescription("Employees", "EmployeeCollectionView", TablesGroup, FiltersSettings.GetEmployeesFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Employees)),
                new DevAVDbModuleDescription("Tasks", "TaskCollectionView", TablesGroup, FiltersSettings.GetTasksFilterTree(this)),
                new DevAVDbModuleDescription("Customers", "CustomerCollectionView", TablesGroup, FiltersSettings.GetCustomersFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Customers)),
                new DevAVDbModuleDescription("Products", "ProductCollectionView", TablesGroup, FiltersSettings.GetProductsFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Products)),
                new DevAVDbModuleDescription("Sales", "OrderCollectionView", TablesGroup, FiltersSettings.GetSalesFilterTree(this)),
                new DevAVDbModuleDescription("Opportunities", "QuoteCollectionView", TablesGroup, FiltersSettings.GetOpportunitiesFilterTree(this)),
            };
        }
        Workspace defaultWorkspace = new Workspace();
        LinksViewModel linksViewModel;

        public override void OnLoaded() {
            base.OnLoaded();
            RegisterJumpList();
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 90);
#if !DEBUG
            timer.Tick += OnTimerTick;
#endif
            timer.Start();
        }

        protected override void OnActiveModuleChanged(DevAVDbModuleDescription oldModule) {
            base.OnActiveModuleChanged(oldModule);
            if(ActiveModule != null && ActiveModule.FilterTreeViewModel != null)
                ActiveModule.FilterTreeViewModel.SetViewModel(DocumentManagerService.ActiveDocument.Content);
            MainWindowService.Title = Convert.ToString(DocumentManagerService.ActiveDocument.Title) + " - DevAV";
            FinishModuleChangingDispatcherService.BeginInvoke(() => {
                UpdateWorkspace(oldModule, ActiveModule);
            });
        }

        void OnTimerTick(object sender, EventArgs e) {
            NotificationService.CreatePredefinedNotification("DevAV Tips & Tricks", "Take users where they want to go with DevExpress Map Controls.", "").ShowAsync();
            ((DispatcherTimer)sender).Stop();
        }

        IDocumentManagerService SignleObjectDocumentManagerService { get { return this.GetService<IDocumentManagerService>("SignleObjectDocumentManagerService"); } }

        IWorkspaceService WorkspaceService { get { return this.GetRequiredService<IWorkspaceService>(); } }

        IMainWindowService MainWindowService { get { return this.GetRequiredService<IMainWindowService>(); } }

        IDispatcherService FinishModuleChangingDispatcherService { get { return this.GetRequiredService<IDispatcherService>("FinishModuleChangingDispatcherService"); } }

        INotificationService NotificationService { get { return this.GetRequiredService<INotificationService>(); } }

        void UpdateWorkspace(DevAVDbModuleDescription oldModule, DevAVDbModuleDescription newModule) {
            Workspace oldWorkspace = WorkspaceService.SaveWorkspace();
            Workspace newWorkspace = new Workspace();
            try {
                if(oldModule != null)
                    oldModule.Workspace = oldWorkspace;
                if(newModule != null)
                    newWorkspace = newModule.Workspace ?? defaultWorkspace;
            } finally {
                WorkspaceService.RestoreWorkspace(newWorkspace);
            }
        }

        public LinksViewModel LinksViewModel {
            get {
                if(linksViewModel == null)
                    linksViewModel = LinksViewModel.Create();
                return linksViewModel;
            }
        }

        void RegisterJumpList() {
#if !CLICKONCE
            IApplicationJumpListService jumpListService = this.GetService<IApplicationJumpListService>();
            jumpListService.Items.AddOrReplace("Modules", "New Employee", EmployeeIcon, () => { SignleObjectDocumentManagerService.ShowNewEntityDocument<Employee>(this); });
            jumpListService.Items.AddOrReplace("Modules", "Customers", CustomerIcon, () => { Show(Modules.Where(m => m.DocumentType == "CustomerCollectionView").First()); });
            jumpListService.Items.AddOrReplace("Modules", "Opportunities", OpportunitiesIcon, () => { Show(Modules.Where(m => m.DocumentType == "QuoteCollectionView").First()); });
            jumpListService.Apply();
#endif
        }
        ImageSource EmployeeIcon { get { return new BitmapImage(new Uri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-new-employee-16.png")); } }
        ImageSource CustomerIcon { get { return new BitmapImage(new Uri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-new-customers-16.png")); } }
        ImageSource OpportunitiesIcon { get { return new BitmapImage(new Uri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-new-opportunities-16.png")); } }
    }

    public partial class DevAVDbModuleDescription : ModuleDescription<DevAVDbModuleDescription> {
        public DevAVDbModuleDescription(string title, string documentType, string group, IFilterTreeViewModel filterTreeViewModel, Func<DevAVDbModuleDescription, object> peekCollectionViewModelFactory = null)
            : base(title, documentType, group, peekCollectionViewModelFactory) {
            ImageSource = new Uri(string.Format(@"pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Modules/icon-nav-{0}-32.png", title.ToLowerInvariant()));
            FilterTreeViewModel = filterTreeViewModel;
        }

        public Workspace Workspace { get; set; }

        public Uri ImageSource { get; private set; }

        public IFilterTreeViewModel FilterTreeViewModel { get; private set; }
    }
}
