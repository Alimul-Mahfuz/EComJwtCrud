namespace EComJwtCrud.Application.CustomException
{
    public class ProductLinkBreakException : System.Exception
    {
        public int StatusCode { get; set; }
        public object? DataObject { get; set; }

        public ProductLinkBreakException(string message, int statusCode = 500, object? dataObject = null)
            : base(message)
        {
            StatusCode = statusCode;
            DataObject = dataObject;
        }
    }
}
