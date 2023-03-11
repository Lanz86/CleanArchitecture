using FastEndpoints.ClientGen;

namespace CleanArchitecture.WebUI.Commons;

public static class GenerateClients
{
    public static async Task GenerateClientsAndExitAsync(this WebApplication app)
    {
        await app.GenerateClientsAndExitAsync(
                documentName: "v1", //must match doc name above
                destinationPath: "ClientApp/src/app/",
                csSettings: null,
                tsSettings: c =>
                {
                    c.ClassName = "webapiclient";
                    c.TypeScriptGeneratorSettings.ModuleName = string.Empty;
                    c.TypeScriptGeneratorSettings.Namespace = string.Empty;
                    c.TypeScriptGeneratorSettings.TypeScriptVersion = 4.3M;
                    c.Template = NSwag.CodeGeneration.TypeScript.TypeScriptTemplate.Angular;
                    c.PromiseType = NSwag.CodeGeneration.TypeScript.PromiseType.Promise;
                    c.HttpClass = NSwag.CodeGeneration.TypeScript.HttpClass.HttpClient;
                    c.WithCredentials = false;
                    c.UseSingletonProvider = true;
                    c.InjectionTokenType = NSwag.CodeGeneration.TypeScript.InjectionTokenType.InjectionToken;
                    c.RxJsVersion = 7.0M;
                    c.TypeScriptGeneratorSettings.DateTimeType = NJsonSchema.CodeGeneration.TypeScript.TypeScriptDateTimeType.Date;
                    c.TypeScriptGeneratorSettings.NullValue = NJsonSchema.CodeGeneration.TypeScript.TypeScriptNullValue.Undefined;
                    c.GenerateClientClasses = true;
                    c.GenerateClientInterfaces = true;
                    c.GenerateOptionalParameters = false;
                    c.TypeScriptGeneratorSettings.ExportTypes = true;
                    c.WrapDtoExceptions = false;
                    c.ExceptionClass = "SwaggerException";
                    c.ClientBaseClass = null;

                    //"wrapResponses": false,
                    //"wrapResponseMethods": [],
                    c.GenerateResponseClasses = true;
                    c.ResponseClass = "SwaggerResponse";
                    c.ProtectedMethods = new string[0];
                    c.ConfigurationClass = null;
                    c.UseTransformOptionsMethod = false;
                    c.UseTransformResultMethod = false;
                    c.GenerateDtoTypes = true;

                    //"operationGenerationMode": "MultipleClientsFromOperationId",
                    c.TypeScriptGeneratorSettings.MarkOptionalProperties = true;
                    c.TypeScriptGeneratorSettings.GenerateCloneMethod = false;
                    c.TypeScriptGeneratorSettings.TypeStyle = NJsonSchema.CodeGeneration.TypeScript.TypeScriptTypeStyle.Class;
                    c.TypeScriptGeneratorSettings.EnumStyle = NJsonSchema.CodeGeneration.TypeScript.TypeScriptEnumStyle.Enum;
                    c.TypeScriptGeneratorSettings.UseLeafType = false;
                    c.TypeScriptGeneratorSettings.ClassTypes = new string[0];
                    c.TypeScriptGeneratorSettings.ExtendedClasses = new string[0];
                    //c.TypeScriptGeneratorSettings.ExtensionCode = null;
                    c.TypeScriptGeneratorSettings.GenerateDefaultValues = true;
                    c.TypeScriptGeneratorSettings.ExcludedTypeNames = new string[0];
                    c.ExcludedParameterNames = new string[0];
                    c.TypeScriptGeneratorSettings.HandleReferences = false;
                    c.TypeScriptGeneratorSettings.GenerateConstructorInterface = true;
                    c.TypeScriptGeneratorSettings.ConvertConstructorInterfaceData = false;
                    c.ImportRequiredTypes = true;
                    c.UseGetBaseUrlMethod = false;
                    c.BaseUrlTokenName = "API_BASE_URL";
                    c.QueryNullValue = string.Empty;
                    c.UseAbortSignal = false;
                    c.TypeScriptGeneratorSettings.InlineNamedDictionaries = false;
                    c.TypeScriptGeneratorSettings.InlineNamedAny = false;
                    c.IncludeHttpContext = false;
                    c.TypeScriptGeneratorSettings.TemplateDirectory = string.Empty;
                });
    }
}
