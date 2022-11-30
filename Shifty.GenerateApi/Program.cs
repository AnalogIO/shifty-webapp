using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.OperationNameGenerators;

namespace Shifty.GenerateApi
{
    /// <summary>
    /// Generate ShiftPlanning Api client from OpenApi specification files in the 'OpenApiSpecs' directory
    /// and stores the C# files in 'Shifty\GeneratedApi\'
    /// </summary>
    public static class Program
    {
        private const string ShiftPlanningV1 = "ShiftPlanningV1";
        
        /// <summary>
        /// Generate ShiftPlanning Api
        /// Expected to be run in the project folder
        /// </summary>
        /// <exception cref="FileNotFoundException">OpenApi specification file could not be found</exception>
        public static async Task Main(string[] args)
        {
            var openApiSpecDirectory = Directory.GetCurrentDirectory() + "/OpenApiSpecs/";
            var outputDirectory = Directory.GetParent(Directory.GetCurrentDirectory()) +
                                  "/Shifty.Api/Generated/";

            await GenerateApi(
                new CSharpClientGeneratorSettings
                {
                    ClassName = ShiftPlanningV1,
                    CSharpGeneratorSettings =
                    {
                        Namespace = $"Shifty.Api.Generated.{ShiftPlanningV1}"
                    },
                    UseBaseUrl = false,
                    OperationNameGenerator = new SingleClientFromPathSegmentsOperationNameGenerator()
                }, 
                openApiSpecDirectory + ShiftPlanningV1 + ".json", 
                outputDirectory + $"{ShiftPlanningV1}/" + ShiftPlanningV1 + ".cs"
                );
        }

        private static async Task GenerateApi(CSharpClientGeneratorSettings settings, string inputFile, string outputFile)
        {
            CheckFileExists(inputFile);
            
            var document = await OpenApiDocument.FromFileAsync(inputFile);

            var generator = new CSharpClientGenerator(document, settings);
            var code = generator.GenerateFile();

            await File.WriteAllTextAsync(outputFile, code);
        }

        private static void CheckFileExists(string inputFile)
        {
            var file = new FileInfo(inputFile);
            if (!file.Exists)
            {
                throw new FileNotFoundException(ShiftPlanningV1);
            }
        }
    }
}