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
        testFilePath = "test_tasks.json"; // ���� � ��������� �����, ������� ����� ������ � ������ ����� ������
    }

    [Test]
    public void SaveAndLoadTasksToJson_ShouldMatchAfterLoading()
    {
        // ��������� �������� ������
        taskManager.AddTask(new MyTask { Name = "Test Task 1", Priority = 1, Deadline = DateTime.Now });
        taskManager.AddTask(new MyTask { Name = "Test Task 2", Priority = 2, Deadline = DateTime.Now });

        // ��������� � JSON ����
        taskManager.SaveTasksToJson(testFilePath);

        // ��������� �� JSON �����
        taskManager.LoadTasksFromJson(testFilePath);

        // ���������, ��� ����������� ������ ������������� ���������
        Assert.AreEqual(2, taskManager.AllTasks().Count); // �������, ��� ��������� ��� ������
        Assert.AreEqual("Test Task 1", taskManager.AllTasks()[0].Name);
        Assert.AreEqual("Test Task 2", taskManager.AllTasks()[1].Name);
    }

    [Test]
    public void SaveAndLoadTasksToXml_ShouldMatchAfterLoading()
    {
        // ��������� �������� ������
        taskManager.AddTask(new MyTask { Name = "Test Task 1", Priority = 1, Deadline = DateTime.Now });
        taskManager.AddTask(new MyTask { Name = "Test Task 2", Priority = 2, Deadline = DateTime.Now });

        // ��������� � XML ����
        taskManager.SaveTasksToXml(testFilePath);

        // ��������� �� XML �����
        taskManager.LoadTasksFromXml(testFilePath);

        // ���������, ��� ����������� ������ ������������� ���������
        Assert.AreEqual(2, taskManager.AllTasks().Count); // �������, ��� ��������� ��� ������
        Assert.AreEqual("Test Task 1", taskManager.AllTasks()[0].Name);
        Assert.AreEqual("Test Task 2", taskManager.AllTasks()[1].Name);
    }

    [Test]
    public void SaveAndLoadTasksToSQLite_ShouldMatchAfterLoading()
    {
        // ��������� �������� ������
        taskManager.AddTask(new MyTask { Name = "Test Task 1", Priority = 1, Deadline = DateTime.Now });
        taskManager.AddTask(new MyTask { Name = "Test Task 2", Priority = 2, Deadline = DateTime.Now });

        // ��������� � SQLite ���� ������
        taskManager.SaveTasksToSQLite("Data Source=test_tasks.db");

        // ��������� �� SQLite ���� ������
        taskManager.LoadTasksFromSQLite("Data Source=test_tasks.db");

        // ���������, ��� ����������� ������ ������������� ���������
        Assert.AreEqual(2, taskManager.AllTasks().Count); // �������, ��� ��������� ��� ������
        Assert.AreEqual("Test Task 1", taskManager.AllTasks()[0].Name);
        Assert.AreEqual("Test Task 2", taskManager.AllTasks()[1].Name);
    }

    [TearDown]
    public void TearDown()
    {
        // �������� ��������� ����� ����� ���������� ������
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }
}