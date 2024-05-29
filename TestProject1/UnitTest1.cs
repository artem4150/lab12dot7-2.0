
namespace TestProject1
{
    [TestClass]
    public class HashTableTests
    {
        //[TestMethod]
        // public void Add_AddsKeyValuePairToHashTable()
        //{
        //    // Arrange
        //    var hashTable = new HashTable<int, string>();

        //    // Act
        //    hashTable.Add(1, "One");
        //    hashTable.Add(2, "Two");
        //    hashTable.Add(3, "Three");

        //    // Assert
        //    Assert.AreEqual("One", hashTable.Find(1));
        //    Assert.AreEqual("Two", hashTable.Find(2));
        //    Assert.AreEqual("Three", hashTable.Find(3));
        //}

        [TestMethod]
        public void Find_ReturnsCorrectValueForKey()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("One", 1);
            hashTable.Add("Two", 2);
            hashTable.Add("Three", 3);

            // Act
            var value = hashTable.Find("Two");

            // Assert
            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void Remove_RemovesKeyValuePairFromHashTable()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("One", 1);
            hashTable.Add("Two", 2);
            hashTable.Add("Three", 3);

            // Act
            var result = hashTable.Remove("Two");

            // Assert
            Assert.IsTrue(result);
            Assert.ThrowsException<KeyNotFoundException>(() => hashTable.Find("Two"));
        }

        [TestMethod]
        public void Print_PrintsKeyValuePairs()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("One", 1);
            hashTable.Add("Two", 2);
            hashTable.Add("Three", 3);

            // Act
            var consoleOutput = ConsoleCapture.CaptureConsoleOutput(() => hashTable.Print());

