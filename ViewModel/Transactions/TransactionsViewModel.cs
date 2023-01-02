using Microsoft.Extensions.DependencyInjection;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RUTS.ViewModel.Transactions;

public class TransactionsViewModel : ViewModelBase
{
    private int _CurrentPage     = 1;
    private int _NumberOfPages   = 10;
    private int _SelectedRecored = 10;

    //Fields
    private DateTime? _StartDate = null;
    private DateTime? _EndDate = null;
    private bool _CreateWindow        = false;
    private bool _EditWindow          = false;
    private bool _isViewVisible       = true;
    private bool _GoToCreate          = false;
    private bool _GoToEdit            = false;
    private IUnitofwork _UnitOfWork;
    private Transaction _SelectedItem;
    public IEnumerable<Transaction> _Data { get; set; }

    public List<Transaction> _ListOfRecords;

    //Constructor
    public TransactionsViewModel()
    {
        _UnitOfWork        = App.AppHost.Services.GetRequiredService<IUnitofwork>();
        if (Search != null)
            Data = _UnitOfWork.Transaction.GetAll(u => u.Deleted_at == null && u.StatementDate == null && (u.Currency.Name.ToLower().Contains(Search.ToLower()) && u.Correspondent.Name.ToLower().Contains(Search.ToLower())), includeProps: "Currency,Correspondent,User,Benificary,ResourcesItem,Bank");
        else
            Data = _UnitOfWork.Transaction.GetAll(u => u.Deleted_at == null && u.StatementDate == null, includeProps: "Currency,Correspondent,User,Benificary,ResourcesItem,Bank");
        ConfirmTransactionCommand = new ViewModelCommand(ExecuteConfirmTransactionCommand, CanExecuteConfirmTransactionCommand);
        ShowConfirmTransaction    = new ViewModelCommand(ExecuteShowConfirmTransactionCommand);
        EditTransaction    = new ViewModelCommand(ExecuteEditTransactionCommand);
        CreateTransaction  = new ViewModelCommand(ExecuteCreateTransactionCommand);
        RemoveTransaction  = new ViewModelCommand(ExecuteRemoveTransactionCommand);
        CloseToaster       = new ViewModelCommand(ExecuteCloseToasterCommand);
        ConfirmDelete      = new ViewModelCommand(ExecuteConfirmDeleteCommand);
        FirstCommand       = new ViewModelCommand(ExecuteFirstCommand);
        PreviousCommand    = new ViewModelCommand(ExecutePreviousCommand);
        NextCommand        = new ViewModelCommand(ExecuteNextCommand);
        LastCommand        = new ViewModelCommand(ExecuteLastCommand);
        FilterData         = new ViewModelCommand(ExecuteFilterDataCommand);
        _ListOfRecords     = new List<Transaction>();
        UpdateCollections(Data.Take(SelectedRecord));
        UpdateRecordCount();
    }

    //commands
    public ICommand EditTransaction { get; }
    public ICommand ShowConfirmTransaction { get; }
    public ICommand CreateTransaction { get; }
    public ICommand RemoveTransaction { get; }
    public ICommand FirstCommand { get; }
    public ICommand PreviousCommand { get; }
    public ICommand NextCommand { get; }
    public ICommand LastCommand { get; }
    public ICommand ConfirmTransactionCommand { get; }

    int RecordStartsFrom = 0;

    private void ExecuteLastCommand(object obj)
    {
        UpdateCollections(Data.Skip(Data.Count() - SelectedRecord));
        CurrentPage = NumberOfPages;
        UpdateEnableState();
    }

    private void ExecuteNextCommand(object obj)
    {
        RecordStartsFrom  = CurrentPage * SelectedRecord;
        var RecordsToShow = Data.Skip(RecordStartsFrom).Take(SelectedRecord);
        UpdateCollections(RecordsToShow);
        CurrentPage++;
        UpdateEnableState();
    }

    private void ExecutePreviousCommand(object obj)
    {
        CurrentPage--;
        RecordStartsFrom = Data.Count() - SelectedRecord * (NumberOfPages - (CurrentPage - 1));
        var ReordsToShow = Data.Skip(RecordStartsFrom).Take(SelectedRecord);
        UpdateCollections(ReordsToShow);
        UpdateEnableState();
    }

    private void ExecuteFirstCommand(object obj)
    {
        UpdateCollections(Data.Take(SelectedRecord));
        CurrentPage = 1;
        UpdateEnableState();
    }
    public List<Transaction> ListOfRecords
    {
        get
        {
            return _ListOfRecords;
        }
        set
        {
            _ListOfRecords = value;
            OnPropertyChanged(nameof(ListOfRecords));
            UpdateEnableState();
        }
    }

