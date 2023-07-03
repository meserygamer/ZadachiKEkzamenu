using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadachiKEkzamenu.Северо_западныйУгол
{
    public class NorthWestAngleReaderFromFile
    {
        private string _filePath;
        private List<string> TextFromFile;

        public NorthWestAngleReaderFromFile(string FilePath)
        {
            _filePath = FilePath;
            TextFromFile = new List<string>(File.ReadAllLines(@FilePath));
        }

        public int[] powerOfSuppliarsFromFile() => TextFromFile[0].Split(" ").ToList().ConvertAll<int>(x => Convert.ToInt32(x)).ToArray();

        public int[] powerOfConsumersFromFile() => TextFromFile[1].Split(" ").ToList().ConvertAll<int>(x => Convert.ToInt32(x)).ToArray();

        public int[,] lossMatrix()
        {
            int[,] MatrixOfLoss = new int[TextFromFile.Count - 2, TextFromFile[2].Split(" ").Count()];
            for(int Stolb = 0; Stolb < MatrixOfLoss.GetLength(1); Stolb++)
            {
                for(int Stroka = 2; Stroka < MatrixOfLoss.GetLength(0) + 2; Stroka++)
                {
                    MatrixOfLoss[Stroka - 2, Stolb] = Convert.ToInt32(TextFromFile[Stroka].Split(" ")[Stolb]);
                }
            }
            return MatrixOfLoss;
        }

        public NorthWestAngle CreateNorthWestAngle() => new NorthWestAngle(powerOfSuppliarsFromFile(), powerOfConsumersFromFile(), lossMatrix());
    }

    public class NorthWestAngle
    {
        #region Private поля класса
        /// <summary>
        /// Вектор силы поставщиков
        /// </summary>
        private int[] _powerOfSuppliars; 
        /// <summary>
        /// Вектор потребления
        /// </summary>
        private int[] _powerOfConsumers;

        /// <summary>
        /// Матрица стоимости перевозок из пункта в пункт
        /// </summary>
        private int[,] _LossMatrix;
        /// <summary>
        /// Матрица перевозок
        /// </summary>
        public int[,] _supplyMatrix;

        /// <summary>
        /// стоиммость перевозок
        /// </summary>
        private int _shippingСost;
        #endregion

        /// <summary>
        /// Своство для определения равенства спроса и предложения
        /// </summary>
        public bool IsDemandEqualsSupply
        {
            get => _powerOfConsumers.Sum() == _powerOfSuppliars.Sum();
        }

        /// <summary>
        /// Конструктор метода северо-западного угла
        /// </summary>
        /// <param name="powerOfSuppliars">Вектор мощности поставщиков</param>
        /// <param name="powerOfConsumers">Вектор мощности покупателей</param>
        /// <param name="lossMatrix">Матрица затрат</param>
        public NorthWestAngle(int[] powerOfSuppliars, int[] powerOfConsumers, int[,] lossMatrix)
        {
            _powerOfConsumers = powerOfConsumers;
            _powerOfSuppliars = powerOfSuppliars;
            _LossMatrix = lossMatrix;
            _supplyMatrix = new int[_LossMatrix.GetLength(0), _LossMatrix.GetLength(1)]; //Инициализируем пустой массив перевоз с размерностью равной массиву затрат
            _shippingСost = 0; //Суммарная стоимость поставок
        }

        /// <summary>
        /// Метод поиска решения
        /// </summary>
        public void FindSolution()
        {
            if(!IsDemandEqualsSupply) //Если спрос не равен предложению, выводим сообщение и выходим
            {
                Console.WriteLine("Спрос и предложение не равны");
                return;
            }
            //Заполняем массив поставок
            _supplyMatrix = FindDistribution();
            //Проходмся по массиву поставок, чтобы определить суммарную стоимость
            _shippingСost = FindSumOfDistribution();
            Console.WriteLine($"F = {_shippingСost}");
        }

        /// <summary>
        /// Нахождение массива распределения поставок
        /// </summary>
        /// <returns>Массив поставок</returns>
        public int[,] FindDistribution()
        {
            int[,] Distibution = new int[_LossMatrix.GetLength(0), _LossMatrix.GetLength(1)];

            for (int Stolb = 0; Stolb < _supplyMatrix.GetLength(0); Stolb++) //Идем по столбцам
            {
                for (int Stroka = 0; Stroka < _supplyMatrix.GetLength(1); Stroka++) //Идем по строкам
                {
                    if (_powerOfSuppliars[Stolb] != 0 && _powerOfConsumers[Stroka] != 0) //Если постовщики могут поставлять товары, а покупатели их принимать,
                                                                                         //то осуществляем доставку в этот пункт
                    {
                        int DeliveryQuantity = (_powerOfSuppliars[Stolb] > _powerOfConsumers[Stroka]) ? _powerOfConsumers[Stroka] : _powerOfSuppliars[Stolb]; //Количество доставленных товаров =
                                                                                                                                                              //Наименьшее из мощности поставщиков
                                                                                                                                                              //и мощности спроса
                        Distibution[Stroka, Stolb] = DeliveryQuantity; //Поставляем товары в клетку
                        _powerOfSuppliars[Stolb] -= DeliveryQuantity; //Вычитаем из мощности поставщика количесво поставленных товаров
                        _powerOfConsumers[Stroka] -= DeliveryQuantity; //Вычитаем из мощности потребителей количесво поставленных товаров
                    }
                }
            }
            return Distibution;
        }

        /// <summary>
        /// Метод нахождения суммарной стоимости поставки
        /// </summary>
        /// <returns>Функция F - стоимость поставок</returns>
        public int FindSumOfDistribution()
        {
            int Sum = 0;
            for (int Stolb = 0; Stolb < _supplyMatrix.GetLength(0); Stolb++) //Идем по столбцам
            {
                for (int Stroka = 0; Stroka < _supplyMatrix.GetLength(1); Stroka++) //Идем по строкам
                {
                    Sum += _supplyMatrix[Stroka, Stolb] * _LossMatrix[Stroka, Stolb]; //Стоимость поставки в клетку равна
                                                                                      //количеству товаров, умноженному на их стоимость поставки в клетку
                }
            }
            return Sum;
        }
    }
}
