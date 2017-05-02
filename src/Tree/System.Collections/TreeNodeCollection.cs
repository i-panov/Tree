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

		public int Count => _nodes.Count;

		public bool IsReadOnly { get; set; } = false;

		public TreeNodeCollection()
		{
		}

	    public TreeNodeCollection(bool isReadOnly) : this()
	    {
	        IsReadOnly = isReadOnly;
	    }

		public TreeNode this[int index]
		{
			get
			{
				return _nodes[index];
			}

			set
			{
				if (IsReadOnly)
					throw new InvalidOperationException("Collection is readonly!");

			    _nodes[index].Parent = null;
                value.SetParent(Parent);
			    value.Index = index;
                _nodes[index] = value;
            }
		}

		public void Insert(int index, TreeNode value)
		{
            value.Parent?.Children.Remove(value);
            value.SetParent(Parent);
            value.Index = index;
            _nodes.Insert(index, value);
		}

		public void Add(TreeNode value)
		{
		    value.Parent?.Children.Remove(value);
            value.SetParent(Parent);
            value.Index = _nodes.Count;
			_nodes.Add(value);
		}

		public bool Remove(TreeNode value)
		{
			var result = _nodes.Remove(value);

		    if (result)
		        value.SetParent(null);

		    return result;
		}

		public void RemoveAt(int index)
		{
		    _nodes[index].SetParent(null);
			_nodes.RemoveAt(index);
		}

		public void Clear()
		{
            _nodes.ForEach(x => x.SetParent(null));
			_nodes.Clear();
		}

		public IEnumerator<TreeNode> GetEnumerator() => _nodes.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

		public int IndexOf(TreeNode value) => _nodes.IndexOf(value);

		public bool Contains(TreeNode value) => _nodes.Contains(value);

		public void CopyTo(TreeNode[] array, int index) => _nodes.CopyTo(array, index);

	    public static TreeNodeCollection FromCollection(IEnumerable values, Func<object, int> getLevel, Func<object, object> itemSelector)
	    {
            var result = new TreeNodeCollection();

	        foreach (var value in values)
	        {
	            var level = getLevel(value);
	            var node = new TreeNode(itemSelector(value));
	            var nodesByLevel = GetNodesByLevel(level, result);

                nodesByLevel?.Add(node);
	        }

	        return result;
	    }

	    private static TreeNodeCollection GetNodesByLevel(int level, TreeNodeCollection nodes)
	    {
	        if (nodes.Level == level)
	            return nodes;

	        if (nodes.Count > 0)
	            return GetNodesByLevel(level, nodes[nodes.Count - 1].Children);

            var currentNode = new TreeNode();
            nodes.Add(currentNode);

	        return GetNodesByLevel(level, currentNode.Children);
	    }
	}
}
