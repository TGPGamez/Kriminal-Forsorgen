using System.Collections.Generic;

public interface IDataGet<T> 
{
    T GetData(Dictionary<string, object> parameters);
}
