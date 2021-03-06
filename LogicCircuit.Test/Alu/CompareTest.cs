﻿using FluentAssertions;
using LogicCircuit.Alu.Compare;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using bool3 = System.Tuple<bool, bool, bool>;

namespace LogicCircuit.Test.Alu
{
    [TestClass]
    public class CompareTest
    {
        [TestMethod]
        public void LessThanComparesCorrectly()
        {
            //Given
            var lessThan = new LessThan();
            var table = new List<bool3>
            {
                new bool3(false, false, false),
                new bool3(false, true, true),
                new bool3(true, false, false),
                new bool3(true, true, false),
            };

            foreach (var t in table)
            {
                //When
                lessThan.InputA.State = t.Item1;
                lessThan.InputB.State = t.Item2;

                //Then
                lessThan.Output.State.Should().Be(t.Item3);
            }
        }

        [TestMethod]
        public void GreaterThanComparesCorrectly()
        {
            //Given
            var greaterThan = new GreaterThan();
            var table = new List<bool3>
            {
                new bool3(false, false, false),
                new bool3(false, true, false),
                new bool3(true, false, true),
                new bool3(true, true, false),
            };

            foreach (var t in table)
            {
                //When
                greaterThan.InputA.State = t.Item1;
                greaterThan.InputB.State = t.Item2;

                //Then
                greaterThan.Output.State.Should().Be(t.Item3);
            }
        }

        [TestMethod]
        public void ComparerComparesCorrectly()
        {
            ComparerComparesCorrectly(false);
        }

        [TestMethod]
        public void ComparerDoesNotOutputAnythingWhenTurnedOff()
        {
            ComparerComparesCorrectly(true);
        }

        public void ComparerComparesCorrectly(bool isOff)
        {
            //Given
            var truthTable = new List<CompareInputOutput>
            {
                new CompareInputOutput(false, false, isEqualTo: !isOff),
                new CompareInputOutput(false, true, isLessThan: !isOff),
                new CompareInputOutput(true, false, isGreaterThan: !isOff),
                new CompareInputOutput(true, true, isEqualTo: !isOff),
            };
            var comparer = new Comparer();
            comparer.IsOff.State = isOff;

            foreach(var t in truthTable)
            {
                Trace.WriteLine(t);

                //When
                comparer.InputA.State = t.InputA;
                comparer.InputB.State = t.InputB;

                //Then
                comparer.IsLessThan.State.Should().Be(t.IsLessThan);
                comparer.IsEqualTo.State.Should().Be(t.IsEqualTo);
                comparer.IsGreaterThan.State.Should().Be(t.IsGreaterThan);              
            }
        }

        private class CompareInputOutput
        {
            public bool InputA { get; set; }
            public bool InputB { get; set; }
            public bool IsLessThan { get; set; }
            public bool IsGreaterThan { get; set; }
            public bool IsEqualTo { get; set; }

            public CompareInputOutput(bool inputA, bool inputB, bool isLessThan = false, bool isGreaterThan = false, bool isEqualTo = false)
            {
                InputA = inputA;
                InputB = inputB;
                IsLessThan = isLessThan;
                IsGreaterThan = isGreaterThan;
                IsEqualTo = isEqualTo;
            }

            public override string ToString()
            {
                return $"{nameof(InputA)}: {InputA}, {nameof(InputB)}: {InputB}, " +
                    $"{nameof(IsLessThan)}: {IsLessThan}, {nameof(IsEqualTo)}: {IsEqualTo}, {nameof(IsGreaterThan)}: {IsGreaterThan}"; 
            }
        }
    }
}
