using System;

namespace SignalRApp.Entities
{
    public class MessageEntity : BaseEntity
    {
        public Guid AuthorUserId { get; set; }
        public Guid RecipientUserId { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }

    }
}
