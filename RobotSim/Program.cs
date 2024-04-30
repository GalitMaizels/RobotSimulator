
namespace ToyRobotSimulation
{
    public enum Facing
    {
            NORTH ,
            EAST,
            SOUTH,
            WEST
    }


    public class Robot
    {
        public int X {get;set;}
        public int Y {get;set;}
        public Facing Face {get;set;}
        private bool isPlacedCmd;

        public Robot()
        {
            X = 0;
            Y = 0;
            Face = Facing.NORTH;
            isPlacedCmd=false;
        }

        public void Place(int x, int y, Facing f)
        {
            if (x >= 0 && x < 5 && y >= 0 && y < 5 ) //valid board location
            {
                X = x;
                Y = y;
                Face= f;
                isPlacedCmd = true;
            }
            else
            {
                Console.WriteLine("Please insert valid PLACE command values");
            }
        }

        public void Move()
        {
            if (isPlacedCmd)
            {
                switch (Face)
                {
                    case Facing.NORTH:
                        if (Y < 4)
                            Y++;
                        break;
                    case Facing.SOUTH:
                        if (Y > 0)
                            Y--;
                        break;
                    case Facing.EAST:
                        if (X < 4)
                            X++;
                        break;
                    case Facing.WEST:
                        if (X > 0)
                            X--;
                        break;
                }
            }
        }


        public void Rotate(int rotation)
        {
            if (isPlacedCmd)
            {
                Face=(Facing)(((int)Face + rotation ) % 4);
            }
        }


        public void Report()
        {
            if (isPlacedCmd)
            {
                Console.WriteLine($"Robot Position: {X},{Y},{Face.ToString()}");
            }
        }
    }

    public class CommandParser
    {
        private Robot robot;

        public CommandParser(Robot r)
        {
            robot = r;
        }

        public void ExecuteCommandsFromFile(string filePath)
        {
            try
            {
                filePath=Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
                if (File.Exists(filePath))
                {
                    string[] commands = File.ReadAllLines(filePath);
                    foreach (string command in commands)
                    {
                        ExecuteCommand(command);
                    }
                }
                else
                {
                    Console.WriteLine("File not found: " + filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ExecuteCommand(string command)
        {
            string[] commandSections = command.Split(' ');
            string act = commandSections[0].ToUpper();

            switch (act)
            {
                case "PLACE":
                    if (commandSections.Length == 2)
                    {
                        string[] placeData = commandSections[1].Split(',');
                        if (placeData.Length == 3 &&
                        int.TryParse(placeData[0], out int x) && 
                        int.TryParse(placeData[1], out int y) &&
                        Enum.TryParse(placeData[2].ToUpper(), out Facing facing))
                        {
                            robot.Place(x, y, facing);
                        }
                    }
                    break;
                case "MOVE":
                    robot.Move();
                    break;
                case "LEFT":
                    robot.Rotate(3);
                    break;
                case "RIGHT":
                    robot.Rotate(1);
                    break;
                case "REPORT":
                    robot.Report();
                    break;
                default:
                    Console.WriteLine("Please insert a valid command");
                    break;
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Robot robot=new Robot();
            CommandParser cp = new CommandParser(robot);

            Console.WriteLine("Toy Robot Simulation: ");
            
            string filePath = "commands.txt";

            cp.ExecuteCommandsFromFile(filePath);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        } 
    }
}
