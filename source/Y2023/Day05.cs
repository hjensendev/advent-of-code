using System.Diagnostics;


namespace Y2023;

public static class Day05
{
    private static bool _debug;
    private static bool _trace = false;


    public static string Part1(string text, bool debug = false)
    {
        _debug = debug;
        var almanac = ParseText(text, false);
        Console.WriteLine($"Seed {almanac.Result.Seed} is mapped to soil {almanac.GetSoilMapping(almanac.Result.Seed)} fertilizer {almanac.GetFertilizerMapping(almanac.Result.Seed)} water {almanac.GetWaterMapping(almanac.Result.Seed)} light {almanac.GetLightMapping(almanac.Result.Seed)} temperature {almanac.GetTemperatureMapping(almanac.Result.Seed)} humidity {almanac.GetHumidityMapping(almanac.Result.Seed)} location {almanac.GetLocationMapping(almanac.Result.Seed)}");
        Console.WriteLine($"Result: {almanac.Result.Location}");
        return almanac.Result.Location.ToString();
    }



    public static string Part2(string text, bool debug = false)
    {
        _debug = debug;
        var almanac = ParseText(text, true);
        Console.WriteLine($"Seed {almanac.Result.Seed} is mapped to soil {almanac.GetSoilMapping(almanac.Result.Seed)} fertilizer {almanac.GetFertilizerMapping(almanac.Result.Seed)} water {almanac.GetWaterMapping(almanac.Result.Seed)} light {almanac.GetLightMapping(almanac.Result.Seed)} temperature {almanac.GetTemperatureMapping(almanac.Result.Seed)} humidity {almanac.GetHumidityMapping(almanac.Result.Seed)} location {almanac.GetLocationMapping(almanac.Result.Seed)}");
        Console.WriteLine($"Result: {almanac.Result.Location}");
        return almanac.Result.Location.ToString();
    }

    public static Almanac ParseText(string text, bool asRange = false)
    {
        if (_debug) Console.WriteLine("Parse Text from input file");

        const string sSeedsPrefix = "seeds: ";
        const string sSeedToSoilPrefix = "seed-to-soil map:\r\n";
        const string sSoilToFertilizerPrefix = "soil-to-fertilizer map:\r\n";
        const string sFertilizerToWaterPrefix = "fertilizer-to-water map:\r\n";
        const string sWaterToLightPrefix = "water-to-light map:\r\n";
        const string sLightToTemperaturePrefix = "light-to-temperature map:\r\n";
        const string sTemperatureToHumidityPrefix = "temperature-to-humidity map:\r\n";
        const string sHumidityToLocationPrefix = "humidity-to-location map:\r\n";

        var textArray = text.Split("\r\n\r\n");

        IEnumerable<double> seedNumbers = Array.Empty<double>();
        IList<SeedRange> seedRanges = new List<SeedRange>();

        var seeds = textArray.First(item => item.StartsWith(sSeedsPrefix)).Replace(sSeedsPrefix, "");
        var seedArray = seeds.Split(" ");
  
        
        if (asRange)
        {
            for (var i = 0; i <= seedArray.Length - 1; i = i + 2)
            {
                var seedStart = Convert.ToDouble(seedArray[i]);
                var seedRangeLength = Convert.ToDouble(seedArray[i + 1]);
                seedRanges.Add(new SeedRange(seedStart, seedStart + seedRangeLength));
            }
            if (_debug) Console.WriteLine($"There are {seedRanges.Count} seed ranges");
        }
        else
        {
            seedNumbers = seedArray.Select(Convert.ToDouble).ToArray();
            if (_debug) Console.WriteLine($"There are {seedNumbers.Count()} seed numbers");
        }


        var seedToSoilMap = textArray
            .First(item => item.StartsWith(sSeedToSoilPrefix))
            .Replace(sSeedToSoilPrefix, "")
            .Split("\r\n");

        var soilToFertilizerMap = textArray
            .First(item => item.StartsWith(sSoilToFertilizerPrefix))
            .Replace(sSoilToFertilizerPrefix, "")
            .Split("\r\n");

        var fertilizerToWaterMap = textArray
            .First(item => item.StartsWith(sFertilizerToWaterPrefix))
            .Replace(sFertilizerToWaterPrefix, "")
            .Split("\r\n");

        var waterToLightMap = textArray
            .First(item => item.StartsWith(sWaterToLightPrefix))
            .Replace(sWaterToLightPrefix, "")
            .Split("\r\n");

        var lightToTemperatureMap = textArray
            .First(item => item.StartsWith(sLightToTemperaturePrefix))
            .Replace(sLightToTemperaturePrefix, "")
            .Split("\r\n");

        var temperatureToHumidityMap = textArray
            .First(item => item.StartsWith(sTemperatureToHumidityPrefix))
            .Replace(sTemperatureToHumidityPrefix, "")
            .Split("\r\n");

        var humidityToLocationMap = textArray
            .First(item => item.StartsWith(sHumidityToLocationPrefix))
            .Replace(sHumidityToLocationPrefix, "")
            .Split("\r\n");

        var almanac = new Almanac();
        almanac.AddMap(MapType.SeedToSoil, seedToSoilMap);
        almanac.AddMap(MapType.SoilToFertilizer, soilToFertilizerMap);
        almanac.AddMap(MapType.FertilizerToWater, fertilizerToWaterMap);
        almanac.AddMap(MapType.WaterToLight, waterToLightMap);
        almanac.AddMap(MapType.LightToTemperature, lightToTemperatureMap);
        almanac.AddMap(MapType.TemperatureToHumidity, temperatureToHumidityMap);
        almanac.AddMap(MapType.HumidityToLocation, humidityToLocationMap);

        if (asRange)
        {
            almanac.SeedRanges = seedRanges;
            almanac.ReverseCalculateLocations();
            return almanac;
        }
        almanac.Seeds = seedNumbers.ToArray();
        almanac.CalculateLocations();
        return almanac;
    }

