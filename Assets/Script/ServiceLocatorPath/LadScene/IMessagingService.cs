using System;

public interface IMessagingService
{
    void ShowYesAndNo(string title, string message, Action actionYes, Action actionNo);
}