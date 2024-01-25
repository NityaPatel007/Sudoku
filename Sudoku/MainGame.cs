using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MainGame : MGame
{
        
    private KeyboardState keyboardState;
    int level = 1;
    int score = 0;
    Sudoku sudoku;
    List<SudokuCell> sudokuCells;
    List<SudokuBtn> buttons;
    int[,] solveGrid;
    int[,] grid;
    static int menuY = 130;
    SudokuCell selectedCell;
    int gameStatus = 0;

    public MainGame( )
    {
        initLevel(1);
    }
    Button[] btns = new Button[]
    {
        new Button(4,new Rectangle(Utils.get().width - 200,menuY + 500,200,50),"Back",Color.White),
    };
    public void loadGame()
    {
        List<int> data = Utils.get().loadGame();
        level = data[0];

        Debug.WriteLine("load: " +level);
        //foreach(int i in data)
        //{
        //    Debug.WriteLine(i);
        //}
        initLevel(level);
    }

    public void initLevel(int level)
    {
        int diff;
        if(level < 9)
        {
            diff = 10 - level;
        }
        else
        {
            diff = 1;
        }
        Debug.WriteLine(diff);

        sudoku = new Sudoku();
        sudokuCells = new List<SudokuCell>();
        buttons = new List<SudokuBtn>();
        int rows = 9;
        int cols = 9;
        int x = 50, y = 50;
        int cellSize = 64;
        int offset = 1;
        solveGrid = sudoku.getSolve(); // solve grid
        grid = sudoku.getHideGrid(diff, solveGrid);
        selectedCell = null;

      

        for (int row = 0; row < rows; row++)
        {
            for(int col = 0; col < cols; col++)
            {
                string text = grid[row, col].ToString();
                SudokuCell cell = new SudokuCell(row,col,new Rectangle(x,y, cellSize, cellSize), text);
                sudokuCells.Add(cell);

                if (col == 2 || col == 5)
                {
                    x += cellSize + offset * 4;
                }
                else
                {
                    x += cellSize + offset;
                }
            }
            if (row == 2 || row == 5)
            {
                y += cellSize + offset * 4;
            }
            else
            {
                y += cellSize + offset;
            }
            x = 50;
        }

        int id = 1;
        x = 800;
        y = 200;
        int size = 64;
        for (int row = 0; row < 3; row++)
        {
            for(int col = 0; col < 3; col++)
            {
                SudokuBtn button = new SudokuBtn(id, new Rectangle(x,y, size, size), id.ToString(), Color.Black,true);
                buttons.Add(button);
                x += size + 20;
                id ++;

            }
            x = 800;
            y += size + 20;
        }
    }

    void updateEvent()
    {
        keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Escape))
        {
            Utils.get().saveGame(level, score);
            
            Utils.get().game.Exit();
        }

        var mouseState = Mouse.GetState();
        float screenX = mouseState.X;
        float screenY = mouseState.Y;
        Vector2 mousePos = new Vector2(screenX, screenY);


        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            foreach (SudokuCell cell in sudokuCells)
            {
                if (cell.getRect().Contains(mousePos) && cell.isEditable())
                {
                    selectedCell = cell;
                    break;
                }
            }
        }
        if(selectedCell == null)
        {
            return;
        }

        if(mouseState.LeftButton == ButtonState.Pressed)
        {
            
            foreach (SudokuBtn button in buttons)
            {
                if (button.getRect().Contains(mousePos))
                {
                    selectedCell.textStr = button.id.ToString();
                    grid[selectedCell.getRow(), selectedCell.getCol()] = button.id;

                    checkWinner();

                    if (grid[selectedCell.getRow(), selectedCell.getCol()] == solveGrid[selectedCell.getRow(), selectedCell.getCol()])
                    {
                        score += 10;
                        selectedCell = null;
                    }

                    if (gameStatus == 1)
                    {
                        Utils.get().currentGame = new MenuScreen(true, score);
                        return;
                    }
                    if (gameStatus == 2)
                    {
                        level++;
                        score += 100;
                        initLevel(level);
                        return;
                    }
                }
            }
        }
        foreach (Button btn in btns) // click button
        {
            btn.color = Color.Black;
            if (screenX >= btn.getRect().X &&
                    screenX <= btn.getRect().X + btn.getRect().Width &&
                    screenY >= btn.getRect().Y &&
                    screenY <= btn.getRect().Y + btn.getRect().Height)
            {
                btn.color = Color.Tomato;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    switch (btn.getId())
                    {
                        case 4:
                            Utils.get().currentGame = new MenuScreen();
                            break;
                    }


                }
            }
        }
    }

    void checkWinner()
    {
        bool win = true;
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (grid[row,col] != 0 && grid[row,col] != solveGrid[row,col])
                {
                    //mistake++;
                    gameStatus = 1;
                }
                if (grid[row,col] != solveGrid[row,col])
                {
                    win = false;
                }
            }
        }
        if (win)
        {
            gameStatus = 2;
        }
    }


    override
    public void update()
    {
        updateEvent();

        foreach (SudokuCell cell in sudokuCells)
        {
        }
    }

    override
    public void draw()
    {
        foreach(SudokuCell cell in sudokuCells)
        {
            cell.color = Color.White;
            if(cell == selectedCell)
            {
                cell.color = Color.Tomato;
            }
            cell.draw();
        }
        foreach(SudokuBtn button in buttons)
        {
            button.draw();
        }

        Utils.get().drawText(800, 500, "Level: " + level.ToString() , Color.Yellow, Utils.get().font12);
        Utils.get().drawText(800, 530, "Score: " + score.ToString(), Color.LightCyan, Utils.get().font12);

        foreach (Button btn in btns)
        {
            btn.draw();
        }
    }
}
