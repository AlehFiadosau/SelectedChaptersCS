using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    internal class DriverService : IService<Driver, int>
    {
        private readonly IGenericRepository<DriverDto, int> _driverRepository;
        private readonly IMapper _mapper;

        public DriverService(IGenericRepository<DriverDto, int> driverRepository,
            IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(Driver item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(Driver item)
        {
            if (item.DateOfBirth >= DateTimeOffset.Now)
            {
                throw new DateException("Date of birth cannot be in the present or future tense", nameof(item));
            }

            if (item.DateOfRights >= DateTimeOffset.Now)
            {
                throw new DateException("Date of rights cannot be in the present or future tense", nameof(item));
            }

            if (item.DateOfRights <= item.DateOfBirth)
            {
                throw new DateException("The date of receipt of rights may not be earlier than the date of birth", nameof(item));
            }

            await _driverRepository.CreateAsync(_mapper.Map<DriverDto>(item));

            var allDrivers = await _driverRepository.GetAllAsync();
            var driver = allDrivers.Last();

            return driver.Id;
        }

        public Task DeleteAsync(Driver item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        private async Task DeleteInternalAsync(Driver item)
        {
            var allDrivers = await _driverRepository.GetAllAsync();
            if (!allDrivers.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Driver)} not found", nameof(allDrivers));
            }

            await _driverRepository.DeleteAsync(_mapper.Map<DriverDto>(item));
        }

        public async Task<List<Driver>> GetAllAsync()
        {
            var allDrivers = await _driverRepository.GetAllAsync();
            if (!allDrivers.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Driver)} not found", nameof(allDrivers));
            }

            var result = _mapper.Map<List<Driver>>(allDrivers);
            return result;
        }

        public async Task<Driver> GetByIdAsync(int id)
        {
            var driver = await _driverRepository.GetByIdAsync(id);
            if (driver == null)
            {
                throw new NotFoundException($"Entity {nameof(Driver)} by Id not found", nameof(driver));
            }

            var result = _mapper.Map<Driver>(driver);
            return result;
        }

        public Task UpdateAsync(Driver item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task UpdateInternalAsync(Driver item)
        {
            var allDrivers = await _driverRepository.GetAllAsync();
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

            await _driverRepository.UpdateAsync(_mapper.Map<DriverDto>(item));
        }
    }
}
