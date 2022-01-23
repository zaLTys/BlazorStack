using BlazorStack.Shared;

namespace BlazorStack.Client.Services
{
    public interface IUnitService
    {
        IList<UnitModel> Units { get;}
        IList<UserUnitModel> MyUnits { get; set; }

        void AddUnit(int unitId);
    }
}
