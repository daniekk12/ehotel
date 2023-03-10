namespace CodeCool.EhotelBuffet.Simulator.Service;

public class TimeService : ITimeService
{
    private DateTime _currentTime;

    public DateTime GetCurrentTime()
    {
        return _currentTime;
    }

    public DateTime SetCurrentTime(DateTime time)
    {
        _currentTime = time;
        return _currentTime;
    }

    public DateTime IncreaseCurrentTime(int minutes)
    {
        _currentTime = _currentTime.AddMinutes(30);
        return _currentTime;
    }
}
