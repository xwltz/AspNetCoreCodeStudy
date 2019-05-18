using System.Collections.Generic;
using DotnetSpider.Core;
using DotnetSpider.Extension;
using DotnetSpider.Extension.Model;
using DotnetSpider.Extension.Pipeline;
using DotnetSpider.Extraction;
using DotnetSpider.Extraction.Model;
using DotnetSpider.Extraction.Model.Attribute;
using DotnetSpider.Extraction.Model.Formatter;

namespace WebSpider
{
    public class BaiDuPcSpider : EntitySpider
    {
        public static void Run()
        {
            var spider = new Spider();
            spider.Run();
        }

        protected override void OnInit(params string[] arguments)
        {
            const string word = "可乐|雪碧";
            AddRequest($"http://news.baidu.com/ns?word={word}&tn=news&from=news&cl=2&pn=0&rn=20&ct=1",
                new Dictionary<string, dynamic> {{"Keyword", word}});
            AddEntityType<BaiduSearchEntry>();
            AddPipeline(new ConsoleEntityPipeline());
        }

        [Schema("baidu", "baidu_search_entity_model")]
        [Entity(Expression = ".//div[@class='result']", Type = SelectorType.XPath)]
        public class BaiduSearchEntry : BaseEntity
        {
            [Column]
            [Field(Expression = "Keyword", Type = SelectorType.Enviroment)]
            public string Keyword { get; set; }

            [Column]
            [Field(Expression = ".//h3[@class='c-title']/a")]
            [ReplaceFormatter(NewValue = "", OldValue = "<em>")]
            [ReplaceFormatter(NewValue = "", OldValue = "</em>")]
            public string Title { get; set; }

            [Column]
            [Field(Expression = ".//h3[@class='c-title']/a/@href")]
            public string Url { get; set; }

            [Column]
            [Field(Expression = ".//div/p[@class='c-author']/text()")]
            [ReplaceFormatter(NewValue = "-", OldValue = "&nbsp;")]
            public string Website { get; set; }

            [Column]
            [Field(Expression = ".//div/span/a[@class='c-cache']/@href")]
            public string Snapshot { get; set; }

            [Column]
            [Field(Expression = ".//div[@class='c-summary c-row ']", Option = FieldOptions.InnerText)]
            [ReplaceFormatter(NewValue = "", OldValue = "<em>")]
            [ReplaceFormatter(NewValue = "", OldValue = "</em>")]
            [ReplaceFormatter(NewValue = " ", OldValue = "&nbsp;")]
            public string Details { get; set; }

            [Column(Length = 0)]
            [Field(Expression = ".", Option = FieldOptions.InnerText)]
            [ReplaceFormatter(NewValue = "", OldValue = "<em>")]
            [ReplaceFormatter(NewValue = "", OldValue = "</em>")]
            [ReplaceFormatter(NewValue = " ", OldValue = "&nbsp;")]
            public string PlainText { get; set; }
        }
    }
}