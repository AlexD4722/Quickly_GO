using System.ComponentModel.DataAnnotations;

namespace QuicklyGo.Models.Common
{
    public abstract class BaseEntity
    {
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
