# Library Management System

A comprehensive **C# .NET Console Application** demonstrating advanced Object-Oriented Programming principles, generics, and file-based data persistence using JSON format.

## 🚀 Project Overview

This Library Management System is a feature-rich console application built with **C# .NET** that showcases professional software development practices. The system manages books, members, and library transactions while implementing all four pillars of Object-Oriented Programming and advanced C# features.

## 🎯 Key Features

- **Book Management**: Add, search, update, and manage book inventory
- **Member Management**: Register members with different privilege levels
- **Transaction System**: Issue and return books with automatic fine calculation
- **Advanced Search**: Multi-criteria search functionality using generics
- **Data Persistence**: JSON-based file storage system
- **Reporting**: Overdue books, member activity, and inventory reports
- **Fine Management**: Automatic calculation and tracking of late fees

## 🔧 C# OOP Implementation - Four Pillars

### 1. **Encapsulation** 🔒
- **Private Fields**: Internal data protection with controlled access
- **Properties**: Public getters/setters with validation logic
- **Access Modifiers**: Strategic use of private, protected, and public
- **Data Hiding**: Internal implementation details hidden from external access

```csharp
public class Member : Person
{
    public int MaxBooksAllowed { get; private set; }  // Read-only property
    private decimal fineAmount;                        // Private field
    
    // Encapsulated method - internal logic hidden
    private void SetMaxBooksAllowed()
    {
        MaxBooksAllowed = Type switch
        {
            MemberType.Student => 3,
            MemberType.Faculty => 10,
            MemberType.GeneralPublic => 2,
            _ => 2
        };
    }
}
```

### 2. **Inheritance** 🧬
- **Abstract Base Class**: `Person` class providing common functionality
- **Class Hierarchy**: `Member` inherits from `Person`
- **Code Reusability**: Shared properties and methods across derived classes
- **IS-A Relationship**: Clear hierarchical relationships

```csharp
// Base class with common properties
public abstract class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
    // ... other common properties
}

// Derived class extending base functionality
public class Member : Person
{
    public MemberType Type { get; set; }
    public List<Transaction> BorrowHistory { get; set; }
    // ... member-specific properties
}
```

### 3. **Polymorphism** 🎭
- **Method Overriding**: Virtual and abstract method implementations
- **Interface Implementation**: Multiple interface contracts
- **Runtime Polymorphism**: Dynamic method resolution
- **Compile-time Polymorphism**: Method overloading and generics

```csharp
// Abstract method - must be overridden
public abstract void DisplayInfo();

// Virtual method - can be overridden
public virtual string GetFullDetails() { /* base implementation */ }

// Polymorphic implementation in derived class
public override void DisplayInfo()
{
    // Member-specific implementation
}
```

### 4. **Abstraction** 🎨
- **Abstract Classes**: `Person` class with abstract methods
- **Interfaces**: `IBorrowable`, `ISearchable` defining contracts
- **Implementation Hiding**: Complex logic abstracted behind simple interfaces
- **Contract Definition**: Clear API contracts for different functionalities

```csharp
// Interface defining search contract
public interface ISearchable
{
    bool MatchesSearch(string searchTerm);
}

// Interface defining borrowing behavior
public interface IBorrowable
{
    bool CanBorrow(Member member);
    void BorrowBook(Member member);
    void ReturnBook();
}
```

## Exception Handling
The application features robust exception management throughout file I/O and data operations, ensuring that errors are caught, reported, and do not crash the system.
All file operations, user input translation, and critical business logic are wrapped in error traps for user-friendly error reporting and recovery.

## ⚡ C# Generics and Advanced Features

### Generic Collections and LINQ
```csharp
// Generic collections for type safety
private List<Book> books;
private List<Member> members;
private List<Transaction> transactions;

// LINQ for advanced querying
public List<Book> SearchBooks(string searchTerm)
{
    return books.Where(b => b.MatchesSearch(searchTerm)).ToList();
}

// Generic method with constraints
public T FindEntity<T>(string id, List<T> collection) where T : class
{
    return collection.FirstOrDefault(/* lambda expression */);
}
```

