using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TCL_Framework;
using TCL_Framework.Abstractions;
using TCL_Framework.TCLPostgreSQL;

public class Demo
{
    static void Main(string[] args)
    {
        // Display the number of command line arguments.
        Console.WriteLine(args.Length);

        TCLConnection connection =
            TCLPostgreSQLConnection.GetInstance(
                "Server=127.0.0.1;Port=5432;Database=tcl-framwork-demo;User Id=admin;Password=admin;");
        connection.Open();
        List<StudentGroup> students = connection
            .Select<StudentGroup>()
            .AllRow()
            .GroupBy("class_id")
            .Having("count(*) <= 2")
            .Run();
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }
}