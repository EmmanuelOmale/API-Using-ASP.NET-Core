using Data.Entities;
using TutorialHell.Models;

namespace Data.Repositories
{
    public interface IContactRepository 
    {
        Task<Contact> GetContactAsync(string contactId);
        Task<IEnumerable<Contact>> GetContactsAsync();
        Task<IEnumerable<Contact>> SearchContactAsync(string firstName, string lastName);
        Task<int> DeleteContactAsync(string id);
        Task<Contact> AddContactAsync(Contact contact);
        Task<int> UpdateContactAsync(Contact updateContactEntity);
        PaginatedContacts paginatedAsync(List<Contact> contacts, int pageNumber, int perPageSize);
    }
}
