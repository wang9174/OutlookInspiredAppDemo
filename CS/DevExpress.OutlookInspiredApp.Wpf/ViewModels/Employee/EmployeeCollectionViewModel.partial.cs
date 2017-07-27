using System;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;

namespace DevExpress.DevAV.ViewModels {
    partial class EmployeeCollectionViewModel : ISupportFiltering<Employee> {
        static string HomeAddress { get { return "505 N Brand Blvd, Glendale, CA 91203"; } }
        static GeoPoint HomeLocation { get { return new GeoPoint(34.153454, -118.255111); } }
        internal static NavigatorMapViewModel<Employee> CreateEmployeeMapViewModel(Employee employee, Action<Address> applyDestination) {
            return NavigatorMapViewModel<Employee>.Create(
                employee,
                HomeAddress,
                HomeLocation,
                employee.Address.ToString(),
                new GeoPoint(employee.Address.Latitude, employee.Address.Longitude),
                applyDestination);
        }

        EmployeeContactsViewModel entityPanelViewModel;
        EmployeeQuickLetterViewModel quickLetter;
        ViewSettingsViewModel viewSettings;

        public EmployeeContactsViewModel EntityPanelViewModel {
            get {
                if(entityPanelViewModel == null)
                    entityPanelViewModel = EmployeeContactsViewModel.Create();
                return entityPanelViewModel;
            }
        }
        public ViewSettingsViewModel ViewSettings {
            get {
                if(viewSettings == null)
                    viewSettings = ViewSettingsViewModel.Create(CollectionViewKind.CardView, this);
                return viewSettings;
            }
        }
        public EmployeeQuickLetterViewModel QuickLetter {
            get {
                if(quickLetter == null)
                    quickLetter = EmployeeQuickLetterViewModel.Create().SetParentViewModel(this);
                return quickLetter;
            }
        }
        public void ShowMap() {
            var mapViewModel = CreateEmployeeMapViewModel(SelectedEntity, destination => {
                SelectedEntity.Address = destination;
                Save(SelectedEntity);
            });
            this.GetRequiredService<IDocumentManagerService>().CreateDocument("EmployeeMapView", mapViewModel, null, this).Show();
        }
        public bool CanShowMap() {
            return SelectedEntity != null;
        }

        public void PrintEmployeeProfile() {
            ShowReport(ReportInfoFactory.EmployeeProfile(SelectedEntity));
        }
        public bool CanPrintEmployeeProfile() {
            return SelectedEntity != null;
        }
        public void PrintSummaryReport() {
            ShowReport(ReportInfoFactory.EmployeeSummary(Entities));
        }
        public void PrintDirectory() {
            ShowReport(ReportInfoFactory.EmployeeDirectory(Entities));
        }
        public void PrintTaskList() {
            ShowReport(ReportInfoFactory.TaskListReport(CreateUnitOfWork().Tasks.ToList()));
        }

        void ShowReport(IReportInfo reportInfo) {
            ExportService.ShowReport(reportInfo);
            PrintService.ShowReport(reportInfo);
        }
        void SetDefaultReport(IReportInfo reportInfo) {
            if(this.IsInDesignMode()) return;
            ExportService.SetDefaultReport(reportInfo);
            PrintService.SetDefaultReport(reportInfo);
        }
        IReportService PrintService { get { return this.GetRequiredService<IReportService>("PrintService"); } }
        IReportService ExportService { get { return this.GetRequiredService<IReportService>("ExportService"); } }

        protected override void OnSelectedEntityChanged() {
            base.OnSelectedEntityChanged();
            QuickLetter.Entity = SelectedEntity;
            if(SelectedEntity != null)
                EntityPanelViewModel.Entity = SelectedEntity;
            SetDefaultReport(ReportInfoFactory.EmployeeProfile(SelectedEntity));
        }
        public override void UpdateSelectedEntity() {
            base.UpdateSelectedEntity();
            QuickLetter.RaisePropertyChanged(x => x.Entity);
            EntityPanelViewModel.RaisePropertyChanged(x => x.Entity);
        }
        public void ShowScheduler(string title) {
            MessageBoxService.Show(@"This demo does not include integration with our WPF Scheduler Suite but you can easily introduce Outlook-inspired scheduling and task management capabilities to your apps with DevExpress Tools.",
                    title, MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK);
        }
        public void CreateCustomFilter() {
            Messenger.Default.Send(new CreateCustomFilterMessage<Employee>());
        }

        public void AddTask() {
            Action<EmployeeTask> initializer = x => {
                x.OwnerId = SelectedEntity.Id;
            };
            IDocument document = this.GetRequiredService<IDocumentManagerService>().CreateDocument("EmployeeTaskView", null, initializer, this);
            EmployeeTaskViewModel taskViewModel = (EmployeeTaskViewModel)(document.Content);
            taskViewModel.Entity.AssignedEmployees.Add(taskViewModel.EntityContextLookUpEmployees.Single(x => x.Id == SelectedEntity.Id));
            taskViewModel.RaisePropertyChanged(x => x.Entity);
            document.Show();
            this.Refresh();
        }
        public bool CanAddTask() {
            return SelectedEntity != null;
        }

        #region ISupportFiltering
        Expression<Func<Employee, bool>> ISupportFiltering<Employee>.FilterExpression {
            get { return FilterExpression; }
            set { FilterExpression = value; }
        }
        #endregion
    }
}
