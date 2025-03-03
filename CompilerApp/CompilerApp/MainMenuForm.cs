using System.Windows.Forms;

namespace CompilerApp
{
    public partial class MainMenuForm : Form // Основная форма приложения
    {
        private string? currentFilePath = null; // Текущий путь файла
        private bool isTextChanged = false; // Отслеживание изменений текста

        public MainMenuForm()
        {
            InitializeComponent();
            fontSizeComboBox.SelectedItem = "9"; // Начальный размер шрифта (область редактирования, вывода результатов)

            // Привязка обработчиков событий перетаскивания и отпускания
            // файла в область редактирования (inputArea)
            inputArea.AllowDrop = true;
            inputArea.DragEnter += InputArea_DragEnter;
            inputArea.DragDrop += InputArea_DragDrop;
        }

        // Обработчики событий
        private void ItemCreateFile_Click(object sender, EventArgs e) // Обработчик события создания файла
        {
            FileActions.CreateFile(this);
        }

        private void ItemOpenFile_Click(object sender, EventArgs e) // Обработчик события открытия файла
        {
            FileActions.OpenFile(this);
        }

        private void ItemSaveFile_Click(object sender, EventArgs e) // Обработчик события сохранения файла
        {
            FileActions.SaveFile(this);
        }

        private void ItemSaveFileAs_Click(object sender, EventArgs e) // Обработчик события сохранения файла как
        {
            FileActions.SaveFileAs(this);
        }

        private void ItemExit_Click(object sender, EventArgs e) // Обработчик события выхода из приложения
        {
            FileActions.ExitApplication(this);
        }

        private void InputArea_TextChanged(object sender, EventArgs e) // Обработчик события изменения текста
        {
            SetTextChanged(true);
        }

        private void ItemUndo_Click(object sender, EventArgs e) // Обработчик события отмены предыдущего изменения в тексте
        {
            EditActions.Undo(this);
        }

        private void ItemRedo_Click(object sender, EventArgs e) // Обработчик события повторения предыдущего изменения в тексте
        {
            EditActions.Redo(this);
        }

        private void ItemCut_Click(object sender, EventArgs e) // Обработчик события - вырезать текстовый фрагмент
        {
            EditActions.Cut(this);
        }

        private void ItemCopy_Click(object sender, EventArgs e) // Обработчик события - копировать текстовый фрагмент
        {
            EditActions.Copy(this);
        }

        private void ItemPaste_Click(object sender, EventArgs e) // Обработчик события - вставить текстовый фрагмент
        {
            EditActions.Paste(this);
        }

        private void ItemDelete_Click(object sender, EventArgs e) // Обработчик события - удалить текстовый фрагмент
        {
            EditActions.Delete(this);
        }

        private void ItemSelectAll_Click(object sender, EventArgs e) // Обработчик события - выделить весь текст
        {
            EditActions.SelectAll(this);
        }

        private void ItemCallHelp_Click(object sender, EventArgs e) // Обработчик события вызова окна справки
        {
            HelpActions.ShowHelp();
        }

        private void ItemAboutProgram_Click(object sender, EventArgs e) // Обработчик события вызова окна "О программе"
        {
            HelpActions.ShowAboutProgram();
        }

        // Обработчик события закрытия формы (через крестик)
        private void MainMenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsTextChanged())
            {
                var result = MessageBox.Show(
                    "Сохранить изменения перед выходом?",
                    "Несохраненные изменения",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    if (!FileActions.SaveFile(this))
                    {
                        e.Cancel = true; // Если сохранение не прошло, отменяем закрытие
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // Отменяем закрытие, если пользователь нажал «Отмена»
                }
            }
        }

        // Работа с RichTextBox
        public RichTextBox GetInputArea() // Возвращает объект inputArea (область ввода)
        {
            return inputArea;
        }

        public RichTextBox GetOutputArea() // Возвращает объект outputArea (область вывода)
        {
            return outputArea;
        }

        // Работа с путём файла
        public void SetCurrentFilePath(string path) // Устанавливает текущий путь файла
        {
            currentFilePath = path;
        }

        public string? GetCurrentFilePath() // Возвращает текущий путь файла
        {
            return currentFilePath;
        }

        // Работа с изменениями текста
        public void SetTextChanged(bool changed) // Устанавливает флаг изменения текста (true - текст изменён, false - текст не изменён)
        {
            isTextChanged = changed;
        }

        public bool IsTextChanged() // Возвращает флаг изменения текста
        {
            return isTextChanged;
        }

        // Дополнительные обработчики

        // Обработчик события выбора размера шрифта (область редактирования, вывода результатов)
        private void FontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(fontSizeComboBox.SelectedItem.ToString(), out int fontSize))
            {
                inputArea.Font = new Font(inputArea.Font.FontFamily, fontSize);
                outputArea.Font = new Font(outputArea.Font.FontFamily, fontSize);
            }
        }

        // Обработчик событий нажатий клавиш (горячие клавиши для быстрых команд)
        private void MainMenuForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N) // Ctrl + N — создать новый файл
            {
                FileActions.CreateFile(this);
                e.SuppressKeyPress = true;       // Отмена двойных срабатываний
            }
            else if (e.Control && e.KeyCode == Keys.O) // Ctrl + O — открыть файл
            {
                FileActions.OpenFile(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.S && !e.Shift) // Ctrl + S — сохранить
            {
                FileActions.SaveFile(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.S && e.Shift) // Ctrl + Shift + S — сохранить как
            {
                FileActions.SaveFileAs(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Q) // Ctrl + Q — выход
            {
                FileActions.ExitApplication(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Z) // Ctrl + Z — отмена
            {
                EditActions.Undo(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y) // Ctrl + Y — повтор
            {
                EditActions.Redo(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.X) // Ctrl + X — вырезать
            {
                EditActions.Cut(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.C) // Ctrl + C — копировать
            {
                EditActions.Copy(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.V) // Ctrl + V — вставить
            {
                EditActions.Paste(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.A) // Ctrl + A — выделить всё
            {
                EditActions.SelectAll(this);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F1) // F1 — справка
            {
                HelpActions.ShowHelp();
                e.SuppressKeyPress = true;
            }
        }

        // Обработчик события перетаскивания файла в область редактирования
        private void InputArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        // Обработчик события отпускания файла в область редактирования
        private void InputArea_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0 && Path.GetExtension(files[0]).Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                string filePath = files[0];
                inputArea.Text = File.ReadAllText(filePath);
                SetCurrentFilePath(filePath);
                SetTextChanged(false);

                UpdateStatus($"Открыт файл: {Path.GetFileName(filePath)}"); // Обновление строки состояния
            }
            else
            {
                MessageBox.Show("Можно открыть только текстовые файлы (*.txt).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Дополнительные методы

        // Метод обновления строки состояния
        public void UpdateStatus(string message)
        {
            statusLabel.Text = message;
        }
    }
}
