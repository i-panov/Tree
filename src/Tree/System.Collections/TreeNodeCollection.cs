using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace System.Collections
{
	public class TreeNodeCollection : IList<TreeNode>
	{
		private List<TreeNode> _nodes = new List<TreeNode>();

		public TreeNode Parent { get; internal set; }

	    public int Level => Parent?.Level + 1 ?? 0;

        /// <summary>
        /// Возвращает общее число TreeNode объектов в коллекции.
        /// </summary>
		public int Count => _nodes.Count;

        /// <summary>
        /// Возвращает значение, указывающее, является ли коллекция доступной только для чтения.
        /// </summary>
		public bool IsReadOnly { get; } = false;

        /// <summary>
        /// Возвращает или задает TreeNode по указанному индексу в коллекции.
        /// </summary>
        /// <param name="index">Индекс элемента в дереве</param>
        /// <returns>Элемент, расположенный по данному индексу</returns>
		public TreeNode this[int index]
		{
			get
			{
				return _nodes[index];
			}

			set
			{
			    _nodes[index].Parent = null;
                value.SetParent(Parent);
			    value.Index = index;
                _nodes[index] = value;
            }
		}

        /// <summary>
        /// Создает узел дерева с указанным текстом и вставляет его по указанному индексу.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		public void Insert(int index, TreeNode value)
		{
            value.Parent?.Children.Remove(value);
            value.SetParent(Parent);
            value.Index = index;
            _nodes.Insert(index, value);
		}

        /// <summary>
        /// Добавляет новый узел дерева в конец текущей коллекции узлов дерева.
        /// </summary>
        /// <param name="value"></param>
		public void Add(TreeNode value)
		{
		    value.Parent?.Children.Remove(value);
            value.SetParent(Parent);
            value.Index = _nodes.Count;
			_nodes.Add(value);
		}

        /// <summary>
        /// Удаляет указанный узел дерева из коллекции узлов дерева.
        /// </summary>
        /// <param name="index"></param>
		public bool Remove(TreeNode value)
		{
			var result = _nodes.Remove(value);

		    if (result)
		        value.SetParent(null);

		    return result;
		}

        /// <summary>
        /// Удаляет узел дерева из коллекции узлов дерева с заданным индексом.
        /// </summary>
        /// <param name="index"></param>
		public void RemoveAt(int index)
		{
		    _nodes[index].SetParent(null);
			_nodes.RemoveAt(index);
		}

        /// <summary>
        /// Удаляет все узлы дерева из коллекции.
        /// </summary>
		public void Clear()
		{
            _nodes.ForEach(x => x.SetParent(null));
			_nodes.Clear();
		}

        /// <summary>
        /// Возвращает перечислитель, который может использоваться для итерации по коллекции узлов дерева.
        /// </summary>
        /// <returns></returns>
		public IEnumerator<TreeNode> GetEnumerator() => _nodes.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

        /// <summary>
        /// Возвращает индекс указанного узла дерева в коллекции.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(TreeNode value) => _nodes.IndexOf(value);

        /// <summary>
        /// Определяет, является ли указанный узел дерева членом коллекции.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public bool Contains(TreeNode value) => _nodes.Contains(value);

        /// <summary>
        /// Копирует всю коллекцию в массив, существующие в указанной позиции в массиве.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
		public void CopyTo(TreeNode[] array, int index) => _nodes.CopyTo(array, index);
	}
}
