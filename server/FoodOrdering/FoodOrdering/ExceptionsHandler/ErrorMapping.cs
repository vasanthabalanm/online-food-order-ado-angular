using System.Text.Json;

namespace FoodOrdering.ExceptionsHandler
{
    public class ErrorMapping
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ErrorMapping(int ID, string Message)
        {
            this.StatusCode = ID;
            this.Message = Message;
        }

        //public override string ToString()
        //{
        //    // Return a JSON representation of the object
        //    return JsonSerializer.Serialize(this);
        //}
    }
}
