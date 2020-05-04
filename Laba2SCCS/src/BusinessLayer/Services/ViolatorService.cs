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
    internal class ViolatorService : IService<Violator>
    {
        private readonly IGenericRepository<ViolatorDto> _violatorRepository;
        private readonly IMapper _mapper;

        public ViolatorService(IGenericRepository<ViolatorDto> violatorRepository)
        {
            _violatorRepository = violatorRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Violator, ViolatorDto>();
                cfg.CreateMap<ViolatorDto, Violator>();
            }).CreateMapper();
        }

        public Task<int> Create(Violator item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternal(item);
        }

        private async Task<int> CreateInternal(Violator item)
        {
            if (item.ReinspectionDate < DateTimeOffset.Now)
            {
                throw new DateException("Reinspection date cannot be in the past", nameof(item));
            }

            await _violatorRepository.Create(_mapper.Map<ViolatorDto>(item));

            var allViolators = await _violatorRepository.GetAll();
            var violator = allViolators.Last();

            return violator.Id;
        }

        public Task Delete(Violator item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternal(item);
        }

        private async Task DeleteInternal(Violator item)
        {
            var allViolators = await _violatorRepository.GetAll();
            if (!allViolators.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violator)} not found", nameof(allViolators));
            }

            await _violatorRepository.Delete(_mapper.Map<ViolatorDto>(item));
        }

        public async Task<IEnumerable<Violator>> GetAll()
        {
            var allViolators = await _violatorRepository.GetAll();
            if (!allViolators.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violator)} not found", nameof(allViolators));
            }

            var result = _mapper.Map<IEnumerable<Violator>>(allViolators);
            return result;
        }

        public async Task<Violator> GetById(int id)
        {
            var violator = await _violatorRepository.GetById(id);
            if (violator == null)
            {
                throw new NotFoundException($"Entity {nameof(Violator)} by Id not found", nameof(violator));
            }

            var result = _mapper.Map<Violator>(violator);
            return result;
        }

        public Task Update(Violator item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternal(item);
        }

        private async Task UpdateInternal(Violator item)
        {
            var allViolators = await _violatorRepository.GetAll();
            if (!allViolators.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Violator)} not found", nameof(allViolators));
            }

            if (item.ReinspectionDate < DateTimeOffset.Now)
            {
                throw new DateException("Reinspection date cannot be in the past", nameof(item));
            }

            await _violatorRepository.Update(_mapper.Map<ViolatorDto>(item));
        }
    }
}
