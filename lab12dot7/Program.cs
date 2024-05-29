
using laba12;
using System.ComponentModel.Design;
using лаба10;
using lab12dot7;
using System.Xml.Linq;
namespace laba12
{
    
        internal class Program
    {

        static void Main(string[] args)
        {
            DoublyLinkedList<MusicalInstrument> instrumentsList = new DoublyLinkedList<MusicalInstrument>();
            Random rand = new Random();
            //MusicalInstrument[] instruments = new MusicalInstrument[10]; // Замените размер массива и инициализацию элементов в соответствии с вашими объектами
            
            int arraySize = 10; // Размер массива

            MusicalInstrument[] instrumentsArray = new MusicalInstrument[arraySize];
            HashTable<int, MusicalInstrument> instrumentsTable = new HashTable<int, MusicalInstrument>();


            // Заполнение массива рандомными объектами MusicalInstrument
            for (int i = 0; i < arraySize; i++)
            {
                switch (rand.Next(3)) // Выбор случайного инструмента
                {
                    case 0:
                        instrumentsArray[i] = new Guitar();
                        break;
                    case 1:
                        instrumentsArray[i] = new Piano();
                        break;
                    case 2:
                        instrumentsArray[i] = new ElectricGuitar();
                        break;
                }

                // Инициализация объекта
                instrumentsArray[i].RandomInit();
            }
            BalancedBinaryTree<MusicalInstrument> tree = new BalancedBinaryTree<MusicalInstrument>(instrumentsArray);

            while (true)
            {
                int pointsMainMenu = 4;

                Console.WriteLine("\nМеню приложения:");

                Console.WriteLine("1 - Меню работы с двунаправленным списком");
                Console.WriteLine("2 - хеш-таблица");
                Console.WriteLine("3 - Меню работы с идеально сбалансированным деревом");
                Console.WriteLine("3 - Меню работы с идеально сбалансированным деревом");
                Console.WriteLine("0 - Выйти из приложения");

                int choiceMainMenu = InputInt(0, pointsMainMenu);

                if (choiceMainMenu == 0)
                {
                    Console.WriteLine("\n0 - Выход из приложения");
                    break;
                }

                switch (choiceMainMenu)
                {
                    case 1:
                        BiCaseMenu(ref instrumentsList);
                        break;
                    case 2:
                        HashTable(ref instrumentsTable);
                        break;
                    case 3:
                        TreeCaseMenu(ref tree);
                        break;
                    case 4:
                        MyCollectionTest();
                        break;



                }
            }
        }
       
        private static int InputInt(int min, int max)
        {
            int number;
            bool inputCheck;
            do
            {
                Console.Write("Ввод: ");
                inputCheck = int.TryParse(Console.ReadLine(), out number) && number >= min && number <= max;
                if (!inputCheck) Console.WriteLine("Ошибка ввода! Введите целое число в пределах от {0} до {1} (включительно)", min, max);
            } while (!inputCheck);
            return number;
        }
        public static MusicalInstrument[] GenerateInstrumentsArray(int arraySize)
        {
            Random rand = new Random();
            MusicalInstrument[] instrumentsArray = new MusicalInstrument[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                switch (rand.Next(3)) // Выбор случайного инструмента
                {
                    case 0:
                        instrumentsArray[i] = new Guitar();
                        break;
                    case 1:
                        instrumentsArray[i] = new Piano();
                        break;
                    case 2:
                        instrumentsArray[i] = new ElectricGuitar();
                        break;
                }

                // Инициализация объекта
                instrumentsArray[i].RandomInit();
                
            }
            return instrumentsArray;
        }

        private static void MyCollectionTest()
        {
            // Создаем коллекцию и заполняем ее объектами MusicalInstrument
            MyCollection<string, MusicalInstrument> collection = new MyCollection<string, MusicalInstrument>(10);

            // Добавляем случайные объекты MusicalInstrument в коллекцию
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                MusicalInstrument instrument;
                switch (rand.Next(3))
                {
                    case 0:
                        instrument = new Guitar();
                        break;
                    case 1:
                        instrument = new Piano();
                        break;
                    case 2:
                        instrument = new ElectricGuitar();
                        break;
                    default:
                        instrument = new MusicalInstrument();
                        break;
                }
                instrument.RandomInit();
                collection.Add(instrument.Name, instrument);
            }

            // Выводим содержимое коллекции
            collection.DisplayContents();

            // Демонстрация работы всех реализованных методов
            // Удаление элемента по ключу
            collection.Remove("YAMAHA C40");

            // Выводим содержимое коллекции после изменений
            Console.WriteLine("\nСодержимое коллекции после изменений:");
            collection.DisplayContents();

        }

