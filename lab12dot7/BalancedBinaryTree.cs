using System;
using System.Collections.Generic;
using lab12dot7;
using лаба10;

namespace Lab12dot7
{
    public class BalancedBinaryTree<T> where T : IComparable<T>
    {
        private class TreeNode
        {
            public T Data;
            public TreeNode LeftChild;
            public TreeNode RightChild;

            public TreeNode(T data)
            {
                Data = data;
                LeftChild = null;
                RightChild = null;
            }
        }

        private TreeNode _root;

        public BalancedBinaryTree(T[] elements)
        {
            if (elements == null || elements.Length == 0)
            {
                throw new ArgumentException("Массив элементов не может быть пустым");
            }
            Array.Sort(elements); // Убедимся, что элементы отсортированы
            _root = ConstructBalancedTree(elements, 0, elements.Length - 1);
        }
        
        private TreeNode ConstructBalancedTree(T[] elements, int start, int end)
        {
            if (start > end)
            {
                return null;
            }

            int mid = (start + end) / 2;
            TreeNode node = new TreeNode(elements[mid])
            {
                LeftChild = ConstructBalancedTree(elements, start, mid - 1),
                RightChild = ConstructBalancedTree(elements, mid + 1, end)
            };

            return node;
        }

        public void PrintLevelOrder()
        {
            if (_root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(_root);

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode currentNode = queue.Dequeue();
                    Console.Write("| "+currentNode.Data + "| ");

                    if (currentNode.LeftChild != null)
                    {
                        queue.Enqueue(currentNode.LeftChild);
                    }

                    if (currentNode.RightChild != null)
                    {
                        queue.Enqueue(currentNode.RightChild);
                    }
                }

                Console.WriteLine();
            }
        }

        public T FindMax()
        {
            if (_root == null)
            {
                throw new InvalidOperationException("Дерево пустое");
            }

            return FindMax(_root);
        }

        private T FindMax(TreeNode node)
        {
            while (node.RightChild != null)
            {
                node = node.RightChild;
            }
            return node.Data;
        }

        public BalancedBinaryTree<T> Balance()
        {
            List<T> elements = new List<T>();
            InOrderTraversal(_root, elements);

            return new BalancedBinaryTree<T>(elements.ToArray());
        }

        private void InOrderTraversal(TreeNode node, List<T> elements)
        {
            if (node == null)
            {
                return;
            }

            InOrderTraversal(node.LeftChild, elements);
            elements.Add(node.Data);
            InOrderTraversal(node.RightChild, elements);
        }

        public bool Remove(T key)
        {
            if (_root == null)
            {
                return false; // Дерево пустое, элемент не найден
            }

            TreeNode parent = null;
            TreeNode current = _root;
            bool isLeftChild = false; // Флаг, указывающий, является ли текущий узел левым ребенком

            // Находим удаляемый узел и его родителя
            while (current != null && !current.Data.Equals(key))
            {
                parent = current;
                if (key.CompareTo(current.Data) < 0)
                {
                    current = current.LeftChild;
                    isLeftChild = true;
                }
                else
                {
                    current = current.RightChild;
                    isLeftChild = false;
                }
            }

            if (current == null)
            {
                return false; // Элемент не найден в дереве
            }

            // У удаляемого узла нет детей
            if (current.LeftChild == null && current.RightChild == null)
            {
                if (current == _root)
                {
                    _root = null; // Удаляем корневой узел, если он единственный
                }
                else if (isLeftChild)
                {
                    parent.LeftChild = null;
                }
                else
                {
                    parent.RightChild = null;
                }
            }
            // У удаляемого узла есть только один ребенок
            else if (current.RightChild == null)
            {
                if (current == _root)
                {
                    _root = current.LeftChild;
                }
                else if (isLeftChild)
                {
                    parent.LeftChild = current.LeftChild;
                }
                else
                {
                    parent.RightChild = current.LeftChild;
                }
            }
            else if (current.LeftChild == null)
            {
                if (current == _root)
                {
                    _root = current.RightChild;
                }
                else if (isLeftChild)
                {
                    parent.LeftChild = current.RightChild;
                }
                else
                {
                    parent.RightChild = current.RightChild;
                }
            }
            // У удаляемого узла есть оба ребенка
            else
            {
                TreeNode successor = GetSuccessor(current);
                if (current == _root)
                {
                    _root = successor;
                }
                else if (isLeftChild)
                {
                    parent.LeftChild = successor;
                }
                else
                {
                    parent.RightChild = successor;
                }
                successor.LeftChild = current.LeftChild;
            }

            return true; // Элемент успешно удален из дерева
        }

        // Вспомогательная функция для нахождения преемника узла
        private TreeNode GetSuccessor(TreeNode node)
        {
            TreeNode parentOfSuccessor = node;
            TreeNode successor = node;
            TreeNode current = node.RightChild;
            while (current != null)
            {
                parentOfSuccessor = successor;
                successor = current;
                current = current.LeftChild;
            }
            if (successor != node.RightChild)
            {
                parentOfSuccessor.LeftChild = successor.RightChild;
                successor.RightChild = node.RightChild;
            }
            return successor;
        }
        public void DeepClear()
        {
            DeepClear(_root);
            _root = null; // Убедимся, что корневой узел установлен в null
        }
        
        private void DeepClear(TreeNode node)
        {
            if (node == null)
                return;

            // Рекурсивно вызываем DeepClear для левого и правого поддеревьев
            DeepClear(node.LeftChild);
            DeepClear(node.RightChild);

            // Освобождаем память, занятую текущим узлом
            node.LeftChild = null;
            node.RightChild = null;
        }
        public BalancedBinaryTree<T> TransformToSearchTree()
        {
            List<T> elements = new List<T>();
            InOrderTraversal(_root, elements);
            elements.Sort(); // Сортируем элементы по алфавиту

            _root = ConstructBalancedTree(elements.ToArray(), 0, elements.Count - 1); // Обновляем корневой узел

            return this; // Возвращаем измененное дерево
        }
        private TreeNode ConstructSearchTree(T[] elements, int start, int end)
        {
            if (start > end)
            {
                return null;
            }

            int mid = (start + end) / 2;
            TreeNode node = new TreeNode(elements[mid]);

            node.LeftChild = ConstructSearchTree(elements, start, mid - 1);
            node.RightChild = ConstructSearchTree(elements, mid + 1, end);

            return node;
        }
        

    }
}