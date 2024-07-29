namespace Ticketing.Dtos.MessageDtos;

public class MessagesOfTicketDto
{
    public int Id { get; set; }
    public string Body { get; set; }
    public DateTime SendDate { get; set; }
    public string SenderUsername { get; set; }
    public MessagesOfTicketDto ParentMessage { get; set; }
}