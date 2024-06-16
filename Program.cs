using System;
using System.Collections.Generic;

namespace StudentManagement
{
    class Student : IComparable<Student>
    {
        // Поля для хранения данных
        private string? name;
        private string? patronymic;
        private string? surname;
        private DateTime birthDate;
        private string? address;
        private string? phone;

        // Поля для хранения оценок
        private LinkedList<int> marks = new LinkedList<int>(); // Зачёты
        private LinkedList<int> courseworks = new LinkedList<int>(); // Курсовые работы
        private LinkedList<int> exams = new LinkedList<int>(); // Экзамены
        private double rating; // Рейтинг

        // Свойства для полей
        public string? Name
        {
            get { return name; }
            set { name = value; }
        }

        public string? Patronymic
        {
            get { return patronymic; }
            set { patronymic = value; }
        }

        public string? Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public string? Address
        {
            get { return address; }
            set { address = value; }
        }

        public string? Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        // Конструктор без параметров
        public Student() : this("Unknown", "Unknown", "Unknown", DateTime.MinValue, "Unknown", "Unknown")
        {
            Console.WriteLine("Constructor without parameters");
        }

        // Конструктор с параметрами: имя, отчество, фамилия
        public Student(string name, string patronymic, string surname) : this(name, patronymic, surname, DateTime.MinValue, "Unknown", "Unknown")
        {
            Console.WriteLine("Constructor with name, patronymic, surname");
        }

        // Конструктор с параметрами: имя, отчество, фамилия, адрес
        public Student(string name, string patronymic, string surname, string address) : this(name, patronymic, surname, DateTime.MinValue, address, "Unknown")
        {
            Console.WriteLine("Constructor with name, patronymic, surname, address");
        }

        // Основной конструктор с параметрами: имя, отчество, фамилия, дата рождения, адрес, телефон
        public Student(string name, string patronymic, string surname, DateTime birthDate, string address, string phone)
        {
            Name = name;
            Patronymic = patronymic;
            Surname = surname;
            BirthDate = birthDate;
            Address = address;
            Phone = phone;
            Console.WriteLine("Main constructor with all parameters");
        }

        // Методы для добавления оценок в зачёты, курсовые работы и экзамены
        public void AddMark(int value)
        {
            if (value < 1 || value > 12) return;
            marks.AddLast(value);
            ResetRating();
        }

        public void AddCoursework(int value)
        {
            if (value < 1 || value > 12) return;
            courseworks.AddLast(value);
            ResetRating();
        }

        public void AddExam(int value)
        {
            if (value < 1 || value > 12) return;
            exams.AddLast(value);
            ResetRating();
        }

        // Показ всех данных о студенте
        public void PrintStudent()
        {
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Отчество: {Patronymic}");
            Console.WriteLine($"Фамилия: {Surname}");
            Console.WriteLine($"Дата рождения: {BirthDate.ToShortDateString()}");
            Console.WriteLine($"Адрес: {Address}");
            Console.WriteLine($"Номер телефона: {Phone}");
            Console.Write("Оценки по зачётам: ");
            foreach (var mark in marks)
            {
                Console.Write($"{mark} ");
            }
            Console.WriteLine();
            Console.Write("Оценки по курсовым: ");
            foreach (var coursework in courseworks)
            {
                Console.Write($"{coursework} ");
            }
            Console.WriteLine();
            Console.Write("Оценки по экзаменам: ");
            foreach (var exam in exams)
            {
                Console.Write($"{exam} ");
            }
            Console.WriteLine();
            Console.WriteLine($"Рейтинг оценок: {rating:F1}");
        }

        // Метод для пересчета рейтинга
        private void ResetRating()
        {
            int totalGradesCount = marks.Count + courseworks.Count + exams.Count;

            if (totalGradesCount == 0)
            {
                rating = 0;
                return;
            }

            int totalSum = CalculateSum(marks) + CalculateSum(courseworks) + CalculateSum(exams);
            rating = (double)totalSum / totalGradesCount;
        }

        // Метод для вычисления суммы значений
        private int CalculateSum(LinkedList<int> list)
        {
            int sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            return sum;
        }

        // Метод для сортировки студентов по заданному критерию
        public static void Sort(Student[] students, IComparer<Student> comparer)
        {
            Array.Sort(students, comparer);
        }

        // Метод для поиска студента по заданному критерию
        public static Student? Search(Student[] students, Predicate<Student> match)
        {
            foreach (var student in students)
            {
                if (match(student))
                {
                    return student;
                }
            }
            return null;
        }

        // Реализация интерфейса IComparable<Student>
        public int CompareTo(Student? other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        // Вложенные классы для различных критериев сортировки
        public class SortBySurname : IComparer<Student>
        {
            public int Compare(Student? x, Student? y)
            {
                if (x == null || y == null) throw new ArgumentNullException();
                return string.Compare(x.Surname, y.Surname, StringComparison.OrdinalIgnoreCase);
            }
        }

        public class SortByBirthDate : IComparer<Student>
        {
            public int Compare(Student? x, Student? y)
            {
                if (x == null || y == null) throw new ArgumentNullException();
                return DateTime.Compare(x.BirthDate, y.BirthDate);
            }
        }

        public class SortByRating : IComparer<Student>
        {
            public int Compare(Student? x, Student? y)
            {
                if (x == null || y == null) throw new ArgumentNullException();
                return x.rating.CompareTo(y.rating);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Пример создания студентов
            Student student1 = new Student("Василий", "Алибабаевич", "Пупкин", new DateTime(1995, 02, 06), "ул. Литературная, д. 18", "+380955289873");
            Student student2 = new Student("Анна", "Владимировна", "Смирнова", new DateTime(1994, 03, 22), "ул. Цветочная, д. 11", "+380955289874");
            Student student3 = new Student("Петр", "Иванович", "Васечкин", new DateTime(1996, 04, 15), "ул. Заречная, д. 19", "+380955289875");

            student1.AddMark(12);
            student1.AddMark(10);
            student1.AddMark(11);
            student1.AddCoursework(12);
            student1.AddExam(12);

            student2.AddMark(9);
            student2.AddMark(8);
            student2.AddCoursework(10);
            student2.AddExam(11);

            student3.AddMark(7);
            student3.AddCoursework(9);
            student3.AddExam(10);

            Student[] students = { student1, student2, student3 };

            // Сортировка по имени
            Student.Sort(students, Comparer<Student>.Default);
            Console.WriteLine("Сортировка по имени:");
            foreach (var student in students)
            {
                student.PrintStudent();
                Console.WriteLine();
            }

            // Сортировка по фамилии
            Student.Sort(students, new Student.SortBySurname());
            Console.WriteLine("Сортировка по фамилии:");
            foreach (var student in students)
            {
                student.PrintStudent();
                Console.WriteLine();
            }

            // Сортировка по дате рождения
            Student.Sort(students, new Student.SortByBirthDate());
            Console.WriteLine("Сортировка по дате рождения:");
            foreach (var student in students)
            {
                student.PrintStudent();
                Console.WriteLine();
            }

            // Сортировка по рейтингу
            Student.Sort(students, new Student.SortByRating());
            Console.WriteLine("Сортировка по рейтингу:");
            foreach (var student in students)
            {
                student.PrintStudent();
                Console.WriteLine();
            }

            // Поиск студента по имени
            Student? foundStudent = Student.Search(students, s => s.Name == "Анна");
            if (foundStudent != null)
            {
                Console.WriteLine("Найден студент:");
                foundStudent.PrintStudent();
            }
            else
            {
                Console.WriteLine("Студент не найден");
            }
        }
    }
}
