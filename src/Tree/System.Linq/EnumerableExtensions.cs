using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        private static IEnumerable<TreeNode> GetFlatten(TreeNodeCollection nodes)
        {
            foreach (var node in nodes)
            {
                yield return node;

                foreach (var subnode in GetFlatten(node.Children))
                    yield return subnode;
            }
        }

        public static IEnumerable ToEnumerable(this TreeNodeCollection nodes, Func<TreeNode, object> selector)
            => GetFlatten(nodes).Select(selector);

        /*public static IEnumerable<T> ToEnumerable<T>(this TreeNodeCollection<T> nodes, Func<TreeNode<T>, T> selector)
            => ToEnumerable(nodes.ToTree(), x => selector((TreeNode)x));*/

        private static TreeNodeCollection GetNodesByLevel(int level, TreeNodeCollection nodes)
        {
            if (nodes.Level == level)
                return nodes;

            if (nodes.Count == 0)
                nodes.Add(new TreeNode());

            var lastNode = nodes[nodes.Count - 1];

            return GetNodesByLevel(level, lastNode.Children);
        }

        public static TreeNodeCollection ToTree(this IEnumerable values, Func<object, int> getLevel, Func<object, int, object> selector)
        {
            var result = new TreeNodeCollection();

            foreach (var value in values)
            {
                var level = getLevel(value);
                var convertedValue = selector?.Invoke(value, level) ?? value;
                var node = new TreeNode(convertedValue);
                var nodesByLevel = GetNodesByLevel(level, result);

                nodesByLevel?.Add(node);
            }

            return result;
        }

        /*public static TreeNodeCollection<T> ToTree<T>(this TreeNodeCollection nodes)
        {
            return null;
        }

        public static TreeNodeCollection ToTree<T>(this TreeNodeCollection<T> nodes)
        {
            return null;
        }

        public static TreeNodeCollection<T> ToTree<T>(this IEnumerable<T> values, Func<T, int> getLevel, Func<T, int, object> selector)
            => ToTree((IEnumerable)values, x => getLevel((T)x), (x, l) => selector((T)x, l)).ToTree<T>();*/
    }
}
