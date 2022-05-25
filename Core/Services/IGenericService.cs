using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SharedLibrary.Dto;

namespace Core.Services
{
    public interface IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<ResponseDto<TDto>> GetByIdAsync(int id);
        Task<ResponseDto<IEnumerable<TDto>>> GetAll();
        Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<ResponseDto<TDto>> AddAsync(TEntity entity);
        Task<ResponseDto<NoDataDto>> Remove(TEntity entity);
        Task<ResponseDto<NoDataDto>> Update(TEntity entity);
    }
}