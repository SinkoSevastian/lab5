using System;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Security;

namespace struct_lab_student
{
    struct Student
    {
        public string surName;
        public string firstName;
        public string patronymic;
        public char sex;
        public string dateOfBirth;
        public char mathematicsMark;
        public char physicsMark;
        public char informaticsMark;
        public int scholarship;

        public Student(string lineWithAllData)
        {
            string[] student = Regex.Split(lineWithAllData, @" ");
            surName = student[0];
            firstName = student[1];
            patronymic = student[2];
            sex = Convert.ToChar(student[3]);
            dateOfBirth = student[4];
            mathematicsMark = Convert.ToChar(student[5]);
            physicsMark = Convert.ToChar(student[6]);
            informaticsMark = Convert.ToChar(student[7]);
            scholarship = Convert.ToInt32(student[8]);

        }
        public override string ToString()
        {
            return $"{surName} {firstName} {patronymic} {sex} {dateOfBirth} {mathematicsMark} {physicsMark} {informaticsMark} {scholarship}";
        }
    }
    internal class Program
    {
        public static List<Student> ReadData()
        {
            FileStream fs = File.Open(@"data.txt", FileMode.Open);
            List<Student> students = new List<Student>();
            using (var reader = new StreamReader(fs))
            {
                while (reader.Peek() >= 0)
                {
                    string str = reader.ReadLine() ?? string.Empty;
                    Student student = new Student(str);
                    students.Add(student);
                }
            }
            return students;
        }

        static double GetSummarySerArifm(List<Student> allStudents)
        {
            double summary_arifm = 0;

            for (int i = 0; i < allStudents.Count; i++)
            {
                Student student = allStudents[i];
                if (student.mathematicsMark == '-')
                {
                    student.mathematicsMark = '2';
                }
                if (student.physicsMark == '-')
                {
                    student.physicsMark = '2';
                }
                if (student.informaticsMark == '-')
                {
                    student.informaticsMark = '2';
                }

                summary_arifm += GetArifm(student);
            }
            return summary_arifm / allStudents.Count;
        }

        static double GetArifm(Student student)
        {
            double math = Char.GetNumericValue(student.mathematicsMark);
            double physics = Char.GetNumericValue(student.physicsMark);
            double it = Char.GetNumericValue(student.informaticsMark);

            return (math + physics + it)/3.0;
        }
        public static List<Student> SerArifmetic_14var(List<Student> allStudents)
        {
            List<Student> result = new List<Student>();

            double summary_arifm = GetSummarySerArifm(allStudents);

            Console.WriteLine("Загальнє середнє арифметичне: {0}", summary_arifm);
            foreach(Student student in allStudents)
            {
                if(GetArifm(student)>=summary_arifm)
                {
                    result.Add(student);
                    Console.WriteLine(student.surName);
                }
            }           
            return result;
        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<Student> students = ReadData();

            SerArifmetic_14var(students);
        }
    }
}