namespace Ticketing.Dtos.MessageDtos;

public class MessageInputDto
{
    public string? Body { get; set; }

    public int? SenderId { get; set; }

    public int? TicketId { get; set; }

    public int? ParentMessageId { get; set; }
}