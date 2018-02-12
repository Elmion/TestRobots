using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class LineTemplate
{

    public void Check()
    {
        string INPUT = "ore/cup/ore/ore/cup/oil";
        string INPUTTMP = "ore/TMP0/ANY";

        //готовим инпут
        List<string> s = new List<string>(INPUT.Split('/'));
        Dictionary<string, string> dl = new Dictionary<string, string>();
        int counter = 0;
        for (int i = 0; i < s.Count; i++)
        {
            if (!dl.ContainsValue(s[i]))
            {
                dl.Add("T" + counter, s[i]);
                INPUT = INPUT.Replace(s[i], "T" + counter.ToString().PadLeft(2, '0'));
                counter++;
            }
        }
        //готовим инпуттмп
        List<string> s1 = new List<string>(INPUTTMP.Split('/'));
        Dictionary<string, string> dlt = new Dictionary<string, string>();
        counter = 0;
        for (int i = 0; i < s1.Count; i++)
        {
            if (!dlt.ContainsValue(s1[i]))
            {
                dlt.Add("T" + counter, s1[i]);
                if (s1[i] == "ANY")
                {
                    INPUTTMP = INPUTTMP.Replace(s1[i], "(...)");
                }
                else
                {
                    INPUTTMP = INPUTTMP.Replace(s1[i], "T" + counter.ToString().PadLeft(2, '0'));
                }
                counter++;
            }
        }

        MatchCollection c = Regex.Matches(INPUT, INPUTTMP);

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
