using System.Collections.Generic;
using TWWwork.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface ISiteReadQualityRepository
    {
        List<SiteReadQuality> GetSiteReadQualitys(string dateMin,string dateMax);
    }
}