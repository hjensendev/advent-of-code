namespace Y2023;

public static class Day05
{
    private static bool _debug;
    
    public static string Part1(string text, bool debug = false)
    {
        _debug = debug;

        var almanac = ParseText(text);

        if (debug)
        {
            foreach (var seed in almanac.Seeds)
            {
                Console.WriteLine($"Seed {seed} is mapped to soil {almanac.GetSoilMapping(seed)}");
            }
        }

        var result = 0;
        Console.WriteLine($"Result: {result}");
        return result.ToString();
    }
    
    
    
    public static string Part2(string text, bool debug = false)
    {
        _debug = debug;
        
        var result = 0;
        Console.WriteLine($"Result: {result}");
        return result.ToString();

    }

    public static Almanac ParseText(string text)
    {
        const string sSeedsPrefix = "seeds: ";
        const string sSeedToSoilPrefix = "seed-to-soil map:\r\n";
        const string sSoilToFertilizerPrefix = "soil-to-fertilizer map:\r\n";
        const string sFertilizerToWaterPrefix = "fertilizer-to-water map:\r\n";
        const string sWaterToLightPrefix = "water-to-light map:\r\n";
        const string sLightToTemperaturePrefix = "light-to-temperature map:\r\n";
        const string sTemperatureToHumidityPrefix = "temperature-to-humidity map:\r\n";
        const string sHumidityToLocationPrefix = "humidity-to-location map:\r\n";
        
        var textArray = text.Split("\r\n\r\n");

        var seeds = textArray.First(item => item.StartsWith(sSeedsPrefix)).Replace(sSeedsPrefix,"");
        var seedArray = seeds.Split(" ");
        var seedNumbers = seedArray.Select(Convert.ToDouble).ToArray();

        var seedToSoilMap = textArray
            .First(item => item.StartsWith(sSeedToSoilPrefix))
            .Replace(sSeedToSoilPrefix,"")
            .Split("\r\n");

        var soilToFertilizerMap = textArray
            .First(item => item.StartsWith(sSoilToFertilizerPrefix))
            .Replace(sSoilToFertilizerPrefix, "")
            .Split("\r\n");

        var fertilizerToWaterMap = textArray
            .First(item => item.StartsWith(sFertilizerToWaterPrefix))
            .Replace(sFertilizerToWaterPrefix,"")
            .Split("\r\n");

        var waterToLightMap = textArray
            .First(item => item.StartsWith(sWaterToLightPrefix))
            .Replace(sWaterToLightPrefix,"")
            .Split("\r\n");
        
        var lightToTemperatureMap = textArray
            .First(item => item.StartsWith(sLightToTemperaturePrefix))
            .Replace(sLightToTemperaturePrefix,"")
            .Split("\r\n");
        
        var temperatureToHumidityMap = textArray
            .First(item => item.StartsWith(sTemperatureToHumidityPrefix))
            .Replace(sTemperatureToHumidityPrefix,"")
            .Split("\r\n");

        var humidityToLocationMap = textArray
            .First(item => item.StartsWith(sHumidityToLocationPrefix))
            .Replace(sHumidityToLocationPrefix,"")
            .Split("\r\n");
        
        var almanac = new Almanac(seedNumbers);
        almanac.AddMap(MapType.SeedToSoil, seedToSoilMap);
        almanac.AddMap(MapType.SoilToFertilizer, soilToFertilizerMap);
        almanac.AddMap(MapType.FertilizerToWater, fertilizerToWaterMap);
        almanac.AddMap(MapType.WaterToLight, waterToLightMap);
        almanac.AddMap(MapType.LightToTemperature, lightToTemperatureMap);
        almanac.AddMap(MapType.TemperatureToHumidity, temperatureToHumidityMap);
        almanac.AddMap(MapType.HumidityToLocation, humidityToLocationMap);
        
        return almanac;
    }

    public class Almanac
    {
        public double[] Seeds;
        private IList<Map> _maps;

        public Almanac(double[] seeds)
        {
            Seeds = seeds;
            _maps = new List<Map>();
        }
        
        public void AddMap(MapType mapType, IEnumerable<string> mappings)
        {
            foreach (var mapping in mappings)
            {
                var mappingNumbers = mapping
                    .Split(" ")
                    .Select(Convert.ToDouble)
                    .ToArray();
                var map = new Map(mapType);
                var range = mappingNumbers[2];
                map.SourceStart = mappingNumbers[0];
                map.SourceEnd = mappingNumbers[0] + range;
                map.DestinationStart = mappingNumbers[1];
                map.DestinationEnd = mappingNumbers[1] + range;
                _maps.Add(map);
            }
        }

        private static double GetMapValue(double seed, Map map)
        {
            if (!(seed >= map.SourceStart) || !(seed <= map.SourceEnd)) return seed;
            return map.SourceStart - map.DestinationStart + seed;
        }

        public double GetSoilMapping(double seed)
        {
            var maps = _maps.Where(m => m.MapType == MapType.SeedToSoil).ToList();
            foreach (var mapping in maps.Select(map => GetMapValue(seed, map)).Where(mapping => Math.Abs(seed - mapping) != 0))
            {
                return mapping;
            }
            return seed;
        }
    }



    public class Map
    {
        public MapType MapType;
        public double SourceStart;
        public double SourceEnd;
        public double DestinationStart;
        public double DestinationEnd;

        public Map(MapType mapType)
        {
            MapType = mapType;
        }
    }


    
    public enum MapType
    {
        SeedToSoil = 0,
        SoilToFertilizer = 1,
        FertilizerToWater = 2,
        WaterToLight = 3,
        LightToTemperature = 4,
        TemperatureToHumidity = 5,
        HumidityToLocation = 6
    }
}
