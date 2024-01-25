using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Sudoku
{
    Random random = new Random();

    public int[,] getSolve()
    {
        int[,] arr = new int[9,9];

        generate(arr);
        solve(arr, 0, 0);
        int[,] result = new int[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                result[i,j] = arr[i,j];
                //printf("%d ", result[i][j]);
            }
            //printf("\n");
        }
        return result;
    }
    public  int[,] getHideGrid(int difficult, int[,] solveGrid)
    {
        int[,] result = new int[9,9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                int d = random.Next(0,10);
                if (d >= difficult)
                {
                    result[i,j] = 0;
                }
                else
                {
                    result[i,j] = solveGrid[i,j];
                }
            }
        }
        return result;
    }
    private  int counter = 0;
    private  void swap(int a, int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }
    private  void shuffle(int[] arr, int n)
    {
        Random rand = new Random();
        for (int i = n - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            swap(arr[i], arr[j]);
        }
    }
    private  void print(int[,] arr)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (col == 3 || col == 6)
                   Debug.Write( " | ");
                Debug.Write( arr[row,col] + " ");
            }
            if (row == 2 || row == 5)
            {
                Debug.Write(" \n ");
                for (int i = 0; i < 9; i++)
                    Debug.Write("---");
            }
            Debug.Write(" \n ");
        }
    }
    private  void generate(int[,] arr)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                arr[i,j] = 0;
            }
        }
    }
    private  bool isSafe(int[,] grid, int row, int col, int num)
    {
        for (int x = 0; x <= 8; x++)
            if (grid[row,x] == num)
                return false;

        for (int x = 0; x <= 8; x++)
            if (grid[x,col] == num)
                return false;

        int startRow = row - row % 3,
                startCol = col - col % 3;

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (grid[i + startRow,j + startCol] == num)
                    return false;

        return true;
    }
    private  bool solve(int[,] grid, int row, int col)
    {
        if (row == 9 - 1 && col == 9)
            return true;

        if (col == 9)
        {
            row++;
            col = 0;
        }

        if (grid[row,col] > 0)
            return solve(grid, row, col + 1);
        int[] nms = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        shuffle(nms, 9);
        for (int num = 1; num <= 9; num++)
        {
            if (isSafe(grid, row, col, nms[num - 1]))
            {
                grid[row,col] = nms[num - 1];
                counter += 1;
                if (solve(grid, row, col + 1))
                    return true;
            }
            grid[row,col] = 0;
        }
        return false;
    }
    private  bool isSolve(int[,] arr)
    {
        if (solve(arr, 0, 0) == true)
        {
            return true;
        }
        return false;
    }
    private  void copy(int[,] arr1, int[,] arr2)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                arr2[i,j] = arr1[i,j];
            }
        }
    }
    //private  void random(int[,] grid)
    //{

    //    int randX = rand() % 8;
    //    int randY = rand() % 8;
    //    while (grid[randX][randY] == 0)
    //    {
    //        randX = rand() % 8;
    //        randY = rand() % 8;
    //    }
    //    int backup = grid[randX][randY];
    //    grid[randX][randY] = 0;
    //    int newGrid[9][9];
    //    copy(grid, newGrid);
    //    if (isSafe(grid, randX, randY, backup) != 1)
    //    {
    //        newGrid[randX][randY] = backup;
    //        grid[randX][randY] = backup;
    //    }
    //    copy(newGrid, grid);
    //}
};