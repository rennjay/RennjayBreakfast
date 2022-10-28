namespace RennjayBreakfast.Services.Breakfast
{
    public class BreakfastService : IBreakfastService
    {
        private static readonly Dictionary<Guid, Models.Breakfast> _breakfasts = new();
        public void CreateBreakfast(Models.Breakfast request)
        {
            _breakfasts.Add(request.Id, request);
        }

        public void DeleteBreakfast(Guid id)
        {
            _breakfasts.Remove(id);
        }

        public Models.Breakfast GetBreakfast(Guid id)
        {
            return _breakfasts[id];
        }
    }
}