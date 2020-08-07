using System.Collections.Generic;
using TWWwork.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface IDataChangeModelRepository
    {
        List<DataChangeModel> GetDataChangeModels(string dateMin,string dateMax);
    }
}