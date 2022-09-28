
namespace MyClothes.ViewModels.GarmentsModels
{
    using System.Collections.Generic;

    public class AllPicturesViewModel
    {
        public IEnumerable<GarmentViewModel> GarmentsModelList { get; set; } = new List<GarmentViewModel>();
        
    }
}
