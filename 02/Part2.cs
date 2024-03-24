using System.Text.RegularExpressions;
//56776fhjvhjhfbkjkkjmukjjjjkkmlllkljbnkllmlhkljkgkjfuklgg

public class Cube
{
    public string color;

    public Regex regex;

    public int amountAvailable;

    public int minRequired = 0;
}

public class Part2
{
    private static List<Cube> cubes = new List<Cube>
    {
        new Cube { color = "red", regex = new Regex(@"(\d+)\s(?=red)"), amountAvailable = 12 },
        new Cube { color = "blue", regex = new Regex(@"(\d+)\s(?=blue)"), amountAvailable = 14 },
        new Cube { color = "green", regex = new Regex(@"(\d+)\s(?=green)"), amountAvailable = 13 },
    };

    public static void Run()
    {
        //using var sr = new StreamReader(File.OpenRead("Data\\input_short.txt"));
        using var sr = new StreamReader(File.OpenRead("Data\\input.txt"));
        var line = sr.ReadLine();
        var sum = 0;


        while (line is not null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var gameDataStr = line[(line.IndexOf(':') + 1)..];

            var draws = gameDataStr.Split(";").Select(x => x.Trim()).ToList();

            foreach (var cube in cubes)
            {
                cube.minRequired = 0;
            }

            foreach (var draw in draws)
            {
                IsValidDraw(draw);
            }

            var power = cubes.Aggregate(1, (x, y) => x * (y.minRequired == 0 ? 1 : y.minRequired));
            sum += power;

            line = sr.ReadLine();
        }

        Console.WriteLine($"Sum is {sum}");
    }

    static bool IsValidDraw(string drawData)
    {
        foreach (var cube in cubes)
        {
            if (!CheckCubes(drawData, cube.regex, cube.amountAvailable, ref cube.minRequired)) return false;
        }

        return true;
    }

    private static bool CheckCubes(string draw, Regex cubeRegex, int cubesAvailable, ref int highestAmount)
    {
        var cubeAmountStr = cubeRegex.Match(draw).Groups[1].Value;
        if (string.IsNullOrWhiteSpace(cubeAmountStr)) cubeAmountStr = "0";

        var cubesAmount = Convert.ToInt32(cubeAmountStr);
        if (cubesAmount > highestAmount) highestAmount = cubesAmount;

        //if (cubesAmount > cubesAvailable) return false;

        return true;
    }
}
