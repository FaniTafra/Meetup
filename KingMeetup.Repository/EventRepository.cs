using KingMeetup.Model;
using KingMeetup.Model.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMeetup.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly MeetupDbContext _context;

        public EventRepository(MeetupDbContext context)
        {
            _context = context;
        }
        public async Task Create(Event newEvent, CancellationToken cancellationToken)
        {
            await _context.Events.AddAsync(newEvent, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<Event> FindById(int id, CancellationToken cancellationToken) 
        {
            try 
            {
                return await _context.Events.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            
        }
        public async Task<bool> Update(Event newEvent, CancellationToken cancellationToken) 
        {
            Event? updatedEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == newEvent.Id, cancellationToken);
            if (updatedEvent != null) 
            {
                updatedEvent.Name = newEvent.Name;
                updatedEvent.Active = newEvent.Active;
                updatedEvent.AttendeesOnSite = newEvent.AttendeesOnSite;
                updatedEvent.AttendeesOnLine = newEvent.AttendeesOnLine;
                updatedEvent.Address = newEvent.Address;
                updatedEvent.City = newEvent.City;
                updatedEvent.State = newEvent.State;
                updatedEvent.DateModified = DateTime.Now;
                updatedEvent.StartDateTime = newEvent.StartDateTime;
                updatedEvent.EndDateTime = newEvent.EndDateTime;
                updatedEvent.Status = newEvent.Status;
                updatedEvent.Description = newEvent.Description;
                updatedEvent.ModifiedBy = newEvent.ModifiedBy;

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
        public async Task<List<Event?>> GetUserEvents(int userId)
        {
            try
            {
                return await _context.Events.Where(e => e.Active).Join(_context.UsersInterests.Where(ui => ui.UserId == userId), e => e.InterestId, ui => ui.InterestId, (e, ui) => e).OrderBy(e => e.StartDateTime).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<Event?>> GetAdminEvents(int userId)
        {
            try
            {
                return await _context.Events.Where(ui => ui.UserId == userId && ui.Active).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<Event?>> GetSignedUpEvents(int userId)
        {
            try
            {
                return await _context.Events.Where(e => e.Active).Join(_context.AttendeeLists.Where(al => al.UserId == userId), e => e.Id, al => al.EventId, (e, al) => e).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
