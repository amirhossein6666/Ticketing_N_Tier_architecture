namespace Ticketing.Dtos.MessageDtos;

public class CreateUpdateMessageDto
{
    public int Id { get; set; }

    public string Body { get; set; }

    public DateTime SendDate { get; set; }

    public int SenderId { get; set; }

    public int TicketId { get; set; }

    public int? ParentMessageId { get; set; }

}