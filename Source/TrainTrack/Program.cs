using System;
using System.Threading;

namespace TrainTrack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    class Passenger
    {
        private int _id;
        private string _fullName;
        public int ID { get => _id; }
        public string FullName { get => _fullName; }

        public Passenger(int id, string fullName)
        {
            _id = id;
            _fullName = fullName;
        }
    }

    class Train
    {
        private int _id;
        private string _name;
        private int _maxSpeed;
        private bool _operated;

        public int ID { get => _id; }
        public string Name { get => _name; }
        public int MaxSpeed { get => _maxSpeed; }
        public bool Operated { get => _operated; }

        public Train(int id, string name, int maxSpeed, bool operated)
        {
            _id = id;
            _name = name;
            _maxSpeed = maxSpeed;
            _operated = operated;
        }
    }

    class Station
    {
        private int _id;
        private string _name;
        private bool _endStation;

        public int ID { get => _id; }
        public string Name { get => _name; }
        public bool EndStation { get => _endStation; }

        public Station(int id, string name, bool endStation)
        {
            _id = id;
            _name = name;
            _endStation = endStation;
        }
    }

    class Timetable
    {
        private int _id;
        private int _stationId;
        private int _trainId;
        private string _departureTime;
        private string _arrivalTime;

        public int ID { get => _id; }
        public int StationID { get => _stationId; }
        public int TrainID { get => _trainId; }
        public string DepartureTime { get => _departureTime; }
        public string ArrivalTime { get => _arrivalTime; }

        public Timetable(int id, int stationId, int trainId, string departureTime, string arrivalTime)
        {
            _id = id;
            _stationId = stationId;
            _trainId = trainId;
            _departureTime = departureTime;
            _arrivalTime = arrivalTime;
        }
    }
}
