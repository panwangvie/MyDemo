using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pwj.Shared.DateModel
{
    /// <summary>
    /// 功能描述    ：实体类基类  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/5 11:08:49 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/5 11:08:49 
    /// </summary>
    public class BaseEntity
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
