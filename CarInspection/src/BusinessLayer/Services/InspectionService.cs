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
    internal class InspectionService : IService<Inspection, int>
    {
        private readonly IGenericRepository<InspectionDto, int> _inspectionRepository;
        private readonly IMapper _mapper;

        public InspectionService(IGenericRepository<InspectionDto, int> inspectionRepository, IMapper mapper)
        {
            _inspectionRepository = inspectionRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(Inspection item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(Inspection item)
        {
            if (item.InspectionDate < DateTimeOffset.Now)
            {
                throw new DateException("Inspection date cannot be in the past", nameof(item));
            }

            if (item.Price < 0)
            {
                throw new ArgumentException("The price must be positive", nameof(item));
            }

            await _inspectionRepository.CreateAsync(_mapper.Map<InspectionDto>(item));

            var allInspection = await _inspectionRepository.GetAllAsync();
            var inspection = allInspection.Last();

            return inspection.Id;
        }

        public Task DeleteAsync(Inspection item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        private async Task DeleteInternalAsync(Inspection item)
        {
            var allInspection = await _inspectionRepository.GetAllAsync();
            if (!allInspection.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Inspection)} not found", nameof(allInspection));
            }

            await _inspectionRepository.DeleteAsync(_mapper.Map<InspectionDto>(item));
        }

        public async Task<List<Inspection>> GetAllAsync()
        {
            var allInspection = await _inspectionRepository.GetAllAsync();
            if (!allInspection.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Inspection)} not found", nameof(allInspection));
            }

            var result = _mapper.Map<List<Inspection>>(allInspection);
            return result;
        }

        public async Task<Inspection> GetByIdAsync(int id)
        {
            var inspection = await _inspectionRepository.GetByIdAsync(id);
            if (inspection == null)
            {
                throw new NotFoundException($"Entity {nameof(Inspection)} by Id not found", nameof(inspection));
            }

            var result = _mapper.Map<Inspection>(inspection);
            return result;
        }

        public Task UpdateAsync(Inspection item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task UpdateInternalAsync(Inspection item)
        {
            var allInspection = await _inspectionRepository.GetAllAsync();
            if (!allInspection.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Inspection)} not found", nameof(allInspection));
            }

            if (item.InspectionDate < DateTimeOffset.Now)
            {
                throw new DateException("Inspection date cannot be in the past", nameof(item));
            }

            if (item.Price < 0)
            {
                throw new ArgumentException("The price must be positive", nameof(item));
            }

            await _inspectionRepository.UpdateAsync(_mapper.Map<InspectionDto>(item));
        }
    }
}
