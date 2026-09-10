using System;
using System.Linq;
using System.Threading;
using Microsoft.Data.Sqlite;

namespace ConsoleApp1;


public class Person
{
    // Field - private, internal storage
    private int _age;

    // Property - public interface to the field
    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    public string Name { get; set; }
    public DateTime CreatedAt {get;} = DateTime.Now;
}

class Program
{
    static void Main(string[] args)
    {   

        // we have to find out the product so that you can gain the maximum number.
        // any of two numbers product but it should be a maximum.
        // what will be the maxinumb and receive from two numbers?
        // to bring out that result.
        // int [] nums = {1, 2, 3, 4, 5, 6};// See https://aka.ms/new-console-template for more information
        // var result = MaxProduct(nums);
        // Console.WriteLine($"The maximum product of two numbers in the array is: {result}");

        // // how to merge two objects in C#?
        // var obj1 = new { Name = "John", Age = 30 };
        // var obj2 = new { Name = "Jane", Age = 25 };

        // var obj3 = new[] { obj1, obj2 };

        // Console.WriteLine($"Merged Object: Name = {obj3[0].Name}, Age = {obj3[0].Age}");
   
        // PrintAll("Hello", "World", "This", "is", "a", "test");

        // new Thread(() =>
        // {
        //     Thread.Sleep(2000);
        //     Console.WriteLine("Thread 1");
        // }).Start(); 


        // // C# Generic Class Example
        // var intBox = new Box<int> { Content = 42 };
        // intBox.LogContent();

        // var stringBox = new Box<string> { Content = "Hello, Generics!" };
        // stringBox.LogContent();

        // /*

        // LINQ = retriving data from a data source. By using a LINQ Query Operation you can get data from different types of data sources.

        // */

        // string [] fruits = { "Apple", "Banana", "Cherry", "Date" };
        // var query = from fruit in fruits
        //             orderby fruit ascending
        //             select fruit;
        
        // foreach (var i in query)
        // {
        //     Console.WriteLine(i);
        // }

        // What happends on assignment:
        // Value type - independent copy
        // int a = 5;
        // int b = a; // b is now 5, a copy of the value of a
        // b = 10; // changing b does not affect a
        // Console.WriteLine($"a: {a}, b: {b}"); // Output: a: 5, b: 10

        // // Reference type - shared reference
        // var list1 = new List<int> {1 , 2};
        // var list2 = list1;
        // list2.Add(3); // modifying list2 affects list1
        // Console.WriteLine($"list1: [{string.Join(", ", list1)}], list2: [{string.Join(", ", list2)}]"); // Output: list1: [1, 2, 3], list2: [1, 2, 3]

        // if/else - categorize a number
        int number = 5;
        if (number < 0)
            Console.WriteLine("Negative");
        else if (number == 0)
            Console.WriteLine("Zero");
        else
            Console.WriteLine("Positive");

        string category = number switch
        {
          < 0 => "Negative",
          0 => "Zero",
          > 0 => "Positive",  
        };    
        // Console.ReadLine(); 
    }

    static int MaxProduct (int[] arr)
    {
        var desc = arr.OrderByDescending(x => x).ToArray();
        return desc[0] * desc[1];
    }

    static void PrintAll(params string[] items)
    {
        foreach (var item in items) Console.WriteLine(item);
    }

    static void ShowProducts()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        Console.WriteLine("SQLite connection opened.");

        using var command = new SqliteCommand("CREATE TABLE Products (Id INTEGER PRIMARY KEY, Name TEXT, Price REAL)", connection);
        command.ExecuteNonQuery();

        using var insertCommand = new SqliteCommand("INSERT INTO Products (Name, Price) VALUES (@name, @price)", connection);
        insertCommand.Parameters.AddWithValue("@name", "Product A");
        insertCommand.Parameters.AddWithValue("@price", 10.99);
        insertCommand.ExecuteScalar();
    }
}