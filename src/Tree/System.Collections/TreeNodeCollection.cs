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

		public int Count => _nodes.Count;

		public bool IsReadOnly { get; set; } = false;

		public TreeNodeCollection()
		{
		}

		public TreeNode this[int index]
		{
			get
			{
				return _nodes[index];
			}

			set
			{
				if (!IsReadOnly)
					_nodes[index] = value;
			}
		}

		public void Insert(int index, TreeNode value)
		{
			value.Index = index;
			_nodes.Insert(index, value);
		}

		public void Add(TreeNode value)
		{
			_nodes.Add(value);
		}

		public bool Remove(TreeNode value)
		{
			return _nodes.Remove(value);
		}

		public void RemoveAt(int index)
		{
			_nodes.RemoveAt(index);
		}

		public void Clear()
		{
			_nodes.Clear();
		}

		public IEnumerator<TreeNode> GetEnumerator() => _nodes.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

		public int IndexOf(TreeNode value) => _nodes.IndexOf(value);

		public bool Contains(TreeNode value) => _nodes.Contains(value);

		public void CopyTo(TreeNode[] array, int index) => _nodes.CopyTo(array, index);
	}
}
