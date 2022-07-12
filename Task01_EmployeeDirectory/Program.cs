using System;
using System.IO;
using System.Text;

namespace Task01_EmployeeDirectory
{
    internal class Program
    {
        /// <summary>
        /// Задача. Создайте справочник «Сотрудники».
        /// Разработайте для предполагаемой компании программу, которая будет добавлять записи новых сотрудников в файл.
        /// </summary>
        static void Main(string[] args)
        {
            const string FILE_NAME = "Сотрудники.txt";
            Console.WriteLine("Вас приветствует справочник сотрудников!");
            FileInfo fileInfo = new FileInfo(FILE_NAME);
            while (true)
            {
                Console.WriteLine("Введите 1 чтобы прочитать справочник");
                Console.WriteLine("Введите 2 чтобы записать новые данные в справочник");
                Console.Write("Ваш выбор(нажмите Enter для выхода из программы): ");
                string inputText = Console.ReadLine();
                if (String.IsNullOrEmpty(inputText))
                {
                    Console.WriteLine("Программа завершила свою работу");
                    break;
                }
                if (!ushort.TryParse(inputText, out ushort selectNum))
                {
                    Console.WriteLine("Вы ввели не допустимое значение!");
                    continue;
                }
                switch (selectNum)
                {
                    case 1:
                        if(Exists(fileInfo))
                            PrintEmployeesInfo(fileInfo);
                        break;
                    case 2:
                        AddEmployee(fileInfo);
                        break;
                    default:
                        Console.WriteLine($"Под номером {selectNum} нет функционала!");
                        break;
                }
            }
        }
        /// <summary>
        /// Метод добавления информации о новых сотрудниках
        /// </summary>
        /// <param name="fileInfo">Файл с информацией о сотрудниках</param>
        /// <param name="separator">Разделитель</param>
        static void AddEmployee(FileInfo fileInfo, char separator = '#')
        {
            int id;
            if (!Exists(fileInfo))
            {
                id = 1;
                Console.WriteLine("Приступаем к созданию нового файла!");
            }
            else id = GetLastID(fileInfo.FullName);          
            using (StreamWriter writer = new StreamWriter(fileInfo.Open(FileMode.Append)))
            {
                StringBuilder stringBuilder = new StringBuilder();
                string[] userParam = { "ID", "DateTime", "Фамилия Имя Отчество: ", "Возраст: ", "Рост: ", "Дата рождения: ", "Место рождения: " };
                string[] userData = new string[userParam.Length];
                for (int i = 0; i < userParam.Length; i++)
                {
                    if (i > 1)
                    {
                        Console.Write(userParam[i]);
                        userData[i] = Console.ReadLine();
                    }
                    if (i == 1) userData[i] = DateTime.Now.ToString("g");
                    if (i == 0) userData[i] = id.ToString();
                }
                stringBuilder.AppendJoin(separator, userData);
                Console.WriteLine(stringBuilder);
                writer.WriteLine(stringBuilder.ToString());
            }
        }
        /// <summary>
        /// Вывод информации справочника в консоль
        /// </summary>
        /// <param name="fileInfo">Файл с информацией о сотрудниках</param>
        static void PrintEmployeesInfo(FileInfo fileInfo)
        {
            using (StreamReader reader = new StreamReader(fileInfo.FullName))
            {
                while (!reader.EndOfStream)
                {
                    Console.WriteLine(reader.ReadLine());
                }
            }
        }
        /// <summary>
        /// Метод определения последнего Id в справочнике
        /// </summary>
        /// <param name="filePath">Файл с информацией о сотрудниках</param>
        /// <returns>Возвращает кол-во записей в справочнике</returns>
        static int GetLastID(string filePath)
        {
            int counter = 1;
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    reader.ReadLine();
                    counter++;
                }
                return counter;
            }
            
        }
        /// <summary>
        /// Метод проверки на существование файла с заданным именем
        /// </summary>
        /// <param name="fileInfo">Файл с информацией о сотрудниках</param>
        /// <returns></returns>
        static bool Exists(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                Console.WriteLine("справочник не найден!");
                return false;
            }
            return true;
        }
    }
}
