namespace MyClothes.Services
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ISeasonService
    {
        SelectList SeasonList();
    }
}
