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

    static string ReverseString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    static IEnumerable<int> FindDuplicates(IEnumerable<int> numbers)
    {
        var counts = new Dictionary<int, int>();
        
        foreach (var n in numbers)
        {
           counts.TryGetValue(n, out int count);
           counts[n] = count + 1;
        }

        return counts.Where(kvp => kvp.Value > 1).Select(kvp => kvp.Key);
    }

    static int FindLargest(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
            throw new ArgumentException("Array cannot be null or empty.");

        int max = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] > max)
                max = numbers[i];
        }
        return max;
    }

    static Dictionary<char, int> CountCharacters(string input)
    {
        var counts = new Dictionary<char, int>();

        foreach (var c in input)
        {
            counts.TryGetValue(c, out int count);
            counts[c] = count + 1;
        }

        return counts;
    }

    record Person(string Name, int Age);

    static List<Person> GetAdults(List<Person> people)
    {
        return people.Where(p => p.Age >= 18).ToList();
    }
}

record Product(int Id, string Name, decimal Price);

class ProductRepository
{
    private readonly List<Product> _products = new();
    private int _nextId = 1;


    // CREATE
    public Product CreateProduct()
    {
        var product = new Product(_nextId, $"Product {_nextId}", _nextId * 10.0m);
        _products.Add(product);
        _nextId++;
        return product;
    }

    // READ - all
    public IEnumerable<Product> GetAllProducts() => _products.AsReadOnly();

    // READ - by id
    public Product? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public bool UpdateProduct(int id, string name, decimal price)
    {
        var product = GetProductById(id);
        if (product == null) return false;

        // Records are immutable - replace with a new instance
        var index = _products.IndexOf(product);
        _products[index] = product with { Name = name, Price = price }; 
        return true;
    }

    // DELETE
    public bool DeleteProduct(int id)
    {
        var product = GetProductById(id);
        if (product == null) return false;
        _products.Remove(product);
        return true;
    }
}