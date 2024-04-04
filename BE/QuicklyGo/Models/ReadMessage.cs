using QuicklyGo.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace QuicklyGo.Models
{
    public class ReadMessage: BaseEntity
    {
        public int Id { get; set; }
        public string ReceipientId { get; set; }
        public int MessageId { get; set; }
        public Message Message { get; set; }
        public User Receipient { get; set; }
    }
}
