using System.Collections.Generic;
using TWWwork.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface ISuccessRateModelRepository
    {
        List<SuccessRateModel> GetSuccessRateModels(string dateMin,string dateMax);
    }
}