    public class Almanac
    {
        public readonly Result Result;
        public double[] Seeds;
        public IEnumerable<SeedRange> SeedRanges;
        private readonly IList<Map> _maps;

        public Almanac()
        {
            _maps = new List<Map>();
            SeedRanges = new List<SeedRange>();
            Seeds = Array.Empty<double>();
            Result = new Result()
            {
                Location = double.MaxValue,
                Seed = double.MaxValue
            };
        }


        public void CalculateLocations()
        {
            if (_debug) Console.WriteLine("CalculateLocations");
            var locations = new List<double>();
            foreach (var seed in Seeds)
            {
                var location = GetLocationMapping(seed);
                if (location < Result.Location)
                {
                    Result.Location = location;
                    Result.Seed = seed;
                }
            }
            Console.WriteLine($"{Result.Seed}:{Result.Location}");
        }



        public void ReverseCalculateLocations()
        {
            if (_debug) Console.WriteLine("ReverseCalculateLocations");
            double lastValidSourceStart = 1;
            //var soilMaps = _maps.Where(m => m.MapType == MapType.SeedToSoil);
            // var fertilizerMaps = _maps.Where(m => m.MapType == MapType.SoilToFertilizer);
            // var waterMaps = _maps.Where(m => m.MapType == MapType.FertilizerToWater);
            // var lightMaps = _maps.Where(m => m.MapType == MapType.WaterToLight);
            // var temperatureMaps = _maps.Where(m => m.MapType == MapType.LightToTemperature);
            // var humidityMaps = _maps.Where(m => m.MapType == MapType.TemperatureToHumidity);
            
            // Find which map contains lowest location
            var locationMaps = _maps.Where(m => m.MapType == MapType.HumidityToLocation);
            var minLocationMap = locationMaps.MinBy(map => map.DestinationStart);
            var nextSourceStart = minLocationMap.MapType == MapType.Transparent
                ? lastValidSourceStart
                : minLocationMap.SourceStart;
            lastValidSourceStart = nextSourceStart;

            // Find which maps that would lead to the lowest location
            var minHumidityMap = GetMapByValue(MapType.HumidityToLocation, nextSourceStart);
            nextSourceStart = minHumidityMap.MapType == MapType.Transparent
                ? lastValidSourceStart
                : minHumidityMap.SourceStart;
            lastValidSourceStart = nextSourceStart;

            var minTemperatureMap = GetMapByValue(MapType.TemperatureToHumidity, nextSourceStart);
            nextSourceStart = minTemperatureMap.MapType == MapType.Transparent
                ? lastValidSourceStart
                : minTemperatureMap.SourceStart;
            lastValidSourceStart = nextSourceStart;

            var minLightMap = GetMapByValue(MapType.LightToTemperature, nextSourceStart);
            nextSourceStart = minLightMap.MapType == MapType.Transparent
                ? lastValidSourceStart
                : minLightMap.SourceStart;
            lastValidSourceStart = nextSourceStart;

            var minWaterMap = GetMapByValue(MapType.WaterToLight, nextSourceStart);
            nextSourceStart = minWaterMap.MapType == MapType.Transparent
                ? lastValidSourceStart
                : minWaterMap.SourceStart;
            lastValidSourceStart = nextSourceStart;

            var minFertilizerMap = GetMapByValue(MapType.FertilizerToWater, nextSourceStart);
            nextSourceStart = minFertilizerMap.MapType == MapType.Transparent
                ? lastValidSourceStart
                : minFertilizerMap.SourceStart;
            lastValidSourceStart = nextSourceStart;

            var minSoilMap = GetMapByValue(MapType.SoilToFertilizer, nextSourceStart);
            nextSourceStart = minSoilMap.MapType == MapType.Transparent
                ? lastValidSourceStart
                : minSoilMap.SourceStart;

            var minSeed = SeedRanges.Min(seeds => seeds.Start);
            var minSeedMap = GetMapByValue(MapType.SeedToSoil, nextSourceStart);
            if (minSeedMap.MapType == MapType.Transparent)
            {
                minSeedMap = GetMapByValue(MapType.SeedToSoil, minSeed);
            }

            Result.Seed = minSeed;
            Result.Location = GetLocationMapping(minSeed);


            Console.WriteLine($"{Result.Seed}:{Result.Location}");
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
                    if (_trace)
                        Console.WriteLine($"Found mapping for value {value} in map {map}. Mapped to {mapValue}");
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
            var light = GetLightMapping(seed);
            var maps = _maps.Where(m => m.MapType == MapType.LightToTemperature).ToList();
            return GetMapValue(light, maps);
        }



