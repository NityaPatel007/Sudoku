using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HighScoreScreen : MGame
{

    private KeyboardState keyboardState;
    Vector2 mousePos;
    Texture2D bg;
    bool showGameOver = false;

    static int menuY = 130;
    int score;
    List<int> topScores;


    Button[] btns = new Button[]
    {
        new Button(4,new Rectangle(Utils.get().width - 200,menuY + 500,200,50),"Back",Color.Black),
    };

    public HighScoreScreen(bool showGameOver = false,int score = 0)
    {
        this.score = score;
        this.showGameOver = showGameOver;
        if (showGameOver)
        {
            Utils.get().saveScore(score);
        }
        topScores = Utils.get().loadTop5Score();
        bg = Utils.get().content.Load<Texture2D>("bg");

        topScores = Utils.get().loadTop5Score();
        Debug.WriteLine("score:");
        foreach(int i in topScores)
        {
            Debug.WriteLine(i);
        }
    }

    void updateEvent()
    {
        keyboardState = Keyboard.GetState();

        var mouseState = Mouse.GetState();
        float screenX = mouseState.X;
        float screenY = mouseState.Y;
        mousePos = new Vector2(screenX, screenY);
           
        foreach (Button btn in btns) // click button
        {
            btn.color = Color.Black;
            if (    screenX >= btn.getRect().X &&
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

    override
    public void update()
    {
        updateEvent();

    }

    override
    public void draw()
    {
     
        Utils.get().batch.Draw(bg, new Vector2(0,0), new Rectangle(0,0,bg.Width,bg.Height), Color.White);

        foreach (Button btn in btns)
        {
            btn.draw();
        }
        Utils.get().drawText(380, 200, "Top 5 Highscore", Color.Black, Utils.get().font32);
        int y = 350;
        for (int i = topScores.Count - 1; i >= 0; i--)
        {
            Utils.get().drawText(480, y, topScores[i].ToString(), Color.Black, Utils.get().font32);
            y += 40;
        }
    }
}
