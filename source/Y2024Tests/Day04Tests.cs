using Aoc.Core;

namespace Y2024Tests;

[TestClass]
public class Day04Tests
{
    private const int Day = 4;
    
    [TestMethod]
    public void Test_Day04_Part1_ExampleData()
    {
        const string expectedResult = "18";
        
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Example);
        var actualResult = Y2024.Day04.Part1(data,debug:true);
        Assert.AreEqual(expectedResult, actualResult);
    }   
    [TestMethod]
    public void Test_Day04_Part1_RealData()
    {
        const string expectedResult = "2530";
        
        var data = DataFileReader.ReadFileAsLines(Day,DataFileType.Real);
        var actualResult = Y2024.Day04.Part1(data,debug:false);
        Assert.AreEqual(expectedResult, actualResult);
        
    }      
}