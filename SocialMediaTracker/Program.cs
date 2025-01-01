using System;
using System.Collections.Generic;

namespace SocialMediaTracker
{
	public class Edge
	{
		public string From { get; set; }

		public string To { get; set; }

		public int Weight { get; set; }
	}

	public class Program
	{
		private static Dictionary<string, List<Edge>> graph;

		public static void Main(string[] args)
		{
			graph = ReadInputEdges();

			Dictionary<string, double> distances = InitializeDistances();

			Stack<string> sortedNodes = TopologicalSort();

			string startNode = Console.ReadLine();

			string endNode = Console.ReadLine();

			Dictionary<string, int> hopsByNode = InitializeHopsByNode();

			ApplyLongestPath(sortedNodes, distances, hopsByNode, startNode);

			Console.WriteLine($"({distances[endNode]}, {hopsByNode[endNode]})");
		}

		private static void ApplyLongestPath(
			Stack<string> sortedNodes,
			Dictionary<string, double> distances,
			Dictionary<string, int> hopsByNode,
			string startNode)
		{
			distances[startNode] = 0;

			while (sortedNodes.Count > 0)
			{
				string node = sortedNodes.Pop();

				foreach (var edge in graph[node])
				{
					double newDistance = distances[edge.From] + edge.Weight;

					if (newDistance > distances[edge.To])
					{
						distances[edge.To] = newDistance;

						hopsByNode[edge.To] = hopsByNode[edge.From] + 1;
					}
				}
			}
		}

		private static Dictionary<string, int> InitializeHopsByNode()
		{
			Dictionary<string, int> hopsByNode = new Dictionary<string, int>();

			foreach (var key in graph.Keys)
			{
				hopsByNode.Add(key, 0);
			}

			return hopsByNode;
		}

		private static Stack<string> TopologicalSort()
		{
			Stack<string> sorted = new Stack<string>();

			HashSet<string> visited = new HashSet<string>();

			foreach (var node in graph.Keys)
			{
				Dfs(node, visited, sorted);
			}

			return sorted;
		}

		private static void Dfs(string node, HashSet<string> visited, Stack<string> sorted)
		{
			if (visited.Contains(node))
			{
				return;
			}

			visited.Add(node);

			foreach (var edge in graph[node])
			{
				Dfs(edge.To, visited, sorted);
			}

			sorted.Push(node);
		}

		private static Dictionary<string, double> InitializeDistances()
		{
			Dictionary<string, double> distanceByNode = new Dictionary<string, double>();

			foreach (var key in graph.Keys)
			{
				distanceByNode.Add(key, double.NegativeInfinity);
			}

			return distanceByNode;
		}

		private static Dictionary<string, List<Edge>> ReadInputEdges()
		{
			Dictionary<string, List<Edge>> inputGraph = new Dictionary<string, List<Edge>>();

			int edgesCount = int.Parse(Console.ReadLine());

			for (int i = 0; i < edgesCount; i++)
			{
				string[] edgeParts = Console.ReadLine()
					.Split(' ', StringSplitOptions.RemoveEmptyEntries);

				string fromNode = edgeParts[0];

				string toNode = edgeParts[1];

				if (!inputGraph.ContainsKey(fromNode))
				{
					inputGraph.Add(fromNode, new List<Edge>());
				}

				inputGraph[fromNode].Add(new Edge()
				{
					From = fromNode,
					To = toNode,
					Weight = int.Parse(edgeParts[2]),
				});

				if (!inputGraph.ContainsKey(toNode))
				{
					inputGraph.Add(toNode, new List<Edge>());
				}
			}

			return inputGraph;
		}
	}
}
