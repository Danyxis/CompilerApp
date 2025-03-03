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
    public partial class AboutProgramForm : Form // Форма окна "О программе"
    {
        public AboutProgramForm()
        {
            InitializeComponent();
            LoadAboutText();
        }

        private void LoadAboutText() // Метод загрузки текста о программе
        {
            aboutTextBox.Clear(); // Очищаем текст

            string title = "Приложение «Компилятор»\n\n";

            // Добавляем заголовок
            AppendText(aboutTextBox, title,
                new Font("Segoe UI", 14, FontStyle.Bold), Color.DarkBlue);

            // Центрируем заголовок
            aboutTextBox.Select(0, title.Length);
            aboutTextBox.SelectionAlignment = HorizontalAlignment.Center;

            // Разработчик
            AppendText(aboutTextBox, "Разработчик: ",
                new Font("Segoe UI", 12, FontStyle.Bold), Color.Black);
            AppendText(aboutTextBox, "Даниил Орлов\n\n",
                new Font("Segoe UI", 12, FontStyle.Regular), Color.Black);

            // Назначение приложения
            AppendText(aboutTextBox, "Назначение приложения:\n",
                new Font("Segoe UI", 12, FontStyle.Bold), Color.Black);
            AppendText(aboutTextBox,
                "• Редактирование текста.\n" +
                "• Сохранение и открытие текстовых файлов.\n" +
                "• Выполнение базовых операций с текстом.\n" +
                "• Вызов справочной информации.\n\n",
                new Font("Segoe UI", 12, FontStyle.Regular), Color.Black);

            // Версия
            AppendText(aboutTextBox, "Версия: ",
                new Font("Segoe UI", 12, FontStyle.Bold), Color.Black);
            AppendText(aboutTextBox, "1.0\n\n",
                new Font("Segoe UI", 12, FontStyle.Regular), Color.Black);

            AppendText(aboutTextBox, "Спасибо за использование приложения!",
                new Font("Segoe UI", 12, FontStyle.Italic), Color.DarkGreen);
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
    }
}
