using CodeCool.EhotelBuffet.Simulator.Model;

namespace CodeCool.EhotelBuffet.Simulator.Service;

public interface IDiningSimulator
{
    DiningSimulationResults Run(DiningSimulatorConfig config);
}
