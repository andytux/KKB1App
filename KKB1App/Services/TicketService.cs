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

        public async Task<bool> SellTicketAsync(TicketSaleVM ticketSaleVM)
        {
            bool seatTaken = await dbContext.Tickets.AnyAsync(t =>
            t.ShowId == ticketSaleVM.ShowId &&
            t.Row == ticketSaleVM.Row &&
            t.SeatNumber == ticketSaleVM.SeatNumber);

            if (seatTaken)
            {
                return false;
            }

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
    }
}
