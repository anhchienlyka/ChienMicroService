using Contracts.Commons.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Infrastructure.Commons;

public class SerializeService : ISerializeService
{
    public T Deserialize<T>(string text)
    {
        throw new NotImplementedException();
    }

    public string Serialize<T>(T obj)
    {
        throw new NotImplementedException();
    }

    public string Serialize<T>(T obj, Type type)
    {
        throw new NotImplementedException();
    }
}