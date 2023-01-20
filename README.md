# Aplicando o conceito de Caching em um projeto .NET

## Recursos utilizados

- Visual Studio 2022
- .NET Core 5.0
- Microsoft.Extensions.Caching.Memory

## Versão do Projeto

- v1.0.2, 2023-01-20, 19:33 | ApiCache ( a versão não possui Views)


## O que é Caching?

Caching é uma técnica de otimização de desempenho que consiste em armazenar em memória dados que são frequentemente utilizados, para que não seja necessário acessar o banco de dados toda vez que for necessário recuperar esses dados.

## Por que utilizar Caching?

O Caching é uma técnica de otimização de desempenho que consiste em armazenar em memória dados que são frequentemente utilizados, para que não seja necessário acessar o banco de dados toda vez que for necessário recuperar esses dados.

## Como utilizar Caching?

Para utilizar o Caching em um projeto .NET, é necessário adicionar a referência do pacote Microsoft.Extensions.Caching.Memory.

## Exemplo de utilização

### Adicionando o pacote

Para adicionar o pacote, basta executar o seguinte comando no Package Manager Console:

```powershell
Install-Package Microsoft.Extensions.Caching.Memory
```

### Criando a classe de Caching

```csharp

using Microsoft.Extensions.Caching.Memory;
using System;

namespace Caching
{
    public class Cache
    {
        private static IMemoryCache _cache;

        public Cache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public static void Set(string key, object value)
        {
            _cache.Set(key, value);
        }

        public static T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public static void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}

```

### Utilizando a classe de Caching

```csharp

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public ValuesController(IMemoryCache cache)
        {
            _cache = cache;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var cache = new Cache(_cache);

            var values = cache.Get<IEnumerable<string>>("values");

            if (values == null)
            {
                values = new string[] { "value1", "value2" };
                cache.Set("values", values);
            }

            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

```


### Versão do Prejto e do .NET Core

```powershell
dotnet --version
```

```powershell
5.0.400
```

```powershell
dotnet --info
```

```powershell
.NET SDK (reflecting any global.json):
 Version:   5.0.400
 Commit:    5d8b0fbbdb

Runtime Environment:
    OS Name:     Windows
    OS Version:  10.0.19043
    OS Platform: Windows
    RID:         win10-x64
    Base Path:   C:\Program Files\dotnet\sdk\5.0.400\

Host (useful for support):
    Version: 5.0.9
    Commit:  0d0c902b77

.NET SDKs installed:
    5.0.400 [C:\Program Files\dotnet\sdk]

.NET runtimes installed:

    Microsoft.AspNetCore.App 5.0.9 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
    Microsoft.NETCore.App 5.0.9 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]

To install additional .NET runtimes or SDKs:

    https://aka.ms/dotnet-download
```




## Referências

[https://docs.microsoft.com/pt-br/aspnet/core/performance/caching/memory?view=aspnetcore-2.2](https://docs.microsoft.com/pt-br/aspnet/core/performance/caching/memory?view=aspnetcore-2.2)
[https://docs.microsoft.com/pt-br/aspnet/core/performance/caching/memory?view=aspnetcore-2.2#distributed-memory-cache](https://docs.microsoft.com/pt-br/aspnet/core/performance/caching/memory?view=aspnetcore-2.2#distributed-memory-cache)
