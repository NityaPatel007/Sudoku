using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AboutScreen : MGame
{

    private KeyboardState keyboardState;
    Vector2 mousePos;
    Texture2D bg;
    bool showGameOver = false;

    int score;
    List<int> topScores;


    Button[] btns = new Button[]
    {
        new Button(4,new Rectangle(Utils.get().width - 180,Utils.get().height - 50,200,50),"Back",Color.Black),
    };

    public AboutScreen(bool showGameOver = false,int score = 0)
    {
        this.score = score;
        this.showGameOver = showGameOver;
        if (showGameOver)
        {
            Utils.get().saveScore(score);
        }
        topScores = Utils.get().loadTop5Score();
        bg = Utils.get().content.Load<Texture2D>("bg");
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
            btn.color = Color.White;
            if (    screenX >= btn.getRect().X &&
                    screenX <= btn.getRect().X + btn.getRect().Width &&
                    screenY >= btn.getRect().Y &&
                    screenY <= btn.getRect().Y + btn.getRect().Height)
            {
                btn.color = Color.Red;
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
        Utils.get().drawText(190, 100, "Developers", Color.White, Utils.get().font60);
        Utils.get().drawText(220, 400, $"Nitya Patel -  Developer", Color.White, Utils.get().font32);
    }
}
