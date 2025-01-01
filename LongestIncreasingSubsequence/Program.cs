using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestIncreasingSubsequence
{
	public class Program
	{
		public static void Main(string[] args)
		{
			int[] numbers = Console.ReadLine()
				.Split(' ', StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();

			int[] lengths = new int[numbers.Length];

			int[] previousOnes = new int[numbers.Length];

			int indexOfLastAddedNumber = 0;

			FillArrays(numbers, lengths, previousOnes, ref indexOfLastAddedNumber);

			Stack<int> elements = GetElements(numbers, previousOnes, indexOfLastAddedNumber);

            Console.WriteLine(string.Join(' ', elements));
        }

		private static Stack<int> GetElements(int[] numbers, int[] previousOnes, int indexOfLastAddedNumber)
		{
			Stack<int> elements = new Stack<int>();

			while (indexOfLastAddedNumber != -1)
			{
				elements.Push(numbers[indexOfLastAddedNumber]);

				indexOfLastAddedNumber = previousOnes[indexOfLastAddedNumber];
			}

			return elements;
		}

		private static void FillArrays(int[] numbers, int[] lengths, int[] previousOnes, ref int indexOfLastAddedNumber)
		{
			int bestLength = 0;

			for (int i = 0; i < numbers.Length; i++)
			{
				int currentNumber = numbers[i];

				int currentLength = 1;

				int currentParent = -1;

				for (int j = 0; j < i; j++)
				{
					if (numbers[j] < currentNumber && lengths[j] + 1 > currentLength)
					{
						currentLength = lengths[j] + 1;

						currentParent = j;
					}
				}

				lengths[i] = currentLength;

				previousOnes[i] = currentParent;

				if (currentLength > bestLength)
				{
					bestLength = currentLength;

					indexOfLastAddedNumber = i;
				}
			}
		}
	}
}
