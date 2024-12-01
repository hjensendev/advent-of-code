using Aoc.Core;

namespace Y2024Tests;

[TestClass]
public class Day01Tests
{
    private const int Day = 1;
    [TestMethod]
    public void Test_Day01_Part1_ExampleData()
    {
        const string expectedResult = "11";
        
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Example);
        var actualResult = Y2024.Day01.Part1(data,debug:true);
        Assert.AreEqual(expectedResult, actualResult);
    } 
    
    [TestMethod]
    public void Test_Day01_Part1_RealData()
    {
        const string expectedResult = "1938424";
        
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Real);
        var actualResult = Y2024.Day01.Part1(data);
        Assert.AreEqual(expectedResult, actualResult);
    }
    
    [TestMethod]
    public void Test_Day01_Part2_ExampleData()
    {
        const string expectedResult = "31";
        
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Example);
        var actualResult = Y2024.Day01.Part2(data,debug:true);
        Assert.AreEqual(expectedResult, actualResult);
    } 
    
    [TestMethod]
    public void Test_Day01_Part2_RealData()
    {
        const string expectedResult = "22014209";
        
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Real);
        var actualResult = Y2024.Day01.Part2(data);
        Assert.AreEqual(expectedResult, actualResult);
    } 
}