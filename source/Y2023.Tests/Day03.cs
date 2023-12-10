using Aoc.Core;
namespace Y2023.Tests;

[TestClass]
public class Day03
{
    [TestMethod]
    public void TestDay03PartOneExample()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsArray(3, null, true);
        var exampleResult = Y2023.Day03.Part1(exampleData);
        Assert.AreEqual("4361", exampleResult);
    }
    
    [TestMethod]
    public void TestDay03PartOneReal()
    {
        // Real
        var realData = DataFileReader.ReadFileAsArray(3);
        var realResult = Y2023.Day03.Part1(realData);
        Assert.AreEqual("529618", realResult);
    }
    
    
    [TestMethod]
    public void TestDay03PartTwoExample()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsArray(3, null, true);
        var exampleResult = Y2023.Day03.Part2(exampleData);
        Assert.AreEqual("467835", exampleResult);
    }
    
    [TestMethod]
    public void TestDay03PartTwoReal()
    {
        // Example
        var realData = DataFileReader.ReadFileAsArray(3);
        var realResult = Y2023.Day03.Part2(realData);
        Assert.AreEqual("77509019", realResult);
    }
}