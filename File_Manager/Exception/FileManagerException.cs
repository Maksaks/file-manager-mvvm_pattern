using System;
using System.Collections.Generic;
using System.Text;

namespace File_Manager.Exception
{
    public abstract class FileManagerException : System.Exception
    {
        public string Message{ get; set; }

        public FileManagerException(string mes)
        {
            Message = mes;
        }
    }

    public class OperationCutException : FileManagerException
    {
        public OperationCutException() : base("Неможливо вставити об'єкт у самого себе!!!")
        {

        }
    }
    public class NoAccesToCreateDir : FileManagerException
    {
        public NoAccesToCreateDir() : base("Неможливо створити директорію, можливо відсутні права досутпу!!!")
        {

        }
    }
    public class NoAccesToCreateDirInMainMenu : FileManagerException
    {
        public NoAccesToCreateDirInMainMenu() : base("Неможливо створити директорію в головному меню")
        {

        }
    }
    public class NoAccesToDeleteDir : FileManagerException
    {
        public NoAccesToDeleteDir() : base("Неможливо видалити директорію, можливо відсутні права досутпу!!!")
        {

        }
    }
    public class NoAccesToDeleteDisk : FileManagerException
    {
        public NoAccesToDeleteDisk() : base("Не можна видаляти накопичувачі!!!")
        {

        }
    }
    public class NoAccesToRenameDisk : FileManagerException
    {
        public NoAccesToRenameDisk() : base("Неможливо змінити ім'я накопичувача, можливо відсутні права досутпу!!!")
        {

        }
    }
    public class NoOperationsWithDisk : FileManagerException
    {
        public NoOperationsWithDisk() : base("Операції копіювання, вирізання та вставки з диском не підтримуються!!!")
        {

        }
    }
    public class NoAccesToPasteDir : FileManagerException
    {
        public NoAccesToPasteDir() : base("Неможливо вставити директорію, можливо відсутні права досутпу!!!")
        {

        }
    }
    public class NoAccesToRenameDir : FileManagerException
    {
        public NoAccesToRenameDir() : base("Неможливо змінити ім'я директорії, можливо відсутні права досутпу!!!")
        {

        }
    }
    public class NoAccesToPasteFile : FileManagerException
    {
        public NoAccesToPasteFile() : base("Неможливо вставити файл, можливо відсутні права досутпу!!!")
        {

        }
    }
    public class NoAccesToRenameFile : FileManagerException
    {
        public NoAccesToRenameFile() : base("Неможливо змінити ім'я файлу, можливо відсутні права досутпу!!!")
        {

        }
    }
    public class FileAlreadyExsist : FileManagerException
    {
        public FileAlreadyExsist() : base("Файл вже знаходиться у даному каталозі")
        {

        }
    }
    public class DirAlreadyExsist : FileManagerException
    {
        public DirAlreadyExsist() : base("Каталог вже знаходиться у даному каталозі")
        {

        }
    }
    public class EmptySearchResult : FileManagerException
    {
        public EmptySearchResult() : base("Результати пошуку відсутні!")
        {

        }
    }
    public class EmptyFilterhResult : FileManagerException
    {
        public EmptyFilterhResult() : base("Результати фільтру відсутні!")
        {

        }
    }
    public class NoFileMenuItemsExsist : FileManagerException
    {
        public NoFileMenuItemsExsist(string path) : base($"Відсутній файл з закладками каталогів. Буде створено новий! Шлях до нового файлу: {path}")
        {

        }
    }
    public class NoOpenTabs : FileManagerException
    {
        public NoOpenTabs() : base("Операція не доступна, спочатку відкрийте нову вкладку")
        {

        }
    }
    public class IncorrectNewNameFile : FileManagerException
    {
        public IncorrectNewNameFile() : base("Введено некоректне ім'я. Заборонено вводити символи \\|/:*?\"<> в ім'я файлу")
        {

        }
    }
}
