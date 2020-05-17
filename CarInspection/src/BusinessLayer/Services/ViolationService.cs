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
    internal class ViolationService : IService<Violation, int>
    {
        private readonly IGenericRepository<ViolationDto, int> _violantionsRepository;
        private readonly IMapper _mapper;

        public ViolationService(IGenericRepository<ViolationDto, int> violantionsRepository, IMapper mapper)
        {
            _violantionsRepository = violantionsRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(Violation item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(Violation item)
        {
            
            await _violantionsRepository.CreateAsync(_mapper.Map<ViolationDto>(item));

            var allViolations = await _violantionsRepository.GetAllAsync();
            var violation = allViolations.Last();

            return violation.Id;
        }

        public Task DeleteAsync(Violation item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        private async Task DeleteInternalAsync(Violation item)
        {
            var allViolations = await _violantionsRepository.GetAllAsync();
            if (!allViolations.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violation)} not found", nameof(allViolations));
            }

            await _violantionsRepository.DeleteAsync(_mapper.Map<ViolationDto>(item));
        }

        public async Task<List<Violation>> GetAllAsync()
        {
            var allViolations = await _violantionsRepository.GetAllAsync();
            if (!allViolations.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violation)} not found", nameof(allViolations));
            }

            var result = _mapper.Map<List<Violation>>(allViolations);
            return result;
        }

        public async Task<Violation> GetByIdAsync(int id)
        {
            var violation = await _violantionsRepository.GetByIdAsync(id);
            if (violation == null)
            {
                throw new NotFoundException($"Entity {nameof(Violation)} by Id not found", nameof(violation));
            }

            var result = _mapper.Map<Violation>(violation);
            return result;
        }

        public Task UpdateAsync(Violation item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task UpdateInternalAsync(Violation item)
        {
            var allViolations = await _violantionsRepository.GetAllAsync();
            if (!allViolations.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violation)} not found", nameof(allViolations));
            }

            await _violantionsRepository.UpdateAsync(_mapper.Map<ViolationDto>(item));
        }
    }
}
