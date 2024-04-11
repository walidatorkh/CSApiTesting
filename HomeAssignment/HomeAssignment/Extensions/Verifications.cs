using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace HomeAssignment.HomeAssignment.Extensions
{
    internal class Verifications
    {
        public static void VerifyEquals(List<string> actual, List<string> expected)
        {
            try
            {
                if (!actual.SequenceEqual(expected))
                {
                    Console.WriteLine("Verification passed");
                }
                else
                {
                    Console.WriteLine("Verification failed: Lists are not equal");
                    // If you want to print the differences, you can iterate through each list and print the elements
                    Console.WriteLine("Actual list:");
                    foreach (string str in actual)
                    {
                        Console.WriteLine(str);
                    }
                    Console.WriteLine("Expected list:");
                    foreach (string str in expected)
                    {
                        Console.WriteLine(str);
                    }
                    throw new Exception("Lists are not equal");
                }
            }
            catch (Exception e)
            {
                Assert.Fail("Verification failed: " + e.Message);
            }
        }
    }
}
