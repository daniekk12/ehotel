using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Menu.Service;

public interface IMenuProvider
{
    IEnumerable<MenuItem> MenuItems { get; }
}
