using System;

public class DeviceNotSupportedException : Exception
{
    public DeviceNotSupportedException()
    {

    }
    public DeviceNotSupportedException(string message)
        : base(message)
    {
    }
}
