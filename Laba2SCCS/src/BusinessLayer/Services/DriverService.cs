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
    internal class DriverService : IService<Driver>
    {
        private readonly IGenericRepository<DriverDto> _driverRepository;
        private readonly IMapper _mapper;

        public DriverService(IGenericRepository<DriverDto> driverRepository)
        {
            _driverRepository = driverRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Driver, DriverDto>();
                cfg.CreateMap<DriverDto, Driver>();
            }).CreateMapper();
        }

        public Task<int> Create(Driver item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternal(item);
        }

        private async Task<int> CreateInternal(Driver item)
        {
            if (item.DateOfBirth >= DateTimeOffset.Now)
            {
                throw new DateException("Date of birth cannot be in the past", nameof(item));
            }

            if (item.DateOfRights >= DateTimeOffset.Now)
            {
                throw new DateException("Date of rights cannot be in the past", nameof(item));
            }

            if (item.DateOfRights <= item.DateOfBirth)
            {
                throw new DateException("The date of receipt of rights may not be earlier than the date of birth", nameof(item));
            }

            await _driverRepository.Create(_mapper.Map<DriverDto>(item));

            var allDrivers = await _driverRepository.GetAll();
            var driver = allDrivers.Last();

            return driver.Id;
        }

        public Task Delete(Driver item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternal(item);
        }

        private async Task DeleteInternal(Driver item)
        {
            var allDrivers = await _driverRepository.GetAll();
            if (!allDrivers.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Driver)} not found", nameof(allDrivers));
            }

            await _driverRepository.Delete(_mapper.Map<DriverDto>(item));
        }

        public async Task<IEnumerable<Driver>> GetAll()
        {
            var allDrivers = await _driverRepository.GetAll();
            if (!allDrivers.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Driver)} not found", nameof(allDrivers));
            }

            var result = _mapper.Map<IEnumerable<Driver>>(allDrivers);
            return result;
        }

        public async Task<Driver> GetById(int id)
        {
            var driver = await _driverRepository.GetById(id);
            if (driver == null)
            {
                throw new NotFoundException($"Entity {nameof(Driver)} by Id not found", nameof(driver));
            }

            var result = _mapper.Map<Driver>(driver);
            return result;
        }

        public Task Update(Driver item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternal(item);
        }

        private async Task UpdateInternal(Driver item)
        {
            var allDrivers = await _driverRepository.GetAll();
            if (!allDrivers.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Driver)} not found", nameof(allDrivers));
            }

            if (item.DateOfBirth >= DateTimeOffset.Now)
            {
                throw new DateException("Date of birth cannot be in the past", nameof(item));
            }

            if (item.DateOfRights >= DateTimeOffset.Now)
            {
                throw new DateException("Date of rights cannot be in the past", nameof(item));
            }

            if (item.DateOfRights <= item.DateOfBirth)
            {
                throw new DateException("The date of receipt of rights may not be earlier than the date of birth", nameof(item));
            }

            await _driverRepository.Update(_mapper.Map<DriverDto>(item));
        }
    }
}