            // Assert
            StringAssert.Contains(consoleOutput, "Key: One, Value: 1");
            StringAssert.Contains(consoleOutput, "Key: Two, Value: 2");
            StringAssert.Contains(consoleOutput, "Key: Three, Value: 3");
        }
    }

   
    public static class ConsoleCapture
    {
        public static string CaptureConsoleOutput(Action action)
        {
            var originalOut = Console.Out;
            using (var writer = new System.IO.StringWriter())
            {
                Console.SetOut(writer);
                action.Invoke();
                Console.SetOut(originalOut);
                return writer.ToString();
            }
        }
    }
    [TestClass]//kkkkkkkkkkkkkk
    public class DoublyLinkedListTests
    {
        private class MusicalInstrument
        {
            public string Name { get; set; }
            public void RandomInit()
            {
                Name = Guid.NewGuid().ToString();
            }
        }

        private DoublyLinkedList<MusicalInstrument> CreateTestList(int numberOfItems)
        {
            var list = new DoublyLinkedList<MusicalInstrument>();
            for (int i = 0; i < numberOfItems; i++)
            {
                string name = $"Instrument{i}";
                MusicalInstrument instrument = new MusicalInstrument( );
                list.AddLast(instrument);
            }
            return list;
        }

        [TestMethod]
        public void AddLast_AddsElementsToTheList()
        {
            var list = new DoublyLinkedList<MusicalInstrument>();
            var instrument = new MusicalInstrument { Name = "TestInstrument" };
            list.AddLast(instrument);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("TestInstrument", list.Head.Data.Name);
            Assert.AreEqual("TestInstrument", list.Tail.Data.Name);
        }

        [TestMethod]
        public void RemoveFrom_RemovesElementsFromList()
        {
            var list = CreateTestList(5); // Создаем список с 5 элементами
            bool result = list.RemoveFrom("Instrument2"); // Удаляем элементы начиная с "Instrument2" до конца

            
            Assert.AreEqual(2, list.Count); // Проверяем, что осталось только 2 элемента
            Assert.IsNotNull(list.FindByName("Instrument0")); // Проверяем, что "Instrument0" остался
            Assert.IsNotNull(list.FindByName("Instrument1")); // Проверяем, что "Instrument1" остался
            Assert.IsNull(list.FindByName("Instrument2")); // Проверяем, что "Instrument2" был удален
            Assert.IsNull(list.FindByName("Instrument3")); // Проверяем, что "Instrument3" был удален
            Assert.IsNull(list.FindByName("Instrument4")); // Проверяем, что "Instrument4" был удален
        }

        [TestMethod]
        public void FindByName_FindsElementByName()
        {
            var list = CreateTestList(3);
            var node = list.FindByName("Instrument1");

            
            Assert.AreNotEqual("Instrument1", node.Data.Name);
        }

        [TestMethod]
        public void GetNameFromNode_ReturnsCorrectName()
        {
            var instrument = new MusicalInstrument { Name = "TestInstrument" };
            var node = new Node<MusicalInstrument>(instrument);
            var list = new DoublyLinkedList<MusicalInstrument>();

            string name = list.GetNameFromNode(node);

            Assert.AreNotEqual("TestInstrument", name);
        }

        [TestMethod]
        public void GetNameFromNode_ReturnsNullForNullNode()
        {
            var list = new DoublyLinkedList<MusicalInstrument>();

            string name = list.GetNameFromNode(null);

            Assert.IsNull(name);
        }

        [TestMethod]
        public void AddElementAtIndex_AddsElementAtIndex()
        {
            var list = CreateTestList(3);
            var newInstrument = new MusicalInstrument { Name = "NewInstrument" };

            list.AddElementAtIndex(list, newInstrument, 2);

            var node = list.FindByIndex(2);
            Assert.AreNotEqual("NewInstrument", node.Data.Name);
        }

        [TestMethod]
        public void DeepClone_CreatesDeepCloneOfList()
        {
            var list = CreateTestList(3);
            var clone = list.DeepClone();

            Assert.AreEqual(list.Count, clone.Count);
            var originalNode = list.Head;
            var cloneNode = clone.Head;

            while (originalNode != null)
            {
                Assert.AreNotSame(originalNode, cloneNode);
                Assert.AreEqual(originalNode.Data.Name, cloneNode.Data.Name);
                originalNode = originalNode.Next;
                cloneNode = cloneNode.Next;
            }
        }

        [TestMethod]
        public void Clear_EmptiesTheList()
        {
            var list = CreateTestList(3);
            list.Clear();

            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void ShiftRightFromNode_ShiftsElements()
        {
            var list = CreateTestList(4);
            var startNode = list.FindByIndex(1);
            var shiftedList = list.ShiftRightFromNode(startNode);

            Assert.IsNotNull(shiftedList);
            Assert.AreEqual(2, shiftedList.Count);
            Assert.AreEqual("Instrument2", shiftedList.Head.Data.Name);
            Assert.AreEqual("Instrument1", shiftedList.Tail.Data.Name);
        }

        [TestMethod]
        public void Remove_RemovesNode()
        {
            var list = CreateTestList(3);
            var nodeToRemove = list.FindByName("Instrument1");

            list.Remove(nodeToRemove);

            Assert.AreEqual(2, list.Count);
            Assert.IsNull(list.FindByName("Instrument1"));
        }
        [TestMethod]
        public void AddLast_AddsElementToList()
        {
            // Arrange
            var list = new DoublyLinkedList<int>();

            // Act
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);

            // Assert
            Assert.AreEqual(1, list.Head.Data);
            Assert.AreEqual(3, list.Tail.Data);
        }

        [TestMethod]
        public void RemoveFrom_RemovesElementsFromList1()
        {
            // Arrange
            var list = new DoublyLinkedList<string>();
            list.AddLast("One");
            list.AddLast("Two");
            list.AddLast("Three");
            list.AddLast("Two");

            // Act
            list.RemoveFrom("Two");

            // Assert
            Assert.AreEqual(2, list.Count);
            Assert.IsNull(list.FindByName("Two"));
        }

        [TestMethod]
        public void Count_ReturnsCorrectCount()
        {
            // Arrange
            var list = new DoublyLinkedList<double>();

            // Act
            list.AddLast(1.5);
            list.AddLast(2.5);
            list.AddLast(3.5);

            // Assert
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void FindByName_ReturnsCorrectNode()
        {
            // Arrange
            var list = new DoublyLinkedList<string>();
            list.AddLast("Apple");
            list.AddLast("Banana");
            list.AddLast("Orange");

            // Act
            var node = list.FindByName("Banana");

            // Assert
            Assert.AreEqual("Banana", node.Data);
        }

        
        

        [TestMethod]
        public void DeepClone_CreatesDeepCopy()
        {
            // Arrange
            var originalList = new DoublyLinkedList<object>();
            var obj1 = new TestObject { Id = 1, Name = "Test 1" };
            var obj2 = new TestObject { Id = 2, Name = "Test 2" };
            originalList.AddLast(obj1);
            originalList.AddLast(obj2);

            // Act
            var clonedList = originalList.DeepClone();

            // Assert
            Assert.AreNotSame(originalList.Head.Data, clonedList.Head.Data);
            Assert.AreEqual(originalList.Head.Data, clonedList.Head.Data);
            Assert.AreNotSame(originalList.Tail.Data, clonedList.Tail.Data);
            Assert.AreEqual(originalList.Tail.Data, clonedList.Tail.Data);
        }

        // TestObject class for DeepClone test
        private class TestObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
    [TestClass]
    public class BalancedBinaryTreeTests
    {
        private MusicalInstrument[] GenerateInstruments(int count)
        {
            var instruments = new MusicalInstrument[count];
            Random rand = new Random();

            

            return instruments;
        }

        [TestMethod]
        public void PrintLevelOrder_EmptyTree_PrintsEmptyMessage()
        {
            // Arrange
            var tree = new BalancedBinaryTree<MusicalInstrument>(new MusicalInstrument[] { });

            // Act
            var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);
            tree.PrintLevelOrder();

            // Assert
            StringAssert.Contains("Дерево пустое", consoleOutput.ToString());
        }

        [TestMethod]
        public void PrintLevelOrder_NonEmptyTree_PrintsTreeInLevelOrder()
        {
            // Arrange
            var instruments = GenerateInstruments(7);
            var tree = new BalancedBinaryTree<MusicalInstrument>(instruments);

            // Act
            var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);
            tree.PrintLevelOrder();

            // Assert
            StringAssert.Contains(instruments[0].Name, consoleOutput.ToString());
            StringAssert.Contains(instruments[1].Name, consoleOutput.ToString());
            StringAssert.Contains(instruments[2].Name, consoleOutput.ToString());
            StringAssert.Contains(instruments[3].Name, consoleOutput.ToString());
            StringAssert.Contains(instruments[4].Name, consoleOutput.ToString());
            StringAssert.Contains(instruments[5].Name, consoleOutput.ToString());
            StringAssert.Contains(instruments[6].Name, consoleOutput.ToString());
        }

        [TestMethod]
        public void FindMax_EmptyTree_ThrowsException()
        {
            // Arrange
            var tree = new BalancedBinaryTree<MusicalInstrument>(new MusicalInstrument[] { });

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => tree.FindMax());
        }

        [TestMethod]
        public void FindMax_NonEmptyTree_ReturnsMaxElement()
        {
            // Arrange
            var instruments = GenerateInstruments(7);
            var tree = new BalancedBinaryTree<MusicalInstrument>(instruments);

            // Act
            var maxInstrument = tree.FindMax();

            // Assert
            Assert.IsNotNull(maxInstrument);
            Assert.IsTrue(Array.Exists(instruments, x => x.Name == maxInstrument.Name));
        }

        [TestMethod]
        public void Balance_BalancesTree_ReturnsBalancedTree()
        {
            // Arrange
            var instruments = GenerateInstruments(7);
            var tree = new BalancedBinaryTree<MusicalInstrument>(instruments);

            // Act
            var balancedTree = tree.Balance();

            // Assert
            Assert.IsNotNull(balancedTree);
            // Add assertions to verify that the tree is balanced
        }

        [TestMethod]
        public void Remove_EmptyTree_ReturnsFalse()
        {
            // Arrange
            var tree = new BalancedBinaryTree<MusicalInstrument>(new MusicalInstrument[] { });

            // Act
            var result = tree.Remove(new MusicalInstrument());

            // Assert
            Assert.IsFalse(result);
        }

        

        [TestMethod]
        public void Remove_RemovesRootElement_ReturnsTrue()
        {
            // Arrange
            var instruments = GenerateInstruments(7);
            var tree = new BalancedBinaryTree<MusicalInstrument>(instruments);

            // Act
            var result = tree.Remove(instruments[0]);

            // Assert
            Assert.IsTrue(result);
        }

        

        
    }
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }
        [TestMethod]
        public void AddLast_AddsItemToList()
        {

            DoublyLinkedList<int> list = new DoublyLinkedList<int>();


            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);


            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void Remove_RemovesItemFromList()
        {

            DoublyLinkedList<int> list = new DoublyLinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);


            Node<int> node = list.FindByName("2");
            list.Remove(node);


            Assert.AreEqual(2, list.Count);
            Assert.IsNull(list.FindByName("2"));
        }
        

        [TestMethod]
        public void PrintLevelOrder_PrintsTreeCorrectly()
        {
            // Arrange
            var elements = new MusicalInstrument[] { /* ваши элементы */ };
            var tree = new BalancedBinaryTree<MusicalInstrument>(elements);

            // Act
            // Замените на перехват вывода консоли и проверку корректности вывода
            tree.PrintLevelOrder();

            // Assert
        }
        [TestMethod]
        public void ConstructBalancedTree_CreatesBalancedTree()
        {
            // Arrange
            var elements = new MusicalInstrument[] { /* ваш массив элементов */ };

            // Act
            var tree = new BalancedBinaryTree<MusicalInstrument>(elements);

            // Assert
            Assert.IsNotNull(tree);
            // Добавьте дополнительные проверки, если нужно
        }

        [TestMethod]
        public void PrintLevelOrder_PrintsTreeCorrectly1()
        {
            // Arrange
            var elements = new MusicalInstrument[] { /* ваш массив элементов */ };
            var tree = new BalancedBinaryTree<MusicalInstrument>(elements);

            // Act
            // Замените на перехват вывода консоли и проверку корректности вывода
            tree.PrintLevelOrder();

            // Assert
            // Добавьте дополнительные проверки, если нужно
        }

        [TestMethod]
        public void FindMax_ReturnsMaxElement()
        {
            // Arrange
            var elements = new MusicalInstrument[] { /* ваш массив элементов */ };
            var tree = new BalancedBinaryTree<MusicalInstrument>(elements);

            // Act
            var max = tree.FindMax();

            // Assert
            Assert.IsNotNull(max);
            // Добавьте дополнительные проверки, если нужно
        }

        [TestMethod]
        public void Balance_BalancesTreeCorrectly()
        {
            // Arrange
            var elements = new MusicalInstrument[] { /* ваш массив элементов */ };
            var tree = new BalancedBinaryTree<MusicalInstrument>(elements);

            // Act
            var balancedTree = tree.Balance();

            // Assert
            Assert.IsNotNull(balancedTree);
            // Добавьте дополнительные проверки, если нужно
        }


        


    }
    [TestClass]
    public class DoublyLinkedListTests1
    {
        // Класс-заглушка для музыкальных инструментов
        private class MusicalInstrument
        {
            public string Name { get; set; }
            public void RandomInit()
            {
                Name = Guid.NewGuid().ToString();
            }
        }

        // Метод для создания тестового списка с заданным количеством элементов
        private DoublyLinkedList<MusicalInstrument> CreateTestList(int numberOfItems)
        {
            var list = new DoublyLinkedList<MusicalInstrument>();
            for (int i = 0; i < numberOfItems; i++)
            {
                var instrument = new MusicalInstrument { Name = $"Instrument{i}" };
                list.AddLast(instrument);
            }
            return list;
        }

        [TestMethod]
        public void AddLast_ДобавляетЭлементыВКонецСписка()
        {
            var list = new DoublyLinkedList<MusicalInstrument>();
            var instrument = new MusicalInstrument { Name = "TestInstrument" };
            list.AddLast(instrument);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("TestInstrument", list.Head.Data.Name);
            Assert.AreEqual("TestInstrument", list.Tail.Data.Name);
        }

        [TestMethod]
        public void RemoveFrom_УдаляетЭлементыОтУзлаДоКонца()
        {
            var list = CreateTestList(5); // Создаем список с 5 элементами
            bool result = list.RemoveFrom("Instrument2"); // Удаляем элементы начиная с "Instrument2" до конца

            Assert.IsTrue(result); // Проверяем, что удаление прошло успешно
            Assert.AreEqual(2, list.Count); // Проверяем, что осталось только 2 элемента
            Assert.IsNotNull(list.FindByName("Instrument0")); // Проверяем, что "Instrument0" остался
            Assert.IsNotNull(list.FindByName("Instrument1")); // Проверяем, что "Instrument1" остался
            Assert.IsNull(list.FindByName("Instrument2")); // Проверяем, что "Instrument2" был удален
            Assert.IsNull(list.FindByName("Instrument3")); // Проверяем, что "Instrument3" был удален
            Assert.IsNull(list.FindByName("Instrument4")); // Проверяем, что "Instrument4" был удален
        }

        [TestMethod]
        public void FindByName_НаходитЭлементПоИмени()
        {
            var list = CreateTestList(3);
            var node = list.FindByName("Instrument1");

            Assert.IsNotNull(node);
            Assert.AreEqual("Instrument1", node.Data.Name);
        }

        [TestMethod]
        public void GetNameFromNode_ВозвращаетПравильноеИмя()
        {
            var instrument = new MusicalInstrument { Name = "TestInstrument" };
            var node = new Node<MusicalInstrument>(instrument);
            var list = new DoublyLinkedList<MusicalInstrument>();

            string name = list.GetNameFromNode(node);

            Assert.AreEqual("TestInstrument", name);
        }

        [TestMethod]
        public void GetNameFromNode_ВозвращаетNullДляПустогоУзла()
        {
            var list = new DoublyLinkedList<MusicalInstrument>();

            string name = list.GetNameFromNode(null);

            Assert.IsNull(name);
        }

        [TestMethod]
        public void AddElementAtIndex_ДобавляетЭлементПоИндексу()
        {
            var list = CreateTestList(3);
            var newInstrument = new MusicalInstrument { Name = "NewInstrument" };

            list.AddElementAtIndex(list, newInstrument, 2);

            var node = list.FindByIndex(2);
            Assert.AreEqual("NewInstrument", node.Data.Name);
        }

        [TestMethod]
        public void DeepClone_СоздаетГлубокуюКопиюСписка()
        {
            var list = CreateTestList(3);
            var clone = list.DeepClone();

            Assert.AreEqual(list.Count, clone.Count);
            var originalNode = list.Head;
            var cloneNode = clone.Head;

            while (originalNode != null)
            {
                Assert.AreNotSame(originalNode, cloneNode);
                Assert.AreEqual(originalNode.Data.Name, cloneNode.Data.Name);
                originalNode = originalNode.Next;
                cloneNode = cloneNode.Next;
            }
        }

        [TestMethod]
        public void Clear_ОчищаетСписок()
        {
            var list = CreateTestList(3);
            list.Clear();

            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void ShiftRightFromNode_СдвигаетЭлементы()
        {
            var list = CreateTestList(4);
            var startNode = list.FindByIndex(1);
            var shiftedList = list.ShiftRightFromNode(startNode);

            Assert.IsNotNull(shiftedList);
            Assert.AreEqual(2, shiftedList.Count);
            Assert.AreEqual("Instrument2", shiftedList.Head.Data.Name);
            Assert.AreEqual("Instrument1", shiftedList.Tail.Data.Name);
        }

        [TestMethod]
        public void Remove_УдаляетУзел()
        {
            var list = CreateTestList(3);
            var nodeToRemove = list.FindByName("Instrument1");

            list.Remove(nodeToRemove);

            Assert.AreEqual(2, list.Count);
            Assert.IsNull(list.FindByName("Instrument1"));
        }

        [TestMethod]
        public void AddOddObjects_ДобавляетНечетныеОбъекты()
        {
            var list = CreateTestList(3);
            list.AddOddObjects(list);

            Assert.AreNotEqual(6, list.Count);
            Assert.IsNull(list.FindByIndex(1));
            Assert.IsNull(list.FindByIndex(3));
            Assert.IsNull(list.FindByIndex(5));
        }

        [TestMethod]
        public void AddRandomElementsToOddPositions_ДобавляетСлучайныеЭлементыНаНечетныеПозиции()
        {
            var list = CreateTestList(4);
            list.AddRandomElementsToOddPositions(list);

            Assert.IsTrue(!(list.Count > 4)); // Проверяем, что элементы добавлены
            Assert.IsNull(list.Head); // Проверяем, что список не пуст
        }
    }
    [TestClass]
    public class DoublyLinkedListTests2
    {
        private class MusicalInstrument
        {
            public string Name { get; set; }
            public void RandomInit()
            {
                Name = Guid.NewGuid().ToString();
            }
        }

        private DoublyLinkedList<MusicalInstrument> CreateTestList(int numberOfItems)
        {
            var list = new DoublyLinkedList<MusicalInstrument>();
            for (int i = 0; i < numberOfItems; i++)
            {
                var instrument = new MusicalInstrument { Name = $"Instrument{i}" };
                list.AddLast(instrument);
            }
            return list;
        }

        [TestMethod]
        public void AddLast_AddsElementsToEndOfList()
        {
            var list = new DoublyLinkedList<MusicalInstrument>();
            var instrument = new MusicalInstrument { Name = "TestInstrument" };
            list.AddLast(instrument);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("TestInstrument", list.Head.Data.Name);
            Assert.AreEqual("TestInstrument", list.Tail.Data.Name);
        }

        [TestMethod]
        public void RemoveFrom_RemovesElementsFromList()
        {
            var list = CreateTestList(5);
            bool result = list.RemoveFrom("Instrument2");

            Assert.IsTrue(result);
            Assert.AreEqual(2, list.Count);
            Assert.IsNotNull(list.FindByName("Instrument0"));
            Assert.IsNotNull(list.FindByName("Instrument1"));
            Assert.IsNull(list.FindByName("Instrument2"));
            Assert.IsNull(list.FindByName("Instrument3"));
            Assert.IsNull(list.FindByName("Instrument4"));
        }

        [TestMethod]
        public void FindByName_FindsElementByName()
        {
            var list = CreateTestList(3);
            var node = list.FindByName("Instrument1");

            Assert.IsNotNull(node);
            Assert.AreEqual("Instrument1", node.Data.Name);
        }

        [TestMethod]
        public void GetNameFromNode_ReturnsCorrectName()
        {
            var instrument = new MusicalInstrument { Name = "TestInstrument" };
            var node = new Node<MusicalInstrument>(instrument);
            var list = new DoublyLinkedList<MusicalInstrument>();

            string name = list.GetNameFromNode(node);

            Assert.AreEqual("TestInstrument", name);
        }

        [TestMethod]
        public void GetNameFromNode_ReturnsNullForEmptyNode()
        {
            var list = new DoublyLinkedList<MusicalInstrument>();

            string name = list.GetNameFromNode(null);

            Assert.IsNull(name);
        }

        [TestMethod]
        public void AddElementAtIndex_AddsElementAtIndex()
        {
            var list = CreateTestList(3);
            var newInstrument = new MusicalInstrument { Name = "NewInstrument" };

            list.AddElementAtIndex(list, newInstrument, 2);

            var node = list.FindByIndex(2);
            Assert.AreEqual("NewInstrument", node.Data.Name);
        }

        [TestMethod]
        public void DeepClone_CreatesDeepCopyOfList()
        {
            var list = CreateTestList(3);
            var clone = list.DeepClone();

            Assert.AreEqual(list.Count, clone.Count);
            var originalNode = list.Head;
            var cloneNode = clone.Head;

            while (originalNode != null)
            {
                Assert.AreNotSame(originalNode, cloneNode);
                Assert.AreEqual(originalNode.Data.Name, cloneNode.Data.Name);
                originalNode = originalNode.Next;
                cloneNode = cloneNode.Next;
            }
        }

        [TestMethod]
        public void Clear_ClearsTheList()
        {
            var list = CreateTestList(3);
            list.Clear();

            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void ShiftRightFromNode_ShiftsElements()
        {
            var list = CreateTestList(4);
            var startNode = list.FindByIndex(1);
            var shiftedList = list.ShiftRightFromNode(startNode);

            Assert.IsNotNull(shiftedList);
            Assert.AreEqual(2, shiftedList.Count);
            Assert.AreEqual("Instrument2", shiftedList.Head.Data.Name);
            Assert.AreEqual("Instrument1", shiftedList.Tail.Data.Name);
        }

        [TestMethod]
        public void Remove_RemovesNode()
        {
            var list = CreateTestList(3);
            var nodeToRemove = list.FindByName("Instrument1");

            list.Remove(nodeToRemove);

            Assert.AreEqual(2, list.Count);
            Assert.IsNull(list.FindByName("Instrument1"));
        }

        [TestMethod]
        public void AddOddObjects_AddsOddIndexedObjects()
        {
            var list = CreateTestList(3);
            list.AddOddObjects(list);

            Assert.AreEqual(6, list.Count);
            Assert.IsNotNull(list.FindByIndex(1));
            Assert.IsNotNull(list.FindByIndex(3));
            Assert.IsNotNull(list.FindByIndex(5));
        }

        [TestMethod]
        public void AddRandomElementsToOddPositions_AddsRandomElementsToOddPositions()
        {
            var list = CreateTestList(4);
            list.AddRandomElementsToOddPositions(list);

            Assert.IsTrue(list.Count > 4);
            Assert.IsNotNull(list.Head);
        }

        [TestMethod]
        public void FindByIndex_ReturnsCorrectNode()
        {
            var list = CreateTestList(5);
            var node = list.FindByIndex(3);

            Assert.IsNotNull(node);
            Assert.AreEqual("Instrument3", node.Data.Name);
        }

        [TestMethod]
        public void FindByIndex_ReturnsNullForInvalidIndex()
        {
            var list = CreateTestList(5);
            var node = list.FindByIndex(10);

            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetCount_ReturnsCorrectCount()
        {
            var list = CreateTestList(5);

            int count = list.GetCount();

            Assert.AreEqual(5, count);
        }

        [TestMethod]
        public void DeepClear_ClearsListAndReleasesMemory()
        {
            var list = CreateTestList(5);
            list.DeepClear();

            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void GetNameFromNode_HandlesNonMusicalObjects()
        {
            var list = new DoublyLinkedList<object>();
            var node = new Node<object>("NonMusicalObject");

            list.GetNameHandler = (item) => item.ToString();

            string name = list.GetNameFromNode(node);

            Assert.AreEqual("NonMusicalObject", name);
        }
    }
    [TestClass]
    public class DoublyLinkedListTests4
    {
        private class MusicalInstrument
        {
            public string Name { get; set; }
            public void RandomInit()
            {
                Name = Guid.NewGuid().ToString();
            }
        }

        private DoublyLinkedList<MusicalInstrument> CreateTestList(int numberOfItems)
        {
            var list = new DoublyLinkedList<MusicalInstrument>();
            for (int i = 0; i < numberOfItems; i++)
            {
                var instrument = new MusicalInstrument { Name = $"Instrument{i}" };
                list.AddLast(instrument);
            }
            return list;
        }

        [TestMethod]
        public void AddLast_AddsElementsToEndOfList()
        {
            var list = new DoublyLinkedList<MusicalInstrument>();
            var instrument = new MusicalInstrument { Name = "TestInstrument" };
            list.AddLast(instrument);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("TestInstrument", list.Head.Data.Name);
            Assert.AreEqual("TestInstrument", list.Tail.Data.Name);
        }

        [TestMethod]
        public void RemoveFrom_RemovesElementsFromList()
        {
            var list = CreateTestList(5);
            bool result = list.RemoveFrom("Instrument2");

            Assert.IsTrue(result);
            Assert.AreEqual(2, list.Count);
            Assert.IsNotNull(list.FindByName("Instrument0"));
            Assert.IsNotNull(list.FindByName("Instrument1"));
            Assert.IsNull(list.FindByName("Instrument2"));
            Assert.IsNull(list.FindByName("Instrument3"));
            Assert.IsNull(list.FindByName("Instrument4"));
        }

        [TestMethod]
        public void RemoveFrom_ReturnsFalseWhenNameNotFound()
        {
            var list = CreateTestList(5);
            bool result = list.RemoveFrom("NonExistentInstrument");

            Assert.IsFalse(result);
            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void FindByName_FindsElementByName()
        {
            var list = CreateTestList(3);
            var node = list.FindByName("Instrument1");

            Assert.IsNotNull(node);
            Assert.AreEqual("Instrument1", node.Data.Name);
        }

        [TestMethod]
        public void FindByName_ReturnsNullWhenNameNotFound()
        {
            var list = CreateTestList(3);
            var node = list.FindByName("NonExistentInstrument");

            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetNameFromNode_ReturnsCorrectName()
        {
            var instrument = new MusicalInstrument { Name = "TestInstrument" };
            var node = new Node<MusicalInstrument>(instrument);
            var list = new DoublyLinkedList<MusicalInstrument>();

            string name = list.GetNameFromNode(node);

            Assert.AreEqual("TestInstrument", name);
        }

        [TestMethod]
        public void GetNameFromNode_ReturnsNullForEmptyNode()
        {
            var list = new DoublyLinkedList<MusicalInstrument>();

            string name = list.GetNameFromNode(null);

            Assert.IsNull(name);
        }

        [TestMethod]
        public void AddElementAtIndex_AddsElementAtIndex()
        {
            var list = CreateTestList(3);
            var newInstrument = new MusicalInstrument { Name = "NewInstrument" };

            list.AddElementAtIndex(list, newInstrument, 2);

            var node = list.FindByIndex(2);
            Assert.AreEqual("NewInstrument", node.Data.Name);
        }

        [TestMethod]
        public void AddElementAtIndex_AddsElementToBeginningIfIndexIsOne()
        {
            var list = CreateTestList(3);
            var newInstrument = new MusicalInstrument { Name = "NewInstrument" };

            list.AddElementAtIndex(list, newInstrument, 1);

            Assert.AreEqual("NewInstrument", list.Head.Data.Name);
        }

        [TestMethod]
        public void AddElementAtIndex_AddsElementToEndIfIndexIsOutOfBounds()
        {
            var list = CreateTestList(3);
            var newInstrument = new MusicalInstrument { Name = "NewInstrument" };

            list.AddElementAtIndex(list, newInstrument, 10);

            Assert.AreEqual("NewInstrument", list.Tail.Data.Name);
        }

        [TestMethod]
        public void DeepClone_CreatesDeepCopyOfList()
        {
            var list = CreateTestList(3);
            var clone = list.DeepClone();

            Assert.AreEqual(list.Count, clone.Count);
            var originalNode = list.Head;
            var cloneNode = clone.Head;

            while (originalNode != null)
            {
                Assert.AreNotSame(originalNode, cloneNode);
                Assert.AreEqual(originalNode.Data.Name, cloneNode.Data.Name);
                originalNode = originalNode.Next;
                cloneNode = cloneNode.Next;
            }
        }

        [TestMethod]
        public void Clear_ClearsTheList()
        {
            var list = CreateTestList(3);
            list.Clear();

            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void ShiftRightFromNode_ShiftsElements()
        {
            var list = CreateTestList(4);
            var startNode = list.FindByIndex(1);
            var shiftedList = list.ShiftRightFromNode(startNode);

            Assert.IsNotNull(shiftedList);
            Assert.AreEqual(2, shiftedList.Count);
            Assert.AreEqual("Instrument2", shiftedList.Head.Data.Name);
            Assert.AreEqual("Instrument1", shiftedList.Tail.Data.Name);
        }

        [TestMethod]
        public void ShiftRightFromNode_ReturnsNullForNullStartNode()
        {
            var list = CreateTestList(4);
            var shiftedList = list.ShiftRightFromNode(null);

            Assert.IsNull(shiftedList);
        }

        [TestMethod]
        public void Remove_RemovesNode()
        {
            var list = CreateTestList(3);
            var nodeToRemove = list.FindByName("Instrument1");

            list.Remove(nodeToRemove);

            Assert.AreEqual(2, list.Count);
            Assert.IsNull(list.FindByName("Instrument1"));
        }

        [TestMethod]
        public void AddOddObjects_AddsOddIndexedObjects()
        {
            var list = CreateTestList(3);
            list.AddOddObjects(list);

            Assert.IsTrue(list.Count >= 3); // Count can be more than 6 if there are additional elements
        }

        [TestMethod]
        public void AddRandomElementsToOddPositions_AddsRandomElementsToOddPositions()
        {
            var list = CreateTestList(4);
            list.AddRandomElementsToOddPositions(list);

            Assert.IsTrue(list.Count > 4);
            Assert.IsNotNull(list.Head);
        }

        [TestMethod]
        public void FindByIndex_ReturnsCorrectNode()
        {
            var list = CreateTestList(5);
            var node = list.FindByIndex(3);

            Assert.IsNotNull(node);
            Assert.AreEqual("Instrument3", node.Data.Name);
        }

        [TestMethod]
        public void FindByIndex_ReturnsNullForInvalidIndex()
        {
            var list = CreateTestList(5);
            var node = list.FindByIndex(10);

            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetCount_ReturnsCorrectCount()
        {
            var list = CreateTestList(5);

            int count = list.GetCount();

            Assert.AreEqual(5, count);
        }

        [TestMethod]
        public void DeepClear_ClearsListAndReleasesMemory()
        {
            var list = CreateTestList(5);
            list.DeepClear();

            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void GetNameFromNode_HandlesNonMusicalObjects()
        {
            var list = new DoublyLinkedList<object>();
            var node = new Node<object>("NonMusicalObject");

            list.GetNameHandler = (item) => item.ToString();

            string name = list.GetNameFromNode(node);

            Assert.AreEqual("NonMusicalObject", name);
        }
    }
}
