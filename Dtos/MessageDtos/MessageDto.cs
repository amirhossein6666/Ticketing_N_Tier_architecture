using Ticketing.DataAccessLayer.Entities;
using Ticketing.Dtos.BaseDtos;

namespace Ticketing.Dtos.MessageDtos;

public class MessageDto
{
    public string Body { get; set; }
    public DateTime SendDate { get; set; }
    public string SenderUsername { get; set; }
    public string ParentMessageBody{ get; set; }
    public DateTime ParentMessageSendDate{ get; set; }
    public string ParentMessageSenderUsername{ get; set; }
}