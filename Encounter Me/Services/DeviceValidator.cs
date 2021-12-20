using System;
using System.Threading.Tasks;
using BlazorCurrentDevice;
using Microsoft.AspNetCore.Components;
using BlazorCurrentDevice;

public class DeviceValidator
{
    [Inject] IBlazorCurrentDeviceService BlazorCurrentDeviceService { get; set; }
    public async Task ValidateDevice()
    {
        string g = await BlazorCurrentDeviceService.Type();

        Console.WriteLine(g);
        if (g != "Desktop")
        {
            throw new DeviceNotSupportedException();
        }
    }
}
