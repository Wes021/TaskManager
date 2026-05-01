using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.ResponseModels
{
    public class DomainResponseModel
    {
        public bool Succeeded { get; init; }
        public string Error { get; init; }

        public static DomainResponseModel Success() =>
            new() { Succeeded = true };

        public static DomainResponseModel Fail(string error) =>
            new() { Succeeded = false, Error = error };
    }
}
