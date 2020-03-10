using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace TrainTrack
{
    public enum SwitchDirection
    {
        Left,
        Right
    }

    class Program
    {
        static List<Passenger> passengers;
        static List<Train> trains;
        static List<Station> stations;
        static List<TimeTable> timeTables;

        static void Main(string[] args)
        {
            FetchData();
            PrintHeader();

            for (int i = 0; i < 2; i++)
                AddToControllerLog("hej" + i);

            timeTables.ForEach(tt => Console.WriteLine("{0}\t{1}\t{2}\t{3}", tt.TrainID, tt.StationID, tt.DepartureTime, tt.ArrivalTime));

            // Control Tower
            // Carlos Lynos
        }

        /// <summary>
        /// Reads all data from ./assets folder and populates static lists in Program
        /// </summary>
        static void FetchData()
        {
            passengers = ORM.GetPassengers(ORM.ReadFile(@"assets/passengers.txt"));
            trains = ORM.GetTrains(ORM.ReadFile(@"assets/trains.txt"));
            stations = ORM.GetStations(ORM.ReadFile(@"assets/stations.txt"));
            timeTables = ORM.GetTimeTables(ORM.ReadFile(@"assets/timetable.txt"));
        }

        public static void AddToControllerLog(string logEntry)
        {
            File.AppendAllText(@"assets/controllerlog.txt", logEntry + Environment.NewLine);
        }

        /// <summary>
        /// Not neccessary, but cool
        /// </summary>
        public static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n");
            Console.WriteLine(@"
   _ \         _)  |                             ___|                               /    
  |   |   _` |  |  | \ \  \   /  _` |  |   |    |       __|  _ \   |   |  __ \      _ \  
  __ <   (   |  |  |  \ \  \ /  (   |  |   |    |   |  |    (   |  |   |  |   |    (   | 
 _| \_\ \__,_| _| _|   \_/\_/  \__,_| \__, |   \____| _|   \___/  \__,_|  .__/    \___/  
                                      ____/                              _|              ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class Passenger
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


    public class Train 
    {
        private int _id;
        private string _name;
        private int _speed;
        private bool _operated;

        public int ID { get => _id; }
        public string Name { get => _name; }
        public int Speed { get => _speed; }
        public bool Operated { get => _operated; }

        public Train(int id, string name, int speed, bool operated)
        {
            _id = id;
            _name = name;
            _speed = speed;
            _operated = operated;
        }
    }

    public class Station
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

    public class Switch
    {
        private SwitchDirection _direction;
        SwitchDirection Direction { 
            get => _direction;
            set => _direction = (value == SwitchDirection.Left || value == SwitchDirection.Right) ? value : throw new ArgumentException();
        }

        public Switch(SwitchDirection direction)
        {
            Direction = direction;
        }
    }

    public class TimeTable
    {
        private int _stationId;
        private int _trainId;
        private string _departureTime;
        private string _arrivalTime;

        public int StationID { get => _stationId; }
        public int TrainID { get => _trainId; }
        public string DepartureTime { get => _departureTime; }
        public string ArrivalTime { get => _arrivalTime; }

        public TimeTable(int stationId, int trainId, string departureTime, string arrivalTime)
        {
            _stationId = stationId;
            _trainId = trainId;
            _departureTime = departureTime;
            _arrivalTime = arrivalTime;
        }
    }
}
