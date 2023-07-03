using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadachiKEkzamenu.Минимальный_элемент
{
    public class MinElemFromFile
    {
        private string _filePath;
        private List<string> DataFromFile;

        public MinElemFromFile(string filePath)
        {
            _filePath = filePath;
            DataFromFile = File.ReadAllLines(@filePath).ToList();
        }

        public int[] SuppliarsPowerFromFile() => DataFromFile[0].Split(" ").ToList().ConvertAll(x => Convert.ToInt32(x)).ToArray();

        public int[] ConsumersPowerFromFile() => DataFromFile[1].Split(" ").ToList().ConvertAll(x => Convert.ToInt32(x)).ToArray();

        public int[,] LossMatrixFromFile()
        {
            int[,] lossMatrix = new int[DataFromFile.Count - 2, DataFromFile[2].Split(" ").Count()];
            for (int stroka = 0; stroka < lossMatrix.GetLength(0); stroka++)
            {
                for(int stolb = 0; stolb < lossMatrix.GetLength(1); stolb++)
                {
                    lossMatrix[stroka, stolb] = Convert.ToInt32(DataFromFile[stroka + 2].Split(" ")[stolb]);
                }
            }
            return lossMatrix;
        }

        public MinElem CreateMinElemFromFile() => new MinElem(SuppliarsPowerFromFile(), ConsumersPowerFromFile(), LossMatrixFromFile());
    }

    public class MinElem
    {
        #region private поля класса
        /// <summary>
        /// Вектор мощности поставщиков
        /// </summary>
        private int[] _suppliarsPower;
        /// <summary>
        /// Вектор мощности потребителей
        /// </summary>
        private int[] _consumersPower;
        /// <summary>
        /// Матрица затрат на перевозки
        /// </summary>
        private int[,] _lossMatrix;
        /// <summary>
        /// Матрица транспортировки
        /// </summary>
        private int[,] _transportMatrix;
        /// <summary>
        /// Затраты на перевозки
        /// </summary>
        private int _targetFunction;
        #endregion

        public MinElem(int[] suppliarsPower, int[] consumersPower, int[,] lossMatrix)
        {
            _suppliarsPower = suppliarsPower;
            _consumersPower = consumersPower;
            _lossMatrix = lossMatrix;
            _transportMatrix = new int[_lossMatrix.GetLength(0), _lossMatrix.GetLength(1)];
        }

        /// <summary>
        /// Метод для нахождения первоначального распределения при помощи метода минимального элемента
        /// </summary>
        /// <returns>Результат целевой функции</returns>
        public int FindSolution()
        {
            int[]? CoordinateOfMinElem = FindMinElem();
            while (CoordinateOfMinElem is not null) //Пока не будет найден последний минимальный элемент,
                                             //мы будем продолжать цикл
            {
                _transportMatrix[CoordinateOfMinElem[0], CoordinateOfMinElem[1]] =
                    (_suppliarsPower[CoordinateOfMinElem[1]] > _consumersPower[CoordinateOfMinElem[0]]) ?
                    _consumersPower[CoordinateOfMinElem[0]] : _suppliarsPower[CoordinateOfMinElem[1]]; //Определяем сколько поставлять товара в пункт
                                                                                                       //с минимальными затратами
                _consumersPower[CoordinateOfMinElem[0]] -= _transportMatrix[CoordinateOfMinElem[0], CoordinateOfMinElem[1]]; //Вычитаем поставки
                                                                                                                             //из мощностей векторов
                _suppliarsPower[CoordinateOfMinElem[1]] -= _transportMatrix[CoordinateOfMinElem[0], CoordinateOfMinElem[1]];
                CoordinateOfMinElem = FindMinElem(); //Находим новый минимальный элемент
            }
            _targetFunction = FindTargetFunc();
            Console.WriteLine($"F = {_targetFunction}");
            return _targetFunction;
        }

        /// <summary>
        /// Метод для нахождения координат минимальной незаполненой поставки
        /// </summary>
        /// <returns>Координаты поставки с минимальными затратами</returns>
        public int[]? FindMinElem()
        {
            int[]? MinElementCoordinates = null; //Координаты элемента с минимальными затратами
            for(int stroka = 0; stroka < _transportMatrix.GetLength(0); stroka++) //Проходка по матрице
            {
                for(int stolb = 0; stolb < _transportMatrix.GetLength(1); stolb++)
                {
                    if (_transportMatrix[stroka,stolb] == 0 &&
                        (_suppliarsPower[stolb] != 0) &&
                        (_consumersPower[stroka] != 0)) //Ищем элементы с возможными поставками, но ещё не заполненными
                    {
                        if(MinElementCoordinates is null ||
                            _lossMatrix[stroka,stolb] < _lossMatrix[MinElementCoordinates[0],
                            MinElementCoordinates[1]]) //Записываем координаты,
                                                       //если это первый встретившийся элемент или,
                                                       //если найденый меньше текущего минимального
                        MinElementCoordinates = new int[] {stroka, stolb};
                    }
                }
            }
            return MinElementCoordinates;
        }

        /// <summary>
        /// Поиск суммы затрат на перевозку товара
        /// </summary>
        /// <returns>Сумма затрат</returns>
        public int FindTargetFunc()
        {
            int targetFunc = 0;
            for (int stroka = 0; stroka < _transportMatrix.GetLength(0); stroka++) //Проходка по матрице
            {
                for (int stolb = 0; stolb < _transportMatrix.GetLength(1); stolb++)
                {
                    targetFunc += _transportMatrix[stroka, stolb] * _lossMatrix[stroka, stolb];
                }
            }
            return targetFunc;
        }
    }
}
