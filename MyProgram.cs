using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace labyrinth
{
    class MyProgram
    {
        public void Run()
        {
            Random random = new Random();
            int[][] playerPos = new int[2][];
            int[][] prevPosition = new int[2][];
            int[][] curentPos = new int[2][];
            int[] dir = new int[4];
            float windowSizeX;
            float windowSizeY;
            int direction;
            int startside;
            int prevPosIndex = 0;
            int posDir;
            bool exit = true;
            int[] tempArr;
            int tempsInt = 0;
            Console.WriteLine("Select window size. Fullscreen = 210,50"); // fixar med fönster storleken
            windowSizeX = Convert.ToInt32(Console.ReadLine());
            windowSizeY = Convert.ToInt32(Console.ReadLine());
            if (windowSizeX >= 210)
            {
                windowSizeX = 209;
            }
            if (windowSizeY >= 50)
            {
                windowSizeY = 49;
            }
            if (windowSizeX % 2 == 0)
            {
                windowSizeX -= 1;
            }
            if (windowSizeY % 2 == 0)
            {
                windowSizeY -= 1;
            }
            if (windowSizeX <= 16)
            {
                windowSizeX = 17;
            }
            if (windowSizeY <= 16)
            {
                windowSizeY = 17;
            }
            Console.SetWindowSize(Convert.ToInt32(windowSizeX), Convert.ToInt32(windowSizeY));
            Console.Clear();
            Console.SetBufferSize(Convert.ToInt32(windowSizeX), Convert.ToInt32(windowSizeY));
            for (int i = 0; i < MathF.Floor(windowSizeY / 2); i++) //skapar gridet
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    Console.Write("#");
                }
                for (int j = 0; j < MathF.Floor(windowSizeX / 2); j++)
                {
                    Console.Write("# ");
                }
                Console.Write("#");
            }
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("#");
            }
            startside = random.Next(1, 5); // fixar med startar positionen för skapandet av laburinten
            if (startside == 1)
            {
                Console.SetCursorPosition(1 + 2 * random.Next(0, (Console.WindowWidth - 2) / 2), 0);
                Console.Write(" ");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop + 1);
            }
            else if (startside == 2)
            {
                Console.SetCursorPosition(Console.WindowWidth - 1, 1 + 2 * random.Next(0, (Console.WindowHeight - 2) / 2));
                Console.Write(" ");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
            else if (startside == 3)
            {
                Console.SetCursorPosition(1 + 2 * random.Next(0, (Console.WindowWidth - 2) / 2), Console.WindowHeight - 1);
                Console.Write(" ");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop - 1);
            }
            else if (startside == 4)
            {
                Console.SetCursorPosition(0, 1 + 2 * random.Next(0, (Console.WindowHeight - 2) / 2));
                Console.Write(" ");
            }
            prevPosition[0] = new int[1000]; // deklarerar grid positioner and stuf
            prevPosition[1] = new int[1000];
            playerPos[0] = new int[Console.WindowWidth];
            playerPos[1] = new int[Console.WindowHeight];
            curentPos[0] = new int[Console.CursorLeft];
            curentPos[1] = new int[Console.CursorTop];
            for (int i = 1; i < Console.WindowWidth/2 + 1; i++)
            {
                playerPos[0][i] = (i * 2) - 1;
            }
            for (int i = 1; i < Console.WindowHeight/2 + 1; i++)
            {
                playerPos[1][i] = (i * 2) - 1;
            }
            while (exit)
            {
                posDir = 0;
                for (int i = 1; i < playerPos[1].Length; i++)
                {
                    for (int j = 1; j < playerPos[0].Length; j++)
                    {
                        if (Console.BufferHeight < Console.CursorTop + 2 && prevPosition[0][j] != Console.CursorLeft || prevPosition[1][i] != Console.CursorTop - 2)
                        { // kollar om man kan gå ett snep neråt
                            posDir++;
                            dir[2] = 1;
                            break;
                        }
                    }
                    if (dir[2] == 1)
                    {
                        break;
                    }
                }
                for (int i = 1; i < playerPos[1].Length; i++)
                {
                    for (int j = 1; j < playerPos[0].Length; j++)
                    {
                        if (Console.BufferWidth < Console.CursorLeft + 2 && prevPosition[0][j] != Console.CursorLeft + 2 || prevPosition[1][i] != Console.CursorTop)
                        { // kollar om man kan gå ett snep höger
                            posDir++;
                            dir[1] = 1;
                            break;
                        }
                    }
                    if (dir[1] == 1)
                    {
                        break;
                    }
                }
                for (int i = 1; i < playerPos[1].Length; i++)
                {
                    for (int j = 1; j < playerPos[0].Length; j++)
                    {
                        if ( 0 > Console.CursorTop - 2 && prevPosition[0][j] != Console.CursorLeft || prevPosition[1][i] != Console.CursorTop + 2)
                        { // kollar om man kan gå ett snep upp
                            posDir++;
                            dir[0] = 1;
                            break;
                        }
                    }
                    if (dir[0] == 1)
                    {
                        break;
                    }
                }
                for (int i = 1; i < playerPos[1].Length; i++)
                {
                    for (int j = 1; j < playerPos[0].Length; j++)
                    {
                        if (0 > Console.CursorLeft - 2 && prevPosition[0][j] != Console.CursorLeft - 2 || prevPosition[1][i] != Console.CursorTop)
                        { // kollar om man kan gå ett snep vänster
                            posDir++;
                            dir[3] = 1;
                            break;
                        }
                    }
                    if (dir[3] == 1)
                    {
                        break;
                    }
                }
                tempArr = new int[posDir];       //ger ett random nummer från en lista av nummer
                for (int i = 0; i < posDir; i++) 
                {
                    if (dir[i] == 1)
                    {
                        tempArr[tempsInt] = i;
                        tempsInt++;
                    }
                }
                tempsInt = 0;
                direction = random.Next(0, posDir); // 0=upp 1=höger 2=ner 3=vänster;       DET ÄR HÄR JAG HAR GJORT FEL
                if (tempArr[direction] == 0) //går upp 
                {
                    prevPosition[0][prevPosIndex] = Console.CursorLeft;
                    prevPosition[1][prevPosIndex] = Console.CursorTop;
                    prevPosIndex++;
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop + 1);
                }
                else if (tempArr[direction] == 1) //går höger
                {
                    prevPosition[0][prevPosIndex] = Console.CursorLeft;
                    prevPosition[1][prevPosIndex] = Console.CursorTop;
                    prevPosIndex++;
                    Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                    Console.Write(" ");
                }
                else if (tempArr[direction] == 2) // går ner
                {
                    prevPosition[0][prevPosIndex] = Console.CursorLeft;
                    prevPosition[1][prevPosIndex] = Console.CursorTop;
                    prevPosIndex++;
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop - 1);
                }
                else if (tempArr[direction] == 3) // går vänster
                {
                    prevPosition[0][prevPosIndex] = Console.CursorLeft;
                    prevPosition[1][prevPosIndex] = Console.CursorTop;
                    prevPosIndex++;
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
                }
                if (posDir == 0)
                {
                    Console.SetCursorPosition(prevPosition[0][prevPosIndex - 1], prevPosition[1][prevPosIndex - 1]);
                }
                if (prevPosition[0][0] == playerPos[0][0] && prevPosition[1][0] == playerPos[1][0])
                {
                    exit = false;
                }
            }
            Console.ReadLine();
        }
       
        /* Rules
         * implementera classer
         * labyrint ska vara i consolpanelen  ()
         * man kan välja vilken storleken laburinten ska vara  ()
         * en stig igenom
         * inga chunks av väggar
         * startar på en random plats inuti laburinten
         * 
         * A.I
         * löser laburinten igenom att hålla vänster väg
         * löser laburinten igenom att hålla höger väg
         * rekulsiv lösning
         * 
         * ta tid på hur snabbt de olika lösningarna tar och du ska kunna lägga in hur många laburinter de olika AI:n ska lösa
         * utan att visa något i consolpanelen förutom tiden 
         */
    }
}
