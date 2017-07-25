using System;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Data.Utils;
using DevExpress.DevAV.Common.DataModel;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace DevExpress.DevAV.ViewModels {
    public class EmployeeTaskDetailsCollectionViewModel : CollectionViewModel<EmployeeTask, long, IDevAVDbUnitOfWork> {
        public static EmployeeTaskDetailsCollectionViewModel Create(IUnitOfWorkFactory<IDevAVDbUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new EmployeeTaskDetailsCollectionViewModel(unitOfWorkFactory));
        }
        protected EmployeeTaskDetailsCollectionViewModel(IUnitOfWorkFactory<IDevAVDbUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Tasks, query => query.OrderByDescending(x => x.DueDate)) {
        }
        public IDocumentManagerService AssignTaskService { get { return this.GetService<IDocumentManagerService>("EmployeeAssignService"); } }

        public override void Edit(EmployeeTask projectionEntity) {
            base.Edit(projectionEntity);
            UpdateFilter();
        }
        public override void Delete(EmployeeTask projectionEntity) {
            base.Delete(projectionEntity);
            UpdateFilter();
        }
        public override void New() {
            GetDocumentManagerService().ShowNewEntityDocument<EmployeeTask>(this);
            UpdateFilter();
        }
        public virtual void AssignTask(EmployeeTask task) {
            IDocument document = AssignTaskService.CreateDocument("EmployeeAssignView", task.Id, this);
            if(document != null)
                document.Show();
            UpdateFilter();
        }
        public virtual bool CanAssignTask(EmployeeTask task) {
            return !IsLoading && task != null;
        }
        public void UpdateFilter() {
            var viewModel = this.GetParentViewModel<EmployeeViewModel>();
            var unitOfWork = unitOfWorkFactory.CreateUnitOfWork();
            var actualEntity = unitOfWork.Employees.FirstOrDefault(x => x.Id == viewModel.Entity.Id);
            if(viewModel != null)
                FilterExpression = CriteriaOperatorToExpressionConverter.GetGenericWhere<EmployeeTask>(CriteriaOperator.Parse(EmployeeTaskCollectionViewModel
                    .TasksToFilterCriteriaConverter((actualEntity == null) ? null : actualEntity.AssignedEmployeeTasks)));
        }
    }
}
