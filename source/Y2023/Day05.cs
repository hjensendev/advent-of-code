using System.Data.Common;

namespace Y2023;

public static class Day05
{
    private static bool _debug;
    private static bool _trace = false;

    
    public static string Part1(string text, bool debug = false)
    {
        _debug = debug;

        var almanac = ParseText(text);


        if (debug)
        {
            foreach (var seed in almanac.Seeds)
            {
                Console.WriteLine($"Seed {seed} is mapped to soil {almanac.GetSoilMapping(seed)} fertilizer {almanac.GetFertilizerMapping(seed)} water {almanac.GetWaterMapping(seed)} light {almanac.GetLightMapping(seed)} temperature {almanac.GetTemperatureMapping(seed)} humidity {almanac.GetHumidityMapping(seed)} location {almanac.GetLocationMapping(seed)}");
            }
        }

        var result = almanac.Locations.Min(location => location);
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
        almanac.CalculateLocations();
        
        return almanac;
    }

    public class Almanac
    {
        public double[] Seeds;
        public double[] Locations;
        private IList<Map> _maps;

        public Almanac(double[] seeds)
        {
            Seeds = seeds;
            _maps = new List<Map>();
        }

        
        
        public void CalculateLocations()
        {
            var locations = new List<double>();                                          
            foreach (var seed in Seeds)                                                  
            {                                                                            
                var location = GetLocationMapping(seed);                         
                locations.Add(location);                                                 
            }                                                                            
            Locations = locations.ToArray();                                     
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
                map.SourceStart = mappingNumbers[1];
                map.SourceEnd = mappingNumbers[1] + range;
                map.DestinationStart = mappingNumbers[0];
                map.DestinationEnd = mappingNumbers[0] + range;
                _maps.Add(map);
            }
        }

        private static double GetMapValue(double value, List<Map> maps)
        {
            if (_trace) Console.WriteLine($"Finding map for value {value}");
            foreach (var map in maps)
            {
                if (value >= map.SourceStart && value <= map.SourceEnd)
                {
                    var delta = value - map.SourceStart;
                    var mapValue = map.DestinationStart + delta;
                    if (_trace) Console.WriteLine($"Found mapping for value {value} in map {map}. Mapped to {mapValue}");
                    return mapValue;
                }
            }
            if (_trace) Console.WriteLine($"No mapping found for value {value}");
            return value;
        }
    
        

        public double GetSoilMapping(double seed)
        {
            if (_trace) Console.WriteLine($"GetSoilMapping {seed}");
            var maps = _maps.Where(m => m.MapType == MapType.SeedToSoil).ToList();
            return GetMapValue(seed, maps);
        }
        
        
        
        public double GetFertilizerMapping(double seed)
        {
            if (_trace) Console.WriteLine($"GetFertilizerMapping {seed}");
            var soil = GetSoilMapping(seed);
            var maps = _maps.Where(m => m.MapType == MapType.SoilToFertilizer).ToList();
            return GetMapValue(soil, maps);
        }
        
        
        
        public double GetWaterMapping(double seed)
        {
            if (_trace) Console.WriteLine($"GetWaterMapping {seed}");
            var fertilizer = GetFertilizerMapping(seed);
            var maps = _maps.Where(m => m.MapType == MapType.FertilizerToWater).ToList();
            return GetMapValue(fertilizer, maps);
        }
        
        
        public double GetLightMapping(double seed)
        {
            if (_trace) Console.WriteLine($"GetWaterMapping {seed}");
            var water = GetWaterMapping(seed);
            var maps = _maps.Where(m => m.MapType == MapType.WaterToLight).ToList();
            return GetMapValue(water, maps);
        }
        
        

        public double GetTemperatureMapping(double seed)
        {
            if (_trace) Console.WriteLine($"GetTemperatureMapping {seed}");
            var light  = GetLightMapping(seed);
            var maps = _maps.Where(m => m.MapType == MapType.LightToTemperature).ToList();
            return GetMapValue(light , maps);
        }  
        
        
        
        public double GetHumidityMapping(double seed)
        {
            if (_trace) Console.WriteLine($"GetHumidityMapping {seed}");
            var temperature   = GetTemperatureMapping(seed);
            var maps = _maps.Where(m => m.MapType == MapType.TemperatureToHumidity).ToList();
            return GetMapValue(temperature  , maps);
        }  
        
        
        
        public double GetLocationMapping(double seed)
        {
            if (_trace) Console.WriteLine($"GetLocationMapping {seed}");
            var humidity   = GetHumidityMapping(seed);
            var maps = _maps.Where(m => m.MapType == MapType.HumidityToLocation).ToList();
            return GetMapValue(humidity  , maps);
        }  
    }



    public class Map
    {
        public readonly MapType MapType;
        public double SourceStart;
        public double SourceEnd;
        public double DestinationStart;
        public double DestinationEnd;

        public Map(MapType mapType)
        {
            MapType = mapType;
        }

        public override string ToString()
        {
            return $"{MapType}:{SourceStart}:{SourceEnd} -> {DestinationStart}:{DestinationEnd}";
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
