using AutoMapper;
using KingMeetup.Contract;
using KingMeetup.Messaging;
using KingMeetup.Model;
using KingMeetup.Model.Enums;
using KingMeetup.Model.Repositories;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;

namespace KingMeetup.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAttendeeListRepository _attendeeListRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EventService> _logger;

        public EventService(IEventRepository eventRepository, IMapper mapper, ILogger<EventService> logger, IAttendeeListRepository attendeeListRepository)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _logger = logger;
            _attendeeListRepository = attendeeListRepository;
        }

        public async Task<EventResponse> Create(EventRequest request, CancellationToken cancellationToken)
        {
            EventResponse response = new EventResponse()
            {
                Request = request,
            };
            try 
            {
                Event? newEvent = _mapper.Map<Event>(request);
                await _eventRepository.Create(newEvent, cancellationToken);
                response.Success = true;
                response.Message = "Događaj uspješno kreiran.";
                _logger.LogDebug("Creating event: {@request} {@response}", request, response);
                return response;            
            }
            catch (Exception ex) 
            {
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogDebug("Error while creating event: {@request} {@response}", request, response);

                return response;
            }          
        }
        public async Task<EventResponse> FindById(EventRequest request, CancellationToken cancellationToken) 
        {
            EventResponse response = new EventResponse()
            {
                Request = request
            };
            Event eventFromDb = await _eventRepository.FindById(request.Id, cancellationToken);           
            if(eventFromDb != null)
            {
                response.Request = _mapper.Map<EventRequest>(eventFromDb);
                response.Success = true;
                response.Message = "Event pronađen";

                return response;
            }
            else
            {
                response.Success = false;
                response.Message = "Nije pronađen event za taj Id.";
                return response;
            }
        }

        public async Task<List<AttendeeList>> GetEventAttendeesById(int id, CancellationToken cancellationToken)
        {
            return await _attendeeListRepository.GetEventAttendeesByEventId(id, cancellationToken);
        }

        public async Task PutOnWaitListIfFull(AttendeeList request,Event selectedEvent, CancellationToken cancellationToken) 
        {
            request.StatusId = (int)AttendeeStatus.In;
            if (request.IsOnSite && (await _attendeeListRepository.GetNumberOfSignedUpOnSite(request.EventId, true, cancellationToken) >= selectedEvent.AttendeesOnSite))
                request.StatusId = AttendeeStatus.Waiting;              
            else if (!request.IsOnSite && await _attendeeListRepository.GetNumberOfSignedUpOnLine(request.EventId, false, cancellationToken) >= selectedEvent.AttendeesOnLine)
                request.StatusId = AttendeeStatus.Waiting;
        }
        public async Task<AttendeeListsResponse> SignUp(AttendeeListsRequest request, CancellationToken cancellationToken)
        {
            AttendeeListsResponse response = new AttendeeListsResponse() 
            {
                Request = request,
                Message ="Korisnik uspješno prijavljen na događaj.",
            };
            try 
            {
                AttendeeList attendeeList = await _attendeeListRepository.GetAttendeeListWithEvent(request.EventId,request.UserId, cancellationToken);                       
                if (attendeeList == null)
                {
                    AttendeeList? newSignup = _mapper.Map<AttendeeList>(request);
                    newSignup.Active = true;
                    await PutOnWaitListIfFull(newSignup, await _eventRepository.FindById(request.EventId, cancellationToken), cancellationToken);
                    await _attendeeListRepository.EventSignup(newSignup, cancellationToken);
                    await _attendeeListRepository.Save(cancellationToken);
                    if (newSignup.StatusId == AttendeeStatus.Waiting)
                        response.Message = "Dodan na listu čekanja.";
                }
                else 
                {
                    attendeeList.Active = true;
                    attendeeList.IsOnSite = request.IsOnSite;
                    attendeeList.DateModified = DateTime.Now;
                    await PutOnWaitListIfFull(attendeeList, attendeeList.Event, cancellationToken);
                    await _attendeeListRepository.Save(cancellationToken);
                    if (attendeeList.StatusId == AttendeeStatus.Waiting)
                        response.Message = "Dodan na listu čekanja.";                   
                }
                response.Success = true;
                
                _logger.LogDebug("Signing up for event: {@request} {@response}", request, response);
                return response;

            }
            catch (Exception ex) 
            { 
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogDebug("Error while signing up for event: {@request} {@response}", request, response);

                return response;
            }
        }
        public async Task<AttendeeListsResponse> SignOff(AttendeeListsRequest request, CancellationToken cancellationToken) 
        {
            AttendeeListsResponse response = new AttendeeListsResponse() 
            {
                Request = request,
            };
            AttendeeList? attendeeList = await _attendeeListRepository.GetByEventAndUserId(request.EventId, request.UserId,cancellationToken);
            attendeeList.StatusId = AttendeeStatus.Out;
            attendeeList.Active = false;
            attendeeList.DateModified = DateTime.Now;
            await _attendeeListRepository.Save(cancellationToken);    
            response.Success = true;
            response.Message = "Korisnik odjavljen s eventa.";
            _logger.LogDebug("Signing off for event: {@request} {@response}", request, response);
            return response;
        }
        public async Task<AttendeeListsResponse> IsUserSignedUp(AttendeeListsRequest request, CancellationToken cancellationToken)
        {
            AttendeeListsResponse response = new AttendeeListsResponse() 
            {
                Request = request,
            };
            AttendeeList? attendeeList = await _attendeeListRepository.GetAttendeeListWithEvent(request.EventId, request.UserId, cancellationToken);
            if (attendeeList != null && attendeeList.Active)
            {
                response.Message = "Korisnik je prijavljen na event.";
                response.IsSignedUp = true;
            }
            else 
            {
                response.Message = "Korisnik nije prijavljen na event.";
                response.IsSignedUp = false;
            }
            response.Success = true;

            _logger.LogDebug("Checking if user is signed up for event: {@request} {@response}", request, response);

            return response;
        }

        public async Task<List<EventResponse>> GetUserEvents(int userId)
        {
            List<Event> events = await _eventRepository.GetUserEvents(userId);
            return _mapper.Map<List<EventResponse>>(events);
        }

        public async Task<List<EventResponse>> GetAdminEvents(int userId)
        {
            List<Event> events = await _eventRepository.GetAdminEvents(userId);
            return _mapper.Map<List<EventResponse>>(events);
        }
        public async Task<EventResponse> Update(EventRequest request, CancellationToken cancellationToken) 
        {
            EventResponse response = new EventResponse()
            {
                Request = request,
                Message = "Event neuspješno updatean.",
                Success = false,
            };
            Event eventFromDB = _mapper.Map<Event>(request);
            if (await _eventRepository.Update(eventFromDB, cancellationToken))
            {
                response.Success = true;
                response.Message = "Event uspješno updatean.";
                _logger.LogDebug("Updating event: {@request} {@response}", request, response);

                return response;
            }
            _logger.LogDebug("Error while updating event: {@request} {@response}", request, response);
            return response;
        }

        public async Task<List<EventResponse>> GetSignedUpEvents(int userId)
        {
            List<Event> events = await _eventRepository.GetSignedUpEvents(userId);
            return _mapper.Map<List<EventResponse>>(events);
        }
    }
}
