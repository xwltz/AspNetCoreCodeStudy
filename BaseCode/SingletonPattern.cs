using System;

namespace BaseCode
{
    /// <summary>
    /// 懒汉式单例
    /// </summary>
    public sealed class SingletonPattern
    {
        private static SingletonPattern _singleton = null;
        private static readonly object SingletonLock = new object();

        public static SingletonPattern CreateInstance()
        {
            if (_singleton != null) return _singleton;

            lock (SingletonLock)
            {
                return _singleton ?? (_singleton = new SingletonPattern());
            }
        }

        private SingletonPattern()
        {
            Console.WriteLine("Singleton-Mode-初始化");
        }
    }

    /// <summary>
    /// 饿汉式静态构造函数单例
    /// </summary>
    public sealed class SingletonPatternSecond
    {
        private static readonly SingletonPatternSecond SingletonPattern;

        public static SingletonPatternSecond CreateInstance()
        {
            return SingletonPattern;
        }

        /// <summary>
        /// 静态构造函数
        /// 由CLR自动调用并只会调用一次
        /// </summary>
        static SingletonPatternSecond()
        {
            SingletonPattern = new SingletonPatternSecond();
        }

        private SingletonPatternSecond()
        {
            Console.WriteLine("Singleton-Mode-初始化");
        }
    }

    /// <summary>
    /// 饿汉式静态字段单例
    /// </summary>
    public sealed class SingletonPatternThird
    {
        /// <summary>
        /// 静态构造字段
        /// 由CLR自动调用并只会调用一次
        /// </summary>
        private static readonly SingletonPatternThird SingletonPattern = new SingletonPatternThird();

        public static SingletonPatternThird CreateInstance()
        {
            return SingletonPattern;
        }

        private SingletonPatternThird()
        {
            Console.WriteLine("Singleton-Mode-初始化");
        }
    }
}
