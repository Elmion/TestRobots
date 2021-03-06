﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class LineTemplate
{
    //Исходящий блок
    public  int Cicles = 0; //Количество циклов шаблона
    public System.Action TemplateAction = null;
    public List<string> Tail = null;

    private int TemplatePosition = 0;
    private int MissPositons = 0;
    private IndexItem CurrentItem = null; //Текущий элемент в индексе, на нём продолжится поиск
    private List<IndexItem> Template = null; //лист для шаблона
    private List<string> History = null;
    LineState state = LineState.StartNewLine;

    private IndexItem orgin;

    public LineTemplate()
    {
        orgin = new IndexItem();
        History = new List<string>();
    }

    public bool Next(string nextResource)
    {
        History.Add(nextResource);
        switch (state)
        {
            case LineState.StartNewLine:
                CurrentItem = orgin;
                Template = new List<IndexItem>();
                Cicles = 0;
                MissPositons = 0;
                state = LineState.TemplateSearch;
                History.Clear();
                return Next(nextResource); 
            case LineState.TemplateSearch:
                int index = CurrentItem.listNexts.FindIndex(x => x.Data == nextResource);
                if (index != -1) //нашли сдвигаеся по индексу
                {
                    CurrentItem = CurrentItem.listNexts[index];
                    Template.Add(CurrentItem);
                    return true;
                }
                else
                {
                    // ищем самый большой подходящий шаблон
                    index = Template.FindLastIndex(x => x.canByEnd == true);
                    List<IndexItem> Tail = Template.GetRange(index+1, (Template.Count-1) - index);

                    if (index == -1) // не нашли не одного шаблона
                    {
                        state = LineState.Miss;
                        Cicles = 0;
                        MissPositons = 1;// последняя введёная позиция ошибочна
                        return false;
                    }
                    //Проверяем стукуемость с оставшимся хвостом
                    bool tailOK = true;
                    int counterTmp = (Template.Count - 1) - index; //кэшируем 
                    for (int i = 0; i <= counterTmp; i++)
                    {
                        if((i <counterTmp && Template[i].Data != Tail[i].Data) || (i == (counterTmp) && (Template[0].Data != nextResource) ))
                          {
                            tailOK = false;
                            break;
                          } 
                    }
                    Template.RemoveRange(index + 1, (Template.Count - 1) - index);//Сформировали шаблон, хоть какой то
                    if (tailOK)
                        {
                             //шаблон найден  и хвост соотвествтует
                             state = LineState.SearchContinue;                                         
                             Cicles = (int)System.Math.Floor((float)((Tail.Count+1)/ Template.Count)) +1;//(хвост + последний введёный символ )/ длина шаблона + сам шаблон                  
                             TemplatePosition = ((Tail.Count + 1) + Template.Count) - Cicles * Template.Count;//Полная длина -  Кол-во позиций с полными циклами
                            // return  Next(nextResource);
                        }
                        else
                        {
                              //нашли шаблон но последняя в хвосте не соотвествует;
                              state = LineState.Miss;
                              Cicles = (int)System.Math.Floor((float)((Tail.Count + 1) / Template.Count)) + 1;
                              MissPositons = 1;
                        }
                 }
                return false;
            case LineState.SearchContinue:
                if (Template[TemplatePosition].Data == nextResource )
                {
                    TemplatePosition++;
                    if (TemplatePosition > Template.Count - 1)
                    {
                        TemplatePosition = 0;
                        Cicles++;
                    }
                    return true;
                }
                state = LineState.Miss;
                return Next(nextResource);
            case LineState.Miss:
                MissPositons++;
                break;
        }
        return false;
    }
    public bool Preverios()
    {
        if (History.Count == 0) return false;
        History.RemoveAt(History.Count - 1);//Удалили последний
        string[] buff = new string[History.Count];//В буффер, история затрётся.
        History.CopyTo(buff);
        state = LineState.StartNewLine;
        for (int i = 0; i < buff.Length ; i++) Next(buff[i]);
        return true;
    }

    public void AddTemplate(string template)
    {
        List<string> s = new List<string>(template.Split('/'));
        IndexItem next = orgin;
        for (int i = 0; i < s.Count; i++)
        {
            int numTMP = next.listNexts.FindIndex(x => x.Data == s[i]);
            if (numTMP == -1)
            {
                IndexItem newBranch = new IndexItem() { Data = s[i], parent = next };
                next.listNexts.Add(newBranch);
                next = newBranch;
            }
            else
            {
                next = next.listNexts[numTMP];
            }
        }
        next.canByEnd = true;//может быть окончанием
    }
    public void EndLine()
    {
        History.Clear();
        state = LineState.StartNewLine;
    }
    private class IndexItem
    {
        public string Data = string.Empty;
        public bool canByEnd = false; //может быть окончанием
        public System.Action<int> EndAction = null; 
        public List<IndexItem> listNexts;
        public IndexItem parent;
        public IndexItem()
        {
            parent = null;
            listNexts = new List<IndexItem>();
        }
    }
    private enum LineState
    {
        StartNewLine,
        TemplateSearch,
        SearchContinue,
        EndLine,
        Miss
    }
}