        private static void TreeCaseMenu(ref BalancedBinaryTree<MusicalInstrument> tree)
        {
            BalancedBinaryTree<MusicalInstrument> searchTree = tree.Balance();
            MusicalInstrument[] instrumentsArray = GenerateInstrumentsArray(10);
            while (true)
            {
                int pointsCaseMenu = 8;

                Console.WriteLine("\nМеню работы с идеально сбалансированным деревом:");
                Console.WriteLine("1 - Формирование дерева");
                Console.WriteLine("2 - Распечатать полученное дерево");
                Console.WriteLine("3 - Найти максимальный элемент в дереве ");
                Console.WriteLine("4 - Преобразовать идеально сбалансированное дерево в дерево поиска");
                Console.WriteLine("5 - Распечатать полученное дерево (полученное)");
                Console.WriteLine("6 - Удалить из дерева поиска элемент с заданным ключом");
                Console.WriteLine("7- Удалить дерево из памяти");
                Console.WriteLine("8 - Очистка истории");
                Console.WriteLine("0 - Выход из меню");

                int choiceCaseMenu = InputInt(0, pointsCaseMenu);

                if (choiceCaseMenu == 0)
                {
                    Console.WriteLine("\n0 - Выход из меню");
                    break;
                }

                switch (choiceCaseMenu)
                {
                    case 1:
                        {
                            // Создаем идеально сбалансированное бинарное дерево
                            
                            tree = new BalancedBinaryTree<MusicalInstrument>(instrumentsArray);
                        }
                
                        break;
                    case 2:
                        {
                            Console.WriteLine("Исходное идеально сбалансированное бинарное дерево:");
                            tree.PrintLevelOrder();
                            Console.WriteLine();
                        }
                        break;
                    case 3:
                        {
                            // Выполняем обработку дерева (например, поиск максимального элемента)
                            MusicalInstrument maxInstrument = tree.FindMax();
                            Console.WriteLine("Максимальный элемент в дереве: " + maxInstrument);
                            Console.WriteLine();

                        }
                        break;
                    case 4:
                        {
                            //BalancedBinaryTree<MusicalInstrument> searchTree = tree.Balance();
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("Преобразованное дерево поиска:");
                            searchTree.PrintLevelOrder();
                            Console.WriteLine();
                        }
                        break;
                    case 6:
                        {
                            //searchTree.Remove(instrumentsArray[0]);
                            //Console.WriteLine("Удален элемент с ключом: " + instrumentsArray[0].Name);
                            //Console.WriteLine();
                            // Удаляем элемент с заданным ключом из дерева поиска (пример удаления первого элемента)
                            if (searchTree.Remove(instrumentsArray[0]))
                            {
                                Console.WriteLine("Элемент с ключом " + instrumentsArray[0].Name + " удален из дерева.");
                            }
                            else
                            {
                                Console.WriteLine("Элемент с ключом " + instrumentsArray[0].Name + " не найден в дереве.");
                            }
                            Console.WriteLine();
                        }
                        break;
                    case 7:
                        {
                            searchTree = null;
                            Console.WriteLine("Дерево удалено из памяти");
                        }
                        break;
                    case 8:
                        {
                            Console.Clear();
                            Console.WriteLine("История была очищена");
                        }
                        break;
                    case 0:
                        {
                            
                        }
                        break;
                }
            }
        }