        public double GetHumidityMapping(double seed)
        {
            if (_trace) Console.WriteLine($"GetHumidityMapping {seed}");
            var temperature = GetTemperatureMapping(seed);
            var maps = _maps.Where(m => m.MapType == MapType.TemperatureToHumidity).ToList();
            return GetMapValue(temperature, maps);
        }



        public double GetLocationMapping(double seed)
        {
            if (_trace) Console.WriteLine($"GetLocationMapping {seed}");
            var humidity = GetHumidityMapping(seed);
            var maps = _maps.Where(m => m.MapType == MapType.HumidityToLocation).ToList();
            return GetMapValue(humidity, maps);
        }

        private Map GetMapByValue(MapType mapType, double value)
        {
            var maps = _maps.Where(m => m.MapType == mapType).ToList();
            foreach (var map in maps)
            {
                if (value >= map.SourceStart && value <= map.SourceEnd) return map;
            }

            return new Map(MapType.Transparent);
        }
    }

    public class SeedRange
    {
        public double Start;
        public double End;

        public SeedRange(double start, double end)
        {
            Start = start;
            End = end;
        }
    }

    public  class Result
    {
        public double Seed = 0;
        public double Location = 0;
    }

    private class Map
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
        HumidityToLocation = 6,
        Transparent = 7,
    }
}
