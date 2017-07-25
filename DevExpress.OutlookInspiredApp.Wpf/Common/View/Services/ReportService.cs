using System;
using System.ComponentModel;
using System.Printing;
using System.Windows;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Ribbon;
using DevExpress.XtraReports;

namespace DevExpress.DevAV.Common.View {
    public abstract class ReportServiceBase : ServiceBase, IReportService {
        class PrintDirectXtraReportPreviewModel : XtraReportPreviewModel {
            protected override void PrintDirect(object parameter) {
                if(parameter is string)
                    PrintDirect((string)parameter);
                else if(parameter is PrintQueue)
                    PrintDirect((PrintQueue)parameter);
                else
                    PrintDirect();
            }
        }

        bool isVisible;
        IReportInfo defaultReportInfo;
        IReportInfo reportInfo;
        IReportInfo actualReportInfo;

        protected bool IsVisible {
            get { return isVisible; }
            set {
                isVisible = value;
                UpdateReport();
                if(!isVisible)
                    this.reportInfo = null;
            }
        }
        protected virtual void SetDefaultReport(IReportInfo reportInfo) {
            this.defaultReportInfo = reportInfo;
            UpdateReport();
        }
        protected virtual void ShowReport(IReportInfo reportInfo) {
            this.reportInfo = reportInfo;
            UpdateReport();
        }
        void UpdateReport() {
            UpdateReportCore(IsVisible ? (reportInfo ?? defaultReportInfo) : null);
        }
        protected virtual void UpdateReportCore(IReportInfo actualReportInfo) {
            UnsubscribeFromParametersViewModel();
            this.actualReportInfo = actualReportInfo;
            SubscribeToParametersViewModel();
            if(this.actualReportInfo == null)
                DestroyReport();
            else
                CreateReport();
        }
        void OnParametersViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            CreateReport();
        }
        void CreateReport() {
            IReport report = actualReportInfo.CreateReport();
            PrintDirectXtraReportPreviewModel model = GetPreviewModel() as PrintDirectXtraReportPreviewModel;
            if(model == null) {
                model = new PrintDirectXtraReportPreviewModel() { ZoomMode = new ZoomFitModeItem(ZoomFitMode.PageWidth) };
                SetPreviewModel(model);
            }
            model.Report = report;
            SetCustomSettingsViewModel(actualReportInfo.ParametersViewModel);
            report.PrintingSystemBase.ClearContent();
            report.CreateDocument(true);
        }
        void DestroyReport() {
            SetCustomSettingsViewModel(null);
            SetPreviewModel(null);
        }
        protected abstract IPreviewModel GetPreviewModel();
        protected abstract void SetPreviewModel(IDocumentPreviewModel previewModel);
        protected abstract void SetCustomSettingsViewModel(object customSettingsViewModel);
        object ActualParametersViewModel { get { return this.actualReportInfo == null ? null : this.actualReportInfo.ParametersViewModel; } }
        void SubscribeToParametersViewModel() {
            INotifyPropertyChanged parametersViewModel = ActualParametersViewModel as INotifyPropertyChanged;
            if(parametersViewModel != null)
                parametersViewModel.PropertyChanged += OnParametersViewModelPropertyChanged;
        }
        void UnsubscribeFromParametersViewModel() {
            INotifyPropertyChanged parametersViewModel = ActualParametersViewModel as INotifyPropertyChanged;
            if(parametersViewModel != null)
                parametersViewModel.PropertyChanged -= OnParametersViewModelPropertyChanged;
        }
        #region IReportService
        void IReportService.SetDefaultReport(IReportInfo reportInfo) {
            SetDefaultReport(reportInfo);
        }
        void IReportService.ShowReport(IReportInfo reportInfo) {
            ShowReport(reportInfo);
        }
        #endregion
    }
    public class BackstageDocumentPreviewReportService : ReportServiceBase {
        public static readonly DependencyProperty BackstageViewIsOpenProperty =
            DependencyProperty.Register("BackstageViewIsOpen", typeof(bool), typeof(BackstageDocumentPreviewReportService), new PropertyMetadata(false, (d, e) =>
                ((BackstageDocumentPreviewReportService)d).OnBackstageViewIsOpenChanged()));
        public static readonly DependencyProperty BackstageItemProperty =
            DependencyProperty.Register("BackstageItem", typeof(BackstageTabItem), typeof(BackstageDocumentPreviewReportService), new PropertyMetadata(null, (d, e) =>
                ((BackstageDocumentPreviewReportService)d).OnBackstageItemChanged((BackstageItem)e.OldValue, (BackstageItem)e.NewValue)));
        public static readonly DependencyProperty BackstageDocumentPreviewProperty =
            DependencyProperty.Register("BackstageDocumentPreview", typeof(BackstageDocumentPreview), typeof(BackstageDocumentPreviewReportService));

        public bool BackstageViewIsOpen {
            get { return (bool)GetValue(BackstageViewIsOpenProperty); }
            set { SetValue(BackstageViewIsOpenProperty, value); }
        }
        public BackstageTabItem BackstageItem {
            get { return (BackstageTabItem)GetValue(BackstageItemProperty); }
            set { SetValue(BackstageItemProperty, value); }
        }
        public BackstageDocumentPreview BackstageDocumentPreview {
            get { return (BackstageDocumentPreview)GetValue(BackstageDocumentPreviewProperty); }
            set { SetValue(BackstageDocumentPreviewProperty, value); }
        }

        protected override void ShowReport(IReportInfo reportInfo) {
            base.ShowReport(reportInfo);
            Dispatcher.BeginInvoke((Action)OpenBackstageView);
        }
        void OpenBackstageView() {
            BackstageItem.Backstage.SelectedTab = BackstageItem;
            BackstageItem.Backstage.IsOpen = true;
        }
        protected override void UpdateReportCore(IReportInfo actualReportInfo) {
            base.UpdateReportCore(actualReportInfo);
            BackstageItem.IsEnabled = actualReportInfo != null;
        }
        void OnBackstageViewIsOpenChanged() {
            IsVisible = BackstageViewIsOpen;
        }
        void OnBackstageItemChanged(BackstageItem oldItem, BackstageItem newItem) {
            if(newItem != null)
                newItem.IsEnabled = oldItem != null && oldItem.IsEnabled;
        }
        protected override IPreviewModel GetPreviewModel() {
            return BackstageDocumentPreview.Model;
        }
        protected override void SetPreviewModel(IDocumentPreviewModel previewModel) {
            BackstageDocumentPreview.Model = previewModel;
        }
        protected override void SetCustomSettingsViewModel(object customSettingsViewModel) {
            BackstageDocumentPreview.CustomSettings = customSettingsViewModel;
        }
    }
    public class DocumentViewerReportService : ReportServiceBase {
        DocumentViewer DocumentViewer { get { return (DocumentViewer)AssociatedObject; } }

        protected override void OnAttached() {
            base.OnAttached();
            IsVisible = true;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            IsVisible = false;
        }
        protected override IPreviewModel GetPreviewModel() {
            return DocumentViewer.Model;
        }
        protected override void SetPreviewModel(IDocumentPreviewModel previewModel) {
            DocumentViewer.Model = previewModel;
        }
        protected override void SetCustomSettingsViewModel(object customSettingsViewModel) { }
    }
}
