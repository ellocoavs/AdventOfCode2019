using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace RectanglesEx
{
    class Program : Form
    {
        public static class Globals
        {
            public static int input = 0; // Unused in Day 13
            public static int relativeBase = 0;

            public static List<int> outputs = new List<int>();

            //the grid to draw!
            public static int [,] grid = new int[50,50];  //100 x 100 with boxes of 5 pixels each
        }
        public Program()
        {
            InitUI();
        }

        private void InitUI()
        {
            Text = "Day 13 part 1 visualizer";
            Paint += new PaintEventHandler(OnPaint);

            ClientSize = new Size(250, 250);  //5x100 by 5x100
            //CenterToScreen();
        }

        void OnPaint(object sender, PaintEventArgs e)
        {
            int step =5;
            int width = 5;
            int height =5;

            Graphics g = e.Graphics;
            
            //loop throuh matrix with values here
            //g.Clear(SystemColors.Control); 
            //g.FillRectangle(Brushes.Sienna, 10, 15, 90, 60);
            // while (true){
            //     for (int i=0; i<Globals.grid.GetUpperBound(1);i++)
            //     {
            //         for (int j=0; j<Globals.grid.GetUpperBound(0);j++)
            //         {
            //             if (Globals.grid[j,i]>0)  //MAKE INTO FUNCTION
            //                 {
            //                     //Console.WriteLine("Drawing something?");
            //                     Pen pen = new Pen(Color.Red, 1);
            //                     Rectangle rect = new Rectangle(new Point(step * j, step * i), new Size(width, height));
            //                     g.DrawRectangle(pen, rect);
            //                     g.FillRectangle(System.Drawing.Brushes.Red, rect);
                                
            //                 }
            //         }
            //     }
            //     Thread.Sleep(100);
            // }
        }

        [STAThread]
        static void Main()
        {
            var text = File.ReadAllText("input.txt");
            string[] split = text.Split(",");
            int[] opcodes= new int[50000];
            int counter = 0;
            
            foreach (string x in split)
            {
                opcodes[counter] =int.Parse(x);
                counter++;
            }

            Task task0 = new Task( () => Compute(opcodes));
            //Task task1 = new Task( () => DrawScreen());
            Task task2 = new Task( () => UpdateGrid());
            task2.Start();
            //task1.Start();
            task0.Start();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            //Task task3 = new Task( () => Application.Run(new Program()));
            //Application.Run(new Program());

          
            
            
            Task.WaitAll(task0);
            int countBlocks = 0;
            for (int i=0; i<Globals.grid.GetUpperBound(1);i++)
                {
                    for (int j=0; j<Globals.grid.GetUpperBound(0);j++)
                    {
                        if (Globals.grid[j,i]==2) 
                            {
                                countBlocks++;
                            }
                    }
                }
            Console.WriteLine("Number of Blocks is: " +countBlocks);
        }
          
        static int GetInput() //grabs value from current position of robot and returns
        {
            //should be returning keyboard input at some points
            return 0;
        }
            
        static void DrawScreen()
        {
            //do some drawing logic based on our grid
        }

        static void UpdateGrid() //maybe change Grid to List?
        {
            //update grid based on output list (every time there are 3 values, do update of x,y with value z)
            //int step = 0;
            while (true)
            {
                
                
                int NumberOfInstructions = Globals.outputs.Count;
                if (NumberOfInstructions >= 3) //3 instructions = full instruction to handle
                {
                    int x = Globals.outputs[0];
                    Globals.outputs.RemoveAt(0);
                    int y = Globals.outputs[0];
                    Globals.outputs.RemoveAt(0);
                    int id = Globals.outputs[0];
                    Globals.outputs.RemoveAt(0);

                    if (id > 4)//invalid id!
                    {
                        Console.WriteLine("Invalid id encountered by Grid. Stopping due to id: "+ id);
                        //Globals.drawStop=true;
                        break;
                    }
                    else {//TODO now put id into grid at x,y
                    Globals.grid[x,y]= id;
                    }
                }
            }
            
        }
        static void DoOutput(int parameter)  //outputs parameter to the queue for the robot
        {
               // Console.WriteLine("Outputting something that should be drawn after three outputs");
                Globals.outputs.Add((int)parameter); //this will likely get more complicated
        }
        
        static int Compute (int[] opcodes) 
        {
            for (int position = 0; ; )
            {
                //Console.WriteLine("Currently instructions in queue: " +  Globals.outputs.Count);
                Thread.Sleep(10);
                
                int rawOpcode = opcodes[position];
                //Console.WriteLine("Processing raw opcode: " + rawOpcode + " at position: " + position);
                int actualOpcode = opcodes[position] % 100; //last two digits
                bool isPosMode1 = (opcodes[position] / 100) % 10 == 0; // digit before last two
                bool isPosMode2 = (opcodes[position] / 1000) % 10 == 0; //digit before last three
                bool isPosMode3 = (opcodes[position] / 10000) % 10 == 0; // digit before last four
                bool isRelMode1 = (opcodes[position] / 100) % 10 == 2; // digit before last two
                bool isRelMode2 = (opcodes[position] / 1000) % 10 == 2; //digit before last three
                bool isRelMode3 = (opcodes[position] / 10000) % 10 == 2; // digit before last four
                

                if (actualOpcode == 1) //ADD 3 params
                {
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
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
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
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
                    //Thread.Sleep(10);
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
                    position += 2;
                }
                else if (actualOpcode == 4) //output what's stored at parameter1
                {
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    //dooutput should save to grid for the drawing component to update
                    DoOutput(parameter1);
                    //Thread.Sleep(25);
                    position += 2;
                }
                else if (actualOpcode == 5) //jump-if-true, 2 params
                {
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    if (parameter1 != 0){position=parameter2;} else {position += 3;}
                }
                else if (actualOpcode == 6) //jump-if-false 2 params
                {
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
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
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
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    if (isPosMode3)
                    {
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

