using BlazorStack.Shared;
using BlazorStack.Shared.Entities;
using BlazorStack.Shared.Models;

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
