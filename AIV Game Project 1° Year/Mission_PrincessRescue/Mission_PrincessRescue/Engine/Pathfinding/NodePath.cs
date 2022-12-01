using System;
using System.Collections.Generic;

namespace Mission_PrincessRescue
{
    class NodePath
    {
        private List<Node> nodes;

        public NodePath()
        {
            nodes = new List<Node>();
        }

        public int Length { get { return nodes.Count; } }

        public void Add(Node aNode)
        {
            nodes.Add(aNode);
        }

        public void Add(Node aNode, int pos)
        {
            nodes.Insert(pos, aNode);
        }

        public Node At(int index)
        {
            if (index < 0)
                return null;

            if (index >= nodes.Count)
                return null;

            return nodes[index];
        }

        public void Print()
        {
            foreach (Node Each in nodes)
                Console.Write(Each.Label + " ");

            Console.WriteLine();
        }

    }
}