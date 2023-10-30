namespace VolgaIt.Service
{
    public class Location
    {
        public double Lat { get; }
        public double Lon { get; }

        public Location(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
        }

        public double DistanceTo(Location other)
        {
            const double R = 6371; // Радиус Земли в километрах
            double dLat = ToRadians(other.Lat - Lat);
            double dLon = ToRadians(other.Lon - Lon);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(Lat)) * Math.Cos(ToRadians(other.Lat)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;
            return distance;
        }

        public bool IsWithinRadius(Location other, double r)
        {
            double distance = DistanceTo(other);
            return distance <= r;
        }

        private static double ToRadians(double angle)
        {
            return angle * (Math.PI / 180);
        }
    }
}
