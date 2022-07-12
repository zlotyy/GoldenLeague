namespace GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Base
{
    public class BaseResponseModel
    {
        public string Message { get; set; } // message if error 500
        public int Results { get; set; } // results count
        public PagingModel Paging { get; set; }
    }
}
