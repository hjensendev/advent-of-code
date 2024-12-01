using Aoc.Core;

namespace Y2024Tests;

[TestClass]
public class Day01Tests
{
    [TestMethod]
    public void TestDay01PartOneExample()
    {
        var exampleData = DataFileReader.ReadFileAsLines(1,null, true);
        var exampleResult = Y2024.Day01.Part1(exampleData,debug:true);
        Assert.AreEqual("11", exampleResult);
    } 
    
    [TestMethod]
    public void TestDay01PartOneReal()
    {
        var realData = DataFileReader.ReadFileAsLines(1);
        var realResult = Y2024.Day01.Part1(realData);
        Assert.AreEqual("1938424", realResult);
    }
    
    [TestMethod]
    public void TestDay01PartTwoExample()
    {
        var exampleData = DataFileReader.ReadFileAsLines(1,null, true);
        var exampleResult = Y2024.Day01.Part2(exampleData,debug:true);
        Assert.AreEqual("31", exampleResult);
    } 
    
    [TestMethod]
    public void TestDay01PartTwoReal()
    {
        var exampleData = DataFileReader.ReadFileAsLines(1,null);
        var realResult = Y2024.Day01.Part2(exampleData);
        Assert.AreEqual("22014209", realResult);
    } 
}