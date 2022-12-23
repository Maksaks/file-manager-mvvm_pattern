namespace File_Manager.ViewModel
{
    public abstract class ObjectsVM : BaseViewModel // базовый класс для объектов 
    {
        public string NAME { get; set; }
        public string FULLNANE { get; set; }
        public ObjectsVM(string NAME)
        {
            this.NAME = NAME;
        }
    }
}
