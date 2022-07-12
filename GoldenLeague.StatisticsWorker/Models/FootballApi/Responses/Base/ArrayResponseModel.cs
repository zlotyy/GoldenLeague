using System.Collections.Generic;

namespace GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Base
{
    public class ArrayResponseModel<T> : BaseResponseModel
    {
        public IEnumerable<T> Response { get; set; }
    }
}
