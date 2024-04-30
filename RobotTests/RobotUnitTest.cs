namespace RobotTests;
using ToyRobotSimulation;

[TestClass]
public class RobotUnitTest
{
    [TestMethod]
    public void TestPlacingRobot()
    {
        Robot robot = new Robot();
        robot.Place(1, 2, Facing.WEST);
        Assert.AreEqual(1, robot.X);
        Assert.AreEqual(2, robot.Y);
        Assert.AreEqual(Facing.WEST, robot.Face);
    }

    [TestMethod]
    public void TestPlacingRobotInvalidLocation()
    {
        Robot robot = new Robot();
        robot.Place(7, 2, Facing.WEST);
        Assert.AreEqual(0, robot.X);
        Assert.AreEqual(0, robot.Y);
        Assert.AreEqual(Facing.NORTH, robot.Face);
    }

    [TestMethod]
        public void TestMovingRobot()
        {
            Robot robot = new Robot();
            robot.Place(0, 0, Facing.NORTH);
            robot.Move();
            Assert.AreEqual(0, robot.X);
            Assert.AreEqual(1, robot.Y);
        }


    [TestMethod]
        public void TestMovingRobotToInvalidLocation()
        {
            Robot robot = new Robot();
            robot.Place(4, 0, Facing.EAST);
            robot.Move();

            Assert.AreEqual(4, robot.X);
            Assert.AreEqual(0, robot.Y);
        }

[TestMethod]
        public void TestMovingRobotBeforePlaceCommand()
        {
            Robot robot = new Robot();
            robot.Move();
            Assert.AreEqual(0, robot.X);
            Assert.AreEqual(0, robot.Y);
        }


        [TestMethod]
        public void TestRotatingRobot()
        {
            Robot robot = new Robot();
            robot.Place(0, 0, Facing.NORTH);
            robot.Rotate(3);
            Assert.AreEqual(Facing.WEST, robot.Face);
            robot.Rotate(1);
            Assert.AreEqual(Facing.NORTH, robot.Face);
            robot.Rotate(1);
            Assert.AreEqual(Facing.EAST, robot.Face);
        }


}