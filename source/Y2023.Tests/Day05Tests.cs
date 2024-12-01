using Aoc.Core;
namespace Y2023.Tests;

[TestClass]
public class Day05Tests
{
    [TestMethod]
    public void TestDay05PartOneExample()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsString(5, null, true);
        var exampleResult = Y2023.Day05.Part1(exampleData);
        Assert.AreEqual("35", exampleResult);
    }
    
    [TestMethod]
    public void TestDay05PartOneReal()
    {
        // Real
        var realData = DataFileReader.ReadFileAsString(5);
        var realResult = Y2023.Day05.Part1(realData);
        Assert.AreEqual("331445006", realResult);
    }
    
    [TestMethod]
    public void TestDay05PartTwoExample()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsString(5, null, true);
        var exampleResult = Y2023.Day05.Part2(exampleData);
        Assert.AreEqual("46", exampleResult);
    }
    
    
    
    // 1: 72511669 is too high
    // 2: 
    
    
    //  [TestMethod]
    //  public void TestDay05PartTwoReal()
    //  {
    //      // Example
    //      var realData = DataFileReader.ReadFileAsString(5);
    //      var realResult = Y2023.Day05.Part2(realData);
    //      Assert.IsTrue(72511669 > Convert.ToDouble(realResult));
    // }
}