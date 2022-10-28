namespace RennjayBreakfast.Services.Breakfast
{
    public interface IBreakfastService
    {
        void CreateBreakfast(RennjayBreakfast.Models.Breakfast request);
        Models.Breakfast GetBreakfast(Guid id);
    }
}
