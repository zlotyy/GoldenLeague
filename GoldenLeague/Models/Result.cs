using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.Models
{
    public class Result<T>
    {
        public Result() { }

        public Result(T data)
        {
            Data = data;
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
