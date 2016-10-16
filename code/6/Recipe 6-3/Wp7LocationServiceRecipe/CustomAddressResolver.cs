using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;

namespace Wp7LocationServiceRecipe
{
    public class CustomAddressResolver : ICivicAddressResolver
    {
        GeocodeService.GeocodeServiceClient proxy = null;
        public CustomAddressResolver()
        {
            proxy = new GeocodeService.GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            proxy.ReverseGeocodeCompleted += new EventHandler<GeocodeService.ReverseGeocodeCompletedEventArgs>(proxy_ReverseGeocodeCompleted);
        }
        public CivicAddress ResolveAddress(GeoCoordinate coordinate)
        {
            throw new NotImplementedException();
        }

        public void ResolveAddressAsync(GeoCoordinate coordinate)
        {
            proxy.ReverseGeocodeAsync(
                new GeocodeService.ReverseGeocodeRequest()
                {
                    Location = new Microsoft.Phone.Controls.Maps.Platform.Location()
                    {
                        Latitude = coordinate.Latitude,
                        Longitude = coordinate.Longitude,
                        Altitude = coordinate.Altitude
                    },
                    Credentials = new Microsoft.Phone.Controls.Maps.Credentials()
                    {
                        ApplicationId = "Aqccy-CkUw0dTAyR7NsX3sLo8WuqUcnRUvNFaHvH6emC3m8AnZgpR9Oe8Zx9Sj-A"
                    }
                });
            
        }

        void proxy_ReverseGeocodeCompleted(object sender, GeocodeService.ReverseGeocodeCompletedEventArgs e)
        {
            ResolveAddressCompleted(
                sender,
                new ResolveAddressCompletedEventArgs(
                    new CivicAddress()
                    {
                        AddressLine1 = e.Result.Results[0].Address.AddressLine,
                        AddressLine2 = e.Result.Results[0].Address.FormattedAddress,
                        Building = "",
                        City = e.Result.Results[0].Address.Locality,
                        CountryRegion = e.Result.Results[0].Address.CountryRegion,
                        FloorLevel = "0",
                        PostalCode = e.Result.Results[0].Address.PostalCode,
                        StateProvince = e.Result.Results[0].Address.PostalTown
                    },
                    e.Error,
                    e.Cancelled,
                    e.UserState));
        }

        public event EventHandler<ResolveAddressCompletedEventArgs> ResolveAddressCompleted;
    }
}
