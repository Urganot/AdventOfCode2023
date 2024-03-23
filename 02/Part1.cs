using System.Text.RegularExpressions;

public class Part1
{
    public static void Run()
    {
        var redCubes = 12;
        var greenCubes = 13;
        var blueCubes = 14;

        var gameIdRegex = new Regex(@"Game (\d*):");
        var redCubesRegex = new Regex(@"(\d+)\s(?=red)");
        var blueCubesRegex = new Regex(@"(\d+)\s(?=blue)");
        var greenCubesRegex = new Regex(@"(\d+)\s(?=green)");

//// using var sr = new StreamReader(File.OpenRead("Data\\input_short.txt"));
        using var sr = new StreamReader(File.OpenRead("Data\\input.txt"));
        var line = sr.ReadLine();
        var sum = 0;


        while (line is not null)
        {
            var matches = gameIdRegex.Matches(line);
            var match = matches.First();
            var groups = match.Groups;
            var group = groups[1];
            var gameIdStr = group.Value;
            var gameId = Convert.ToInt32(gameIdStr);

            var gameDataStr = line[(line.IndexOf(':') + 1)..];

            var gameData = gameDataStr.Split(";").Select(x => x.Trim()).ToList();

            var isValidGame = true;
            foreach (var drawData in gameData)
            {
                isValidGame &= IsValidDraw(drawData);
            }

            if (isValidGame)
                sum += gameId;

            line = sr.ReadLine();
        }

        bool IsValidDraw(string drawData)
        {
            var redCubesStr = redCubesRegex.Match(drawData).Groups[1].Value;
            if (string.IsNullOrWhiteSpace(redCubesStr)) redCubesStr = "0";
            if (Convert.ToInt32(redCubesStr) > redCubes) return false;

            var blueCubesStr = blueCubesRegex.Match(drawData).Groups[1].Value;
            if (string.IsNullOrWhiteSpace(blueCubesStr)) blueCubesStr = "0";
            if (Convert.ToInt32(blueCubesStr) > blueCubes) return false;

            var greenCubesStr = greenCubesRegex.Match(drawData).Groups[1].Value;
            if (string.IsNullOrWhiteSpace(greenCubesStr)) greenCubesStr = "0";
            if (Convert.ToInt32(greenCubesStr) > greenCubes) return false;

            return true;
        }

        Console.WriteLine($"Sum is {sum}");
    }
}
