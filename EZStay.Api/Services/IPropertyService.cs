using EZStay.Api.Models.Domain;

namespace EZStay.Api.Services
{
    public interface IPropertyService
    {
        Task<Property?> GetPropertyByIdAsync(Guid id);
        Task<IEnumerable<Property>> GetAllPropertiesAsync();
        Task<Property> CreatePropertyAsync(Property property);
        Task<Property?> UpdatePropertyAsync(Guid id, Property property);
        Task<bool> DeletePropertyAsync(Guid id);
    }
}
