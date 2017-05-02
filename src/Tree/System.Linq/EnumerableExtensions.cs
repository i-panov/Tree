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
        private static IEnumerable<TreeNode<T>> GetFlatten<T>(TreeNodeCollection<T> nodes)
        {
            foreach (var node in nodes)
            {
                yield return node;

                foreach (var subnode in GetFlatten(node.Children))
                    yield return subnode;
            }
        }

        public static IEnumerable<T> ToEnumerable<T>(this TreeNodeCollection<T> nodes, Func<TreeNode<T>, T> selector)
            => GetFlatten(nodes).Select(selector);

        private static TreeNodeCollection<T> GetNodesByLevel<T>(int level, TreeNodeCollection<T> nodes)
        {
            if (nodes.Level == level)
                return nodes;

            if (nodes.Count == 0)
                nodes.Add(new TreeNode<T>());

            var lastNode = nodes[nodes.Count - 1];

            return GetNodesByLevel(level, lastNode.Children);
        }

        public static TreeNodeCollection<T2> ToTree<T1, T2>(this IEnumerable<T1> items, Func<T1, int> getLevel, Func<T1, int, T2> selector)
        {
            var result = new TreeNodeCollection<T2>();

            foreach (var value in items)
            {
                var level = getLevel(value);
                var convertedValue = selector?.Invoke(value, level) ?? value;
                var node = new TreeNode<T2>(convertedValue);
                var nodesByLevel = GetNodesByLevel(level, result);

                nodesByLevel?.Add(node);
            }

            return result;
        }
    }
}
