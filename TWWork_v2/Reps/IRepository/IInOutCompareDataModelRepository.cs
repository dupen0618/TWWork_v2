using System.Collections.Generic;
using TWWwork.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface IInOutCompareDataModelRepository
    {
        List<InOutCompareDataModel> GetInOutCompareDataModels(string dateMin,string dateMax,string deviceName);
    }
}