
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string fileName = "StudentData.txt";

        List<Student> students = ReadStudentData(fileName);

        if (students.Count > 0)
        {
            Console.WriteLine("Student data before sorting:");
            DisplayStudentData(students);

            students.Sort((s1, s2) => string.Compare(s1.Name, s2.Name, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("\nStudent data after sorting by name:");
            DisplayStudentData(students);

            Console.WriteLine("\nEnter a student name to search:");
            string searchName = Console.ReadLine();

            SearchStudentByName(students, searchName);
        }
        else
        {
            Console.WriteLine("No student data found in the file.");
        }

        Console.ReadLine();
    }

    static List<Student> ReadStudentData(string fileName)
    {
        List<Student> students = new List<Student>();

        try
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(',');

                    if (parts.Length == 2)
                    {
                        students.Add(new Student(parts[0].Trim(), parts[1].Trim()));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
        }

        return students;
    }

    static void DisplayStudentData(List<Student> students)
    {
        foreach (var student in students)
        {
            Console.WriteLine($"{student.Name}, {student.Class}");
        }
    }

    static void SearchStudentByName(List<Student> students, string searchName)
    {
        var result = students.FindAll(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

        if (result.Count > 0)
        {
            Console.WriteLine($"\nSearch results for '{searchName}':");
            DisplayStudentData(result);
        }
        else
        {
            Console.WriteLine($"No student found with the name '{searchName}'.");
        }
    }
}

class Student
{
    public string Name { get; set; }
    public string Class { get; set; }

    public Student(string name, string studentClass)
    {
        Name = name;
        Class = studentClass;
    }
}
