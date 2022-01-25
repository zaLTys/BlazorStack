using BlazorStack.Shared;
using BlazorStack.Shared.Entities;
using BlazorStack.Shared.Models;

namespace BlazorStack.Client.Services
{
    public interface IUnitService
    {
        IList<Unit> Units { get; set; }
        IList<UserUnit> MyUnits { get; set; }

        Task AddUnit(int unitId);
        Task LoadUnitsAsync();

        Task LoadUserUnitsAsync();
        Task ReviveArmy();
    }
}
