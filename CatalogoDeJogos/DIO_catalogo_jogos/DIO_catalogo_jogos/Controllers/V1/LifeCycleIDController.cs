using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIO_catalogo_jogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class LifeCycleIDController
    {
        public readonly ISingletonExample _singletonExample1;
        public readonly ISingletonExample _singletonExample2;

        public readonly IScopedExample _scopedExample1;
        public readonly IScopedExample _scopedExample2;

        public readonly ITransientExample _transientExample1;
        public readonly ITransientExample _transientExample2;

        public LifeCycleIDController(ISingletonExample exemploSingleton1,
                                     ISingletonExample exemploSingleton2,
                                     IScopedExample exemploScoped1,
                                     IScopedExample exemploScoped2,
                                     ITransientExample exemploTransient1,
                                     ITransientExample exemploTransient2)
        {
            _singletonExample1 = exemploSingleton1;
            _singletonExample2 = exemploSingleton2;
            _scopedExample1 = exemploScoped1;
            _scopedExample2 = exemploScoped2;
            _transientExample1 = exemploTransient1;
            _transientExample2 = exemploTransient2;
        }

        [HttpGet]
        public Task<string> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Singleton 1: {_singletonExample1.Id}");
            stringBuilder.AppendLine($"Singleton 2: {_singletonExample2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Scoped 1: {_scopedExample1.Id}");
            stringBuilder.AppendLine($"Scoped 2: {_scopedExample2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Transient 1: {_transientExample1.Id}");
            stringBuilder.AppendLine($"Transient 2: {_transientExample2.Id}");

            return Task.FromResult(stringBuilder.ToString());
        }
    }

    public interface IGeneralExample
    {
        public Guid Id { get; }
    }

    public interface ISingletonExample : IGeneralExample
    { }

    public interface IScopedExample : IGeneralExample
    { }

    public interface ITransientExample : IGeneralExample
    { }

    public class LifeCycleExample: ISingletonExample, IScopedExample, ITransientExample
    {
        private readonly Guid _guid;

        public LifeCycleExample()
        {
            _guid = Guid.NewGuid();
        }

        public Guid Id => _guid;
    }
}
