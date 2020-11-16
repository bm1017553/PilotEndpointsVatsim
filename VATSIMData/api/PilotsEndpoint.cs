using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace api
{
    public class PilotsEndpoint
    {
        public static async Task CallsignEndpoint(HttpContext context)
        {
            string responseText = null;
            string callsign = context.Request.RouteValues["callsign"] as string;
            switch((callsign ?? "").ToLower())
            {
                case "aal1":
                    responseText = "Callsign: AAL1";
                    break;
                default:
                    responseText = "Callsign: INVALID";
                    break;
            }

            if(callsign != null)
            {
                await context.Response.WriteAsync($"{responseText} is the callsign");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }


        /* NOTE: All of these require that you first obtain a pilot and then search in Positions */

        public static async Task AltitudeEndpoint(HttpContext context)
        {
            string responseText = null;
            string flightAltitude = context.Request.RouteValues["altitude"] as string;

            using(var db = new VatsimDbContext())
            {
                Console.WriteLine($"{flightAltitude}");

                var _altitude = from pilot in db.Pilots
                join position in db.Positions
                where pilot.callsign == position.callsign
                group by position.altitude;
                
                if(_altitude != null)
                {
                    Console.WriteLine($"{flightAltitude}");

                    var _altitude = await db.Pilots.Where(f => f.callsign == (flightAltitude ?? "").ToUpper()).ToListAsync();
                    responseText = $"It is likely that at least {_altitude.Count()} flights are at {flightAltitude}";                    
                    await context.Response.WriteAsync($"RESPONSE: {responseText}");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }

        }

        public static async Task GroundspeedEndpoint(HttpContext context)
        {
            string responseText = null;
            string flightGroundspeed = context.Request.RouteValues["groundspeed"] as string;

            using(var db = new VatsimDbContext())
            {
                Console.WriteLine($"{flightGroundspeed}");

                var _groundspeed = from pilot in db.Pilots
                join position in db.Positions
                where pilot.callsign == position.callsign
                group by position.groundspeed;
                
                if(_groundspeed != null)
                {
                    Console.WriteLine($"{flightGroundspeed}");

                    var _groundspeed = await db.Pilots.Where(f => f.callsign == (flightAltitude ?? "").ToUpper()).ToListAsync();
                    responseText = $"It is likely that at least {_groundspeed.Count()} flights are at {flightGroundspeed}";                    
                    await context.Response.WriteAsync($"RESPONSE: {responseText}");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
        }

        //Latitude and longitude appear on pilot db as well

        public static async Task LatitudeEndpoint(HttpContext context)        
        {
            string responseText = null;
            string flightLatitude = context.Request.RouteValues["latitude"] as string;

            using(var db = new VatsimDbContext())
            {
                if(flightDeparture != null)
                {
                    Console.WriteLine($"{flightLatitude}");

                    var _latitude = await db.Pilots.Where(f => f.Latitude == (flightLatitude ?? "").ToUpper()).ToListAsync();
                    responseText = $"It is likely that at least {_latitude.Count()} flights are at {flightLatitude}";                    
                    await context.Response.WriteAsync($"RESPONSE: {responseText}");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
        }

        public static async Task LongitudeEndpoint(HttpContext context)
        {
            string responseText = null;
            string flightLongitude = context.Request.RouteValues["longitude"] as string;

            using(var db = new VatsimDbContext())
            {
                if(flightDeparture != null)
                {
                    Console.WriteLine($"{flightLongitude}");

                    var _longitude = await db.Pilots.Where(f => f.Longitude == (flightLongitude ?? "").ToUpper()).ToListAsync();
                    responseText = $"It is likely that at least {_longitude.Count()} flights are at {flightLongitude}";                    
                    await context.Response.WriteAsync($"RESPONSE: {responseText}");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
        }
    }
}