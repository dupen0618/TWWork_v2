using System.Collections.Generic;
using TWWork_v2.Models;

namespace TWWork_v2.Reps.IRepository
{
    public interface IRetrospectiveInquiryRepository
    {
        List<RetrospectiveInquiry> GetRetrospectiveInquiries(
            string dateMin,string dateMax,string device,string pushOrPop );
    }
}