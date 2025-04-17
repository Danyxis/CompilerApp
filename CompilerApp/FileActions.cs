using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    public static class FileActions // Класс для реализации пункта меню "Файл"
    {
        public static void CreateFile(MainMenuForm form) // Создание файла
        {
            if (form.IsTextChanged() && !AskToSaveChanges(form))
                return; // Отмена действия

            // Очищаем рабочие области
            form.GetInputArea().Clear();
            form.GetOutputTable().Rows.Clear();

            form.SetCurrentFilePath(null); // Сбрасываем путь к файлу
            form.SetTextChanged(false);    // Сбрасываем флаг изменений

            form.UpdateStatus("Создан новый файл"); // Обновление строки состояния
        }

        public static void OpenFile(MainMenuForm form) // Открытие файла
        {
            if (form.IsTextChanged() && !AskToSaveChanges(form))
                return; // Отмена действия

            using OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                form.GetInputArea().Text = File.ReadAllText(openFileDialog.FileName);
                form.SetCurrentFilePath(openFileDialog.FileName); // Сохраняем путь к файлу
                form.SetTextChanged(false); // Сброс изменений, так как только что открыли файл

                form.UpdateStatus($"Открыт файл: {Path.GetFileName(openFileDialog.FileName)}"); // Обновление строки состояния
            }
        }

        public static bool SaveFile(MainMenuForm form) // Сохранение файла
        {
            if (form.GetCurrentFilePath() == null)
            {
                return SaveFileAs(form); // Если файл ещё не сохранён, вызываем "Сохранить как"
            }

            // Если файл уже существует – просто сохраняем
            File.WriteAllText(form.GetCurrentFilePath(), form.GetInputArea().Text);
            form.SetTextChanged(false); // Сбрасываем флаг изменений

            form.UpdateStatus("Файл сохранён"); // Обновление строки состояния

            return true;
        }

        public static bool SaveFileAs(MainMenuForm form) // Сохранение файла как
        {
            using SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, form.GetInputArea().Text);
                form.SetCurrentFilePath(saveFileDialog.FileName); // Сохраняем путь к файлу
                form.SetTextChanged(false); // Сбрасываем флаг изменений

                form.UpdateStatus("Файл сохранён"); // Обновление строки состояния

                return true;
            }
            return false;
        }

        public static bool AskToSaveChanges(MainMenuForm form) // Запрос на сохранение изменений в тексте
        {
            DialogResult result = MessageBox.Show(
                "Сохранить изменения в текущем файле?",
                "Несохраненные изменения",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                return SaveFile(form); // Пытаемся сохранить файл
            }

            return result != DialogResult.Cancel; // Продолжаем только если пользователь не отменил
        }

        public static void ExitApplication(MainMenuForm form) // Выход из приложения
        {
            if (form.IsTextChanged() && !AskToSaveChanges(form))
                return; // Пользователь отменил выход

            form.UpdateStatus("Выход из программы..."); // Обновление строки состояния

            Application.Exit();
        }
    }
}