        private static void HashTable(ref HashTable<int, MusicalInstrument> instrumentsTable)
        {
            while (true)
            {
                int pointsCaseMenu = 7;

                Console.WriteLine("\nМеню работы с хеш-таблицей:");
                Console.WriteLine("1 - Формирование таблицы");
                Console.WriteLine("2 - Поиск элемента");
                Console.WriteLine("3 - Удаление элемента");
                Console.WriteLine("4 - Печать таблицы");
                Console.WriteLine("5 - Показать, что будет при добавлении элемента в хеш-таблицу, если в таблице уже находится максимальное число элементов");
                Console.WriteLine("6 - Очистка истории");
                Console.WriteLine("7 - Добавить вручную");
                Console.WriteLine("0 - Выход из меню");

                int choiceCaseMenu = InputInt(0, pointsCaseMenu);

                if (choiceCaseMenu == 0)
                {
                    Console.WriteLine("\n0 - Выход из меню");
                    break;
                }

                switch (choiceCaseMenu)
                {
                    case 1:
                        {
                            // Заполнение таблицы
                            for (int i = 0; i < 10; i++)
                            {
                                MusicalInstrument instrument = GenerateRandomInstrument();
                                instrumentsTable.Add(instrument.GetHashCode(), instrument);
                            }
                        }
                        break;

                    case 2:
                        {
                            if (instrumentsTable.GetCount() == 0)
                            {
                                Console.WriteLine("Хеш-таблица пустая.");
                                break;
                            }
                            // Поиск элемента в таблице
                            try
                            {
                                Console.WriteLine("Поиск элемента по ключу:");
                                int key = Functions.Input();
                                MusicalInstrument foundInstrument = instrumentsTable.Find(key);
                                Console.WriteLine($"Найденный инструмент: {foundInstrument}");
                            }
                            catch (KeyNotFoundException)
                            {
                                Console.WriteLine("Инструмент не найден.");
                            }
                        }
                        break;

                    case 3:
                        {
                            if (instrumentsTable.GetCount() == 0)
                            {
                                Console.WriteLine("Хеш-таблица пустая.");
                                break;
                            }
                            // Удаление элемента из таблицы
                            Console.WriteLine("Удаление элемента по ключу:");
                            int key = Functions.Input();
                            bool removed = instrumentsTable.Remove(key);
                            Console.WriteLine($"Элемент удален из таблицы: {removed}");
                        }
                        break;

                    case 4:
                        {
                            if (instrumentsTable.GetCount() == 0)
                            {
                                Console.WriteLine("Хеш-таблица пустая.");
                                break;
                            }
                            // Печать таблицы
                            Console.WriteLine("Хеш-таблица:");
                            instrumentsTable.Print();
                        }
                        break;

                    case 5:
                        {
                            int count = instrumentsTable.GetCount();
                            Random rand = new Random();
                            // xДобавление элемента в полностью заполненную таблицу
                            Console.WriteLine("Попытка добавления элемента в полностью заполненную таблицу:");
                            try
                            {
                                
                                MusicalInstrument newInstrument = GenerateRandomInstrument();
                                Console.WriteLine("Добавляемый элемент");
                                newInstrument.Show();
                                instrumentsTable.Add(newInstrument.GetHashCode(), newInstrument);
                                if (count == instrumentsTable.GetCount())
                                {
                                    Console.WriteLine("Элемент не добавлен");
                                }
                                else { Console.WriteLine("Элемент добавлен"); }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка: {ex.Message}");
                            }
                        }
                        break;

                    case 6:
                        {
                            Console.Clear();
                            Console.WriteLine("История была очищена");
                        }
                        break;
                    case 7:
                        {

                            int count = instrumentsTable.GetCount();
                            Random rand = new Random();
                            // Добавление элемента в полностью заполненную таблицу
                            Console.WriteLine("введите элемент:");
                            try
                            {

                                ElectricGuitar newInstrument = new ElectricGuitar();
                                newInstrument.Init();
                                Console.WriteLine("Добавляемый элемент");
                                newInstrument.Show();
                                instrumentsTable.Add(newInstrument.GetHashCode(), newInstrument);
                                if (count == instrumentsTable.GetCount())
                                {
                                    Console.WriteLine("Элемент не добавлен");
                                }
                                else { Console.WriteLine("Элемент добавлен"); }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка: {ex.Message}");
                            }
                        }
                        break;
                }
            }
        }



