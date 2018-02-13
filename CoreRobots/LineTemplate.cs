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
        string INPUTTMP = "ore";

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

public enum TemplateType
{

}
