using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using Wintellect.PowerCollections;

namespace TravelOptimizer
{
	public class Edge
	{
		public int First { get; set; }

		public int Second { get; set; }

		public int Weight { get; set; }
	}

	public class Program
	{
		public static void Main(string[] args)
		{
			Dictionary<int, List<Edge>> graph = ReadInputEdges();

			int start = int.Parse(Console.ReadLine());

			int destination = int.Parse(Console.ReadLine());

			int stopsCount = int.Parse(Console.ReadLine());

			int biggestNode = graph.Keys.Max();

			double[] distances = InitializeDistances(biggestNode + 1);

			int[] parents = InitializeParents(biggestNode + 1);

			ApplyDijkstra(graph, distances, parents, start, destination, stopsCount + 1);

			if (parents[destination] != -1)
			{
				Stack<int> elements = GetElements(parents, destination);

                Console.WriteLine(string.Join(" ", elements));
            }
			else
			{
                Console.WriteLine("There is no such path.");
            }
		}

		private static Stack<int> GetElements(int[] parents, int node)
		{
			Stack<int> elements = new Stack<int>();

			while (node != -1)
			{
				elements.Push(node);

				node = parents[node];
			}

			return elements;
		}

		private static void ApplyDijkstra(
			Dictionary<int, List<Edge>> graph,
			double[] distances,
			int[] parents,
			int start,
			int destination,
			int maxStopsCount)
		{
			distances[start] = 0;

			OrderedBag<int> priorityQueue =
				new OrderedBag<int>(Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));

			priorityQueue.Add(start);

			int stopsTaken = 0;

			while (priorityQueue.Count > 0)
			{
				int minNode = priorityQueue.RemoveFirst();

				if (double.IsPositiveInfinity(distances[minNode]))
				{
					break;
				}

				if (minNode == destination)
				{
					break;
				}

				if (stopsTaken == maxStopsCount)
				{
					break;
				}

				foreach (var edge in graph[minNode])
				{
					int otherNode = minNode == edge.First ? edge.Second : edge.First;

					if (double.IsPositiveInfinity(distances[otherNode]))
					{
						priorityQueue.Add(otherNode);
					}

					double newDistance = distances[minNode] + edge.Weight;

					if (newDistance < distances[otherNode])
					{
						distances[otherNode] = newDistance;

						parents[otherNode] = minNode;

						priorityQueue =
							new OrderedBag<int>(
								priorityQueue,
								Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
					}
				}

				stopsTaken++;
			}
		}

		private static int[] InitializeParents(int size)
		{
			int[] arr = new int[size];

			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = -1;
			}

			return arr;
		}

		private static double[] InitializeDistances(int size)
		{
			double[] arr = new double[size];

			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = double.PositiveInfinity;
			}

			return arr;
		}

		private static Dictionary<int, List<Edge>> ReadInputEdges()
		{
			Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>();

			int edgesCount = int.Parse(Console.ReadLine());

			for (int i = 0; i < edgesCount; i++)
			{
				int[] edgeData = Console.ReadLine()
					.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse)
					.ToArray();

				int firstNode = edgeData[0];

				int secondNode = edgeData[1];

				Edge edge = new Edge()
				{
					First = firstNode,
					Second = secondNode,
					Weight = edgeData[2],
				};

				if (!graph.ContainsKey(firstNode))
				{
					graph.Add(firstNode, new List<Edge>());
				}

				if (!graph.ContainsKey(secondNode))
				{
					graph.Add(secondNode, new List<Edge>());
				}

				graph[firstNode].Add(edge);

				graph[secondNode].Add(edge);
			}

			return graph;
		}
	}
}
