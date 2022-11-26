using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApp.Models
{
    public class TredModel
    {
        public TredModel(Guid userId, string userName, string photo, int messageCount)
        {
            RecipientUserId = userId;
            RecipientUsername = userName;
            UnreadMessagesCount = messageCount;
            RecipientJpegPhoto = photo;


        }
        public int UnreadMessagesCount { get; set; }
        public Guid RecipientUserId { get; set; }
        public string RecipientUsername { get; set; }
        public string RecipientJpegPhoto { get; set; }
    }
}
