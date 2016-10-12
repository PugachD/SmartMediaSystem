using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartMediaSystems
{
    class Program
    {
        static private List<SourceData> listSourceData;
        static private List<TreeViewElements> listTreeViewElements;
        static private int CountColumn;

        static void Main(string[] args)
        {
            listTreeViewElements = new List<TreeViewElements>();

            //Инициализация для проверки работоспособности
            CountColumn = 6;
            listSourceData = new List<SourceData>
            {
                new SourceData() { NameColumns = new object[] { "Охта Парк", "Охта-Парк (1 очередь)", "Общая часть", "Общая часть", "Общая часть", 2}  },
                new SourceData() { NameColumns = new object[] { "Охта Парк", "Охта-Парк (1 очередь)", "Общая часть", "Общая часть", "Общая часть",3} },
                new SourceData() { NameColumns = new object[] { "Охта Парк", "Охта-Парк (2 очередь)", "Общая часть", "Общая часть 2", "Общая часть", 232 } },
                new SourceData() { NameColumns = new object[] { "Охта Парк", "Охта-Парк (2 очередь)", "Общая часть 2", "Общая часть", "Общая часть" , 444} },
                new SourceData() { NameColumns = new object[] { "Красная поляна", "Реконструкция Псехако F", "Общая часть", "Общая часть", "Горнолыжные трассы F1 - F8", 32523 } },
                new SourceData() { NameColumns = new object[] { "Красная поляна", "Реконструкция Псехако F", "Общая часть", "Общая часть", "Горнолыжные трассы F1 - F8", 1 } },
                new SourceData() { NameColumns = new object[] { "Красная поляна", "Реконструкция Псехако D", "Общая часть", "Общая часть", "Сооружения инженерной защиты", 9 } }

            };
            //Подключение к БД с помощью класса DataBaseWork
            //.....

            //Считывание в список List<SourceData> данных из исходной таблицы БД
            //.....

            CreateTreeView();

            //listTreeViewElements и есть список, который представляет таблицу - результат
        }

        /// <summary>
        /// Создание "дерева" 
        /// </summary>
        static private void CreateTreeView()
        {
            for (int i = 0; i != CountColumn - 1; i++)
            {
                if (i == 0)
                {
                    listTreeViewElements.AddRange(from source in
                                                      (listSourceData.Select(row => new { Name = row.NameColumns[i].ToString() })
                                                      .Distinct().ToList())
                                                  orderby source.Name
                                                  select new TreeViewElements
                                                  {
                                                      ID = listTreeViewElements.Count + 1,
                                                      Parent_ID = 0,
                                                      Name = source.Name,
                                                      CID = -2
                                                  });
                }
                else
                {
                    List<TreeViewElements> newList = new List<TreeViewElements>(listTreeViewElements);
                    listTreeViewElements.AddRange(from source in listSourceData
                                                  from tree in newList
                                                  where FindToHierarchy(i, tree , source)
                                                  orderby source.NameColumns[i]
                                                  select new TreeViewElements
                                                  {
                                                      ID = listTreeViewElements.Count + 1,
                                                      Parent_ID = tree.ID,
                                                      Name = source.NameColumns[i].ToString(),
                                                      CID = (i == CountColumn -2) ? ((source.NameColumns[i+1] is int) ? (int)source.NameColumns[i + 1] : 0) : -2
                                                  });
                   listTreeViewElements = listTreeViewElements.Distinct().ToList();
                    int j = 1;
                    listTreeViewElements.ForEach(delegate (TreeViewElements tree) { tree.ID = j; j++; });
                }
            }
        }

        /// <summary>
        /// Проверка на принадлежность элемента к иерархии 
        /// </summary>
        private static bool FindToHierarchy(int i, TreeViewElements tree, SourceData source)
        {
            bool result = false;
            if (tree.Name == source.NameColumns[i - 1].ToString())
                if (i > 1)
                    result = FindToHierarchy(i - 1, listTreeViewElements.Find(item => item.ID == tree.Parent_ID), source);
                else result = true;
            else
                result = false;
            return result;
        }
    }
}
