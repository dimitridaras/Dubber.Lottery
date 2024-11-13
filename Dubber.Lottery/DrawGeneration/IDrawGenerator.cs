namespace Dubber.Lottery.DrawGeneration;

internal interface IDrawGenerator
{
    LotteryNumber DrawBonusBall();
    IEnumerable<LotteryNumber> DrawNumbers(int numberToGenerate, int min, int max);
}