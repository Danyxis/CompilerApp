namespace CompilerApp
{
    partial class MainMenuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
            menuStrip1 = new MenuStrip();
            itemFile = new ToolStripMenuItem();
            itemCreateFile = new ToolStripMenuItem();
            itemOpenFile = new ToolStripMenuItem();
            itemSaveFile = new ToolStripMenuItem();
            itemSaveFileAs = new ToolStripMenuItem();
            itemExit = new ToolStripMenuItem();
            itemCorrection = new ToolStripMenuItem();
            itemUndo = new ToolStripMenuItem();
            itemRedo = new ToolStripMenuItem();
            itemCut = new ToolStripMenuItem();
            itemCopy = new ToolStripMenuItem();
            itemPaste = new ToolStripMenuItem();
            itemDelete = new ToolStripMenuItem();
            itemSelectAll = new ToolStripMenuItem();
            itemText = new ToolStripMenuItem();
            itemSettingTask = new ToolStripMenuItem();
            itemGrammar = new ToolStripMenuItem();
            itemGrammarClassification = new ToolStripMenuItem();
            itemMethodOfAnalysis = new ToolStripMenuItem();
            itemDiagnosisAndNeutralizationOfErrors = new ToolStripMenuItem();
            itemTestExamples = new ToolStripMenuItem();
            itemListOfLiterature = new ToolStripMenuItem();
            itemSourceCodeOfProgram = new ToolStripMenuItem();
            itemLaunch = new ToolStripMenuItem();
            comboModeSelector = new ToolStripComboBox();
            itemReference = new ToolStripMenuItem();
            itemCallHelp = new ToolStripMenuItem();
            itemAboutProgram = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            buttonCreateDocument = new ToolStripButton();
            buttonOpenDocument = new ToolStripButton();
            buttonSaveChanges = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            fontSizeComboBox = new ToolStripComboBox();
            toolStripSeparator2 = new ToolStripSeparator();
            buttonUndoChanges = new ToolStripButton();
            buttonRedoLastChange = new ToolStripButton();
            buttonCopyTextFragment = new ToolStripButton();
            buttonCutTextFragment = new ToolStripButton();
            buttonPasteTextFragment = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            buttonRunParser = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            buttonCallHelp = new ToolStripButton();
            buttonCallInformationAboutProgram = new ToolStripButton();
            splitContainerWorkArea = new SplitContainer();
            panelEditorArea = new Panel();
            inputArea = new RichTextBox();
            lineNumbersBox = new RichTextBox();
            tabControlOutput = new TabControl();
            tabPageTokens = new TabPage();
            outputTable = new DataGridView();
            LexemeIndex = new DataGridViewTextBoxColumn();
            LexemeCode = new DataGridViewTextBoxColumn();
            TokenType = new DataGridViewTextBoxColumn();
            LexemeValue = new DataGridViewTextBoxColumn();
            LexemeLocation = new DataGridViewTextBoxColumn();
            tabPageErrors = new TabPage();
            errorsTable = new DataGridView();
            ErrorIndex = new DataGridViewTextBoxColumn();
            ErrorDescription = new DataGridViewTextBoxColumn();
            ErrorValue = new DataGridViewTextBoxColumn();
            ErrorLocation = new DataGridViewTextBoxColumn();
            tabPageResults = new TabPage();
            outputArea = new RichTextBox();
            statusStrip1 = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerWorkArea).BeginInit();
            splitContainerWorkArea.Panel1.SuspendLayout();
            splitContainerWorkArea.Panel2.SuspendLayout();
            splitContainerWorkArea.SuspendLayout();
            panelEditorArea.SuspendLayout();
            tabControlOutput.SuspendLayout();
            tabPageTokens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)outputTable).BeginInit();
            tabPageErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorsTable).BeginInit();
            tabPageResults.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Control;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { itemFile, itemCorrection, itemText, itemLaunch, comboModeSelector, itemReference });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1082, 32);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // itemFile
            // 
            itemFile.DropDownItems.AddRange(new ToolStripItem[] { itemCreateFile, itemOpenFile, itemSaveFile, itemSaveFileAs, itemExit });
            itemFile.Name = "itemFile";
            itemFile.Size = new Size(59, 28);
            itemFile.Text = "Файл";
            // 
            // itemCreateFile
            // 
            itemCreateFile.Name = "itemCreateFile";
            itemCreateFile.Size = new Size(192, 26);
            itemCreateFile.Text = "Создать";
            itemCreateFile.Click += ItemCreateFile_Click;
            // 
            // itemOpenFile
            // 
            itemOpenFile.Name = "itemOpenFile";
            itemOpenFile.Size = new Size(192, 26);
            itemOpenFile.Text = "Открыть";
            itemOpenFile.Click += ItemOpenFile_Click;
            // 
            // itemSaveFile
            // 
            itemSaveFile.Name = "itemSaveFile";
            itemSaveFile.Size = new Size(192, 26);
            itemSaveFile.Text = "Сохранить";
            itemSaveFile.Click += ItemSaveFile_Click;
            // 
            // itemSaveFileAs
            // 
            itemSaveFileAs.Name = "itemSaveFileAs";
            itemSaveFileAs.Size = new Size(192, 26);
            itemSaveFileAs.Text = "Сохранить как";
            itemSaveFileAs.Click += ItemSaveFileAs_Click;
            // 
            // itemExit
            // 
            itemExit.Name = "itemExit";
            itemExit.Size = new Size(192, 26);
            itemExit.Text = "Выход";
            itemExit.Click += ItemExit_Click;
            // 
            // itemCorrection
            // 
            itemCorrection.DropDownItems.AddRange(new ToolStripItem[] { itemUndo, itemRedo, itemCut, itemCopy, itemPaste, itemDelete, itemSelectAll });
            itemCorrection.Name = "itemCorrection";
            itemCorrection.Size = new Size(74, 28);
            itemCorrection.Text = "Правка";
            // 
            // itemUndo
            // 
            itemUndo.Name = "itemUndo";
            itemUndo.Size = new Size(186, 26);
            itemUndo.Text = "Отменить";
            itemUndo.Click += ItemUndo_Click;
            // 
            // itemRedo
            // 
            itemRedo.Name = "itemRedo";
            itemRedo.Size = new Size(186, 26);
            itemRedo.Text = "Повторить";
            itemRedo.Click += ItemRedo_Click;
            // 
            // itemCut
            // 
            itemCut.Name = "itemCut";
            itemCut.Size = new Size(186, 26);
            itemCut.Text = "Вырезать";
            itemCut.Click += ItemCut_Click;
            // 
            // itemCopy
            // 
            itemCopy.Name = "itemCopy";
            itemCopy.Size = new Size(186, 26);
            itemCopy.Text = "Копировать";
            itemCopy.Click += ItemCopy_Click;
            // 
            // itemPaste
            // 
            itemPaste.Name = "itemPaste";
            itemPaste.Size = new Size(186, 26);
            itemPaste.Text = "Вставить";
            itemPaste.Click += ItemPaste_Click;
            // 
            // itemDelete
            // 
            itemDelete.Name = "itemDelete";
            itemDelete.Size = new Size(186, 26);
            itemDelete.Text = "Удалить";
            itemDelete.Click += ItemDelete_Click;
            // 
            // itemSelectAll
            // 
            itemSelectAll.Name = "itemSelectAll";
            itemSelectAll.Size = new Size(186, 26);
            itemSelectAll.Text = "Выделить всё";
            itemSelectAll.Click += ItemSelectAll_Click;
            // 
            // itemText
            // 
            itemText.DropDownItems.AddRange(new ToolStripItem[] { itemSettingTask, itemGrammar, itemGrammarClassification, itemMethodOfAnalysis, itemDiagnosisAndNeutralizationOfErrors, itemTestExamples, itemListOfLiterature, itemSourceCodeOfProgram });
            itemText.Name = "itemText";
            itemText.Size = new Size(59, 28);
            itemText.Text = "Текст";
            // 
            // itemSettingTask
            // 
            itemSettingTask.Name = "itemSettingTask";
            itemSettingTask.Size = new Size(363, 26);
            itemSettingTask.Text = "Постановка задачи";
            itemSettingTask.Click += ItemSettingTask_Click;
            // 
            // itemGrammar
            // 
            itemGrammar.Name = "itemGrammar";
            itemGrammar.Size = new Size(363, 26);
            itemGrammar.Text = "Грамматика";
            itemGrammar.Click += ItemGrammar_Click;
            // 
            // itemGrammarClassification
            // 
            itemGrammarClassification.Name = "itemGrammarClassification";
            itemGrammarClassification.Size = new Size(363, 26);
            itemGrammarClassification.Text = "Классификация грамматики";
            itemGrammarClassification.Click += ItemGrammarClassification_Click;
            // 
            // itemMethodOfAnalysis
            // 
            itemMethodOfAnalysis.Name = "itemMethodOfAnalysis";
            itemMethodOfAnalysis.Size = new Size(363, 26);
            itemMethodOfAnalysis.Text = "Метод анализа";
            itemMethodOfAnalysis.Click += ItemMethodOfAnalysis_Click;
            // 
            // itemDiagnosisAndNeutralizationOfErrors
            // 
            itemDiagnosisAndNeutralizationOfErrors.Name = "itemDiagnosisAndNeutralizationOfErrors";
            itemDiagnosisAndNeutralizationOfErrors.Size = new Size(363, 26);
            itemDiagnosisAndNeutralizationOfErrors.Text = "Диагностика и нейтрализация ошибок";
            itemDiagnosisAndNeutralizationOfErrors.Click += ItemDiagnosisAndNeutralizationOfErrors_Click;
            // 
            // itemTestExamples
            // 
            itemTestExamples.Name = "itemTestExamples";
            itemTestExamples.Size = new Size(363, 26);
            itemTestExamples.Text = "Тестовые примеры";
            itemTestExamples.Click += ItemTestExamples_Click;
            // 
            // itemListOfLiterature
            // 
            itemListOfLiterature.Name = "itemListOfLiterature";
            itemListOfLiterature.Size = new Size(363, 26);
            itemListOfLiterature.Text = "Список литературы";
            itemListOfLiterature.Click += ItemListOfLiterature_Click;
            // 
            // itemSourceCodeOfProgram
            // 
            itemSourceCodeOfProgram.Name = "itemSourceCodeOfProgram";
            itemSourceCodeOfProgram.Size = new Size(363, 26);
            itemSourceCodeOfProgram.Text = "Исходный код программы";
            itemSourceCodeOfProgram.Click += ItemSourceCodeOfProgram_Click;
            // 
            // itemLaunch
            // 
            itemLaunch.Name = "itemLaunch";
            itemLaunch.Size = new Size(55, 28);
            itemLaunch.Text = "Пуск";
            itemLaunch.Click += ItemLaunch_Click;
            // 
            // comboModeSelector
            // 
            comboModeSelector.BackColor = SystemColors.Control;
            comboModeSelector.Margin = new Padding(0);
            comboModeSelector.Name = "comboModeSelector";
            comboModeSelector.Padding = new Padding(5, 0, 5, 0);
            comboModeSelector.Size = new Size(260, 28);
            comboModeSelector.Text = "Режим анализа";
            comboModeSelector.SelectedIndexChanged += comboModeSelector_SelectedIndexChanged;
            // 
            // itemReference
            // 
            itemReference.DropDownItems.AddRange(new ToolStripItem[] { itemCallHelp, itemAboutProgram });
            itemReference.Name = "itemReference";
            itemReference.Size = new Size(81, 28);
            itemReference.Text = "Справка";
            // 
            // itemCallHelp
            // 
            itemCallHelp.Name = "itemCallHelp";
            itemCallHelp.Size = new Size(197, 26);
            itemCallHelp.Text = "Вызов справки";
            itemCallHelp.Click += ItemCallHelp_Click;
            // 
            // itemAboutProgram
            // 
            itemAboutProgram.Name = "itemAboutProgram";
            itemAboutProgram.Size = new Size(197, 26);
            itemAboutProgram.Text = "О программе";
            itemAboutProgram.Click += ItemAboutProgram_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.Lavender;
            toolStrip1.ImageScalingSize = new Size(40, 40);
            toolStrip1.Items.AddRange(new ToolStripItem[] { buttonCreateDocument, buttonOpenDocument, buttonSaveChanges, toolStripSeparator1, fontSizeComboBox, toolStripSeparator2, buttonUndoChanges, buttonRedoLastChange, buttonCopyTextFragment, buttonCutTextFragment, buttonPasteTextFragment, toolStripSeparator3, buttonRunParser, toolStripSeparator4, buttonCallHelp, buttonCallInformationAboutProgram });
            toolStrip1.Location = new Point(0, 32);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1082, 47);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // buttonCreateDocument
            // 
            buttonCreateDocument.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonCreateDocument.Image = Properties.Resources.iconCreateDocument;
            buttonCreateDocument.ImageTransparentColor = Color.Magenta;
            buttonCreateDocument.Margin = new Padding(0, 1, 10, 2);
            buttonCreateDocument.Name = "buttonCreateDocument";
            buttonCreateDocument.Size = new Size(44, 44);
            buttonCreateDocument.Text = "Создать";
            buttonCreateDocument.Click += ItemCreateFile_Click;
            // 
            // buttonOpenDocument
            // 
            buttonOpenDocument.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonOpenDocument.Image = Properties.Resources.iconOpenDocument;
            buttonOpenDocument.ImageTransparentColor = Color.Magenta;
            buttonOpenDocument.Margin = new Padding(0, 1, 10, 2);
            buttonOpenDocument.Name = "buttonOpenDocument";
            buttonOpenDocument.Size = new Size(44, 44);
            buttonOpenDocument.Text = "Открыть";
            buttonOpenDocument.Click += ItemOpenFile_Click;
            // 
            // buttonSaveChanges
            // 
            buttonSaveChanges.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonSaveChanges.Image = Properties.Resources.iconSaveChanges;
            buttonSaveChanges.ImageTransparentColor = Color.Magenta;
            buttonSaveChanges.Margin = new Padding(0, 1, 10, 2);
            buttonSaveChanges.Name = "buttonSaveChanges";
            buttonSaveChanges.Size = new Size(44, 44);
            buttonSaveChanges.Text = "Сохранить";
            buttonSaveChanges.Click += ItemSaveFile_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(0, 0, 10, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 47);
            // 
            // fontSizeComboBox
            // 
            fontSizeComboBox.BackColor = SystemColors.Control;
            fontSizeComboBox.DropDownWidth = 75;
            fontSizeComboBox.IntegralHeight = false;
            fontSizeComboBox.Items.AddRange(new object[] { "9", "10", "12", "14", "16", "18" });
            fontSizeComboBox.Margin = new Padding(1, 0, 10, 0);
            fontSizeComboBox.Name = "fontSizeComboBox";
            fontSizeComboBox.Size = new Size(75, 47);
            fontSizeComboBox.SelectedIndexChanged += FontSizeComboBox_SelectedIndexChanged;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(0, 0, 10, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 47);
            // 
            // buttonUndoChanges
            // 
            buttonUndoChanges.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonUndoChanges.Image = Properties.Resources.iconUndoChanges;
            buttonUndoChanges.ImageTransparentColor = Color.Magenta;
            buttonUndoChanges.Margin = new Padding(0, 1, 10, 2);
            buttonUndoChanges.Name = "buttonUndoChanges";
            buttonUndoChanges.Size = new Size(44, 44);
            buttonUndoChanges.Text = "Отменить";
            buttonUndoChanges.Click += ItemUndo_Click;
            // 
            // buttonRedoLastChange
            // 
            buttonRedoLastChange.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonRedoLastChange.Image = Properties.Resources.iconRepeatLastChange;
            buttonRedoLastChange.ImageTransparentColor = Color.Magenta;
            buttonRedoLastChange.Margin = new Padding(0, 1, 10, 2);
            buttonRedoLastChange.Name = "buttonRedoLastChange";
            buttonRedoLastChange.Size = new Size(44, 44);
            buttonRedoLastChange.Text = "Повторить";
            buttonRedoLastChange.Click += ItemRedo_Click;
            // 
            // buttonCopyTextFragment
            // 
            buttonCopyTextFragment.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonCopyTextFragment.Image = Properties.Resources.iconCopyTextFragment;
            buttonCopyTextFragment.ImageTransparentColor = Color.Magenta;
            buttonCopyTextFragment.Margin = new Padding(0, 1, 10, 2);
            buttonCopyTextFragment.Name = "buttonCopyTextFragment";
            buttonCopyTextFragment.Size = new Size(44, 44);
            buttonCopyTextFragment.Text = "Копировать";
            buttonCopyTextFragment.Click += ItemCopy_Click;
            // 
            // buttonCutTextFragment
            // 
            buttonCutTextFragment.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonCutTextFragment.Image = Properties.Resources.iconCutTextFragment;
            buttonCutTextFragment.ImageTransparentColor = Color.Magenta;
            buttonCutTextFragment.Margin = new Padding(0, 1, 10, 2);
            buttonCutTextFragment.Name = "buttonCutTextFragment";
            buttonCutTextFragment.Size = new Size(44, 44);
            buttonCutTextFragment.Text = "Вырезать";
            buttonCutTextFragment.Click += ItemCut_Click;
            // 
            // buttonPasteTextFragment
            // 
            buttonPasteTextFragment.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonPasteTextFragment.Image = Properties.Resources.iconInsertTextFragment;
            buttonPasteTextFragment.ImageTransparentColor = Color.Magenta;
            buttonPasteTextFragment.Margin = new Padding(0, 1, 10, 2);
            buttonPasteTextFragment.Name = "buttonPasteTextFragment";
            buttonPasteTextFragment.Size = new Size(44, 44);
            buttonPasteTextFragment.Text = "Вставить";
            buttonPasteTextFragment.Click += ItemPaste_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Margin = new Padding(0, 0, 10, 0);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 47);
            // 
            // buttonRunParser
            // 
            buttonRunParser.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonRunParser.Image = Properties.Resources.iconRunParser;
            buttonRunParser.ImageTransparentColor = Color.Magenta;
            buttonRunParser.Margin = new Padding(0, 1, 10, 2);
            buttonRunParser.Name = "buttonRunParser";
            buttonRunParser.Size = new Size(44, 44);
            buttonRunParser.Text = "Пуск";
            buttonRunParser.Click += ItemLaunch_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Margin = new Padding(0, 0, 10, 0);
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 47);
            // 
            // buttonCallHelp
            // 
            buttonCallHelp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonCallHelp.Image = Properties.Resources.iconCallHelp;
            buttonCallHelp.ImageTransparentColor = Color.Magenta;
            buttonCallHelp.Margin = new Padding(0, 1, 10, 2);
            buttonCallHelp.Name = "buttonCallHelp";
            buttonCallHelp.Size = new Size(44, 44);
            buttonCallHelp.Text = "Вызов справки";
            buttonCallHelp.Click += ItemCallHelp_Click;
            // 
            // buttonCallInformationAboutProgram
            // 
            buttonCallInformationAboutProgram.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonCallInformationAboutProgram.Image = Properties.Resources.iconCallInformationAboutProgram;
            buttonCallInformationAboutProgram.ImageTransparentColor = Color.Magenta;
            buttonCallInformationAboutProgram.Margin = new Padding(0, 1, 10, 2);
            buttonCallInformationAboutProgram.Name = "buttonCallInformationAboutProgram";
            buttonCallInformationAboutProgram.Size = new Size(44, 44);
            buttonCallInformationAboutProgram.Text = "О программе";
            buttonCallInformationAboutProgram.Click += ItemAboutProgram_Click;
            // 
            // splitContainerWorkArea
            // 
            splitContainerWorkArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainerWorkArea.BackColor = Color.LightSteelBlue;
            splitContainerWorkArea.Location = new Point(12, 93);
            splitContainerWorkArea.Name = "splitContainerWorkArea";
            splitContainerWorkArea.Orientation = Orientation.Horizontal;
            // 
            // splitContainerWorkArea.Panel1
            // 
            splitContainerWorkArea.Panel1.Controls.Add(panelEditorArea);
            // 
            // splitContainerWorkArea.Panel2
            // 
            splitContainerWorkArea.Panel2.Controls.Add(tabControlOutput);
            splitContainerWorkArea.Size = new Size(1058, 615);
            splitContainerWorkArea.SplitterDistance = 300;
            splitContainerWorkArea.SplitterWidth = 14;
            splitContainerWorkArea.TabIndex = 2;
            // 
            // panelEditorArea
            // 
            panelEditorArea.Controls.Add(inputArea);
            panelEditorArea.Controls.Add(lineNumbersBox);
            panelEditorArea.Dock = DockStyle.Fill;
            panelEditorArea.Location = new Point(0, 0);
            panelEditorArea.Name = "panelEditorArea";
            panelEditorArea.Size = new Size(1058, 300);
            panelEditorArea.TabIndex = 0;
            // 
            // inputArea
            // 
            inputArea.BackColor = SystemColors.Control;
            inputArea.Dock = DockStyle.Fill;
            inputArea.Location = new Point(40, 0);
            inputArea.Name = "inputArea";
            inputArea.Size = new Size(1018, 300);
            inputArea.TabIndex = 0;
            inputArea.Text = "";
            inputArea.TextChanged += InputArea_TextChanged;
            // 
            // lineNumbersBox
            // 
            lineNumbersBox.Dock = DockStyle.Left;
            lineNumbersBox.Enabled = false;
            lineNumbersBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lineNumbersBox.ForeColor = SystemColors.WindowText;
            lineNumbersBox.Location = new Point(0, 0);
            lineNumbersBox.Name = "lineNumbersBox";
            lineNumbersBox.ReadOnly = true;
            lineNumbersBox.ScrollBars = RichTextBoxScrollBars.None;
            lineNumbersBox.Size = new Size(40, 300);
            lineNumbersBox.TabIndex = 0;
            lineNumbersBox.Text = "";
            // 
            // tabControlOutput
            // 
            tabControlOutput.Controls.Add(tabPageTokens);
            tabControlOutput.Controls.Add(tabPageErrors);
            tabControlOutput.Controls.Add(tabPageResults);
            tabControlOutput.Dock = DockStyle.Fill;
            tabControlOutput.Location = new Point(0, 0);
            tabControlOutput.Name = "tabControlOutput";
            tabControlOutput.SelectedIndex = 0;
            tabControlOutput.Size = new Size(1058, 301);
            tabControlOutput.TabIndex = 0;
            // 
            // tabPageTokens
            // 
            tabPageTokens.BackColor = Color.Transparent;
            tabPageTokens.Controls.Add(outputTable);
            tabPageTokens.Location = new Point(4, 29);
            tabPageTokens.Name = "tabPageTokens";
            tabPageTokens.Padding = new Padding(3);
            tabPageTokens.Size = new Size(1050, 268);
            tabPageTokens.TabIndex = 0;
            tabPageTokens.Text = "Список лексем";
            // 
            // outputTable
            // 
            outputTable.AllowUserToAddRows = false;
            outputTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            outputTable.BackgroundColor = SystemColors.Control;
            outputTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            outputTable.Columns.AddRange(new DataGridViewColumn[] { LexemeIndex, LexemeCode, TokenType, LexemeValue, LexemeLocation });
            outputTable.Dock = DockStyle.Fill;
            outputTable.Location = new Point(3, 3);
            outputTable.MultiSelect = false;
            outputTable.Name = "outputTable";
            outputTable.ReadOnly = true;
            outputTable.RowHeadersWidth = 51;
            outputTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            outputTable.Size = new Size(1044, 262);
            outputTable.TabIndex = 0;
            // 
            // LexemeIndex
            // 
            LexemeIndex.FillWeight = 30F;
            LexemeIndex.HeaderText = "№";
            LexemeIndex.MinimumWidth = 6;
            LexemeIndex.Name = "LexemeIndex";
            LexemeIndex.ReadOnly = true;
            // 
            // LexemeCode
            // 
            LexemeCode.FillWeight = 42.7807465F;
            LexemeCode.HeaderText = "Код";
            LexemeCode.MinimumWidth = 6;
            LexemeCode.Name = "LexemeCode";
            LexemeCode.ReadOnly = true;
            // 
            // TokenType
            // 
            TokenType.FillWeight = 119.3847F;
            TokenType.HeaderText = "Тип";
            TokenType.MinimumWidth = 6;
            TokenType.Name = "TokenType";
            TokenType.ReadOnly = true;
            // 
            // LexemeValue
            // 
            LexemeValue.FillWeight = 118.267F;
            LexemeValue.HeaderText = "Значение";
            LexemeValue.MinimumWidth = 6;
            LexemeValue.Name = "LexemeValue";
            LexemeValue.ReadOnly = true;
            // 
            // LexemeLocation
            // 
            LexemeLocation.FillWeight = 119.567543F;
            LexemeLocation.HeaderText = "Местоположение";
            LexemeLocation.MinimumWidth = 6;
            LexemeLocation.Name = "LexemeLocation";
            LexemeLocation.ReadOnly = true;
            // 
            // tabPageErrors
            // 
            tabPageErrors.Controls.Add(errorsTable);
            tabPageErrors.Location = new Point(4, 29);
            tabPageErrors.Name = "tabPageErrors";
            tabPageErrors.Padding = new Padding(3);
            tabPageErrors.Size = new Size(1050, 268);
            tabPageErrors.TabIndex = 1;
            tabPageErrors.Text = "Список ошибок";
            tabPageErrors.UseVisualStyleBackColor = true;
            // 
            // errorsTable
            // 
            errorsTable.AllowUserToAddRows = false;
            errorsTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            errorsTable.BackgroundColor = SystemColors.Control;
            errorsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            errorsTable.Columns.AddRange(new DataGridViewColumn[] { ErrorIndex, ErrorDescription, ErrorValue, ErrorLocation });
            errorsTable.Dock = DockStyle.Fill;
            errorsTable.Location = new Point(3, 3);
            errorsTable.MultiSelect = false;
            errorsTable.Name = "errorsTable";
            errorsTable.ReadOnly = true;
            errorsTable.RowHeadersWidth = 51;
            errorsTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            errorsTable.Size = new Size(1044, 262);
            errorsTable.TabIndex = 0;
            // 
            // ErrorIndex
            // 
            ErrorIndex.FillWeight = 30F;
            ErrorIndex.HeaderText = "№";
            ErrorIndex.MinimumWidth = 6;
            ErrorIndex.Name = "ErrorIndex";
            ErrorIndex.ReadOnly = true;
            // 
            // ErrorDescription
            // 
            ErrorDescription.FillWeight = 270F;
            ErrorDescription.HeaderText = "Описание";
            ErrorDescription.MinimumWidth = 6;
            ErrorDescription.Name = "ErrorDescription";
            ErrorDescription.ReadOnly = true;
            // 
            // ErrorValue
            // 
            ErrorValue.FillWeight = 170F;
            ErrorValue.HeaderText = "Значение";
            ErrorValue.MinimumWidth = 6;
            ErrorValue.Name = "ErrorValue";
            ErrorValue.ReadOnly = true;
            // 
            // ErrorLocation
            // 
            ErrorLocation.FillWeight = 130F;
            ErrorLocation.HeaderText = "Местоположение";
            ErrorLocation.MinimumWidth = 6;
            ErrorLocation.Name = "ErrorLocation";
            ErrorLocation.ReadOnly = true;
            // 
            // tabPageResults
            // 
            tabPageResults.Controls.Add(outputArea);
            tabPageResults.Location = new Point(4, 29);
            tabPageResults.Name = "tabPageResults";
            tabPageResults.Size = new Size(1050, 268);
            tabPageResults.TabIndex = 2;
            tabPageResults.Text = "Вывод результата";
            tabPageResults.UseVisualStyleBackColor = true;
            // 
            // outputArea
            // 
            outputArea.BackColor = SystemColors.Control;
            outputArea.Dock = DockStyle.Fill;
            outputArea.Location = new Point(0, 0);
            outputArea.Name = "outputArea";
            outputArea.ReadOnly = true;
            outputArea.Size = new Size(1050, 268);
            outputArea.TabIndex = 0;
            outputArea.Text = "";
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.Control;
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip1.Location = new Point(0, 727);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1082, 26);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.BackColor = SystemColors.Control;
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(57, 20);
            statusLabel.Text = "Готово";
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(1082, 753);
            Controls.Add(statusStrip1);
            Controls.Add(splitContainerWorkArea);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(700, 250);
            Name = "MainMenuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Компилятор";
            FormClosing += MainMenuForm_FormClosing;
            KeyDown += MainMenuForm_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            splitContainerWorkArea.Panel1.ResumeLayout(false);
            splitContainerWorkArea.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerWorkArea).EndInit();
            splitContainerWorkArea.ResumeLayout(false);
            panelEditorArea.ResumeLayout(false);
            tabControlOutput.ResumeLayout(false);
            tabPageTokens.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)outputTable).EndInit();
            tabPageErrors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorsTable).EndInit();
            tabPageResults.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem itemFile;
        private ToolStripMenuItem itemCorrection;
        private ToolStripMenuItem itemText;
        private ToolStripMenuItem itemLaunch;
        private ToolStripMenuItem itemReference;
        private ToolStripMenuItem itemCreateFile;
        private ToolStripMenuItem itemOpenFile;
        private ToolStripMenuItem itemSaveFile;
        private ToolStripMenuItem itemSaveFileAs;
        private ToolStripMenuItem itemExit;
        private ToolStripMenuItem itemUndo;
        private ToolStripMenuItem itemRedo;
        private ToolStripMenuItem itemCut;
        private ToolStripMenuItem itemCopy;
        private ToolStripMenuItem itemPaste;
        private ToolStripMenuItem itemDelete;
        private ToolStripMenuItem itemSelectAll;
        private ToolStripMenuItem itemSettingTask;
        private ToolStripMenuItem itemGrammar;
        private ToolStripMenuItem itemGrammarClassification;
        private ToolStripMenuItem itemMethodOfAnalysis;
        private ToolStripMenuItem itemDiagnosisAndNeutralizationOfErrors;
        private ToolStripMenuItem itemTestExamples;
        private ToolStripMenuItem itemListOfLiterature;
        private ToolStripMenuItem itemSourceCodeOfProgram;
        private ToolStripMenuItem itemCallHelp;
        private ToolStripMenuItem itemAboutProgram;
        private ToolStrip toolStrip1;
        private ToolStripButton buttonCreateDocument;
        private ToolStripButton buttonOpenDocument;
        private ToolStripButton buttonSaveChanges;
        private ToolStripButton buttonUndoChanges;
        private ToolStripButton buttonRedoLastChange;
        private ToolStripButton buttonCopyTextFragment;
        private ToolStripButton buttonCutTextFragment;
        private ToolStripButton buttonPasteTextFragment;
        private ToolStripButton buttonRunParser;
        private ToolStripButton buttonCallHelp;
        private ToolStripButton buttonCallInformationAboutProgram;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private SplitContainer splitContainerWorkArea;
        private RichTextBox inputArea;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripComboBox fontSizeComboBox;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
        private DataGridView outputTable;
        private Panel panelEditorArea;
        private RichTextBox lineNumbersBox;
        private TabControl tabControlOutput;
        private TabPage tabPageTokens;
        private TabPage tabPageErrors;
        private DataGridView errorsTable;
        private DataGridViewTextBoxColumn ErrorIndex;
        private DataGridViewTextBoxColumn ErrorDescription;
        private DataGridViewTextBoxColumn ErrorValue;
        private DataGridViewTextBoxColumn ErrorLocation;
        private DataGridViewTextBoxColumn LexemeIndex;
        private DataGridViewTextBoxColumn LexemeCode;
        private DataGridViewTextBoxColumn TokenType;
        private DataGridViewTextBoxColumn LexemeValue;
        private DataGridViewTextBoxColumn LexemeLocation;
        private TabPage tabPageResults;
        private RichTextBox outputArea;
        private ToolStripComboBox comboModeSelector;
    }
}
