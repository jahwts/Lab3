using Lab3;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Data.Sqlite;

[TestFixture]
public class TaskManagerSaveLoadTests
{
    private TaskManager taskManager;
    private string testFilePath;

    [SetUp]
    public void Setup()
    {
        taskManager = new TaskManager();
        testFilePath = "test_tasks.json"; // Путь к тестовому файлу, который будет создан и удален после тестов
    }

    [Test]
    public void SaveAndLoadTasksToJson_ShouldMatchAfterLoading()
    {
        // Добавляем тестовые задачи
        taskManager.AddTask(new MyTask { Name = "Test Task 1", Priority = 1, Deadline = DateTime.Now });
        taskManager.AddTask(new MyTask { Name = "Test Task 2", Priority = 2, Deadline = DateTime.Now });

        // Сохраняем в JSON файл
        taskManager.SaveTasksToJson(testFilePath);

        // Загружаем из JSON файла
        taskManager.LoadTasksFromJson(testFilePath);

        // Проверяем, что загруженные задачи соответствуют ожидаемым
        Assert.AreEqual(2, taskManager.AllTasks().Count); // Ожидаем, что загружено две задачи
        Assert.AreEqual("Test Task 1", taskManager.AllTasks()[0].Name);
        Assert.AreEqual("Test Task 2", taskManager.AllTasks()[1].Name);
    }

    [Test]
    public void SaveAndLoadTasksToXml_ShouldMatchAfterLoading()
    {
        // Добавляем тестовые задачи
        taskManager.AddTask(new MyTask { Name = "Test Task 1", Priority = 1, Deadline = DateTime.Now });
        taskManager.AddTask(new MyTask { Name = "Test Task 2", Priority = 2, Deadline = DateTime.Now });

        // Сохраняем в XML файл
        taskManager.SaveTasksToXml(testFilePath);

        // Загружаем из XML файла
        taskManager.LoadTasksFromXml(testFilePath);

        // Проверяем, что загруженные задачи соответствуют ожидаемым
        Assert.AreEqual(2, taskManager.AllTasks().Count); // Ожидаем, что загружено две задачи
        Assert.AreEqual("Test Task 1", taskManager.AllTasks()[0].Name);
        Assert.AreEqual("Test Task 2", taskManager.AllTasks()[1].Name);
    }

    [Test]
    public void SaveAndLoadTasksToSQLite_ShouldMatchAfterLoading()
    {
        // Добавляем тестовые задачи
        taskManager.AddTask(new MyTask { Name = "Test Task 1", Priority = 1, Deadline = DateTime.Now });
        taskManager.AddTask(new MyTask { Name = "Test Task 2", Priority = 2, Deadline = DateTime.Now });

        // Сохраняем в SQLite базу данных
        taskManager.SaveTasksToSQLite("Data Source=test_tasks.db");

        // Загружаем из SQLite базы данных
        taskManager.LoadTasksFromSQLite("Data Source=test_tasks.db");

        // Проверяем, что загруженные задачи соответствуют ожидаемым
        Assert.AreEqual(2, taskManager.AllTasks().Count); // Ожидаем, что загружено две задачи
        Assert.AreEqual("Test Task 1", taskManager.AllTasks()[0].Name);
        Assert.AreEqual("Test Task 2", taskManager.AllTasks()[1].Name);
    }

    [TearDown]
    public void TearDown()
    {
        // Удаление тестового файла после выполнения тестов
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }
}