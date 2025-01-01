using System;
using System.Collections.Generic;
using System.Linq;

namespace TimberMaxDilemma
{
	public class Program
	{
		public static void Main(string[] args)
		{
			List<int> prices = new List<int> { 0 };

			prices.AddRange(Console.ReadLine()
				.Split(' ', StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse));

			int length = int.Parse(Console.ReadLine());

			int[] bestPricesByLength = new int[Math.Max(prices.Count, length + 1)];

			int[] lastAddedLenghts = new int[Math.Max(prices.Count, length + 1)];

			FindOptimalSolution(prices, length, bestPricesByLength, lastAddedLenghts);

			Console.WriteLine(bestPricesByLength[length]);
		}

		private static void FindOptimalSolution(
			List<int> prices, 
			int length, 
			int[] bestPricesByLength, 
			int[] lastAddedLenghts)
		{
			for (int i = 1; i <= length; i++)
			{
				bestPricesByLength[i] = int.MinValue;

				for (int j = 1; j <= i; j++)
				{
					if (j == prices.Count)
					{
						break;
					}

					int newCost = prices[j] + bestPricesByLength[i - j];

					if (newCost > bestPricesByLength[i])
					{
						bestPricesByLength[i] = newCost;

						lastAddedLenghts[i] = j;
					}
				}
			}
		}
	}
}
