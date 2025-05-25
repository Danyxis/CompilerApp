using System.Text;
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
            AttachInputAreaEvents(); // Привязка событий к области редактирования (inputArea)
            InitializeModeSelector(); // Инициализируем ComboBox с режимами анализа
        }

        // Метод привязки событий к области редактирования (inputArea)
        private void AttachInputAreaEvents()
        {
            // Привязка обработчиков событий перетаскивания и отпускания
            // файла в область редактирования (inputArea)
            inputArea.AllowDrop = true;
            inputArea.DragEnter += InputArea_DragEnter;
            inputArea.DragDrop += InputArea_DragDrop;

            // Обновление нумерации строк при вертикальной прокрутке, изменении текста, изменении размера области, 
            inputArea.VScroll += (s, e) => UpdateLineNumbers(inputArea, lineNumbersBox);
            inputArea.TextChanged += (s, e) => UpdateLineNumbers(inputArea, lineNumbersBox);
            inputArea.Resize += (s, e) => UpdateLineNumbers(inputArea, lineNumbersBox);
        }

        // Инициализация ComboBox с режимами анализа
        private void InitializeModeSelector()
        {
            // Добавляем элементы в ComboBox: каждый содержит enum и отображаемое название
            foreach (var mode in Enum.GetValues(typeof(AnalysisMode)).Cast<AnalysisMode>())
            {
                comboModeSelector.Items.Add(new ComboBoxItem
                {
                    Mode = mode, // Значение перечисления
                    Display = AnalysisModeNames.DisplayNames[mode] // Отображаемое название
                });
            }
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

        private void ItemSettingTask_Click(object sender, EventArgs e) // Обработчик события вызова "Постановка задачи" (из РГЗ)
        {
            TextActions.ShowSettingTask();
        }

        private void ItemGrammar_Click(object sender, EventArgs e) // Обработчик события вызова "Грамматика" (из РГЗ)
        {
            TextActions.ShowGrammar();
        }

        private void ItemGrammarClassification_Click(object sender, EventArgs e) // Обработчик события вызова "Классификация грамматики" (из РГЗ)
        {
            TextActions.ShowGrammarClassification();
        }

        private void ItemMethodOfAnalysis_Click(object sender, EventArgs e) // Обработчик события вызова "Метод анализа" (из РГЗ)
        {
            TextActions.ShowMethodOfAnalysis();
        }

        // Обработчик события вызова "Диагностика и нейтрализация ошибок" (из РГЗ)
        private void ItemDiagnosisAndNeutralizationOfErrors_Click(object sender, EventArgs e)
        {
            TextActions.ShowDiagnosisAndNeutralizationOfErrors();
        }

        private void ItemTestExamples_Click(object sender, EventArgs e) // Обработчик события вызова "Тестовые примеры" (из РГЗ)
        {
            TextActions.ShowTestExamples();
        }

        private void ItemListOfLiterature_Click(object sender, EventArgs e) // Обработчик события вызова "Список литературы" (из РГЗ)
        {
            TextActions.ShowListOfLiterature();
        }

        private void ItemSourceCodeOfProgram_Click(object sender, EventArgs e) // Обработчик события вызова "Исходный код программы" (из РГЗ)
        {
            TextActions.ShowSourceCodeOfProgram();
        }

        private void ItemLaunch_Click(object sender, EventArgs e) // Обработчик события - запуск компилятора
        {
            // Проверяем, выбран ли режим
            if (comboModeSelector.SelectedItem is not ComboBoxItem item)
            {
                MessageBox.Show("Пожалуйста, выберите режим анализа перед запуском.",
                                "Режим не выбран",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            var selectedMode = item.Mode;

            // Вызываем соответствующий анализатор в зависимости от выбранного режима
            switch (selectedMode)
            {
                // Объявление прототипа функции на языке C/C++
                case AnalysisMode.FunctionPrototype:
                    LaunchActions.LaunchFunctionPrototypeAnalyzer(this);
                    break;

                // Анализ польской инверсной записи (ПОЛИЗ)
                case AnalysisMode.PolishReverseNotation:
                    LaunchActions.LaunchPolishReverseNotationAnalyzer(this);
                    break;

                // Поиск шестнадцатеричных кодов (HEX) цвета
                case AnalysisMode.HexColor:
                    LaunchActions.LaunchHexColorSearch(this);
                    break;

                // Поиск шестнадцатеричных чисел
                case AnalysisMode.HexNumber:
                    LaunchActions.LaunchHexNumberSearch(this);
                    break;

                // Поиск действительных чисел (включая экспоненциальную форму)
                case AnalysisMode.RealNumber:
                    LaunchActions.LaunchRealNumberSearch(this);
                    break;

                // Анализ арифметического выражения
                case AnalysisMode.ArithmeticExpression:
                    LaunchActions.LaunchArithmeticExpressionAnalyzer(this);
                    break;
            }
        }

        // Обработчик события — изменение выбранного режима анализа
        private void comboModeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboModeSelector.SelectedItem is ComboBoxItem item)
            {
                var selectedMode = item.Mode;

                // Подставляем тестовый пример для выбранного режима
                if (AnalysisExamples.TestExamples.TryGetValue(selectedMode, out var example))
                {
                    inputArea.Text = example;
                }
            }
        }

        private void ItemCallHelp_Click(object sender, EventArgs e) // Обработчик события вызова справки
        {
            HelpActions.ShowHelp();
        }

        private void ItemAboutProgram_Click(object sender, EventArgs e) // Обработчик события вызова "О программе"
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

        // Работа с RichTextBox и DataGridView

        // Возвращает объект inputArea (область ввода)
        public RichTextBox GetInputArea() => inputArea;

        // Возвращает объект outputTable (таблица вывода)
        public DataGridView GetOutputTable() => outputTable;

        // Возвращает объект errorsTable (таблица вывода)
        public DataGridView GetErrorsTable() => errorsTable;

        // Возвращает объект outputArea (область вывода)
        public RichTextBox GetOutputArea() => outputArea;

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

        // Обработчик события выбора размера шрифта из ComboBox
        private void FontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(fontSizeComboBox.SelectedItem.ToString(), out int fontSize))
            {
                ApplyFontSizeToAll(fontSize); // Применяем размер ко всем компонентам
                UpdateLineNumbers(inputArea, lineNumbersBox); // Обновляем нумерацию строк (размер шрифта влияет на количество строк)
            }
        }

        // Метод изменения шрифта для всех визуальных компонентов
        private void ApplyFontSizeToAll(int fontSize)
        {
            Font newFont = new Font(inputArea.Font.FontFamily, fontSize);

            inputArea.Font = newFont;
            lineNumbersBox.Font = newFont;

            outputTable.DefaultCellStyle.Font = newFont;
            outputTable.ColumnHeadersDefaultCellStyle.Font = newFont;

            errorsTable.DefaultCellStyle.Font = newFont;
            errorsTable.ColumnHeadersDefaultCellStyle.Font = newFont;

            outputArea.Font = newFont;
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

        // Метод обновления нумерации строк в окне редактирования текста (inputArea)
        private void UpdateLineNumbers(RichTextBox inputArea, RichTextBox lineNumbersBox)
        {
            if (inputArea == null || lineNumbersBox == null) return;

            // Получение номера первой отображаемой строки
            int firstIndex = inputArea.GetCharIndexFromPosition(new Point(0, 0));
            int firstLine = inputArea.GetLineFromCharIndex(firstIndex);

            // Получение номера последней отображаемой строки
            int lastIndex = inputArea.GetCharIndexFromPosition(new Point(0, inputArea.Height));
            int lastLine = inputArea.GetLineFromCharIndex(lastIndex);

            // Очистка и перезапись номеров строк
            lineNumbersBox.Clear();
            for (int i = firstLine; i <= lastLine; i++)
            {
                lineNumbersBox.AppendText((i + 1) + Environment.NewLine);
            }
        }

        // Методы переключения вкладок

        // Метод переключения на вкладку tabPageTokens
        public void SelectTokensTab()
        {
            tabControlOutput.SelectedTab = tabPageTokens;
        }

        // Метод переключения на вкладку tabPageErrors
        public void SelectErrorsTab()
        {
            tabControlOutput.SelectedTab = tabPageErrors;
        }

        // Метод переключения на вкладку tabPageResults
        public void SelectResultsTab()
        {
            tabControlOutput.SelectedTab = tabPageResults;
        }

        // Методы для подсветки ошибок в редакторе

        // Метод подсветки ошибок в редакторе
        internal void HighlightErrors(List<ParseError> errors)
        {
            // Сначала убираем все предыдущие выделения
            inputArea.SelectAll();
            inputArea.SelectionBackColor = inputArea.BackColor;

            // Обрабатываем каждую ошибку из списка
            foreach (var error in errors)
            {
                // Пытаемся извлечь позицию из строки (строка X, символы A-B)
                if (TryExtractPosition(error.Position, out int line, out int start, out int end))
                {
                    try
                    {
                        // Получаем индекс первого символа нужной строки
                        int charIndex = inputArea.GetFirstCharIndexFromLine(line - 1);

                        // Вычисляем абсолютную позицию начала ошибки в тексте
                        int selectionStart = charIndex + start - 1;

                        // Длина выделения = (конец - начало) + 1
                        int length = end - start + 1;

                        // Выделяем ошибочный участок и закрашиваем его
                        inputArea.Select(selectionStart, length);
                        inputArea.SelectionBackColor = Color.Pink;
                    }
                    catch
                    {
                        // Если произошла ошибка (например, индекс за пределами текста) — игнорируем
                    }
                }
            }

            // Снимаем выделение и возвращаем курсор в конец текста
            inputArea.SelectionStart = inputArea.TextLength;
            inputArea.SelectionLength = 0;
            inputArea.SelectionBackColor = inputArea.BackColor;
        }

        // Метод попытки извлечения позиции из строки
        private bool TryExtractPosition(string position, out int line, out int start, out int end)
        {
            line = start = end = 0;

            try
            {
                // Пример строки позиции: "строка 1, символы 5-10"
                string[] parts = position.Split(',');

                // Извлекаем номер строки: "строка 1" -> 1
                line = int.Parse(parts[0].Split(' ')[1]);

                // Извлекаем диапазон символов: "символы 5-10" -> 5 и 10
                string[] symbols = parts[1].Split(' ')[2].Split('-');
                start = int.Parse(symbols[0]);
                end = int.Parse(symbols[1]);
                return true;
            }
            catch
            {
                // Если не удалось разобрать позицию — возвращаем false
                return false;
            }
        }
    }
}
