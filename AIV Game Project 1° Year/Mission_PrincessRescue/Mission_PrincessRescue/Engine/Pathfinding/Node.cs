using System;
using System.Collections.Generic;
using System.Linq;

namespace Mission_PrincessRescue
{
    class Node
    {
        public string Label { get; }
        public int Row { get; }
        public int Col { get; }
        public Dictionary<Node, int> WeigthedEdges { get; }
        public List<Node> Edges { get { return WeigthedEdges.Keys.ToList<Node>(); } }

        public Node(string label, int row = -1, int col = -1)
        {
            Label = label;
            WeigthedEdges = new Dictionary<Node, int>();
            Row = row;
            Col = col;
        }

        public override string ToString()
        {
            return Label;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Node))
                return false;

            Node other = (Node)obj;

            return Label == other.Label;
        }


        public void Link(Node other, int cost = 1)
        {
            if (other.Equals(this))
                return;

            if (WeigthedEdges.ContainsKey(other))
                return;

            WeigthedEdges[other] = cost;
            other.WeigthedEdges[this] = cost;
        }

        public void LinkTo(Node other, int cost = 1)
        {
            if (other.Equals(this))
                return;

            if (WeigthedEdges.ContainsKey(other))
                return;

            WeigthedEdges[other] = cost;
        }

        public void PrintLinkTo()
        {
            String line = Label + ": \n";

            foreach (KeyValuePair<Node, int> Pair in WeigthedEdges)
                line += ("\t" + "{" + Pair.Key.Label + "} = " + Pair.Value + "\n");

            Console.Write(line);
        }

    }
}