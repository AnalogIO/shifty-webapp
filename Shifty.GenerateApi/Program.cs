using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.OperationNameGenerators;

namespace Shifty.GenerateAp{
    /// <summary>
    /// Generate ShiftPlanning Api client from OpenApi specification files in the 'OpenApiSpecs' directory
    /// and stores the C# files in 'Shifty\GeneratedApi\'
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Generate ShiftPlanning Api
        /// Expected to be run in the project folder
        /// </summary>
        /// <exception cref="FileNotFoundException">OpenApi specification file could not be found</exception>
        public static async Task Main(string[] args)
        {
            await GenerateApi(apiName: "AnalogCoreV1");
            await GenerateApi(apiName: "AnalogCoreV2");
        }

        private static async Task GenerateApi(string apiName){
            var openApiSpecDirectory = Directory.GetCurrentDirectory() + "/OpenApiSpecs/";
            var outputDirectory = Directory.GetParent(Directory.GetCurrentDirectory()) +
                                  "/Shifty.Api/Generated/";
                                  
            await GenerateApiHelper(
                new CSharpClientGeneratorSettings
                {
                    ClassName = apiName,
                    CSharpGeneratorSettings =
                    {
                        Namespace = $"Shifty.Api.Generated.{apiName}"
                    },
                    UseBaseUrl = false,
                    OperationNameGenerator = new SingleClientFromPathSegmentsOperationNameGenerator()
                }, 
                openApiSpecDirectory + apiName + ".json", 
                outputDirectory + $"{apiName}",
                outputDirectory + $"{apiName}/" + apiName + ".cs"
                );
        }

        private static async Task GenerateApiHelper(CSharpClientGeneratorSettings settings, string inputFile, string outputDirectory, string outputFile)
        {
            CheckFileExists(inputFile);
            
            var document = await OpenApiDocument.FromFileAsync(inputFile);

            var generator = new CSharpClientGenerator(document, settings);
            var code = generator.GenerateFile();

            System.IO.Directory.CreateDirectory(outputDirectory);
            await File.WriteAllTextAsync(outputFile, code);
        }

        private static void CheckFileExists(string inputFile)
        {
            var file = new FileInfo(inputFile);
            if (!file.Exists)
            {
                throw new FileNotFoundException(inputFile);
            }
        }
    }
}