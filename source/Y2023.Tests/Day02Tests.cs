using Aoc.Core;
using Y2023;
namespace Y2023.Tests;

[TestClass]
public class Day02Tests
{
    [TestMethod]
    public void TestDay02PartOneExample()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsLines(2,null, true);
        var exampleResult = Y2023.Day02.Part1(exampleData);
        Assert.AreEqual("8", exampleResult);
    }
    
    [TestMethod]
    public void TestDay02PartOneReal()
    {
        // Real
        var realData = DataFileReader.ReadFileAsLines(2);
        var realResult = Y2023.Day02.Part1(realData);
        Assert.AreEqual("2512", realResult);        
    }    
    
    
    [TestMethod]
    public void TestDay02PartTwoExample()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsLines(2,null, true);
        var exampleResult = Y2023.Day02.Part2(exampleData);
        Assert.AreEqual("2286", exampleResult);
    }
    
    [TestMethod]
    public void TestDay02PartTwoReal()
    {
        // Real
        var realData = DataFileReader.ReadFileAsLines(2);
        var realResult = Y2023.Day02.Part2(realData);
        Assert.AreEqual("67335", realResult);     
    }
}