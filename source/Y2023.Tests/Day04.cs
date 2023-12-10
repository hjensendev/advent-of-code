using Aoc.Core;
namespace Y2023.Tests;

[TestClass]
public class Day04
{
    [TestMethod]
    public void TestDay04PartOneExample()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsLines(4, null, true);
        var exampleResult = Y2023.Day04.Part1(exampleData);
        Assert.AreEqual("13", exampleResult);
    }
    
    [TestMethod]
    public void TestDay04PartOneReal()
    {
        // Real
        var realData = DataFileReader.ReadFileAsLines(4);
        var realResult = Y2023.Day04.Part1(realData);
        Assert.AreEqual("20829", realResult);
    }
    
    
    // [TestMethod]
    // public void TestDay04PartTwoExample()
    // {
    //     // Example
    //     var exampleData = DataFileReader.ReadFileAsArray(4, null, true);
    //     var exampleResult = Y2023.Day04.Part2(exampleData);
    //     Assert.AreEqual("x", exampleResult);
    // }
    //
    // [TestMethod]
    // public void TestDay04PartTwoReal()
    // {
    //     // Example
    //     var realData = DataFileReader.ReadFileAsArray(4);
    //     var realResult = Y2023.Day04.Part2(realData);
    //     Assert.AreEqual("x", realResult);
    //}
}