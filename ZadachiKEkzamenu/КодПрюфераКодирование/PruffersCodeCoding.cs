using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ZadachiKEkzamenu.КодПрюфераКодирование
{
    public class PruffersCodeFromFile
    {
        /// <summary>
        /// Путь к файлу
        /// </summary>
        private string _filePath;
        /// <summary>
        /// Информация внутри файла
        /// </summary>
        private List<string> _dataFromFile;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="filePath">путь к файлу с информацией для кода прюфера</param>
        public PruffersCodeFromFile(string filePath)
        {
            _filePath = filePath;
            _dataFromFile = new List<string>(File.ReadAllLines(@_filePath));
        }

        public PruffersCodeCoding CreatePruffersCodeCoding() =>
            new PruffersCodeCoding(_dataFromFile.ConvertAll<int[]>(stroka => stroka.Split(" ").ToList().ConvertAll<int>(node => Convert.ToInt32(node)).ToArray()));
    }

    public class PruffersCodeCoding
    {
        #region Private поля
        /// <summary>
        /// Ребра графа
        /// </summary>
        private List<int[]> _graphsEdges;
        /// <summary>
        /// Вершины графа
        /// </summary>
        private List<int> _graphsNodes;
        /// <summary>
        /// Листья графа
        /// </summary>
        private List<int> _graphPapers;
        #endregion

        /// <summary>
        /// Коструктор кода прюфера кодирование
        /// </summary>
        /// <param name="graphEdges">Ребра графа</param>
        public PruffersCodeCoding(List<int[]> graphEdges)
        {
            _graphsEdges = graphEdges;
        }

        /// <summary>
        /// Метод поиска всех вершин графа
        /// </summary>
        /// <param name="graphsEdges">Ребра графа</param>
        /// <returns>Список вершин графа</returns>
        public List<int> FindAllGraphsNodes(List<int[]> graphsEdges)
        {
            List<int> graphsNodes = new List<int>(); //Список вершин графа
            graphsEdges.ForEach(graph => { graphsNodes.AddRange(graph); }); //Переводим список ребер графа из двух мерной формы в одномерную
            return graphsNodes.Distinct().ToList(); //Возвращаем список ребер, но без повторений, то есть список вершин
        }

        /// <summary>
        /// Метод поиска листьев графа
        /// </summary>
        /// <param name="_graphsNodes">список вершин графа</param>
        /// <returns>Список листьев</returns>
        public List<int> FindAllGraphsPapers(List<int> _graphsNodes, List<int[]> graphEdges)
        {
            List<int> graphsPapers = new List<int>(); //Список листьев
            List<int> graphsNodesIn1IntDemension = new List<int>(); //Список ребер графа, но в одномерном представлении
            graphEdges.ForEach(graph => { graphsNodesIn1IntDemension.AddRange(graph);}); //Перевод ребер графа из двух мерной формы в одно мерную
            foreach (var node in _graphsNodes) //Проходимся по массиву вершин, чтобы определить какие из них являются листьями
            {
                if(graphsNodesIn1IntDemension.Where(graphsNode => graphsNode == node).Count() == 1) //Если вершина встречается 1 раз в списке ребер,
                                                                                                    //то она является листом графа
                {
                    graphsPapers.Add(node);
                }
                
            }
            return graphsPapers;
        }

        /// <summary>
        /// Метод поиска вершины, предшествующей минимальному листу
        /// </summary>
        /// <param name="MinPaper">Минимальный лист</param>
        /// <param name="graphEdges">Список ребер</param>
        /// <returns>Вершина, предшественник минимального листа</returns>
        public int FindFatherOfMinPaper(int MinPaper, List<int[]> graphEdges)
        {
            int FatherOfMinPaper = 0;
            foreach(var edge in graphEdges) //Проходимся по списку ребер
            {
                if (edge[0] == MinPaper) //Если искомый минимальный элемент первый в ребре,
                                         //то возвращаем второй и удаляем ребро
                {
                    FatherOfMinPaper = edge[1];
                    graphEdges.Remove(edge);
                    break;
                }
                if (edge[1] == MinPaper) //Если искомый минимальный элемент второй в ребре,
                                         //то возвращаем первый и удаляем ребро
                {
                    FatherOfMinPaper = edge[0];
                    graphEdges.Remove(edge);
                    break;
                }
            }
            return FatherOfMinPaper;
        }

        /// <summary>
        /// Метод поиска кода Прюфера
        /// </summary>
        /// <returns>Код Прюфера</returns>
        public string FindSolution()
        {
            string pruffersCode = ""; 
            while(_graphsEdges.Count() != 1) //Пока количество ребер графа не равно единице,
                                             //будем искать отца минимального листа,
                                             //вносить в код и удалять из списка ребер
            {
                _graphsNodes = FindAllGraphsNodes(_graphsEdges);
                _graphPapers = FindAllGraphsPapers(_graphsNodes, _graphsEdges);
                pruffersCode += Convert.ToString(FindFatherOfMinPaper(_graphPapers.Min(), _graphsEdges)) + ",";
            }
            Console.WriteLine(pruffersCode[0..(pruffersCode.Length - 1)]);
            return pruffersCode[0..(pruffersCode.Length-1)]; //Возвращаем весь код,
                                                             //за исключением последнего элемента,
                                                             //так как там запятая
        }
    }
}
