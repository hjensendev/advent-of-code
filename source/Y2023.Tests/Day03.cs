using Aoc.Core;
using Y2023;
namespace Y2023.Tests;

[TestClass]
public class Day03
{
    [TestMethod]
    public void TestDay02PartOneExample()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsArray(3, null, true);
        var exampleResult = Y2023.Day03.Part1(exampleData);
        Assert.AreEqual("4361", exampleResult);
    }
    
    [TestMethod]
    public void TestDay02PartOneReal()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsArray(3);
        var exampleResult = Y2023.Day03.Part1(exampleData);
        Assert.AreEqual("529618", exampleResult);
    }
}