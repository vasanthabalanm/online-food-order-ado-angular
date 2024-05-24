namespace FoodOrdering.ExceptionsHandler
{
    public class NotFoundExceptionHandler : Exception
    {
        string message;

        public NotFoundExceptionHandler(string message)
        {
            this.message = message;
        }

        public override string Message
        {
            get { return message; }
        }
    }
}
