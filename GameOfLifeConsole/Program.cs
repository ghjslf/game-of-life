using System;
using CellularAutomatonEngine;

namespace GameOfLifeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int sparseness = 4;

            Console.ReadLine();

            Console.CursorVisible = false;

            Console.SetCursorPosition(0, 0);

            var engine = new GameOfLifeEngine
                (
                    rows: Console.WindowHeight - 1,
                    cols: Console.WindowWidth - 1
                );

            engine.RandomFilling(sparseness);

            while (true)
            {
                Console.Title = Convert.ToString(engine.Generation);

                var renderField = engine.GetCurrentGeneration();

                var line = new char[renderField.GetLength(0)];

                for (int y = 0; y < renderField.GetLength(1); y++)
                {
                    for (int x = 0; x < renderField.GetLength(0); x++)
                    {
                        if (renderField[x, y])
                        {
                            line[x] = '#';
                        }
                        else
                        {
                            line[x] = ' ';
                        }
                    }
                    Console.WriteLine(line);
                }

                Console.SetCursorPosition(0, 0);
                engine.CreateNextGeneration();
            }
        }
    }
}
