using System;
using laba12;
using System.ComponentModel.Design;
using лаба10;
using lab12dot7;
using System.Xml.Linq;
namespace lab12dot7
{

    public class BalancedBinaryTree<T> where T : MusicalInstrument
    {
        private class TreeNode
        {
            public T Data;
            public TreeNode LeftChild;
            public TreeNode RightChild;

            public TreeNode(T data)
            {
                Data = data;
            }
        }

        private TreeNode _root;

        public BalancedBinaryTree(T[] elements)
        {
            _root = ConstructBalancedTree(elements, 0, elements.Length - 1);
        }

        private TreeNode ConstructBalancedTree(T[] elements, int start, int end)
        {
            if (start > end)
            {
                return null;
            }

            int mid = (start + end) / 2;
            TreeNode node = new TreeNode(elements[mid]);

            node.LeftChild = ConstructBalancedTree(elements, start, mid - 1);
            node.RightChild = ConstructBalancedTree(elements, mid + 1, end);

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
                    Console.Write(currentNode.Data + " ");

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
            if (node.RightChild == null)
            {
                return node.Data;
            }

            return FindMax(node.RightChild);
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
        public bool Remove(T item)
        {
            if (_root == null)
            {
                return false; // Дерево пустое, элемент не найден
            }

            TreeNode parent = null;
            TreeNode current = _root;

            // Находим удаляемый узел и его родителя
            while (current != null && !current.Data.Equals(item))
            {
                parent = current;
                if (item.CompareTo(current.Data) < 0)
                {
                    current = current.LeftChild;
                }
                else
                {
                    current = current.RightChild;
                }
            }

            if (current == null)
            {
                return false; // Элемент не найден в дереве
            }

            // У удаляемого узла нет детей
            if (current.LeftChild == null && current.RightChild == null)
            {
                if (current != _root)
                {
                    if (parent.LeftChild == current)
                    {
                        parent.LeftChild = null;
                    }
                    else
                    {
                        parent.RightChild = null;
                    }
                }
                else
                {
                    _root = null;
                }
            }
            // У удаляемого узла есть только один ребенок
            else if (current.RightChild == null)
            {
                if (current != _root)
                {
                    if (parent.LeftChild == current)
                    {
                        parent.LeftChild = current.LeftChild;
                    }
                    else
                    {
                        parent.RightChild = current.LeftChild;
                    }
                }
                else
                {
                    _root = current.LeftChild;
                }
            }
            else if (current.LeftChild == null)
            {
                if (current != _root)
                {
                    if (parent.LeftChild == current)
                    {
                        parent.LeftChild = current.RightChild;
                    }
                    else
                    {
                        parent.RightChild = current.RightChild;
                    }
                }
                else
                {
                    _root = current.RightChild;
                }
            }
            // У удаляемого узла есть оба ребенка
            else
            {
                TreeNode successor = GetSuccessor(current);
                if (current != _root)
                {
                    if (parent.LeftChild == current)
                    {
                        parent.LeftChild = successor;
                    }
                    else
                    {
                        parent.RightChild = successor;
                    }
                }
                else
                {
                    _root = successor;
                }
                successor.LeftChild = current.LeftChild;
            }

            return true; // Элемент успешно удален из дерева
        }

        // Вспомогательная функция для нахождения преемника узла
        private TreeNode GetSuccessor(TreeNode node)
        {
            TreeNode parentOfSuccessor = null;
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
    }
}

