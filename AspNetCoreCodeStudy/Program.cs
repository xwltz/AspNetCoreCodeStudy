using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AspNetCoreCodeStudy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = new EfDataContext();
            Console.WriteLine("transfer all data to the mangodb");

            var rowCount = db.KeyWordArticle.Count(x => x.EnabledMark && !x.DeleteMark);
            Console.WriteLine($"data row：{rowCount}");

            Console.WriteLine("read table data into the list");

            Stopwatch st1 = new Stopwatch();
            st1.Start();
            var list = db.KeyWordArticle.Where(x => x.EnabledMark && !x.DeleteMark).Take(1000).ToList();
            st1.Stop();
            Console.WriteLine($"[EntityFrameworkCore] a total of {list.Count} lines of data are read, and the time is {st1.ElapsedMilliseconds}ms.");

            Stopwatch st2 = new Stopwatch();
            st2.Start();
            var addCount = MongoDbHelper<KeyWordArticleEntity>.InsertMany(new MongoDbHost(), list.ToList());
            st2.Stop();
            Console.WriteLine($"[MongoDb] a total of {addCount} lines of data are processed, and the time is {st2.ElapsedMilliseconds}ms.");

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //foreach (var str in AddSuccess(list))
            //{
            //    Console.WriteLine(str);
            //}
            //stopwatch.Stop();
            //Console.WriteLine($"[MongoDb] yield add data, when: {stopwatch.ElapsedMilliseconds}ms.");

            //Stopwatch st3 = new Stopwatch();
            //st3.Start();
            //var readCount = MongoDbHelper<KeyWordArticleEntity>.FindList(new MongoDbHost(), FilterDefinition<KeyWordArticleEntity>.Empty);
            //st3.Stop();
            //Console.WriteLine($"[MongoDb] a total of {readCount.Count} lines of data are read, and the time is {st3.ElapsedMilliseconds}ms.");

            //for (int i = 0; i < 10; i++)
            //{
            //    Stopwatch st = new Stopwatch();
            //    st.Start();
            //    var readRow = MongoDbHelper<KeyWordArticleEntity>.FindList(new MongoDbHost(), FilterDefinition<KeyWordArticleEntity>.Empty);
            //    st.Stop();
            //    Console.WriteLine($"[MongoDb] a total of {readRow.Count} lines of data are read, and the time is {st.ElapsedMilliseconds}ms.");
            //}

            Console.ReadLine();
        }

        public static IEnumerable<string> AddSuccess(List<KeyWordArticleEntity> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Stopwatch st = new Stopwatch();
                st.Start();
                var addCount = MongoDbHelper<KeyWordArticleEntity>.Add(new MongoDbHost(), list[i]);
                st.Stop();
                var result = $"no.[{i + 1}] execution completed, when: {st.ElapsedMilliseconds}ms.";
                yield return result;
            }
        }
    }
}