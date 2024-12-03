using Aoc.Core;

namespace Y2024Tests;

[TestClass]
public class Day03Tests
{
    private const int Day = 3;
    
    [TestMethod]
    public void Test_Day03_Part1_ExampleData()
    {
        const string expectedResult = "161";
        
        var data = DataFileReader.ReadFileAsString(Day,DataFileType.Example);
        var actualResult = Y2024.Day03.Part1(data,debug:true);
        Assert.AreEqual(expectedResult, actualResult);
    }     
    
    [TestMethod]
    public void Test_Day03_Part1_RealData()
    {
        const string expectedResult = "179571322";
        
        var data = DataFileReader.ReadFileAsString(Day,DataFileType.Real);
        var actualResult = Y2024.Day03.Part1(data,debug:true);
        Assert.AreEqual(expectedResult, actualResult);
    }   
    
    [TestMethod]
    public void Test_Day03_Part2_ExampleData()
    {
        const string expectedResult = "48";
        
        var data = DataFileReader.ReadFileAsString(Day,DataFileType.Example2);
        var actualResult = Y2024.Day03.Part2(data,debug:true);
        Assert.AreEqual(expectedResult, actualResult);
    }   
    
    [TestMethod]
    public void Test_Day03_Part2_RealData()
    {
        const string expectedResult = "103811193";
        
        var data = DataFileReader.ReadFileAsString(Day,DataFileType.Real);
        var actualResult = Y2024.Day03.Part2(data,debug:true);
        Assert.AreEqual(expectedResult, actualResult);
    }       
    // 98308764 too low
    // 107506439 not correct
}