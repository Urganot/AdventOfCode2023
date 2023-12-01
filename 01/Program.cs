using System.Text.RegularExpressions;
using _01;

(string first, string last) GetDigits(Regex regex, string s)
{
    var matches = regex.Matches(s);
    var first1 = matches.First().Groups[1].Value;
    var last1 = matches.Last().Groups[1].Value;
    return (first1, last1);
}

// Part 1
var regexPart1 = new Regex("([0-9])");

// Part 2
var regexPart2 = new Regex("(?=([0-9]|one|two|three|four|five|six|seven|eight|nine))");

using var sr = new StreamReader(File.OpenRead("Data\\input.txt"));
var sum = 0;
var line = sr.ReadLine();

while (line is not null)
{
    var (first, last) = GetDigits(regexPart1, line);

    var code = $"{NumbersMap.Map[first]}{NumbersMap.Map[last]}";

    Console.WriteLine(code);

    sum += Convert.ToInt32(code); 
    line = sr.ReadLine();
}

Console.WriteLine($"Sum is {sum}");