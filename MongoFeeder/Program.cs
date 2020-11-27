using System;
using MongoFeeder.Services;

using System.Collections.Generic;

namespace MongoFeeder
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertIntoDatabase.StartProgram();
            ShowFromDatabase.ShowAllRecords();
        }
    }
}