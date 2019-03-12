using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Mcma.Core;
using Mcma.Core.Serialization;
using Mcma.Aws;

namespace Mcma.Tests
{
    public static class SerializationTests
    {
        public static void RunAll()
        {
            foreach (var method in typeof(SerializationTests).GetMethods(BindingFlags.Static | BindingFlags.Public).Where(m => m.Name != nameof(RunAll)))
                method.Invoke(null, new object[0]);
        }

        public static void ToMcmaObject_ShouldDeserializeWorkflowJob()
        {
            var workflowJobJson =
            @"{
                ""@type"": ""WorkflowJob"",
                ""jobProfile"": ""https://w7eijekmuf.execute-api.us-east-2.amazonaws.com/dev/job-profiles/89f432b2-8e90-4d64-9d5d-9c082d6c3574"",
                ""jobInput"": {
                    ""@type"": ""JobParameterBag"",
                    ""metadata"": {
                        ""name"": ""test 1"",
                        ""description"": ""test 1""
                    },
                    ""inputFile"": {
                        ""@type"": ""S3Locator"",
                        ""awsS3Bucket"": ""triskel.mcma.us-east-2.dev.upload"",
                        ""awsS3Key"": ""ShowbizPKG091218__091118178.mp4""
                    }
                }
            }";

            var workflowJob = JObject.Parse(workflowJobJson).ToMcmaObject<WorkflowJob>();

            var serialized = workflowJob.ToMcmaJson();
            
            Console.WriteLine(serialized);
        }
    
        public static void ToMcmaObject_ShouldDeserializeBmEssence()
        {
            var bmEssenceJObj = JObject.Parse(File.ReadAllText("json/BmEssence.json"));

            var bmEssence = bmEssenceJObj.ToMcmaObject<BMEssence>();

            Console.WriteLine(bmEssence.Id);
            Console.WriteLine(bmEssence.Locations[0].Type);
            Console.WriteLine(((S3Locator)bmEssence.Locations[0]).AwsS3Key);
            Console.WriteLine(((S3Locator)bmEssence.Locations[0]).AwsS3Bucket);
        }
    }
}
