using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TypingApp.Commands;
using TypingApp.Models;
using TypingApp.Services.DatabaseProviders;
using TypingApp.Stores;
using NavigationService = TypingApp.Services.NavigationService;

namespace TypingApp.ViewModels;

public class MyLessonsViewModel : ViewModelBase
{
    private ObservableCollection<Lesson>? _lessons;
    private ObservableCollection<Exercise>? _exercises;
    private Lesson? _selectedLesson;
    private Exercise? _selectedExercise;

    public ICommand BackButton { get; }
    public ICommand CreateExerciseButton { get; }
    
    public ObservableCollection<Lesson>? Lessons
    {
        get => _lessons;
        set
        {
            if (value == null) return;
            _lessons = value;
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<Exercise>? Exercises
    {
        get => _exercises;
        set
        {
            if (value == null) return;
            _exercises = value;
            OnPropertyChanged();
        }
    }
    public Lesson SelectedLesson
    {
        get => _selectedLesson;
        set
        {
            _selectedLesson = value;
            OnPropertyChanged();
        }
    }

    public Exercise SelectedExercise
    {
        get => _selectedExercise;
        set
        {
            _selectedExercise = value;
            OnPropertyChanged();
        }
    }

    

    public MyLessonsViewModel(NavigationService teacherDashboardNavigationService, NavigationService createExerciseNavigationService, UserStore userStore)
    {
        BackButton = new NavigateCommand(teacherDashboardNavigationService);
        CreateExerciseButton = new NavigateCommand(createExerciseNavigationService);
        Exercises = new ObservableCollection<Exercise>();
        Lessons = new ObservableCollection<Lesson>();

        // Populate Exercises
        if (userStore.Teacher == null) return;
        var exercises = new ExerciseProvider().GetAll(userStore.Teacher.Id);
        exercises?.ForEach(e => Exercises?.Add(new Exercise((string)e["text"])));

        //Test Exercises
        Exercises.Add(new Exercise("Test"));
        Exercises.Add(new Exercise("Test"));
        Exercises.Add(new Exercise("Test"));
        Exercises.Add(new Exercise("Test"));
        Exercises.Add(new Exercise("Test"));
        Exercises.Add(new Exercise("Test"));
        Exercises.Add(new Exercise("Test"));
        Exercises.Add(new Exercise("Test"));
        Exercises.Add(new Exercise("Test"));
        Exercises.Add(new Exercise("Test"));
        Exercises.Add(new Exercise("Test"));
        Exercises.Add(new Exercise("Test"));


        //Test Lessons
        Lessons.Add(new Lesson("Test", "TestTeacher", 1));
        Lessons.Add(new Lesson("Test", "TestTeacher", 1));
        Lessons.Add(new Lesson("Test", "TestTeacher", 1));
        Lessons.Add(new Lesson("Test", "TestTeacher", 1));
        Lessons.Add(new Lesson("Test", "TestTeacher", 1));
        Lessons.Add(new Lesson("Test", "TestTeacher", 1));
        Lessons.Add(new Lesson("Test", "TestTeacher", 1));
        Lessons.Add(new Lesson("Test", "TestTeacher", 1));
        Lessons.Add(new Lesson("Test", "TestTeacher", 1));
        Lessons.Add(new Lesson("Test", "TestTeacher", 1));
        Lessons.Add(new Lesson("Test", "TestTeacher", 1));



    }
}