using ErrorOr;

namespace RennjayBreakfast.Services.Breakfast
{
    public interface IBreakfastService
    {
        void CreateBreakfast(RennjayBreakfast.Models.Breakfast request);
        ErrorOr<Models.Breakfast> GetBreakfast(Guid id);
        void DeleteBreakfast(Guid id);
    }
}
