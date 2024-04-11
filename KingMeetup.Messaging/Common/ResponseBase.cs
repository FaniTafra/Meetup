namespace KingMeetup.Messaging.Common
{
    public abstract class ResponseBase<T> : IResponse where T : IRequest
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Request { get; set; }
    }
}
