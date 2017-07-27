using System.Linq;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace DevExpress.DevAV.ViewModels {
    partial class EmployeeViewModel {
        EmployeeContactsViewModel contacts;
        EmployeeQuickLetterViewModel quickLetter;
        LinksViewModel linksViewModel;
        EmployeeTaskDetailsCollectionViewModel employeeAssignedTasksDetails;
        public EmployeeContactsViewModel Contacts {
            get {
                if(contacts == null) {
                    contacts = EmployeeContactsViewModel.Create().SetParentViewModel(this);
                }
                return contacts;
            }
        }
        public EmployeeTaskDetailsCollectionViewModel EmployeeAssignedTasksDetails {
            get {
                if(employeeAssignedTasksDetails == null) {
                    employeeAssignedTasksDetails = EmployeeTaskDetailsCollectionViewModel.Create().SetParentViewModel(this);
                }
                return employeeAssignedTasksDetails;
            }
        }
        protected override void OnEntityChanged() {
            base.OnEntityChanged();
            Contacts.Entity = Entity;
            QuickLetter.Entity = Entity;
            SetDefaultReport(ReportInfoFactory.EmployeeProfile(Entity));
            EmployeeAssignedTasksDetails.UpdateFilter();
        }

        protected override string GetTitle() {
            return Entity.FullName;
        }
        public EmployeeQuickLetterViewModel QuickLetter {
            get {
                if(quickLetter == null)
                    quickLetter = EmployeeQuickLetterViewModel.Create().SetParentViewModel(this);
                return quickLetter;
            }
        }
        public void ShowMap() {
            var mapViewModel = EmployeeCollectionViewModel.CreateEmployeeMapViewModel(Entity, destination => {
                Entity.Address = destination;
                this.RaisePropertyChanged(x => x.Entity);
            });
            DocumentManagerService.CreateDocument("NavigatorMapView", mapViewModel, null, this).Show();
        }
        protected IDocumentManagerService DocumentManagerService { get { return this.GetRequiredService<IDocumentManagerService>(); } }
        public LinksViewModel LinksViewModel {
            get {
                if(linksViewModel == null)
                    linksViewModel = LinksViewModel.Create();
                return linksViewModel;
            }
        }
        public void ShowScheduler(string title) {
            MessageBoxService.Show(@"This demo does not include integration with our WPF Scheduler Suite but you can easily introduce Outlook-inspired scheduling and task management capabilities to your apps with DevExpress Tools.",
                    title, MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK);
        }
        protected override bool SaveCore() {
            if(IsNew() && string.IsNullOrEmpty(Entity.FullName))
                Entity.FullName = string.Format("{0} {1}", Entity.FirstName, Entity.LastName);
            return base.SaveCore();
        }
        public void PrintEmployeeProfile() {
            ShowReport(ReportInfoFactory.EmployeeProfile(Entity));
        }
        public bool CanPrintEmployeeProfile() {
            return Entity != null;
        }
        public void PrintSummaryReport() {
            ShowReport(ReportInfoFactory.EmployeeSummary(Repository.ToList()));
        }
        public void PrintDirectory() {
            ShowReport(ReportInfoFactory.EmployeeDirectory(Repository.ToList()));
        }
        public void PrintTaskList() {
            ShowReport(ReportInfoFactory.EmployeeTaskList(UnitOfWork.Tasks.ToList()));
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
    }
}
