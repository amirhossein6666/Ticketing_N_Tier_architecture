using AutoMapper;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Enums;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.ResponseDtos.TicketResponseDtos;
using Ticketing.Dtos.TicketDtos;

namespace Ticketing.businessLogicLayer.Services.Implementations;

public class TicketService: ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public TicketService(ITicketRepository ticketRepository, IMapper mapper, IUserRepository userRepository)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<CreateUpdateTicketResponseDto> CreateTicket(TicketInputDto ticketInputDto)
    {
        var creator = await _userRepository.GetUserById(ticketInputDto.CreatorId);
        if (creator is null)
        {
            return new CreateUpdateTicketResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = $"user with id {ticketInputDto.CreatorId} not found",
            };
        }
        var ticket = _mapper.Map<Ticket>(ticketInputDto);
        ticket.Status = Status.Unread;
        try
        {
            var returnedTicket = await _ticketRepository.CreateTicket(ticket);
            return new CreateUpdateTicketResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status201Created,
                Message = $"ticket with id {returnedTicket.Id} created",
                Data = _mapper.Map<CreateUpdateTicketDto>(returnedTicket)
            };
        }
        catch (Exception e)
        {
            return new CreateUpdateTicketResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.ToString()
            };
        }

    }

    public async Task<TicketResponseDto> GetTicketById(int id)
    {
        var ticket = await _ticketRepository.GetTicketById(id);
        if (ticket is null)
        {
            return new TicketResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = "ticket Not Found"
            };
        }
        return new TicketResponseDto()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Ok",
            Data = _mapper.Map<TicketDto>(ticket)
        };
    }

    public async Task<TicketListResponseDto> GetAllTickets()
    {
        var tickets = await _ticketRepository.GetAllTickets();
        var ticketDtos = _mapper.Map<ICollection<TicketListDto>>(tickets);
        return new TicketListResponseDto()
        {
            IsSuccess = true,
            StatusCode = tickets.Count == 0 ? StatusCodes.Status404NotFound : StatusCodes.Status200OK,
            Message = tickets.Count == 0 ? "no tickets found" : $"{tickets.Count} tickets found",
            Data = ticketDtos
        };
    }

    public async Task<TicketListResponseDto> GetTicketsByCreatorId(int creatorId)
    {
        var tickets = await _ticketRepository.GetTicketsByCreatorId(creatorId);
        var ticketDtos = _mapper.Map<ICollection<TicketListDto>>(tickets);
        return new TicketListResponseDto()
        {
            IsSuccess = true,
            StatusCode = tickets.Count == 0 ? StatusCodes.Status404NotFound : StatusCodes.Status200OK,
            Message = tickets.Count == 0 ? "no tickets found" : $"{tickets.Count} tickets found",
            Data = ticketDtos
        };
    }

    public async Task<CreateUpdateTicketResponseDto> UpdateTicket(int id, UpdateTicketInputDto updateTicketInputDto)
    {
        var ticket = await _ticketRepository.GetTicketById(id);
        if (ticket is null)
            return new CreateUpdateTicketResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"ticket with id {id} Not found"
            };
        _mapper.Map(updateTicketInputDto, ticket);
        try
        {
            var returnedTicket = await _ticketRepository.UpdateTicket(ticket);
            return new CreateUpdateTicketResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = $"ticket with id {id} updated",
                Data = _mapper.Map<CreateUpdateTicketDto>(returnedTicket),
            };
        }
        catch (Exception e)
        {
            return new CreateUpdateTicketResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.ToString()
            };
        }

    }

    public async Task<SetTicketRatingResponseDto> SetTicketRating(int ticketId, int rating)
    {
        var ticket = await _ticketRepository.GetTicketById(ticketId);
        if (ticket is null)
            return new SetTicketRatingResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"ticket with id {ticketId} Not found"
            };

        switch (rating)
        {
            case 1:
                ticket.Rating = Rating.OneStar;
                break;
            case 2:
                ticket.Rating = Rating.TwoStar;
                break;
            case 3:
                ticket.Rating = Rating.ThreeStar;
                break;
            case 4:
                ticket.Rating = Rating.FourStar;
                break;
            case 5:
                ticket.Rating = Rating.FiveStar;
                break;
            default:
                return new SetTicketRatingResponseDto()
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = $"Invalid Value for Rating enum"
                };
        }
        try
        {
            var returnedTicket = await _ticketRepository.UpdateTicket(ticket);
            return new SetTicketRatingResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = $"ticket with id {ticketId} updated",
                Data = _mapper.Map<SetTicketRatingDto>(returnedTicket),
            };
        }
        catch (Exception e)
        {
            return new SetTicketRatingResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.ToString()
            };
        }
    }

    public async Task<DeleteTicketResponseDto> DeleteTicket(int id)
    {
        var ticket = await _ticketRepository.GetTicketById(id);
        if (ticket is null)
            return new DeleteTicketResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"ticket with id {id} Not found"
            };
        ticket.IsDeleted = true;
        try
        {
            await _ticketRepository.UpdateTicket(ticket);
            return new DeleteTicketResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = $"ticket with id {id} removed",
            };
        }
        catch (Exception e)
        {
            return new DeleteTicketResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.ToString()
            };
        }
    }
}