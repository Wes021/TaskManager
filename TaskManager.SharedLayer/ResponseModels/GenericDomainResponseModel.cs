using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.ResponseModels
{
    public class GenericDomainResponseModel<T>
    {
        public bool Succeeded { get; init; }
        public string? Error { get; init; }
        public T? Data { get; init; }

        public static GenericDomainResponseModel<T> Success(T data) =>
            new() { Succeeded = true, Data = data };

        public static GenericDomainResponseModel<T> Fail(string error) =>
            new() { Succeeded = false, Error = error };
    }
}
