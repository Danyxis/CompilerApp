using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    public static class HelpActions // Класс для реализации пункта меню "Справка"
    {
        public static void ShowHelp() // Вызов окна справки - руководства пользователя
        {
            ReferenceForm referenceForm = new ReferenceForm();
            referenceForm.ShowDialog();
        }

        public static void ShowAboutProgram() // Вызов окна "О программе"
        {
            AboutProgramForm aboutProgramForm = new AboutProgramForm();
            aboutProgramForm.ShowDialog();
        }
    }
}
