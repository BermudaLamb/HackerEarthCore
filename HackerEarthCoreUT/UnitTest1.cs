using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackerEarthCore;
using System.IO;
using System.Collections.Generic;

namespace HackerEarthCoreUT
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBoardTest1()
        {
            var test = "";
            var winner = Program.TestBoard(2, new[,] { { "A", "B" }, { "A", " " } }, false, ref test);
            Assert.IsTrue(winner);
            Assert.AreEqual("A", test);
        }

        [TestMethod]
        public void TestBoardTest2()
        {
            var test = "";
            var winner = Program.TestBoard(2, new[,] { { "A", "B" }, { " ", " " } }, false, ref test);
            Assert.IsTrue(!winner);
            Assert.AreEqual("", test);
        }

        [TestMethod]
        public void TestBoardTest3()
        {
            var test = "";
            var winner = Program.TestBoard(2, new[,] { { "A", "B" }, { " ", "A" } }, false, ref test);
            Assert.IsTrue(winner);
            Assert.AreEqual("A", test);
        }

        [TestMethod]
        public void TestBoardTest4()
        {
            var test = "";
            var winner = Program.TestBoard(3, new[,] { { "A", "B", "A" },
                { " ", "A", "B" },
                { "A", " ", " " }
            }, false, ref test);
            Assert.IsTrue(winner);
            Assert.AreEqual("A", test);
        }

        [TestMethod]
        public void WriteBoardTest1()
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            Program.WriteBoard(2, new[,] { { "A", "B" }, { "A", "B" } });
            var result = sw.ToString();
            Assert.AreNotEqual(@"A B 
A C 
", result);
            Assert.AreEqual(@"A B 
A B 
", result);
        }

        [TestMethod]
        public void WriteBoardTest2()
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            Program.WriteBoard(3, new[,] { { "A", "B", "A" }, { "A", "B", "B" }, { "B", "A", "A" } });
            var result = sw.ToString();
            Assert.AreEqual(@"A B A 
A B B 
B A A 
", result);
        }

        [TestMethod]
        public void WriteBoardTest3()
        {
            try
            {
                var sw = new StringWriter();
                Console.SetOut(sw);
                Program.WriteBoard(4, new[,] { { "A", "B", "A", "A" },
                { "A", "A", "B", "A" },
                { "B", "B", "A", "B" },
                { "B", "A", "B", "B" } });
                var result = sw.ToString();
                Assert.AreEqual(@"A B A A 
A A B A 
B B A B 
B A B B 
", result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void WriteBoardTest4()
        {
            try
            {
                var sw = new StringWriter();
                Console.SetOut(sw);
                Program.WriteBoard(4, new[,] { { "A", "B", "A", "A" },
                { "A", "A", "B", "A" },
                { "B", "B", "A", "C" },
                { "B", "A", "B", "C" } });
                var result = sw.ToString();
                Assert.AreEqual(@"A B A A 
A A B A 
B B A C 
B A B C 
", result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void CrossesNaughtsTest1()
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var input = new List<string>();
            input.AddRange(new[] { "1", "Alice", "2", "1 1", "2 1", "2 2", "1 2" });
            Program.CrossesNaughts(input);
            var result = sw.ToString();
            Assert.AreEqual(@"Alice wins on move 3
A   
B A 
", result);
        }

        [TestMethod]
        public void CrossesNaughtsTest2()
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var input = new List<string>();
            input.AddRange(new[] { "1", "Bob", "2", "1 1", "2 1", "2 2", "1 2" });
            Program.CrossesNaughts(input);
            var result = sw.ToString();
            Assert.AreNotEqual(@"Alice wins on move 3
A   
B A 
", result);
            Assert.AreEqual(@"Bob wins on move 3
B   
A B 
", result);
        }

        [TestMethod]
        public void CrossesNaughtsTest3()
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var input = new List<string>();
            input.AddRange(new[] { "1", "Bob", "3", "1 1", "1 2", "1 3", "2 1", "2 2", "2 3", "3 3", "3 2", "3 1" });
            Program.CrossesNaughts(input);
            var result = sw.ToString();
            Assert.AreEqual(@"Bob wins on move 7
B A B 
A B A 
    B 
", result);
        }

        [TestMethod]
        public void CrossesNaughtsTest4()
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var input = new List<string>();
            input.AddRange(new[] { "1", "Bob", "4",
                "1 1", "1 2", "1 3", "1 4",
                "4 1", "4 2", "4 3", "4 4",
                "2 1", "2 2", "2 3", "3 3",
                "3 2", "3 1", "2 4", "3 4" });
            Program.CrossesNaughts(input);
            var result = sw.ToString();
            Assert.AreEqual(@"Match drawn!
B A B A 
B A B B 
A B A A 
B A B A 
", result);
        }

        [TestMethod]
        public void FizzBuzzTest1()
        {
            var sw = new StringWriter();
            var rdr = new StringReader(@"2
3 15
");
            Console.SetOut(sw);
            Console.SetIn(rdr);
            Program.fizzbuzz();
            var result = sw.ToString();
            Assert.AreEqual(@"1
2
Fizz
1
2
Fizz
4
Buzz
Fizz
7
8
Fizz
Buzz
11
Fizz
13
14
FizzBuzz
", result);
        }

        [TestMethod]
        public void FizzBuzzTest2()
        {
            var sw = new StringWriter();
            var rdr = new StringReader(@"1
3
");
            Console.SetOut(sw);
            Console.SetIn(rdr);
            Program.fizzbuzz();
            var result = sw.ToString();
            Assert.AreEqual(@"1
2
Fizz
", result);
        }

        [TestMethod]
        public void FizzBuzzTest3()
        {
            var sw = new StringWriter();
            var rdr = new StringReader(@"1
15
");
            Console.SetOut(sw);
            Console.SetIn(rdr);
            Program.fizzbuzz();
            var result = sw.ToString();
            Assert.AreEqual(@"1
2
Fizz
4
Buzz
Fizz
7
8
Fizz
Buzz
11
Fizz
13
14
FizzBuzz
", result);
        }
    }
}
