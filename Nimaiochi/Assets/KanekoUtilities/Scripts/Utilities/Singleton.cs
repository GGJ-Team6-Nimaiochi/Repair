namespace KanekoUtilities
{
    public class Singleton<T> where T : class, new()
    {
        private static readonly T instance = new T();

        //本来ならprivateなコンストラクタを用意するが、コンストラクタで色々したいことが多いのであえて定義しない
        //複数人開発なら用意するべき

        public static T Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
