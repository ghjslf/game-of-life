using System;

namespace CellularAutomatonEngine
{
    public class GameOfLifeEngine
    {
        public uint Generation { get; private set; }
        private bool[,] field;
        private readonly int rows;
        private readonly int cols;


        public GameOfLifeEngine(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            field = new bool[cols, rows];
        }

        public void RandomFilling(int sparseness)
        {
            Random random = new Random();
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    field[x, y] = random.Next(sparseness) == 0;
                }
            }
        }

        public void ClearField()
        {
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    field[x, y] = false;
                }
            }
        }

        public bool[,] GetCurrentGeneration()
        {
            //return result;
            var result = new bool[cols, rows];
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    result[x, y] = field[x, y];
                }
            }

            return field;
        }

        public void CreateNextGeneration()
        {
            var newField = new bool[cols, rows];
            int numberOfAdjacentCells;
            bool isAlive;

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    numberOfAdjacentCells = CountAdjacentCells(x, y);
                    isAlive = field[x, y];

                    if (!isAlive && numberOfAdjacentCells == 3)
                    {
                        newField[x, y] = true;
                    }
                    else if (isAlive && (numberOfAdjacentCells < 2 || numberOfAdjacentCells > 3))
                    {
                        newField[x, y] = false;
                    }
                    else
                    {
                        newField[x, y] = isAlive;
                    }
                }
            }

            Generation++;
            field = newField;
        }

        private int CountAdjacentCells(int x, int y)
        {
            int count = 0;
            int col;
            int row;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    col = (x + i + cols) % cols;
                    row = (y + j + rows) % rows;

                    if (field[col, row] && (col != x || row != y))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private bool ValidateCellPosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }

        private void UpdateCell(int x, int y, bool state)
        {
            if (ValidateCellPosition(x, y))
            {
                field[x, y] = state;
            }
        }

        public void AddCell(int x, int y)
        {
            UpdateCell(x, y, true);
        }

        public void RemoveCell(int x, int y)
        {
            UpdateCell(x, y, false);
        }
    }
}