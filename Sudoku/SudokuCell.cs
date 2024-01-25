using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SudokuCell 
{

    int row, col;
    bool editable = true;
    public string textStr;
    Rectangle rect;
    Texture2D texture;

    public Color color { get; set; }
    public SudokuCell(int row, int col, Rectangle rect, string textStr)
    {
        this.row = row;
        this.col = col;
        this.rect = rect;
        this.textStr = textStr;
        color = Color.White;
    }

    public void draw()
    {
        texture = new Texture2D(Utils.get()._graphics.GraphicsDevice, 1, 1);
        texture.SetData(new[] { Color.White });
        Utils.get().batch.Draw(texture, rect, color);

        if(textStr != "0")
        {
            Vector2 textSize = Utils.get().font12.MeasureString(textStr);
            Utils.get().drawText(rect.X + rect.Width / 2 - textSize.X / 2, rect.Y + rect.Height / 2 - textSize.Y / 2, textStr, Color.Black, Utils.get().font12);

        }
    }

    public Rectangle getRect()
    {
        return rect;
    }

    public bool isEditable()
    {
        return textStr == "0";
    }
    public int getRow()
    {
        return row;
    }
    public int getCol()
    {
        return col;
    }
};