### Advanced C# Features
- **Lambda Expressions**: Functional programming constructs
- **LINQ Queries**: Declarative data querying
- **Nullable Types**: `DateTime?` for optional dates
- **Pattern Matching**: Switch expressions with modern syntax
- **Auto-Properties**: Simplified property declarations
- **Collection Initializers**: Streamlined object initialization

```csharp
// Pattern matching with switch expressions
MaxBooksAllowed = Type switch
{
    MemberType.Student => 3,
    MemberType.Faculty => 10,
    MemberType.GeneralPublic => 2,
    _ => 2
};

// LINQ with lambda expressions
var overdueTransactions = transactions.Where(t => t.IsOverdue()).ToList();
```

## 💾 Data Persistence - JSON File Handling

### Minimal Database Approach
The application uses **JSON files** as a lightweight database alternative, demonstrating:

- **Serialization/Deserialization**: Converting objects to/from JSON
- **File I/O Operations**: Reading and writing data files
- **Data Persistence**: Maintaining state between application runs
- **Error Handling**: Robust file operation exception management

### JSON Output Files
All data files are automatically generated in the **bin folder** during runtime:

```
bin/Debug/net8.0/
├── books.json          # Book inventory data
├── members.json        # Member information
└── transactions.json   # Transaction history
```

### JSON Implementation
```csharp
// Saving data to JSON files
public void SaveData()
{
    var booksJson = JsonSerializer.Serialize(books, new JsonSerializerOptions 
    { 
        WriteIndented = true 
    });
    File.WriteAllText("books.json", booksJson);
}

// Loading data from JSON files
private void LoadData()
{
    if (File.Exists("books.json"))
    {
        var booksJson = File.ReadAllText("books.json");
        books = JsonSerializer.Deserialize<List<Book>>(booksJson) ?? new List<Book>();
    }
}
```

## 🛠️ Prerequisites

- **.NET 8.0 or higher**
- **Visual Studio 2022** or **Visual Studio Code**
- **C# 10.0** language features

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/library-management-system.git
cd library-management-system
```

### 2. Build and Run
```bash
dotnet build
dotnet run
```

### 3. Data Files Location
After running the application, JSON data files will be created in:
```
bin/Debug/net6.0/
```

## 📋 Usage Examples

### Adding a New Book
```
1. Select "Add New Book" from menu
2. Enter ISBN: 978-0134685991
3. Enter Title: Effective Java
4. Enter Author: Joshua Bloch
5. Select Category: Technology
6. Enter Copies: 5
7. Enter Price: 45.99
8. Enter Published Date: 2017-12-27
```

### Registering a Member
```
1. Select "Register New Member"
2. Enter Member ID: S001
3. Enter Name: John Doe
4. Enter Email: john.doe@email.com
5. Enter Phone: 123-456-7890
6. Select Type: Student
```

## 🎯 Learning Outcomes

This project demonstrates mastery of:

### Core OOP Concepts
- ✅ **Encapsulation**: Data protection and controlled access
- ✅ **Inheritance**: Code reuse and hierarchical relationships
- ✅ **Polymorphism**: Method overriding and interface implementation
- ✅ **Abstraction**: Interface design and implementation hiding

### Advanced C# Features
- ✅ **Generics**: Type-safe collections and methods
- ✅ **LINQ**: Declarative data querying
- ✅ **Lambda Expressions**: Functional programming
- ✅ **Pattern Matching**: Modern C# syntax
- ✅ **Nullable Types**: Handling optional values

### Software Engineering Practices
- ✅ **Clean Code**: Readable and maintainable code structure
- ✅ **SOLID Principles**: Object-oriented design principles
- ✅ **Error Handling**: Robust exception management
- ✅ **Data Persistence**: File-based storage implementation


## 📊 Project Statistics

- **Lines of Code**: ~800+
- **Classes**: 8+
- **Interfaces**: 2+
- **Enums**: 3+
- **Methods**: 30+
- **OOP Principles**: All 4 pillars implemented
- **Design Patterns**: Multiple patterns demonstrated

## 🤝 Contributing

This project is designed for educational purposes. Feel free to:
- Fork the repository
- Add new features
- Improve existing functionality
- Submit pull requests
---

**Note**: This project demonstrates professional-level C# programming skills suitable for software development positions requiring strong OOP knowledge and .NET framework expertise.
