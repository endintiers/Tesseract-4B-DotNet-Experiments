using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WildMouse.Unearth.Tesseract4B;

namespace WildMouse.Unearth.TestAppFW
{
    class Program
    {
        static void Main(string[] args)
        {
            var jabberwock = Image.FromFile(Environment.CurrentDirectory + @"\Images\Jabberwock.JPG");
            var text = TesseractHelper.OCRImageWithTesseract(jabberwock);
            Console.WriteLine(text);
            Console.ReadLine();
        }
    }
}
