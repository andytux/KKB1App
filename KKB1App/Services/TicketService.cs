using KKB1App.Data;
using KKB1App.Data.Models;
using KKB1App.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KKB1App.Services
{
    public class TicketService
    {
        private readonly AppDbContext dbContext;

        public TicketService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// speichert ein ticket inklusive besitzer in der datenbank
        /// </summary>
        /// <param name="ticketSaleVM">viewmodel</param>
        /// <returns>bool</returns>
        public async Task<bool> SellTicketAsync(TicketSaleVM ticketSaleVM)
        {
            // überprüft ob der ausgewählte sessel schon besetzt ist
            bool seatTaken = await dbContext.Tickets.AnyAsync(t =>
                                t.ShowId == ticketSaleVM.ShowId &&
                                t.Row == ticketSaleVM.Row &&
                                t.SeatNumber == ticketSaleVM.SeatNumber);

            if (seatTaken)
            {
                return false;
            }

            // erstellt einen neuen besitzer
            var holder = new TicketHolder
            {
                TicketHolderName = ticketSaleVM.FullName,
                Address = ticketSaleVM.Address,
            };

            var ticket = new Ticket
            {
                ShowId = ticketSaleVM.ShowId,
                Row = ticketSaleVM.Row,
                SeatNumber = ticketSaleVM.SeatNumber,
                Discount = ticketSaleVM.Discount,
                TicketHolder = holder
            };

            dbContext.Tickets.Add(ticket);
            await dbContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Erstellt eine liste der besetzten sitze pro reihe 
        /// </summary>
        /// <param name="showId"></param>
        /// <returns>List<Reihe, Sitznummer></returns>
        public async Task<List<(string Row, int Seat)>> GetTakenSeatsAsync(int showId)
        {
            return await dbContext.Tickets
                .Where(t => t.ShowId == showId)
                .Select(t => new ValueTuple<string, int>(t.Row, t.SeatNumber))
                .ToListAsync();
        }

    }
}
