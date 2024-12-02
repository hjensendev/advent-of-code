using Aoc.Core;

namespace Y2024Tests;

[TestClass]
public class Day02Tests
{
    private const int Day = 2;
    [TestMethod]
    public void Test_Day02_Part1_ExampleData()
    {
        const string expectedResult = "2";
        
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Example);
        var actualResult = Y2024.Day02.Part1(data,debug:true);
        Assert.AreEqual(expectedResult, actualResult);
    } 
    
    [TestMethod]
    public void Test_Day02_Part1_RealData()
    {
        const string expectedResult = "670";
        
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Real);
        var actualResult = Y2024.Day02.Part1(data);
        Assert.AreEqual(expectedResult, actualResult);
    }
    
    [TestMethod]
    public void Test_Day02_Part2_ExampleData()
    {
        const string expectedResult = "4";
        
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Example);
        var actualResult = Y2024.Day02.Part2(data,debug:true);
        Assert.AreEqual(expectedResult, actualResult);
    } 
    
    [TestMethod]
    public void Test_Day02_Part2_RealData()
    {
        const string expectedResult = "700";
        
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Real);
        var actualResult = Y2024.Day02.Part2(data);
        Assert.AreEqual(expectedResult, actualResult);
    } 
}