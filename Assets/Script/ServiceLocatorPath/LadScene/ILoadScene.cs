using System;

public interface ILoadScene
{
    void Open(Action action);
    void Close(Action action);
    void Lock();
    void Unlock();
    void ShowMessageWithTwoButton(string title, string message, string titlebuttonone, Action actionButtonOne, string titlebuttontwo, Action actionButtonTwo, Action actionToCancel);
    void ShowMessageWithOneButton(string title, string message, string titleOneButton, Action actionButtonOne, Action actionToCancel);
}