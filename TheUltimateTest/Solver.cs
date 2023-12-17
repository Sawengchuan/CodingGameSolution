using System.Numerics;

namespace TheUltimateTestQ
{
	// https://www.codingame.com/ide/puzzle/the-ultimate-test

	public class Solver
	{
		bool IsInputValid(string NumberString, string TargetString)
		{
			var IsValid = true;

			int target;
			if (!Int32.TryParse(TargetString, out target))
			{
				Console.WriteLine("integer only");
				IsValid = false;
			}
			else if (target > Math.Pow(10, 8))
			{
				Console.WriteLine("K maximum 10^8");
				IsValid = false;
			}
			else if (target < 1)
			{
				Console.WriteLine("K minimum 1");
				IsValid = false;
			}

			if (!IsValid)
				return IsValid;


			BigInteger number;

			if (NumberString.IndexOf("0") != -1)
			{
				Console.WriteLine("no zero");
				IsValid = false;
			}
			else if (!BigInteger.TryParse(NumberString, out number))
			{
				Console.WriteLine("integer only");
				IsValid = false;
			}
			else if (number > (BigInteger)Math.Pow(10, 10))
			{
				Console.WriteLine("N maximum 10^10");
				IsValid = false;
			}
			else if (number < 1)
			{
				Console.WriteLine("N minimum 1");
				IsValid = false;
			}

			return IsValid;
		}

		public List<string> Solve(string NumberString, string TargetString)
		{
			var answers = new List<string>();

			if (!IsInputValid(NumberString, TargetString))
				return answers;


			var maxLevel = NumberString.Length - 1;
			var lNumber = Int64.Parse(NumberString);
			var lTargetAnswer = Int64.Parse(TargetString);

			List<OrderLoL> Matches = new List<OrderLoL>();

			if (lNumber == lTargetAnswer)
			{
				var m = new OrderLoL();
				m.NumberString = NumberString;
				m.AnswerString = NumberString;
				m.OrderSig = NumberString;
				Matches.Add(m);
			}
			else
			{
				Dictionary<int, List<string>> allLevelOperatorPermutation = new Dictionary<int, List<string>>();

				for (int i = maxLevel; i > 0; i--)
				{
					allLevelOperatorPermutation[i] = GenerateOperatorPermutation(i);
				}

				var startingString = NumberString;

				var currentString = startingString;


				var startingNumber = Int64.Parse(currentString);
				var nextNumberPermutation = GetNextNumberPermutation(new Int64[] { startingNumber });

				var nextString = string.Join(" ", nextNumberPermutation);
				while (nextString != currentString)
				{
					currentString = nextString;

					var lstTemp = nextNumberPermutation.ToList();
					lstTemp.Sort();

					var iLast = lstTemp.Last();
					var iSumOfOther = lstTemp.SkipLast(1).Sum();

					if (iLast - iSumOfOther > lTargetAnswer || iLast + iSumOfOther < lTargetAnswer)
					{
						//skip checking
					}
					else
					{
						//do comparison
						int currentLevel = nextNumberPermutation.Length - 1;
						var allOperatorPermutation = allLevelOperatorPermutation[currentLevel];

						foreach (var p in allOperatorPermutation)
						{
							var operatorArray = p.ToArray();
							long result = 0;
							var sTemp = "";
							for (int i = 0; i < nextNumberPermutation.Length; i++)
							{
								if (i == 0)
								{
									result = nextNumberPermutation[i];
									sTemp += result;
								}
								else
								{
									var runningOperator = operatorArray[i - 1];
									if (runningOperator == '+')
									{
										sTemp += "+" + nextNumberPermutation[i];
										result += nextNumberPermutation[i];
									}
									else if (runningOperator == '-')
									{
										sTemp += "-" + nextNumberPermutation[i];
										result -= nextNumberPermutation[i];
									}
								}
							}

							if (result == lTargetAnswer)
							{
								var m = new OrderLoL();
								m.NumberString = currentString;
								m.AnswerString = sTemp;

								var OrderSig = "";
								for (int i = 0; i < nextNumberPermutation.Length; i++)
								{
									OrderSig += nextNumberPermutation[i].ToString().Length;

									if (i != operatorArray.Length)
									{
										var op = operatorArray[i] == '+' ? "2" : "1";
										OrderSig += op;
									}
								}

								m.OrderSig = OrderSig;
								Matches.Add(m);
							}
						}
					}

					nextNumberPermutation = GetNextNumberPermutation(nextNumberPermutation);

					nextString = string.Join(" ", nextNumberPermutation);
				}
			}

			if (Matches.Count > 0)
			{
				var orderedMatches = Matches.OrderBy(s => s.OrderSig).Reverse();

				foreach (var m in orderedMatches)
				{
					answers.Add(m.AnswerString);
				}
			}

			return answers;
		}

		private Int64[] GetNextNumberPermutation(Int64[] startingArray)
		{
			if (startingArray.Length == 1 && startingArray[0] > 9)
			{
				var temp = startingArray[0].ToString();
				var nonLastElement = temp.Substring(0, temp.Length - 1);
				var lastElement = temp.Last().ToString();

				return new Int64[] { Int64.Parse(nonLastElement), Int64.Parse(lastElement) };
			}
			else
			{
				long[] workingArray = new long[startingArray.Length];

				startingArray.CopyTo(workingArray, 0);

				for (int i = workingArray.Length - 1; i >= 0; i--)
				{
					var temp = workingArray[i].ToString();
					if (temp.Length > 1)
					{
						var nonLastElement = temp.Substring(0, temp.Length - 1);
						var lastElement = temp.Last().ToString();

						workingArray[i] = Int64.Parse(nonLastElement);

						var newArray = new long[i + 1 + 1];
						workingArray.Take(i + 1).ToArray().CopyTo(newArray, 0);

						if (i + 1 < workingArray.Length)
						{
							var remainingString = string.Join("", workingArray.TakeLast(workingArray.Length - i - 1));
							newArray[newArray.Length - 1] = Int64.Parse(lastElement + remainingString);

							return newArray;
						}
						else
						{
							newArray[newArray.Length - 1] = Int64.Parse(lastElement);

							return newArray;
						}
					}
				}
			}
			return startingArray;
		}

		List<string> GenerateOperatorPermutation(int level)
		{
			List<string> operatorPermutationStrings = new List<string>();

			var startingString = new string('+', level);

			operatorPermutationStrings.Add(startingString);

			var currentString = startingString;
			var next = GetNextOperatorPermutation(startingString);

			while (currentString != next)
			{
				operatorPermutationStrings.Add(next);
				currentString = next;
				next = GetNextOperatorPermutation(currentString);
			}

			return operatorPermutationStrings;
		}

		string GetNextOperatorPermutation(string currentOperatorString)
		{
			var workingArray = currentOperatorString.ToArray();
			var iLastIndex = currentOperatorString.LastIndexOf("+");

			if (iLastIndex != -1)
			{
				workingArray[iLastIndex] = '-';

				for (int i = iLastIndex + 1; i < workingArray.Length; i++)
				{
					workingArray[i] = '+';
				}
			}

			return new string(workingArray);
		}
	}
}
