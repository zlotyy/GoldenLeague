using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.TransportModels.Common
{
    public class Result<T>
    {
        public Result()
        {
            Errors = new List<string>();
        }

        public Result(T data)
        {
            Data = data;
            Errors = new List<string>();
        }

        public Result(string error)
        {
            Errors = new List<string> { error };
        }

        public Result(List<string> errors)
        {
            Errors = errors;
        }

        public Result(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool Success => !Errors.Any();
    }
}
