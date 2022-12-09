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
    }
}
