using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class LineTemplate
{

    public int CheckPrimary(string input, string inputtmp)
    {
        List<string> inputVector = new List<string>(input.Split('/'));
        List<string> templateVector = new List<string>(inputtmp.Split('/'));
        Dictionary<string, int> OutDataBase = new Dictionary<string, int>();
        int MaxChain = 0; //Максимальное вхождение шаблонов подряд
        for (int i = 0; i < templateVector.Count; i++)
        {
            OutDataBase.Add(templateVector[i], Regex.Matches(input, templateVector[i]).Count);
            MaxChain += OutDataBase[templateVector[i]];
        }
        return MaxChain;
    }

    public void Check2()
    {

        string INPUT = "ore/cup/ore/ore/cup/oil";
        string INPUTTMP = "ore/";

        List<string> inputVector = new List<string>(INPUT.Split('/'));
        List<string> templateVector = new List<string>(INPUTTMP.Split('/'));
        //Dictionary<string, string> templateBase = new Dictionary<string, string>();
        int MaxChain = 0; //Максимальное вхождение шаблонов подряд
        for (int i = 0, k = 0; i < inputVector.Count; i++)
        {
            bool OK = false;
            if (inputVector[i] == templateVector[k] || templateVector[k].Contains("ANY"))
            {
                OK = true;
            }
            else
            if (templateVector[k].Contains("TMP"))
            {
                string ChengingTemplate = templateVector[k];
                for (int j = k; j < templateVector.Count; j++)
                {
                    if (templateVector[j]== ChengingTemplate) templateVector[k] = inputVector[i];
                }
                OK = true;
            }
            k++;
            if (k > templateVector.Count - 1 && OK) { k = 0; MaxChain++; } 
            if (!OK) break;
        }
     }
}
public class IndexTemplate
{
    private class IndexItem
    {
        public string Data = string.Empty;
        public bool canByEnd = false; //может быть окончанием
        public List<IndexItem> listNexts;
        public IndexItem parent;
        public IndexItem()
        {
            parent = null;
            listNexts = new List<IndexItem>();
        }
    }


    private IndexItem orgin;

    public IndexTemplate()
    {
        orgin = new IndexItem();
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


    private int Cicles = 0; //Количество цикллов шаблона
    private int TemplatePosition = 0;
    private IndexItem CurrentItem = null; //Текущий элемент в индексе, на нём продолжится поиск
    private List<IndexItem> Template = null; //лист для шаблона
    private bool TemplateFix = false;//Шаблон найден ищем продолжение по шаблону

    /// <summary>
    /// Смещается по индексу дальше если это возможно, если не возможно возвращается к началу и пробует двигаться 
    /// по тому же шаблону добавляя при этоv Цикл
    /// </summary>
    /// <param name="nextResource"></param>
    /// <returns>если движение вперёд возможно то true</returns>
    public bool Next(string nextResource)
    {
        if (TemplateFix) return NextInTemplate(nextResource);
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
            //Проверяем стукуемость с оставшимся хвостом
            bool tailOK = true;
            TemplatePosition = (Template.Count-1)- index;
            for (int i = index+1; i < Template.Count; i++)
            {
                if (Template[i].Data != Template[index + 1 - i].Data) tailOK = false;
            }
            if (tailOK)
            {
                Template.RemoveRange(TemplatePosition+1, Template.Count - 1 - index);//Сформировали шаблон
                if (Template[TemplatePosition].Data == nextResource)
                {
                    TemplateFix = true;  //фиксируем шаблон
                    Cicles++;
                    TemplatePosition++;
                    if (TemplatePosition > Template.Count - 1) { TemplatePosition = 0; Cicles++; }
                        return true;
                }
            }
        }
        return false;
    }
    private bool NextInTemplate(string nextResource)
    {
        if (Template[TemplatePosition].Data == nextResource)
        {
            TemplatePosition++;
            if (TemplatePosition > Template.Count - 1)
            {
                TemplatePosition = 0;
                Cicles++;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Шаг назад
    /// </summary>
    /// <returns>Если это не начало то true</returns>
        //public bool Preverios()
        //{

        //}
        /// <summary>
        /// Перед новой линией
        /// </summary>
    public void StartNewLine()
    {
        CurrentItem = orgin;
        Template = new List<IndexItem>();
        Cicles = 0;
        TemplateFix = false;
    }
    /// <summary>
    /// для сбора информации перед сбросом.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    //public string EndLine(out Cicles)
    //{

    //}
}

public enum TemplateType
{

}
