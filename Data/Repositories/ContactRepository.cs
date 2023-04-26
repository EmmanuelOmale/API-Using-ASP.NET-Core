using Data.Entities;
using Microsoft.EntityFrameworkCore;
using TutorialHell.Data;
using TutorialHell.Models;

namespace Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsDbContext _dbContext;

        public ContactRepository(ContactsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            await _dbContext.Contacts.AddAsync(contact);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return contact;
            }
            else
            {
                throw new Exception("Contact Not Added Successfully");
            }
        }

        public async Task<int> DeleteContactAsync(string id)
        {
            var contactToDelete = await _dbContext.Contacts.FindAsync(id);
            if (contactToDelete != null)
            {
                _dbContext.Contacts.Remove(contactToDelete);
                var result = await _dbContext.SaveChangesAsync();
                return result;
            }
            else
            {
                return 0;
            }
        }

        public async Task<Contact> GetContactAsync(string Id)
        {
            var result = await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == Id);
            if (result == null)
            {
                throw new Exception("Contact not found");
            }
            return result;
        }
        
        public async Task<IEnumerable<Contact>> SearchContactAsync(string firstName, string lastName)
        {
            firstName ??= string.Empty;
            lastName ??= string.Empty;

            var loadSearchContact = await _dbContext.Contacts.Where(item =>
            item.FirstName.ToLower().Contains(firstName.ToLower().Trim()) &&
            item.LastName.ToLower().Contains(lastName.ToLower().Trim())).ToListAsync();
            return loadSearchContact;
        }

        public async Task<int> UpdateContactAsync(Contact updateContactEntity)
        {
            var FindContact = await _dbContext.Contacts.FindAsync(updateContactEntity.Id);
            if (FindContact == null)
            {
                return 0;
            }

            FindContact.FirstName = updateContactEntity.FirstName;
            FindContact.LastName = updateContactEntity.LastName;
            FindContact.Phone = updateContactEntity.Phone;
            FindContact.Email = updateContactEntity.Email;
            FindContact.Address = updateContactEntity.Address;
            FindContact.Id = updateContactEntity.Id;
            
            return await _dbContext.SaveChangesAsync();


        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            var getContacts = await _dbContext.Contacts.ToListAsync();
            return getContacts;
        }

        public PaginatedContacts paginatedAsync(List<Contact> contacts, int pageNumber, int perPageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            perPageSize = perPageSize < 1 ? 5 : perPageSize;
            var totalCount = contacts.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);
            var paginated = contacts.Skip((pageNumber - 1) * perPageSize).Take(perPageSize).ToList();
            var result = new PaginatedContacts
            {
                CurrentPage = pageNumber,
                PageSize = perPageSize,
                TotalPages = totalPages,
                Contacts = paginated
            };
            return result;
        }
    }
}
