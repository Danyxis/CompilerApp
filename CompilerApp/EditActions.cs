using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    public static class EditActions // Класс для реализации пункта меню "Правка"
    {
        public static void Undo(MainMenuForm form) // Отмена предыдущего изменения в тексте
        {
            if (form.GetInputArea().CanUndo)
                form.GetInputArea().Undo();
        }

        public static void Redo(MainMenuForm form) // Повторение предыдущего изменения в тексте
        {
            if (form.GetInputArea().CanRedo)
                form.GetInputArea().Redo();
        }

        public static void Cut(MainMenuForm form) // Вырезать текстовый фрагмент
        {
            form.GetInputArea().Cut();
        }

        public static void Copy(MainMenuForm form) // Копировать текстовый фрагмент
        {
            form.GetInputArea().Copy();
        }

        public static void Paste(MainMenuForm form) // Вставить текстовый фрагмент
        {
            form.GetInputArea().Paste();
        }

        public static void Delete(MainMenuForm form) // Удалить текстовый фрагмент
        {
            var inputArea = form.GetInputArea();
            int selectionStart = inputArea.SelectionStart;
            inputArea.SelectedText = string.Empty;
            inputArea.SelectionStart = selectionStart;
        }

        public static void SelectAll(MainMenuForm form) // Выделить весь текст
        {
            form.GetInputArea().SelectAll();
        }
    }
}
