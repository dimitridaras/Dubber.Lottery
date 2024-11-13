namespace Dubber.Lottery.DrawGeneration;

// Shoulld not be mutable
public record class LotteryNumber(int Number)
{
    public Colour Colour
    {
        get
        {
            switch (Number)
            {
                case > 0 and < 10:
                    return Colour.Grey;
                case >= 10 and < 20:
                    return Colour.Blue;
                case >= 20 and < 30:
                    return Colour.Pink;
                case >= 30 and < 40:
                    return Colour.Green;
                case >= 40 and < 50:
                    return Colour.Yellow;
                default:
                    return Colour.White;
            }
        }
    }
}
