namespace ShowDirInfo
{
    delegate void ExeptionDelegate(string exeption);
    interface IInfo
    {
        //Функция обработки
        long DirectoriesInfo(string path);
    }
}
