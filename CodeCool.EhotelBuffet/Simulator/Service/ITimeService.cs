namespace CodeCool.EhotelBuffet.Simulator.Service;

public interface ITimeService
{
    DateTime GetCurrentTime();
    DateTime SetCurrentTime(DateTime time);
    DateTime IncreaseCurrentTime(int minutes);

}
