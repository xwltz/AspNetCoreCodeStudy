using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreCodeStudy
{
    public class EfDataContext : DbContext
    {
        public DbSet<KeyWordArticleEntity> KeyWordArticle { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = 47.98.97.153;Initial Catalog = vy_aiseo_com;User Id = vy_aiseo_com;Password = Pki4hPjSam;");
        }
    }


    #region Entity

    /// <summary>
    /// 文章实体
    /// </summary>
    [Table("KeyWordArticle_1")]
    public class KeyWordArticleEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 关键词Id
        /// </summary>
        public int KeyWordId { get; set; }
        /// <summary>
        /// 网站域名
        /// </summary>
        public string WebUrl { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 点击数
        /// </summary>
        public int Click { get; set; }
        /// <summary>
        /// 分类标签
        /// </summary>
        public string Tagids { get; set; }
        /// <summary>
        /// 企业标签
        /// </summary>
        public string EnterpriseLabel { get; set; }
        /// <summary>
        /// 启用标记
        /// </summary>
        public virtual bool EnabledMark { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public virtual bool DeleteMark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        public virtual string DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeleteTime { get; set; }
    }

    #endregion

}
