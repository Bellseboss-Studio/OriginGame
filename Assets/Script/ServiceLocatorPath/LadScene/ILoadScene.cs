using System;

public interface ILoadScene
{
    void Open(Action action);
    void Close(Action action);
}