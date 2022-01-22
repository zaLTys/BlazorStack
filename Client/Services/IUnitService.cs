using BlazorStack.Shared;

namespace BlazorStack.Client.Services
{
    public interface IUnitService
    {
        IList<Unit> Units { get;}
        IList<UserUnit> MyUnits { get; set; }

        void AddUnit(int unitId);
    }
}
