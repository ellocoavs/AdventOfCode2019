using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Day_15
{
    class Program
    {
        public static class Globals
        {
            public static Int64 input = 2; // Unused in Day 11. 1 is test mode, 2 is boost mode
            public static Int64 relativeBase = 0;

            public static int[,] map = new int[500,500];
            public static int infinity = 99999;
            public static int wall = 9999999;
            public enum directions
            {
                north,
                south,
                west,
                east
            }
            public static (int,int) currentposition = (250,250);
            public static List<int> outputs = new List<int>();
            public static Queue<int> inputs = new Queue<int>();
            public static List<(int,int)> visited = new List<(int,int)>();
            public static List<(int,int)> neighbours = new List<(int,int)>();
            public static bool robotStop;
        }
        static void Main(string[] args)
        {
            var text = File.ReadAllText("input.txt");
            string[] split = text.Split(",");
            Int64[] opcodes= new Int64[50000];
            Int64 counter = 0;
            
            foreach (string x in split)
            {
                opcodes[counter] =Int64.Parse(x);
                counter++;
            }

            //Initialize map with infinity distancevalue (99999)
            InitializeMap();
            //Starting at 0 distance position on the map
            Globals.map[Globals.currentposition.Item1,Globals.currentposition.Item2] = 0;

            Task task0 = new Task( () => Compute(opcodes));
            Task task1 = new Task( () => RobotMoves());
            task0.Start();
            task1.Start();
            Task.WaitAll(task0,task1);
         //   Console.WriteLine("The number of panels that was painted at least once is: "+ Globals.PaintedPanels.Count);
            PrintMap();
        }

        static void InitializeMap()
        {
             for (int i =0;i<Globals.map.GetLength(0);i++)
            {
                for (int j =0;j<Globals.map.GetLength(1);j++)
                {
                    Globals.map[j,i] = Globals.infinity;
                }
            }
        }
        static void PrintMap() //Helps with debugging behaviour of robot (moving,painting)
        {
            for (int i =0;i<Globals.map.GetLength(0);i++)
            {
                for (int j =0;j<Globals.map.GetLength(1);j++)
                {
                    //if ((j,i) == Globals.currentposition){
                      //  Console.Write("X");
                    //}
                    //else 
                    //{
                    if (Globals.map[j,i] == 1){
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (Globals.map[j,i] ==0)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.Write(Globals.map[j,i]);
                    //}
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        static async void RobotDijkstraAsync()
        {
            while (true)
            {
                ListNeighbours();//make a list of neighbours to iterate over
                int currentdistance = Globals.map[Globals.currentposition.Item1,Globals.currentposition.Item2];
                
                foreach ((int,int)neighbour in Globals.neighbours)
                {   //mark all neighbours as current + 1, except if already lower?
                    if (currentdistance+1 < Globals.map[neighbour.Item1,neighbour.Item2] && Globals.map[neighbour.Item1,neighbour.Item2] != Globals.wall)
                    {
                        Globals.map[neighbour.Item1,neighbour.Item2] = currentdistance+1;
                    }
                }
            
            //then try to visit.  if wall- > inifnity + visited, else update value? 
            //give instruction (start north at first attemp?) ? create move method?
            //await feedback ? create feedback wait/grab method
            //do next intstruction ? calculate next option here.
            }
        }
        static void ListNeighbours()
        {
            if (!Globals.visited.Contains((Globals.currentposition.Item1+1,Globals.currentposition.Item2)))
            {
                Globals.neighbours.Add((Globals.currentposition.Item1+1,Globals.currentposition.Item2));
            }
            if (!Globals.visited.Contains((Globals.currentposition.Item1-1,Globals.currentposition.Item2)))
            {
                Globals.neighbours.Add((Globals.currentposition.Item1-1,Globals.currentposition.Item2));
            }
            if (!Globals.visited.Contains((Globals.currentposition.Item1,Globals.currentposition.Item2+1)))
            {
                Globals.neighbours.Add((Globals.currentposition.Item1,Globals.currentposition.Item2+1));
            }
            if (!Globals.visited.Contains((Globals.currentposition.Item1,Globals.currentposition.Item2-1)))
            {
                Globals.neighbours.Add((Globals.currentposition.Item1,Globals.currentposition.Item2-1));
            }
        }
        static void RobotMoves()  //This loops grabs input, paints, turns and moves. uses step integer to know if it's on a painting instruction or not
        {
            int step = 0;
            //PrintPanels();
            
            while (true)
            {
                //Thread.Sleep(5);
                
                int NumberOfInstructions = Globals.outputs.Count;
                if (NumberOfInstructions > 0)
                {
                    //Console.WriteLine("Instruction encountered!");
                    // If instruction = 99 STOP WORKING
                    int instruction = Globals.outputs[0];
                    Globals.outputs.RemoveAt(0);

                    if (instruction == 99)
                    {
                        Console.WriteLine("Instruction 99 encountered by robot. Stopping?");
                        Globals.robotStop=true;
                        break;
                    }

                    // if something else? process input!!
                    if (step == 0)
                    {
                        //stop 0 is PAINT current position in instructed color and go to step 1
                        Console.WriteLine("About to paint something "+instruction + " at position: " + Globals.currentposition);
                        Globals.panels[Globals.currentposition.Item1,Globals.currentposition.Item2] = instruction;
                        Globals.PaintedPanels.Add(Globals.currentposition);
                        step++;
                    }
                    else if (step == 1)
                    {   //step 1 is move and go back to step 0
                        //Console.WriteLine("Robot about to start moving.");
                        // TURN  0 = turn left, 1 = turn right
                        //Console.WriteLine("About turn in  direction: " + instruction);
                        switch (Globals.direction)
                        {
                            case Globals.directions.up:
                                if (instruction ==0){Globals.direction = Globals.directions.left;}
                                if (instruction ==1){Globals.direction = Globals.directions.right;}
                                break;

                            case Globals.directions.down:
                                if (instruction ==0){Globals.direction = Globals.directions.right;}
                                if (instruction ==1){Globals.direction = Globals.directions.left;}
                                break;

                            case Globals.directions.left:
                                if (instruction ==0){Globals.direction = Globals.directions.down;}
                                if (instruction ==1){Globals.direction = Globals.directions.up;}
                                break;

                            case Globals.directions.right:
                                if (instruction ==0){Globals.direction = Globals.directions.up;}
                                if (instruction ==1){Globals.direction = Globals.directions.down;}
                                break;
                        }

                        //MOVE
                        MoveInCurrentDirection();
                        step =0;
                    }
                   // PrintPanels();
                    
                }
                else 
                {   
                    //WAIT FOR INPUT
                    //Console.WriteLine("Waiting for instructions!");
                    Thread.Sleep(1); //slowing down a little?
                }
            }
        }
        static void MoveInCurrentDirection() //moves toward current direction
        {
            //Console.WriteLine("About to move towards: " + Globals.direction);
            switch (Globals.direction)
                        {
                            case Globals.directions.up:
                                Globals.currentposition = (Globals.currentposition.Item1, Globals.currentposition.Item2-1);
                                break;

                            case Globals.directions.down:
                                Globals.currentposition = (Globals.currentposition.Item1, Globals.currentposition.Item2+1);
                                break;

                            case Globals.directions.left:
                                Globals.currentposition = (Globals.currentposition.Item1-1, Globals.currentposition.Item2);
                                break;

                            case Globals.directions.right:
                                Globals.currentposition = (Globals.currentposition.Item1+1, Globals.currentposition.Item2);
                                break;
                        }
            Console.WriteLine("Now at: " + Globals.currentposition);
        }
        static int GetInput() //grabs value from current position of robot and returns
        {
            //Console.WriteLine("Grabbing input from robot camera into brain/opcode processor.");
            int input = Globals.inputs.Dequeue();
            Console.WriteLine("Input grabbed was: "+input );
            return input;
        }
            
        static void DoOutput(long parameter)  //outputs parameter to the queue for the robot
        {
            //Console.WriteLine("About to output parameter " + parameter + " to robot");
            if (parameter == 1 | parameter == 0  | parameter == 2)
            {
                Globals.outputs.Add((int)parameter);
              //  Console.WriteLine("Output succesful.");
            }
            else
            {
                Console.WriteLine("Invalid output attempted: " + parameter);
            }
            
        }
        
        static Int64 Compute (Int64[] opcodes) 
        {
            for (Int64 position = 0; ; )
            {
                //Console.WriteLine("Currently instructions in queue: " +  Globals.outputs.Count);
                Thread.Sleep(10);
                
                int rawOpcode = (int)opcodes[position];
                Console.WriteLine("Processing raw opcode: " + rawOpcode + " at position: " + position);
                Int64 actualOpcode = opcodes[position] % 100; //last two digits
                bool isPosMode1 = (opcodes[position] / 100) % 10 == 0; // digit before last two
                bool isPosMode2 = (opcodes[position] / 1000) % 10 == 0; //digit before last three
                bool isPosMode3 = (opcodes[position] / 10000) % 10 == 0; // digit before last four
                bool isRelMode1 = (opcodes[position] / 100) % 10 == 2; // digit before last two
                bool isRelMode2 = (opcodes[position] / 1000) % 10 == 2; //digit before last three
                bool isRelMode3 = (opcodes[position] / 10000) % 10 == 2; // digit before last four
                //Console.WriteLine("Opcode at position 0 is: " + actualOpcode);
                //Console.WriteLine("IsPositionalMode values are " + isPosMode1+isPosMode2+isPosMode3);
                
                //Console.WriteLine("Processing opcodes: " + opcodes[position] + "," + opcodes[position+1]+ "," + opcodes[position+2] + "," + opcodes[position+3]);
                //Console.WriteLine("Operands at: " + opcodes[position+1] + "," + opcodes[position+2] + " are: "+  opcodes[opcodes[position+1]] + "," + opcodes[opcodes[position+2]] );
              

                if (actualOpcode == 1) //ADD 3 params
                {
                    //OLD WAY opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] + opcodes[opcodes[position+2]];
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    //opcodes[opcodes[position+3]] = parameter1 + parameter2;
                    if (isPosMode3)
                    {
                        opcodes[opcodes[position+3]] = parameter1 + parameter2;
                    }
                    else if (isRelMode3)
                    {
                        opcodes[opcodes[position+3]+Globals.relativeBase] = parameter1 + parameter2;
                    }
                    else //direct mode
                    {
                        opcodes[position+3] = parameter1 + parameter2;
                    }
                    position += 4; //+4 because 1 op + 3 params
                }
                else if (actualOpcode == 2) //Multiply 3 params
                {
                    //opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] * opcodes[opcodes[position+2]];
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    //opcodes[opcodes[position+3]] = parameter1 * parameter2;
                    if (isPosMode3)
                    {
                        opcodes[opcodes[position+3]] = parameter1 * parameter2;
                    }
                    else if (isRelMode3)
                    {
                        opcodes[opcodes[position+3]+Globals.relativeBase] = parameter1 * parameter2;
                    }
                    else //direct mode
                    {
                        opcodes[position+3] = parameter1 * parameter2;
                    }
                    position += 4;
                }
                else if (actualOpcode == 3) //only one parameter. grab input from user and store at parameter
                {
                    //CHANGE TO GRAB INPUT FROM camera, make method GetInput (save GetInput result in local var then use.)
                    Thread.Sleep(10);
                    int input =  GetInput();
                    if (isPosMode1)
                    {
                        opcodes[opcodes[position+1]] = input;
                    }
                    else if (isRelMode1)
                    {
                        opcodes[opcodes[position+1]+Globals.relativeBase] = input;
                    }
                    else //direct mode
                    {
                        opcodes[position+1] = input;
                    }
                    //opcodes[opcodes[position+1]] = Globals.input;
                    //Console.WriteLine("Storing value: " + Globals.input + " at position: " +  opcodes[position+1]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    position += 2;
                }
                else if (actualOpcode == 4) //output what's stored at parameter1
                {
                    //output to queue and PAUSE for a bit. use Thread.Sleep(5); outputprocessor runs parallel.
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    //Console.WriteLine("Result about to be sent to robot from position: " + position + " is: " + parameter1);
                    DoOutput(parameter1);
                    Thread.Sleep(25);
                    position += 2;
                }
                else if (actualOpcode == 5) //jump-if-true, 2 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    //if param 1 true go to param 2 else position += 2;
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    if (parameter1 != 0){position=parameter2;} else {position += 3;}
                }
                else if (actualOpcode == 6) //jump-if-false 2 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    // if param 1 false go to param 2 else position += 2;
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    if (parameter1 == 0)
                    {
                        
                        position=parameter2;
                    } 
                    else {position += 3;}
                }
                else if (actualOpcode == 7) //less than, 3 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    // if param 1less than param2 -> 1 else -> 0
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    //opcodes[opcodes[position+3]] = parameter1 < parameter2 ? 1  : 0;
                    if (isPosMode3)
                    {
                        opcodes[opcodes[position+3]] = parameter1 < parameter2 ? 1  : 0;
                    }
                    else if (isRelMode3)
                    {
                        opcodes[opcodes[position+3]+Globals.relativeBase] = parameter1 < parameter2 ? 1  : 0;
                    }
                    else //direct mode
                    {
                        opcodes[position+3] = parameter1 < parameter2 ? 1  : 0;
                    }
                    position += 4;
                }
                else if (actualOpcode == 8) //equals, 3 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    // if param1 equals param2 -> 1 else 0
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    //opcodes[opcodes[position+3]] = parameter1 == parameter2 ? 1 : 0;

                    if (isPosMode3)
                    {
                        //Console.WriteLine("Saving to positional mode position: " + position + " the value of: " + (parameter1 == parameter2 ? 1  : 0));
                        opcodes[opcodes[position+3]] = parameter1 == parameter2 ? 1  : 0;
                    }
                    else if (isRelMode3)
                    {
                        opcodes[opcodes[position+3]+Globals.relativeBase] = parameter1 == parameter2 ? 1  : 0;
                    }
                    else //direct mode
                    {
                        opcodes[position+3] = parameter1 == parameter2 ? 1  : 0;
                    }
                    position += 4;
                }
                else if (actualOpcode == 9) //adjust relative base, 1 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    // if param1 equals param2 -> 1 else 0
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    Globals.relativeBase += parameter1;
                    position += 2;
                }
                else if (actualOpcode == 99)
                {
                    Console.WriteLine("Code 99 encountered, stopping after seeing opcode 99 at: " + position);
                    DoOutput(99);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid opcode detected: " + actualOpcode + " at position: " + position);
                    DoOutput(99);
                    throw new System.InvalidOperationException("Cannot process opcode: " +actualOpcode);
                    //break;
                }

            }
            return opcodes[0];
        }
    }
}
