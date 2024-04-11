namespace KingMeetup.Messaging.Common
{
    internal interface IResponse
    {
        bool Success { get; set; }
        string Message { get; set; }
    }
}
