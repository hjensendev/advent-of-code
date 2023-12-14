using Aoc.Core;
namespace Y2023.Tests;

[TestClass]
public class Day05
{
    [TestMethod]
    public void TestDay05PartOneExample()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsString(5, null, true);
        var exampleResult = Y2023.Day05.Part1(exampleData);
        Assert.AreEqual("35", exampleResult);
    }
    
    // [TestMethod]
    // public void TestDay54PartOneReal()
    // {
    //     // Real
    //     var realData = DataFileReader.ReadFileAsLines(5);
    //     var realResult = Y2023.Day05.Part1(realData);
    //     Assert.AreEqual("xxx", realResult);
    // }
    //
    // [TestMethod]
    // public void TestDay05PartTwoExample()
    // {
    //     // Example
    //     var exampleData = DataFileReader.ReadFileAsLines(5, null, true);
    //     var exampleResult = Y2023.Day05.Part2(exampleData);
    //     Assert.AreEqual("xx", exampleResult);
    // }
    //
    //  [TestMethod]
    //  public void TestDay05PartTwoReal()
    //  {
    //      // Example
    //      var realData = DataFileReader.ReadFileAsLines(5);
    //      var realResult = Y2023.Day05.Part2(realData);
    //      Assert.AreEqual("xx", realResult);
    // }
}