using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerApp
{
    public partial class ReferenceForm : Form // Форма окна "Вызов справки"
    {
        public ReferenceForm()
        {
            InitializeComponent();
            LoadHelpText();
        }

        private void LoadHelpText() // Метод загрузки текста справки
        {
            helpTextBox.Clear(); // Очищаем текст

            string title = "Руководство пользователя приложения «Компилятор»\n\n";

            // Заголовок справки
            AppendText(helpTextBox, title, 
                new Font("Segoe UI", 12, FontStyle.Bold), Color.DarkBlue);

            // Выравниваем заголовок по центру
            helpTextBox.Select(0, title.Length);
            helpTextBox.SelectionAlignment = HorizontalAlignment.Center;

            // Пункт меню «Файл»
            AppendText(helpTextBox, "1. Пункт меню «Файл»:\n", 
                new Font("Segoe UI", 11, FontStyle.Bold), Color.Black);
            AppendIndentedText(helpTextBox,
                "• Создать — очистка рабочей области для создания нового документа. " +
                "Предыдущие изменения сохраняются при необходимости.\n" +
                "• Открыть — загрузка текста из выбранного файла. " +
                "Перед открытием программа предложит сохранить изменения, если они есть.\n" +
                "• Сохранить — сохранение текущего документа. " +
                "Если файл ещё не был сохранён, будет предложено выбрать путь.\n" +
                "• Сохранить как — сохранение документа с выбором имени файла и его расположения.\n" +
                "• Выход — завершение работы программы. " +
                "При наличии несохранённых изменений будет предложено их сохранить.\n\n", 20);

            // Пункт меню «Правка»
            AppendText(helpTextBox, "2. Пункт меню «Правка»:\n", 
                new Font("Segoe UI", 11, FontStyle.Bold), Color.Black);
            AppendIndentedText(helpTextBox,
                "• Отменить — отмена последнего действия в области редактирования текста.\n" +
                "• Повторить — повтор последнего отменённого действия.\n" +
                "• Вырезать — вырезание выделенного фрагмента текста в буфер обмена.\n" +
                "• Копировать — копирование выделенного фрагмента текста в буфер обмена.\n" +
                "• Вставить — вставка текста из буфера обмена.\n" +
                "• Удалить — удаление выделенного фрагмента текста.\n" +
                "• Выделить всё — выделение всего текста в области редактирования.\n\n", 20);

            // Пункт меню «Справка»
            AppendText(helpTextBox, "3. Пункт меню «Справка»:\n", 
                new Font("Segoe UI", 11, FontStyle.Bold), Color.Black);
            AppendIndentedText(helpTextBox,
                "• Вызов справки — отображение данного окна с описанием работы программы.\n" +
                "• О программе — информация о версии программы и разработчике.\n\n", 20);

            // Прокрутка к началу текста
            helpTextBox.SelectionStart = 0;
            helpTextBox.ScrollToCaret();
        }

        private void AppendText(RichTextBox richTextBox, string text, Font font, Color color) // Параметры текста
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionFont = font;
            richTextBox.SelectionColor = color;
            richTextBox.AppendText(text);
            richTextBox.SelectionFont = richTextBox.Font;
            richTextBox.SelectionColor = richTextBox.ForeColor;
        }

        private void AppendIndentedText(RichTextBox richTextBox, string text, int indent) // Параметры текста
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionFont = richTextBox.Font;
            richTextBox.SelectionIndent = indent; // Отступ для пунктов
            richTextBox.AppendText(text);
            richTextBox.SelectionIndent = 0; // Сброс отступа
        }
    }
}
