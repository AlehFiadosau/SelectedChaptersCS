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
    internal class ViolatorService : IService<Violator, int>
    {
        private readonly IGenericRepository<ViolatorDto, int> _violatorRepository;
        private readonly IMapper _mapper;

        public ViolatorService(IGenericRepository<ViolatorDto, int> violatorRepository, IMapper mapper)
        {
            _violatorRepository = violatorRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(Violator item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(Violator item)
        {
            if (item.ReinspectionDate < DateTimeOffset.Now)
            {
                throw new DateException("Reinspection date cannot be in the past", nameof(item));
            }

            await _violatorRepository.CreateAsync(_mapper.Map<ViolatorDto>(item));

            var allViolators = await _violatorRepository.GetAllAsync();
            var violator = allViolators.Last();

            return violator.Id;
        }

        public Task DeleteAsync(Violator item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        private async Task DeleteInternalAsync(Violator item)
        {
            var allViolators = await _violatorRepository.GetAllAsync();
            if (!allViolators.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violator)} not found", nameof(allViolators));
            }

            await _violatorRepository.DeleteAsync(_mapper.Map<ViolatorDto>(item));
        }

        public async Task<List<Violator>> GetAllAsync()
        {
            var allViolators = await _violatorRepository.GetAllAsync();
            if (!allViolators.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violator)} not found", nameof(allViolators));
            }

            var result = _mapper.Map<List<Violator>>(allViolators);
            return result;
        }

        public async Task<Violator> GetByIdAsync(int id)
        {
            var violator = await _violatorRepository.GetByIdAsync(id);
            if (violator == null)
            {
                throw new NotFoundException($"Entity {nameof(Violator)} by Id not found", nameof(violator));
            }

            var result = _mapper.Map<Violator>(violator);
            return result;
        }

        public Task UpdateAsync(Violator item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task UpdateInternalAsync(Violator item)
        {
            var allViolators = await _violatorRepository.GetAllAsync();
            if (!allViolators.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violator)} not found", nameof(allViolators));
            }

            if (item.ReinspectionDate < DateTimeOffset.Now)
            {
                throw new DateException("Reinspection date cannot be in the past", nameof(item));
            }

            await _violatorRepository.UpdateAsync(_mapper.Map<ViolatorDto>(item));
        }
    }
}
