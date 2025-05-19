using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    public static class TextActions // Класс для реализации пункта меню "Текст"
    {
        public static void ShowSettingTask() // Вызов HTML-файла "Постановка задачи"
        {
            EmbeddedHtmlViewer.OpenEmbeddedHtml("CompilerApp.Resources.Text.setting_task.html");
        }

        public static void ShowGrammar() // Вызов HTML-файла "Грамматика"
        {
            EmbeddedHtmlViewer.OpenEmbeddedHtml("CompilerApp.Resources.Text.grammar.html");
        }

        public static void ShowGrammarClassification() // Вызов HTML-файла "Классификация грамматики"
        {
            EmbeddedHtmlViewer.OpenEmbeddedHtml("CompilerApp.Resources.Text.grammar_classification.html");
        }

        public static void ShowMethodOfAnalysis() // Вызов HTML-файла "Метод анализа"
        {
            EmbeddedHtmlViewer.OpenEmbeddedHtmlWithImages("CompilerApp.Resources.Text.method_of_analysis.method_of_analysis.html");
        }

        public static void ShowDiagnosisAndNeutralizationOfErrors() // Вызов HTML-файла "Диагностика и нейтрализация ошибок"
        {
            EmbeddedHtmlViewer.OpenEmbeddedHtmlWithImages("CompilerApp.Resources.Text.diagnosis_and_neutralization_of_errors." +
                "diagnosis_and_neutralization_of_errors.html");
        }

        public static void ShowTestExamples() // Вызов HTML-файла "Тестовые примеры"
        {
            EmbeddedHtmlViewer.OpenEmbeddedHtmlWithImages("CompilerApp.Resources.Text.test_examples.test_examples.html");
        }

        public static void ShowListOfLiterature() // Вызов HTML-файла "Список литературы"
        {
            EmbeddedHtmlViewer.OpenEmbeddedHtml("CompilerApp.Resources.Text.list_of_literature.html");
        }

        public static void ShowSourceCodeOfProgram() // Вызов HTML-файла "Исходный код программы"
        {
            EmbeddedHtmlViewer.OpenEmbeddedHtml("CompilerApp.Resources.Text.source_code_of_program.html");
        }
    }
}
