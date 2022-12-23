using System.IO;

namespace File_Manager.ViewModel
{
    public class FileVM : ObjectsVM // вю модель файла
    {
        public FileVM(string FNAME) : base(FNAME)
        {
            FULLNANE = FNAME;
        }

        public FileVM(FileInfo file) : base(file.Name)
        {
            FULLNANE = file.FullName;
        }
        public FileVM(string FNAME, string FULLNAMEE) : base(FNAME)
        {
            FULLNANE = FULLNAMEE;
        }
    }
}
