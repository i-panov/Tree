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

                if (node.Children.Count > 0)
                {
                    foreach (var subnode in GetFlatten(node.Children))
                        yield return subnode;
                }
            }
        }

        public static IEnumerable ToEnumerable(this TreeNodeCollection nodes, Func<TreeNode, object> itemSelector)
        {
            return GetFlatten(nodes).Select(itemSelector);
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

        public static TreeNodeCollection ToTree(this IEnumerable values, Func<object, int> getLevel, Func<object, object> itemSelector)
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
    }
}
