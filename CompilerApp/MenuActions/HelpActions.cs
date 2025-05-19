using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    public static class HelpActions // Класс для реализации пункта меню "Справка"
    {
        public static void ShowHelp() // Вызов HTML-файла справки
        {
            EmbeddedHtmlViewer.OpenEmbeddedHtml("CompilerApp.Resources.Help.help.html");
        }

        public static void ShowAboutProgram() // Вызов HTML-файла "О программе"
        {
            EmbeddedHtmlViewer.OpenEmbeddedHtml("CompilerApp.Resources.Help.about_program.html");
        }
    }
}
