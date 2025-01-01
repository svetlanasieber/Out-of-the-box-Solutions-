using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxFlow
{
	public class Program
	{
		public static void Main(string[] args)
		{
			int[,] graph = ReadInput();

			int source = int.Parse(Console.ReadLine());

			int destination = int.Parse(Console.ReadLine());

			int[] parents = InitializeParents(graph.GetLength(0));

			int maxFlow = GetMaxFlow(graph, parents, source, destination);

            Console.WriteLine($"Max flow = {maxFlow}");
        }

		private static int GetMaxFlow(int[,] graph, int[] parents, int source, int destination)
		{
			int maxFlow = 0;

			bool hasAugmentingPath = Bfs(source, destination, graph, parents);

			while (hasAugmentingPath)
			{
				int currentFlow = GetCurrentFlow(graph, parents, destination);

				ApplyFlow(graph, parents, destination, currentFlow);

				maxFlow += currentFlow;

				hasAugmentingPath = Bfs(source, destination, graph, parents);
			}

			return maxFlow;
		}

		private static void ApplyFlow(int[,] graph, int[] parents, int node, int currentFlow)
		{
			while (parents[node] != -1)
			{
				int previousNode = parents[node];

				graph[previousNode, node] -= currentFlow;

				node = previousNode;
			}
		}

		private static int GetCurrentFlow(int[,] graph, int[] parents, int node)
		{
			int currentFlow = int.MaxValue;

			while (parents[node] != -1)
			{
				int previousNode = parents[node];

				int possibleFlow = graph[previousNode, node];

				if (currentFlow > possibleFlow)
				{
					currentFlow = possibleFlow;
				}

				node = previousNode;
			}

			return currentFlow;
		}

		private static bool Bfs(int source, int destination, int[,] graph, int[] parents)
		{
			bool[] visited = new bool[graph.GetLength(0)];

			Queue<int> queue = new Queue<int>();

			queue.Enqueue(source);

			visited[source] = true;

			while (queue.Count > 0)
			{
				int currentNode = queue.Dequeue();

				for (int child = 0; child < graph.GetLength(1); child++)
				{
					if (!visited[child] && graph[currentNode, child] > 0)
					{
						queue.Enqueue(child);

						visited[child] = true;

						parents[child] = currentNode;
					}
				}
			}

			return visited[destination];
		}

		private static int[] InitializeParents(int size)
		{
			int[] arr = new int[size];

			Array.Fill(arr, -1);

			return arr;
		}

		private static int[,] ReadInput()
		{
			int nodesCount = int.Parse(Console.ReadLine());

			int[,] inputGraph = new int[nodesCount, nodesCount];

			for (int i = 0; i < nodesCount; i++)
			{
				int[] inputNumbers = Console.ReadLine()
					.Split(", ", StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse)
					.ToArray();

				for (int j = 0; j < inputNumbers.Length; j++)
				{
					inputGraph[i, j] = inputNumbers[j];
				}
			}

			return inputGraph;
		}
	}
}
