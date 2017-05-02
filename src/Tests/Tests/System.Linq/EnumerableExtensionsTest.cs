using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Tests.Data;

namespace Tests.System.Linq
{
    [TestFixture]
    public class EnumerableExtensionsTest
    {
        private readonly DataProvider data = new DataProvider();

        [Test]
        public void ToEnumerableTest()
        {
            var tree = new TreeNodeCollection()
            {
                new TreeNode
                {
                    Value = "1",
                    Children =
                    {
                        new TreeNode("1.1"),
                        new TreeNode("1.2")
                    }
                }
            };

            var values = tree.ToEnumerable(x => x.Value).Cast<object>().ToArray();

            Assert.AreEqual(3, values.Length);
            Assert.AreEqual("1", values[0]);
            Assert.AreEqual("1.1", values[1]);
            Assert.AreEqual("1.2", values[2]);
        }

        [Test]
        public void ToTreeTest()
        {
            var lines = data.Input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Func<object, int> getLevel = line => ((string)line).TakeWhile(char.IsWhiteSpace).Count();
            Func<object, int, object> itemSelector = (line, level) => new string(((string)line).SkipWhile(char.IsWhiteSpace).ToArray());
            var tree = lines.ToTree(getLevel, itemSelector);

            Assert.AreEqual(4, tree.Count);
            Assert.AreEqual(3, tree[0].Children.Count);
            Assert.AreEqual(2, tree[0].Children[0].Children.Count);
            Assert.AreEqual("1.1.1", tree[0].Children[0].Children[0].Value);
            Assert.AreEqual("3.1", tree[2].Children[0].Value);

            Func<TreeNode, string> nodeToLine = x => new string('\t', x.Level) + x.Value;
            var outputLines = tree.ToEnumerable(nodeToLine).Cast<string>().ToArray();
            data.Output = string.Join(Environment.NewLine, outputLines);

            Assert.IsTrue(lines.SequenceEqual(outputLines));
            Assert.AreEqual(data.Input, data.Output);
        }
    }
}
