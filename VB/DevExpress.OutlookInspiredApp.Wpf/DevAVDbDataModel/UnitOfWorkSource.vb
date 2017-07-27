Imports System
Imports System.Linq
Imports System.Data
Imports System.Data.Entity
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.Common.DataModel
Imports DevExpress.DevAV.Common.DataModel.EntityFramework
Imports DevExpress.DevAV
Imports DevExpress.Mvvm
Namespace DevExpress.DevAV.DevAVDbDataModel
  ''' <summary>
  ''' Provides methods to obtain the relevant IUnitOfWorkFactory.
  ''' </summary>
  Public Module UnitOfWorkSource
    Friend Class DbUnitOfWorkFactory
      Implements IUnitOfWorkFactory(Of IDevAVDbUnitOfWork)
      Public Shared ReadOnly Instance As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = New DbUnitOfWorkFactory()
      Private Sub New()
      End Sub
      Private Function CreateUnitOfWork() As IDevAVDbUnitOfWork Implements IUnitOfWorkFactory(Of IDevAVDbUnitOfWork).CreateUnitOfWork
        Return New DevAVDbUnitOfWork(Function() New DevAVDb())
      End Function
    End Class
    Friend Class DesignUnitOfWorkFactory
      Implements IUnitOfWorkFactory(Of IDevAVDbUnitOfWork)
      Public Shared ReadOnly Instance As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = New DesignUnitOfWorkFactory()
      Private Sub New()
      End Sub
      Private Function CreateUnitOfWork() As IDevAVDbUnitOfWork Implements IUnitOfWorkFactory(Of IDevAVDbUnitOfWork).CreateUnitOfWork
        Return New DevAVDbDesignTimeUnitOfWork()
      End Function
    End Class    
    ''' <summary>
    ''' Returns the IUnitOfWorkFactory implementation based on the current mode (run-time or design-time).
    ''' </summary>
    Public Function GetUnitOfWorkFactory() As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork)
      Return GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode)
    End Function    
    ''' <summary>
    ''' Returns the IUnitOfWorkFactory implementation based on the given mode (run-time or design-time).
    ''' </summary>
    ''' <param name="isInDesignTime">Used to determine which implementation of IUnitOfWorkFactory should be returned.</param>
    Public Function GetUnitOfWorkFactory(ByVal isInDesignTime As Boolean) As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork)
      Return If(isInDesignTime, DesignUnitOfWorkFactory.Instance, DbUnitOfWorkFactory.Instance)
    End Function
  End Module
End Namespace
