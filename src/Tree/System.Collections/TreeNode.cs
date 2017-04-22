using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace System.Collections
{
	/// <summary>
	/// Представляет узел в дереве элементов.
	/// </summary>
	public class TreeNode
	{
		/// <summary>
		/// Возвращает значение узла.
		/// </summary>
		/// <value>Значение</value>
		public object Value { get; set; }

		/// <summary>
		/// Возвращает родительский узел дерева для текущего узла дерева.
		/// </summary>
		/// <value>Родитель</value>
		public TreeNode Parent { get; internal set; }

		/// <summary>
		/// Возвращает коллекцию узлов, вложенных в этот узел дерева.
		/// </summary>
		/// <value>Дочерние элементы</value>
		public TreeNodeCollection Children { get; } = new TreeNodeCollection();

		/// <summary>
		/// Возвращает предыдущий одноуровневый элемент узла дерева.
		/// </summary>
		/// <value>Предыдущий узел</value>
		public TreeNode PrevNode { get; }

		/// <summary>
		/// Возвращает следующий одноуровневый элемент узла дерева.
		/// </summary>
		/// <value>Следующий узел</value>
		public TreeNode NextNode { get; }

		/// <summary>
		/// Возвращает значение, указывающее является ли узел корневым.
		/// </summary>
		/// <value><c>true</c> if is root; otherwise, <c>false</c>.</value>
		public bool IsRoot => Parent != null;

		/// <summary>
		/// Возвращает позицию узла в коллекции дочерних элементов родителя.
		/// </summary>
		/// <value>Индекс</value>
		public int Index { get; internal set; }

		/// <summary>
		/// Возвращает отсчитываемую от нуля глубину узла дерева.
		/// </summary>
		/// <value>Глубина</value>
		public int Level { get; internal set; }

		public IEnumerable<TreeNode> AllParents
		{
			get
			{
				var currentNode = this;

				while (!currentNode.IsRoot)
				{
					currentNode = currentNode.Parent;
					yield return currentNode;
				}
			}
		}

		public TreeNode()
		{
			Children.Parent = this;
		}
	}
}
