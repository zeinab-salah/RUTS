using Microsoft.Extensions.DependencyInjection;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RUTS.ViewModel.ResourceItem;

public class CreateResourceItemViewModel : ViewModelBase
{
    //Fields
    private int _Id;
    private string _Name;
    private IResourceItemRepository _ResourceItemRepository;

    public int Id
    {
        get
        {
            return _Id;
        }
        set
        {
            _Id = value;
            OnPropertyChanged(nameof(Id));
        }
    }
    public string Name
    {
        get
        {
            return _Name;
        }
        set
        {
            _Name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    public IResourceItemRepository ResourceItemRepository
    {
        get
        {
            return _ResourceItemRepository;
        }
        set
        {
            _ResourceItemRepository = value;
            OnPropertyChanged(nameof(ResourceItemRepository));
        }
    }

    //Commands
    public ICommand SaveResourceItem { get; }

    //constructor
    public CreateResourceItemViewModel(RUTSDesignTimeDbContextFactory dbContextFactory)
    {
        _UnitOfWork            = App.AppHost.Services.GetRequiredService<IUnitofwork>();
        ResourceItemRepository = _UnitOfWork.ResourceItem;
        CloseToaster           = new ViewModelCommand(ExecuteCloseToasterCommand);
        SaveResourceItem = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
    }

    private bool CanExecuteSaveCommand(object obj)
    {
        bool validateData;
        if (string.IsNullOrEmpty(Name))
            validateData = false;
        else
            validateData = true;
        return validateData;
    }
    private void ExecuteSaveCommand(object obj)
    {
        Models.ResourcesItem resourceItem;
        bool result = true;
        if (Id == 0)
        {
            resourceItem = new Models.ResourcesItem()
            {
                Name = Name
            };
            ResourceItemRepository.Add(resourceItem);
            _UnitOfWork.Save();
        }
        else
        {
            resourceItem = new Models.ResourcesItem()
            {
                Id = Id,
                Name = Name
            };
            result = ResourceItemRepository.Update(resourceItem);
            _UnitOfWork.Save();
        }

        Visibility = "Visible";
        if (result)
        {
            if (Id == 0)
            {
                Name = "";
                FeedbackMessage = "Added Successfully!";
            }
            else
            {
                FeedbackMessage = "Updated Successfully!";
            }
        }
        else
        {
            FeedbackMessage = "Something went wrong";
        }
    }
}
