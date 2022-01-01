﻿using DbgCensus.Core.Json;
using DbgCensus.EventStream.Abstractions;
using DbgCensus.EventStream.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IO;
using System;
using System.Net.WebSockets;
using System.Text.Json;

namespace DbgCensus.EventStream.Extensions;

public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds required services for interacting with the Census REST API.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configureClient">The delegate used to construct your <see cref="IEventStreamClient"/> implementation. Requires an options and name parameter for factory injection.</param>
    /// <returns>A reference to this <see cref="IServiceCollection"/> so that calls may be chained.</returns>
    public static IServiceCollection AddCensusEventStreamServices
    (
        this IServiceCollection serviceCollection,
        Func<IServiceProvider, IOptions<EventStreamOptions>, string, IEventStreamClient> configureClient
    )
    {
        serviceCollection.Configure<JsonSerializerOptions>
        (
            Constants.JsonDeserializationOptionsName,
            o =>
            {
                o.AddCensusDeserializationOptions();
                o.Converters.Add(new SubscribeUInt64ListJsonConverter());
                o.Converters.Add(new SubscribeWorldListJsonConverter());
            }
        );

        serviceCollection.Configure<JsonSerializerOptions>
        (
            Constants.JsonSerializationOptionsName,
            o =>
            {
                o.PropertyNamingPolicy = new CamelCaseJsonNamingPolicy();
                o.Converters.Add(new SubscribeUInt64ListJsonConverter());
                o.Converters.Add(new SubscribeWorldListJsonConverter());
            }
        );

        serviceCollection.TryAddSingleton<RecyclableMemoryStreamManager>();
        serviceCollection.TryAddTransient<ClientWebSocket>();

        serviceCollection.TryAddSingleton<IEventStreamClientFactory>
        (
            s => new EventStreamClientFactory
            (
                s.GetRequiredService<IServiceProvider>(),
                s.GetRequiredService<IOptions<EventStreamOptions>>(),
                configureClient
            )
        );

        return serviceCollection;
    }
}
