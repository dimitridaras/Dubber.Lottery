using System;
namespace Dubber.Lottery.DrawGeneration;

/// <summary>
/// Performs the draw. This class contains state, so be careful with DI scope.
/// Todo. Add ILogger<DrawGenerator> dependency and add some logging!
/// </summary>
internal class DrawGenerator : IDrawGenerator
{
    private static readonly Random RandomGenerator = new Random();
    SortedSet<int> numbers = new SortedSet<int>();
    private int? min;
    private int? max;

    /// <summary>
    /// Generates the draw
    /// </summary>
    /// <returns></returns>
    public IEnumerable<LotteryNumber> DrawNumbers(int numberToGenerate, int min, int max)
    {
        // would add some logging here

        this.numbers.Clear();

        // store these for bonus ball draw
        this.min = min;
        this.max = max;

        // using a SortedSet guarantees unique values.
        // Not super-efficient, but it does the job
        while (this.numbers.Count < numberToGenerate)
        {
            // the Add method just returns false if a number already exists.
            this.numbers.Add(RandomGenerator.Next(min, max + 1));
        }

        // I could add a LotteryNumberComparer implementing IComparer, and then I could remove this mapping
        // step because the SortedSet would be of type LotteryNumber
        return MapNumbers(numbers);
    }

    public LotteryNumber DrawBonusBall()
    {
        if (!this.min.HasValue || !this.max.HasValue)
        {
            throw new InvalidOperationException("You must call DrawNumbers before calling DrawBonusBall");
        }

        int bonus;
        do
        {
            bonus = RandomGenerator.Next(this.min.Value, this.max.Value + 1);
        } while (this.numbers.Contains(bonus));

        return new LotteryNumber(bonus);
    }

    private static IEnumerable<LotteryNumber> MapNumbers(SortedSet<int> numbers)
    {
        var lotteryNumbers = new List<LotteryNumber>();
        foreach (int number in numbers)
        {
            lotteryNumbers.Add(new LotteryNumber(number));
        }

        return lotteryNumbers;
    }
}

