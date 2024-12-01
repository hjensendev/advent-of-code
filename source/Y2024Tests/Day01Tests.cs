using Aoc.Core;

namespace Y2024Tests;

[TestClass]
public class Day01Tests
{
    [TestMethod]
    public void TestDay01PartOneExample()
    {
        var data = DataFileReader.ReadFileAsLines(1,DataFileType.Example);
        var result = Y2024.Day01.Part1(data,debug:true);
        Assert.AreEqual("11", result);
    } 
    
    [TestMethod]
    public void TestDay01PartOneReal()
    {
        var data = DataFileReader.ReadFileAsLines(1,DataFileType.Real);
        var result = Y2024.Day01.Part1(data);
        Assert.AreEqual("1938424", result);
    }
    
    [TestMethod]
    public void TestDay01PartTwoExample()
    {
        var data = DataFileReader.ReadFileAsLines(1,DataFileType.Example);
        var result = Y2024.Day01.Part2(data,debug:true);
        Assert.AreEqual("31", result);
    } 
    
    [TestMethod]
    public void TestDay01PartTwoReal()
    {
        var data = DataFileReader.ReadFileAsLines(1,DataFileType.Real);
        var result = Y2024.Day01.Part2(data);
        Assert.AreEqual("22014209", result);
    } 
}