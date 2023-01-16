using AutoMapper;
using DataAccess;
using Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services
{
    public class GenericServiceAsync<Entity, Response, Request> : IGenericServiceAsync<Entity, Response, Request>
        where Entity : EntityBase 
        where Response : class
    {
        private readonly IGenericRepositoryAsync<Entity> _genericRepositoryAsync;
        private readonly IMapper _mapper;

        public GenericServiceAsync(IGenericRepositoryAsync<Entity> genericRepositoryAsync, IMapper mapper)
        {
            _genericRepositoryAsync = genericRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(Request request)
        {
            var entityModel = _mapper.Map<Entity>(request);
            return await _genericRepositoryAsync.CreateAsync(entityModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _genericRepositoryAsync.DeleteAsync(id);
        }

        public async Task DeleteRange(IEnumerable<Request> elements)
        {
            await _genericRepositoryAsync.DeleteRange(_mapper.Map<IEnumerable<Request>, ICollection<Entity>>(elements));
        }

        public async Task<Response> GetByIdAsync(int id)
        {
            var customer = await _genericRepositoryAsync.GetAsync(id);

            if (customer == null)
                return null;

            return _mapper.Map<Response>(customer);
        }

        public async Task<ICollection<Response>> GetFilter(Expression<Func<Entity, bool>> predicate)
        {
            var collection = await _genericRepositoryAsync.GetFilter(predicate);
            return _mapper.Map<ICollection<Entity>, ICollection<Response>>(collection);
        }

        public async Task<ICollection<Response>> ListAsync()
        {
            var collection = await _genericRepositoryAsync.ListCollectionAsync();
            return _mapper.Map<ICollection<Entity>, ICollection<Response>>(collection);
        }

        public async Task<int> UpdateAsync(int id, Request request)
        {
            var entityModel = _mapper.Map<Entity>(request);
            entityModel.Id = id;
            return await _genericRepositoryAsync.UpdateAsync(entityModel);
        }
    }
}
