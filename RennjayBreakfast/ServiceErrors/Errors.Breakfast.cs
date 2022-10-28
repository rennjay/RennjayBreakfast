using ErrorOr;
namespace RennjayBreakfast.ServiceErrors;

public static class Errors
{
    public static class BreakfastError
    {
        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast not found");
    }
}