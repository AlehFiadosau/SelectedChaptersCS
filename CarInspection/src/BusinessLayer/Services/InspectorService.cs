using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Interfaces;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    internal class InspectorService : IService<Inspector, int>
    {
        private readonly IGenericRepository<InspectorDto, int> _inspectorRepository;
        private readonly IMapper _mapper;

        public InspectorService(IGenericRepository<InspectorDto, int> inspectorRepository, IMapper mapper)
        {
            _inspectorRepository = inspectorRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(Inspector item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(Inspector item)
        {
            if (item.PersonalNumber < 0)
            {
                throw new ArgumentException("The personal number must be positive", nameof(item));
            }

            await _inspectorRepository.CreateAsync(_mapper.Map<InspectorDto>(item));

            var allInspectors = await _inspectorRepository.GetAllAsync();
            var inspector = allInspectors.Last();

            return inspector.Id;
        }

        public Task DeleteAsync(Inspector item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        private async Task DeleteInternalAsync(Inspector item)
        {
            var allInspectors = await _inspectorRepository.GetAllAsync();
            if (!allInspectors.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Inspector)} not found", nameof(allInspectors));
            }

            await _inspectorRepository.DeleteAsync(_mapper.Map<InspectorDto>(item));
        }

        public async Task<List<Inspector>> GetAllAsync()
        {
            var allInspectors = await _inspectorRepository.GetAllAsync();
            if (!allInspectors.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Inspector)} not found", nameof(allInspectors));
            }

            var result = _mapper.Map<List<Inspector>>(allInspectors);
            return result;
        }

        public async Task<Inspector> GetByIdAsync(int id)
        {
            var inspector = await _inspectorRepository.GetByIdAsync(id);
            if (inspector == null)
            {
                throw new NotFoundException($"Entity {nameof(Inspector)} by Id not found", nameof(inspector));
            }

            var result = _mapper.Map<Inspector>(inspector);
            return result;
        }

        public Task UpdateAsync(Inspector item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task UpdateInternalAsync(Inspector item)
        {
            var allInspectors = await _inspectorRepository.GetAllAsync();
            if (!allInspectors.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Inspector)} not found", nameof(allInspectors));
            }

            if (item.PersonalNumber < 0)
            {
                throw new ArgumentException("The personal number must be positive", nameof(item));
            }

            await _inspectorRepository.UpdateAsync(_mapper.Map<InspectorDto>(item));
        }
    }
}
