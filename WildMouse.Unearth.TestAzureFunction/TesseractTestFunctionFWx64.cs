using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;
using WildMouse.Unearth.Tesseract4B;

namespace WildMouse.Unearth.TestAzureFunction
{
    public static class TesseractTestFunctionFWx64
    {
        [FunctionName("TesseractTestFunctionFWx64")]
        [StorageAccount("IngestStore")]
        public static async Task Run(
            [BlobTrigger("testimages/{name}")]Stream imageToOCR,
            string name,
            TraceWriter log)
        { 
            log.Info($"Blob Trigger fired for {name}");
            var jabberwock = Image.FromStream(imageToOCR);
            var text = TesseractHelper.OCRImageWithTesseract(jabberwock);
            log.Info($"OCR Text: {text}");

        }
    }
}
