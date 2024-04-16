using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Tracking_Service.Models;

namespace Tracking_Service.Services
{
    public interface ITracking
    {
        void UpdateParcel (Guid DeliveryId);
        
    }
}
