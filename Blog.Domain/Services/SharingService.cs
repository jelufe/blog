using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class SharingService : ISharingService
    {
        private readonly ISharingRepository _sharingRepository;

        public SharingService(ISharingRepository sharingRepository)
        {
            _sharingRepository = sharingRepository;
        }

        public async Task<bool> InsertSharing(Sharing sharing)
        {
            sharing.CreatedAt = DateTime.Now;

            return await _sharingRepository.InsertSharing(sharing);
        }
    }
}
