using System;
class Program
{
    public static string[,] CreateWorkersList(int days)
    {
        string[] imena = System.IO.File.ReadAllLines("namelist.txt");
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
        int maxChars = imena.Max(x => x.Length); //добавляем в нулевую ячейку элемент форматирования в соответствии с самым длинным именем
        for(int m = 0; m <=maxChars - 1; m++)
        {
            chart[0,0] += " ";
        }
        for(int r = 0; r < rows; r++)  //выравниваем ячейки с именами до одной длины пробелами
        {
            while( chart[r,0].Length < maxChars)
            {
                chart[r,0] += " ";
            }
        }

        
        return chart;
    }
        public static string[,] Shifts(string [,] chart) //заполняем таблицу
    {
        int rows = chart.GetUpperBound(0) + 1;
        int columns = chart.Length / rows;
        for(int j = 1; j < rows; j++) //ставим везде нули
        {
            for(int i = 1; i < columns; i++)
            {
                chart[j,i] = "0 ";
            }
        }
            for(int j = 1; j < columns ;) //ставим дни дежурств
        {
            for (int i = 1; i <rows & j <columns; i++)
            {
                chart[i,j] = "Д ";
                j++;
            }
      
        }
        return chart;
    }
    public static void DrawArray(string[,] toPrint)
    {
        int drawRows = toPrint.GetUpperBound(0) + 1;  //ряд с датами
        int drawColumns = toPrint.Length / drawRows; //ряд с именами
        
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
 //       int leftDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - nowDay;
        DrawArray(Shifts(CreateWorkersList(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - nowDay)));
        Console.ReadLine();
        
        }
    }
}
