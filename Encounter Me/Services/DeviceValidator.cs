using System;
using System.Threading.Tasks;
using BlazorCurrentDevice;
using Microsoft.AspNetCore.Components;

public class DeviceValidator
{
    [Inject] IBlazorCurrentDeviceService BlazorCurrentDeviceService { get; set; }
    public async Task ValidateDevice()
    {
        bool mobile = await BlazorCurrentDeviceService.Mobile();
        if (!mobile)
        {
            throw new DeviceNotSupportedException();
        }
    }
}