    public int CurrentPage
    {
        get
        {
            return _CurrentPage;
        }
        set
        {
            _CurrentPage = value;
            OnPropertyChanged(nameof(CurrentPage));
            UpdateEnableState();
        }
    }
    private DateTime? StartDate
    {
        get
        {
            return _StartDate;
        }
        set
        {
            _StartDate = value;
            OnPropertyChanged(nameof(StartDate));
        }
    }
    private DateTime? EndDate
    {
        get
        {
            return _EndDate;
        }
        set
        {
            _EndDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }
    private void UpdateEnableState()
    {
        IsFirstEnable     = CurrentPage > 1;
        IsPreviouseEnable = CurrentPage > 1;
        IsNextEnable      = CurrentPage < NumberOfPages;
        IsLastEnable      = CurrentPage < NumberOfPages;
    }

    public int NumberOfPages
    {
        get
        {
            return _NumberOfPages;
        }
        set
        {
            _NumberOfPages = value;
            OnPropertyChanged(nameof(NumberOfPages));
            UpdateEnableState();
        }
    }
    public int SelectedRecord
    {
        get
        {
            return _SelectedRecored;
        }
        set
        {
            _SelectedRecored = value;
            OnPropertyChanged(nameof(SelectedRecord));
            UpdateRecordCount();
        }
    }

    private void UpdateRecordCount()
    {
        int reminder  = Data.Count() % SelectedRecord;
        int result    = Data.Count() / SelectedRecord;
        NumberOfPages = reminder > 0 ? result + 1 : result;
        NumberOfPages = NumberOfPages == 0 ? 1 : NumberOfPages;
        UpdateCollections(Data.Take(SelectedRecord));
        CurrentPage   = 1;
    }
    private void UpdateCollections(IEnumerable<Transaction> enumerable)
    {
        List<Transaction> listOfRecords2 = new List<Transaction>();
        foreach (var item in enumerable)
        {
            listOfRecords2.Add(item);
        }
        ListOfRecords = listOfRecords2;
    }
    private bool _IsFirstEnable;

    public bool IsFirstEnable
    {
        get
        {
            return _IsFirstEnable;
        }
        set
        {
            _IsFirstEnable = value;
            OnPropertyChanged(nameof(IsFirstEnable));

        }
    }
    private bool _IsPreviouseEnable;

    public bool IsPreviouseEnable
    {
        get
        {
            return _IsPreviouseEnable;
        }
        set
        {
            _IsPreviouseEnable = value;
            OnPropertyChanged(nameof(IsPreviouseEnable));

        }
    }

    private bool _IsNextEnable;

    public bool IsNextEnable
    {
        get
        {
            return _IsNextEnable;
        }
        set
        {
            _IsNextEnable = value;
            OnPropertyChanged(nameof(IsNextEnable));

        }
    }
    private bool _IsLastEnable;
    private DateTime _StatmentDate = DateTime.Now;

    public bool IsLastEnable
    {
        get
        {
            return _IsLastEnable;
        }
        set
        {
            _IsLastEnable = value;
            OnPropertyChanged(nameof(IsLastEnable));

        }
    }

    public Transaction SelectedItem
    {
        get
        {
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
        }
    }
    public bool IsViewVisible
    {
        get
        {
            return _isViewVisible;
        }
        set
        {
            _isViewVisible = value;
            OnPropertyChanged(nameof(IsViewVisible));
        }
    }
    public bool CreateWindow
    {
        get
        {
            return _CreateWindow;
        }
        set
        {
            _CreateWindow = value;
            OnPropertyChanged(nameof(CreateWindow));
        }
    }
    public DateTime StatmentDate
    {
        get
        {
            return _StatmentDate;
        }
        set
        {
            _StatmentDate = value;
            OnPropertyChanged(nameof(StatmentDate));
        }
    }

    public bool EditWindow
    {
        get
        {
            return _EditWindow;
        }
        set
        {
            _EditWindow = value;
            OnPropertyChanged(nameof(EditWindow));
        }
    }
    public bool GoToCreate
    {
        get
        {
            return _GoToCreate;
        }
        set
        {
            _GoToCreate = value;
            OnPropertyChanged(nameof(GoToCreate));
        }
    }
    public bool GoToEdit
    {
        get
        {
            return _GoToEdit;
        }
        set
        {
            _GoToEdit = value;
            OnPropertyChanged(nameof(GoToEdit));
        }
    }
    public IEnumerable<Transaction> Data
    {
        get
        {
            return _Data;
        }
        set
        {
            _Data = value;
            OnPropertyChanged(nameof(Data));
        }
    }
    private void ExecuteConfirmTransactionCommand(object obj)
    {
        if (null != SelectedItem)
        {
            Transaction t = _UnitOfWork.Transaction.GetFirstOrDefault(u => u.Id == SelectedItem.Id);
            string Amount = SelectedItem.Amount;
            bool check    = CheckAccount(SelectedItem.Type, SelectedItem.CorrespondentId, SelectedItem.CurrencyId, Amount);
            if (check)
            {
                t.StatementDate = StatmentDate;
                _UnitOfWork.Transaction.Update(t);
                _UnitOfWork.Save();
                Data = Data.Where(u => u.StatementDate == null).ToList();
                ListOfRecords = ListOfRecords.Where(u => u.StatementDate == null).ToList();
            }
            ConfirmTransactionVisibility = "Hidden";
        }
    }
    private void ExecuteEditTransactionCommand(object obj)
    {
        CreateWindow  = false;
        EditWindow    = true;
        IsViewVisible = false;
    }
    private void ExecuteCreateTransactionCommand(object obj)
    {
        CreateWindow  = true;
        EditWindow    = false;
        IsViewVisible = false;
    }
    private void ExecuteRemoveTransactionCommand(object obj)
    {
        if (null != SelectedItem)
        {
            ConfirmTransactionVisibility = "Hidden";
            Visibility = "Visible";
            FeedbackMessage = "Are you sure you want to delete this record?";
        }
    }
    private void ExecuteShowConfirmTransactionCommand(object obj)
    {
        if (null != SelectedItem)
        {
            ConfirmTransactionVisibility = "Visible";
        }
    }
    private void ExecuteConfirmDeleteCommand(object obj)
    {
        ConfirmTransactionVisibility = "Hidden";
        Visibility = "Hidden";
        int Id                  = SelectedItem.Id;
        SelectedItem.Deleted_at = DateTime.Now;
        _UnitOfWork.Transaction.Remove(SelectedItem);
        _UnitOfWork.Save();
        ExecuteFilterDataCommand(obj);
    }
    private bool CheckAccount(string Type, int CorrespondentId, int CurrencyId, string Amount)
    {
        bool check = false;
        Account account = _UnitOfWork.Account.GetFirstOrDefault(u => u.CurrencyId == CurrencyId && u.CorrespondentId == CorrespondentId);
        if (Type == "Credit")
        {
            Amount = (Convert.ToDouble(account.Ballance.Trim().Replace(",", "")) + Convert.ToDouble(Amount.Trim().Replace(",", ""))).ToString();
            account.Ballance = Amount;
            _UnitOfWork.Account.Update(account);
            _UnitOfWork.Save();
            check = true;
        }
        else
        {
            if (Convert.ToDouble(account.Ballance.Trim().Replace(",", "")) >= Convert.ToDouble(Amount.Trim().Replace(",", "")))
            {
                Amount = (Convert.ToDouble(account.Ballance.Trim().Replace(",", "")) - Convert.ToDouble(Amount.Trim().Replace(",", ""))).ToString();
                account.Ballance = Amount;
                _UnitOfWork.Account.Update(account);
                _UnitOfWork.Save();
                check = true;
            }
            else
            {
                FeedbackMessage = "Account Ballance Is not enough!";
            }
        }
        if (check)
        {
            FeedbackMessage = "Transaction Confirmed Successfully!";
        }
        else
        {
            FeedbackMessage = "Unable To confirm Transaction!";
        }
        ConfirmVisibility = "Visible";
        return check;
    }
    private bool CanExecuteConfirmTransactionCommand(object obj)
    {
        bool validateData = true;
        if (string.IsNullOrEmpty(StatmentDate.ToString()))
            validateData = false;
        else
            validateData = true;
        return validateData;
    }
    private void ExecuteFilterDataCommand(object obj)
    {
        string[] args = { };
        Data = _UnitOfWork.Transaction.GetAll(u => u.Deleted_at == null && u.StatementDate == null, includeProps: "Currency,Correspondent,User,Benificary,ResourcesItem,Bank");
        string[] words = Search != null ? Search.Split(' ') : args;
        foreach (string word in words)
        {
            Data = Data.Where(u => u.Currency.Name.ToLower().Contains(word.ToLower()) || u.Correspondent.Name.ToLower().Contains(word.ToLower()));
        }
        if (StartDate != null)
        {
            Data = Data.Where(u => u.Created_at <= StartDate);
        }
        if (EndDate != null)
        {
            Data = Data.Where(u => u.Created_at >= EndDate);
        }
        UpdateCollections(Data.Take(SelectedRecord));
        UpdateRecordCount();
    }
}
