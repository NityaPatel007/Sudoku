using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Button 
{
	string text;
	int id;
	public Color color;
	Rectangle rect;
	Texture2D texture;
	public bool clicked = false;
	bool drawRect;

	public Button(int id,Rectangle rect, string text,Color color,bool drawRect = false)
	{
		this.text = text;
		this.id = id;
		this.rect = rect;
		this.color = color;
		this.drawRect = drawRect;
	}

	public void draw()
	{
		texture = new Texture2D(Utils.get()._graphics.GraphicsDevice, 1, 1);
        texture.SetData(new[] { Color.White });
        if (drawRect)
        {
			Utils.get().batch.Draw(texture, rect, Color.White);
		}
		Vector2 textSize = Utils.get().font32.MeasureString(text);

		Utils.get().drawText(rect.X + rect.Width/2 - textSize.X/2, rect.Y + rect.Height/2- textSize.Y / 2, text, color, Utils.get().font32);

	}

	//public void onMouseDown(Vector2 mouse)
 //   {
 //       if (rect.Contains(mouse))
 //       {
	//		clicked = true;
 //       }
 //   }
	//public void OnMouseUp()
 //   {
 //       if (clicked)
 //       {
	//		clicked = false;
 //       }
 //   }

	public int getId()
    {
		return id;
    }

	public Rectangle getRect()
	{
		return rect;
	}

}

