using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.FirstOrDefaultAsync(seller => seller.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            Seller seller = await _context.Seller.FindAsync(id);
            try
            {
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException e)
            {
                throw new IntegrityException("Vendedor possui vendas vinculadas, não é possivel deletar.");
            }

        }

        public async Task UpddateAsync(Seller seller)
        {
            if (! await _context.Seller.AnyAsync(s => s.Id == seller.Id))
            {
                throw new NotFoundException("Vendedor não encontrado!");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
