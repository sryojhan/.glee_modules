using System;
using System.ComponentModel;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TInput = System.String;
using TOutput = Glee.Modules.Importer.JsonContentProcessorResult; 

namespace Glee.Modules.Importer;



[ContentImporter([".json"], DisplayName = "Json importer", DefaultProcessor = nameof(JsonProcessor))]
public class JsonImporter : ContentImporter<TInput>
{
    public override TInput Import(string filename, ContentImporterContext context)
    {
        //TODO: json validation
        return File.ReadAllText(filename);
    }
}


public class JsonContentProcessorResult
{
    public string Json { get; set; }
    public string RuntimeIdentifier { get; set; }
}



[ContentProcessor(DisplayName = "Plain text processor")]
public class JsonProcessor : ContentProcessor<TInput, TOutput>
{
    [DisplayName("Minify Json")]
    public bool Minify { get; set; } = false;

     [DisplayName("Runtime Identifier")]
    public string RuntimeIdentifier { get; set; } = "";



    public override TOutput Process(TInput input, ContentProcessorContext context)
    {
        if (string.IsNullOrEmpty(RuntimeIdentifier))
        {
            throw new Exception("No runtime identifier for this json content");            
        }

        return new TOutput() { Json = input, RuntimeIdentifier = RuntimeIdentifier};
    }
}


[ContentTypeWriter]
public class JsonContentTypeWriter : ContentTypeWriter<TOutput>
{
    private string runtimeIdentifier;

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return runtimeIdentifier;
    }

    protected override void Write(ContentWriter output, TOutput value)
    {
        runtimeIdentifier = value.RuntimeIdentifier;
        output.Write(value.Json);
    }

}