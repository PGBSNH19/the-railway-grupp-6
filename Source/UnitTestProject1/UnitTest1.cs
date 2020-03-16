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
            bool failed;
            //Arrange
            var timeTable = new List<TimeTable> { new TimeTable(1, 1, null, "10:20") };

            try
            {
                //Act
                var result = new TrainPlan()
                .TurnOffSwitch(new TrainSwitch { Status = SwitchStatus.On }, "10:32")
                .SetForTrain(null)
                .FollowTimeTable(timeTable)
                .StartTrain();

                failed = false;
            }
            catch
            {
                failed = true;
            }
            

            //Assert
            Assert.AreEqual(true, failed);
        }

        [TestMethod]
        public void Test_2()
        {
            //Arrange
            var train = new Train(1, "mcTrainface", 60, true);

            //Assert.ThrowsException<NullReferenceException>(() =>
            //new TrainPlan()
            //.TurnOffSwitch(new TrainSwitch { Status = SwitchStatus.On }, "10:32")
            //.SetForTrain(new Train())
            //.FollowTimeTable(new List<TimeTable>())
            //.StartTrain());
        }
    }
}
