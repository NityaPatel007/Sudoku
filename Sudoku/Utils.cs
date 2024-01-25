using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Diagnostics;


public class Utils
{
	private static Utils instance = null;

	public SpriteBatch batch;
	public SpriteFont font12;
	public SpriteFont font24;
	public SpriteFont font32;
	public SpriteFont font60;

	public Game game;

	public ContentManager content;

	public int width = 1280;
	public int height = 720;

	public float deltaTime;

	public Vector2 camera = new Vector2(0, 0);

	public MGame currentGame;

	public GraphicsDeviceManager _graphics;
	private Utils()
	{
	}

	public void setBatch(SpriteBatch batch)
    {
		this.batch = batch;
    }

	public void setContent(ContentManager content)
    {
		this.content = content;
    }

	public static Utils get() // singleton class
	{
		if (instance == null)
			instance = new Utils();
		return instance;
	}

	public void drawText(float x, float y, String text, Color color, SpriteFont font)
	{
		batch.DrawString(font, text, new Vector2(x, y), color);
    }

	public void saveScore(int score)
    {
		using (StreamWriter w = File.AppendText("score.txt"))
		{
			w.WriteLine(score);
		}
	}
	public void saveGame(int level, int score)
	{
		using (StreamWriter w = new StreamWriter("save.txt"))
		{
			w.WriteLine(level);
			w.WriteLine(score);
		}
	}

	public List<int> loadGame()
    {
		List<int> data = new List<int>();
        //return scores;

        try
        {
			using (TextReader reader = File.OpenText("save.txt"))
			{
				var lines = File.ReadLines("save.txt");
				foreach (var line in lines)
				{
					int s = int.Parse(line);
					data.Add(s);
				}
			}
		}
		catch(Exception e)
        {
			data.Add(1);
			data.Add(0);
		} 
		
		return data;
	}

	public List<int> loadTop5Score()
	{
		List<int> scores = new List<int>();

        using (TextReader reader = File.OpenText("score.txt"))
		{
			var lines = File.ReadLines("score.txt");
			foreach (var line in lines)
            {
				int s = int.Parse(line);
				if(s == 0)
                {
					continue;
                }
				scores.Add(s);
			}
		}
		scores.Sort();
		scores.OrderByDescending(o => o).Take(5);
		return scores;
	}
	
}
