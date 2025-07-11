﻿global using FluentValidation;
global using EShop.Basket.Basket.Models;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using EShop.Shared.Contract.CQRS;
global using EShop.Basket.Basket.Dtos;
global using EShop.Basket.Basket.Exceptions;
global using EShop.Basket.Data;
global using Mapster;
global using Carter;
global using MediatR;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Routing;
global using Catalog.Contract.Products.Features.GetProductById;