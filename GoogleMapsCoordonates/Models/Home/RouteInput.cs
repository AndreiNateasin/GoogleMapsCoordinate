using System.ComponentModel.DataAnnotations;

namespace GoogleMapsCoordonates.Models.Home
{
    public class RouteInput
    {
        [Display(Name = "Longitude")]
        public string FromLong { get; set; }

        [Display(Name = "Latitude")]
        public string FromLat { get; set; }

        [Display(Name = "Longitude")]
        public string ToLong { get; set; }

        [Display(Name = "Latitude")]
        public string ToLat { get; set; }
    }
}