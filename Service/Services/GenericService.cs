using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.MappingProfile;
using SharedLibrary.Dto;

namespace Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IUnitOfWork uniOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _uniOfWork = uniOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<ResponseDto<TDto>> GetByIdAsync(int id)
        {
            var product = await _genericRepository.GetByIdAsync(id);
            if (product == null)
            {
                return ResponseDto<TDto>.Fail("Id not found", 404, true);
            }

            return ResponseDto<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(product), 200);
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
            return ResponseDto<IEnumerable<TDto>>.Success(products, 200);
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _genericRepository.Where(predicate);
            return ResponseDto<IEnumerable<TDto>>.Success(
                ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }

        public async Task<ResponseDto<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _genericRepository.AddAsync(newEntity);
            await _uniOfWork.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            return ResponseDto<TDto>.Success(newDto, 200);
        }

        public async Task<ResponseDto<NoDataDto>> Remove(int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);
            }

            _genericRepository.Remove(isExistEntity);
            await _uniOfWork.CommitAsync();
            return ResponseDto<NoDataDto>.Success(204);
        }

        public async Task<ResponseDto<NoDataDto>> Update(TDto entity, int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            _genericRepository.Update(updateEntity);
            await _uniOfWork.CommitAsync();
            return ResponseDto<NoDataDto>.Success(204);
        }
    }
}