using System;
using System.Collections.Generic;
using System.Data;

namespace TypingApp.Services.DatabaseProviders;

public class LessonProvider : BaseProvider
{
    public override Dictionary<string, object>? GetById(int id)
    {
        var cmd = GetSqlCommand();
        cmd.CommandText = "SELECT * FROM [Lesson] WHERE id = @id";
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
        var reader = cmd.ExecuteReader();
        
        return ConvertToList(reader, "LessonProvider.GetById")?[0];
    }
    
    public  List<Dictionary<string, object>>? GetExercises(int lessonId)
    {
        var cmd = GetSqlCommand();
        cmd.CommandText = "SELECT e.id, e.name, e.text FROM [Exercise] e JOIN [Lesson_Exercise] le ON e.id = le.exercise_id WHERE le.lesson_id = @lessonId";
        cmd.Parameters.Add("@lessonId", SqlDbType.Int).Value = lessonId;
        var reader = cmd.ExecuteReader();
        
        return ConvertToList(reader, "LessonProvider.GetExercises");
    }

    public Dictionary<string, object>? Create(string name, int teacherId)
    {
        var cmd = GetSqlCommand();
        cmd.CommandText = "INSERT INTO [Lesson] (name, teacher_id) VALUES (@name, @teacherId); SELECT SCOPE_IDENTITY()";
        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
        cmd.Parameters.Add("@teacherId", SqlDbType.Int).Value = teacherId;
        var id = (decimal)cmd.ExecuteScalar();
        
        return GetById((int)id);
    }
    
    public Dictionary<string, object>? LinkToGroup(int groupId, int lessonId)
    {
        // Create link between student and group.
        var cmd = GetSqlCommand();
        cmd.CommandText = "INSERT INTO [Group_Lesson] (group_id, lesson_id) VALUES (@groupId, @lessonId); SELECT SCOPE_IDENTITY()";
        cmd.Parameters.Add("@groupId", SqlDbType.Int).Value = groupId;
        cmd.Parameters.Add("@lessonId", SqlDbType.Int).Value = lessonId;
        var id = (decimal)cmd.ExecuteScalar();
        
        // Retrieve the newly created link.
        cmd = GetSqlCommand();
        cmd.CommandText = "SELECT * FROM [Group_Lesson] WHERE id = @id";
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = (int)id;
        var reader = cmd.ExecuteReader();
        
        return ConvertToList(reader, "LessonProvider.LinkToGroup")?[0];
    }
    
    public List<Dictionary<string, object>>? GetLinkedGroups(int lessonId)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "SELECT g.* FROM [Group] g JOIN [Group_Lesson] gl ON g.id = gl.group_id WHERE gl.lesson_id = @lessonId";
            cmd.Parameters.Add("@lessonId", SqlDbType.Int).Value = lessonId;
            var reader = cmd.ExecuteReader();
            
            return ConvertToList(reader, "LessonProvider.GetLinkedGroups");
        }
            
        public Dictionary<string, object>? DeleteLinksToGroups(int lessonId)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "DELETE FROM [Group_Lesson] WHERE lesson_id = @lessonId";
            cmd.Parameters.Add("@lessonId", SqlDbType.Int).Value = lessonId;
            var reader = cmd.ExecuteReader();

            return ConvertToList(reader, "AdminProvider.DeleteLinksToGroups")?[0];
        }
        
        public void DeleteLinksToExercises(int lessonId)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "DELETE FROM [Lesson_Exercise] WHERE lesson_id = @lessonId";
            cmd.Parameters.Add("@lessonId", SqlDbType.Int).Value = lessonId;
            var reader = cmd.ExecuteReader();
            
            ConvertToList(reader, "AdminProvider.DeleteLinksToExercises");
        }
        
        public Dictionary<string, object>? UpdateLesson(int lessonId, string name, int teacherId)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "UPDATE [Lesson] SET [name] = @name, teacher_id = @teacherId WHERE id = @lessonId; SELECT SCOPE_IDENTITY()";
            cmd.Parameters.Add("@lessonId", SqlDbType.Int).Value = lessonId;
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            cmd.Parameters.Add("@teacherId", SqlDbType.Int).Value = teacherId;
            cmd.ExecuteNonQuery();

            return GetById(lessonId);
        }
}
