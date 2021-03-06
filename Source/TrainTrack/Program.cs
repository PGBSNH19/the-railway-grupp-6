﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TrainTrack
{
    class Program
    {
        const string GENESIS_TIME = "10:00";

        public static List<Passenger> passengers;
        public static List<Train> trains;
        public static List<Station> stations;
        public static List<TimeTable> timeTables;
        public static TrainSwitch switch1 = new TrainSwitch { Status = SwitchStatus.On };
        public static TrainSwitch switch2 = new TrainSwitch { Status = SwitchStatus.On };

        public static DateTime worldTime;

        private static Timer t;

        static void Main(string[] args)
        {
            Initiate();

            // Control Tower
            // Carlos Lynos
            var plan1 = new TrainPlan()
                .TurnOffSwitch(switch1, "10:15")
                .SetForTrain(trains[0])
                .FollowTimeTable(timeTables)
                .StartTrain();

            var plan2 = new TrainPlan()
                .TurnOffSwitch(switch2,"10:19")
                .SetForTrain(trains[1])
                .FollowTimeTable(timeTables)
                .StartTrain();

            StartTimer();
            Run();
            Thread.Sleep(1000);
            Environment.Exit(0);
        }

        private static void Run()
        {
            while(true)
            {
                // Exit condition => All trains has arrived
                if (trains.Where(t => t.Arrived == true).Count() == 2)
                    return;
            }
        }

        private static void Initiate()
        {
            Console.CursorVisible = false;
            ORM.FetchData(ref passengers, ref trains, ref stations, ref timeTables);
            PrintHeader();
            worldTime = DateTime.Parse(GENESIS_TIME);
        }

        private static void StartTimer()
        {
            t = new Timer(TimerCallback, worldTime, 0, 1000);
        }

        private static void TimerCallback(Object o)
        {
            worldTime = worldTime.AddMinutes(1);
            Console.WriteLine($"\t{worldTime.ToString("hh:mm")}");
            CheckSwitch();
        }

        private static void CheckSwitch()
        {
            if (Program.switch1.GetTime() == Program.worldTime && Program.switch1.Status == SwitchStatus.On)
            {
                Console.WriteLine("\tControl Tower: switch1 turn off");
                Program.switch1.Status = SwitchStatus.Off;
            }

            if (Program.switch2.GetTime() == Program.worldTime && Program.switch2.Status == SwitchStatus.On)
            {
                Console.WriteLine("\tControl Tower: switch2 turn off");
                Program.switch2.Status = SwitchStatus.Off;
            }
        }

        public static void AddToControllerLog(string logEntry)
        {
            File.AppendAllText(@"assets/controllerlog.txt", logEntry + Environment.NewLine);
        }

        /// <summary>
        /// Not neccessary, but cool
        /// </summary>
        private static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n");
            Console.WriteLine(@"
   _ \         _)  |                             ___|                               /    
  |   |   _` |  |  | \ \  \   /  _` |  |   |    |       __|  _ \   |   |  __ \      _ \  
  __ <   (   |  |  |  \ \  \ /  (   |  |   |    |   |  |    (   |  |   |  |   |    (   | 
 _| \_\ \__,_| _| _|   \_/\_/  \__,_| \__, |   \____| _|   \___/  \__,_|  .__/    \___/  
                                      ____/                              _|              ");
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class TrainSwitch
    {
        private DateTime _time;
        private SwitchStatus _status;
        public SwitchStatus Status
        {
            get => _status;
            set => _status = value;
        }

        public void SetTime(string time)
        {
            _time = DateTime.Parse(time);
        }

        public DateTime GetTime()
        {
            return _time;
        }

    }

    public enum SwitchStatus
    {
        On,
        Off
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

        public TrainPlan TurnOffSwitch(TrainSwitch trainSwitch, string time)
        {
            trainSwitch.SetTime(time);
            return this;
        }
    }

    [Obsolete]
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
        private bool _arrived;

        public int ID { get => _id; }
        public string Name { get => _name; }
        public int Speed { get => _speed; }
        public bool Operated { get => _operated; }
        public string StartTime { get => _startTime; set { _startTime = value; } }
        public bool Arrived { get => _arrived; }

        Thread TrainThread;
        List<TimeTable> TimeTables;

        public Train(int id, string name, int speed, bool operated)
        {
            _id = id;
            _name = name;
            _speed = speed;
            _operated = operated;
            TrainThread = new Thread(ProcessTrain);
            TimeTables = new List<TimeTable>();
            _arrived = false;
        }

        public Train FollowTimeTable(List<TimeTable> timeTables)
        {
            TimeTables = timeTables.Where(t => t.TrainID == _id).ToList();
            return this;
        }

        public async Task StartTrain()
        {

            StartTime = TimeTables.First().DepartureTime;
            TimeTables.Remove(TimeTables.First());

            var diff = DateTime.Parse(StartTime).Subtract(Program.worldTime).Duration();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t{this.Name} departing in {diff.Minutes} minutes");
            Console.ForegroundColor = ConsoleColor.White;

            await Task.Delay(diff.Minutes * 1000);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t{this.Name} departing -- Choo! Choo!");
            Console.ForegroundColor = ConsoleColor.White;

            TrainThread.Start();
        }

        public void ProcessTrain()
        {
            // Put thread on hiatus
            if (_arrived)
                return;

            DateTime arrivalTime = DateTime.Parse(TimeTables.First().ArrivalTime);

            while (true)
            {
                Thread.Sleep(1000);

                if (Program.worldTime.Minute == arrivalTime.Minute && Program.worldTime.Hour == arrivalTime.Hour)
                {
                    TimeTables.Remove(TimeTables.First());
                    TrainLogEntry();

                    Thread.Sleep(3000);

                    if (TimeTables.Count == 0)
                    {
                        _arrived = true;
                        return;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\t{this.Name} departing...");
                    Console.ForegroundColor = ConsoleColor.White;
                    arrivalTime = DateTime.Parse(TimeTables.First().ArrivalTime);
                }
            }
        }

        private void TrainLogEntry()
        {
            string logEntry;
            if (TimeTables.Count == 0)
            {
                logEntry = $"\t{this.Name} reached it's endstation";
            }
            else
            {
                logEntry = $"\t{this.Name} making a stop at "
                    + $"{Program.stations.Where(s => s.ID == TimeTables.First().StationID).First().Name}";
                if (2==(Program.stations.Where(s => s.ID == TimeTables.First().StationID).First().ID))
                {

                }
            }
            Program.AddToControllerLog(logEntry);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(logEntry);
            Console.ForegroundColor = ConsoleColor.White;
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

        //Spy @NorShiervani: Another group used distance and trainspeed to
        //calculate the time the train arrives.
        public Station(int id, string name, bool endStation)
        {
            _id = id;
            _name = name;
            _endStation = endStation;
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
