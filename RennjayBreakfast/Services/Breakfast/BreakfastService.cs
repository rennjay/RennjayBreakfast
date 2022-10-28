namespace RennjayBreakfast.Services.Breakfast
{
    public class BreakfastService : IBreakfastService
    {
        private static readonly Dictionary<Guid, Models.Breakfast> _breakfasts = new();
        public void CreateBreakfast(Models.Breakfast request)
        {
            _breakfasts.Add(request.Id, request);
        }

        public Models.Breakfast GetBreakfast(Guid id)
        {
            return _breakfasts[id];
        }
    }
}