        static MusicalInstrument GenerateRandomInstrument() // Принимаем индекс в GenerateRandomInstrument
        {
            Random rand = new Random();
            switch (rand.Next(3))
            {
                case 0:
                    Guitar g = new Guitar();
                    g.RandomInit();
                    
                    return g;
                case 1:
                    Piano p = new Piano();
                    p.RandomInit();
                    
                    return p;
                case 2:
                    ElectricGuitar e = new ElectricGuitar();
                    e.RandomInit();
                    
                    return e;
                default:
                    MusicalInstrument m = new MusicalInstrument();
                    m.RandomInit();
                   
                    return m;
            }
        }
        private static void BiCaseMenu(ref DoublyLinkedList<MusicalInstrument> instrumentsList)
        {

            Random rand = new Random();
            while (true)
            {
                int pointsCaseMenu = 8;

                Console.WriteLine("\nМеню работы с двунаправленным списком");
                Console.WriteLine("1 - Формирование двунаправленного списка");
                Console.WriteLine("2 - Добавление элемента в список");
                Console.WriteLine("3 - Удаление элемента из списка");
                Console.WriteLine("4 - Печать списка");
                Console.WriteLine("5 - Добавление в список элементов с номерами 1, 3, 5 и т.д.");
                Console.WriteLine("6 - Удаление списка из памяти");
                Console.WriteLine("7 - Очистка истории");
                Console.WriteLine("8 - Клонирование списка");
                Console.WriteLine("0 - Выход из меню");

                int choiceCaseMenu = InputInt(0, pointsCaseMenu);

                if (choiceCaseMenu == 0)
                {
                    Console.WriteLine("\n0 - Выход из меню");
                    break;
                }

                switch (choiceCaseMenu)
                {
                    case 1:
                        {
                            instrumentsList.DeepClear();
                            Console.WriteLine("\n1 - Формирование двунаправленного списка");
                            Console.WriteLine("Введите количество объектов для формирования списка:");
                            int number = InputInt(1, 100);

                            Console.WriteLine("Список:");
                            for (int i = 0; i < number; i++)
                            {
                                int r = rand.Next(1, 4);
                                switch (r)
                                {
                                    case 1:
                                        {
                                            ElectricGuitar e = new ElectricGuitar();
                                            e.RandomInit();
                                            instrumentsList.AddLast(e);
                                        }
                                        break;
                                    case 2:
                                        {
                                            Piano p = new Piano();
                                            p.RandomInit();
                                            instrumentsList.AddLast(p);
                                        }
                                        break;
                                    case 3:
                                        {
                                            Guitar g = new Guitar();
                                            g.RandomInit();
                                            instrumentsList.AddLast(g);
                                        }
                                        break;
                                    case 4:
                                        {
                                            MusicalInstrument m = new MusicalInstrument();
                                            m.RandomInit();
                                            instrumentsList.AddLast(m);
                                        }
                                        break;
                                }
                            }
                            Console.WriteLine("Формирование двунаправленного списка завершено");
                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine("\n- Добавление элемента в список");
                            Console.WriteLine("1 - Электрогитара");
                            Console.WriteLine("2 - Пианино");
                            Console.WriteLine("3 - Гитара");
                            Console.WriteLine("4 - Музыкальный инструмент");

                            int z = InputInt(0, 4);
                            switch (z)
                            {
                                case 1:
                                    {
                                        ElectricGuitar e = new ElectricGuitar();
                                        e.Init();
                                        instrumentsList.AddLast(e);
                                    }
                                    break;
                                case 2:
                                    {
                                        Piano p = new Piano();
                                        p.Init();
                                        instrumentsList.AddLast(p);
                                    }
                                    break;
                                case 3:
                                    {
                                        Guitar g = new Guitar();
                                        g.Init();
                                        instrumentsList.AddLast(g);
                                    }
                                    break;
                                case 4:
                                    {
                                        MusicalInstrument m = new MusicalInstrument();
                                        m.Init();
                                        instrumentsList.AddLast(m);
                                    }
                                    break;
                            }


                            Console.WriteLine("Добавление элемента в список завершено");
                        }
                        break;
                    case 3:
                        {



                            Console.WriteLine("\n3 - Удаление элементов из списка");
                            if (instrumentsList.GetCount() == 0)
                            {
                                Console.WriteLine("Список пустой.");
                                break;
                            }
                            // Запрашиваем у пользователя имя элемента для удаления
                            Console.WriteLine("Введите имя элемента для удаления:");
                            string nameToRemove = Console.ReadLine();

                            // Проверяем, есть ли элемент с заданным именем в списке и удаляем его со всеми последующими элементами
                            bool removed = instrumentsList.RemoveFrom(nameToRemove);

                            // Если удаление прошло успешно, сообщаем об этом пользователю
                            if (removed)
                            {
                                Console.WriteLine("Элементы, начиная с элемента с именем '{0}', были успешно удалены из списка.", nameToRemove);
                            }
                            else
                            {
                                Console.WriteLine("Не удалось удалить элементы, начиная с элемента с именем '{0}'.", nameToRemove);
                            }
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("\n4 - Печать списка");
                            if (instrumentsList.GetCount() == 0)
                            {
                                Console.WriteLine("Список пустой.");
                                break;
                            }
                            instrumentsList.Print();
                            Console.WriteLine("Печать списка завершена");
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("\n5 - Добавление в список элементов с номерами 1, 3, 5 и т.д.");
                            instrumentsList = instrumentsList.AddRandomElementsToOddPositions(instrumentsList);
                            Console.WriteLine("Добавление в список элементов с номерами 1, 3, 5 и т.д. завершено");
                        }
                        break;
                    case 6:
                        {
                            Console.WriteLine("\n6 - Удаление списка из памяти");
                            instrumentsList.DeepClear();
                            Console.WriteLine("Удаление списка из памяти завершено");
                        }
                        break;
                    case 7:
                        {
                            Console.Clear();
                            Console.WriteLine("История была очищена");
                        }
                        break;
                    case 8:
                        {
                            Console.WriteLine("\nВывод клонированного списка:");
                            DoublyLinkedList<MusicalInstrument> clonedList = instrumentsList.DeepClone();
                            clonedList.Print();
                            Console.WriteLine("\nВывод оригинального списка списка:");
                            instrumentsList.Print();
                            break;
                        }
                }
            }

        }
    }
}
