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

namespace TestProject2
{
    public class Tests
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
            var loadedTasks = taskManager.AllTasks().ToList();
            Assert.AreEqual(2, loadedTasks.Count); // �������, ��� ��������� ��� ������
            Assert.AreEqual("Test Task 1", loadedTasks[0].Name);
            Assert.AreEqual("Test Task 2", loadedTasks[1].Name);
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
            var loadedTasks = taskManager.AllTasks().ToList();
            Assert.AreEqual(2, loadedTasks.Count); // �������, ��� ��������� ��� ������
            Assert.AreEqual("Test Task 1", loadedTasks[0].Name);
            Assert.AreEqual("Test Task 2", loadedTasks[1].Name);
        }

        /*[Test]
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
            var loadedTasks = taskManager.AllTasks().ToList();
            Assert.AreEqual(2, loadedTasks.Count); // �������, ��� ��������� ��� ������
            Assert.AreEqual("Test Task 1", loadedTasks[0].Name);
            Assert.AreEqual("Test Task 2", loadedTasks[1].Name);
        }*/

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
}