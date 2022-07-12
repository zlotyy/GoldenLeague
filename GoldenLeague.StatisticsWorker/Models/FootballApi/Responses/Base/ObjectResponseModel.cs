namespace GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Base
{
    public class ObjectResponseModel<T> : BaseResponseModel
    {
        public T Response { get; set; }
    }
}
