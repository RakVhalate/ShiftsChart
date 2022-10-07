using System;
class Chart
{

    public static string[,] CreateWorkersList(int days)
    {
        string[] imena = System.IO.File.ReadAllLines("D:\\Programming\\CSharp\\ShiftsChart\\namelist.txt");
        int rows = imena.Length + 1; //количество рядов с поправкой на пустой нулевой ряд где находятся числа месяца
        int firstDay = DateTime.Now.Day;
        int columns = days + 2; //количество колонн с поправкой на нулевую колонну где находятся имена
        string [,] chart = new string[rows,columns];
        for(int j = 1; j < rows; j++)
        {
            chart[j,0] = imena[j-1]; //ряд с именами
        }

        for(int i = 1 ; i < columns; i++)
        {
            chart[0,i] = Convert.ToString(firstDay);
            if(i<10-DateTime.Now.Day) chart [0,i] += " "; //колонна с датами
            firstDay ++ ;
        }       
        return chart;
    }
        public static string[,] Shifts(string [,] chart) //заполняем таблицу
    {
        int rows = chart.GetUpperBound(0) + 1;
        int columns = chart.Length / rows;
        for(int j = 1; j < rows; j++) //ставим везде нули для начала работы с таблицей
        {
            for(int i = 1; i < columns; i++)
            {
                chart[j,i] = "0 ";
            }
        }
        return chart;
    }
    public static string[,] IsAbsent(string[,] toPrint)
    {
        Console.WriteLine("В случае отсутствия человека введите его имя, затем через пробел число, с которого он отсутствует, затем на сколько дней он отсутствует");
        string inp = Console.ReadLine();
        string[] input = inp.Split(' ');
        int rows = toPrint.GetUpperBound(0) + 1;
        int columns = toPrint.Length / rows;
        int abs = 0;
        for(int i = 1; i < rows; i++)
        {
            if(toPrint[i,0] == input[0])
            {
                Console.WriteLine("Дежурный " + toPrint[i,0] + " будет отсутствовать с " + input[1] + " числа " + input[2] + " дней. Прогульщик!"); //ищем порядковый номер отсутствующего
                abs = i;
            }
        }
        for(int j = int.Parse(input[1]) - (DateTime.Now.Day - 1); j < int.Parse(input[1]) - (DateTime.Now.Day - 1) + int.Parse(input[2]); j++)
                {
                    toPrint[abs,j] = "Н ";
                }
        return toPrint;
        
    }
    public static string[,] PutShifts(string[,] toShifts) //проставляем дежурства в зависимости от условий в таблице
    {
        int rows = toShifts.GetUpperBound(0) + 1;
        int columns = toShifts.Length / rows;

      for(int j = 1; j < columns ;) //ставим дни дежурств
        {
            for (int i = 1; i <rows & j <columns; i++)
            {
                if (toShifts[i,j] == "Н ") i++;
                toShifts[i,j] = "Д ";
                j++;
            }
      
        }
      return toShifts;


    }
    public static void DrawArray(string[,] toPrint)  //метод, рисующий таблицу
    {
        int drawRows = toPrint.GetUpperBound(0) + 1;  //ряд с датами
        int drawColumns = toPrint.Length / drawRows; //ряд с именами
        string[] imena = System.IO.File.ReadAllLines("D:\\Programming\\CSharp\\ShiftsChart\\namelist.txt");
        int maxChars = imena.Max(x => x.Length); //добавляем в нулевую ячейку элемент форматирования в соответствии с самым длинным именем
        for(int m = 0; m <= maxChars - 1; m++)
        {
            toPrint[0,0] += " ";
        }
        for(int r = 0; r < drawRows; r++)  //выравниваем ячейки с именами до одной длины пробелами
        {
            while( toPrint[r,0].Length < maxChars)
            {
                toPrint[r,0] += " ";
            }
        }
        
        for(int i = 0; i < drawRows; i++)
        {
            for(int j = 0; j < drawColumns; j++) //рисуем ряды развёрткой
            {
            Console.Write($"{toPrint[i,j]}" + " ");
            }
            Console.WriteLine();
        }
    }

    public static void Main()
    {
        {
        int nowDay = DateTime.Now.Day;
        DrawArray(Shifts(CreateWorkersList(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - nowDay)));
        DrawArray(PutShifts(IsAbsent(Shifts(CreateWorkersList(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - nowDay)))));
        Console.WriteLine("Нажмите любую клавишу для выхода")
        Console.ReadLine();
        //тестовая строка
        }
    }
}
