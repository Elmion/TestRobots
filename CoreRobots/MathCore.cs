using System.Collections;
using System.Collections.Generic;
using System;

public class MathCore  {

    public static readonly MathCore Instance = new MathCore();

    private MathCore()
    {
        Resource.CreateNewResurce("Ore");
        Resource.CreateNewResurce("Cuper");
        Resource.CreateNewResurce("Plastic");
        Resource.CreateNewResurce("Coal");
        Resource.CreateNewResurce("Oil");

        ResourcesStorage storage1 = new ResourcesStorage();
        storage1.AddNewResource("Cuper",100);
        storage1.AddNewResource("Ore",100);
        storage1.AddNewResource("Plastic", 100);

        ResourcesStorage storage2 = new ResourcesStorage();
        storage2.AddNewResource("Oil",100); 
        storage1.AddNewResource("Ore", 100); 
        storage2.AddNewResource("Coal",100); 
        //NewGame(6, 6, storage1);

    }

    public void Restart()
    {

    }
    
}



