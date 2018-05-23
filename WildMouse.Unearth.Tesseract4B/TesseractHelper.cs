using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace WildMouse.Unearth.Tesseract4B
{
    public static class TesseractHelper
    {
        [HandleProcessCorruptedStateExceptions]
        public static string OCRImageWithTesseract(Image theImage)
        {
            if (theImage is Bitmap)
            {
                var bmp = (Bitmap)theImage;
                return OCRImageWithTesseract(bmp);
            }
            else
            {
                throw new ApplicationException("OCRImageWithTesseract: Image must be Bitmap");
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public static string OCRImageWithTesseract(Bitmap theBmp)
        {
            var ocrText = string.Empty;
            try
            {
                using (var engine = new Tesseract.TesseractEngine(@".\tessdata\", "eng", Tesseract.EngineMode.LstmOnly))
                {
                    var pix = Tesseract.PixConverter.ToPix(theBmp);
                    using (var tessPage = engine.Process(pix))
                    {
                        ocrText = tessPage.GetText();
                    }
                    using (var tessPage = engine.Process(pix, Tesseract.PageSegMode.Auto))
                    {
                        ocrText = tessPage.GetText().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is AccessViolationException || ex is InvalidOperationException)
                {
                    Trace.TraceError("Tesseract Error: " + ex.Message);
                }
                else
                {
                    throw;
                }
            }
            return ocrText;
        }
    }
}
