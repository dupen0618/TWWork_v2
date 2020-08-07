using System.Collections.Generic;
using TWWwork.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface IOBTRFIDCountModelRepository
    {
        List<OBTRFIDCountModel> GetOBTRFIDCountModels(string dateMin, string dateMax);
    }
}