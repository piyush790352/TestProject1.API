namespace DemoProject1.API.Model.Domain
{
    public class Response<T>
    {
            public T Result { get; set; }
            public string StatusMessage { get; set; }
            public string Error { get; set; }
    }
}
