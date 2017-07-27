using System;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;

namespace DevExpress.DevAV.ViewModels {
    partial class OrderCollectionViewModel : ISupportFiltering<Order> {
        ViewSettingsViewModel viewSettings;

        public ViewSettingsViewModel ViewSettings {
            get {
                if(viewSettings == null)
                    viewSettings = ViewSettingsViewModel.Create(CollectionViewKind.ListView, this);
                return viewSettings;
            }
        }
        public override void New() { ShowProductEditForm(); }
        public override void Edit(Order entity) { ShowProductEditForm(); }

        protected override void OnSelectedEntityChanged() {
            base.OnSelectedEntityChanged();
            SetDefaultReport(ReportInfoFactory.SalesInvoice(SelectedEntity));
        }

        public void PrintInvoice() {
            ShowReport(ReportInfoFactory.SalesInvoice(SelectedEntity));
        }
        public bool CanPrintInvoice() {
            return SelectedEntity != null;
        }

        public void PrintSummaryReport() {
            ShowReport(ReportInfoFactory.SalesOrdersSummaryReport(QueriesHelper.GetSaleSummaries(CreateUnitOfWork().OrderItems)));
        }

        public void PrintSalesAnalysisReport() {
            ShowReport(ReportInfoFactory.SalesAnalysisReport(QueriesHelper.GetSaleAnalysis(CreateUnitOfWork().OrderItems)));
        }

        void ShowReport(IReportInfo reportInfo) {
            ExportService.ShowReport(reportInfo);
            PrintService.ShowReport(reportInfo);
        }
        void SetDefaultReport(IReportInfo reportInfo) {
            this.GetRequiredService<IReportService>("DocumentViewerService").SetDefaultReport(reportInfo);
            if(this.IsInDesignMode()) return;
            ExportService.SetDefaultReport(reportInfo);
            PrintService.SetDefaultReport(reportInfo);
        }
        IReportService PrintService { get { return this.GetRequiredService<IReportService>("PrintService"); } }
        IReportService ExportService { get { return this.GetRequiredService<IReportService>("ExportService"); } }

        IDocumentManagerService DocumentManagerService { get { return this.GetRequiredService<IDocumentManagerService>(); } }

        public void ShowMap() {
            var mapViewModel = NavigatorMapViewModel<CustomerStore>.Create(
                SelectedEntity.Store,
                SelectedEntity.Customer.HomeOffice.ToString(),
                new GeoPoint(SelectedEntity.Customer.HomeOffice.Latitude, SelectedEntity.Customer.HomeOffice.Longitude),
                SelectedEntity.Store.Address.ToString(),
                new GeoPoint(SelectedEntity.Store.Address.Latitude, SelectedEntity.Store.Address.Longitude)
            );
            DocumentManagerService.CreateDocument("OrderMapView", mapViewModel, null, this).Show();
        }
        public bool CanShowMap() {
            return SelectedEntity != null;
        }
        void ShowProductEditForm() {
            MessageBoxService.Show(@"You can easily create custom edit forms using the 40+ controls that ship as part of the DevExpress Data Editors Library. To see what you can build, activate the Employees module.",
                "Edit Sales", MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK);
        }
        public void CreateCustomFilter() {
            Messenger.Default.Send(new CreateCustomFilterMessage<Order>());
        }

        public void QuickLetter(string templateName) {
            long? key = SelectedEntity.Customer.Employees.FirstOrDefault() == null ? (long?)null : SelectedEntity.Customer.Employees.FirstOrDefault().Id;
            var mailMergeViewModel = MailMergeViewModel<CustomerEmployee, LinksViewModel>.Create(unitOfWorkFactory, x => x.CustomerEmployees, key, templateName, LinksViewModel.Create());
            DocumentManagerService.CreateDocument("EmployeeMailMergeView", mailMergeViewModel, null, this).Show();
        }
        public bool CanQuickLetter(string templateName) {
            return SelectedEntity != null;
        }
        public override void Delete(Order entity) {
            MessageBoxService.ShowMessage("To ensure data integrity, the Sales module doesn't allow records to be deleted. Record deletion is supported by the Employees module.", "Delete Sale", MessageButton.OK);
        }
        #region ISupportFiltering
        Expression<Func<Order, bool>> ISupportFiltering<Order>.FilterExpression {
            get { return FilterExpression; }
            set { FilterExpression = value; }
        }
        #endregion
    }
}
