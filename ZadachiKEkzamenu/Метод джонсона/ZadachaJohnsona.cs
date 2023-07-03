using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadachiKEkzamenu.Метод_джонсона
{
    public class ZadachaJohnsonaFromFile
    {
        private string _filePath;
        private List<string> DataFromFiles;

        public ZadachaJohnsonaFromFile(string filePath)
        {
            _filePath = filePath;
            DataFromFiles = File.ReadAllLines(@_filePath).ToList();
        }

        public ZadachaJohnsona CreateZadachaJohnsona() => new ZadachaJohnsona(DataFromFiles.ConvertAll(x => new Detal(x.Split(" "))));
    }

    public struct Detal
    {
        /// <summary>
        /// Время обработки на первом станке
        /// </summary>
        public int TimeForProcessingOnFirst;
        /// <summary>
        /// Время обработки на втором станке
        /// </summary>
        public int TimeForProcessingOnSecond;

        public Detal(string[] DetalTimes)
        {
            TimeForProcessingOnFirst = Convert.ToInt32(DetalTimes[0]);
            TimeForProcessingOnSecond = Convert.ToInt32(DetalTimes[1]);
        }

        public int MinTimeForProcessing
        {
            get => (TimeForProcessingOnFirst < TimeForProcessingOnSecond) ? TimeForProcessingOnFirst : TimeForProcessingOnSecond;
        }

        public int SumTimeForProcessing
        {
            get => (TimeForProcessingOnFirst + TimeForProcessingOnSecond);
        }
    }

    public class ZadachaJohnsona
    {
        /// <summary>
        /// Список деталей, не сортированный
        /// </summary>
        private List<Detal> _notSortedListOfDetails;

        /// <summary>
        /// Оптимальное распределение деталей
        /// </summary>
        private List<Detal> _sortedListOfDetails;

        public ZadachaJohnsona(List<Detal> notSortedListOfDetails)
        {
            _notSortedListOfDetails = notSortedListOfDetails;
            _sortedListOfDetails = new List<Detal>();
        }

        public int FindSolution()
        {
            List<Detal> _notSortedListOfDetailsWhatFastOnFirstStanc = new(); //Список деталей быстрее обрабатывающихся на 1 станке
            List<Detal> _notSortedListOfDetailsWhatFastOnSecondStanc = new(); //Список деталей быстрее обрабатывающихся на 2 станке
            FirstStepOfSorting(_notSortedListOfDetailsWhatFastOnFirstStanc, _notSortedListOfDetailsWhatFastOnSecondStanc); //Разделение несортированного массива на 2
            SecondStepOfSorting(_notSortedListOfDetailsWhatFastOnFirstStanc, 1);
            SecondStepOfSorting(_notSortedListOfDetailsWhatFastOnSecondStanc, 2);
            _notSortedListOfDetailsWhatFastOnFirstStanc.ForEach(x => _sortedListOfDetails.Add(x));
            _notSortedListOfDetailsWhatFastOnSecondStanc.ForEach(x => _sortedListOfDetails.Add(x));
            Console.WriteLine($"Время простоя - {CalculateFreeTime()}");
            return CalculateFreeTime();
        }

        /// <summary>
        /// Метод разделения не сортированного массива на два, где в первом находятся детали быстрее обрабатывающиеся на 1 станке, а во втором, быстрее обрабатывающиеся на 2
        /// </summary>
        /// <param name="notSortedListOfDetailsWhatFastOnFirstStanc">Список деталей быстрее обрабатывающихся на 1 станке</param>
        /// <param name="notSortedListOfDetailsWhatFastOnSecondStanc">Список деталей быстрее обрабатывающихся на 2 станке</param>
        public void FirstStepOfSorting(List<Detal> notSortedListOfDetailsWhatFastOnFirstStanc, List<Detal> notSortedListOfDetailsWhatFastOnSecondStanc)
        {
            foreach(var detal in _notSortedListOfDetails)
            {
                if(detal.TimeForProcessingOnFirst < detal.TimeForProcessingOnSecond) notSortedListOfDetailsWhatFastOnFirstStanc.Add(detal);
                else notSortedListOfDetailsWhatFastOnSecondStanc.Add(detal);
            }
        }

        /// <summary>
        /// Метод для сортировки деталей внутри контейнера, отсортированного по скорости
        /// </summary>
        /// <param name="notSortedList">Список деталей</param>
        /// <param name="Mode">1 - список деталей быстрее обрабатывающихся на первом, 2 - спсиок деталей быстрее обрабатывающихся на втором</param>
        public void SecondStepOfSorting(List<Detal> notSortedList, int Mode)
        {
            if(Mode == 1) //Сортировка деталей быстрее обрабатывающихся на первом станке
            {
                bool SortFlag = true;
                while(SortFlag)
                {
                    SortFlag = false;
                    for (int NumberOfDetal = 0; NumberOfDetal < notSortedList.Count - 1; NumberOfDetal++)
                    {
                        if (notSortedList[NumberOfDetal].MinTimeForProcessing > notSortedList[NumberOfDetal+1].MinTimeForProcessing)
                        {
                            SortFlag = true;
                            Detal DetalForSwap = notSortedList[NumberOfDetal];
                            notSortedList[NumberOfDetal] = notSortedList[NumberOfDetal + 1];
                            notSortedList[NumberOfDetal + 1] = DetalForSwap;
                        }
                        else if (notSortedList[NumberOfDetal].MinTimeForProcessing == notSortedList[NumberOfDetal + 1].MinTimeForProcessing
                            && notSortedList[NumberOfDetal].SumTimeForProcessing < notSortedList[NumberOfDetal + 1].SumTimeForProcessing)
                        {
                            SortFlag = true;
                            Detal DetalForSwap = notSortedList[NumberOfDetal];
                            notSortedList[NumberOfDetal] = notSortedList[NumberOfDetal + 1];
                            notSortedList[NumberOfDetal + 1] = DetalForSwap;
                        }
                    }
                }
            }
            if(Mode == 2) //Сортировка деталей быстрее обрабатывающихся на втором станке
            {
                bool SortFlag = true;
                while (SortFlag)
                {
                    SortFlag = false;
                    for (int NumberOfDetal = 0; NumberOfDetal < notSortedList.Count - 1; NumberOfDetal++)
                    {
                        if (notSortedList[NumberOfDetal].MinTimeForProcessing < notSortedList[NumberOfDetal + 1].MinTimeForProcessing)
                        {
                            SortFlag = true;
                            Detal DetalForSwap = notSortedList[NumberOfDetal];
                            notSortedList[NumberOfDetal] = notSortedList[NumberOfDetal + 1];
                            notSortedList[NumberOfDetal + 1] = DetalForSwap;
                        }
                        else if (notSortedList[NumberOfDetal].MinTimeForProcessing == notSortedList[NumberOfDetal + 1].MinTimeForProcessing
                            && notSortedList[NumberOfDetal].SumTimeForProcessing > notSortedList[NumberOfDetal + 1].SumTimeForProcessing)
                        {
                            SortFlag = true;
                            Detal DetalForSwap = notSortedList[NumberOfDetal];
                            notSortedList[NumberOfDetal] = notSortedList[NumberOfDetal + 1];
                            notSortedList[NumberOfDetal + 1] = DetalForSwap;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод расчета времени простоя
        /// </summary>
        /// <returns>время простоя</returns>
        public int CalculateFreeTime()
        {
            List<int> FreeTimes = new List<int>();
            for(int numberOfDetail = 0; numberOfDetail < _sortedListOfDetails.Count; numberOfDetail++)
            {
                if (numberOfDetail == 0) FreeTimes.Add(_sortedListOfDetails[0].TimeForProcessingOnFirst);
                else FreeTimes.Add(FreeTimes[numberOfDetail - 1] +
                    _sortedListOfDetails[numberOfDetail].TimeForProcessingOnFirst -
                    _sortedListOfDetails[numberOfDetail - 1].TimeForProcessingOnSecond);
            }
            return FreeTimes.Max();
        }
    }
}
