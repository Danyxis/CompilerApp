using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompilerApp
{
    public static class EmbeddedHtmlViewer // Класс встроенного просмотрщика HTML
    {
        // Метод для загрузки и показа встроенного HTML-ресурса
        public static void OpenEmbeddedHtml(string resourceName)
        {
            // Получаем текущую сборку, содержащую встроенные ресурсы
            var assembly = Assembly.GetExecutingAssembly();

            // Получаем поток ресурса по имени (если файл встроен в проект)
            using Stream stream = assembly.GetManifestResourceStream(resourceName);

            // Проверяем, найден ли ресурс; если нет — выводим сообщение об ошибке
            if (stream == null)
            {
                System.Windows.Forms.MessageBox.Show("Файл ресурса не найден: " + resourceName);
                return;
            }

            // Читаем содержимое HTML-файла из потока
            using StreamReader reader = new StreamReader(stream);
            string htmlContent = reader.ReadToEnd();

            // Генерируем временный путь и сохраняем HTML-контент во временный файл
            // Файл будет временно сохраняться в системную папку Temp текущего пользователя
            string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".html");
            File.WriteAllText(tempFilePath, htmlContent);

            // Открываем временный HTML-файл в браузере по умолчанию
            Process.Start(new ProcessStartInfo
            {
                FileName = tempFilePath, // Путь к временно созданному HTML-файлу
                UseShellExecute = true // Указывает системе использовать стандартное приложение (браузер)
            });
        }

        // Метод для загрузки и показа встроенного HTML-ресурса с изображениями
        public static void OpenEmbeddedHtmlWithImages(string htmlResourceName)
        {
            // Получаем сборку, в которой находятся встроенные ресурсы
            var assembly = Assembly.GetExecutingAssembly();

            // Пытаемся получить поток ресурса HTML по имени (htmlResourceName)
            using Stream htmlStream = assembly.GetManifestResourceStream(htmlResourceName)
                ?? throw new FileNotFoundException("Не найден HTML-ресурс: " + htmlResourceName);

            // Читаем содержимое HTML-файла из потока
            using StreamReader reader = new StreamReader(htmlStream);
            string htmlContent = reader.ReadToEnd();

            // Ищем все теги <img src="..."> в HTML и обрабатываем каждый
            htmlContent = Regex.Replace(htmlContent, "<img[^>]*src=[\"']([^\"']+)[\"'][^>]*>", match =>
            {
                // Извлекаем путь к изображению из атрибута src
                string imgPath = match.Groups[1].Value;

                // Преобразуем путь изображения в полное имя ресурса (заменяя / и \ на .)
                string resourcePath = FindImageResourceFullName(assembly, imgPath);

                // Если ресурс не найден, оставляем тег изображения без изменений
                if (resourcePath == null)
                    return match.Value;

                // Загружаем поток изображения из ресурсов
                using Stream imageStream = assembly.GetManifestResourceStream(resourcePath);
                using MemoryStream ms = new MemoryStream();

                // Копируем содержимое потока изображения в память
                imageStream.CopyTo(ms);

                // Преобразуем байты изображения в строку base64
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Определяем MIME-тип изображения по его расширению
                string extension = Path.GetExtension(imgPath).ToLower().TrimStart('.');
                string mimeType = extension == "png" ? "image/png" :
                                  extension == "jpg" || extension == "jpeg" ? "image/jpeg" :
                                  extension == "gif" ? "image/gif" : "application/octet-stream";

                // Заменяем значение src на встроенные base64-данные
                return Regex.Replace(match.Value, "src=[\"'][^\"']+[\"']",
                    $"src=\"data:{mimeType};base64,{base64}\"");
            });

            // Создаём временный HTML-файл в системной папке Temp
            string tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".html");

            // Записываем изменённое HTML-содержимое в файл
            File.WriteAllText(tempFile, htmlContent);

            // Открываем временный HTML-файл в браузере по умолчанию
            Process.Start(new ProcessStartInfo
            {
                FileName = tempFile, // Путь к временно созданному HTML-файлу
                UseShellExecute = true // Указывает системе использовать стандартное приложение (браузер)
            });
        }

        /// <summary>
        /// Поиск полного имени встроенного ресурса изображения по его относительному пути.
        /// Заменяет символы '/' и '\' на точки и ищет совпадение среди всех ресурсов сборки.
        /// </summary>
        private static string FindImageResourceFullName(Assembly assembly, string shortName)
        {
            // Преобразуем путь файла в формат встроенного ресурса (через точки)
            string target = shortName.Replace('/', '.').Replace('\\', '.');

            // Ищем ресурс, имя которого оканчивается на преобразованный путь
            return assembly.GetManifestResourceNames().
                FirstOrDefault(name => name.EndsWith(target, StringComparison.OrdinalIgnoreCase));
        }

    }
}
