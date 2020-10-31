using System;

public class DelayedNodifyMessage
{
    public Type observeType;
    public long code;
    public object msg;
    public DelayedNodifyMessage(Type observeType, long code, object msg)
    {
        this.observeType = observeType;
        this.code = code;
        this.msg = msg;
    }
}
