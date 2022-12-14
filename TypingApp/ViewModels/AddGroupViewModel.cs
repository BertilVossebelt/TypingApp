using System;
using System.ComponentModel;
using System.Windows.Input;
using TypingApp.Commands;
using TypingApp.Models;
using TypingApp.Services;
using TypingApp.Stores;

namespace TypingApp.ViewModels;

public class AddGroupViewModel : ViewModelBase , INotifyPropertyChanged
{
    private string _groupCode;
    private string _groupName;

    public ICommand BackButton { get; }
    public ICommand CancelButton { get; }
    public ICommand SaveButton { get; set; }
    public ICommand UpdateGroupCodeButton { get; set; }

    public string GroupCode
    {
        get => _groupCode;
        set
        {
            _groupCode = value;
            OnPropertyChanged();
        }
    }

    public string GroupName
    {
        get => _groupName;
        set
        {
            _groupName = value; 
            OnPropertyChanged();
        }
    }
    
    public AddGroupViewModel(NavigationService studentDashboardNavigationService,
        NavigationService teacherDashboardNavigationService, UserStore userStore)
    { 
        
        
        var student = new NavigateCommand(studentDashboardNavigationService);
        var teacher = new NavigateCommand(teacherDashboardNavigationService);
        
        BackButton = userStore.Teacher == null ? student : teacher;
        CancelButton = new CancelCommand(teacherDashboardNavigationService);
        SaveButton = new CreateGroupCommand(userStore, teacherDashboardNavigationService, this);
        UpdateGroupCodeButton = new UpdateGroupsCodeCommand(this);
    }
    
}