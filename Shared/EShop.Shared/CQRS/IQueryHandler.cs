﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.CQRS
{
    public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, Unit>
      where TQuery : IQuery<Unit>
    {
    }
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    {
    }
}
