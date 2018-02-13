using System.Collections;
using System.Collections.Generic;

public class GameCore
{
    public List<List<byte>> GameField { get; private set; }
    public ResourcesStorage storage;
    private List<SimpleTile> CurrentLine;
    //public UsageItems
    public GameCore()
    {

    }
    public bool NewGame(int x, int y)
    {
        GameField = new List<List<byte>>();
        for (int i = 0; i < x; i++)
        {
            GameField.Add(new List<byte>());
            for (int j = 0; j < y; j++)
            {
                Resource r = storage.GetOneRandomResource();
                if (r != null)
                    GameField[i].Add((byte)storage.GetOneRandomResource().Id);
                else return false;
            }
        }
        return true;
    }
    public bool NewGame(int x, int y, ResourcesStorage storage)
    {
        GameField = new List<List<byte>>();
        for (int i = 0; i < x; i++)
        {
            GameField.Add(new List<byte>());
            for (int j = 0; j < y; j++)
            {
                Resource r = storage.GetOneRandomResource();
                if (r != null)
                    GameField[i].Add((byte)storage.GetOneRandomResource().Id);
                else return false;
            }
        }
        this.storage = storage;
        return true;
    }
    /// <summary>
    /// проверяет можно ли испльзовать клетку
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>0 - нельзя, 1 - можно, -1 - задний ход</returns>
    public int CheckInputTile(int x, int y)
    {
        //границы поля
        if (!(x >= 0 && x < GameField[GameField.Count - 1].Count && y >= 0 && y < GameField.Count)) return 0;
        //проверяем что этой клетки нет в линии или она последняя
        int IndexTile = CurrentLine.FindLastIndex(u => u.x == x && u.y == y);
        if(IndexTile == -1) return 1;
        if(IndexTile == CurrentLine.Count - 1) return -1;
        return 0;
    }
    public bool AddTileInLine(int x, int y)
    {
        switch (CheckInputTile(x, y))
        {
            case 0:
                CurrentLine.Add(new SimpleTile() { x = x, y = y });
                break;
            case 1:
                break;
            case -1:
                CurrentLine.RemoveAt(CurrentLine.Count - 1);
                break;
        }
        if (CheckInputTile(x, y) == 1)
        {
            return true;
        }
        return false;
    }
    public void LineСompleted()
    {
        //Формируем шаблон для анализа
        List<int> lineTemplate = new List<int>();
        for (int i = 0; i < CurrentLine.Count; i++)
        {
            lineTemplate.Add(GameField[CurrentLine[i].x][CurrentLine[i].y]);
        }


    }
}
public class SimpleTile
{
    public Resource resource;
    public int x;
    public int y;
}
