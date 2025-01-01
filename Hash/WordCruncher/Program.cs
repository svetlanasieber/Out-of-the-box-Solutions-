using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCruncher
{
		class Program
	{
		private static string targetWord;
		private static Dictionary<int, List<string>> collectionOfWordPartsByIndex;
		private static Dictionary<string, int> wordPartsCount;
		private static LinkedList<string> usedWordParts;

		static void Main()
		{
			string[] wordParts = Console.ReadLine()
				.Split(", ", StringSplitOptions.RemoveEmptyEntries);

			targetWord = Console.ReadLine();

			collectionOfWordPartsByIndex = new Dictionary<int, List<string>>();

			wordPartsCount = new Dictionary<string, int>();

			foreach (var wordPart in wordParts)
			{
				int index = targetWord.IndexOf(wordPart);

				if (index == -1)
				{
					continue;
				}

				if (wordPartsCount.ContainsKey(wordPart))
				{
					wordPartsCount[wordPart]++;

					continue;
				}

				wordPartsCount[wordPart] = 1;

				while (index != -1)
				{
					if (!collectionOfWordPartsByIndex.ContainsKey(index))
					{
						collectionOfWordPartsByIndex[index] = new List<string>();
					}

					collectionOfWordPartsByIndex[index].Add(wordPart);

					index = targetWord.IndexOf(wordPart, index + wordPart.Length);
				}
			}

			usedWordParts = new LinkedList<string>();

			GenerateSolutions(0);
		}

		private static void GenerateSolutions(int index)
		{
			if (index == targetWord.Length)
			{
                Console.WriteLine(string.Join(" ", usedWordParts));

				return;
            }

			if (!collectionOfWordPartsByIndex.ContainsKey(index))
			{
				return;
			}

			foreach (var wordPart in collectionOfWordPartsByIndex[index])
			{
				if (wordPartsCount[wordPart] == 0)
				{
					continue;
				}

				usedWordParts.AddLast(wordPart);

				wordPartsCount[wordPart]--;

				GenerateSolutions(index + wordPart.Length);

				usedWordParts.RemoveLast();

				wordPartsCount[wordPart]++;
			}
		}
	}
}
