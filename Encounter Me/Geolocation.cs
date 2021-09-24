using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Encounter_Me
{
    /*
     * Geolocates the user using Geolocation.js
     * 
     */
    public class Geolocation
    {
        private readonly IJSRuntime js;
        float latitude;
        float longitude;

        public Geolocation(IJSRuntime js)
        {
            this.js = js;
        }
        public async ValueTask GetUserCoords()
        {
            //Runs the JS script that asks the browser for the location.
            await js.InvokeVoidAsync("Geolocate");
        }

        public async ValueTask UpdateCoords()
        {
            await js.InvokeVoidAsync("Geolocate");
            latitude = await js.InvokeAsync<float>("getLat");
            longitude = await js.InvokeAsync<float>("getLong");
        }

        public float getLatitude()
        {
            UpdateCoords();
            return this.latitude;
        }
        public float getLongitude()
        {
            UpdateCoords();
            return this.longitude;
        }



    }
}
