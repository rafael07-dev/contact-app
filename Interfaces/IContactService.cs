﻿using ContactApp.Models;

namespace ContactApp.Interfaces
{
    public interface IContactService
    {
        Task<PaginatedList<Contact>> GetContactAsync(int pageIndex, int pageSize);
        Task<PaginatedList<Contact>> GetContactBySearchAsync(string search, int pageIndex, int pageSize);
        Task<Contact> GetByIdAsync(int id);
        Task<Contact> CreateAsync(Contact entity);
        Task UpdateAsync(int id, Contact entity);
        Task<Contact> DeleteAsync(int id);
    }
}
