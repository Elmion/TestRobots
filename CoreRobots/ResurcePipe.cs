using System.Collections;
using System.Collections.Generic;

public class ResourcesStorage  {
    private static System.Random rnd = new System.Random();
    public List<Resource> Storage { get; private set; }
    public ResourcesStorage()
    {
        Storage = new List<Resource>();
    }
    public ResourcesStorage(string[] NamesResources)
    {
        Storage = new List<Resource>();
        for (int i = 0; i < NamesResources.Length; i++)
        {
            AddNewResource(NamesResources[i]);
        }
    }
    public void Clear()
    {
        Storage.Clear();
    }
    public bool GetSomeResource(int id,int count)
    {
        Resource r = Storage.Find(x => x.Id == id);
        if (r != null)
        {
            if (r.Count == int.MinValue) return true;
            if (r.Count >= count) { r.Count -= count; return true; }
        }
        return false;
    }
    /// <summary>
    /// Возвращает еденицу рандомного ресурса из банка
    /// </summary>
    public Resource GetOneRandomResource()
    {
        List<Resource> NotEmptyResources = Storage.FindAll(x => (x.Count > 0 || x.Count == int.MinValue));
        if(NotEmptyResources != null)
        {
            Resource OUT = NotEmptyResources[rnd.Next(0, NotEmptyResources.Count)];
            GetSomeResource(OUT.Id, 1);
            return OUT;
        }
        return null;
    }
    /// <summary>
    /// Добавляет нужное количество ресурсов
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool AddSomeResource(int id,int count)
    {
        Resource r = Storage.Find(x => x.Id == id);
        if (r != null)
        {
            if (r.Count == int.MinValue) return true;
            r.Count += count;
            return true;
        }
        return false;
    }
    public bool AddSomeResource(string Name, int count)
    {
        Resource r = Storage.Find(x => x.Name == Name);
        if (r != null)
        {
            if (r.Count == int.MinValue) return true;
            r.Count += count;
            return true;
        }
        return false;
    }
    public Resource AddNewResource(string s)
    {
        Resource r = Resource.PoolResources.Find(x => x.Name == s);
        if(r != null && Storage.Find(x=> x.Name == r.Name) == null) 
        {
            Storage.Add(Resource.GetCopyBy(r));
            return r;
        }
        return null;
    }
    public Resource AddNewResource(string s,int Count)
    {
        Resource rOriginal = Resource.PoolResources.Find(x => x.Name == s);
        if (rOriginal != null && Storage.Find(x => x.Name == rOriginal.Name) == null)
        {
            Resource rCopy = Resource.GetCopyBy(rOriginal);
            Storage.Add(rCopy);
            rCopy.Count = Count;
            return rCopy;
        }
        return null;
    }
    public bool LoadResources()
    {
        return true;
    }
    public bool SaveResources()
    {
        return true;
    }
}
public class Resource
{
    private static int IdCounter;
    public int Id { get; private set; }
    public string Name {get; private set; }
    public static readonly List<Resource> PoolResources = new List<Resource>();
    public int Count;
    
    public static bool CreateNewResurce(string NameResource)
    {
        if (PoolResources.Find(x => x.Name == NameResource) != null) return false;
        Resource r = new Resource
        {
            Id = IdCounter++,
            Name = NameResource,
            Count = 0
        };
        PoolResources.Add(r);
        return true;
    }
    public static Resource GetCopyBy(Resource copy)
    {
        Resource r = new Resource()
        {
            Id = copy.Id,
            Name = copy.Name,
            Count = 0
        };
        return r;
    }
    private Resource() {}
}

