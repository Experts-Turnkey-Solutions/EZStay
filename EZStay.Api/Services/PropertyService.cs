using EZStay.Api.Models.Domain;
using EZStay.Api.Repositories;

namespace EZStay.Api.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IRepository<Property> _repository;

        public PropertyService(IRepository<Property> repository)
        {
            _repository = repository;
        }

        public async Task<Property> CreatePropertyAsync(Property property) =>
            await _repository.AddAsync(property);

        public async Task<bool> DeletePropertyAsync(Guid id) =>
            await _repository.DeleteAsync(id);

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync() =>
            await _repository.GetAllAsync();

        public async Task<Property?> GetPropertyByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<Property?> UpdatePropertyAsync(Guid id, Property property) =>
            await _repository.UpdateAsync(id, property);
    }
}
