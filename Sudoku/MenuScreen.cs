using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MenuScreen : MGame
{

    private KeyboardState keyboardState;
    Vector2 mousePos;
    Texture2D bg;
    bool showGameOver = false;

    static int menuY = -100;
    int score;
    List<int> topScores;


    Button[] btns = new Button[]
    {
        new Button(0,new Rectangle(Utils.get().width/2 - 300/2,menuY + 300,300,50),"New Game",Color.Black),
        new Button(1,new Rectangle(Utils.get().width/2 - 300/2,menuY + 350 + 20,300,50),"Continue",Color.Black),
        new Button(2,new Rectangle(Utils.get().width/2 - 300/2,menuY + 400 + 20* 2,300,50),"HighScore",Color.Black),
        new Button(3,new Rectangle(Utils.get().width/2 - 300/2,menuY + 450 + 20* 3,300,50),"Help",Color.Black),
        new Button(4,new Rectangle(Utils.get().width/2 - 300/2,menuY + 500 + 20* 4,300,50),"About",Color.Black),
        new Button(5,new Rectangle(Utils.get().width/2 - 300/2,menuY + 550 + 20* 5,300,50),"Quit",Color.Black),
    };

    public MenuScreen(bool showGameOver = false,int score = 0)
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
                        case 0:
                            Utils.get().currentGame = new MainGame();
                            return;
                            break;
                        case 1:
                            Utils.get().currentGame = new MainGame();
                            ((MainGame)Utils.get().currentGame).loadGame();
                            break;
                        case 2:
                            Utils.get().currentGame = new HighScoreScreen();
                            break;
                        case 3:
                            Utils.get().currentGame = new HelpScreen();
                            break;
                        case 4:
                            Utils.get().currentGame = new AboutScreen();
                            break;
                        case 5:
                            Utils.get().game.Exit();
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
        if (showGameOver)
        {
            Utils.get().drawText(50, 650, "Game Over!", Color.Red, Utils.get().font24);
            Utils.get().drawText(50, 680, "Score: " + score.ToString(), Color.LightCyan, Utils.get().font24);
        }

        
    }
}
