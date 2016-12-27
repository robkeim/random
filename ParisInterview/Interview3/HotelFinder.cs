namespace Interview3
{
    using System;
    using System.Collections.Generic;

    public class HotelFinder
    {
        private Dictionary<BucketPoint, List<Hotel>> worldMap;
        // This represents the size of items to group together:
        // ex: if this was 10, items 0.0 through 9.999999 would all fall into the same bucket 0
        private const int bucketSize = 10;

        public HotelFinder()
        {
            worldMap = new Dictionary<BucketPoint, List<Hotel>>();

            foreach (Hotel hotel in this.hotels)
            {
                BucketPoint bucketLocation = new BucketPoint { X = ((int)Math.Round(hotel.Location.X)) / bucketSize, Y = ((int)Math.Round(hotel.Location.Y) / bucketSize) };

                if (!worldMap.ContainsKey(bucketLocation))
                {
                    worldMap[bucketLocation] = new List<Hotel>();
                }

                worldMap[bucketLocation].Add(hotel);

            }
        }

        // Given a specific location and radius return a list of all of the hotels within that area
        public List<Hotel> FindHotelsNearLocation(Point location, double searchRadius)
        {
            List<Hotel> results = new List<Hotel>();

            int leftEdge = ((int)Math.Floor(location.X - searchRadius)) / bucketSize;
            int rightEdge = ((int)Math.Floor(location.X + searchRadius)) / bucketSize;

            int topEdge = ((int)Math.Floor(location.Y - searchRadius)) / bucketSize;
            int bottomEdge = ((int)Math.Floor(location.Y + searchRadius)) / bucketSize;

            for (int i = leftEdge; i <= rightEdge; i++)
            {
                for (int j = topEdge; j <= bottomEdge; j++)
                {
                    BucketPoint currentBucket = new BucketPoint { X = i, Y = j };

                    if (worldMap.ContainsKey(currentBucket))
                    {
                        foreach (Hotel hotel in worldMap[currentBucket])
                        {
                            if (this.IsInRadius(location, hotel.Location, searchRadius))
                            {
                                results.Add(hotel);
                            }
                        }
                    }
                }
            }

            return results;
        }

        // Return a value indicating if two points are within a specified distance
        private bool IsInRadius(Point location1, Point location2, double distance)
        {
            return Math.Sqrt((location1.X - location2.X) * (location1.X - location2.X) + (location1.Y - location2.Y) * (location1.Y - location2.Y)) <= distance;
        }

        private List<Hotel> hotels = new List<Hotel>
                                         {
                                             new Hotel { Name = "A", Location = new Point { X = 5.0, Y = 5.0 }},
                                             new Hotel { Name = "B", Location = new Point { X = 2.0, Y = 3.0 }},
                                             new Hotel { Name = "C", Location = new Point { X = -5.0, Y = -5.0 }},
                                             new Hotel { Name = "D", Location = new Point { X = 15.0, Y = 15.0 }},
                                             new Hotel { Name = "E", Location = new Point { X = 7.2, Y = 5.1 }},
                                         };

        private struct BucketPoint
        {
            public int X;
            public int Y;
        }
    }

    public struct Hotel
    {
        public string Name;
        public Point Location;
    }

    public struct Point
    {
        public double X;
        public double Y;
    }
}
