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
    internal class InspectorService : IService<Inspector>
    {
        private readonly IGenericRepository<InspectorDto> _inspectorRepository;
        private readonly IMapper _mapper;

        public InspectorService(IGenericRepository<InspectorDto> inspectorRepository)
        {
            _inspectorRepository = inspectorRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Inspector, InspectorDto>();
                cfg.CreateMap<InspectorDto, Inspector>();
            }).CreateMapper();
        }

        public Task<int> Create(Inspector item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternal(item);
        }

        private async Task<int> CreateInternal(Inspector item)
        {
            if (item.PersonalNumber < 0)
            {
                throw new ArgumentException("The personal number must be positive", nameof(item));
            }

            await _inspectorRepository.Create(_mapper.Map<InspectorDto>(item));

            var allInspectors = await _inspectorRepository.GetAll();
            var inspector = allInspectors.Last();

            return inspector.Id;
        }

        public Task Delete(Inspector item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternal(item);
        }

        private async Task DeleteInternal(Inspector item)
        {
            var allInspectors = await _inspectorRepository.GetAll();
            if (!allInspectors.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Inspector)} not found", nameof(allInspectors));
            }

            await _inspectorRepository.Delete(_mapper.Map<InspectorDto>(item));
        }

        public async Task<IEnumerable<Inspector>> GetAll()
        {
            var allInspectors = await _inspectorRepository.GetAll();
            if (!allInspectors.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Inspector)} not found", nameof(allInspectors));
            }

            var result = _mapper.Map<IEnumerable<Inspector>>(allInspectors);
            return result;
        }

        public async Task<Inspector> GetById(int id)
        {
            var inspector = await _inspectorRepository.GetById(id);
            if (inspector == null)
            {
                throw new NotFoundException($"Entity {nameof(Inspector)} by Id not found", nameof(inspector));
            }

            var result = _mapper.Map<Inspector>(inspector);
            return result;
        }

        public Task Update(Inspector item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternal(item);
        }

        private async Task UpdateInternal(Inspector item)
        {
            var allInspectors = await _inspectorRepository.GetAll();
            if (!allInspectors.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Inspector)} not found", nameof(allInspectors));
            }

            if (item.PersonalNumber < 0)
            {
                throw new ArgumentException("The personal number must be positive", nameof(item));
            }

            await _inspectorRepository.Update(_mapper.Map<InspectorDto>(item));
        }
    }
}
