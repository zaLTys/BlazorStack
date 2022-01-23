using BlazorStack.Shared;

namespace BlazorStack.Client.Services
{
    public interface IUnitService
    {
        IList<Unit> Units { get; set; }
        IList<UserUnitModel> MyUnits { get; set; }

        void AddUnit(int unitId);
        Task LoadUnitsAsync();
    }
}
