using Aoc.Core;

namespace Y2024Tests;

[TestClass]
public class Day01Tests
{
    private const int Day = 1;
    [TestMethod]
    public void Test_Day01_Part1_ExampleData()
    {
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Example);
        var result = Y2024.Day01.Part1(data,debug:true);
        Assert.AreEqual("11", result);
    } 
    
    [TestMethod]
    public void Test_Day01_Part1_RealData()
    {
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Real);
        var result = Y2024.Day01.Part1(data);
        Assert.AreEqual("1938424", result);
    }
    
    [TestMethod]
    public void Test_Day01_Part2_ExampleData()
    {
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Example);
        var result = Y2024.Day01.Part2(data,debug:true);
        Assert.AreEqual("31", result);
    } 
    
    [TestMethod]
    public void Test_Day01_Part2_RealData()
    {
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Real);
        var result = Y2024.Day01.Part2(data);
        Assert.AreEqual("22014209", result);
    } 
}