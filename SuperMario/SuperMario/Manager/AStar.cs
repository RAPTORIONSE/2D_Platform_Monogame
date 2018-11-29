using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario
{

    class AStar
    {
        public static Node ReconstructPath(GameObject[][] grid, int fromI, int fromJ, int toI, int toJ, int prevI, int prevJ)
        {
            Node end = aStar(grid, fromI, fromJ, toI, toJ, prevI, prevJ);
            if (end == null)
            {
                return null;
            }
            Stack<Node> path = new Stack<Node>();
            while (end.i != fromI || end.j != fromJ)
            {
                path.Push(end);
                end = end.parentNode;
            }
            path.Push(end);
            while (path.Count > 0)
            {
                Node node = path.Pop();//current node
                if (path.Count == 0)
                {
                    return null;
                }
                return node = path.Pop();//next move node
            }
            return null;
        }
        public class Node
        {
            public int i, j;
            public int f = 0, g = 0, h = 0;
            public Node parentNode;
        }
        public static Node aStar(GameObject[][] grid, int fromI, int fromJ, int toI, int toJ, int prevI, int prevJ)
        {
            Dictionary<string, Node> openSet = new Dictionary<string, Node>();
            Dictionary<string, Node> closedSet = new Dictionary<string, Node>();
            Node start = new Node { i = fromI, j = fromJ };
            string key = start.i.ToString() + start.j.ToString();
            openSet.Add(key, start);

            Func<KeyValuePair<string, Node>> lowestValue = () =>
            {
                KeyValuePair<string, Node> lowest = openSet.ElementAt(0);
                foreach (KeyValuePair<string, Node> item in openSet)
                {
                    if (item.Value.f < lowest.Value.f)
                    {
                        lowest = item;
                    }
                    else if (item.Value.f == lowest.Value.f && item.Value.h < lowest.Value.h)
                    {
                        lowest = item;
                    }
                }
                return lowest;
            };
            List<KeyValuePair<int, int>> neighboringNodes = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(-1,0),
                new KeyValuePair<int, int>(1,0),
                new KeyValuePair<int, int>(0,-1),
                new KeyValuePair<int, int>(0,1)
            };

            int maxI = grid.GetLength(0);
            if (maxI == 0)
            {
                return null;
            }
            int maxJ = grid.GetLength(1);

            while (true)
            {
                if (openSet.Count == 0)
                {
                    return null;
                }
                KeyValuePair<string, Node> currentNode = lowestValue();
                if (currentNode.Value.i == toI && currentNode.Value.j == toJ)
                {
                    return currentNode.Value;//closedSet;
                }

                openSet.Remove(currentNode.Key);
                closedSet.Add(currentNode.Key, currentNode.Value);

                foreach (KeyValuePair<int, int> item in neighboringNodes)
                {
                    int A = currentNode.Value.i + item.Key;//need rename
                    int B = currentNode.Value.j + item.Value;//need rename
                    string C = A.ToString() + B.ToString();//need rename

                    //Notice grid[A][B] is Wall is for detection in pacman and might need to change in later inplementation
                    if (A < 0 || B < 0 || A >= maxI || B >= maxJ || A == prevI && B == prevJ /*|| grid[A][B] is Wall */|| closedSet.ContainsKey(C))
                    {
                        continue;
                    }
                    if (openSet.ContainsKey(C))
                    {
                        Node current = openSet[C];
                        int from = Math.Abs(A - fromI) + Math.Abs(B - fromJ);
                        if (from < current.g)
                        {
                            current.g = from;
                            current.f = current.g + current.h;
                            current.parentNode = currentNode.Value;
                        }
                    }
                    else//make a new node
                    {
                        Node current = new Node { i = A, j = B };
                        current.g = Math.Abs(A - fromI) + Math.Abs(B - fromJ);
                        current.h = Math.Abs(A - toI) + Math.Abs(B - toJ);
                        current.f = current.g + current.h;
                        current.parentNode = currentNode.Value;
                        openSet.Add(C, current);
                    }
                }
            }
        }
    }
}