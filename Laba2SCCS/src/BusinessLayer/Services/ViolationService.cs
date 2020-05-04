using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Infrastructe;
using BusinessLayer.Interfaces;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    internal class ViolationService : IService<Violation>
    {
        private readonly IGenericRepository<ViolationDto> _violantionsRepository;
        private readonly IMapper _mapper;

        public ViolationService(IGenericRepository<ViolationDto> violantionsRepository)
        {
            _violantionsRepository = violantionsRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Violation, ViolationDto>();
                cfg.CreateMap<ViolationDto, Violation>();
            }).CreateMapper();
        }

        public Task<int> Create(Violation item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternal(item);
        }

        private async Task<int> CreateInternal(Violation item)
        {
            
            await _violantionsRepository.Create(_mapper.Map<ViolationDto>(item));

            var allViolations = await _violantionsRepository.GetAll();
            var violation = allViolations.Last();

            return violation.Id;
        }

        public Task Delete(Violation item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternal(item);
        }

        private async Task DeleteInternal(Violation item)
        {
            var allViolations = await _violantionsRepository.GetAll();
            if (!allViolations.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violation)} not found", nameof(allViolations));
            }

            await _violantionsRepository.Delete(_mapper.Map<ViolationDto>(item));
        }

        public async Task<IEnumerable<Violation>> GetAll()
        {
            var allViolations = await _violantionsRepository.GetAll();
            if (!allViolations.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violation)} not found", nameof(allViolations));
            }

            var result = _mapper.Map<IEnumerable<Violation>>(allViolations);
            return result;
        }

        public async Task<Violation> GetById(int id)
        {
            var violation = await _violantionsRepository.GetById(id);
            if (violation == null)
            {
                throw new NotFoundException($"Entity {nameof(Violation)} by Id not found", nameof(violation));
            }

            var result = _mapper.Map<Violation>(violation);
            return result;
        }

        public Task Update(Violation item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternal(item);
        }

        private async Task UpdateInternal(Violation item)
        {
            var allViolations = await _violantionsRepository.GetAll();
            if (!allViolations.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violation)} not found", nameof(allViolations));
            }

            await _violantionsRepository.Update(_mapper.Map<ViolationDto>(item));
        }
    }
}
