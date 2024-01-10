using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Data.Sqlite;

namespace Lab3
{
    public class TaskManager
    {
        public readonly List<MyTask> taskList = new List<MyTask>();

        public void AddTask(MyTask task)
        {
            taskList.Add(task);
        }

        public void RemoveTask(MyTask task)
        {
            if (taskList.Contains(task))
            {
                taskList.Remove(task);
                Console.WriteLine($"Задача \"{task.Name}\" успешно удалена.");
            }
            else
            {
                Console.WriteLine("Такая задача не найдена в списке.");
            }
        }

        public IEnumerable<MyTask> GetTopPriorityTasks()
        {
            var highestPriorityTasks = taskList.Where(task => !task.IsDone)
                                                .OrderByDescending(task => task.Priority)
                                                .Take(1)
                                                .ToList();

            return highestPriorityTasks;
        }

        public IEnumerable<MyTask> GetClosestDeadlineTasks()
        {
            var closestDeadlineTasks = taskList.Where(task => !task.IsDone)
                                               .OrderBy(task => task.Deadline)
                                               .Take(1)
                                               .ToList();

            return closestDeadlineTasks;
        }

        public IEnumerable<MyTask> ShowAllTasks()
        {
            return taskList;
        }

        public IEnumerable<MyTask> FindTasksByPriority(int priority)
        {
            return taskList.Where(task => task.Priority == priority).ToList();
        }

        public IEnumerable<MyTask> AllTasks()
        {
            return taskList;
        }

        public void SaveTasksToJson(string filePath)
        {
            try
            {
                var json = JsonSerializer.Serialize(taskList);
                File.WriteAllText(filePath, json);
                Console.WriteLine("Задачи успешно сохранены в JSON файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении задач в JSON: {ex.Message}");
            }
        }

        public void LoadTasksFromJson(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    taskList.Clear();
                    taskList.AddRange(JsonSerializer.Deserialize<List<MyTask>>(json));
                    Console.WriteLine("Задачи успешно загружены из JSON файла.");
                }
                else
                {
                    Console.WriteLine("Файл не существует. Загрузка не выполнена.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке задач из JSON: {ex.Message}");
            }
        }

        public void SaveTasksToXml(string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<MyTask>));
                using (var stream = new StreamWriter(filePath))
                {
                    serializer.Serialize(stream, taskList);
                    Console.WriteLine("Задачи успешно сохранены в XML файл.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении задач в XML: {ex.Message}");
            }
        }

        public void LoadTasksFromXml(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var serializer = new XmlSerializer(typeof(List<MyTask>));
                    using (var stream = new StreamReader(filePath))
                    {
                        taskList.Clear();
                        taskList.AddRange((List<MyTask>)serializer.Deserialize(stream));
                        Console.WriteLine("Задачи успешно загружены из XML файла.");
                    }
                }
                else
                {
                    Console.WriteLine("Файл не существует. Загрузка не выполнена.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке задач из XML: {ex.Message}");
            }
        }

        public void SaveTasksToSQLite(string connectionString)
        {
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Tasks (
                            Title TEXT,
                            Priority INTEGER,
                            Deadline TEXT,
                            IsCompleted INTEGER
                        )";
                        command.ExecuteNonQuery();

                        foreach (var task in taskList)
                        {
                            command.CommandText = $@"
                            INSERT INTO Tasks (Title, Priority, Deadline, IsCompleted)
                            VALUES ('{task.Name}', {task.Priority}, '{task.Deadline:yyyy-MM-dd}', {(task.IsDone ? 1 : 0)})";

                            command.ExecuteNonQuery();
                        }

                        Console.WriteLine("Задачи успешно сохранены в базе данных SQLite.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении задач в SQLite: {ex.Message}");
            }
        }

        public void LoadTasksFromSQLite(string connectionString)
        {
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Tasks";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                taskList.Add(new MyTask
                                {
                                    Name = reader.GetString(0),
                                    Priority = reader.GetInt32(1),
                                    Deadline = DateTime.Parse(reader.GetString(2)),
                                    IsDone = reader.GetInt32(3) == 1
                                });
                            }
                        }

                        Console.WriteLine("Задачи успешно загружены из базы данных SQLite.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке задач из SQLite: {ex.Message}");
            }
        }
    }
}
