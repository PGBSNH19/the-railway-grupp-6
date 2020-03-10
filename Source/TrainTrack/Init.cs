﻿using System;
using System.Collections.Generic;
using System.IO;

namespace TrainTrack
{
    public class Init
    {
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

        public static string[] ReadFile(string url)
        {
            var data = File.ReadAllLines(url);
            return data;
        }

        public static List<Passenger> GetPassengers(string[] data)
        {
            var passengers = new List<Passenger>();

            foreach (string passenger in data)
            {
                var passengerData = passenger.Split(';');
                try
                {
                    passengers.Add(new Passenger(
                        int.Parse(passengerData[0]),
                        passengerData[1]
                    ));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error parsing Passenger from file: {0}", e.Message);
                }
            }
            return passengers;
        }

        public static List<Train> GetTrains(string[] data)
        {
            var trains = new List<Train>();

            foreach (string train in data)
            {
                var trainData = train.Split(',');
                try
                {
                    trains.Add(new Train(
                        int.Parse(trainData[0]),
                        trainData[1],
                        int.Parse(trainData[2]),
                        bool.Parse(trainData[3])
                    ));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error parsing Train from file: {0}", e.Message);
                }
            }

            return trains;
        }

        public static List<Station> GetStations(string[] data)
        {
            var stations = new List<Station>();

            foreach (string station in data)
            {
                var stationData = station.Split('|');
                try
                {
                    stations.Add(new Station(
                        int.Parse(stationData[0]),
                        stationData[1],
                        bool.Parse(stationData[2])
                    ));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error parsing Station from file: {0}",  e.Message);
                }
            }
            return stations;
        }

        public static List<TimeTable> GetTimeTables(string[] data)
        {
            var timeTables = new List<TimeTable>();

            foreach (string timeTable in data)
            {
                var timeTableData = timeTable.Split(',');
                try
                {
                    timeTables.Add(new TimeTable(
                        int.Parse(timeTableData[0]),
                        int.Parse(timeTableData[1]),
                        timeTableData[2],
                        timeTableData[3]
                    ));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error parsing TimeTable from file: {0}", e.Message);
                }
            }
            return timeTables;
        }
    }
}
