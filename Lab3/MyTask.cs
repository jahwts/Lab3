using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Data.Sqlite;

namespace Lab3
{
    public class MyTask
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsDone { get; set; }
    }
}
