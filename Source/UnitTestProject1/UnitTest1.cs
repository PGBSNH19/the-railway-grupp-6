using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TrainTrack;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public static List<Passenger> passengers;
        public static List<Train> trains;
        public static List<Station> stations;
        public static List<TimeTable> timeTables;

        [TestMethod]
        public void Test_NoTrainSetThrowsException()
        {
            Assert.ThrowsException<NullReferenceException>(() => 
            new TrainPlan()
            .TurnOffSwitch(new TrainSwitch { Status = SwitchStatus.On }, "10:32")
            .SetForTrain(null)
            .FollowTimeTable(timeTables)
            .StartTrain());
        }

        [TestMethod]
        public void Test_2()
        {
            Assert.ThrowsException<NullReferenceException>(() =>
            new TrainPlan()
            .TurnOffSwitch(new TrainSwitch { Status = SwitchStatus.On }, "10:32")
            .SetForTrain(trains[0])
            .FollowTimeTable(null)
            .StartTrain());
        }
    }
}
