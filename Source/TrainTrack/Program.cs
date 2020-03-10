using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace TrainTrack
{
    class Program
    {
        static List<Passenger> passengers;
        static List<Train> trains;
        static List<Station> stations;
        static List<TimeTable> timeTables;

        static void Main(string[] args)
        {
            
            FetchData();
            Init.PrintHeader();

            Console.WriteLine("The Railway Group 6");
            Console.WriteLine("Passengers:");
            Console.WriteLine("{0}\t{1}", "id", "fullName");
            passengers.ForEach(p => {
                Console.WriteLine("{0}\t{1}", p.ID, p.FullName);
                Thread.Sleep(10);
            });

            Console.WriteLine("\nTrains:");
            Console.WriteLine("{0}\t{1}\t\t\t{2}\t{3}", "id", "name", "speed", "operated");
            trains.ForEach(t => {
                Console.WriteLine("{0}\t{1}\t\t{2}\t{3}", t.ID, t.Name, t.Speed, t.Operated);
                Thread.Sleep(80);
            });

            Console.WriteLine("\nStations:");
            Console.WriteLine("{0}\t{1}\t\t\t{2}", "id", "name", "endStation");
            stations.ForEach(s => {
                Console.WriteLine("{0}\t{1}\t\t{2}", s.ID, s.Name.Substring(0,(s.Name.Length > 15) ? 15 : s.Name.Length), s.EndStation);
                Thread.Sleep(80);
            });

            Console.WriteLine("\nTimeTables:");
            Console.WriteLine("{0}\t{1}\t\t{2}\t{3}", "trainID", "stationID", "departureTime", "arrivalTime");
            timeTables.ForEach(t => {
                Console.WriteLine("{0}\t{1}\t\t\t{2}\t\t{3}", t.TrainID, t.StationID, t.DepartureTime, t.ArrivalTime);
                Thread.Sleep(80);
            });

        }

        /// <summary>
        /// Reads all data from ./assets folder and populates static lists in Program
        /// </summary>
        static void FetchData()
        {
            passengers = Init.GetPassengers(Init.ReadFile(@"assets/passengers.txt"));
            trains = Init.GetTrains(Init.ReadFile(@"assets/trains.txt"));
            stations = Init.GetStations(Init.ReadFile(@"assets/stations.txt"));
            timeTables = Init.GetTimeTables(Init.ReadFile(@"assets/timetable.txt"));
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

    /// <summary>
    /// Interface for any object that can be positioned inside a train tile item
    /// </summary>
    public interface ITileItem
    {
        // Not yet implemented
        // Tile tile;
    }

    public class Train : ITileItem
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

        public void DoStuff()
        {
            for (int i = 0; i < 40; i++)
            {
                Console.WriteLine($"{this.Name} travelling {i}");
                Thread.Sleep(this.Speed);
            }
        }
    }

    public class Route
    {

    }

    public class Station : ITileItem
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

    public class Switch : ITileItem
    {

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
