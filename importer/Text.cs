using System;
using System.ComponentModel;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TInput = System.String;
using TOutput = Glee.Assets.Internal.TextAssetRaw; 

namespace Glee.Modules.Importer;



[ContentImporter([".txt, .ini, .csv, .json"], DisplayName = "Simple text importer", DefaultProcessor = nameof(TextAssetProcessor))]
public class TextAssetImpoter : ContentImporter<TInput>
{
    public override TInput Import(string filename, ContentImporterContext context)
    {
        return File.ReadAllText(filename);
    }
}


[ContentProcessor(DisplayName = "Plain text processor")]
public class TextAssetProcessor : ContentProcessor<TInput, TOutput>
{
    public override TOutput Process(TInput input, ContentProcessorContext context)
    {
        return new TOutput() { Value = input};
    }
}


[ContentTypeWriter]
public class TextAssetWriter : ContentTypeWriter<TOutput>
{
    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return typeof(Assets.Internal.TextAssetReader).AssemblyQualifiedName;
    }

    protected override void Write(ContentWriter output, TOutput text)
    {
        output.Write(text.Value);
    }

}