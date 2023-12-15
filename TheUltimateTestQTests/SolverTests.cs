using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheUltimateTestQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheUltimateTestQ.Tests
{
	[TestClass()]
	public class SolverTests
	{
		[TestMethod()]
		public void Solve_123_6_CorrectResult()
		{
			Solver solver = new Solver();
			var answers = solver.Solve("123", "6");

			foreach(var a in answers)
			{
				Assert.AreEqual("1+2+3", a);
			}
		}

		//123+9-8+7+65+4
		[TestMethod()]
		public void Solve_123987654_200_CorrectResult()
		{
			Solver solver = new Solver();
			var answers = solver.Solve("123987654", "200");

			foreach (var a in answers)
			{
				Assert.AreEqual("123+9-8+7+65+4", a);
			}
		}

		[TestMethod()]
		public void Solve_1234789_45_CorrectResult()
		{
			Solver solver = new Solver();
			var answers = solver.Solve("1234789", "45");

			Assert.AreEqual(3, answers.Count);
			Assert.AreEqual("123+4+7-89", answers[0]);
			Assert.AreEqual("12+3+47-8-9", answers[1]);
			Assert.AreEqual("1+2+34+7-8+9", answers[2]);
		}

		[TestMethod()]
		public void Solve_89765421_65_CorrectResult()
		{
			Solver solver = new Solver();
			var answers = solver.Solve("89765421", "65");

			Assert.AreEqual(4, answers.Count);
			Assert.AreEqual("8+97+6-5-42+1", answers[0]);
			Assert.AreEqual("8+97-65+4+21", answers[1]);
			Assert.AreEqual("8-9+76-5-4-2+1", answers[2]);
			Assert.AreEqual("8-9+7+6+54-2+1", answers[3]);
		}

		[TestMethod()]
		public void Solve_9999999999_65_NoResult()
		{
			Solver solver = new Solver();
			var answers = solver.Solve("9999999999", "65");

			Assert.AreEqual(0, answers.Count);
		}

		[TestMethod()]
		public void Solve_9999_100000000_NoResult()
		{
			Solver solver = new Solver();
			var answers = solver.Solve("9999", "100000000");

			Assert.AreEqual(0, answers.Count);
		}

		[TestMethod()]
		[DataRow("-123")]
		[DataRow("-1")]
		[DataRow("-9999999999")]
		public void Solve_NegativeNumber_NoResult(string NumberString)
		{
			Solver solver = new Solver();
			var answers = solver.Solve(NumberString, "123");

			Assert.AreEqual(0, answers.Count);
		}

		[TestMethod()]
		[DataRow("0")]
		[DataRow("100")]
		[DataRow("1032")]
		[DataRow("45099")]
		public void Solve_NumberWithZero_NoResult(string NumberString)
		{
			Solver solver = new Solver();
			var answers = solver.Solve(NumberString, "123");

			Assert.AreEqual(0, answers.Count);
		}

		[TestMethod()]
		[DataRow("ss0")]
		[DataRow("10da0")]
		[DataRow("10fas32")]
		[DataRow("4509jhk9")]
		public void Solve_NonNumericNumber_NoResult(string NumberString)
		{
			Solver solver = new Solver();
			var answers = solver.Solve(NumberString, "123");

			Assert.AreEqual(0, answers.Count);
		}

		[TestMethod()]
		[DataRow("3123.123")]
		[DataRow("74345.334")]
		[DataRow("6545.24")]
		[DataRow("634523.33")]
		[DataRow("324.33")]
		[DataRow("942.33")]
		public void Solve_NonIntegerNumber_NoResult(string NumberString)
		{
			Solver solver = new Solver();
			var answers = solver.Solve(NumberString, "123");

			Assert.AreEqual(0, answers.Count);
		}

		[TestMethod()]
		[DataRow("-123")]
		[DataRow("-1")]
		[DataRow("-9999999999")]
		public void Solve_NegativeTarget_NoResult(string TargetString)
		{
			Solver solver = new Solver();
			var answers = solver.Solve("1234", TargetString);

			Assert.AreEqual(0, answers.Count);
		}

		[TestMethod()]
		[DataRow("jhdsa")]
		[DataRow("78g")]
		[DataRow("FMCAKJ&*&*")]
		[DataRow("mn23kj4")]
		[DataRow("&^&JKKJ")]
		public void Solve_NonNumericTarget_NoResult(string TargetString)
		{
			Solver solver = new Solver();
			var answers = solver.Solve("1234", TargetString);

			Assert.AreEqual(0, answers.Count);
		}

		[TestMethod()]
		[DataRow("9.77")]
		[DataRow("53422.4")]
		[DataRow("12312.33")]
		[DataRow("12312.34534")]
		public void Solve_NonIntegerTarget_NoResult(string TargetString)
		{
			Solver solver = new Solver();
			var answers = solver.Solve("1234", TargetString);

			Assert.AreEqual(0, answers.Count);
		}

		[TestMethod()]
		[DataRow("0")]
		public void Solve_ZeroTarget_NoResult(string TargetString)
		{
			Solver solver = new Solver();
			var answers = solver.Solve("1234", TargetString);

			Assert.AreEqual(0, answers.Count);
		}

		[TestMethod()]
		public void Solve_9999999999_81_CorrectResult()
		{
			Solver solver = new Solver();
			var answers = solver.Solve("9999999999", "81");

			//assume 428 correct
			Assert.AreEqual(428, answers.Count);
		}
	}
}