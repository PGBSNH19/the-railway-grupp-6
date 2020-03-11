﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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

        DateTime currentTime = DateTime.Now;

        static void Main(string[] args)
        {
            ORM.FetchData(ref passengers, ref trains, ref stations, ref timeTables);
            PrintHeader();

            var plan1 = new TrainPlan()
                .SetForTrain(trains[0])
                .StartTrain(":)")
                .FollowTimeTable(timeTables);

            var plan2 = new TrainPlan()
                .SetForTrain(trains[1])
                .StartTrain(":)")
                .FollowTimeTable(timeTables);
            //.StopTrain();
            // @Spy Pierre
            // Alot of groups are trying to move the train from StationA to StationB

            //Train train1 = new Train(1,"McTrain", 100, true).StartTrain("10:40");

            for (int i = 0; i < 2; i++)
                AddToControllerLog("hej" + i);

            timeTables.ForEach(tt => Console.WriteLine("{0}\t{1}\t{2}\t{3}", tt.TrainID, tt.StationID, tt.DepartureTime, tt.ArrivalTime));

            // Control Tower
            // Carlos Lynos
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

    public class TrainPlan
    {

        public TrainPlan()
        {
        }

        public Train SetForTrain(Train train)
        {
            return train;
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
        private string _startTime;

        public int ID { get => _id; }
        public string Name { get => _name; }
        public int Speed { get => _speed; }
        public bool Operated { get => _operated; }
        public string StartTime { get => _startTime; set; }

        Thread TrainThread;
        List<TimeTable> TimeTables;

        //static Timer trainProcess = new Timer(ProcessTrain, null, 0, 50);

        public Train(int id, string name, int speed, bool operated)
        {
            _id = id;
            _name = name;
            _speed = speed;
            _operated = operated;
            TrainThread = new Thread(ProcessTrain);
            TimeTables = new List<TimeTable>();
        }

        public Train FollowTimeTable(List<TimeTable> timeTables)
        {
            // @pierre-nygard
            // Continue here
            TimeTables = timeTables.Where(t => t.TrainID == _id).ToList();
            TimeTables.ForEach(t => Console.WriteLine(t.ArrivalTime));
            return this;
        }

        public Train StartTrain(string startTime)
        {
            StartTime = startTime;
            Console.WriteLine("StartCycle enter");

            Thread.Sleep(1000);

            Console.WriteLine("StartCycle complete.");

            TrainThread.Start();

            return this;
        }

        //@to do fix this. 
        [Obsolete]
        public void ProcessTrainDeprecated()
        {
            // Count time
            double minutesPassed = 0.0;
            while (true)
            {
                Console.WriteLine(this._name + " travelling");
                Thread.Sleep(50);
                minutesPassed += 0.05;

                if ((minutesPassed % 1) == 0)
                    Console.WriteLine("Foo");
            }
            Console.WriteLine("HandleCyckel start");
        }

        public void ProcessTrain()
        {
            int timeElapsed = 0;
            while (true) {
                Thread.Sleep(50);
                timeElapsed += 50;
                if( (timeElapsed % 1000) == 0)
                {
                    TimeTables.FirstOrDefault(t => t.ArrivalTime)
                }
            }
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

        public TimeTable(int trainId, int stationId, string departureTime, string arrivalTime)
        {
            _stationId = stationId;
            _trainId = trainId;
            _departureTime = departureTime;
            _arrivalTime = arrivalTime;
        }
    }
}
