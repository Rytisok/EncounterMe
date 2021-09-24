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

        public Geolocation(IJSRuntime js)
        {
            this.js = js;
        }
        public async ValueTask GetUserCoords()
        {
            //Runs the JS script that asks the browser for the location.
            await js.InvokeVoidAsync("Geolocate");
        }

       
    }
}
