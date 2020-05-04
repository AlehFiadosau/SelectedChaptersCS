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
    internal class InspectionService : IService<Inspection>
    {
        private readonly IGenericRepository<InspectionDto> _inspectionRepository;
        private readonly IMapper _mapper;

        public InspectionService(IGenericRepository<InspectionDto> inspectionRepository)
        {
            _inspectionRepository = inspectionRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Inspection, InspectionDto>();
                cfg.CreateMap<InspectionDto, Inspection>();
            }).CreateMapper();
        }

        public Task<int> Create(Inspection item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternal(item);
        }

        private async Task<int> CreateInternal(Inspection item)
        {
            if (item.InspectionDate < DateTimeOffset.Now)
            {
                throw new DateException("Inspection date cannot be in the past", nameof(item));
            }

            if (item.Price < 0)
            {
                throw new ArgumentException("The price must be positive", nameof(item));
            }

            await _inspectionRepository.Create(_mapper.Map<InspectionDto>(item));

            var allInspection = await _inspectionRepository.GetAll();
            var inspection = allInspection.Last();

            return inspection.Id;
        }

        public Task Delete(Inspection item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternal(item);
        }

        private async Task DeleteInternal(Inspection item)
        {
            var allInspection = await _inspectionRepository.GetAll();
            if (!allInspection.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Inspection)} not found", nameof(allInspection));
            }

            await _inspectionRepository.Delete(_mapper.Map<InspectionDto>(item));
        }

        public async Task<IEnumerable<Inspection>> GetAll()
        {
            var allInspection = await _inspectionRepository.GetAll();
            if (!allInspection.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Inspection)} not found", nameof(allInspection));
            }

            var result = _mapper.Map<IEnumerable<Inspection>>(allInspection);
            return result;
        }

        public async Task<Inspection> GetById(int id)
        {
            var inspection = await _inspectionRepository.GetById(id);
            if (inspection == null)
            {
                throw new NotFoundException($"Entity {nameof(Inspection)} by Id not found", nameof(inspection));
            }

            var result = _mapper.Map<Inspection>(inspection);
            return result;
        }

        public Task Update(Inspection item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternal(item);
        }

        private async Task UpdateInternal(Inspection item)
        {
            var allInspection = await _inspectionRepository.GetAll();
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

            await _inspectionRepository.Update(_mapper.Map<InspectionDto>(item));
        }
    }
}
