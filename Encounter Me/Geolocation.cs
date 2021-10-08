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

        public async ValueTask UpdateMap()
        {
            await js.InvokeVoidAsync("setInterval","UpdateMap",10);
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

        public void setLatitude(float latitude)
        {
            this.latitude = latitude;
        }

        public void setLongitude(float longitude)
        {
            this.longitude = longitude;
        }

        //I don't know if this works, need to get the API to work
        public async ValueTask<float[]> GetCoordsObj()
        {
            return await js.InvokeAsync<float[]>("getMapsObj");
        }

        //Trying as a simple literal instead of a google maps object
        public async ValueTask<float[]> GetCoordsObjJS()
        {
            return await js.InvokeAsync<float[]>("getMapsObjJS");
        }

    }
}
