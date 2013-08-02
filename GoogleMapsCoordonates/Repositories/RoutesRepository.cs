using System;
using System.Collections.Generic;
using System.Linq;
using GoogleMapsCoordonates.Models;

namespace GoogleMapsCoordonates.Repositories
{
    public class RoutesRepository
    {
        private RoutesDataContext _db;

        public RoutesRepository()
        {
            _db = new RoutesDataContext();
        }

        public List<long> GetIMEIs()
        {
            return _db.Coordinates.ToList().Distinct(new CoordinateCommparer())
                .Select(x => x.imei.GetValueOrDefault()).ToList();
        }

        public void Add(Coordinate route)
        {
            _db.Coordinates.InsertOnSubmit(route);

            _db.SubmitChanges();
        }

        public void Delete(int coordinateId)
        {
            var route = _db.Coordinates.FirstOrDefault(x => x.coordinateId == coordinateId);

            if (route != null) _db.Coordinates.DeleteOnSubmit(route);
            _db.SubmitChanges();
        }

        public IList<Coordinate> GetCoordinatesByIMEI(long imei)
        {
            return _db.Coordinates.Where(x => x.imei == imei).ToList().Select(x => new Coordinate
                {
                    latitude = ConvertCoord(x.latitude),
                    longitude = ConvertCoord(x.longitude)
                }).ToList();
        }

        private string ConvertCoord(string longitude)
        {
            char[] charArray = longitude.ToCharArray(1, longitude.Length - 2);
            return string.Format("{0}{1}.{2}", longitude[0], longitude[1], string.Join("", charArray));
        }
    }

    public class CoordinateCommparer : IEqualityComparer<Coordinate>
    {
        public bool Equals(Coordinate x, Coordinate y)
        {
            return x.imei == y.imei;
        }

        public int GetHashCode(Coordinate obj)
        {
            return obj.imei.GetHashCode();
        }
    }
}