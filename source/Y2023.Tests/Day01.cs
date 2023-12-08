using Aoc.Core;
using Y2023;
namespace Y2023.Tests;

[TestClass]
public class Day01
{
    [TestMethod]
    public void TestDay01PartOneExample()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsLines(1,1, true);
        var exampleResult = Y2023.Day01.Part1(exampleData);
        Assert.AreEqual("142", exampleResult);
    }    
    
    [TestMethod]
    public void TestDay01PartOneReal()
    {
        // Real
        var realData = DataFileReader.ReadFileAsLines(1);
        var realResult = Y2023.Day01.Part1(realData);
        Assert.AreEqual("56397", realResult);        
    }   
    
    [TestMethod]
    public void TestDay01PartTwoExample()
    {
        // Example
        var exampleData = DataFileReader.ReadFileAsLines(1,2 ,true);
        var exampleResult = Y2023.Day01.Part2(exampleData,true);
        Assert.AreEqual("281", exampleResult);
    }  
    
    [TestMethod]
    public void TestDay01PartTwoReal()
    {
        // Real
        var realData = DataFileReader.ReadFileAsLines(1);
        var realResult = Y2023.Day01.Part2(realData);
        Assert.AreEqual("55701", realResult);        
    }    
}