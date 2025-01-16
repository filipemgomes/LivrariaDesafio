using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task AddAsync(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var subject = await GetByIdAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }
    }
}
