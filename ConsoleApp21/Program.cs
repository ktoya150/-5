using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

// Абстрактный класс Employee
public abstract class Employee
{
    public string Name { get; set; }
    public int Experience { get; set; }

    public Employee() { }
    public Employee(string name, int experience)
    {
        Name = name;
        Experience = experience;
    }

    public abstract void DisplayInfo();

    // Проверка наличия буквы 'і' в имени
    public bool HasLetterI()
    {
        return Name.Contains('і');
    }
}

// Интерфейс ICashier
public interface ICashier
{
    void SellTickets();
}

// Интерфейс ITechnicalStaff
public interface ITechnicalStaff
{
    void MaintainEquipment();
}

// Класс Cashier
public class Cashier : Employee, ICashier
{
    public Cashier() { }
    public Cashier(string name, int experience) : base(name, experience) { }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Касир: {Name}, Досвід: {Experience} років");
        if (HasLetterI())
        {
            Console.WriteLine("Ім'я містить букву 'і'.");
        }
    }

    public void SellTickets()
    {
        Console.WriteLine($"{Name} продає квитки.");
    }
}

// Класс Technician
public class Technician : Employee, ITechnicalStaff
{
    public Technician() { }
    public Technician(string name, int experience) : base(name, experience) { }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Технічний працівник: {Name}, Досвід: {Experience} років");
        if (HasLetterI())
        {
            Console.WriteLine("Ім'я містить букву 'і'.");
        }
    }

    public void MaintainEquipment()
    {
        Console.WriteLine($"{Name} обслуговує обладнання.");
    }
}

class Program
{
    static List<Employee> employees = new List<Employee>();

    static void Main(string[] args)
    {
        int choice;
        do
        {
            Console.WriteLine("\nСистема керування персоналом у кінотеатрі:");
            Console.WriteLine("1. Додати працівника");
            Console.WriteLine("2. Показати всіх працівників");
            Console.WriteLine("3. Зберегти працівників у файл");
            Console.WriteLine("4. Завантажити працівників із файлу");
            Console.WriteLine("0. Вихід");
            Console.Write("Оберіть дію: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddEmployee();
                    break;
                case 2:
                    DisplayEmployees();
                    break;
                case 3:
                    SaveEmployees();
                    break;
                case 4:
                    LoadEmployees();
                    break;
                case 0:
                    Console.WriteLine("Вихід...");
                    break;
                default:
                    Console.WriteLine("Неправильний вибір. Спробуйте ще раз.");
                    break;
            }
        } while (choice != 0);
    }

    static void AddEmployee()
    {
        Console.WriteLine("Додати працівника:");
        Console.WriteLine("1. Касир");
        Console.WriteLine("2. Технічний працівник");
        Console.Write("Оберіть тип працівника: ");
        int type = int.Parse(Console.ReadLine());

        Console.Write("Введіть ім'я: ");
        string name = Console.ReadLine();
        Console.Write("Введіть досвід роботи (років): ");
        int experience = int.Parse(Console.ReadLine());

        if (type == 1)
        {
            employees.Add(new Cashier(name, experience));
            Console.WriteLine("Касир доданий.");
        }
        else if (type == 2)
        {
            employees.Add(new Technician(name, experience));
            Console.WriteLine("Технічний працівник доданий.");
        }
        else
        {
            Console.WriteLine("Неправильний вибір типу працівника.");
        }
    }

    static void DisplayEmployees()
    {
        Console.WriteLine("\nСписок працівників:");
        foreach (var employee in employees)
        {
            employee.DisplayInfo();
        }
    }

    static void SaveEmployees()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>), new Type[] { typeof(Cashier), typeof(Technician) });
            using (FileStream fs = new FileStream("employees.xml", FileMode.Create))
            {
                serializer.Serialize(fs, employees);
            }
            Console.WriteLine("Дані працівників збережені у файл employees.xml.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка під час збереження: {ex.Message}");
        }
    }

    static void LoadEmployees()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>), new Type[] { typeof(Cashier), typeof(Technician) });
            using (FileStream fs = new FileStream("employees.xml", FileMode.Open))
            {
                employees = (List<Employee>)serializer.Deserialize(fs);
            }
            Console.WriteLine("Дані працівників завантажені з файлу employees.xml.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка під час завантаження: {ex.Message}");
        }
    }
}
