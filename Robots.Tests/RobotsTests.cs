namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    public class RobotsTests
    {
        private RobotManager robotManager;
        private Robot robot;

        [SetUp]
        public void SetUp()
        {
            robotManager = new RobotManager(5);
            robot = new Robot("robotName", 100);
        }

        [Test]
        public void CapacityCannotBeLessThanZero()
        {
            Assert.Throws<ArgumentException>(() => robotManager = new RobotManager(-2));
        }

        [Test]
        public void CapacityGetterWorks()
        {
            Assert.That(robotManager.Capacity, Is.EqualTo(5));
        }

        [Test]
        public void CountGetterWorks()
        {
            Assert.That(robotManager.Count, Is.EqualTo(0));
            robotManager.Add(robot);
            Assert.That(robotManager.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddWithExistentRoboThrows()
        {
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() => robotManager.Add(robot));
        }

        [Test]
        public void AddWithWithFilledCapacityThrows()
        {
            robotManager = new RobotManager(1);
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() => robotManager.Add(new Robot("name", 50)));
        }

        [Test]
        public void RemoveWithNullThrows()
        {
            Assert.Throws<InvalidOperationException>(() => robotManager.Remove(null));
        }

        [Test]
        public void RemoveWorks()
        {
            robotManager.Add(robot);
            robotManager.Remove("robotName");
            Assert.That(robotManager.Count, Is.EqualTo(0));
        }

        [Test]
        public void WorkWithNonExistentRobotThrows()
        {
            Assert.Throws<InvalidOperationException>(() => robotManager.Work("name", "job", 10));
        }

        [Test]
        public void WorkWithNotEnoughBatteryThrows()
        {
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() => robotManager.Work("robotName", "job", 10000));
        }

        [Test]
        public void WorkMethodWorks()
        {
            robotManager.Add(robot);
            var result = robot.Battery - 5;
            robotManager.Work("robotName", "job", 5);
            Assert.That(robot.Battery, Is.EqualTo(result));
        }

        [Test]
        public void ChargeWithNonExistentRobotThrows()
        {
            Assert.Throws<InvalidOperationException>(() => robotManager.Charge("robotName"));
        }

        [Test]
        public void ChargeWorks()
        {
            robotManager.Add(robot);
            robotManager.Work("robotName", "job", 5);
            robotManager.Charge("robotName");
            Assert.That(robot.Battery, Is.EqualTo(robot.MaximumBattery));
        }
    }
}
