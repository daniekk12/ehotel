namespace CodeCool.EhotelBuffet.Simulator.Model;

public record DiningSimulatorConfig()
{
    public int Cycles { get; }
    public DateTime End { get; }
    public int CycleLengthInMinutes { get; }
    public int MinimumGroupCount { get; }
    public DateTime Start { get; }

    public DiningSimulatorConfig(
        DateTime start,
        DateTime end,
        int cycleLengthInMinutes,
        int minimumGroupCount) : this()
    {
        Start = start;
        End = end;
        CycleLengthInMinutes = cycleLengthInMinutes;
        MinimumGroupCount = minimumGroupCount;

        var diff = End - Start;
        Cycles = diff.Hours *60 / cycleLengthInMinutes;
    }
}
