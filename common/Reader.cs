using System.Text.Json;
using Microsoft.Xna.Framework.Content;



namespace Glee.Modules.Importer;

public class JsonContentTypeReader<T> : ContentTypeReader<T>
{
    protected override T Read(ContentReader input, T existingInstance)
    {
        string json = input.ReadString();
        T result = JsonSerializer.Deserialize<T>(json);
        return result;
    }
}
