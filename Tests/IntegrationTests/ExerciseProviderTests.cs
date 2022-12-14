using NUnit.Framework;
using Tests.Attributes;
using TypingApp.Models;
using TypingApp.Services.DatabaseProviders;
using TypingApp.Services.PasswordHash;

namespace Tests.IntegrationTests;

[TestFixture]
public class ExerciseProviderTests
{
    private string _firstName = null!;
    private string _lastName = null!;
    private PasswordHash _hash = null!;
    private byte[] _password = null!;
    private string _groupName = null!;
    private string _groupCode = null!;
    private string _lessonName = null!;
    [SetUp]
    public void Setup()
    {
        _firstName = "If you see this in the database, ";
        _lastName = "it means something went wrong while unit testing";
        _groupName = "If you see this in the database, it means something went wrong while unit testing";
        _lessonName = "If you see this in the database, it means something went wrong while unit testing";
        _groupCode = "UnitTest";
        _hash = new PasswordHash("UnitTest");
        _password = _hash.ToArray();
    }
    
    [Test, Rollback]
    public void ExerciseProvider_Create_Should_ReturnCreatedExercise_When_Called()
    {
            // Arrange
            const string firstName = "If you see this in the database, ";
            const string lastName = "it means something went wrong while unit testing";
            var hash = new PasswordHash("UnitTest");
            var password = hash.ToArray();

            var teacher = new TeacherProvider().Create("unit@test.nl", password, hash.Salt, firstName, null, lastName);
            if (teacher == null) Assert.Fail("Teacher could not be created");

            // Act
            var exercise = new ExerciseProvider().Create((int)teacher?["id"]!, "UnitTest", "UnitTest");

            // Assert
            Assert.AreEqual("UnitTest", exercise?["name"]);
    }

    [Test, Rollback]
    public void ExerciseProvider_GetById_Should_ReturnCorrectExercise_When_GivenId()
    {
        // Arrange
        const string firstName = "If you see this in the database, ";
        const string lastName = "it means something went wrong while unit testing";
        var hash = new PasswordHash("UnitTest");
        var password = hash.ToArray();

        var teacher = new TeacherProvider().Create("unit@test.nl", password, hash.Salt, firstName, null, lastName);
        if (teacher == null) Assert.Fail("Teacher could not be created");

        const string exerciseName = "UnitTest";
        const string exerciseText =
            "If you see this exercise in the database, it means something went wrong while unit testing";
        var newExercise = new ExerciseProvider().Create((int)teacher?["id"], exerciseName, exerciseText);
        if (newExercise == null) Assert.Fail("Exercise could not be created");

        // Act
        var exercise = new ExerciseProvider().GetById((int)newExercise["id"]);

        // Assert
        Assert.AreEqual("UnitTest", exercise?["name"]);
    }

    [Test, Rollback]
    public void ExerciseProvider_GetAll_Should_ReturnAllExercises_When_GivenTeacherId()
    {
        // Arrange
        const string firstName = "If you see this in the database, ";
        const string lastName = "it means something went wrong while unit testing";
        var hash = new PasswordHash("UnitTest");
        var password = hash.ToArray();

        var teacher = new TeacherProvider().Create("unit@test.nl", password, hash.Salt, firstName, null, lastName);
        if (teacher == null) Assert.Fail("Teacher could not be created");

        const string exerciseName = "UnitTest";
        const string exerciseText =
            "If you see this exercise in the database, it means something went wrong while unit testing";
        var newExercise = new ExerciseProvider().Create((int)teacher?["id"], exerciseName, exerciseText);
        if (newExercise == null) Assert.Fail("Exercise could not be created");

        // Act
        var exercises = new ExerciseProvider().GetAll((int)teacher?["id"]);

        // Assert
        Assert.AreEqual(1, exercises.Count);
    }
    [Test, Rollback]
    public void ExerciseProvider_LinkToLesson_Should_LinkLessonToExerciseAndGetExerciseLessonLink_When_GivenLessonIdAndExerciseId()
    {
        // Arrange
        var teacher = new TeacherProvider().Create("unit@test.nl", _password, _hash.Salt, _firstName, null, _lastName);
        if (teacher == null) Assert.Fail("Teacher could not be created");
        var lesson = new LessonProvider().Create(_lessonName, (int)teacher?["id"]!);
        if (lesson == null) Assert.Fail("Lesson could not be created");
        var exercise = new ExerciseProvider().Create((int)teacher?["id"]!, "UnitTest", "UnitTest");
        if (exercise == null) Assert.Fail("Exercise could not be created");

        // Act
        var Exercise_lessonLink = new ExerciseProvider().LinkToLesson((int)lesson?["id"]!, (int)exercise?["id"]!);

        // Assert
        Assert.IsNotNull(Exercise_lessonLink);

    }
}